using System;
using System.Collections.Generic;
using System.Text;
using WzComparerR2.WzLib;

namespace WzComparerR2.Common
{
    public class StringLinker
    {
        public StringLinker()
        {
            stringEqp = new Dictionary<int, StringResult>();
            stringItem = new Dictionary<int, StringResult>();
            stringMap = new Dictionary<int, StringResult>();
            stringMob = new Dictionary<int, StringResult>();
            stringNpc = new Dictionary<int, StringResult>();
            stringSkill = new Dictionary<int, StringResult>();
            stringSkill2 = new Dictionary<string, StringResult>();
            stringSetItem = new Dictionary<int, StringResult>();
        }

        public bool Load(Wz_File stringWz, Wz_File itemWz, Wz_File etcWz)
        {
            if (stringWz == null || stringWz.Node == null || itemWz == null || itemWz.Node == null || etcWz == null || etcWz.Node == null)
                return false;
            this.Clear();
            int id;
            foreach (Wz_Node node in stringWz.Node.Nodes)
            {
                Wz_Image image = node.Value as Wz_Image;
                if (image == null)
                    continue;
                switch (node.Text)
                {
                    case "Pet.img":
                    case "Cash.img":
                    case "Ins.img":
                    case "Consume.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree in image.Node.Nodes)
                        {
                            Wz_Node test_tree = tree;
                            if (test_tree.Value is Wz_Uol)
                            {
                                Wz_Uol uol = test_tree.Value as Wz_Uol;
                                Wz_Node uolNode = uol.HandleUol(tree);
                                if (uolNode != null)
                                {
                                    test_tree = uolNode;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (Int32.TryParse(tree.Text, out id))
                            {
                                StringResult strResult = new StringResult();
                                strResult.Name = GetDefaultString(test_tree, "name");
                                strResult.Desc = GetDefaultString(test_tree, "desc");
                                strResult.AutoDesc = GetDefaultString(test_tree, "autodesc");
                                if (tree.FullPath == test_tree.FullPath)
                                {
                                    strResult.FullPath = tree.FullPath;
                                }
                                else
                                {
                                    strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                }

                                AddAllValue(strResult, test_tree);
                                stringItem[id] = strResult;
                            }
                        }
                        break;
                    case "Etc.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree0 in image.Node.Nodes)
                        {
                            foreach (Wz_Node tree in tree0.Nodes)
                            {
                                Wz_Node test_tree = tree;
                                if (test_tree.Value is Wz_Uol)
                                {
                                    Wz_Uol uol = test_tree.Value as Wz_Uol;
                                    Wz_Node uolNode = uol.HandleUol(tree);
                                    if (uolNode != null)
                                    {
                                        test_tree = uolNode;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (Int32.TryParse(tree.Text, out id))
                                {
                                    StringResult strResult = new StringResult();
                                    strResult.Name = GetDefaultString(test_tree, "name");
                                    strResult.Desc = GetDefaultString(test_tree, "desc");
                                    if (tree.FullPath == test_tree.FullPath)
                                    {
                                        strResult.FullPath = tree.FullPath;
                                    }
                                    else
                                    {
                                        strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                    }

                                    AddAllValue(strResult, test_tree);
                                    stringItem[id] = strResult;
                                }
                            }
                        }
                        break;
                    case "Mob.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree in image.Node.Nodes)
                        {
                            Wz_Node test_tree = tree;
                            if (test_tree.Value is Wz_Uol)
                            {
                                Wz_Uol uol = test_tree.Value as Wz_Uol;
                                Wz_Node uolNode = uol.HandleUol(tree);
                                if (uolNode != null)
                                {
                                    test_tree = uolNode;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (Int32.TryParse(tree.Text, out id))
                            {
                                StringResult strResult = new StringResult();
                                strResult.Name = GetDefaultString(test_tree, "name");
                                if (tree.FullPath == test_tree.FullPath)
                                {
                                    strResult.FullPath = tree.FullPath;
                                }
                                else
                                {
                                    strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                }

                                AddAllValue(strResult, test_tree);
                                stringMob[id] = strResult;
                            }
                        }
                        break;
                    case "Npc.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree in image.Node.Nodes)
                        {
                            Wz_Node test_tree = tree;
                            if (test_tree.Value is Wz_Uol)
                            {
                                Wz_Uol uol = test_tree.Value as Wz_Uol;
                                Wz_Node uolNode = uol.HandleUol(tree);
                                if (uolNode != null)
                                {
                                    test_tree = uolNode;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (Int32.TryParse(tree.Text, out id))
                            {
                                StringResult strResult = new StringResult();
                                strResult.Name = GetDefaultString(test_tree, "name");
                                strResult.Desc = GetDefaultString(test_tree, "func");
                                if (tree.FullPath == test_tree.FullPath)
                                {
                                    strResult.FullPath = tree.FullPath;
                                }
                                else
                                {
                                    strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                }

                                AddAllValue(strResult, test_tree);
                                stringNpc[id] = strResult;
                            }
                        }
                        break;
                    case "Map.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree0 in image.Node.Nodes)
                        {
                            foreach (Wz_Node tree in tree0.Nodes)
                            {
                                Wz_Node test_tree = tree;
                                if (test_tree.Value is Wz_Uol)
                                {
                                    Wz_Uol uol = test_tree.Value as Wz_Uol;
                                    Wz_Node uolNode = uol.HandleUol(tree);
                                    if (uolNode != null)
                                    {
                                        test_tree = uolNode;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (Int32.TryParse(tree.Text, out id))
                                {
                                    StringResult strResult = new StringResult();
                                    strResult.Name = string.Format("{0} : {1}",
                                        GetDefaultString(test_tree, "streetName"),
                                        GetDefaultString(tree, "mapName"));
                                    strResult.Desc = GetDefaultString(test_tree, "mapDesc");
                                    if (tree.FullPath == test_tree.FullPath)
                                    {
                                        strResult.FullPath = tree.FullPath;
                                    }
                                    else
                                    {
                                        strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                    }

                                    AddAllValue(strResult, test_tree);
                                    stringMap[id] = strResult;
                                }
                            }
                        }
                        break;
                    case "Skill.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree in image.Node.Nodes)
                        {
                            Wz_Node test_tree = tree;
                            if (test_tree.Value is Wz_Uol)
                            {
                                Wz_Uol uol = test_tree.Value as Wz_Uol;
                                Wz_Node uolNode = uol.HandleUol(tree);
                                if (uolNode != null)
                                {
                                    test_tree = uolNode;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            StringResult strResult = new StringResultSkill();
                            strResult.Name = GetDefaultString(test_tree, "name");//?? GetDefaultString(tree, "bookName");
                            strResult.Desc = GetDefaultString(test_tree, "desc");
                            strResult.Pdesc = GetDefaultString(test_tree, "pdesc");
                            strResult.SkillH.Add(GetDefaultString(test_tree, "h"));
                            strResult.SkillpH.Add(GetDefaultString(test_tree, "ph"));
                            strResult.SkillhcH.Add(GetDefaultString(test_tree, "hch"));
                            if (strResult.SkillH[0] == null)
                            {
                                strResult.SkillH.RemoveAt(0);
                                for (int i = 1; ; i++)
                                {
                                    string hi = GetDefaultString(test_tree, "h" + i);
                                    if (string.IsNullOrEmpty(hi))
                                        break;
                                    strResult.SkillH.Add(hi);
                                }
                            }
                            strResult.SkillH.TrimExcess();
                            strResult.SkillpH.TrimExcess();
                            if (tree.FullPath == test_tree.FullPath)
                            {
                                strResult.FullPath = tree.FullPath;
                            }
                            else
                            {
                                strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                            }

                            AddAllValue(strResult, test_tree);
                            if (test_tree.Text.Length >= 7 && Int32.TryParse(test_tree.Text, out id))
                            {
                                stringSkill[id] = strResult;
                            }
                            stringSkill2[test_tree.Text] = strResult;
                        }
                        break;
                    case "Eqp.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree0 in image.Node.Nodes)
                        {
                            foreach (Wz_Node tree1 in tree0.Nodes)
                            {
                                foreach (Wz_Node tree in tree1.Nodes)
                                {
                                    Wz_Node test_tree = tree;
                                    if (test_tree.Value is Wz_Uol)
                                    {
                                        Wz_Uol uol = test_tree.Value as Wz_Uol;
                                        Wz_Node uolNode = uol.HandleUol(tree);
                                        if (uolNode != null)
                                        {
                                            test_tree = uolNode;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (Int32.TryParse(tree.Text, out id))
                                    {
                                        StringResult strResult = new StringResult();
                                        strResult.Name = GetDefaultString(test_tree, "name");
                                        strResult.Desc = GetDefaultString(test_tree, "desc");
                                        if (tree.FullPath == test_tree.FullPath)
                                        {
                                            strResult.FullPath = tree.FullPath;
                                        }
                                        else
                                        {
                                            strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                        }

                                        AddAllValue(strResult, test_tree);
                                        stringEqp[id] = strResult;
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            foreach (Wz_Node node in itemWz.Node.FindNodeByPath("Special").Nodes)
            {
                Wz_Image image = node.Value as Wz_Image;
                if (image == null)
                    continue;
                switch (node.Text)
                {
                    case "0910.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree in image.Node.Nodes)
                        {
                            Wz_Node test_tree = tree;
                            if (test_tree.Value is Wz_Uol)
                            {
                                Wz_Uol uol = test_tree.Value as Wz_Uol;
                                Wz_Node uolNode = uol.HandleUol(tree);
                                if (uolNode != null)
                                {
                                    test_tree = uolNode;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (Int32.TryParse(tree.Text, out id))
                            {
                                StringResult strResult = new StringResult();
                                strResult.Name = GetDefaultString(tree, "name");
                                strResult.Desc = GetDefaultString(tree, "desc");
                                if (tree.FullPath == test_tree.FullPath)
                                {
                                    strResult.FullPath = tree.FullPath;
                                }
                                else
                                {
                                    strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                }

                                AddAllValue(strResult, test_tree);
                                stringItem[id] = strResult;
                            }
                        }
                        break;
                }
            }

            foreach (Wz_Node node in etcWz.Node.Nodes)
            {
                Wz_Image image = node.Value as Wz_Image;
                if (image == null)
                    continue;
                switch (node.Text)
                {
                    case "SetItemInfo.img":
                        if (!image.TryExtract()) break;
                        foreach (Wz_Node tree in image.Node.Nodes)
                        {
                            Wz_Node test_tree = tree;
                            if (test_tree.Value is Wz_Uol)
                            {
                                Wz_Uol uol = test_tree.Value as Wz_Uol;
                                Wz_Node uolNode = uol.HandleUol(tree);
                                if (uolNode != null)
                                {
                                    test_tree = uolNode;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (Int32.TryParse(tree.Text, out id))
                            {
                                StringResult strResult = new StringResult();
                                strResult.Name = GetDefaultString(test_tree, "setItemName");
                                if (tree.FullPath == test_tree.FullPath)
                                {
                                    strResult.FullPath = tree.FullPath;
                                }
                                else
                                {
                                    strResult.FullPath = tree.FullPath + " -> " + test_tree.FullPath;
                                }

                                AddAllValue(strResult, test_tree);
                                stringSetItem[id] = strResult;
                            }
                        }
                        break;
                }
            }

            return this.HasValues;
        }

        public void Clear()
        {
            stringEqp.Clear();
            stringItem.Clear();
            stringMob.Clear();
            stringMap.Clear();
            stringNpc.Clear();
            stringSkill.Clear();
            stringSkill2.Clear();
            stringSetItem.Clear();
        }

        public bool HasValues
        {
            get
            {
                return (stringEqp.Count + stringItem.Count + stringMap.Count +
                    stringMob.Count + stringNpc.Count + stringSkill.Count + stringSetItem.Count > 0);
            }
        }

        private Dictionary<int, StringResult> stringEqp;
        private Dictionary<int, StringResult> stringItem;
        private Dictionary<int, StringResult> stringMap;
        private Dictionary<int, StringResult> stringMob;
        private Dictionary<int, StringResult> stringNpc;
        private Dictionary<int, StringResult> stringSkill;
        private Dictionary<string, StringResult> stringSkill2;
        private Dictionary<int, StringResult> stringSetItem;

        private string GetDefaultString(Wz_Node node, string searchNodeText)
        {
            node = node.FindNodeByPath(searchNodeText);
            return node == null ? null : Convert.ToString(node.Value);
        }

        private void AddAllValue(StringResult sr, Wz_Node node)
        {
            foreach (Wz_Node child in node.Nodes)
            {
                if (child.Value != null)
                {
                    sr[child.Text] = child.GetValue<string>();
                }
            }
        }

        public Dictionary<int, StringResult> StringEqp
        {
            get { return stringEqp; }
        }

        public Dictionary<int, StringResult> StringItem
        {
            get { return stringItem; }
        }

        public Dictionary<int, StringResult> StringMap
        {
            get { return stringMap; }
        }

        public Dictionary<int, StringResult> StringMob
        {
            get { return stringMob; }
        }

        public Dictionary<int, StringResult> StringNpc
        {
            get { return stringNpc; }
        }

        public Dictionary<int, StringResult> StringSkill
        {
            get { return stringSkill; }
        }

        public Dictionary<string, StringResult> StringSkill2
        {
            get { return stringSkill2; }
        }

        public Dictionary<int, StringResult> StringSetItem
        {
            get { return stringSetItem; }
        }
    }
}
