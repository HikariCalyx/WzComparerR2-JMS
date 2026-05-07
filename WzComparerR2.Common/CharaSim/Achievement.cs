using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WzComparerR2.WzLib;

namespace WzComparerR2.CharaSim
{
    public class Achievement
    {
        public Achievement()
        {
            this.ID = -1;
            this.PriorIDs = new List<int>();
            this.Missions = new List<string>();
            this.Rewards = new List<AchievementReward>();
        }

        private string _mainCategory { get; set; }
        private string _subCategory { get; set; }
        public int ID { get; set; }
        public int Score { get; set; }
        public string MainCategory
        {
            get { return GetMainCategoryStr(); }
        }
        public string SubCategory
        {
            get { return GetSubCategoryStr(); }
        }
        public string Difficulty { get; set; }
        public string UiForm { get; set; }
        public string Block { get; set; }
        public string PriorCondition { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public List<int> PriorIDs { get; set; }
        public List<string> Missions { get; set; }
        public List<AchievementReward> Rewards { get; set; }

        public bool ShowMissions
        {
            get
            {
                return (this.UiForm == "mission" || this.UiForm == "all")
                    && this.Missions.Count > 0;
            }
        }
        public bool HasRewards
        {
            get { return this.Rewards.Count > 0; }
        }
        public bool Hide
        {
            get { return this.Block == "hide"; }
        }

        public static Achievement CreateFromNode(
            Wz_Node node,
            GlobalFindNodeFunction findNode,
            GlobalFindNodeFunction2 findNode2,
            Wz_File wzf = null
        )
        {
            if (node == null)
                return null;

            Match m = Regex.Match(node.Text, @"^(\d+)\.img$");
            if (!(m.Success && Int32.TryParse(m.Result("$1"), out int achievementID)))
            {
                return null;
            }

            Achievement achievement = new Achievement();
            achievement.ID = achievementID;
            Wz_Node infoNode = node.FindNodeByPath("info").ResolveUol();
            if (infoNode != null)
            {
                foreach (var propNode in infoNode.Nodes)
                {
                    switch (propNode.Text)
                    {
                        case "score":
                            achievement.Score = propNode.GetValueEx<int>(0);
                            break;
                        case "mainCategory":
                            achievement._mainCategory = propNode.GetValueEx<string>(null);
                            break;
                        case "subCategory":
                            achievement._subCategory = propNode.GetValueEx<string>(null);
                            break;
                        case "difficulty":
                            achievement.Difficulty = propNode.GetValueEx<string>("normal");
                            break;
                        case "prior":
                            var prior = propNode.FindNodeByPath("achievement_id");
                            if (prior != null)
                                achievement.PriorIDs.Add(prior.GetValueEx<int>(-1));
                            else
                            {
                                var valueNode = propNode.FindNodeByPath("values");
                                foreach (
                                    var value in valueNode?.Nodes
                                        ?? new Wz_Node.WzNodeCollection(null)
                                )
                                {
                                    prior = value.FindNodeByPath("achievement_id");
                                    var priorID = prior.GetValueEx<int>(-1);
                                    if (priorID > -1)
                                    {
                                        achievement.PriorIDs.Add(prior.GetValueEx<int>(priorID));
                                    }
                                }
                            }
                            achievement.PriorCondition = propNode
                                .FindNodeByPath("condition")
                                .GetValueEx<string>(null);
                            break;
                        case "uiType":
                            achievement.UiForm = propNode
                                .FindNodeByPath("uiForm")
                                .GetValueEx<string>("basic");
                            break;
                        case "block":
                            achievement.Block = propNode.GetValueEx<string>("none");
                            break;
                        case "period":
                            achievement.Start = propNode
                                .FindNodeByPath("start")
                                .GetValueEx<string>(null);
                            achievement.End = propNode
                                .FindNodeByPath("end")
                                .GetValueEx<string>(null);
                            break;
                    }
                }
            }

            Wz_Node missionNode = node.FindNodeByPath("mission").ResolveUol();
            if (missionNode != null)
            {
                foreach (var mission in missionNode.Nodes)
                {
                    var missionName = mission.FindNodeByPath("name").GetValueEx<string>(null);
                    if (!string.IsNullOrEmpty(missionName))
                    {
                        achievement.Missions.Add(missionName);
                    }
                }
            }

            Wz_Node rewardNode = node.FindNodeByPath("reward").ResolveUol();
            if (rewardNode != null)
            {
                foreach (var reward in rewardNode.Nodes)
                {
                    var id = reward.FindNodeByPath("id").GetValueEx<int>(-1);
                    var desc = reward.FindNodeByPath("desc").GetValueEx<string>(null);
                    if (id >= 0 && !string.IsNullOrEmpty(desc))
                    {
                        achievement.Rewards.Add(new AchievementReward() { ID = id, Desc = desc });
                    }
                }
            }

            return achievement;
        }

        private string GetMainCategoryStr()
        {
            // Etc/Achievement/AchievementInfo.img/Category
            switch (this._mainCategory)
            {
                case "general":
                    return "一般";
                case "growth":
                    return "育成";
                case "job":
                    return "職業";
                case "item":
                    return "アイテム";
                case "adventure":
                    return "冒険";
                case "battle":
                    return "戦闘";
                case "social":
                    return "ソーシャル";
                case "event":
                    return "イベント";
                case "memory":
                    return "思い出";

                default:
                    return this._mainCategory;
            }
        }

        private string GetSubCategoryStr()
        {
            // Etc/Achievement/AchievementInfo.img/Category
            switch (this._mainCategory)
            {
                case "general":
                case "event":
                case "memory":
                    return null;
            }

            switch (this._subCategory)
            {
                case "level":
                    return "レベル";
                case "stat":
                    return "能力値";
                case "personality":
                    return "性向";
                case "makingSkill":
                    return "専業技術";
                case "union":
                    return "ユニオン";

                case "story":
                    return "ストーリー";
                case "jobChange":
                    return "転職";
                case "skill":
                    return "スキル";
                case "vMatrix":
                    return "Vマトリックス";
                case "linkSkill":
                    return "リンクスキル";

                case "collection":
                    return "収集";
                case "enchantment":
                    return "強化";
                case "equip":
                    return "着用";

                case "exploration":
                    return "探検";
                case "quest":
                    return "クエスト";
                case "cooperation":
                    return "協同";
                case "special":
                    return "スペシャル";

                case "field":
                    return "フィールド";
                case "boss":
                    return "ボス";
                case "loot":
                    return "戦利品";

                case "party":
                    return "パーティー";
                case "guild":
                    return "ギルド";
                case "trade":
                    return "取引";
                case "etc":
                    return "その他";

                case "progress":
                    return "進行中のイベント";
                case "complete":
                    return "終了したイベント";

                default:
                    return this._subCategory;
            }
        }

        public struct AchievementReward
        {
            public int ID;
            public string Desc;
        }
    }
}
