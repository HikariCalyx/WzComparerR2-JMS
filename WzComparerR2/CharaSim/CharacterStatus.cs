﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace WzComparerR2.CharaSim
{
    public class CharacterStatus
    {
        public CharacterStatus()
        {
            this.maxHP = new CharaProp(99999);
            this.maxMP = new CharaProp(99999);
            this.pdd = new CharaProp(9999);
            this.mdd = new CharaProp(9999);
            this.pAcc = new CharaProp(9999);
            this.mAcc = new CharaProp(9999);
            this.pEva = new CharaProp(9999);
            this.mEva = new CharaProp(9999);

            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo f in fields)
            {
                if (f.FieldType == typeof(CharaProp) && f.GetValue(this) == null)
                {
                    f.SetValue(this, new CharaProp());
                }
            }
        }

        private int job;
        private int level;
        private long hp;
        private long mp;
        private long exp;
        private int ap;
        private int pop;
        private int unionLevel;
        private int dojoFloor;

        private CharaProp specialValue;

        private CharaProp maxHP;
        private CharaProp maxMP;
        private CharaProp str = null;
        private CharaProp dex = null;
        private CharaProp inte = null;
        private CharaProp luk = null;

        private CharaProp pad = null;
        private CharaProp mad = null;
        private CharaProp pdd;
        private CharaProp mdd;
        private CharaProp pAcc;
        private CharaProp mAcc;
        private CharaProp pEva;
        private CharaProp mEva;
        private CharaProp crit = null;
        private CharaProp move = null;
        private CharaProp jump = null;
        //private CharaProp critDamMax = null;
        //private CharaProp critDamMin = null;
        private CharaProp attrange = null;
        private CharaProp cprange = null;
        private CharaProp critDam = null;
        private CharaProp mastery = null;
        private CharaProp damR = null;
        private CharaProp bossDamR = null;
        private CharaProp finalDamR = null;
        private CharaProp ignoreMobDefR = null;
        private CharaProp statusRes = null;
        private CharaProp knockbackRes = null;
        private CharaProp normalMonsterDamR = null;

        private CharaProp cooltimeReduceSecond = null;
        private CharaProp cooltimeReduceR = null;
        private CharaProp cooltimeIgnoreR = null;
        private CharaProp buffDurationIncR = null;
        private CharaProp tamingMobDurationIncR = null;

        private CharaProp mesoGainR = null;
        private CharaProp dropGainR = null;
        private double abnormalDmgR;
        private double expGainR;

        private CharaProp starForce = null;
        private CharaProp arcaneForce = null;
        private CharaProp authenticForce = null;

        private CharaProp stance = null;
        private CharaProp defense = null;
        private CharaProp attackspeed = null;

        #region 基础属性
        /// <summary>
        /// 获取或设置角色的职业代码。
        /// </summary>
        public int Job
        {
            get { return job; }
            set { job = value; }
        }

        /// <summary>
        /// 获取或设置角色的等级。
        /// </summary>
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        /// <summary>
        /// 获取或设置角色的当前HP。
        /// </summary>
        public long HP
        {
            get { hp = Math.Max(0, Math.Min(maxHP.GetSum(), hp)); return hp; }
            set { value = Math.Max(0, Math.Min(maxHP.GetSum(), value)); hp = value; }
        }

        /// <summary>
        /// 获取角色的特殊能量值（DF、TF、灵力、PP）。
        /// </summary>
        public CharaProp SpecialValue
        {
            get { return specialValue; }
        }

        /// <summary>
        /// 获取角色的HP上限。
        /// </summary>
        public CharaProp MaxHP
        {
            get { return maxHP; }
        }

        /// <summary>
        /// 获取或设置角色的当前MP。
        /// </summary>
        public long MP
        {
            get { mp = Math.Max(0, Math.Min(maxMP.GetSum(), mp)); return mp; }
            set { value = Math.Max(0, Math.Min(maxMP.GetSum(), value)); mp = value; }
        }

        /// <summary>
        /// 获取角色的MP上限。
        /// </summary>
        public CharaProp MaxMP
        {
            get { return maxMP; }
        }

        /// <summary>
        /// 获取或设置角色的当前经验值。
        /// </summary>
        public long Exp
        {
            get { exp = (Exptnl == -1) ? -1 : Math.Max(0, Math.Min(Exptnl - 1, exp)); return exp; }
            set { value = (Exptnl == -1) ? -1 : Math.Max(0, Math.Min(Exptnl - 1, value)); exp = value; }
        }

        /// <summary>
        /// 获取角色当前升级经验值。
        /// </summary>
        public long Exptnl
        {
            get { return Character.ExpToNextLevel(this.level); }
        }

        /// <summary>
        /// 获取或设置角色的人气度。
        /// </summary>
        public int Pop
        {
            get { return pop; }
            set { pop = value; }
        }

        /// <summary>
        /// 获取或设置角色的冒险岛联盟等级。
        /// </summary>
        public int UnionLevel
        {
            get { return unionLevel; }
            set { unionLevel = value; }
        }

        /// <summary>
        /// 获取或设置角色的武陵道场等级。
        /// </summary>
        public int DojoFloor
        {
            get { return dojoFloor; }
            set { dojoFloor = value; }
        }

        /// <summary>
        /// 获取或设置角色的可分配AP。
        /// </summary>
        public int Ap
        {
            get { return ap; }
            set { if (value >= 0) ap = value; }
        }

        /// <summary>
        /// 获取角色的力量值。
        /// </summary>
        public CharaProp Strength
        {
            get { return str; }
        }

        /// <summary>
        /// 获取角色的敏捷值。
        /// </summary>
        public CharaProp Dexterity
        {
            get { return dex; }
        }

        /// <summary>
        /// 获取角色的智力值。
        /// </summary>
        public CharaProp Intelligence
        {
            get { return inte; }
        }

        /// <summary>
        /// 获取角色的运气值。
        /// </summary>
        public CharaProp Luck
        {
            get { return luk; }
        }
        #endregion

        #region 扩展属性
        /// <sumary>
        /// 获取角色的攻击力范围
        /// </sumary>
        public CharaProp attackRange
        {
            get { return attrange; }
        }
        /// <sumary>
        /// 获取角色的战斗力范围
        /// </sumary>
        public CharaProp combatPower
        {
            get { return cprange; }
        }
        /// <summary>
        /// 获取角色的攻击力。
        /// </summary>
        public CharaProp PADamage
        {
            get { return pad; }
        }

        /// <summary>
        /// 获取角色的魔法攻击力。
        /// </summary>
        public CharaProp MADamage
        {
            get { return mad; }
        }

        /// <summary>
        /// 获取角色的物理防御力。
        /// </summary>
        public CharaProp PDDamage
        {
            get { return pdd; }
        }

        /// <summary>
        /// 获取角色的魔法防御力。
        /// </summary>
        public CharaProp MDDamage
        {
            get { return mdd; }
        }

        /// <summary>
        /// 获取角色的物理命中率。
        /// </summary>
        public CharaProp PAccurate
        {
            get { return pAcc; }
        }

        /// <summary>
        /// 获取角色的魔法命中率。
        /// </summary>
        public CharaProp MAccurate
        {
            get { return mAcc; }
        }

        /// <summary>
        /// 获取角色的物理回避率。
        /// </summary>
        public CharaProp PEvasion
        {
            get { return pEva; }
        }

        /// <summary>
        /// 获取角色的魔法回避率。
        /// </summary>
        public CharaProp MEvasion
        {
            get { return mEva; }
        }

        /// <summary>
        /// 获取角色的暴击率，这是一个百分比属性。
        /// </summary>
        public CharaProp CriticalRate
        {
            get { return crit; }
        }

        /// <summary>
        /// 获取角色的移动速度，这是一个百分比属性。
        /// </summary>
        public CharaProp MoveSpeed
        {
            get { return move; }
        }

        /// <summary>
        /// 获取角色的跳跃力，这是一个百分比属性。
        /// </summary>
        public CharaProp Jump
        {
            get { return jump; }
        }

        /*/// <summary>
        /// 获取角色的暴击最大伤害，这是一个隐藏的百分比属性。
        /// </summary>
        public CharaProp CriticalDamageMax
        {
            get { return critDamMax; }
        }*/

        /*/// <summary>
        /// 获取角色的暴击最小伤害，这是一个隐藏的百分比属性。
        /// </summary>
        public CharaProp CriticalDamageMin
        {
            get { return critDamMin; }
        }*/

        /// <summary>
        /// 获取角色的暴击伤害，这是一个隐藏的百分比属性。
        /// </summary>
        public CharaProp CriticalDamage
        {
            get { return critDam; }
        }

        /// <summary>
        /// 获取角色的攻击熟练度，这是一个隐藏的百分比属性。
        /// </summary>
        public CharaProp Mastery
        {
            get { return mastery; }
        }

        /// <summary>
        /// 获取角色的攻击力百分比加成，这是一个隐藏的百分比属性。
        /// </summary>
        public CharaProp DamageRate
        {
            get { return damR; }
        }

        /// <summary>
        /// 获取角色的BOSS攻击力百分比加成，这是一个隐藏的百分比属性。
        /// </summary>
        public CharaProp BossDamageRate
        {
            get { return bossDamR; }
        }

        public CharaProp FinalDamageRate
        {
            get { return finalDamR; }
        }

        public CharaProp IgnoreMobDefenceRate
        {
            get { return ignoreMobDefR; }
        }

        public CharaProp StatusResistance
        {
            get { return statusRes; }
        }

        public CharaProp KnockbackResistance
        {
            get { return knockbackRes; }
        }

        public CharaProp NormalMonsterDamR
        {
            get { return normalMonsterDamR; }
        }

        public CharaProp CooltimeReduceSecond
        {
            get { return cooltimeReduceSecond; }
        }
        public CharaProp CooltimeReduceR
        {
            get { return cooltimeReduceR; }
        }
        public CharaProp CooltimeIgnoreR
        {
            get { return cooltimeIgnoreR; }
        }
        public CharaProp BuffDurationIncR
        {
            get { return buffDurationIncR; }
        }
        public CharaProp TamingMobDurationIncR
        {
            get { return tamingMobDurationIncR; }
        }

        public double AbnormalDmgR
        {
            get { return abnormalDmgR; }
            set { abnormalDmgR = value; }
        }

        public CharaProp MesoGainR
        {
            get { return mesoGainR; }
        }

        public CharaProp DropGainR
        {
            get { return dropGainR; }
        }

        public double ExpGainR
        {
            get { return expGainR; }
            set { expGainR = value; }
        }

        public CharaProp StarForce
        {
            get { return starForce; }
        }

        public CharaProp ArcaneForce
        {
            get { return arcaneForce; }
        }

        public CharaProp AuthenticForce
        {
            get { return authenticForce; }
        }

        public CharaProp Stance
        {
            get { return stance; }
        }

        public CharaProp Defense
        {
            get { return defense; }
        }

        public CharaProp attackSpeed
        {
            get { return attackspeed; }
        }
        #endregion
    }
}