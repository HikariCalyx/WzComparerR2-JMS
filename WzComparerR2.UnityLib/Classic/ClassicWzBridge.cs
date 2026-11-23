using System;
using System.Collections.Generic;
using System.IO;
using WzComparerR2.UnityLib.Unity;
using WzComparerR2.UnityLib.Wzjs;
using WzComparerR2.WzLib;

namespace WzComparerR2.UnityLib.Classic
{
    /// <summary>
    /// 非 WZ ファイルソース（Unity アセット）の img 基底クラス: <see cref="Wz_Image"/> を継承して、WZ 型しか扱わない
    /// <c>Wz_Image</c> しか扱わない既存コード（MapRender など）からそのまま利用できます。
    /// <para>
    /// <see cref="Wz_Image.TryExtract(out Exception)"/> はアセット内容をその場で WZ ノードに変換します
    /// （<see cref="Wz_Image.Node"/>）。ノード表示名は <c>&lt;名前&gt;.img</c> の従来の慣例に従います。
    /// </para>
    /// </summary>
    public abstract class UnityAssetImage : Wz_Image
    {
        private readonly object extractLock = new object();
        private Wz_Node rootedNode;
        private readonly object rootedLock = new object();
        private bool extracted;

        protected UnityAssetImage(ClassicWzSource source, string imageKey)
            : base(imageKey, GetTrailingName(imageKey) + ".img")
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (imageKey == null)
            {
                throw new ArgumentNullException(nameof(imageKey));
            }
            this.Source = source;
        }

        /// <summary>img の論理パス（例: <c>Character/00002000</c>、<c>Sound/Bgm00</c>）。</summary>
        public string ImageKey
        {
            get { return this.Name; }
        }

        /// <summary>所属データセット。</summary>
        public ClassicWzSource Source { get; private set; }

        /// <summary>アセット内容を <see cref="Wz_Image.Node"/> に格納します（1 回だけ呼ばれます）。</summary>
        protected abstract void PopulateContent();

        /// <summary>内容が既に展開済みか（比較ツールが自分で解放すべきかの判断用）。</summary>
        public bool IsExtracted
        {
            get { return this.extracted; }
        }

        /// <summary>アセット内容を WZ ノードとして展開します（複数回呼び出し可能）。</summary>
        public override bool TryExtract(out Exception e)
        {
            try
            {
                this.EnsureExtracted();
                e = null;
                return true;
            }
            catch (Exception ex)
            {
                e = ex;
                return false;
            }
        }

        private void EnsureExtracted()
        {
            if (this.extracted)
            {
                return;
            }
            lock (this.extractLock)
            {
                if (this.extracted)
                {
                    return;
                }
                this.PopulateContent();
                this.extracted = true;
            }
        }

        /// <summary>WZ ノードツリーとして内容を取得します（初回呼び出し時に変換してキャッシュ）。</summary>
        public Wz_Node AsWzNode()
        {
            this.EnsureExtracted();
            return this.Node;
        }

        /// <summary>
        /// 展開済みの内容を解放します（比較ツールが毎回の比較後に呼び出します）。次に <see cref="TryExtract(out Exception)"/> を呼ぶと
        /// 次回はアセットから再度変換します。アセット自体はデータを保持しているため、繰り返し展開できます。
        /// </summary>
        public override void Unextract()
        {
            lock (this.extractLock)
            {
                if (!this.extracted)
                {
                    return;
                }
                this.extracted = false;
                this.Node.Nodes.Clear();
            }
        }

        /// <summary>
        /// 内容ルートノード。必要に応じてディレクトリと同名の上位ノードチェーンを補います
        /// （例: <c>Character\Weapon\01442000\...</c>）に補うことで、
        /// <see cref="Wz_Node.FullPathToFile"/> などのパス判定を WZ のロジックのまま利用できます。
        /// </summary>
        public Wz_Node GetRootedNode()
        {
            Wz_Node content = this.AsWzNode();
            if (content.ParentNode != null)
            {
                return content;
            }
            lock (rootedLock)
            {
                if (this.rootedNode == null)
                {
                    this.rootedNode = CreateRootedTree(this.Node, this.Name);
                    if (this.rootedNode != null)
                    {
                        this.OwnerNode = this.Node.ParentNode;
                    }
                }
            }
            return content;
        }

