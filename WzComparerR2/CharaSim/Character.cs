﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WzComparerR2.CharaSim
{
    public class Character
    {
        public Character()
        {
            this.Name = "光卡的拉羊羊";
            this.status = new CharacterStatus();
            this.status.Pop = 627;
            this.status.Job = 16001;
            this.status.Level = 286;
            this.status.MaxHP.BaseVal = 56517;
            this.status.HP = 56517;
            this.status.MaxMP.BaseVal = 53441;
            this.status.MP = 53441;
            this.status.SpecialValue.BaseVal = 0;
            this.status.combatPower.BaseVal = 187371842;
            this.status.attackRange.BaseVal = 48240839;

            this.status.Strength.BaseVal = 2278;
            this.status.Dexterity.BaseVal = 2206;
            this.status.Intelligence.BaseVal = 43515;
            this.status.Luck.BaseVal = 3367;
            this.status.UnionLevel = 9376;
            this.status.DojoFloor = 65;

            this.status.CriticalRate.BaseVal = 89;
            this.status.MoveSpeed.BaseVal = 155;
            this.status.Jump.BaseVal = 123;
            this.status.CriticalDamage.BaseVal = 72;

            this.status.PADamage.BaseVal = 1198;
            this.status.MADamage.BaseVal = 4198;

            this.status.NormalMonsterDamR.BaseVal = 12;
            this.status.CooltimeReduceSecond.BaseVal = 5;
            this.status.CooltimeReduceR.BaseVal = 0;
            this.status.CooltimeIgnoreR.BaseVal = 0;
            this.status.AbnormalDmgR = 11.00;
            this.status.BuffDurationIncR.BaseVal = 54;
            this.status.TamingMobDurationIncR.BaseVal = 40;
            
            this.status.MesoGainR.BaseVal = 2;
            this.status.DropGainR.BaseVal = 0;
            this.status.ExpGainR = 36.00;
            this.status.StarForce.BaseVal = 295;
            this.status.ArcaneForce.BaseVal = 1350;
            this.status.AuthenticForce.BaseVal = 660;

            this.itemSlots = new ItemBase[5][];
            for (int i = 0; i < this.itemSlots.Length; i++)
            {
                this.itemSlots[i] = new ItemBase[96];
            }

            this.equip = new CharaEquip();
        }

        private static FormulaVersion version;

        /// <summary>
        /// 获取或设置角色属性计算的公式版本。
        /// </summary>
        public static FormulaVersion Version
        {
            get { return Character.version; }
            set { Character.version = value; }
        }

        private string name;
        private string guild;
        private CharacterStatus status;
        private ItemBase[][] itemSlots;
        private CharaEquip equip;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Guild
        {
            get { return guild; }
            set { guild = value; }
        }

        public CharacterStatus Status
        {
            get { return status; }
        }

        public ItemBase[][] ItemSlots
        {
            get { return itemSlots; }
        }
        public CharaEquip Equip
        {
            get { return equip; }
            set { equip = value; }
        }

        public void UpdateProps()
        {
            status.Strength.ResetAdd();
            status.Dexterity.ResetAdd();
            status.Intelligence.ResetAdd();
            status.Luck.ResetAdd();
            status.MaxHP.ResetAdd();
            status.MaxMP.ResetAdd();

            status.PADamage.ResetAll();
            status.MADamage.ResetAll();
            status.PDDamage.ResetAll();
            status.MDDamage.ResetAll();
            status.PAccurate.ResetAll();
            status.MAccurate.ResetAll();
            status.PEvasion.ResetAll();
            status.MEvasion.ResetAll();

            status.MoveSpeed.ResetAdd();
            status.Jump.ResetAdd();
            status.CriticalRate.ResetAdd();
            status.CriticalDamage.ResetAdd();
            status.DamageRate.ResetAll();

            //foreach (Buff buff in buffs)
            //{
            //    foreach (KeyValuePair<GearPropType, int> prop in buff.props)
            //    {
            //        AddBuffProp(prop.Key, prop.Value, buff.Type == BuffType.passiveSkill);
            //    }
            //}

            foreach (Gear gear in equip.GearsEquiped)
            {
                if (gear.State == GearState.enable)
                {
                    foreach (KeyValuePair<GearPropType, int> prop in gear.Props)
                    {
                        addProp(prop.Key, prop.Value);
                    }
                    foreach (Potential potential in gear.Options)
                    {
                        if (potential != null)
                        {
                            foreach (KeyValuePair<GearPropType, int> prop in potential.props)
                            {
                                addProp(prop.Key, prop.Value);
                            }
                        }
                    }
                    foreach (Addition addition in gear.Additions)
                    {
                        foreach (KeyValuePair<GearPropType, int> prop in getAdditionProps(addition))
                        {
                            addProp(prop.Key, prop.Value);
                        }
                    }
                }
            }

            checkSetItemEnabled();
            foreach (SetItem setItem in CharaSimLoader.LoadedSetItems.Values)
            {
                foreach (SetItemEffect effect in setItem.Effects.Values)
                {
                    if (effect.Enabled)
                    {
                        foreach (KeyValuePair<GearPropType, object> prop in effect.Props)
                        {
                            if (prop.Key == GearPropType.Option)
                            {
                                List<Potential> potens = prop.Value as List<Potential>;
                                foreach (Potential p in potens)
                                {
                                    foreach (KeyValuePair<GearPropType, int> pprop in p.props)
                                    {
                                        addProp(pprop.Key, pprop.Value);
                                    }
                                }
                            }
                            else
                            {
                                addProp(prop.Key, Convert.ToInt32(prop.Value));
                            }
                        }
                    }
                }
            }

            int[] sum = new int[4] { status.Strength.GetSum(),
                status.Dexterity.GetSum(),
                status.Intelligence.GetSum(),
                status.Luck.GetSum() };
            if (version == FormulaVersion.Bigbang)
            {
                status.PDDamage.BaseVal = (int)Math.Floor(sum[0] * 1.2 + sum[1] * 0.5 + sum[2] * 0.4 + sum[3] * 0.5);
                status.MDDamage.BaseVal = (int)Math.Floor(sum[0] * 0.4 + sum[1] * 0.5 + sum[2] * 1.2 + sum[3] * 0.5);
                status.PAccurate.BaseVal = (int)Math.Floor(sum[1] * 1.2 + sum[3] * 1.0);
                status.MAccurate.BaseVal = (int)Math.Floor(sum[2] * 1.2 + sum[3] * 1.0);
                status.PEvasion.BaseVal = sum[1] * 1 + sum[3] * 2;
                status.MEvasion.BaseVal = sum[2] * 1 + sum[3] * 2;
            }
            else if (version == FormulaVersion.Chaos)
            {
                status.PDDamage.BaseVal = (int)Math.Floor(sum[0] * 1.5 + sum[1] * 0.4 + sum[2] * 0 + sum[3] * 0.4);
                status.MDDamage.BaseVal = (int)Math.Floor(sum[0] * 0 + sum[1] * 0.4 + sum[2] * 1.5 + sum[3] * 0.4);
                status.PAccurate.BaseVal = (int)Math.Floor(sum[0] * 0.4 + sum[1] * 1.6 + sum[2] * 0 + sum[3] * 0.8);
                status.MAccurate.BaseVal = (int)Math.Floor(sum[0] * 0 + sum[1] * 0.4 + sum[2] * 1.6 + sum[3] * 0.8);
                status.PEvasion.BaseVal = (int)Math.Floor(sum[0] * 0.2 + sum[1] * 0.6 + sum[2] * 0 + sum[3] * 1.4);
                status.MEvasion.BaseVal = (int)Math.Floor(sum[0] * 0 + sum[1] * 0.2 + sum[2] * 0.6 + sum[3] * 1.4);
            }
        }

        private void addProp(GearPropType type, int value)
        {
            switch (type)
            {
                case GearPropType.incSTR: status.Strength.GearAdd += value; break;
                case GearPropType.incSTRr: status.Strength.Rate += value; break;
                case GearPropType.incDEX: status.Dexterity.GearAdd += value; break;
                case GearPropType.incDEXr: status.Dexterity.Rate += value; break;
                case GearPropType.incINT: status.Intelligence.GearAdd += value; break;
                case GearPropType.incINTr: status.Intelligence.Rate += value; break;
                case GearPropType.incLUK: status.Luck.GearAdd += value; break;
                case GearPropType.incLUKr: status.Luck.Rate += value; break;
                case GearPropType.incAllStat:
                    status.Strength.GearAdd += value;
                    status.Dexterity.GearAdd += value;
                    status.Intelligence.GearAdd += value;
                    status.Luck.GearAdd += value;
                    break;
                case GearPropType.incPAD: status.PADamage.GearAdd += value; break;
                case GearPropType.incPADr: status.PADamage.Rate += value; break;
                case GearPropType.incPDD: status.PDDamage.GearAdd += value; break;
                case GearPropType.incPDDr: status.PDDamage.Rate += value; break;
                case GearPropType.incMAD: status.MADamage.GearAdd += value; break;
                case GearPropType.incMADr: status.MADamage.Rate += value; break;
                case GearPropType.incMDD: status.MDDamage.GearAdd += value; break;
                case GearPropType.incMDDr: status.MDDamage.Rate += value; break;

                case GearPropType.incACC: status.PAccurate.GearAdd += value; status.MAccurate.GearAdd += value; break;
                case GearPropType.incACCr: status.PAccurate.Rate += value; status.MAccurate.Rate += value; break;
                case GearPropType.incEVA: status.PEvasion.GearAdd += value; status.MEvasion.GearAdd += value; break;
                case GearPropType.incEVAr: status.PEvasion.Rate += value; status.MEvasion.Rate += value; break;
                case GearPropType.incCr: status.CriticalRate.GearAdd += value; break;

                case GearPropType.incMHP: status.MaxHP.GearAdd += value; break;
                case GearPropType.incMHPr: status.MaxHP.Rate += value; break;
                case GearPropType.incMMP: status.MaxMP.GearAdd += value; break;
                case GearPropType.incMMPr: status.MaxMP.Rate += value; break;
                case GearPropType.incSpeed: status.MoveSpeed.GearAdd += value; break;
                case GearPropType.incJump: status.Jump.GearAdd += value; break;

                //case GearPropType.incCriticaldamageMax: status.CriticalDamageMax.GearAdd += value; break;
                //case GearPropType.incCriticaldamageMin: status.CriticalDamageMin.GearAdd += value; break;
                case GearPropType.incCriticaldamage: status.CriticalDamage.GearAdd += value; break;
            }
        }

        private Dictionary<GearPropType, int> getAdditionProps(Addition addition)
        {
            Dictionary<GearPropType, int> props = new Dictionary<GearPropType, int>();
            if (addition != null &&
                (addition.Type == AdditionType.critical || addition.Type == AdditionType.statinc))
            {
                bool con = false;
                switch (addition.ConType)
                {
                    case GearPropType.reqLevel:
                        con = (this.status.Level >= addition.ConValue[0]);
                        break;
                    case GearPropType.reqJob:
                        foreach (int val in addition.ConValue)
                        {
                            con |= this.status.Job == val;
                        }
                        break;
                    case GearPropType.reqCraft:
                    default:
                        con = true;
                        break;
                }
                if (con)
                {
                    string strcr; int cr;
                    if (addition.Props.TryGetValue("prob", out strcr) && Int32.TryParse(strcr, out cr))
                        props.Add(GearPropType.incCr, cr);

                    if (addition.Type == AdditionType.statinc)
                    {
                        foreach (var kv in addition.Props)
                        {
                            try
                            {
                                GearPropType propType = (GearPropType)Enum.Parse(typeof(GearPropType), kv.Key);
                                if ((int)propType > 0 && (int)propType < 100)
                                    props.Add(propType, Convert.ToInt32(kv.Value));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            return props;
        }

        private void checkSetItemEnabled()
        {
            //重置所有setItem
            List<int> idList = new List<int>();
            foreach (SetItem setItem in CharaSimLoader.LoadedSetItems.Values)
            {
                foreach (KeyValuePair<int, SetItemIDPart> idPart in setItem.ItemIDs.Parts)
                {
                    idList.AddRange(idPart.Value.ItemIDs.Keys);
                    foreach (int id in idList)
                    {
                        idPart.Value.ItemIDs[id] = false;
                    }
                    idList.Clear();
                }
                setItem.currentCount = 0;
            }
            //验证有效装备
            int setItemID;
            foreach (Gear gear in equip.GearsEquiped)
            {
                if (gear.State == GearState.enable
                    && gear.Props.TryGetValue(GearPropType.setItemID, out setItemID))
                {
                    CharaSimLoader.LoadedSetItems[setItemID].ItemIDs[gear.ItemID] = true;
                    CharaSimLoader.LoadedSetItems[setItemID].currentCount++;
                }
            }
            //验证所有setItem
            foreach (SetItem setItem in CharaSimLoader.LoadedSetItems.Values)
            {
                foreach (KeyValuePair<int, SetItemEffect> effect in setItem.Effects)
                {
                    effect.Value.Enabled = (setItem.currentCount >= effect.Key);
                }
            }
        }

        public void ChangeGear(Gear newGear)
        {
            int emptyIdx = this.equip.GetEmptySlotIndex(newGear.type, newGear.Cash);
            ChangeGear(newGear, emptyIdx);
        }

        public void ChangeGear(Gear newGear, int index)
        {
            ItemBase[] itemTab = this.itemSlots[0];
            int newGearIndex = Array.IndexOf<ItemBase>(itemTab, newGear);
            if (newGearIndex < 0 || newGear.State != GearState.itemList)
            {
                throw new InvalidOperationException("不明なエラー: 装備がインベントリにありません。");
            }

            int onlyEquip;
            if (newGear.Props.TryGetValue(GearPropType.onlyEquip, out onlyEquip) && onlyEquip > 0)
            {
                foreach (Gear gear in this.equip.GearsEquiped)
                {
                    if (gear.ItemID == newGear.ItemID)
                    {
                        throw new InvalidOperationException("このアイテムは一度に 1 つしか装備できません。");
                    }
                }
            }

            string errorString;
            if (!checkGearReq(newGear, out errorString))
            {
                throw new InvalidOperationException(errorString);
            }

            Gear[] removedGear;
            if (!this.equip.AddGear(newGear, out removedGear))
            {
                throw new InvalidOperationException("不明なエラー: 装備の追加に失敗しました。");
            }

            CheckGearEnabled();

            if (newGear.State == GearState.enable)
            {
                Queue<int> emptyItemSlot = new Queue<int>();
                emptyItemSlot.Enqueue(newGearIndex);

                if (removedGear.Length > 1) //检查剩余背包大小
                {
                    for (int i = 0; i < itemTab.Length; i++)
                    {
                        if (itemTab[i] == null)
                        {
                            emptyItemSlot.Enqueue(i);
                        }
                    }
                }
                if (emptyItemSlot.Count >= removedGear.Length)
                {
                    for (int i = 0; i < removedGear.Length; i++)
                    {
                        Gear gear = removedGear[i];
                        gear.State = GearState.itemList;
                        itemTab[emptyItemSlot.Dequeue()] = gear;
                    }
                    return; //函数出口
                }
                else
                {
                    errorString = "インベントリはいっぱいです。";
                }
            }
            else
            {
                errorString = "このアイテムを装備するために必要な条件を満たしていません。";
            }

            //还原装备
            foreach (Gear gear in removedGear)
            {
                Gear[] arg;
                this.equip.AddGear(gear, index, out arg); //可以证明直接输入index是可以还原的。
            }
            newGear.State = GearState.itemList;
            throw new InvalidOperationException(errorString);
        }

        private bool checkGearReq(Gear gear, out string errorMessage)
        {
            if (Gear.IsMechanicGear(gear.type) && status.Job / 100 != 35)
            {
                errorMessage = "Mechanic only";
                return false;
            }
            if (Gear.IsDragonGear(gear.type) && status.Job / 100 != 22)
            {
                errorMessage = "Evan only";
                return false;
            }
            if (gear.type == GearType.katara && status.Job / 10 != 43)
            {
                errorMessage = "Dual Blades only";
                return false;
            }
            if (gear.type == GearType.shield &&
                (status.Job / 10 == 43 || status.Job / 100 == 23 || status.Job / 100 == 31))
            {
                errorMessage = "この職業は盾を装備できません。";
                return false;
            }
            if (gear.type == GearType.magicArrow && status.Job / 100 != 23)
            {
                errorMessage = "Mercedes only";
                return false;
            }
            if (gear.type == GearType.demonShield && status.Job / 100 != 31)
            {
                errorMessage = "Demon only";
                return false;
            }
            if (!checkGearPropReq(gear))
            {
                errorMessage = "このアイテムを装備するために必要な条件を満たしていません。";
                return false;
            }
            errorMessage = null;
            return true;
        }

        private bool checkGearPropReq(Gear gear)
        {
            return checkGearPropReq(gear.Props, GearPropType.reqSTR, status.Strength.GetGearReqSum())
                && checkGearPropReq(gear.Props, GearPropType.reqDEX, status.Dexterity.GetGearReqSum())
                && checkGearPropReq(gear.Props, GearPropType.reqINT, status.Intelligence.GetGearReqSum())
                && checkGearPropReq(gear.Props, GearPropType.reqLUK, status.Luck.GetGearReqSum())
                && checkGearPropReq(gear.Props, GearPropType.reqLevel, status.Level)
                && checkGearPropReq(gear.Props, GearPropType.reqPOP, status.Pop)
                && checkGearJobReq(gear.Props, gear.type);
        }

        private bool checkGearPropReq(Dictionary<GearPropType, int> props, GearPropType prop, int value)
        {
            int v;
            if (!props.TryGetValue(prop, out v) || value >= v)
            {
                return true;
            }
            return false;
        }

        private bool checkGearJobReq(Dictionary<GearPropType, int> props, GearType type)
        {
            int reqJob;
            props.TryGetValue(GearPropType.reqJob, out reqJob);
            int jobClass = status.Job % 1000 / 100;
            if (reqJob == 0) //全职
                return true;
            if (reqJob == -1) //新手
                return jobClass == 0;
            return (reqJob & (1 << (jobClass - 1))) != 0;
        }

        /// <summary>
        /// 检查指定的职业ID是否归属于标准职业。
        /// </summary>
        /// <param name="jobID">要检查的职业ID。</param>
        /// <param name="baseJob">标准职业代码。0-新手 1-战士 2-法师 3-弓手 4-飞侠 5-海盗</param>
        /// <returns></returns>
        public static bool CheckJobReq(int jobID, int baseJob)
        {
            switch (jobID / 100)
            {
                case 27: return baseJob == 2; //夜光
                case 36: return baseJob == 4 || baseJob == 5; //煎饼
                default:
                    return jobID / 100 % 10 == baseJob;
            }
        }

        public bool CheckGearEnabled()
        {
            List<Gear> gearsEquip = new List<Gear>(this.equip.GearsEquiped);
            List<GearState> oldStates = new List<GearState>(gearsEquip.Count);
            foreach (Gear gear in gearsEquip)
            {
                oldStates.Add(gear.State);
                gear.State = GearState.enable;
            }

            while (true)
            {
                bool reset = false;
                //逐个装备判定装备要求
                foreach (Gear gear in gearsEquip)
                {
                    if (gear.State == GearState.enable)
                    {
                        gear.State = GearState.disable;
                        UpdateProps();

                        //判定装备要求
                        if (!checkGearPropReq(gear))
                        {
                            reset = true; //如果不符合 无效化装备 进行下一轮判断
                            break;
                        }

                        //恢复有效性
                        gear.State = GearState.enable;
                    }
                }
                if (!reset) //如果本轮判断没变化则停止 可以证明是不会进入死循环的
                    break;
            }

            for (int i = 0; i < gearsEquip.Count; i++)
            {
                if (gearsEquip[i].State != oldStates[i]) //装备状态变化
                {
                    return true;
                }
            }

            return false;
        }

        public void CalcAttack(out double max, out double min)
        {
            int sign;
            CalcAttack(out max, out min, out sign);
        }

        public void CalcAttack(out double max, out double min, out int sign)
        {
            max = CalcAttack(status.Strength.GetSum(),
                status.Dexterity.GetSum(),
                status.Intelligence.GetSum(),
                status.Luck.GetSum(),
                status.PADamage.GetSum(),
                status.MADamage.GetSum(),
                GearType.totem,
                version);
            min = max * status.Mastery.GetSum() / 100;
            sign = 0;
        }

        public static double CalcAttack(int str, int dex, int inte, int luk, int pad, int mad,
            GearType WeaponType, FormulaVersion version)
        {
            switch (WeaponType)
            {
                case GearType.ohSword:
                case GearType.ohAxe:
                case GearType.ohBlunt:
                    return (str * 4 + dex) * 1.2 * pad * 0.01;
                case GearType.dagger:
                    return (str + dex + luk * 4) * 1.3 * pad * 0.01;
                case GearType.cane:
                    return (dex + luk * 4) * 1.3 * pad * 0.01;
                case GearType.wand:
                case GearType.staff:
                    return (inte * 4 + luk) * 1.0 * mad * 0.01;
                case GearType.barehand:
                    return (str * 4 + dex) * 1.43 * 1 * 0.01;
                case GearType.thSword:
                case GearType.thAxe:
                case GearType.thBlunt:
                    if (version == FormulaVersion.Bigbang)
                        return (str * 4 + dex) * 1.32 * pad * 0.01;
                    else if (version == FormulaVersion.Chaos)
                        return (str * 4 + dex) * 1.34 * pad * 0.01;
                    break;
                case GearType.spear:
                case GearType.polearm:
                    return (str * 4 + dex) * 1.49 * pad * 0.01;
                case GearType.bow:
                    if (version == FormulaVersion.Bigbang)
                        return (dex * 4 + str) * 1.2 * pad * 0.01;
                    else if (version == FormulaVersion.Chaos)
                        return (dex * 4 + str) * 1.3 * pad * 0.01;
                    break;

                case GearType.crossbow:
                    return (dex * 4 + str) * 1.35 * pad * 0.01;
                case GearType.throwingGlove:
                    return (dex + luk * 4) * 1.75 * pad * 0.01;
                case GearType.knuckle:
                    return (str * 4 + dex) * 1.7 * pad * 0.01;
                case GearType.gun:
                    return (dex * 4 + str) * 1.5 * pad * 0.01;
                case GearType.dualBow:
                    return (dex * 4 + str) * 1.3 * pad * 0.01;
                case GearType.handCannon:
                    return (str * 4 + dex) * 1.5 * pad * 0.01;
            }
            return 0;
        }

        private static long[] _exptnl = new long[]
        {
            15,34,57,92,
            135,372,560,840,
            1242,2207026470,1731919984062
        };

        public static long ExpToNextLevel(int level)
        {
            long exp;
            if (level < 1 || level > 300)
                return -1;
            if (level < 10)
                return _exptnl[level - 1];
            if (level >= 10 && level <= 14)
                return ExpToNextLevel(9);
            if (level >= 15 && level <= 29)
            {
                exp = ExpToNextLevel(14);
                while (level > 14)
                {
                    exp = (long)(exp * 1.2);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 30 && level <= 34)
                return ExpToNextLevel(29);
            if (level >= 35 && level <= 39)
            {
                exp = ExpToNextLevel(34);
                while (level > 34)
                {
                    exp = (long)(exp * 1.2);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 40 && level <= 59)
            {
                exp = ExpToNextLevel(39);
                while (level > 39)
                {
                    exp = (long)(exp * 1.08);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 60 && level <= 64)
                return ExpToNextLevel(59);
            if (level >= 65 && level <= 74)
            {
                exp = ExpToNextLevel(64);
                while (level > 64)
                {
                    exp = (long)(exp * 1.075);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 75 && level <= 89)
            {
                exp = ExpToNextLevel(74);
                while (level > 74)
                {
                    exp = (long)(exp * 1.07);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 90 && level <= 99)
            {
                exp = ExpToNextLevel(89);
                while (level > 89)
                {
                    exp = (long)(exp * 1.065);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 100 && level <= 104)
                return ExpToNextLevel(99);
            if (level >= 105 && level <= 139)
            {
                exp = ExpToNextLevel(104);
                while (level > 104)
                {
                    exp = (long)(exp * 1.065);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 140 && level <= 169)
            {
                exp = ExpToNextLevel(139);
                while (level > 139)
                {
                    exp = (long)(exp * 1.0625);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 170 && level <= 199)
            {
                exp = ExpToNextLevel(169);
                while (level > 169)
                {
                    exp = (long)(exp * 1.05);
                    level -= 1;
                }
                return exp;
            }
            if (level == 200)
                return _exptnl[9];
            if (level >= 201 && level <= 209)
            {
                exp = ExpToNextLevel(200);
                while (level > 200)
                {
                    exp = (long)(exp * 1.12);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 210 && level <= 214)
            {
                exp = (long)(ExpToNextLevel(209) * 1.3);
                while (level > 210)
                {
                    exp = (long)(exp * 1.11);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 215 && level <= 219)
            {
                exp = (long)(ExpToNextLevel(214) * 1.3);
                while (level > 215)
                {
                    exp = (long)(exp * 1.09);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 220 && level <= 224)
            {
                exp = (long)(ExpToNextLevel(219) * 1.3);
                while (level > 220)
                {
                    exp = (long)(exp * 1.07);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 225 && level <= 229)
            {
                exp = (long)(ExpToNextLevel(224) * 1.3);
                while (level > 225)
                {
                    exp = (long)(exp * 1.07);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 230 && level <= 234)
            {
                exp = (long)(ExpToNextLevel(229) * 1.3);
                while (level > 230)
                {
                    exp = (long)(exp * 1.03);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 235 && level <= 239)
            {
                exp = (long)(ExpToNextLevel(234) * 1.3);
                while (level > 235)
                {
                    exp = (long)(exp * 1.03);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 240 && level <= 244)
            {
                exp = (long)(ExpToNextLevel(239) * 1.3);
                while (level > 240)
                {
                    exp = (long)(exp * 1.03);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 245 && level <= 249)
            {
                exp = (long)(ExpToNextLevel(244) * 1.3);
                while (level > 245)
                {
                    exp = (long)(exp * 1.03);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 250 && level <= 259)
            {
                exp = (long)(ExpToNextLevel(249) * 1.5);
                while (level > 250)
                {
                    exp = (long)(exp * 1.03);
                    level -= 1;
                }
                return exp;
            }
            if (level == 260)
                return _exptnl[10];
            if (level >= 261 && level <= 264)
            {
                exp = ExpToNextLevel(260);
                while (level > 260)
                {
                    exp = (long)(exp * 1.01);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 265 && level <= 269)
            {
                exp = (long)(ExpToNextLevel(264) * 1.3);
                while (level > 265)
                {
                    exp = (long)(exp * 1.01);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 270 && level <= 274)
            {
                exp = (long)(ExpToNextLevel(269) * 2.22);
                while (level > 269)
                {
                    exp = (long)(exp * 1.01);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 275 && level <= 279)
            {
                exp = (long)(ExpToNextLevel(274) * 2.02);
                while (level > 275)
                {
                    exp = (long)(exp * 1.1);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 280 && level <= 284)
            {
                exp = (long)(ExpToNextLevel(279) * 2.02);
                while (level > 280)
                {
                    exp = (long)(exp * 1.1);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 285 && level <= 289)
            {
                exp = (long)(ExpToNextLevel(284) * 2.02);
                while (level > 285)
                {
                    exp = (long)(exp * 1.1);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 290 && level <= 294)
            {
                exp = (long)(ExpToNextLevel(289) * 2.02);
                while (level > 290)
                {
                    exp = (long)(exp * 1.1);
                    level -= 1;
                }
                return exp;
            }
            if (level >= 295 && level <= 298)
            {
                exp = (long)(ExpToNextLevel(294) * 2.02);
                while (level > 295)
                {
                    exp = (long)(exp * 1.1);
                    level -= 1;
                }
                return exp;
            }
            if (level == 299)
                return (long)(ExpToNextLevel(298) * 1.5);
            return 0;
        }
    }
}
