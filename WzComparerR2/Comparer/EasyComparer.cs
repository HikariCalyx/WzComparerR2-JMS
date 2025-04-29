﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using WzComparerR2.WzLib;
using WzComparerR2.Common;
using WzComparerR2.PluginBase;
using WzComparerR2.CharaSimControl;
using WzComparerR2.CharaSim;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace WzComparerR2.Comparer
{
    public class EasyComparer
    {
        public EasyComparer()
        {
            this.Comparer = new WzFileComparer();
        }
        private Wz_Node[] WzNewOld { get; set; } = new Wz_Node[2];
        private Wz_File[] WzFileNewOld { get; set; } = new Wz_File[2];
        private Wz_File[] StringWzNewOld { get; set; } = new Wz_File[2];
        private Wz_File[] ItemWzNewOld { get; set; } = new Wz_File[2];
        private Wz_File[] EtcWzNewOld { get; set; } = new Wz_File[2];
        private List<string> OutputSkillTooltipIDs { get; set; } = new List<string>();
        private List<string> OutputCashTooltipIDs { get; set; } = new List<string>();
        private List<string> OutputGearTooltipIDs { get; set; } = new List<string>();
        private List<string> OutputItemTooltipIDs { get; set; } = new List<string>();
        private List<string> OutputMobTooltipIDs { get; set; } = new List<string>();
        private List<string> OutputNpcTooltipIDs { get; set; } = new List<string>();
        private Dictionary<string, List<string>> DiffCashTags { get; set; } = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DiffGearTags { get; set; } = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DiffItemTags { get; set; } = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DiffMobTags { get; set; } = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DiffNpcTags { get; set; } = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DiffSkillTags { get; set; } = new Dictionary<string, List<string>>();
        public WzFileComparer Comparer { get; protected set; }
        private string stateInfo;
        private string stateDetail;
        public bool OutputPng { get; set; }
        public bool OutputAddedImg { get; set; }
        public bool OutputRemovedImg { get; set; }
        public bool EnableDarkMode { get; set; }
        public bool OutputCashTooltip { get; set; }
        public bool OutputGearTooltip { get; set; }
        public bool OutputItemTooltip { get; set; }
        public bool OutputMobTooltip { get; set; }
        public bool OutputNpcTooltip { get; set; }
        public bool OutputSkillTooltip { get; set; }
        public bool HashPngFileName { get; set; }
        public bool Enable22AniStyle { get; set; }
        public bool ShowObjectID { get; set; }
        public bool ShowChangeType { get; set; }
        public bool ShowPrice { get; set; }

        public string StateInfo
        {
            get { return stateInfo; }
            set
            {
                stateInfo = value;
                this.OnStateInfoChanged(EventArgs.Empty);
            }
        }

        public string StateDetail
        {
            get { return stateDetail; }
            set
            {
                stateDetail = value;
                this.OnStateDetailChanged(EventArgs.Empty);
            }
        }

        public event EventHandler StateInfoChanged;
        public event EventHandler StateDetailChanged;
        public event EventHandler<Patcher.PatchingEventArgs> PatchingStateChanged;

        protected virtual void OnStateInfoChanged(EventArgs e)
        {
            if (this.StateInfoChanged != null)
                this.StateInfoChanged(this, e);
        }

        protected virtual void OnStateDetailChanged(EventArgs e)
        {
            if (this.StateDetailChanged != null)
                this.StateDetailChanged(this, e);
        }

        protected virtual void OnPatchingStateChanged(Patcher.PatchingEventArgs e)
        {
            if (this.PatchingStateChanged != null)
                this.PatchingStateChanged(this, e);
        }

        public void EasyCompareWzFiles(Wz_File fileNew, Wz_File fileOld, string outputDir, StreamWriter index = null)
        {
            StateInfo = "比較中...";

            if ((fileNew.Type == Wz_Type.Base || fileOld.Type == Wz_Type.Base) && index == null) //至少有一个base 拆分对比
            {
                var virtualNodeNew = RebuildWzFile(fileNew);
                var virtualNodeOld = RebuildWzFile(fileOld);
                WzFileComparer comparer = new WzFileComparer();
                comparer.IgnoreWzFile = true;

                if (OutputCashTooltip || OutputGearTooltip || OutputItemTooltip || OutputMobTooltip || OutputNpcTooltip || OutputSkillTooltip)
                {
                    this.WzNewOld[0] = fileNew.Node;
                    this.WzNewOld[1] = fileOld.Node;
                    this.WzFileNewOld[0] = fileNew.Node.GetNodeWzFile();
                    this.WzFileNewOld[1] = fileOld.Node.GetNodeWzFile();
                }


                var dictNew = SplitVirtualNode(virtualNodeNew);
                var dictOld = SplitVirtualNode(virtualNodeOld);

                //寻找共同wzType
                var wzTypeList = dictNew.Select(kv => kv.Key)
                    .Where(wzType => dictOld.ContainsKey(wzType));

                CreateStyleSheet(outputDir);

                string htmlFilePath = Path.Combine(outputDir, "index.html");

                FileStream htmlFile = null;
                StreamWriter sw = null;
                StateInfo = "インデックスファイルを作成中...";
                StateDetail = "ファイルの作成";
                try
                {
                    htmlFile = new FileStream(htmlFilePath, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(htmlFile, Encoding.UTF8);
                    sw.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\">");
                    sw.WriteLine("<title>Index {1} → {0}</title>", fileNew.Header.WzVersion, fileOld.Header.WzVersion);
                    sw.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"style.css\" />");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    //输出概况
                    sw.WriteLine("<p class=\"wzf\">");
                    sw.WriteLine("<table>");
                    sw.WriteLine("<tr><th>ファイル名</th><th>新しいバージョンのサイズ</th><th>古いバージョンのサイズ</th><th>変更済み</th><th>追加</th><th>削除されました</th></tr>");
                    foreach (var wzType in wzTypeList)
                    {
                        var vNodeNew = dictNew[wzType];
                        var vNodeOld = dictOld[wzType];
                        var cmp = comparer.Compare(vNodeNew, vNodeOld);
                        OutputFile(vNodeNew.LinkNodes.Select(node => node.Value).OfType<Wz_File>().ToList(),
                            vNodeOld.LinkNodes.Select(node => node.Value).OfType<Wz_File>().ToList(),
                            wzType,
                            cmp.ToList(),
                            outputDir,
                            sw);
                    }
                    sw.WriteLine("</table>");
                    sw.WriteLine("</p>");

                    //html结束
                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                }
                finally
                {
                    try
                    {
                        if (sw != null)
                        {
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else //执行传统对比
            {
                WzFileComparer comparer = new WzFileComparer();
                comparer.IgnoreWzFile = false;
                var cmp = comparer.Compare(fileNew.Node, fileOld.Node);
                CreateStyleSheet(outputDir);
                OutputFile(fileNew, fileOld, fileNew.Type, cmp.ToList(), outputDir, index);
            }

            GC.Collect();
        }

        public void EasyCompareWzStructures(Wz_Structure structureNew, Wz_Structure structureOld, string outputDir, StreamWriter index)
        {
            var virtualNodeNew = RebuildWzStructure(structureNew);
            var virtualNodeOld = RebuildWzStructure(structureOld);
            WzFileComparer comparer = new WzFileComparer();
            comparer.IgnoreWzFile = true;

            var dictNew = SplitVirtualNode(virtualNodeNew);
            var dictOld = SplitVirtualNode(virtualNodeOld);

            //寻找共同wzType
            var wzTypeList = dictNew.Select(kv => kv.Key)
                .Where(wzType => dictOld.ContainsKey(wzType));

            CreateStyleSheet(outputDir);

            foreach (var wzType in wzTypeList)
            {
                var vNodeNew = dictNew[wzType];
                var vNodeOld = dictOld[wzType];
                var cmp = comparer.Compare(vNodeNew, vNodeOld);
                OutputFile(vNodeNew.LinkNodes.Select(node => node.Value).OfType<Wz_File>().ToList(),
                    vNodeOld.LinkNodes.Select(node => node.Value).OfType<Wz_File>().ToList(),
                    wzType,
                    cmp.ToList(),
                    outputDir,
                    index);
            }
        }

        public void EasyCompareWzStructuresToWzFiles(Wz_File fileNew, Wz_Structure structureOld, string outputDir, StreamWriter index)
        {
            var virtualNodeOld = RebuildWzStructure(structureOld);
            WzFileComparer comparer = new WzFileComparer();
            comparer.IgnoreWzFile = true;

            var dictOld = SplitVirtualNode(virtualNodeOld);

            //寻找共同wzType
            var wzTypeList = dictOld.Select(kv => kv.Key)
                .Where(wzType => dictOld.ContainsKey(wzType));

            CreateStyleSheet(outputDir);

            foreach (var wzType in wzTypeList)
            {
                var vNodeOld = dictOld[wzType];
                var cmp = comparer.Compare(fileNew.Node, vNodeOld);
                OutputFile(new List<Wz_File>() { fileNew },
                    vNodeOld.LinkNodes.Select(node => node.Value).OfType<Wz_File>().ToList(),
                    wzType,
                    cmp.ToList(),
                    outputDir,
                    index);
            }
        }

        private WzVirtualNode RebuildWzFile(Wz_File wzFile)
        {
            //分组
            List<Wz_File> subFiles = new List<Wz_File>();
            WzVirtualNode topNode = new WzVirtualNode(wzFile.Node);

            foreach (var childNode in wzFile.Node.Nodes)
            {
                var subFile = childNode.GetValue<Wz_File>();
                if (subFile != null && !subFile.IsSubDir) //wz子文件
                {
                    subFiles.Add(subFile);
                }
                else //其他
                {
                    topNode.AddChild(childNode, true);
                }
            }

            if (wzFile.Type == Wz_Type.Base)
            {
                foreach (var grp in subFiles.GroupBy(f => f.Type))
                {
                    WzVirtualNode fileNode = new WzVirtualNode();
                    fileNode.Name = grp.Key.ToString();
                    foreach (var file in grp)
                    {
                        fileNode.Combine(file.Node);
                    }
                    topNode.AddChild(fileNode);
                }
            }
            return topNode;
        }

        private WzVirtualNode RebuildWzStructure(Wz_Structure wzStructure)
        {
            //分组
            List<Wz_File> subFiles = wzStructure.wz_files.Where(wz_file => wz_file != null).ToList();
            WzVirtualNode topNode = new WzVirtualNode();

            foreach (var grp in subFiles.GroupBy(f => f.Type))
            {
                WzVirtualNode fileNode = new WzVirtualNode();
                fileNode.Name = grp.Key.ToString();
                foreach (var file in grp)
                {
                    fileNode.Combine(file.Node);
                }
                topNode.AddChild(fileNode);
            }
            return topNode;
        }

        private Dictionary<Wz_Type, WzVirtualNode> SplitVirtualNode(WzVirtualNode node)
        {
            var dict = new Dictionary<Wz_Type, WzVirtualNode>();
            Wz_File wzFile = null;
            if (node.LinkNodes.Count > 0)
            {
                wzFile = node.LinkNodes[0].Value as Wz_File;
                dict[wzFile.Type] = node;
            }

            if (wzFile?.Type == Wz_Type.Base || node.LinkNodes.Count == 0) //额外处理
            {
                var wzFileList = node.ChildNodes
                    .Select(child => new { Node = child, WzFile = child.LinkNodes[0].Value as Wz_File })
                    .Where(item => item.WzFile != null);

                foreach (var item in wzFileList)
                {
                    dict[item.WzFile.Type] = item.Node;
                }
            }

            return dict;
        }

        private IEnumerable<string> GetFileInfo(Wz_File wzf, Func<Wz_File, string> extractor)
        {
            IEnumerable<string> result = new[] { extractor.Invoke(wzf) }
                .Concat(wzf.MergedWzFiles.Select(extractor.Invoke));

            if (wzf.Type != Wz_Type.Base)
            {
                result = result.Concat(wzf.Node.Nodes.Where(n => n.Value is Wz_File).SelectMany(nwzf => GetFileInfo((Wz_File)nwzf.Value, extractor)));
            }

            return result;
        }

        private void OutputFile(Wz_File fileNew, Wz_File fileOld, Wz_Type type, List<CompareDifference> diffLst, string outputDir, StreamWriter index)
        {
            OutputFile(new List<Wz_File>() { fileNew },
                new List<Wz_File>() { fileOld },
                type,
                diffLst,
                outputDir,
                index);
        }
        private void OutputFile(List<Wz_File> fileNew, List<Wz_File> fileOld, Wz_Type type, List<CompareDifference> diffLst, string outputDir, StreamWriter index = null)
        {
            string htmlFilePath = Path.Combine(outputDir, type.ToString() + ".html");
            for (int i = 1; File.Exists(htmlFilePath); i++)
            {
                htmlFilePath = Path.Combine(outputDir, string.Format("{0}_{1}.html", type, i));
            }
            string srcDirPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(htmlFilePath) + "_files");
            if (OutputPng && !Directory.Exists(srcDirPath))
            {
                Directory.CreateDirectory(srcDirPath);
            }
            string skillTooltipPath = Path.Combine(outputDir, "SkillTooltips");
            string itemTooltipPath = Path.Combine(outputDir, "ItemTooltips");
            string gearTooltipPath = Path.Combine(outputDir, "GearTooltips");
            string mobTooltipPath = Path.Combine(outputDir, "MobTooltips");
            string npcTooltipPath = Path.Combine(outputDir, "NpcTooltips");

            FileStream htmlFile = null;
            StreamWriter sw = null;
            StateInfo = type + "を作成しています...";
            StateDetail = "出力ファイルを作成しています...";
            try
            {
                htmlFile = new FileStream(htmlFilePath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(htmlFile, Encoding.UTF8);
                sw.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\">");
                sw.WriteLine("<title>{0} v{2} → v{1}</title>", type, fileNew[0].GetMergedVersion(), fileOld[0].GetMergedVersion());
                sw.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"style.css\" />");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                //输出概况
                sw.WriteLine("<p class=\"wzf\">");
                sw.WriteLine("<table>");
                sw.WriteLine("<tr><th>&nbsp;</th><th>ファイル名</th><th>サイズ</th><th>バージョン</th></tr>");
                sw.WriteLine("<tr><td>新しいバージョン</td><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                    string.Join("<br/>", fileNew.SelectMany(wzf => GetFileInfo(wzf, ewzf => ewzf.Header.FileName))),
                    string.Join("<br/>", fileNew.SelectMany(wzf => GetFileInfo(wzf, ewzf => ewzf.Header.FileSize.ToString("N0")))),
                    string.Join("<br/>", fileNew.Select(wzf => wzf.GetMergedVersion()))
                    );
                sw.WriteLine("<tr><td>古いバージョン</td><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                    string.Join("<br/>", fileOld.SelectMany(wzf => GetFileInfo(wzf, ewzf => ewzf.Header.FileName))),
                    string.Join("<br/>", fileOld.SelectMany(wzf => GetFileInfo(wzf, ewzf => ewzf.Header.FileSize.ToString("N0")))),
                    string.Join("<br/>", fileOld.Select(wzf => wzf.GetMergedVersion()))
                    );
                sw.WriteLine("<tr><td>現在の時刻</td><td colspan='3'>{0:M-d-yyyy HH:mm:ss.fff}</td></tr>", DateTime.Now);
                sw.WriteLine("<tr><td>オプション</td><td colspan='3'>{0}</td></tr>", string.Join("<br/>", new[] {
                    this.OutputPng ? "- PNGファイルを出力" : null,
                    this.OutputAddedImg ? "- 追加ファイル" : null,
                    this.OutputRemovedImg ? "- 削除済みファイル" : null,
                    this.EnableDarkMode ? "- ダークモード" : null,
                    "- Compare " + this.Comparer.PngComparison,
                    this.Comparer.ResolvePngLink ? "- PNGリンクを解決" : null,
                }.Where(p => p != null)));
                sw.WriteLine("</table>");
                sw.WriteLine("</p>");

                //输出目录
                StringBuilder[] sb = { new StringBuilder(), new StringBuilder(), new StringBuilder() };
                int[] count = new int[6];
                string[] diffStr = { "変更", "追加", "削除" };
                foreach (CompareDifference diff in diffLst)
                {
                    int idx = -1;
                    string detail = null;
                    switch (diff.DifferenceType)
                    {
                        case DifferenceType.Changed:
                            idx = 0;
                            detail = string.Format("<a name=\"m_{1}_{2}\" href=\"#a_{1}_{2}\">{0}</a>", diff.NodeNew.FullPathToFile, idx, count[idx]);
                            break;
                        case DifferenceType.Append:
                            idx = 1;
                            if (this.OutputAddedImg)
                            {
                                detail = string.Format("<a name=\"m_{1}_{2}\" href=\"#a_{1}_{2}\">{0}</a>", diff.NodeNew.FullPathToFile, idx, count[idx]);
                            }
                            else
                            {
                                detail = diff.NodeNew.FullPathToFile;
                            }
                            break;
                        case DifferenceType.Remove:
                            idx = 2;
                            if (this.OutputRemovedImg)
                            {
                                detail = string.Format("<a name=\"m_{1}_{2}\" href=\"#a_{1}_{2}\">{0}</a>", diff.NodeOld.FullPathToFile, idx, count[idx]);
                            }
                            else
                            {
                                detail = diff.NodeOld.FullPathToFile;
                            }
                            break;
                        default:
                            continue;
                    }
                    sb[idx].Append("<tr><td>");
                    sb[idx].Append(detail);
                    sb[idx].AppendLine("</td></tr>");
                    count[idx]++;
                }
                StateDetail = "目次を作成しています";
                Array.Copy(count, 0, count, 3, 3);
                for (int i = 0; i < sb.Length; i++)
                {
                    sw.WriteLine("<table class=\"lst{0}\">", i);
                    sw.WriteLine("<tr><th><a name=\"m_{0}\">{1}:{2}</a></th></tr>", i, diffStr[i], count[i]);
                    sw.Write(sb[i].ToString());
                    sw.WriteLine("</table>");
                    sb[i] = null;
                    count[i] = 0;
                }

                Patcher.PatchPartContext part = new Patcher.PatchPartContext("", 0, 0);
                part.NewFileLength = count[3] + (this.OutputAddedImg ? count[4] : 0) + (this.OutputRemovedImg ? count[5] : 0);

                OnPatchingStateChanged(new Patcher.PatchingEventArgs(part, Patcher.PatchingState.CompareStarted));

                foreach (CompareDifference diff in diffLst)
                {
                    OnPatchingStateChanged(new Patcher.PatchingEventArgs(part, Patcher.PatchingState.TempFileBuildProcessChanged, count[0] + count[1] + count[2]));
                    switch (diff.DifferenceType)
                    {
                        case DifferenceType.Changed:
                            {
                                StateInfo = string.Format("{0}/{1} 変更: {2}", count[0], count[3], diff.NodeNew.FullPath);
                                Wz_Image imgNew, imgOld;
                                if ((imgNew = diff.ValueNew as Wz_Image) != null
                                    && ((imgOld = diff.ValueOld as Wz_Image) != null))
                                {
                                    string anchorName = "a_0_" + count[0];
                                    string menuAnchorName = "m_0_" + count[0];
                                    CompareImg(imgNew, imgOld, diff.NodeNew.FullPathToFile, anchorName, menuAnchorName, srcDirPath, sw);
                                }
                                count[0]++;
                            }
                            break;

                        case DifferenceType.Append:
                            if (this.OutputAddedImg)
                            {
                                StateInfo = string.Format("{0}/{1} 追加: {2}", count[1], count[4], diff.NodeNew.FullPath);
                                Wz_Image imgNew = diff.ValueNew as Wz_Image;
                                if (imgNew != null)
                                {
                                    string anchorName = "a_1_" + count[1];
                                    string menuAnchorName = "m_1_" + count[1];
                                    OutputImg(imgNew, diff.DifferenceType, diff.NodeNew.FullPathToFile, anchorName, menuAnchorName, srcDirPath, sw);
                                }
                                count[1]++;
                            }
                            break;

                        case DifferenceType.Remove:
                            if (this.OutputRemovedImg)
                            {
                                StateInfo = string.Format("{0}/{1} 削除: {2}", count[2], count[5], diff.NodeOld.FullPath);
                                Wz_Image imgOld = diff.ValueOld as Wz_Image;
                                if (imgOld != null)
                                {
                                    string anchorName = "a_2_" + count[2];
                                    string menuAnchorName = "m_2_" + count[2];
                                    OutputImg(imgOld, diff.DifferenceType, diff.NodeOld.FullPathToFile, anchorName, menuAnchorName, srcDirPath, sw);
                                }
                                count[2]++;
                            }
                            break;

                        case DifferenceType.NotChanged:
                            break;
                    }

                }
                //html结束
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");

                if (index != null)
                {
                    index.WriteLine("<tr><td><a href=\"{0}.html\">{0}.wz</a></td><td>{1}</td><td>{2}</td><td><a href=\"{0}.html#m_0\">{3}</a></td><td><a href=\"{0}.html#m_1\">{4}</a></td><td><a href=\"{0}.html#m_2\">{5}</a></td></tr>",
                        type.ToString(),
                        string.Join("<br/>", fileNew.SelectMany(wzf => GetFileInfo(wzf, ewzf => ewzf.Header.FileSize.ToString("N0")))),
                        string.Join("<br/>", fileOld.SelectMany(wzf => GetFileInfo(wzf, ewzf => ewzf.Header.FileSize.ToString("N0")))),
                        count[3],
                        count[4],
                        count[5]
                        );
                    index.Flush();
                }
            }
            finally
            {
                try
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch
                {
                }
                OnPatchingStateChanged(new Patcher.PatchingEventArgs(null, Patcher.PatchingState.CompareFinished));
            }

            if (OutputSkillTooltip && type.ToString() == "String" && OutputSkillTooltipIDs != null)
            {
                if (!Directory.Exists(skillTooltipPath))
                {
                    Directory.CreateDirectory(skillTooltipPath);
                }
                SaveSkillTooltip(skillTooltipPath);
            }
            if (OutputItemTooltip && type.ToString() == "String" && OutputItemTooltipIDs != null)
            {
                if (!Directory.Exists(itemTooltipPath))
                {
                    Directory.CreateDirectory(itemTooltipPath);
                }
                SaveItemTooltip(itemTooltipPath);
            }
            if (OutputGearTooltip && type.ToString() == "String" && OutputGearTooltipIDs != null)
            {
                if (!Directory.Exists(gearTooltipPath))
                {
                    Directory.CreateDirectory(gearTooltipPath);
                }
                if (this.Enable22AniStyle)
                {
                    SaveGearTooltip3(gearTooltipPath);
                }
                else
                {
                    SaveGearTooltip(gearTooltipPath);
                }
            }
            if (OutputMobTooltip && type.ToString() == "String" && OutputMobTooltipIDs != null)
            {
                if (!Directory.Exists(mobTooltipPath))
                {
                    Directory.CreateDirectory(mobTooltipPath);
                }
                SaveMobTooltip(mobTooltipPath);
            }
            if (OutputNpcTooltip && type.ToString() == "String" && OutputNpcTooltipIDs != null)
            {
                if (!Directory.Exists(npcTooltipPath))
                {
                    Directory.CreateDirectory(npcTooltipPath);
                }
                SaveNpcTooltip(npcTooltipPath);
            }
            if (OutputCashTooltip && type.ToString() == "String" && OutputCashTooltipIDs != null)
            {
                if (!Directory.Exists(itemTooltipPath))
                {
                    Directory.CreateDirectory(itemTooltipPath);
                }
                SaveCashTooltip(itemTooltipPath);
            }
        }

        // 変更されたスキルツールチップ出力
        private void SaveSkillTooltip(string skillTooltipPath)
        {
            SkillTooltipRender2[] skillRenderNewOld = new SkillTooltipRender2[2];
            int count = 0;
            int allCount = OutputSkillTooltipIDs.Count;
            var skillTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                skillRenderNewOld[i] = new SkillTooltipRender2();
                skillRenderNewOld[i].StringLinker = new StringLinker();
                skillRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                skillRenderNewOld[i].ShowObjectID = this.ShowObjectID;
                skillRenderNewOld[i].ShowDelay = true;
                skillRenderNewOld[i].wzNode = WzNewOld[i];
                skillRenderNewOld[i].DiffSkillTags = this.DiffSkillTags;
                skillRenderNewOld[i].IgnoreEvalError = true;
                skillRenderNewOld[i].Enable22AniStyle = this.Enable22AniStyle;
            }

            foreach (var skillID in OutputSkillTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} スキル: {2}", ++count, allCount, skillID);
                StateDetail = "Skill 変更点をツールチップ画像に出力中...";

                string skillType = "";
                string skillNodePath = int.Parse(skillID) / 10000000 == 8 ? String.Format(@"\{0:D}.img\skill\{1:D}", int.Parse(skillID) / 100, skillID) : String.Format(@"\{0:D}.img\skill\{1:D}", int.Parse(skillID) / 10000, skillID);
                if (int.Parse(skillID) / 10000 == 0) skillNodePath = String.Format(@"\000.img\skill\{0:D7}", skillID);
                StringResult sr;
                string skillName;
                if (skillRenderNewOld[1].StringLinker == null || !skillRenderNewOld[1].StringLinker.StringSkill.TryGetValue(int.Parse(skillID), out sr))
                {
                    sr = new StringResultSkill();
                    sr.Name = "未知のスキル";
                }
                skillName = sr.Name;
                if (skillRenderNewOld[0].StringLinker == null || !skillRenderNewOld[0].StringLinker.StringSkill.TryGetValue(int.Parse(skillID), out sr))
                {
                    sr = new StringResultSkill();
                    sr.Name = "未知のスキル";
                }
                if (skillName != sr.Name && skillName != "未知のスキル" && sr.Name != "未知のスキル")
                {
                    skillName += "_" + sr.Name;
                }
                else if (skillName == "未知のスキル")
                {
                    skillName = sr.Name;
                }
                if (String.IsNullOrEmpty(skillName)) skillName = "未知のスキル";
                skillName = RemoveInvalidFileNameChars(skillName);
                int nullSkillIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    Skill skill = Skill.CreateFromNode(PluginManager.FindWz("Skill" + skillNodePath, WzFileNewOld[i]), PluginManager.FindWz, WzFileNewOld[i]) ??
                        (Skill.CreateFromNode(PluginManager.FindWz("Skill001" + skillNodePath, WzFileNewOld[i]), PluginManager.FindWz, WzFileNewOld[i]) ??
                        (Skill.CreateFromNode(PluginManager.FindWz("Skill002" + skillNodePath, WzFileNewOld[i]), PluginManager.FindWz, WzFileNewOld[i]) ??
                        Skill.CreateFromNode(PluginManager.FindWz("Skill003" + skillNodePath, WzFileNewOld[i]), PluginManager.FindWz, WzFileNewOld[i])));

                    if (skill != null)
                    {
                        skill.Level = skill.MaxLevel;
                        skillRenderNewOld[i].Skill = skill;
                    }
                    else
                    {
                        nullSkillIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullSkillIdx)
                {
                    case 0: // change
                        skillType = "変更";

                        Bitmap ImageNew = skillRenderNewOld[0].Render(true);
                        Bitmap ImageOld = skillRenderNewOld[1].Render(true);
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        skillType = "削除";

                        resultImage = skillRenderNewOld[1].Render();
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        skillType = "追加";

                        resultImage = skillRenderNewOld[0].Render();
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                var skillTypeTextInfo = g.MeasureString(skillType, GearGraphics.ItemDetailFont);
                int picH = 13;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, skillType, skillTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(skillTypeTextInfo.Width) + 2, ref picH, 10);

                string categoryPath = (ItemStringHelper.GetJobName(int.Parse(skillID) / 10000) ?? "その他");

                if (!Directory.Exists(Path.Combine(skillTooltipPath, categoryPath)))
                {
                    Directory.CreateDirectory(Path.Combine(skillTooltipPath, categoryPath));
                }

                string imageName = Path.Combine(skillTooltipPath, categoryPath, "スキル_" + skillID + "_" + skillName + "_" + skillType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputSkillTooltipIDs.Clear();
            DiffSkillTags.Clear();
        }

        // 노드에서 스킬 ID 얻기
        private void GetSkillID(Wz_Node node, bool change)
        {
            if (node == null) return;

            Match match = Regex.Match(node.FullPathToFile, @"^String\\Skill.img\\(\d+).*");
            string tag = null;

            if (!match.Success)
            {
                tag = node.Text;
                match = Regex.Match(node.FullPathToFile, @"^Skill\d*\\\d+.img\\skill\\(\d+)\\(common|masterLevel|combatOrders|action|isPetAutoBuff|isSequenceOn|BGM).*"); // 변경점 중 스킬 툴팁 출력할 것들

                if (change && !match.Success)
                {
                    match = Regex.Match(node.FullPathToFile, @"^Skill\\_Canvas\\\d+.img\\skill\\(\d+)\\(icon)$"); // 스킬 아이콘 변경 체크
                }
            }

            if (match.Success)
            {
                string skillID = match.Groups[1].ToString();

                if (skillID != null)
                {
                    if (!OutputSkillTooltipIDs.Contains(skillID))
                    {
                        OutputSkillTooltipIDs.Add(skillID);
                        DiffSkillTags[skillID] = new List<string>();
                    }

                    if (tag != null && !DiffSkillTags[skillID].Contains(tag))
                    {
                        DiffSkillTags[skillID].Add(tag);
                    }
                }
            }
        }

        // 変更されたアイテムツールチップ出力
        private void SaveItemTooltip(string itemTooltipPath)
        {
            ItemTooltipRender2[] itemRenderNewOld = new ItemTooltipRender2[2];
            int count = 0;
            int allCount = OutputItemTooltipIDs.Count;
            var itemTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                itemRenderNewOld[i] = new ItemTooltipRender2();
                itemRenderNewOld[i].StringLinker = new StringLinker();
                itemRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                itemRenderNewOld[i].ShowObjectID = this.ShowObjectID;
                itemRenderNewOld[i].Enable22AniStyle = this.Enable22AniStyle;
                itemRenderNewOld[i].ShowSoldPrice = this.ShowPrice;
                itemRenderNewOld[i].ShowCashPurchasePrice = this.ShowPrice;
            }

            foreach (var itemID in OutputItemTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} アイテム: {2}", ++count, allCount, itemID);
                StateDetail = "Item 変更点をツールチップ画像に出力中...";
                string itemType = "";
                string itemNodePath = null;
                string categoryPath = "";

                if (!int.TryParse(itemID, out _)) continue;

                if (itemID.StartsWith("03015")) // 判断开头是否是03015
                {
                    itemNodePath = String.Format(@"Item\Install\0{0:D}.img\{1:D}", int.Parse(itemID) / 100, itemID);
                    categoryPath = "Chair_椅子";
                }
                else if (itemID.StartsWith("0301")) // 判断开头是否是0301
                {
                    itemNodePath = String.Format(@"Item\Install\0{0:D}.img\{1:D}", int.Parse(itemID) / 1000, itemID);
                    categoryPath = "Chair_椅子";
                }
                else if (itemID.StartsWith("500")) // 判断开头是否是0500
                {
                    itemNodePath = String.Format(@"Item\Pet\{0:D}.img", itemID);
                    categoryPath = "Pet_ペット";
                }
                else if (itemID.StartsWith("02")) // 判断第1位是否是02
                {
                    itemNodePath = String.Format(@"Item\Consume\0{0:D}.img\{1:D}", int.Parse(itemID) / 10000, itemID);
                    categoryPath = "Consumable_消耗品";
                }
                else if (itemID.StartsWith("03")) // 判断第1位是否是03
                {
                    itemNodePath = String.Format(@"Item\Install\0{0:D}.img\{1:D}", int.Parse(itemID) / 10000, itemID);
                    categoryPath = "OtherSetup";
                }
                else if (itemID.StartsWith("04")) // 判断第1位是否是04
                {
                    itemNodePath = String.Format(@"Item\Etc\0{0:D}.img\{1:D}", int.Parse(itemID) / 10000, itemID);
                    categoryPath = "Etc_その他";
                }
                else if (itemID.StartsWith("05")) // 判断第1位是否是02
                {
                    itemNodePath = String.Format(@"Item\Cash\0{0:D}.img\{1:D}", int.Parse(itemID) / 10000, itemID);
                    categoryPath = "Cash_ポイント";
                }

                StringResult sr;
                string ItemName;
                if (itemRenderNewOld[1].StringLinker == null || !itemRenderNewOld[1].StringLinker.StringItem.TryGetValue(int.Parse(itemID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のアイテム";
                }
                ItemName = sr.Name;
                if (itemRenderNewOld[0].StringLinker == null || !itemRenderNewOld[0].StringLinker.StringItem.TryGetValue(int.Parse(itemID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のアイテム";
                }
                if (ItemName != sr.Name && ItemName != "未知のアイテム" && sr.Name != "未知のアイテム")
                {
                    ItemName += "_" + sr.Name;
                }
                else if (ItemName == "未知のアイテム")
                {
                    ItemName = sr.Name;
                }
                if (String.IsNullOrEmpty(ItemName)) ItemName = "未知のアイテム";
                ItemName = RemoveInvalidFileNameChars(ItemName);
                int nullItemIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    Item item = Item.CreateFromNode(PluginManager.FindWz(itemNodePath, WzFileNewOld[i]), PluginManager.FindWz);

                    if (item != null)
                    {
                        itemRenderNewOld[i].Item = item;
                    }
                    else
                    {
                        nullItemIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullItemIdx)
                {
                    case 0: // change
                        itemType = "変更";

                        Bitmap ImageNew = itemRenderNewOld[0].Render();
                        Bitmap ImageOld = itemRenderNewOld[1].Render();
                        if (GetBitmapHash(ImageNew) == GetBitmapHash(ImageOld)) continue;
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        itemType = "削除";

                        resultImage = itemRenderNewOld[1].Render();
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        itemType = "追加";

                        resultImage = itemRenderNewOld[0].Render();
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                if (!Directory.Exists(Path.Combine(itemTooltipPath, categoryPath)))
                {
                    Directory.CreateDirectory(Path.Combine(itemTooltipPath, categoryPath));
                }

                var itemTypeTextInfo = g.MeasureString(itemType, GearGraphics.ItemDetailFont);
                int picH = 13;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, itemType, itemTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(itemTypeTextInfo.Width) + 2, ref picH, 10);

                string imageName = Path.Combine(itemTooltipPath, categoryPath, "アイテム_" + itemID + "_" + ItemName + "_" + itemType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputItemTooltipIDs.Clear();
            DiffItemTags.Clear();
        }

        // 変更された装備ツールチップ出力
        private void SaveGearTooltip(string gearTooltipPath)
        {
            GearTooltipRender2[] gearRenderNewOld = new GearTooltipRender2[2];
            int count = 0;
            int allCount = OutputGearTooltipIDs.Count;
            var gearTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                gearRenderNewOld[i] = new GearTooltipRender2();
                gearRenderNewOld[i].StringLinker = new StringLinker();
                gearRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                gearRenderNewOld[i].ShowObjectID = this.ShowObjectID;
                gearRenderNewOld[i].ShowSoldPrice = this.ShowPrice;
                gearRenderNewOld[i].ShowCashPurchasePrice = this.ShowPrice;
                gearRenderNewOld[i].ShowCombatPower = true;
            }

            foreach (var gearID in OutputGearTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} 装備: {2}", ++count, allCount, gearID);
                StateDetail = "Character 変更点をツールチップ画像に出力中...";
                string gearType = "";
                string gearNodePath = null;
                string categoryPath = "";

                if (!int.TryParse(gearID, out _)) continue;

                if (Regex.IsMatch(gearID, "^0101|^0102|^0103|^0112|^0113|^0114|^0115|^0116|^0118|^0119")) // 判断开头是否是0101~0103或0112~0116-0118~0119
                {
                    gearNodePath = String.Format(@"Character\Accessory\{0:D}.img", gearID);
                    categoryPath = "Accessory_飾り";
                }
                else if (gearID.StartsWith("0100")) // 判断开头是否是0100
                {
                    gearNodePath = String.Format(@"Character\Cap\{0:D}.img", gearID);
                    categoryPath = "Hat_帽子";
                }
                else if (gearID.StartsWith("0104")) // 判断开头是否是0104
                {
                    gearNodePath = String.Format(@"Character\Coat\{0:D}.img", gearID);
                    categoryPath = "Top_服(上)";
                }
                else if (gearID.StartsWith("0105")) // 判断开头是否是0104
                {
                    gearNodePath = String.Format(@"Character\Longcoat\{0:D}.img", gearID);
                    categoryPath = "Overall_服(全身)";
                }
                else if (gearID.StartsWith("0106")) // 判断开头是否是0106
                {
                    gearNodePath = String.Format(@"Character\Pants\{0:D}.img", gearID);
                    categoryPath = "Bottom_服(下)";
                }
                else if (gearID.StartsWith("0107")) // 判断开头是否是0107
                {
                    gearNodePath = String.Format(@"Character\Shoes\{0:D}.img", gearID);
                    categoryPath = "Shoes_靴";
                }
                else if (gearID.StartsWith("0108")) // 判断开头是否是0108
                {
                    gearNodePath = String.Format(@"Character\Glove\{0:D}.img", gearID);
                    categoryPath = "Glove_手袋";
                }
                else if (gearID.StartsWith("0109")) // 判断开头是否是0109
                {
                    gearNodePath = String.Format(@"Character\Shield\{0:D}.img", gearID);
                    categoryPath = "Shield_盾";
                }
                else if (gearID.StartsWith("0110")) // 判断开头是否是0110
                {
                    gearNodePath = String.Format(@"Character\Cape\{0:D}.img", gearID);
                    categoryPath = "Cape_マント";
                }
                else if (gearID.StartsWith("0111")) // 判断开头是否是0111
                {
                    gearNodePath = String.Format(@"Character\Ring\{0:D}.img", gearID);
                    categoryPath = "Ring_指輪";
                }
                else if (gearID.StartsWith("0120") || gearID.StartsWith("120")) // 判断开头是否是0120
                {
                    gearNodePath = String.Format(@"Character\Totem\{0:D}.img", gearID);
                    categoryPath = "Totem_トーテム";
                }
                else if (Regex.IsMatch(gearID, "^012[1-9]|^013|^014|^015|^0160|^0169|^0170")) // 判断开头是否是012~015、0160或0169-0179
                {
                    gearNodePath = String.Format(@"Character\Weapon\{0:D}.img", gearID);
                    categoryPath = "Weapon_武器";
                }
                else if (Regex.IsMatch(gearID, "^0161|^0162|^0163|^0164|^0165"))// 判断开头是否是0161~0165
                {
                    gearNodePath = String.Format(@"Character\Mechanic\{0:D}.img", gearID);
                    categoryPath = "MechanicPart_メカニックパーツ";
                }
                else if (Regex.IsMatch(gearID, "^0166|^0167")) // 判断开头是否是0166或0167
                {
                    gearNodePath = String.Format(@"Character\Android\{0:D}.img", gearID);
                    categoryPath = "Android_アンドロイド";
                }
                else if (gearID.StartsWith("0168")) // 判断开头是否是0168
                {
                    gearNodePath = String.Format(@"Character\Bits\{0:D}.img", gearID);
                    categoryPath = "Bits_拼図";
                }
                else if (gearID.StartsWith("01712")) // 判断开头是否是01712
                {
                    gearNodePath = String.Format(@"Character\ArcaneForce\{0:D}.img", gearID);
                    categoryPath = "Arcane";
                }
                else if (Regex.IsMatch(gearID, "^01713|^01714")) // 判断开头是否是01713或01714
                {
                    gearNodePath = String.Format(@"Character\AuthenticForce\{0:D}.img", gearID);
                    categoryPath = "Authentic";
                }
                else if (Regex.IsMatch(gearID, "^0178"))  // 判断开头是否是0178
                {
                    gearNodePath = String.Format(@"Character\Jewel\{0:D}.img", gearID);
                    categoryPath = "Jewel_宝玉";
                }
                else if (Regex.IsMatch(gearID, "^0179"))  // 判断开头是否是0179
                {
                    gearNodePath = String.Format(@"Character\NT_Beauty\{0:D}.img", gearID);
                    categoryPath = "MSN_Cosmetic_化粧";
                }
                else if (gearID.StartsWith("018")) // 判断开头是否是018
                {
                    gearNodePath = String.Format(@"Character\PetEquip\{0:D}.img", gearID);
                    categoryPath = "PetEquipment_ペット装備";
                }
                else if (Regex.IsMatch(gearID, "^0194|^0195|^0196|^0197")) // 判断开头是否是0194~0197
                {
                    gearNodePath = String.Format(@"Character\Dragon\{0:D}.img", gearID);
                    categoryPath = "EvanDragonEquip_エヴァンドラゴン装備";
                }
                else if (Regex.IsMatch(gearID, "^0190|^0191|^0192|^0193|^0198")) // 判断开头是否是0190~0193或0198
                {
                    gearNodePath = String.Format(@"Character\TamingMob\{0:D}.img", gearID);
                    categoryPath = "TamedMonster_テイムドモンスター";
                }
                else if (Regex.IsMatch(gearID, "^0002|^0005")) // 判断开头是否是0002或0005
                {
                    gearNodePath = String.Format(@"Character\Face\{0:D}.img", gearID);
                    categoryPath = "Cosmetic_化粧";
                }
                else if (Regex.IsMatch(gearID, "^0003|^0004|^0006")) // 判断开头是否是0003、0004或0006
                {
                    gearNodePath = String.Format(@"Character\Hair\{0:D}.img", gearID);
                    categoryPath = "Cosmetic_化粧";
                }

                StringResult sr;
                string EqpName;
                if (gearRenderNewOld[1].StringLinker == null || !gearRenderNewOld[1].StringLinker.StringEqp.TryGetValue(int.Parse(gearID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知の装備";
                }
                EqpName = sr.Name;
                if (gearRenderNewOld[0].StringLinker == null || !gearRenderNewOld[0].StringLinker.StringEqp.TryGetValue(int.Parse(gearID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知の装備";
                }
                if (EqpName != sr.Name && EqpName != "未知の装備" && sr.Name != "未知の装備")
                {
                    EqpName += "_" + sr.Name;
                }
                else if (EqpName == "未知の装備")
                {
                    EqpName = sr.Name;
                }
                if (String.IsNullOrEmpty(EqpName)) EqpName = "未知の装備";
                EqpName = RemoveInvalidFileNameChars(EqpName);
                int nullEqpIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    Gear gear = Gear.CreateFromNode(PluginManager.FindWz(gearNodePath, WzFileNewOld[i]), PluginManager.FindWz);

                    if (gear != null)
                    {
                        gearRenderNewOld[i].Gear = gear;
                    }
                    else
                    {
                        nullEqpIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullEqpIdx)
                {
                    case 0: // change
                        gearType = "変更";

                        Bitmap ImageNew = gearRenderNewOld[0].Render();
                        Bitmap ImageOld = gearRenderNewOld[1].Render();
                        if (GetBitmapHash(ImageNew) == GetBitmapHash(ImageOld)) continue;
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        gearType = "削除";

                        resultImage = gearRenderNewOld[1].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        gearType = "追加";

                        resultImage = gearRenderNewOld[0].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                if (!Directory.Exists(Path.Combine(gearTooltipPath, categoryPath)))
                {
                    Directory.CreateDirectory(Path.Combine(gearTooltipPath, categoryPath));
                }

                var gearTypeTextInfo = g.MeasureString(gearType, GearGraphics.ItemDetailFont);
                int picH = 13;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, gearType, gearTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(gearTypeTextInfo.Width) + 2, ref picH, 10);

                string imageName = Path.Combine(gearTooltipPath, categoryPath, "装備_" + gearID + "_" + EqpName + "_" + gearType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputGearTooltipIDs.Clear();
            DiffGearTags.Clear();
        }

        private void SaveGearTooltip3(string gearTooltipPath)
        {
            GearTooltipRender3[] gearRenderNewOld = new GearTooltipRender3[2];
            int count = 0;
            int allCount = OutputGearTooltipIDs.Count;
            var gearTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                gearRenderNewOld[i] = new GearTooltipRender3();
                gearRenderNewOld[i].StringLinker = new StringLinker();
                gearRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                gearRenderNewOld[i].ShowObjectID = this.ShowObjectID;
                gearRenderNewOld[i].ShowSoldPrice = this.ShowPrice;
                gearRenderNewOld[i].ShowCashPurchasePrice = this.ShowPrice;
            }

            foreach (var gearID in OutputGearTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} 装備: {2}", ++count, allCount, gearID);
                StateDetail = "Character 変更点をツールチップ画像に出力中...";
                string gearType = "";
                string gearNodePath = null;
                string categoryPath = "";

                if (!int.TryParse(gearID, out _)) continue;

                if (Regex.IsMatch(gearID, "^0101|^0102|^0103|^0112|^0113|^0114|^0115|^0116|^0118|^0119")) // 判断开头是否是0101~0103或0112~0116-0118~0119
                {
                    gearNodePath = String.Format(@"Character\Accessory\{0:D}.img", gearID);
                    categoryPath = "Accessory_飾り";
                }
                else if (gearID.StartsWith("0100")) // 判断开头是否是0100
                {
                    gearNodePath = String.Format(@"Character\Cap\{0:D}.img", gearID);
                    categoryPath = "Hat_帽子";
                }
                else if (gearID.StartsWith("0104")) // 判断开头是否是0104
                {
                    gearNodePath = String.Format(@"Character\Coat\{0:D}.img", gearID);
                    categoryPath = "Top_服(上)";
                }
                else if (gearID.StartsWith("0105")) // 判断开头是否是0104
                {
                    gearNodePath = String.Format(@"Character\Longcoat\{0:D}.img", gearID);
                    categoryPath = "Overall_服(全身)";
                }
                else if (gearID.StartsWith("0106")) // 判断开头是否是0106
                {
                    gearNodePath = String.Format(@"Character\Pants\{0:D}.img", gearID);
                    categoryPath = "Bottom_服(下)";
                }
                else if (gearID.StartsWith("0107")) // 判断开头是否是0107
                {
                    gearNodePath = String.Format(@"Character\Shoes\{0:D}.img", gearID);
                    categoryPath = "Shoes_靴";
                }
                else if (gearID.StartsWith("0108")) // 判断开头是否是0108
                {
                    gearNodePath = String.Format(@"Character\Glove\{0:D}.img", gearID);
                    categoryPath = "Glove_手袋";
                }
                else if (gearID.StartsWith("0109")) // 判断开头是否是0109
                {
                    gearNodePath = String.Format(@"Character\Shield\{0:D}.img", gearID);
                    categoryPath = "Shield_盾";
                }
                else if (gearID.StartsWith("0110")) // 判断开头是否是0110
                {
                    gearNodePath = String.Format(@"Character\Cape\{0:D}.img", gearID);
                    categoryPath = "Cape_マント";
                }
                else if (gearID.StartsWith("0111")) // 判断开头是否是0111
                {
                    gearNodePath = String.Format(@"Character\Ring\{0:D}.img", gearID);
                    categoryPath = "Ring_指輪";
                }
                else if (gearID.StartsWith("0120") || gearID.StartsWith("120")) // 判断开头是否是0120
                {
                    gearNodePath = String.Format(@"Character\Totem\{0:D}.img", gearID);
                    categoryPath = "Totem_トーテム";
                }
                else if (Regex.IsMatch(gearID, "^012[1-9]|^013|^014|^015|^0160|^0169|^0170")) // 判断开头是否是012~015、0160或0169-0179
                {
                    gearNodePath = String.Format(@"Character\Weapon\{0:D}.img", gearID);
                    categoryPath = "Weapon_武器";
                }
                else if (Regex.IsMatch(gearID, "^0161|^0162|^0163|^0164|^0165"))// 判断开头是否是0161~0165
                {
                    gearNodePath = String.Format(@"Character\Mechanic\{0:D}.img", gearID);
                    categoryPath = "MechanicPart_メカニックパーツ";
                }
                else if (Regex.IsMatch(gearID, "^0166|^0167")) // 判断开头是否是0166或0167
                {
                    gearNodePath = String.Format(@"Character\Android\{0:D}.img", gearID);
                    categoryPath = "Android_アンドロイド";
                }
                else if (gearID.StartsWith("0168")) // 判断开头是否是0168
                {
                    gearNodePath = String.Format(@"Character\Bits\{0:D}.img", gearID);
                    categoryPath = "Bits_拼図";
                }
                else if (gearID.StartsWith("01712")) // 判断开头是否是01712
                {
                    gearNodePath = String.Format(@"Character\ArcaneForce\{0:D}.img", gearID);
                    categoryPath = "Arcane";
                }
                else if (Regex.IsMatch(gearID, "^01713|^01714")) // 判断开头是否是01713或01714
                {
                    gearNodePath = String.Format(@"Character\AuthenticForce\{0:D}.img", gearID);
                    categoryPath = "Authentic";
                }
                else if (Regex.IsMatch(gearID, "^0178"))  // 判断开头是否是0178
                {
                    gearNodePath = String.Format(@"Character\Jewel\{0:D}.img", gearID);
                    categoryPath = "Jewel_宝玉";
                }
                else if (Regex.IsMatch(gearID, "^0179"))  // 判断开头是否是0179
                {
                    gearNodePath = String.Format(@"Character\NT_Beauty\{0:D}.img", gearID);
                    categoryPath = "MSN_Cosmetic_化粧";
                }
                else if (gearID.StartsWith("018")) // 判断开头是否是018
                {
                    gearNodePath = String.Format(@"Character\PetEquip\{0:D}.img", gearID);
                    categoryPath = "PetEquipment_ペット装備";
                }
                else if (Regex.IsMatch(gearID, "^0194|^0195|^0196|^0197")) // 判断开头是否是0194~0197
                {
                    gearNodePath = String.Format(@"Character\Dragon\{0:D}.img", gearID);
                    categoryPath = "EvanDragonEquip_エヴァンドラゴン装備";
                }
                else if (Regex.IsMatch(gearID, "^0190|^0191|^0192|^0193|^0198")) // 判断开头是否是0190~0193或0198
                {
                    gearNodePath = String.Format(@"Character\TamingMob\{0:D}.img", gearID);
                    categoryPath = "TamedMonster_テイムドモンスター";
                }
                else if (Regex.IsMatch(gearID, "^0002|^0005")) // 判断开头是否是0002或0005
                {
                    gearNodePath = String.Format(@"Character\Face\{0:D}.img", gearID);
                    categoryPath = "Cosmetic_化粧";
                }
                else if (Regex.IsMatch(gearID, "^0003|^0004|^0006")) // 判断开头是否是0003、0004或0006
                {
                    gearNodePath = String.Format(@"Character\Hair\{0:D}.img", gearID);
                    categoryPath = "Cosmetic_化粧";
                }

                StringResult sr;
                string EqpName;
                if (gearRenderNewOld[1].StringLinker == null || !gearRenderNewOld[1].StringLinker.StringEqp.TryGetValue(int.Parse(gearID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知の装備";
                }
                EqpName = sr.Name;
                if (gearRenderNewOld[0].StringLinker == null || !gearRenderNewOld[0].StringLinker.StringEqp.TryGetValue(int.Parse(gearID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知の装備";
                }
                if (EqpName != sr.Name && EqpName != "未知の装備" && sr.Name != "未知の装備")
                {
                    EqpName += "_" + sr.Name;
                }
                else if (EqpName == "未知の装備")
                {
                    EqpName = sr.Name;
                }
                if (String.IsNullOrEmpty(EqpName)) EqpName = "未知の装備";
                EqpName = RemoveInvalidFileNameChars(EqpName);
                int nullEqpIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    Gear gear = Gear.CreateFromNode(PluginManager.FindWz(gearNodePath, WzFileNewOld[i]), PluginManager.FindWz);

                    if (gear != null)
                    {
                        gearRenderNewOld[i].Gear = gear;
                    }
                    else
                    {
                        nullEqpIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullEqpIdx)
                {
                    case 0: // change
                        gearType = "変更";

                        Bitmap ImageNew = gearRenderNewOld[0].Render();
                        Bitmap ImageOld = gearRenderNewOld[1].Render();
                        if (GetBitmapHash(ImageNew) == GetBitmapHash(ImageOld)) continue;
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        gearType = "削除";

                        resultImage = gearRenderNewOld[1].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        gearType = "追加";

                        resultImage = gearRenderNewOld[0].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                if (!Directory.Exists(Path.Combine(gearTooltipPath, categoryPath)))
                {
                    Directory.CreateDirectory(Path.Combine(gearTooltipPath, categoryPath));
                }

                var gearTypeTextInfo = g.MeasureString(gearType, GearGraphics.ItemDetailFont);
                int picH = 13;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, gearType, gearTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(gearTypeTextInfo.Width) + 2, ref picH, 10);

                string imageName = Path.Combine(gearTooltipPath, categoryPath, "装備_" + gearID + "_" + EqpName + "_" + gearType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputGearTooltipIDs.Clear();
            DiffGearTags.Clear();
        }


        private void SaveMobTooltip(string mobTooltipPath)
        {
            MobTooltipRenderer[] mobRenderNewOld = new MobTooltipRenderer[2];
            int count = 0;
            int allCount = OutputMobTooltipIDs.Count;
            var mobTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                mobRenderNewOld[i] = new MobTooltipRenderer();
                mobRenderNewOld[i].StringLinker = new StringLinker();
                mobRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                mobRenderNewOld[i].ShowObjectID = this.ShowObjectID;
            }

            foreach (var mobID in OutputMobTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} モンスター: {2}", ++count, allCount, mobID);
                StateDetail = "Mob 変更点をツールチップ画像に出力中...";
                string mobType = "";
                string mobNodePath = String.Format(@"Mob\{0:D}.img", mobID);

                StringResult sr;
                string MobName;
                if (mobRenderNewOld[1].StringLinker == null || !mobRenderNewOld[1].StringLinker.StringMob.TryGetValue(int.Parse(mobID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のモンスター";
                }
                MobName = sr.Name;
                if (mobRenderNewOld[0].StringLinker == null || !mobRenderNewOld[0].StringLinker.StringMob.TryGetValue(int.Parse(mobID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のモンスター";
                }
                if (MobName != sr.Name && MobName != "未知のモンスター" && sr.Name != "未知のモンスター")
                {
                    MobName += "_" + sr.Name;
                }
                else if (MobName == "未知のモンスター")
                {
                    MobName = sr.Name;
                }
                if (String.IsNullOrEmpty(MobName)) MobName = "未知のモンスター";
                MobName = RemoveInvalidFileNameChars(MobName);
                int nullMobIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    Mob mob = Mob.CreateFromNode(PluginManager.FindWz(mobNodePath, WzFileNewOld[i]), PluginManager.FindWz);

                    if (mob != null)
                    {
                        mobRenderNewOld[i].MobInfo = mob;
                    }
                    else
                    {
                        nullMobIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullMobIdx)
                {
                    case 0: // change
                        mobType = "変更";

                        Bitmap ImageNew = mobRenderNewOld[0].Render();
                        Bitmap ImageOld = mobRenderNewOld[1].Render();
                        if (GetBitmapHash(ImageNew) == GetBitmapHash(ImageOld)) continue;
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        mobType = "削除";

                        resultImage = mobRenderNewOld[1].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        mobType = "追加";

                        resultImage = mobRenderNewOld[0].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                var mobTypeTextInfo = g.MeasureString(mobType, GearGraphics.ItemDetailFont);
                int picH = 1;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, mobType, mobTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(mobTypeTextInfo.Width) + 2, ref picH, 10);

                string imageName = Path.Combine(mobTooltipPath, "モンスター_" + mobID + "_" + MobName + "_" + mobType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputMobTooltipIDs.Clear();
            DiffMobTags.Clear();
        }

        private void SaveNpcTooltip(string npcTooltipPath)
        {
            NpcTooltipRenderer[] npcRenderNewOld = new NpcTooltipRenderer[2];
            int count = 0;
            int allCount = OutputNpcTooltipIDs.Count;
            var npcTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                npcRenderNewOld[i] = new NpcTooltipRenderer();
                npcRenderNewOld[i].StringLinker = new StringLinker();
                npcRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                npcRenderNewOld[i].ShowObjectID = this.ShowObjectID;
            }

            foreach (var npcID in OutputNpcTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} NPC: {2}", ++count, allCount, npcID);
                StateDetail = "Npc 変更点をツールチップ画像に出力中...";
                string npcType = "";
                string npcNodePath = String.Format(@"Npc\{0:D}.img", npcID);

                StringResult sr;
                string NpcName;
                if (npcRenderNewOld[1].StringLinker == null || !npcRenderNewOld[1].StringLinker.StringNpc.TryGetValue(int.Parse(npcID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のNPC";
                }
                NpcName = sr.Name;
                if (npcRenderNewOld[0].StringLinker == null || !npcRenderNewOld[0].StringLinker.StringNpc.TryGetValue(int.Parse(npcID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のNPC";
                }
                if (NpcName != sr.Name && NpcName != "未知のNPC" && sr.Name != "未知のNPC")
                {
                    NpcName += "_" + sr.Name;
                }
                else if (NpcName == "未知のNPC")
                {
                    NpcName = sr.Name;
                }
                if (String.IsNullOrEmpty(NpcName)) NpcName = "未知のNPC";
                NpcName = RemoveInvalidFileNameChars(NpcName);
                int nullNpcIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    Npc npc = Npc.CreateFromNode(PluginManager.FindWz(npcNodePath, WzFileNewOld[i]), PluginManager.FindWz);

                    if (npc != null)
                    {
                        npcRenderNewOld[i].NpcInfo = npc;
                    }
                    else
                    {
                        nullNpcIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullNpcIdx)
                {
                    case 0: // change
                        npcType = "変更";

                        Bitmap ImageNew = npcRenderNewOld[0].Render();
                        Bitmap ImageOld = npcRenderNewOld[1].Render();
                        if (GetBitmapHash(ImageNew) == GetBitmapHash(ImageOld)) continue;
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        npcType = "削除";

                        resultImage = npcRenderNewOld[1].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        npcType = "追加";

                        resultImage = npcRenderNewOld[0].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                var npcTypeTextInfo = g.MeasureString(npcType, GearGraphics.ItemDetailFont);
                int picH = 1;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, npcType, npcTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(npcTypeTextInfo.Width) + 2, ref picH, 10);

                string imageName = Path.Combine(npcTooltipPath, "Npc_" + npcID + "_" + NpcName + "_" + npcType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputNpcTooltipIDs.Clear();
            DiffNpcTags.Clear();
        }

        private void SaveCashTooltip(string itemTooltipPath)
        {
            CashPackageTooltipRender[] cashRenderNewOld = new CashPackageTooltipRender[2];
            int count = 0;
            int allCount = OutputCashTooltipIDs.Count;
            var itemTypeFont = new Font("MS Gothic", 11f, GraphicsUnit.Pixel);

            for (int i = 0; i < 2; i++) // 0: New, 1: Old
            {
                this.StringWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("String").GetNodeWzFile();
                this.ItemWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Item").GetNodeWzFile();
                this.EtcWzNewOld[i] = WzNewOld[i]?.FindNodeByPath("Etc").GetNodeWzFile();

                cashRenderNewOld[i] = new CashPackageTooltipRender();
                cashRenderNewOld[i].StringLinker = new StringLinker();
                cashRenderNewOld[i].StringLinker.Load(StringWzNewOld[i], ItemWzNewOld[i], EtcWzNewOld[i]);
                cashRenderNewOld[i].ShowObjectID = this.ShowObjectID;
            }

            foreach (var itemID in OutputCashTooltipIDs)
            {
                StateInfo = string.Format("{0}/{1} パッケージ: {2}", ++count, allCount, itemID);
                StateDetail = "Item 変更点をツールチップ画像に出力中...";
                string itemType = "";
                string itemNodePath = null;

                if (itemID.StartsWith("9")) // 判断第1位是否是09
                {
                    itemNodePath = String.Format(@"Item\Special\0{0:D}.img\{1:D}", int.Parse(itemID) / 10000, itemID);
                }

                StringResult sr;
                string ItemName;
                if (cashRenderNewOld[1].StringLinker == null || !cashRenderNewOld[1].StringLinker.StringItem.TryGetValue(int.Parse(itemID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のパッケージ";
                }
                ItemName = sr.Name;
                if (cashRenderNewOld[0].StringLinker == null || !cashRenderNewOld[0].StringLinker.StringItem.TryGetValue(int.Parse(itemID), out sr))
                {
                    sr = new StringResult();
                    sr.Name = "未知のパッケージ";
                }
                if (ItemName != sr.Name && ItemName != "未知のパッケージ" && sr.Name != "未知のパッケージ")
                {
                    ItemName += "_" + sr.Name;
                }
                else if (ItemName == "未知のパッケージ")
                {
                    ItemName = sr.Name;
                }
                if (String.IsNullOrEmpty(ItemName)) ItemName = "未知のパッケージ";
                ItemName = RemoveInvalidFileNameChars(ItemName);
                int nullItemIdx = 0;

                // 変更前後のツールチップ画像の作成
                for (int i = 0; i < 2; i++) // 0: New, 1: Old
                {
                    CashPackage item = CashPackage.CreateFromNode(PluginManager.FindWz(itemNodePath, WzFileNewOld[i]), PluginBase.PluginManager.FindWz(string.Format(@"Etc\CashPackage.img\{0}", itemID)), PluginManager.FindWz);

                    if (item != null)
                    {
                        cashRenderNewOld[i].CashPackage = item;
                    }
                    else
                    {
                        nullItemIdx = i + 1;
                    }
                }

                // ツールチップ画像を合わせる
                Bitmap resultImage = null;
                Graphics g = null;

                switch (nullItemIdx)
                {
                    case 0: // change
                        itemType = "変更";

                        Bitmap ImageNew = cashRenderNewOld[0].Render();
                        Bitmap ImageOld = cashRenderNewOld[1].Render();
                        if (GetBitmapHash(ImageNew) == GetBitmapHash(ImageOld)) continue;
                        resultImage = new Bitmap(ImageNew.Width + ImageOld.Width, Math.Max(ImageNew.Height, ImageOld.Height));
                        g = Graphics.FromImage(resultImage);

                        g.DrawImage(ImageOld, 0, 0);
                        g.DrawImage(ImageNew, ImageOld.Width, 0);
                        break;

                    case 1: // delete
                        itemType = "削除";

                        resultImage = cashRenderNewOld[1].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    case 2: // add
                        itemType = "追加";

                        resultImage = cashRenderNewOld[0].Render();
                        if (resultImage == null) continue;
                        g = Graphics.FromImage(resultImage);
                        break;

                    default:
                        break;
                }

                if (resultImage == null || g == null)
                {
                    continue;
                }

                var itemTypeTextInfo = g.MeasureString(itemType, GearGraphics.ItemDetailFont);
                int picH = 13;
                if (ShowChangeType) GearGraphics.DrawPlainText(g, itemType, itemTypeFont, Color.FromArgb(255, 255, 255), 2, (int)Math.Ceiling(itemTypeTextInfo.Width) + 2, ref picH, 10);

                string imageName = Path.Combine(itemTooltipPath, "Item_" + itemID + "_" + ItemName + "_" + itemType + ".png");
                if (!File.Exists(imageName))
                {
                    resultImage.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                }
                resultImage.Dispose();
                g.Dispose();
            }
            OutputCashTooltipIDs.Clear();
            DiffCashTags.Clear();
        }


        //異なるItemノードからItemIDを取得する
        private void GetItemID(Wz_Node node)
        {
            if (node == null) return;

            Match match = Regex.Match(node.FullPathToFile, @"^String\\(Cash.img|Consume.img|Etc.img\\Etc|Ins.img|Pet.img)\\(\d+).*");
            string tag = null;
            if (!match.Success)
            {
                tag = node.Text;
                match = Regex.Match(node.FullPathToFile, @"^Item\\(Cash|Consume|Etc|Install)\\\d+.img\\(\d+)\\.*");
            }

            if (match.Success)
            {
                string itemID = match.Groups[2].ToString();

                if (itemID != null)
                {
                    if (!itemID.StartsWith("500"))
                    {
                        itemID = itemID.PadLeft(8, '0');
                    }

                    if (!OutputItemTooltipIDs.Contains(itemID))
                    {
                        OutputItemTooltipIDs.Add(itemID);
                        DiffItemTags[itemID] = new List<string>();
                    }

                    if (tag != null && !DiffItemTags[itemID].Contains(tag))
                    {
                        DiffItemTags[itemID].Add(tag);
                    }
                }
            }
        }

        //異なるItem/SpecialノードからCashIDを取得する
        private void GetCashID(Wz_Node node)
        {
            if (node == null) return;

            Match match = Regex.Match(node.FullPathToFile, @"^Item\\Special.img\\0910.img\\(\d+)\\name");
            string tag = null;
            if (!match.Success)
            {
                tag = node.Text;
                match = Regex.Match(node.FullPathToFile, @"^Item\\Special\\\d+.img\\(\d+)\\.*");
            }

            if (match.Success)
            {
                string cashID = match.Groups[1].ToString();

                if (cashID != null)
                {
                    if (!OutputCashTooltipIDs.Contains(cashID))
                    {
                        OutputCashTooltipIDs.Add(cashID);
                        DiffCashTags[cashID] = new List<string>();
                    }

                    if (tag != null && !DiffCashTags[cashID].Contains(tag))
                    {
                        DiffCashTags[cashID].Add(tag);
                    }
                }
            }
        }

        //異なるCharacterノードからGearIDを取得する
        private void GetGearID(Wz_Node node)
        {
            if (node == null) return;

            Match match = Regex.Match(node.FullPathToFile, @"^String\\Gear.img\\Gear\\\w+\\(\d+).*");
            string tag = null;
            if (!match.Success)
            {
                tag = node.Text;
                match = Regex.Match(node.FullPathToFile, @"^Character\\\w+\\(\d+).img\\.*");
            }

            if (match.Success)
            {
                string gearID = match.Groups[1].ToString();

                if (gearID != null)
                {
                    if (!OutputGearTooltipIDs.Contains(gearID))
                    {
                        OutputGearTooltipIDs.Add(gearID);
                        DiffGearTags[gearID] = new List<string>();
                    }

                    if (tag != null && !DiffGearTags[gearID].Contains(tag))
                    {
                        DiffGearTags[gearID].Add(tag);
                    }
                }
            }
        }

        //異なるMobノードからMobIDを取得する
        private void GetMobID(Wz_Node node)
        {
            if (node == null) return;

            Match match = Regex.Match(node.FullPathToFile, @"^String\\Mob.img\\(\d+).*");
            string tag = null;
            if (!match.Success)
            {
                tag = node.Text;
                match = Regex.Match(node.FullPathToFile, @"^Mob\\(\d+).img\\.*");
            }

            if (match.Success)
            {
                string gearID = match.Groups[1].ToString();

                if (gearID != null)
                {
                    if (!OutputMobTooltipIDs.Contains(gearID))
                    {
                        OutputMobTooltipIDs.Add(gearID);
                        DiffMobTags[gearID] = new List<string>();
                    }

                    if (tag != null && !DiffMobTags[gearID].Contains(tag))
                    {
                        DiffMobTags[gearID].Add(tag);
                    }
                }
            }
        }

        //異なるNPCノードからNpcIDを取得する
        private void GetNpcID(Wz_Node node)
        {
            if (node == null) return;

            Match match = Regex.Match(node.FullPathToFile, @"^String\\Npc.img\\(\d+).*");
            string tag = null;
            if (!match.Success)
            {
                tag = node.Text;
                match = Regex.Match(node.FullPathToFile, @"^Npc\\(\d+).img\\.*");
            }

            if (match.Success)
            {
                string gearID = match.Groups[1].ToString();

                if (gearID != null)
                {
                    if (!OutputNpcTooltipIDs.Contains(gearID))
                    {
                        OutputNpcTooltipIDs.Add(gearID);
                        DiffNpcTags[gearID] = new List<string>();
                    }

                    if (tag != null && !DiffNpcTags[gearID].Contains(tag))
                    {
                        DiffNpcTags[gearID].Add(tag);
                    }
                }
            }
        }

        private void CompareImg(Wz_Image imgNew, Wz_Image imgOld, string imgName, string anchorName, string menuAnchorName, string outputDir, StreamWriter sw)
        {
            StateDetail = "Extracting IMG";
            if (!imgNew.TryExtract() || !imgOld.TryExtract())
                return;
            StateDetail = "Comparing IMG";
            List<CompareDifference> diffList = new List<CompareDifference>(Comparer.Compare(imgNew.Node, imgOld.Node));
            StringBuilder sb = new StringBuilder();
            int[] count = new int[3];
            StateDetail = "Total of " + diffList.Count + " changes";
            foreach (var diff in diffList)
            {
                int idx = -1;
                string col0 = null;
                switch (diff.DifferenceType)
                {
                    case DifferenceType.Changed:
                        idx = 0;
                        col0 = diff.NodeNew.FullPath;
                        break;
                    case DifferenceType.Append:
                        idx = 1;
                        col0 = diff.NodeNew.FullPath;
                        break;
                    case DifferenceType.Remove:
                        idx = 2;
                        col0 = diff.NodeOld.FullPath;
                        break;
                }
                sb.AppendFormat("<tr class=\"r{0}\">", idx);
                sb.AppendFormat("<td>{0}</td>", col0 ?? " ");
                sb.AppendFormat("<td>{0}</td>", OutputNodeValue(col0, diff.NodeOld, 1, outputDir) ?? " ");
                sb.AppendFormat("<td>{0}</td>", OutputNodeValue(col0, diff.NodeNew, 0, outputDir) ?? " ");
                sb.AppendLine("</tr>");
                count[idx]++;

                // 변경된 스킬 툴팁 출력
                if (OutputSkillTooltip && (outputDir.Contains("Skill") || outputDir.Contains("String")))
                {
                    GetSkillID(diff.NodeNew, idx == 0 ? true : false);
                    GetSkillID(diff.NodeOld, idx == 0 ? true : false);
                }
                // 変更的道具Tooltip处理
                if (OutputItemTooltip && (outputDir.Contains("Item") || outputDir.Contains("String")))
                {
                    GetItemID(diff.NodeNew);
                    GetItemID(diff.NodeOld);
                }
                // 変更的装备Tooltip处理
                if (OutputGearTooltip && (outputDir.Contains("Character") || outputDir.Contains("String")))
                {
                    GetGearID(diff.NodeNew);
                    GetGearID(diff.NodeOld);
                }
                // 変更的怪物Tooltip处理
                if (OutputMobTooltip && (outputDir.Contains("Mob") || outputDir.Contains("String")))
                {
                    GetMobID(diff.NodeNew);
                    GetMobID(diff.NodeOld);
                }
                // 変更的Npc Tooltip处理
                if (OutputNpcTooltip && (outputDir.Contains("Npc") || outputDir.Contains("String")))
                {
                    GetNpcID(diff.NodeNew);
                    GetNpcID(diff.NodeOld);
                }
                // 変更的礼包Tooltip处理
                if (OutputCashTooltip && (outputDir.Contains("Item") || outputDir.Contains("String")))
                {
                    GetCashID(diff.NodeNew);
                    GetCashID(diff.NodeOld);
                }
            }
            StateDetail = "ファイルを出力中";
            bool noChange = diffList.Count <= 0;
            sw.WriteLine("<table class=\"img{0}\">", noChange ? " noChange" : "");
            sw.WriteLine("<tr><th colspan=\"3\"><a name=\"{1}\">{0}</a>: 変更: {2}; 追加: {3}; 削除: {4}</th></tr>",
                imgName, anchorName, count[0], count[1], count[2]);
            sw.WriteLine(sb.ToString());
            sw.WriteLine("<tr><td colspan=\"3\"><a href=\"#{1}\">{0}</a></td></tr>", "Go Back", menuAnchorName);
            sw.WriteLine("</table>");
            imgNew.Unextract();
            imgOld.Unextract();
            sb = null;
        }

        private void OutputImg(Wz_Image img, DifferenceType diffType, string imgName, string anchorName, string menuAnchorName, string outputDir, StreamWriter sw)
        {
            StateDetail = "IMGを抽出中";
            if (!img.TryExtract())
                return;

            int idx = 0; ;
            switch (diffType)
            {
                case DifferenceType.Changed:
                    idx = 0;
                    break;
                case DifferenceType.Append:
                    idx = 1;
                    break;
                case DifferenceType.Remove:
                    idx = 2;
                    break;
            }
            Action<Wz_Node> fnOutput = null;
            fnOutput = node =>
            {
                if (node != null)
                {
                    string fullPath = node.FullPath;
                    sw.Write("<tr class=\"r{0}\">", idx);
                    sw.Write("<td>{0}</td>", fullPath ?? " ");
                    sw.Write("<td>{0}</td>", OutputNodeValue(fullPath, node, 0, outputDir) ?? " ");
                    sw.WriteLine("</tr>");

                    // 변경된 스킬 툴팁 출력
                    if (OutputSkillTooltip && (outputDir.Contains("Skill") || outputDir.Contains("String")))
                    {
                        GetSkillID(node, idx == 0 ? true : false);
                    }
                    if (OutputItemTooltip && outputDir.Contains("Item"))
                    {
                        GetItemID(node);
                    }
                    if (OutputGearTooltip && outputDir.Contains("Character"))
                    {
                        GetGearID(node);
                    }
                    if (OutputMobTooltip && outputDir.Contains("Mob"))
                    {
                        GetMobID(node);
                    }
                    if (OutputNpcTooltip && outputDir.Contains("Npc"))
                    {
                        GetNpcID(node);
                    }


                    if (node.Nodes.Count > 0)
                    {
                        foreach (Wz_Node child in node.Nodes)
                        {
                            fnOutput(child);
                        }
                    }
                }
            };

            StateDetail = "IMG構造を作成中";
            sw.WriteLine("<table class=\"img\">");
            sw.WriteLine("<tr><th colspan=\"2\"><a name=\"{1}\">{0}</a></th></tr>", imgName, anchorName);
            fnOutput(img.Node);
            sw.WriteLine("<tr><td colspan=\"2\"><a href=\"#{1}\">{0}</a></td></tr>", "Go Back", menuAnchorName);
            sw.WriteLine("</table>");
            img.Unextract();
        }

        protected virtual string OutputNodeValue(string fullPath, Wz_Node value, int col, string outputDir)
        {
            if (value == null)
                return null;

            Wz_Node linkNode;
            if ((linkNode = value.GetLinkedSourceNode(path => PluginBase.PluginManager.FindWz(path, value.GetNodeWzFile()))) != value)
            {
                return "(link) " + OutputNodeValue(fullPath, linkNode, col, outputDir);
            }

            switch (value.Value)
            {
                case Wz_Png png:
                    if (OutputPng)
                    {
                        char[] invalidChars = Path.GetInvalidFileNameChars();
                        string colName = col == 0 ? "new" : (col == 1 ? "old" : col.ToString());
                        string fileName = fullPath.Replace('\\', '.');
                        string suffix = "_" + colName + ".png";
                        string canvas = "_Canvas";

                        if (this.HashPngFileName)
                        {
                            fileName = ToHexString(MD5Hash(fileName));
                            // TODO: save file name mapping to another file?
                        }
                        else
                        {
                            for (int i = 0; i < invalidChars.Length; i++)
                            {
                                fileName = fileName.Replace(invalidChars[i], '_');
                            }
                            if (outputDir.Length + fileName.Length > 240)
                            {
                                fileName = fileName.Substring(0, 40) + "_" + ToHexString(MD5Hash(fileName)).Substring(0, 8);
                            }
                        }

                        fileName = fileName + suffix;
                        string outputDirName = new DirectoryInfo(outputDir).Name;
                        bool isCanvas = fileName.Contains(canvas);
                        if (isCanvas)
                        {
                            outputDir = Path.Combine(outputDir, canvas);
                            if (!Directory.Exists(outputDir))
                            {
                                Directory.CreateDirectory(outputDir);
                            }
                        }
                        using (Bitmap bmp = png.ExtractPng())
                        {
                            bmp.Save(Path.Combine(outputDir, fileName), System.Drawing.Imaging.ImageFormat.Png);
                        }
                        return string.Format("<img src=\"{0}/{1}\" />", isCanvas ? Path.Combine(outputDirName, canvas) : outputDirName, WebUtility.UrlEncode(fileName));
                    }
                    else
                    {
                        return string.Format("PNG {0}*{1} ({2}B)", png.Width, png.Height, png.DataLength);
                    }

                case Wz_Uol uol:
                    return "(uol) " + uol.Uol;

                case Wz_Vector vector:
                    return string.Format("({0}, {1})", vector.X, vector.Y);

                case Wz_Sound sound:
                    if (OutputPng)
                    {
                        char[] invalidChars = Path.GetInvalidFileNameChars();
                        string colName = col == 0 ? "new" : (col == 1 ? "old" : col.ToString());
                        string filePath = fullPath.Replace('\\', '.') + "_" + colName + ".mp3";

                        for (int i = 0; i < invalidChars.Length; i++)
                        {
                            filePath = filePath.Replace(invalidChars[i].ToString(), null);
                        }

                        byte[] mp3 = sound.ExtractSound();
                        if (mp3 != null)
                        {
                            FileStream fileStream = new FileStream(Path.Combine(outputDir, filePath), FileMode.Create, FileAccess.Write);
                            fileStream.Write(mp3, 0, mp3.Length);
                            fileStream.Close();
                        }
                        return string.Format("<audio controls src=\"{0}\" type=\"audio/mpeg\">audio {1} ms\n</audio>", Path.Combine(new DirectoryInfo(outputDir).Name, filePath), sound.Ms);
                    }
                    else
                    {
                        return string.Format("audio {0} ms", sound.Ms);
                    }

                case Wz_Convex convex:
                    return string.Format("convex {0}", string.Join(" ", convex.Points.Select(vec => $"({vec.X},{vec.Y})")));

                case Wz_RawData rawData:
                    return string.Format("rawdata {0} bytes", rawData.Length);

                case Wz_Video video:
                    return string.Format("video {0} bytes", video.Length);

                case Wz_Image _:
                    return "{ img }";

                default:
                    return string.Format("<span title=\"{0}\">{1}</span>", value.GetType().Name, WebUtility.HtmlEncode(Convert.ToString(value)));
            }
        }

        public virtual void CreateStyleSheet(string outputDir)
        {
            string path = Path.Combine(outputDir, "style.css");
            if (File.Exists(path))
                return;
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            if (EnableDarkMode)
            {

                sw.WriteLine("body { font-size:12px; background-color:black; color:white; }");
                sw.WriteLine("a { color:white; }");
                sw.WriteLine("p.wzf { }");
                sw.WriteLine("table, tr, th, td { border:1px solid #ff8000; border-collapse:collapse; }");
                sw.WriteLine("table { margin-bottom:16px; }");
                sw.WriteLine("th { text-align:left; }");
                sw.WriteLine("table.lst0 { }");
                sw.WriteLine("table.lst1 { }");
                sw.WriteLine("table.lst2 { }");
                sw.WriteLine("table.img { }");
                sw.WriteLine("table.img tr.r0 { background-color:#003049; }");
                sw.WriteLine("table.img tr.r1 { background-color:#000000; }");
                sw.WriteLine("table.img tr.r2 { background-color:#462306; }");
                sw.WriteLine("table.img.noChange { display:none; }");
            }
            else
            {
                sw.WriteLine("body { font-size:12px; background-color:#101010; color:#ffffff }");
                sw.WriteLine("p.wzf { }");
                sw.WriteLine("table, tr, th, td { border:2px solid #000000; border-collapse:collapse; }");
                sw.WriteLine("table { margin-bottom:16px; }");
                sw.WriteLine("th { text-align:left; }");
                sw.WriteLine("table.lst0 { background-color:#101010; }");
                sw.WriteLine("table.lst0 a:link { color:#ffffff }");
                sw.WriteLine("table.lst0 a:visited { color:#ffffff }");
                sw.WriteLine("table.lst0 a:hover { color:#ffffff }");
                sw.WriteLine("table.lst0 a:activated { color:#ffffff }");
                sw.WriteLine("table.lst1 { background-color:#101010; color: #ffffff; }");
                sw.WriteLine("table.lst2 { background-color:#101010; color: #ffffff; }");
                sw.WriteLine("table.img tr.r0 { background-color:#CCCC00; color:#000000; }");
                sw.WriteLine("table.img tr.r1 { background-color:#154211; }");
                sw.WriteLine("table.img tr.r2 { background-color:#961e1e; }");
                sw.WriteLine("table.img.noChange { display:none; }");
            }
            sw.Flush();
            sw.Close();
        }

        private static byte[] MD5Hash(string text)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            }
        }

        private static string ToHexString(byte[] inArray)
        {
            StringBuilder hex = new StringBuilder(inArray.Length * 2);
            foreach (byte b in inArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        private static string RemoveInvalidFileNameChars(string fileName)
        {
            string invalidChars = new string(System.IO.Path.GetInvalidFileNameChars());
            string regexPattern = $"[{Regex.Escape(invalidChars)}]";
            return Regex.Replace(fileName, regexPattern, "_");
        }

        private static string GetBitmapHash(Bitmap bitmap)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Lock bits for direct memory access
                BitmapData bmpData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    bitmap.PixelFormat);

                try
                {
                    // Get the raw pixel data
                    int byteCount = Math.Abs(bmpData.Stride) * bitmap.Height;
                    byte[] pixelBuffer = new byte[byteCount];
                    System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelBuffer, 0, byteCount);

                    // Compute the hash from pixel data
                    byte[] hashBytes = sha256.ComputeHash(pixelBuffer);

                    // Convert hash to string
                    return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                }
                finally
                {
                    // Unlock bits
                    bitmap.UnlockBits(bmpData);
                }
            }
        }
    }
}