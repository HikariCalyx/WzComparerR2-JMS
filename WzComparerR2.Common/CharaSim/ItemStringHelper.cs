﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WzComparerR2.CharaSim
{
    public static class ItemStringHelper
    {
        /// <summary>
        /// 获取怪物category属性对应的类型说明。
        /// </summary>
        /// <param Name="category">怪物的category属性的值。</param>
        /// <returns></returns>
        public static string GetMobCategoryName(int category)
        {
            switch (category)
            {
                case 0: return "無";
                case 1: return "動物型";
                case 2: return "植物型";
                case 3: return "魚類型";
                case 4: return "爬虫類型";
                case 5: return "精霊型";
                case 6: return "悪魔型";
                case 7: return "不死型";
                case 8: return "無形";
                default: return null;
            }
        }

        public static string GetGearPropString(GearPropType propType, long value)
        {
            return GetGearPropString(propType, value, 0);
        }

        public static string[] GetGearPropString3(GearPropType propType, long value)
        {
            return GetGearPropString3(propType, value, 0);
        }

        /// <summary>
        /// 获取GearPropType所对应的文字说明。
        /// </summary>
        /// <param Name="propType">表示装备属性枚举GearPropType。</param>
        /// <param Name="Value">表示propType属性所对应的值。</param>
        /// <returns></returns>
        public static string GetGearPropString(GearPropType propType, long value, int signFlag)
        {

            string sign;
            switch (signFlag)
            {
                default:
                case 0: //默认处理符号
                    sign = value > 0 ? "+" : null;
                    break;

                case 1: //固定加号
                    sign = "+";
                    break;

                case 2: //无特别符号
                    sign = "";
                    break;
            }
            switch (propType)
            {
                case GearPropType.incSTR: return "STR : " + sign + value;
                case GearPropType.incSTRr: return "STR : " + sign + value + "%";
                case GearPropType.incDEX: return "DEX : " + sign + value;
                case GearPropType.incDEXr: return "DEX : " + sign + value + "%";
                case GearPropType.incINT: return "INT : " + sign + value;
                case GearPropType.incINTr: return "INT : " + sign + value + "%";
                case GearPropType.incLUK: return "LUK : " + sign + value;
                case GearPropType.incLUKr: return "LUK : " + sign + value + "%";
                case GearPropType.incAllStat: return "Allｽﾃｰﾀｽ：" + sign + value;
                case GearPropType.statR: return "Allｽﾃｰﾀｽ: " + sign + value + "%";
                case GearPropType.incMHP: return "最大HP : " + sign + value;
                case GearPropType.incMHPr: return "最大HP : " + sign + value + "%";
                case GearPropType.incMMP: return "最大MP : " + sign + value;
                case GearPropType.incMMPr: return "最大MP : " + sign + value + "%";
                case GearPropType.incMDF: return "最大DF : " + sign + value;
                case GearPropType.incPAD: return "攻撃力: " + sign + value;
                case GearPropType.incPADr: return "攻撃力: " + sign + value + "%";
                case GearPropType.incMAD: return "魔力: " + sign + value;
                case GearPropType.incMADr: return "魔力: " + sign + value + "%";
                case GearPropType.incPDD: return "防御力 : " + sign + value;
                case GearPropType.incPDDr: return "防御力 : " + sign + value + "%";
                //case GearPropType.incMDD: return "MAGIC DEF. : " + sign + value;
                //case GearPropType.incMDDr: return "MAGIC DEF. : " + sign + value + "%";
                //case GearPropType.incACC: return "ACCURACY : " + sign + value;
                //case GearPropType.incACCr: return "ACCURACY : " + sign + value + "%";
                //case GearPropType.incEVA: return "AVOIDABILITY : " + sign + value;
                //case GearPropType.incEVAr: return "AVOIDABILITY : " + sign + value + "%";
                case GearPropType.incSpeed: return "移動速度: " + sign + value;
                case GearPropType.incJump: return "ジャンプ力: " + sign + value;
                case GearPropType.incCraft: return "器用さ: " + sign + value;
                case GearPropType.damR:
                case GearPropType.incDAMr: return "ダメージ " + sign + value + "%";
                case GearPropType.incCr: return "クリティカル率: " + sign + value + "%";
                case GearPropType.incCDr: return "クリティカルダメージ: " + sign + value + "%";
                case GearPropType.knockback: return "直接打撃の時" + value + "%の確率でノックバック";
                //case GearPropType.incPVPDamage: return "Battle Mode ATT " + sign + " " + value;
                case GearPropType.incPQEXPr: return "グループクエスト経験値 " + value + "% 追加";
                case GearPropType.incEXPr: return "グループ経験値 +" + value + "% 追加";
                case GearPropType.incBDR:
                case GearPropType.bdR: return "ボスモンスター攻撃時のダメージ +" + value + "%";
                case GearPropType.incIMDR:
                case GearPropType.imdR: return "モンスター防御率無視 : +" + value + "%";
                case GearPropType.limitBreak: return "ダメージ上限: " + ToCJKNumberExpr(value);
                case GearPropType.reduceReq: return "装着レベル減少： - " + value;
                case GearPropType.nbdR: return "一般モンスター攻撃時のダメージ: +" + value + "%"; //KMST 1069

                case GearPropType.only: return value == 0 ? null : "固有ｱｲﾃﾑ";
                case GearPropType.tradeBlock: return value == 0 ? null : "交換不可";
                case GearPropType.equipTradeBlock: return value == 0 ? null : "装着すると交換不可";
                case GearPropType.accountSharable: return value == 0 ? null : "ワールド内のキャラクター間移動のみ可能"; //v218 Transferable within world
                case GearPropType.sharableOnce: return value == 0 ? null : "ﾜｰﾙﾄﾞ内の自分のｷｬﾗｸﾀｰ間で1回移動可能\n(移動後交換不可)"; //old "Can be traded once within account"
                case GearPropType.onlyEquip: return value == 0 ? null : "固有装備アイテム";
                case GearPropType.notExtend: return value == 0 ? null : "有効期間延長不可";
                case GearPropType.accountSharableAfterExchange: return value == 0 ? null : "1回交換可能\n(取引後、ワールド内のキャラクター間移動のみ可能)";
                case GearPropType.mintable: return value == 0 ? null : "ミンティング可能";
                case GearPropType.tradeAvailable:
                    switch (value)
                    {
                        case 1: return "#c カルマのはさみを使用すると1回交換可能\n\rになります。#";
                        case 2: return "#c カルマのはさみを使用すると1回交換可能\n\rになります。#";
                        default: return null;
                    }
                case GearPropType.accountShareTag:
                    switch (value)
                    {
                        case 1: return "#c シェアネームタグを使用すると1回同じアカウント内のキャラクターに移動できます。#";
                        default: return null;
                    }
                //case GearPropType.noPotential: return value == 0 ? null : "This item cannot gain Potential.";
                case GearPropType.fixedPotential: return value == 0 ? null : "潜在能力再設定不可";
                case GearPropType.superiorEqp: return value == 0 ? null : "アイテム強化の成功時にさらに高い効果を\r\n得ることができます。";
                case GearPropType.nActivatedSocket: return value == 0 ? null : "#cこのアイテムにはネビュライトを取り付けることができます。#";
                case GearPropType.jokerToSetItem: return value == 0 ? null : " #c3つ以上着用しているすべてのセットアイテムに含まわるラッキーアイテム！(ただし、2つ以上のラッキーアイテム着用をのすると1つの効果のみ適用)#";//\n\r#cThis lucky...
                case GearPropType.plusToSetItem: return value == 0 ? null : "#c装備すると、アイテムセットは2つ装備したものとしてカウントされます。#";
                case GearPropType.abilityTimeLimited: return value == 0 ? null : "期間限定能力値";
                case GearPropType.blockGoldHammer: return value == 0 ? null : "ビシアスのハンマー使用不可";
                case GearPropType.colorvar: return value == 0 ? null : "#cこのアイテムは染料使用可能#";
                case GearPropType.cantRepair: return value == 0 ? null : "修理不可";
                case GearPropType.noLookChange: return value == 0 ? null : "神秘のカナトコ使用不可";

                case GearPropType.incAllStat_incMHP25: return "Allｽﾃｰﾀｽ：" + sign + value + ", 最大HP : " + sign + (value * 25);// check once Lv 250 set comes out in GMS
                case GearPropType.incAllStat_incMHP50_incMMP50: return "Allｽﾃｰﾀｽ：" + sign + value + ", 最大HP / 最大MP : " + sign + (value * 50);
                case GearPropType.incMHP_incMMP: return "最大HP / 最大MP : " + sign + value;
                case GearPropType.incMHPr_incMMPr: return "最大HP / 最大MP : " + sign + value + "%";
                case GearPropType.incPAD_incMAD:
                case GearPropType.incAD: return "攻撃力 / 魔力 : " + sign + value;
                case GearPropType.incPDD_incMDD: return "防御力 : " + sign + value;
                //case GearPropType.incACC_incEVA: return "ACC/AVO :" + sign + value;

                case GearPropType.incARC: return "ARC : " + sign + value;
                case GearPropType.incAUT: return "AUT : " + sign + value;

                case GearPropType.Etuc: return "エクセプショナル強化かできます。 (最大\n\r: " + value + "回)";
                case GearPropType.CuttableCount: return "はさみ使用可能回数：" + value + "回";
                default: return null;
            }
        }

        public static string[] GetGearPropString3(GearPropType propType, long value, int signFlag)
        {
            string[] res = new string[2];
            string sign;
            switch (signFlag)
            {
                default:
                case 0: //默认处理符号
                    sign = value > 0 ? "+" : null;
                    break;

                case 1: //固定加号
                    sign = "+";
                    break;

                case 2: //无特别符号
                    sign = "";
                    break;
            }
            switch (propType)
            {
                case GearPropType.incSTR:
                    res[0] = "STR";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incSTRr:
                    res[0] = "STR";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incDEX:
                    res[0] = "DEX";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incDEXr:
                    res[0] = "DEX";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incINT:
                    res[0] = "INT";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incINTr:
                    res[0] = "INT";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incLUK:
                    res[0] = "LUK";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incLUKr:
                    res[0] = "LUK";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incAllStat:
                    res[0] = "Allｽﾃｰﾀｽ";
                    res[1] = sign + value;
                    return res;
                case GearPropType.statR:
                    res[0] = "Allｽﾃｰﾀｽ";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incMHP:
                    res[0] = "最大HP";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incMHPr:
                    res[0] = "最大HP";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incMMP:
                    res[0] = "最大MP";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incMMPr:
                    res[0] = "最大MP";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incMDF:
                    res[0] = "最大DF";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incPAD:
                    res[0] = "攻撃力";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incPADr:
                    res[0] = "攻撃力";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incMAD:
                    res[0] = "魔力";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incMADr:
                    res[0] = "魔力";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incPDD:
                    res[0] = "防御力";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incPDDr:
                    res[0] = "防御力";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incSpeed:
                    res[0] = "移動速度";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incJump:
                    res[0] = "ジャンプ力";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incCraft:
                    res[0] = "器用さ";
                    res[1] = sign + value;
                    return res;
                case GearPropType.damR:
                case GearPropType.incDAMr:
                    res[0] = "ダメージ";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incCr:
                    res[0] = "クリティカル率";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.incCDr:
                    res[0] = "クリティカルダメージ";
                    res[1] = sign + value + "%";
                    return res;
                case GearPropType.knockback:
                    res[0] = "直接打撃の時" + value + "%の確率でノックバック";
                    return res;
                case GearPropType.incPQEXPr:
                    res[0] = "グループクエスト経験値 ";
                    res[1] = value + "% 追加";
                    return res;
                case GearPropType.incBDR:
                case GearPropType.bdR:
                    res[0] = "ﾎﾞｽﾓﾝｽﾀｰﾀﾞﾒｰｼﾞ";
                    res[1] = "+" + value + "%";
                    return res;
                case GearPropType.incIMDR:
                case GearPropType.imdR:
                    res[0] = "ﾓﾝｽﾀｰ防御率無視";
                    res[1] = "+" + value + "%";
                    return res;
                case GearPropType.limitBreak:
                    res[0] = "ダメージ上限";
                    res[1] = ToCJKNumberExpr(value);
                    return res;
                    /*
                case GearPropType.attackSpeed:
                    if (2 <= value && value <= 9)
                    {
                        res[0] = "攻撃速度";
                        res[1] = $"{10 - value}段階";
                    }
                    return res;
                    */
                case GearPropType.nbdR:
                    res[0] = "一般モンスター攻撃時のダメージ";
                    res[1] = "+" + value + "%";
                    return res;
                case GearPropType.incARC:
                    res[0] = "ARC";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incAUT:
                    res[0] = "AUT";
                    res[1] = sign + value;
                    return res;
                case GearPropType.incCHUC:
                    res[0] = "スターフォース";
                    res[1] = sign + value;
                    return res;

                case GearPropType.tradeBlock:
                    res[0] = value == 0 ? null : "#$r交換不可#";
                    return res;
                case GearPropType.accountSharable:
                    res[0] = value == 0 ? null : "#$rワールド内のキャラクター間移動のみ可能#";
                    return res;
                case GearPropType.sharableOnce:
                    res[0] = value == 0 ? null : "#$rﾜｰﾙﾄﾞ内の自分のｷｬﾗｸﾀｰ間で1回移動可能 (移動後交換不可)#";
                    return res;
                case GearPropType.only:
                    res[0] = value == 0 ? null : "#$r固有ｱｲﾃﾑ#";
                    return res;
                case GearPropType.onlyEquip:
                    res[0] = value == 0 ? null : "#$r固有装備アイテム#";
                    return res;
                case GearPropType.equipTradeBlock:
                    res[0] = value == 0 ? null : "#$r装着すると交換不可#";
                    return res;
                case GearPropType.notExtend:
                    res[0] = value == 0 ? null : " (延長不可)";
                    return res;
                case GearPropType.accountSharableAfterExchange:
                    res[0] = value == 0 ? null : "#$r1回交換可能 (取引後、ワールド内のキャラクター間移動のみ可能)#";
                    return res;
                case GearPropType.timeLimited:
                    res[0] = value == 0 ? null : "期間制アイテム";
                    return res;
                case GearPropType.abilityTimeLimited:
                    res[0] = value == 0 ? null : "期間限定能力値";
                    return res;
                case GearPropType.noLookChange:
                    res[0] = value == 0 ? null : "#$r神秘のカナトコ使用不可#";
                    return res;
                case GearPropType.mintable:
                    res[0] = value == 0 ? null : "#$rミンティング可能#";
                    return res;
                case GearPropType.tradeAvailable:
                    switch (value)
                    {
                        case 1:
                            res[0] = "#$gカルマのはさみを使用すると1回交換可能になります#";
                            return res;
                        case 2:
                            res[0] = "#$gカルマのはさみを使用すると1回交換可能になります#";
                            return res;
                        default: return res;
                    }
                case GearPropType.accountShareTag:
                    switch (value)
                    {
                        case 1:
                            res[0] = "#cシェアネームタグを使用すると1回同じアカウント内のキャラクターに移動できます。#";
                            return res;
                        default: return res;
                    }
                //case GearPropType.noPotential: return value == 0 ? null : "잠재능력 설정 불가";
                //case GearPropType.fixedPotential: return value == 0 ? null : "잠재능력 재설정 불가";
                case GearPropType.superiorEqp:
                    res[0] = value == 0 ? null : "アイテム強化の成功時にさらに高い効果を得ることができます。";
                    return res;
                //case GearPropType.jokerToSetItem: return value == 0 ? null : "#c3개 이상 착용하고 있는 모든 세트 아이템에 포함되는 럭키 아이템! (단, 2개 이상의 럭키 아이템 착용 시 1개만 효과 적용.)#";
                //case GearPropType.cantRepair: return value == 0 ? null : "수리 불가";

                case GearPropType.incAllStat_incMHP25:
                    res[0] = "Allｽﾃｰﾀｽ  " + sign + value + ", 最大HP  " + sign + (value * 25);
                    return res;
                case GearPropType.incAllStat_incMHP50_incMMP50:
                    res[0] = "Allｽﾃｰﾀｽ  " + sign + value + ", 最大HP / 最大MP  " + sign + (value * 50);
                    return res;
                case GearPropType.incMHP_incMMP:
                    res[0] = "最大HP / 最大MP  " + sign + value;
                    return res;
                case GearPropType.incMHPr_incMMPr:
                    res[0] = "最大HP / 最大MP  " + sign + value + "%";
                    return res;
                case GearPropType.incPAD_incMAD:
                case GearPropType.incAD:
                    res[0] = "攻撃力 / 魔力  " + sign + " " + value;
                    return res;
                case GearPropType.incPDD_incMDD:
                    res[0] = "防御力  " + sign + value;
                    return res;

                case GearPropType.Etuc:
                    res[0] = $"#$dエクセプショナル : なし# (最大{value}回)";
                    return res;
                case GearPropType.CuttableCount:
                    res[0] = $" #$r(はさみ使用可能回数: {value} / {value})#";
                    return res;

                case GearPropType.incEXPr:
                default: return res;
            }
        }

        public static string GetGearPropDiffString(GearPropType propType, int value, int standardValue)
        {
            var propStr = GetGearPropString(propType, value);
            if (value > standardValue)
            {
                string subfix = null;
                string openAPISubfix = "";
                switch (propType)
                {
                    case GearPropType.incSTR:
                    case GearPropType.incDEX:
                    case GearPropType.incINT:
                    case GearPropType.incLUK:
                    case GearPropType.incMHP:
                    case GearPropType.incMMP:
                    case GearPropType.incMDF:
                    case GearPropType.incARC:
                    case GearPropType.incAUT:
                    case GearPropType.incPAD:
                    case GearPropType.incMAD:
                    case GearPropType.incPDD:
                    case GearPropType.incMDD:
                    case GearPropType.incSpeed:
                    case GearPropType.incJump:
                        subfix = $"({standardValue} #$e+{value - standardValue}#)"; break;
                    case GearPropType.bdR:
                    case GearPropType.incBDR:
                    case GearPropType.imdR:
                    case GearPropType.incIMDR:
                    case GearPropType.damR:
                    case GearPropType.incDAMr:
                    case GearPropType.statR:
                        subfix = $"({standardValue}% #$y+{value - standardValue}%#)"; break;

                    case GearPropType.addSTR:
                    case GearPropType.addDEX:
                    case GearPropType.addINT:
                    case GearPropType.addLUK:
                    case GearPropType.addMHP:
                    case GearPropType.addMMP:
                    case GearPropType.addPAD:
                    case GearPropType.addMAD:
                    case GearPropType.addDEF:
                    case GearPropType.addSpeed:
                    case GearPropType.addJump:
                    case GearPropType.addLvlDec:
                        openAPISubfix += $"#$g+{value - standardValue}#"; break;


                    case GearPropType.addBDR:
                    case GearPropType.addDamR:
                    case GearPropType.addAllStatR:
                        openAPISubfix += $"#$g+{value - standardValue}%#"; break;

                    case GearPropType.scrollSTR:
                    case GearPropType.scrollDEX:
                    case GearPropType.scrollINT:
                    case GearPropType.scrollLUK:
                    case GearPropType.scrollMHP:
                    case GearPropType.scrollMMP:
                    case GearPropType.scrollPAD:
                    case GearPropType.scrollMAD:
                    case GearPropType.scrollDEF:
                    case GearPropType.scrollSpeed:
                    case GearPropType.scrollJump:
                        openAPISubfix += $" #$e+{value - standardValue}#"; break;

                    case GearPropType.starSTR:
                    case GearPropType.starDEX:
                    case GearPropType.starINT:
                    case GearPropType.starLUK:
                    case GearPropType.starMHP:
                    case GearPropType.starMMP:
                    case GearPropType.starPAD:
                    case GearPropType.starMAD:
                    case GearPropType.starDEF:
                    case GearPropType.starSpeed:
                    case GearPropType.starJump:
                        openAPISubfix += $" #c+{value - standardValue}#"; break;

                }
                if (openAPISubfix.Length > 0 )
                {
                    openAPISubfix = $"({standardValue}" + openAPISubfix + ")";
                }
                propStr = "#$y" + propStr + "# " + subfix + openAPISubfix;
            }
            return propStr;
        }

        public static string[] GetGearPropDiffString3(GearPropType propType, int value, int standardValue)
        {
            string[] res = new string[3];

            var propStr = GetGearPropString3(propType, value, 0);
            res[0] = propStr[0];
            res[1] = propStr[1];

            if (value > standardValue)
            {
                string suffix = null;
                switch (propType)
                {
                    case GearPropType.incSTR:
                    case GearPropType.incDEX:
                    case GearPropType.incINT:
                    case GearPropType.incLUK:
                    case GearPropType.incMHP:
                    case GearPropType.incMMP:
                    case GearPropType.incMDF:
                    case GearPropType.incARC:
                    case GearPropType.incAUT:
                    case GearPropType.incPAD:
                    case GearPropType.incMAD:
                    case GearPropType.incPDD:
                    case GearPropType.incMDD:
                    case GearPropType.incSpeed:
                    case GearPropType.incJump:
                        suffix = $"({standardValue} #$e+{value - standardValue}#)"; break;
                    case GearPropType.bdR:
                    case GearPropType.incBDR:
                    case GearPropType.imdR:
                    case GearPropType.incIMDR:
                    case GearPropType.damR:
                    case GearPropType.incDAMr:
                    case GearPropType.statR:
                        suffix = $"({standardValue}% #$e+{value - standardValue}%#)"; break;
                }
                res[2] = suffix;
            }
            return res;
        }

        /// <summary>
        /// 获取gearGrade所对应的字符串。
        /// </summary>
        /// <param Name="rank">表示装备的潜能等级GearGrade。</param>
        /// <returns></returns>
        public static string GetGearGradeString(GearGrade rank)
        {
            switch (rank)
            {
                //case GearGrade.C: return "C级(一般物品)";
                case GearGrade.B: return "(レアアイテム)";
                case GearGrade.A: return "(エピックアイテム)";
                case GearGrade.S: return "(ユニークアイテム)";
                case GearGrade.SS: return "(レジェンダリーアイテム)";
                case GearGrade.Special: return "(スペシャルアイテム)";
                default: return null;
            }
        }

        /// <summary>
        /// 获取gearType所对应的字符串。
        /// </summary>
        /// <param Name="Type">表示装备类型GearType。</param>
        /// <returns></returns>
        public static string GetGearTypeString(GearType type)
        {
            switch (type)
            {
                //case GearType.body: return "Avatar (Body)";
                case GearType.head:
                case GearType.head_n:  return "スキン";
                case GearType.face:
                case GearType.face2:
                case GearType.face_n: return "顔";
                case GearType.hair:
                case GearType.hair2:
                case GearType.hair3:
                case GearType.hair_n: return "髮";
                case GearType.faceAccessory: return "顔の飾り";
                case GearType.eyeAccessory: return "目の飾り";
                case GearType.earrings: return "イヤリング";
                case GearType.pendant: return "ペンダント";
                case GearType.belt: return "ベルト";
                case GearType.medal: return "勲章";
                case GearType.shoulderPad: return "肩飾り";
                case GearType.cap: return "帽子";
                case GearType.cape: return "マント";
                case GearType.coat: return "服 (上) ";
                case GearType.dragonMask: return "ドラゴン帽子";
                case GearType.dragonPendant: return "ドラゴンペンダント";
                case GearType.dragonWings: return "ドラゴン羽飾り";
                case GearType.dragonTail: return "ドラゴンしっぽ飾り";
                case GearType.glove: return "手袋";
                case GearType.longcoat: return "服 (全身) ";
                case GearType.machineEngine: return "メカニックエンジン";
                case GearType.machineArms: return "メカニックアーム";
                case GearType.machineLegs: return "メカニックレッグ";
                case GearType.machineBody: return "メカニックフレーム";
                case GearType.machineTransistors: return "メカニックトランジスター";
                case GearType.pants: return "服 (下) ";
                case GearType.ring: return "指輪";
                case GearType.shield: return "盾";
                case GearType.shoes: return "靴";
                case GearType.shiningRod: return "シャイニングロッド";
                case GearType.soulShooter: return "ソウルシューター";
                case GearType.ohSword: return "片手剣";
                case GearType.ohAxe: return "片手斧";
                case GearType.ohBlunt: return "片手鈍器";
                case GearType.dagger: return "短剣";
                case GearType.katara: return "ブレイド";
                case GearType.magicArrow: return "魔法矢";
                case GearType.card: return "カード";
                case GearType.box: return "コア";
                case GearType.orb: return "オーブ";
                case GearType.novaMarrow: return "ノヴァの精髄";
                case GearType.soulBangle: return "ソウルリング";
                case GearType.mailin: return "マグナム";
                case GearType.cane: return "ケイン";
                case GearType.wand: return "ワンド";
                case GearType.staff: return "スタッフ";
                case GearType.thSword: return "両手剣";
                case GearType.thAxe: return "両手斧";
                case GearType.thBlunt: return "両手鈍器";
                case GearType.spear: return "槍";
                case GearType.polearm: return "鉾";
                case GearType.bow: return "弓";
                case GearType.crossbow: return "弩";
                case GearType.throwingGlove: return "篭手";
                case GearType.knuckle: return "ナックル";
                case GearType.gun: return "銃";
                case GearType.android: return "アンドロイド";
                case GearType.machineHeart: return "機械心臓部";
                case GearType.pickaxe: return "採鉱工具";
                case GearType.shovel: return "薬草採集工具";
                case GearType.pocket: return "ポケットアイテム";
                case GearType.dualBow: return "デュアルボウガン";
                case GearType.handCannon: return "ハンドキャノン";
                case GearType.badge: return "バッジ";
                case GearType.emblem: return "エンブレム";
                case GearType.soulShield: return "ソウルシールド";
                case GearType.demonShield: return "フォースシールド";
                case GearType.totem: return "トーテム";
                case GearType.petEquip: return "ペット装備";
                case GearType.taming:
                case GearType.taming2:
                case GearType.taming3: 
                case GearType.tamingChair: return "テイムドモンスター";
                case GearType.saddle: return "鞍";
                case GearType.katana: return "刀";
                case GearType.fan: return "扇";
                case GearType.swordZB: return "大剣";
                case GearType.swordZL: return "太刀";
                case GearType.weapon: return "武器";
                case GearType.subWeapon: return "補助武器";
                case GearType.heroMedal: return "メダル";
                case GearType.rosario: return "ロザリオ";
                case GearType.chain: return "チェーン";
                case GearType.book1:
                case GearType.book2:
                case GearType.book3: return "魔導書";
                case GearType.bowMasterFeather: return "矢羽根";
                case GearType.crossBowThimble: return "弓用指貫";
                case GearType.shadowerSheath: return "短剣用鞘";
                case GearType.nightLordPoutch: return "お守り";
                case GearType.viperWristband: return "リストバンド";
                case GearType.captainSight: return "照準器";
                case GearType.cannonGunPowder:
                case GearType.cannonGunPowder2: return "火薬";
                case GearType.aranPendulum: return "錘";
                case GearType.evanPaper: return "文書";
                case GearType.battlemageBall: return "魔法玉";
                case GearType.wildHunterArrowHead: return "矢じり";
                case GearType.cygnusGem: return "宝石";
                case GearType.controller: return "コントローラー";
                case GearType.foxPearl: return "狐玉";
                case GearType.chess: return "チェスピース";
                case GearType.powerSource: return "パワーソース";

                case GearType.energySword: return "エナジーソード";
                case GearType.desperado: return "デスペラード";
                case GearType.memorialStaff: return "メモリアルスタッフ";
                case GearType.magicStick: return "テイマースティック";
                case GearType.leaf:
                case GearType.leaf2: return "リーフ";
                case GearType.boxingClaw: return "フィスト";
                case GearType.kodachi:
                case GearType.kodachi2: return "小太刀";
                case GearType.espLimiter: return "ESPリミッター";

                case GearType.GauntletBuster: return "ｶﾞﾝﾄﾚｯﾄﾘﾎﾞﾙﾊﾞｰ";
                case GearType.ExplosivePill: return "装薬";

                case GearType.chain2: return "チェーン";
                case GearType.magicGauntlet: return "マジックガントレット";
                case GearType.transmitter: return "武器転送装置";
                case GearType.magicWing: return "マジックウィング";
                case GearType.pathOfAbyss: return "パス・オブ・アビス";

                case GearType.relic: return "レリック";
                case GearType.ancientBow: return "エーンシェントボウ";

                case GearType.handFan: return "術扇";
                case GearType.fanTassel: return "飾り房";

                case GearType.tuner: return "チューナー";
                case GearType.bracelet: return "ブレスレット";

                case GearType.breathShooter: return "ブレスシューター";
                case GearType.weaponBelt: return "ウェポンベルト";

                case GearType.ornament: return "装身具";

                case GearType.chakram: return "チャクラム";
                case GearType.hexSeeker: return "ヘックスシーカー";

                case GearType.boxingCannon: return "拳封";
                case GearType.boxingSky: return "拳天";

                case GearType.arcaneSymbol: return "アーケインシンボル";
                case GearType.authenticSymbol: return "オーセンティックシンボル";
                case GearType.grandAuthenticSymbol: return "グランドオーセンティックシンボル";

                case GearType.jewel: return "宝玉";
                default: return null;
            }
        }

        /// <summary>
        /// 获取武器攻击速度所对应的字符串。
        /// </summary>
        /// <param Name="attackSpeed">表示武器的攻击速度，通常为2~9的数字。</param>
        /// <returns></returns>
        public static string GetAttackSpeedString(int attackSpeed)
        {
            switch (attackSpeed)
            {
                case 2:
                case 3: return "かなり早い";
                case 4: 
                case 5: return "速い";
                case 6: return "普通";
                case 7:
                case 8: return "遅い";
                case 9: return "かなり遅い";
                default:
                    return attackSpeed.ToString();
            }
        }

        /// <summary>
        /// 获取套装装备类型的字符串。
        /// </summary>
        /// <param Name="Type">表示套装装备类型的GearType。</param>
        /// <returns></returns>
        public static string GetSetItemGearTypeString(GearType type)
        {
            return GetGearTypeString(type);
        }

        /// <summary>
        /// 获取装备额外职业要求说明的字符串。
        /// </summary>
        /// <param Name="Type">表示装备类型的GearType。</param>
        /// <returns></returns>
        public static string GetExtraJobReqString(GearType type)
        {
            switch (type)
            {
                //0xxx
                case GearType.heroMedal: return "ヒーロー職業群着用可能";
                case GearType.rosario: return "パラディン着用可能";
                case GearType.chain: return "ダークナイト着用可能";
                case GearType.book1: return "火、毒系列魔法使い着用可能";
                case GearType.book2: return "氷、雷系列魔法使い着用可能";
                case GearType.book3: return "ビショップ系列魔法使い着用可能";
                case GearType.bowMasterFeather: return "ボウマスター着用可能";
                case GearType.crossBowThimble: return "クロスボウマスター着用可能";
                case GearType.relic: return "パスファインダー職業群着用可能";
                case GearType.shadowerSheath: return "シャドー職業群着用可能";
                case GearType.nightLordPoutch: return "ナイトロード職業群着用可能";
                case GearType.katara: return "デュアルブレード着用可能";
                case GearType.viperWristband: return "ハイパー職業群着用可能";
                case GearType.captainSight: return "キャプテン職業群着用可能";
                case GearType.cannonGunPowder:
                case GearType.cannonGunPowder2: return "キャノンシューター職業群着用可能";
                case GearType.box:
                case GearType.boxingClaw: return "ジェット着用可能";

                //1xxx
                case GearType.cygnusGem: return "シグナス騎士団着用可能";

                //2xxx
                case GearType.aranPendulum: return GetExtraJobReqString(21);
                case GearType.dragonMask:
                case GearType.dragonPendant:
                case GearType.dragonWings:
                case GearType.dragonTail:
                case GearType.evanPaper: return GetExtraJobReqString(22);
                case GearType.magicArrow: return GetExtraJobReqString(23);
                case GearType.card: return GetExtraJobReqString(24);
                case GearType.foxPearl: return GetExtraJobReqString(25);
                case GearType.orb:
                case GearType.shiningRod: return GetExtraJobReqString(27);

                //3xxx
                case GearType.demonShield: return GetExtraJobReqString(31);
                case GearType.desperado: return "デーモンアヴェンジャー着用可能";
                case GearType.battlemageBall: return "バトルメイジ着用可能";
                case GearType.wildHunterArrowHead: return "ワイルドハンター着用可能";
                case GearType.machineEngine:
                case GearType.machineArms:
                case GearType.machineLegs:
                case GearType.machineBody:
                case GearType.machineTransistors:
                case GearType.mailin: return "メカニック着用可能";
                case GearType.controller:
                case GearType.powerSource:
                case GearType.energySword: return GetExtraJobReqString(36);
                case GearType.GauntletBuster:
                case GearType.ExplosivePill: return GetExtraJobReqString(37);

                //4xxx
                case GearType.katana:
                case GearType.kodachi:
                case GearType.kodachi2: return GetExtraJobReqString(41);
                case GearType.fan: return "カンナ";

                //5xxx
                case GearType.soulShield: return "ミハエル着用可能";

                //6xxx
                case GearType.novaMarrow: return GetExtraJobReqString(61);
                case GearType.weaponBelt:
                case GearType.breathShooter: return GetExtraJobReqString(63);
                case GearType.chain2:
                case GearType.transmitter: return GetExtraJobReqString(64);
                case GearType.soulBangle:
                case GearType.soulShooter: return GetExtraJobReqString(65);

                //10xxx
                case GearType.swordZB:
                case GearType.swordZL: return GetExtraJobReqString(101);

                case GearType.magicStick: return GetExtraJobReqString(112);
                case GearType.leaf:
                case GearType.leaf2:
                case GearType.memorialStaff: return GetExtraJobReqString(172);

                case GearType.espLimiter:
                case GearType.chess: return GetExtraJobReqString(142);

                case GearType.magicGauntlet:
                case GearType.magicWing: return GetExtraJobReqString(152);

                case GearType.pathOfAbyss: return GetExtraJobReqString(155);
                case GearType.handFan:
                case GearType.fanTassel: return GetExtraJobReqString(164);

                case GearType.tuner:
                case GearType.bracelet: return GetExtraJobReqString(151);

                case GearType.boxingCannon:
                case GearType.boxingSky: return GetExtraJobReqString(175);

                case GearType.ornament: return GetExtraJobReqString(162);
                default: return null;
            }
        }

        /// <summary>
        /// 获取装备额外职业要求说明的字符串。
        /// </summary>
        /// <param Name="specJob">表示装备属性的reqSpecJob的值。</param>
        /// <returns></returns>
        public static string GetExtraJobReqString(int specJob)
        {
            switch (specJob)
            {
                case 21: return "アラン着用可能";
                case 22: return "エヴァン着用可能";
                case 23: return "メルセデス着用可能";
                case 24: return "ファントム着用可能";
                case 25: return "隠月着用可能";
                case 27: return "ルミナス着用可能";
                case 31: return "デーモン着用可能";
                case 36: return "ゼノン着用可能";
                case 37: return "ブラスター着用可能";
                case 41: return "ハヤト";
                case 42: return "カンナ";
                case 51: return "ミハエル着用可能";
                case 61: return "カイザー着用可能";
                case 63: return "カイン着用可能";
                case 64: return "カデナ着用可能";
                case 65: return "エンジェリックバスター着用可能";
                case 99: return "ハク";
                case 101: return "ゼロ着用可能";
                case 112: return "ビーストテイマー着用可能";
                case 142: return "キネシス着用可能";
                case 151: return "アデル着用可能";
                case 152: return "イリウム着用可能";
                case 154: return "カーリー着用可能";
                case 155: return "アーク着用可能";
                case 162: return "ララ着用可能";
                case 164: return "虎影着用可能";
                case 172: return "リン着用可能";
                case 175: return "墨玄着用可能";

                default: return null;
            }
        }

        public static string GetReqSpecJobMultipleString(int specJob)
        {
            switch (specJob)
            {
                case 1: return "ﾋｰﾛｰ､ﾊﾟﾗﾃﾞｨﾝ､";
                case 2: return "ｱｰｸﾒｲｼﾞ(氷･雷)､ｱｰｸﾒｲｼﾞ(火･毒)､ﾋﾞｼｮｯﾌﾟ\r\n";
                case 4: return "シャドー､";
                case 11: return "ｿｳﾙﾏｽﾀｰ､";
                case 12: return "ﾌﾚｲﾑｳｨｻﾞｰﾄﾞ､";
                case 22: return "ｴｳﾞｧﾝ､";
                case 32: return "ﾊﾞﾄﾙﾒｲｼﾞ､";
                case 172: return "ﾘﾝ､";

                default: return null;
            }
        }

        public static string GetExtraJobReqString(IEnumerable<int> specJobs)
        {
            List<string> extraJobNames = new List<string>();
            foreach (int specJob in specJobs)
            {
                switch (specJob)
                {
                    case 1: extraJobNames.AddRange(new[] { "ヒーロー", "パラデイン" }); break;
                    case 2: extraJobNames.AddRange(new[] { "ｱｰｸﾒｲｼﾞ(氷･雷)", "ｱｰｸﾒｲｼﾞ(火･毒)", "ﾋﾞｼｮｯﾌﾟ" }); break;
                    case 4: extraJobNames.Add("シャドー"); break;
                    case 11: extraJobNames.Add("ソウルマスター"); break;
                    case 12: extraJobNames.Add("\r\nﾌﾚｲﾑｳｨｻﾞｰﾄﾞ"); break;
                    case 22: extraJobNames.Add("ｴｳﾞｧﾝ"); break;
                    case 32: extraJobNames.Add("ﾊﾞﾄﾙﾒｲｼﾞ"); break;
                    case 172: extraJobNames.Add("ﾘﾝ"); break;
                    default: extraJobNames.Add(specJob.ToString()); break;
                }
            }
            if (extraJobNames.Count == 0)
            {
                return null;
            }
            return string.Join("、", extraJobNames) + "着用可能";
        }

        public static string GetItemPropString(ItemPropType propType, long value)
        {
            switch (propType)
            {
                case ItemPropType.tradeBlock:
                    return GetGearPropString(GearPropType.tradeBlock, value);
                case ItemPropType.useTradeBlock:
                    return value == 0 ? null : "使用後交換不可";
                case ItemPropType.tradeAvailable:
                    return GetGearPropString(GearPropType.tradeAvailable, value);
                case ItemPropType.only:
                    return GetGearPropString(GearPropType.only, value);
                case ItemPropType.accountSharable:
                    return GetGearPropString(GearPropType.accountSharable, value);
                case ItemPropType.sharableOnce:
                    return GetGearPropString(GearPropType.sharableOnce, value);
                case ItemPropType.accountSharableAfterExchange:
                    return GetGearPropString(GearPropType.accountSharableAfterExchange, value);
                case ItemPropType.exchangeableOnce:
                    return value == 0 ? null : "1回交換可能 (取引後交換不可)";
                case ItemPropType.quest:
                    return value == 0 ? null : "クエストアイテム";
                case ItemPropType.pquest:
                    return value == 0 ? null : "パーティクエストアイテム";
                case ItemPropType.permanent:
                    return value == 0 ? null : "魔法の時間が終わらないミラクルペットです。";
                case ItemPropType.multiPet:
                    // return value == 0 ? null : "マルチペット(他のペットと最大3個重複使用可能)";
                    return value == 0 ? "一般ペット(他の一般ペットと重複使用不可)" : "マルチペット(他のペットと最大3個重複使用可能)";
                case ItemPropType.mintable:
                    return GetGearPropString(GearPropType.mintable, value);
                default:
                    return null;
            }
        }

        public static string GetItemCoreSpecString(ItemCoreSpecType coreSpecType, int value, string desc)
        {
            bool hasCoda = false;
            if (desc?.Length > 0)
            {
                char lastCharacter = desc.Last();
                hasCoda = lastCharacter >= '가' && lastCharacter <= '힣' && (lastCharacter - '가') % 28 != 0;
            }
            switch (coreSpecType)
            {
                case ItemCoreSpecType.Ctrl_mobLv:
                    return value == 0 ? null : "Monster Level " + "+" + value;
                case ItemCoreSpecType.Ctrl_mobHPRate:
                    return value == 0 ? null : "Monster HP " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_mobRate:
                    return value == 0 ? null : "Monster Population " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_mobRateSpecial:
                    return value == 0 ? null : "Monster Population " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_change_Mob:
                    return desc == null ? null : "Change monster skins for " + desc;
                case ItemCoreSpecType.Ctrl_change_BGM:
                    return desc == null ? null : "Change music for " + desc;
                case ItemCoreSpecType.Ctrl_change_BackGrnd:
                    return desc == null ? null : "Change background image for " + desc;
                case ItemCoreSpecType.Ctrl_partyExp:
                    return value == 0 ? null : "Party EXP " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_partyExpSpecial:
                    return value == 0 ? null : "Party EXP " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_addMob:
                    return value == 0 || desc == null ? null : desc + ", Link " + value + " added to area";
                case ItemCoreSpecType.Ctrl_dropRate:
                    return value == 0 ? null : "Drop Rate " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_dropRateSpecial:
                    return value == 0 ? null : "Drop Rate " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_dropRate_Herb:
                    return value == 0 ? null : "Herb Drop Rate " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_dropRate_Mineral:
                    return value == 0 ? null : "Mineral Drop Rate " + "+" + value + "%";
                case ItemCoreSpecType.Ctrl_dropRareEquip:
                    return value == 0 ? null : "Rare Equipment Drop";
                case ItemCoreSpecType.Ctrl_reward:
                case ItemCoreSpecType.Ctrl_addMission:
                    return desc;
                default:
                    return null;
            }
        }

        public static string GetSkillReqAmount(int skillID, int reqAmount)
        {
            switch (skillID / 10000)
            {
                case 11200: return "[必要なポポSP: " + reqAmount + "]";
                case 11210: return "[必要なライSP: " + reqAmount + "]";
                case 11211: return "[必要なエカSP: " + reqAmount + "]";
                case 11212: return "[必要なアルSP: " + reqAmount + "]";
                default: return "[必要な??SP: " + reqAmount + "]";
            }
        }

        public static string GetJobName(int jobCode)
        {
            switch (jobCode)
            {
                case 0: return "初心者";
                case 100: return "ファイター";
                case 110: return "ソードマン";
                case 111: return "ナイト";
                case 112: return "ヒーロー";
                case 114: return "ヒーロー(6次)";
                case 120: return "ページ";
                case 121: return "クルセイダー";
                case 122: return "パラディン";
                case 124: return "パラディン(6次)";
                case 130: return "スピアマン";
                case 131: return "バーサーカー";
                case 132: return "ダークナイト";
                case 134: return "ダークナイト(6次)";
                case 200: return "マジシャン";
                case 210: return "ウィザード(火・毒)";
                case 211: return "メイジ(火・毒)";
                case 212: return "アークメイジ(火・毒)";
                case 214: return "アークメイジ(火・毒)(6次)";
                case 220: return "ウィザード(氷・雷)";
                case 221: return "メイジ(氷・雷)";
                case 222: return "アークメイジ(氷・雷)";
                case 224: return "アークメイジ(氷・雷)(6次)";
                case 230: return "クレリック";
                case 231: return "プリースト";
                case 232: return "ビショップ";
                case 234: return "ビショップ(6次)";
                case 300: return "弓使い";
                case 301: return "弓使い";
                case 310: return "ハンター";
                case 311: return "レンジャー";
                case 312: return "ボウマスター";
                case 314: return "ボウマスター(6次)";
                case 320: return "クロスボウマン";
                case 321: return "スナイパー";
                case 322: return "クロスボウマスター";
                case 324: return "クロスボウマスター(6次)";
                case 330: return "エンシェントアーチャー";
                case 331: return "チェイサー";
                case 332: return "パスファインダー";
                case 333: return "パスファインダー(5次)";
                case 334: return "パスファインダー(6次)";
                case 400: return "ローグ";
                case 410: return "アサシン";
                case 411: return "ハーミット";
                case 412: return "ナイトロード";
                case 414: return "ナイトロード(6次)";
                case 420: return "シーフ";
                case 421: return "マスターシーフ";
                case 422: return "シャドー";
                case 424: return "シャドー(6次)";
                case 430: return "セミデュアル";
                case 431: return "デュアル";
                case 432: return "デュアルマスター";
                case 433: return "スラッシャー";
                case 434: return "デュアルブレイド";
                case 436: return "デュアルブレイド(6次)";
                case 500: return "海賊";
                case 501: return "海賊(キャノン)";
                case 508: return "ジェット(1次)";
                case 510: return "インファイター";
                case 511: return "バッカニア";
                case 512: return "バイパー";
                case 514: return "バイパー(6次)";
                case 520: return "ガンスリンガー";
                case 521: return "ヴァイキング";
                case 522: return "キャプテン";
                case 524: return "キャプテン(6次)";
                case 530: return "キャノンシューター";
                case 531: return "キャノンブラスター";
                case 532: return "キャノンマスター";
                case 534: return "キャノンマスター(6次)";
                case 570: return "ジェット(2次)";
                case 571: return "ジェット(3次)";
                case 572: return "ジェット(4次)";

                case 800: 
                case 900: return "運用者";

                case 1000: return "ノーブレス";
                case 1100: return "ソウルマスター(1次)";
                case 1110: return "ソウルマスター(2次)";
                case 1111: return "ソウルマスター(3次)";
                case 1112: return "ソウルマスター(4次)";
                case 1114: return "ソウルマスター((6次)";
                case 1200: return "フレイムウィザード(1次)";
                case 1210: return "フレイムウィザード(2次)";
                case 1211: return "フレイムウィザード(3次)";
                case 1212: return "フレイムウィザード(4次)";
                case 1214: return "フレイムウィザード(6次)";
                case 1300: return "ウインドシューター(1次)";
                case 1310: return "ウインドシューター(2次)";
                case 1311: return "ウインドシューター(3次)";
                case 1312: return "ウィンドシューター(4次)";
                case 1314: return "ウィンドシューター(6次)";
                case 1400: return "ナイトウォーカー(1次)";
                case 1410: return "ナイトウォーカー(2次)";
                case 1411: return "ナイトウォーカー(3次)";
                case 1412: return "ナイトウォーカー(4次)";
                case 1414: return "ナイトウォーカー(6次)";
                case 1500: return "ストライカー(1次)";
                case 1510: return "ストライカー(2次)";
                case 1511: return "ストライカー(3次)";
                case 1512: return "ストライカー(4次)";
                case 1514: return "ストライカー(6次)";


                case 2000: return "アラン";
                case 2001: return "エヴァン";
                case 2002: return "メルセデス";
                case 2003: return "ファントム";
                case 2004: return "ルミナス";
                case 2005: return "隠月";
                case 2100: return "アラン(1次)";
                case 2110: return "アラン(2次)";
                case 2111: return "アラン(3次)";
                case 2112: return "アラン(4次)";
                case 2114: return "アラン(6次)";
                case 2200:
                case 2210: return "エヴァン(1次)";
                case 2211:
                case 2212:
                case 2213: return "エヴァン(2次)";
                case 2214:
                case 2215:
                case 2216: return "エヴァン(3次)";
                case 2217:
                case 2218: return "エヴァン(4次)";
                case 2220: return "エヴァン(6次)";
                case 2300: return "メルセデス(1次)";
                case 2310: return "メルセデス(2次)";
                case 2311: return "メルセデス(3次)";
                case 2312: return "メルセデス(4次)";
                case 2314: return "メルセデス(6次)";
                case 2400: return "ファントム(1次)";
                case 2410: return "ファントム(2次)";
                case 2411: return "ファントム(3次)";
                case 2412: return "ファントム(4次)";
                case 2414: return "ファントム(6次)";
                case 2500: return "隠月(1次)";
                case 2510: return "隠月(2次)";
                case 2511: return "隠月(3次)";
                case 2512: return "隠月(4次)";
                case 2514: return "隠月(6次)";
                case 2700: return "ルミナス(1次)";
                case 2710: return "ルミナス(2次)";
                case 2711: return "ルミナス(3次)";
                case 2712: return "ルミナス(4次)";
                case 2714: return "ルミナス(6次)";


                case 3000: return "市民";
                case 3001: return "デーモン";
                case 3100: return "デーモンスレイヤー(1次)";
                case 3110: return "デーモンスレイヤー(2次)";
                case 3111: return "デーモンスレイヤー(3次)";
                case 3112: return "デーモンスレイヤー(4次)";
                case 3114: return "デーモンスレイヤー(6次)";
                case 3101: return "デーモンアヴェンジャー(1次)";
                case 3120: return "デーモンアヴェンジャー(2次)";
                case 3121: return "デーモンアヴェンジャー(3次)";
                case 3122: return "デーモンアヴェンジャー(4次)";
                case 3124: return "デーモンアヴェンジャー(6次)";
                case 3200: return "バトルメイジ(1次)";
                case 3210: return "バトルメイジ(2次)";
                case 3211: return "バトルメイジ(3次)";
                case 3212: return "バトルメイジ(4次)";
                case 3214: return "バトルメイジ(6次)";
                case 3300: return "ワイルドハンター(1次)";
                case 3310: return "ワイルドハンター(2次)";
                case 3311: return "ワイルドハンター(3次)";
                case 3312: return "ワイルドハンター(4次)";
                case 3314: return "ワイルドハンター(6次)";
                case 3500: return "メカニック(1次)";
                case 3510: return "メカニック(2次)";
                case 3511: return "メカニック(3次)";
                case 3512: return "メカニック(4次)";
                case 3514: return "メカニック(6次)";
                case 3002: return "ゼノン";
                case 3600: return "ゼノン(1次)";
                case 3610: return "ゼノン(2次)";
                case 3611: return "ゼノン(3次)";
                case 3612: return "ゼノン(4次)";
                case 3614: return "ゼノン(6次)";
                case 3700: return "ブラスター(1次)";
                case 3710: return "ブラスター(2次)";
                case 3711: return "ブラスター(3次)";
                case 3712: return "ブラスター(4次)";
                case 3714: return "ブラスター(6次)";

                case 4001: return "ハヤト";
                case 4002: return "カンナ";
                case 4100: return "ハヤト(1次)";
                case 4110: return "ハヤト(2次)";
                case 4111: return "ハヤト(3次)";
                case 4112: return "ハヤト(4次)";
                case 4114: return "ハヤト(6次)";
                case 4200: return "カンナ(1次)";
                case 4210: return "カンナ(2次)";
                case 4211: return "カンナ(3次)";
                case 4212: return "カンナ(4次)";
                case 4216: return "カンナ(6次)";


                case 5000: return "ミハエル";
                case 5100: return "ミハエル(1次)";
                case 5110: return "ミハエル(2次)";
                case 5111: return "ミハエル(3次)";
                case 5112: return "ミハエル(4次)";
                case 5114: return "ミハエル(6次)";


                case 6000: return "カイザー";
                case 6001: return "エンジェリックバスター";
                case 6002: return "カデナ";
                case 6003: return "カイン";
                case 6100: return "カイザー(1次)";
                case 6110: return "カイザー(2次)";
                case 6111: return "カイザー(3次)";
                case 6112: return "カイザー(4次)";
                case 6114: return "カイザー(6次)";
                case 6300: return "カイン(1次)";
                case 6310: return "カイン(2次)";
                case 6311: return "カイン(3次)";
                case 6312: return "カイン(4次)";
                case 6314: return "カイン(6次)";
                case 6400: return "カデナ(1次)";
                case 6410: return "カデナ(2次)";
                case 6411: return "カデナ(3次)";
                case 6412: return "カデナ(4次)";
                case 6414: return "カデナ(6次)";
                case 6500: return "エンジェリックバスター(1次)";
                case 6510: return "エンジェリックバスター(2次)";
                case 6511: return "エンジェリックバスター(3次)";
                case 6512: return "エンジェリックバスター(4次)";
                case 6514: return "エンジェリックバスター(6次)";

                case 7000: return "アビリティ";
                case 7100: return "ユニオン";
                case 7200: return "モンスターライフ";


                case 9100: return "ギルド";
                case 9200:
                case 9201:
                case 9202:
                case 9203:
                case 9204: return "専業技術";

                case 10000: return "ゼロ";
                case 10100: return "ゼロ(1次)";
                case 10110: return "ゼロ(2次)";
                case 10111: return "ゼロ(3次)";
                case 10112: return "ゼロ(4次)";
                case 10114: return "ゼロ(6次)";

                case 11000: return "ビーストテイマー";
                case 11200: return "ビーストテイマー(ポポ)";
                case 11210: return "ビーストテイマー(ライ)";
                case 11211: return "ビーストテイマー(エカ)";
                case 11212: return "ビーストテイマー(アル)";

                case 12000:
                case 12005:
                case 12100: return "竈門炭治郎";

                case 13000: return "ピンクビーン";
                case 13001: return "イェティ";
                case 13100: return "ピンクビーン";
                case 13500: return "イェティ";

                case 14000: return "キネシス";
                case 14200: return "キネシス(1次)";
                case 14210: return "キネシス(2次)";
                case 14211: return "キネシス(3次)";
                case 14212: return "キネシス(4次)";
                case 14213: return "キネシス(5次)";
                case 14214: return "キネシス(6次)";

                case 15000: return "イリウム";
                case 15001: return "アーク";
                case 15002: return "アデル";
                case 15003: return "カーリー";
                case 15100: return "アデル(1次)";
                case 15110: return "アデル(2次)";
                case 15111: return "アデル(3次)";
                case 15112: return "アデル(4次)";
                case 15114: return "アデル(6次)";
                case 15200: return "イリウム(1次)";
                case 15210: return "イリウム(2次)";
                case 15211: return "イリウム(3次)";
                case 15212: return "イリウム(4次)";
                case 15214: return "イリウム(6次)";
                case 15400: return "カーリー(1次)";
                case 15410: return "カーリー(2次)";
                case 15411: return "カーリー(3次)";
                case 15412: return "カーリー(4次)";
                case 15414: return "カーリー(6次)";
                case 15500: return "アーク(1次)";
                case 15510: return "アーク(2次)";
                case 15511: return "アーク(3次)";
                case 15512: return "アーク(4次)";
                case 15514: return "アーク(6次)";

                case 16000: return "アニマ盗賊";
                case 16001: return "ララ";
                case 16200: return "ララ(1次)";
                case 16210: return "ララ(2次)";
                case 16211: return "ララ(3次)";
                case 16212: return "ララ(4次)";
                case 16214: return "ララ(6次)";
                case 16400: return "虎影(1次)";
                case 16410: return "虎影(2次)";
                case 16411: return "虎影(3次)";
                case 16412: return "虎影(4次)";
                case 16414: return "虎影(6次)";

                case 17000: return "墨玄";
                case 17001: return "リン";
                case 17200: return "リン(1次)";
                case 17210: return "リン(2次)";
                case 17211: return "リン(3次)";
                case 17212: return "リン(4次)";
                case 17214: return "リン(6次)";
                case 17500: return "墨玄(1次)";
                case 17510: return "墨玄(2次)";
                case 17511: return "墨玄(3次)";
                case 17512: return "墨玄(4次)";
                case 17514: return "墨玄(6次)";

                case 40000: return "5次";
                case 40001: return "5次(戦士)";
                case 40002: return "5次(魔法使い)";
                case 40003: return "5次(弓使い)";
                case 40004: return "5次(盗賊)";
                case 40005: return "5次(海賊)";


                case 50000: return "6次";
                case 50006: return "6次(強化コア)";
                case 50007: return "6次(ヘキサスタット)";
            }
            return null;
        }

        public static string ToCJKNumberExpr(long value)
        {
            var sb = new StringBuilder(16);
            bool firstPart = true;
            if (value < 0)
            {
                sb.Append("-");
                value = -value; // just ignore the exception -2147483648
            }
            if (value >= 1_0000_0000)
            {
                long part = value / 1_0000_0000;
                sb.AppendFormat("{0}億", part); // Korean: 억, TradChinese+Japanese: 億, SimpChinese: 亿
                value -= part * 1_0000_0000;
                firstPart = false;
            }
            if (value >= 1_0000)
            {
                long part = value / 1_0000;
                sb.Append(firstPart ? null : " ");
                sb.AppendFormat("{0}万", part); // Korean: 만, TradChinese: 萬, SimpChinese+Japanese: 万
                value -= part * 1_0000;
                firstPart = false;
            }
            if (value > 0)
            {
                sb.Append(firstPart ? null : " ");
                sb.AppendFormat("{0}", value);
            }

            return sb.Length > 0 ? sb.ToString() : "0";
        }
    }
}