        /// <summary>
        /// 内容ノードに従来の WZ と同じ上位チェーンを作成します: ディレクトリノード + 同名の <c>&lt;img&gt;.img</c> ノード
        /// （内容ノードはその配下に付きます）。これにより <see cref="Wz_Node.FullPathToFile"/> が
        /// <c>Sound\Bgm00.img\FloralLife</c> のようなパスになります。
        /// </summary>
        private static Wz_Node CreateRootedTree(Wz_Node content, string imageKey)
        {
            string[] segments = imageKey.Split('/');
            if (segments.Length < 2)
            {
                return null;
            }

            Wz_Node root = new Wz_Node(segments[0]);
            Wz_Node parent = root;
            for (int i = 1; i < segments.Length - 1; i++)
            {
                Wz_Node folder = new Wz_Node(segments[i]);
                parent.Nodes.Add(folder);
                parent = folder;
            }

            Wz_Node imageNode = new Wz_Node(content.Text);
            imageNode.Value = content.GetValue<Wz_Image>();
            parent.Nodes.Add(imageNode);
            imageNode.Nodes.Add(content);
            return root;
        }

        private static string GetTrailingName(string name)
        {
            int slash = name.LastIndexOf('/');
            return slash < 0 ? name : name.Substring(slash + 1);
        }
    }

    /// <summary>
    /// 従来の WZ の <see cref="Wz_Node"/> 形式で 1 つの WZJS img を表します。
    /// <para>
    /// Canvas 値は <see cref="UnityCanvas"/> で、同時に <see cref="Wz_Png"/> でもあり、
    /// 画像が必要になった時点でデコードします。
    /// </para>
    /// </summary>
    public sealed class UnityWzImage : UnityAssetImage
    {
        public UnityWzImage(ClassicWzSource source, string name)
            : base(source, name)
        {
        }

        public WzjsNode Root
        {
            get { return this.Source.GetTree(this.Name); }
        }

        /// <summary>取 img 內某節點的 PNG bytes；查無圖時回 null。</summary>
        public byte[] GetPng(string innerPath)
        {
            string path = string.IsNullOrEmpty(innerPath) ? this.Name : this.Name + "/" + innerPath;
            return this.Source.GetImage(path);
        }

        /// <summary>WZ 形式のパスで同じデータセット内のノードを検索します（<c>.img</c> 拡張子は任意）。</summary>
        public Wz_Node FindNode(string wzPath)
        {
            return this.Source.FindNode(wzPath);
        }

        protected override void PopulateContent()
        {
            WzjsNode tree = this.Source.GetTree(this.Name);
            foreach (WzjsNode child in tree.Children)
            {
                this.Node.Nodes.Add(ClassicWzBridge.ConvertNode(
                    child, this.Name, this.Source));
            }
        }
    }

    /// <summary>
    /// Unity 資產樹 → WzComparerR2 <see cref="Wz_Node"/> 的橋接。
    /// </summary>
    public static class ClassicWzBridge
    {
        /// <summary>把一個 WZJS 節點（含子樹）轉成 WzLib 節點。</summary>
        public static Wz_Node ConvertNode(WzjsNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            Wz_Node root = new Wz_Node(node.Name);
            root.Value = ConvertValue(node.Value);
            foreach (WzjsNode child in node.Children)
            {
                root.Nodes.Add(ConvertNode(child));
            }
            return root;
        }

        /// <summary>建立單一 img 的 WZ 節點（含已解碼子樹與 Canvas PNG）。</summary>
        public static Wz_Node CreateImageNode(UnityWzImage image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            return CreateImageNode(image, image.Name);
        }

        /// <summary>
        /// 以 catalog 建出整棵「類 WZ」目錄樹：中繼節點是目錄，末端節點是 img
        /// （Value 為 <see cref="UnityWzImage"/>）、JSON（<see cref="UnityJsonAsset"/>）
        /// 或 AudioClip 資訊。
        /// </summary>
        public static Wz_Node CreateCatalogNode(ClassicWzSource source, bool includeTrees, bool includeImages)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            string rootName = source.ClientInfo?.DisplayName;
            if (string.IsNullOrEmpty(rootName))
            {
                rootName = Path.GetFileName(
                    source.Catalog.RootPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            }
            if (string.IsNullOrEmpty(rootName))
            {
                rootName = "MapleStory Classic";
            }

            Wz_Node root = new Wz_Node(rootName);
            Dictionary<string, Wz_Node> nodes = new Dictionary<string, Wz_Node>(StringComparer.Ordinal);
            nodes[string.Empty] = root;

            foreach (KeyValuePair<string, UnityEntryLocation> entry in source.Catalog.Entries)
            {
                string[] segments = entry.Key.Split('/');
                string currentPath = string.Empty;
                Wz_Node current = root;
                for (int i = 0; i < segments.Length; i++)
                {
                    currentPath = currentPath.Length == 0 ? segments[i] : currentPath + "/" + segments[i];
                    Wz_Node child;
                    if (!nodes.TryGetValue(currentPath, out child))
                    {
                        child = current.Nodes.Add(segments[i]);
                        nodes[currentPath] = child;
                    }
                    current = child;
                }

                switch (entry.Value.ClassId)
                {
                    case UnityClassIds.TextAsset:
                        current.Value = new UnityJsonAsset(source, entry.Key);
                        if (includeTrees)
                        {
                            try
                            {
                                UnityJson.Populate(current, source.ReadTextAsset(entry.Key).Script);
                            }
                            catch (Exception ex) when (ex is UnityException || ex is ClassicAssetException)
                            {
                                current.Nodes.Clear();
                            }
                        }
                        break;

                    case UnityClassIds.AudioClip:
                        if (source.Catalog.IsSoundImage(entry.Key))
                        {
                            // WZ のサウンドは img（Sound/Bgm00.img）配下のノードなので、同じ形で表現します。
                            current.Value = source.GetSoundImage(entry.Key);
                        }
                        else
                        {
                            try
                            {
                                current.Value = source.GetAudioClip(entry.Key);
                            }
                            catch (Exception ex) when (ex is UnityException || ex is ClassicAssetException)
                            {
                                current.Value = null;
                            }
                        }
                        break;

                    case UnityClassIds.MonoBehaviour:
                        current.Value = new UnityWzImage(source, entry.Key);
                        if (includeTrees)
                        {
                            try
                            {
                                WzjsNode tree = source.GetTree(entry.Key);
                                foreach (WzjsNode child in tree.Children)
                                {
                                    current.Nodes.Add(ConvertNode(
                                        child, entry.Key, includeImages ? source : null));
                                }
                            }
                            catch (Exception ex) when (ex is UnityException || ex is WzjsException || ex is ClassicAssetException)
                            {
                                current.Nodes.Clear();
                            }
                        }
                        break;

                    case UnityClassIds.Sprite:
                        // spriteset（SpriteSet/<region>/UI/…）の単一 sprite: デコード時にロケールに応じて画像を取得します。
                        current.Value = new UnityCanvas(source, entry.Key);
                        break;
                }
            }
            return root;
        }

        /// <summary>
        /// 把某個 catalog 資產的內容（WZJS 子樹或 JSON 屬性）附加到
        /// <paramref name="target"/> 底下。
        /// </summary>
        public static void AppendEntryContents(Wz_Node target, ClassicWzSource source, string imageKey)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            UnityEntryLocation location;
            if (!source.Catalog.Entries.TryGetValue(imageKey, out location))
            {
                return;
            }
            switch (location.ClassId)
            {
                case UnityClassIds.TextAsset:
                    UnityJson.Populate(target, source.ReadTextAsset(imageKey).Script);
                    break;

                case UnityClassIds.MonoBehaviour:
                    foreach (WzjsNode child in source.GetTree(imageKey).Children)
                    {
                        target.Nodes.Add(ConvertNode(child, imageKey, source));
                    }
                    break;

                case UnityClassIds.AudioClip:
                    if (source.Catalog.IsSoundImage(imageKey))
                    {
                        UnitySoundImage soundImage = source.GetSoundImage(imageKey);
                        foreach (Wz_Node child in soundImage.AsWzNode().Nodes)
                        {
                            target.Nodes.Add(child);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// アクションフレーム遅延の合計（各フレームの <c>delay</c> の絶対値を加算）。
        /// null または delay がない場合は 0 を返します。
        /// </summary>
        public static int SumActionDelay(Wz_Node actionNode)
        {
            if (actionNode == null)
            {
                return 0;
            }
            int delay = 0;
            foreach (Wz_Node frameNode in actionNode.Nodes)
            {
                Wz_Node delayNode = frameNode.FindNodeByPath("delay");
                if (delayNode != null && delayNode.Value != null)
                {
                    delay += Math.Abs(delayNode.GetValueEx<int>(0));
                }
            }
            return delay;
        }

        /// <summary>把 WZJS 節點值轉成 WzLib 慣用的值型別。</summary>
        public static object ConvertValue(WzjsValue value)        {
            switch (value.Kind)
            {
                case WzjsValueKind.U8:
                case WzjsValueKind.U16:
                case WzjsValueKind.Int:
                    return (int)value.Integer;
                case WzjsValueKind.Long:
                    return value.Integer;
                case WzjsValueKind.Double:
                    return value.Real;
                case WzjsValueKind.Float2:
                    return new[] { value.X, value.Y };
                case WzjsValueKind.Int2:
                    return new[] { value.Int2A, value.Int2B };
                case WzjsValueKind.Vector:
                    return new Wz_Vector(
                        (int)Math.Round(value.X), (int)Math.Round(value.Y));
                case WzjsValueKind.String:
                    return value.Text;
                case WzjsValueKind.Uol:
                    return new Wz_Uol(value.Text);
                default:
                    return null;
            }
        }

        private static Wz_Node CreateImageNode(UnityWzImage image, string imageKey)
        {
            WzjsNode tree = image.Root;
            Wz_Node root = new Wz_Node(tree.Name);
            root.Value = image;
            foreach (WzjsNode child in tree.Children)
            {
                root.Nodes.Add(ConvertNode(child, imageKey, image.Source));
            }
            return root;
        }

        internal static Wz_Node ConvertNode(WzjsNode node, string imageKey, ClassicWzSource imageSource)
        {
            return ConvertNode(node, imageKey, string.Empty, imageSource);
        }

        /// <summary>
        /// 轉換單一節點（含相對路徑追蹤），讓 Canvas 能查到對應 PNG。
        /// </summary>
        private static Wz_Node ConvertNode(WzjsNode node, string imageKey, string parentPath, ClassicWzSource imageSource)
        {
            string path = parentPath.Length == 0 ? node.Name : parentPath + "/" + node.Name;
            Wz_Node result = new Wz_Node(node.Name);
            result.Value = ConvertNodeValue(node, imageKey, path, imageSource);
            foreach (WzjsNode child in node.Children)
            {
                result.Nodes.Add(ConvertNode(child, imageKey, path, imageSource));
            }
            return result;
        }

        private static object ConvertNodeValue(
            WzjsNode node, string imageKey, string innerPath, ClassicWzSource imageSource)
        {
            if (node.IsCanvas)
            {
                if (imageSource == null)
                {
                    return null;
                }
                string fullPath = innerPath.Length == 0 ? imageKey : imageKey + "/" + innerPath;
                return new UnityCanvas(imageSource, fullPath);
            }
            return ConvertValue(node.Value);
        }
    }
}
