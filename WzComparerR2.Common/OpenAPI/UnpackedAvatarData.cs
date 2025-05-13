﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET6_0_OR_GREATER
namespace WzComparerR2.OpenAPI
{
    public class UnpackedAvatarData
    {
        public UnpackedAvatarData(int version)
        {
            Version = version;
            UnknownVer = false;

            if (!Utils.Structure.ContainsKey(version))
            {
                if (version < Utils.Structure.Keys.Min())
                {
                    version = Utils.Structure.Keys.Min();
                }
                else
                {
                    version = Utils.Structure.Keys.Max();
                }
                UnknownVer = true;
            }
                
            Unpacked = Utils.Structure[version].Select(d => new DataInfo(d.Name, d.Bits)
            {
                Value = d.Value
            }).ToList();
        }

        public int Version { get; set; }
        public bool UnknownVer { get; set; }
        public List<DataInfo> Unpacked { get; set; }

        public int GetValue(string name)
        {
            foreach (var data in Unpacked)
            {
                if (data.Name == name)
                {
                    if ((data.Value ^ 0x3FF) != 0)
                        return data.Value;
                    else return -1;
                }
            }

            return -1;
        }

        public int GetBits(string name)
        {
            foreach (var data in Unpacked)
            {
                if (data.Name == name)
                {
                    if ((data.Value ^ 0x3FF) != 0)
                        return data.Bits;
                    else return -1;
                }
            }

            return -1;
        }

        public int GetGender()
        {
            return GetValue("gender") != 0 ? 1 : 0;
        }

        public string GetSkin()
        {
            return GetValue("skinID").ToString().PadLeft(2, '0');
        }

        public string GetFace()
        {
            var id = GetValue("faceID");
            if (id == -1) return "";

            var ret = "";
            ret += GetValue("face50k") != 0 ? 5 : 2;
            ret += GetValue("faceGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetHair()
        {
            var id = GetValue("hairID");
            if (id == -1) return "";

            var ret = "";
            ret += GetValue("hair10k");
            ret += GetValue("hairGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetCap()
        {
            var id = GetValue("capID");
            if (id == -1) return "";

            var ret = "100";
            ret += GetValue("capGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetFaceAcc()
        {
            var id = GetValue("faceAccID");
            if (id == -1) return "";

            var ret = "101";
            ret += GetValue("faceAccGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetEyeAcc()
        {
            var id = GetValue("eyeAccID");
            if (id == -1) return "";

            var ret = "102";
            ret += GetValue("eyeAccGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetEarAcc()
        {
            var id = GetValue("earAccID");
            if (id == -1) return "";

            var ret = "103";
            ret += GetValue("earAccGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetCoat()
        {
            var id = GetValue("coatID");
            if (id == -1) return "";

            var ret = GetValue("isLongCoat") == 1 ? "105" : "104";
            ret += GetValue("coatGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetPants()
        {
            var id = GetValue("pantsID");
            if (id == -1) return "";

            var ret = "106";
            ret += GetValue("pantsGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetShoes()
        {
            var id = GetValue("shoesID");
            if (id == -1) return "";

            var ret = "107";
            ret += GetValue("shoesGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetGloves()
        {
            var id = GetValue("glovesID");
            if (id == -1) return "";

            var ret = "108";
            ret += GetValue("glovesGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetCape()
        {
            var id = GetValue("capeID");
            if (id == -1) return "";

            var ret = "110";
            ret += GetValue("capeGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetShield()
        {
            var id = GetValue("shieldID");
            if (id == -1) return "";

            var ret = "";
            if (GetValue("isNotBlade") == 0) ret += 134;
            else if (GetValue("isSubWeapon") != 0) ret += 135;
            else ret += 109;
            ret += GetValue("shieldGender");
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetCashWeapon()
        {
            bool isCW = GetValue("isCashWeapon") != 0;
            if (!isCW) return "";

            var id = GetValue("cashWeaponID");
            if (id == -1) id = GetValue("weaponID");

            var g = GetValue("cashWeaponGender");
            if (g == -1) g = GetValue("weaponGender");

            var ret = "170";
            ret += g;
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetWeapon()
        {
            var id = GetValue("weaponID");
            var type = GetValue("weaponType");
            if (id == -1) return "";

            var ret = "";
            try
            {
                ret += Utils.WeaponsKMS[type].ToString();
            }
            catch
            {
                return ret;
            }

            var g = GetValue("weaponGender").ToString();
            if (int.Parse(g) <= 0) g = "";

            ret += g;
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public string GetRing(int num)
        {
            var id = GetValue("ringID" + num);
            if (id == -1) return "";

            var g = GetValue("ringGender" + num);
            if (id <= 0 && g <= 0) return "";

            var ret = "111";
            ret += g;
            ret += id.ToString().PadLeft(3, '0');
            return ret;
        }

        public byte GetEarType()
        {
            return (byte)GetValue("earType");
        }

        public byte GetJobWingTailType()
        {
            return (byte)GetValue("jobWingTailType");
        }

        public string GetJobWingTailTypeString()
        {
            switch (this.JobWingTailType)
            {
                case 1:
                    return "虎影";
                case 2:
                    return "ララ";
                default:
                    return null;
            }
        }

        public byte GetWeaponMotionType()
        {
            return (byte)GetValue("weaponMotion");
        }

        public string GetWeaponMotionTypeString()
        {
            switch (this.WeaponMotionType)
            {
                case 1:
                    return "片手武器モーション";
                case 2:
                    return "両手武器モーション";
                case 3:
                    return "銃武器モーション";
                default:
                    return "基本武器モーション";
            }
        }

        public string GetMixHairRatio()
        {
            return GetValue("mixHairRatio").ToString().PadLeft(2, '0');
        }

        public string GetMixHairColor()
        {
            return GetValue("mixHairColor").ToString();
        }

        public string GetMixFaceRatio()
        {
            return GetValue("mixFaceInfo").ToString().PadLeft(3, '0').Substring(1, 2);
        }

        public string GetMixFaceColor()
        {
            return GetValue("mixFaceInfo").ToString().PadLeft(3, '0').Substring(0, 1);
        }

        public PrismInfo GetPrismInfo(string type)
        {
            var ret = new PrismInfo();
            if (GetValue($"has{type}Prism") != 0)
            {
                ret.ColorType = (byte)GetValue($"{type.ToLower()}PrismColorType");
                ret.Brightness = GetValue($"{type.ToLower()}PrismBrightness") - 100;
                ret.Saturation = GetValue($"{type.ToLower()}PrismSaturation") - 100;
                ret.Hue = GetValue($"{type.ToLower()}PrismHue");
                ret.Valid = true;
            }
            else
            {
                ret.Valid = false;
            }
            return ret;
        }

        public void SetProperties()
        {
            Gender = GetGender();

            Skin = GetSkin();
            Face = GetFace();
            Hair = GetHair();

            Cap = GetCap();
            FaceAcc = GetFaceAcc();
            EyeAcc = GetEyeAcc();
            EarAcc = GetEarAcc();
            Coat = GetCoat();
            Pants = GetPants();
            Shoes = GetShoes();
            Gloves = GetGloves();
            Cape = GetCape();
            Shield = GetShield();
            CashWeapon = GetCashWeapon();
            Weapon = GetWeapon();

            Ring1 = GetRing(1);
            Ring2 = GetRing(2);
            Ring3 = GetRing(3);
            Ring4 = GetRing(4);

            EarType = GetEarType();
            JobWingTailType = GetJobWingTailType();
            WeaponMotionType = GetWeaponMotionType();

            MixHairRatio = GetMixHairRatio();
            MixHairColor = GetMixHairColor();
            MixFaceRatio = GetMixFaceRatio();
            MixFaceColor = GetMixFaceColor();

            CapPrismInfo = GetPrismInfo("Cap");
            CoatPrismInfo = GetPrismInfo("Coat");
            PantsPrismInfo = GetPrismInfo("Pants");
            ShoesPrismInfo = GetPrismInfo("Shoes");
            GlovesPrismInfo = GetPrismInfo("Gloves");
            CapePrismInfo = GetPrismInfo("Cape");
            WeaponPrismInfo = GetPrismInfo("Weapon");
            SkinPrismInfo = GetPrismInfo("Skin");
        }

        public int Gender { get; set; }
        public string Skin { get; set; }
        public string Face { get; set; }
        public string Hair { get; set; }
        public string Cap { get; set; }
        public string FaceAcc { get; set; }
        public string EyeAcc { get; set; }
        public string EarAcc { get; set; }
        public string Coat { get; set; }
        public string Pants { get; set; }
        public string Shoes { get; set; }
        public string Gloves { get; set; }
        public string Cape { get; set; }
        public string Shield { get; set; }
        public string CashWeapon { get; set; }
        public string Weapon { get; set; }
        public string Ring1 { get; set; }
        public string Ring2 { get; set; }
        public string Ring3 { get; set; }
        public string Ring4 { get; set; }
        public byte EarType { get; set; }
        public byte JobWingTailType { get; set; }
        public byte WeaponMotionType { get; set; }
        public string MixHairRatio { get; set; }
        public string MixHairColor { get; set; }
        public string MixFaceRatio { get; set; }
        public string MixFaceColor { get; set; }
        public PrismInfo CapPrismInfo { get; set; }
        public PrismInfo CoatPrismInfo { get; set; }
        public PrismInfo PantsPrismInfo { get; set; }
        public PrismInfo ShoesPrismInfo { get; set; }
        public PrismInfo GlovesPrismInfo { get; set; }
        public PrismInfo CapePrismInfo { get; set; }
        public PrismInfo WeaponPrismInfo { get; set; }
        public PrismInfo SkinPrismInfo { get; set; }
    }

    public class PrismInfo
    {
        public bool Valid { get; set; }
        public byte ColorType { get; set; }
        public int Hue { get; set; }
        public int Saturation { get; set; }
        public int Brightness { get; set; }

        public bool HasValues()
        {
            return this.Valid;
        }

        public string GetColorType()
        {
            switch (ColorType)
            {
                case 0:
                    return "全色系";
                case 1:
                    return "赤い系";
                case 2:
                    return "黄色系";
                case 3:
                    return "緑色系";
                case 4:
                    return "ターコイズ系";
                case 5:
                    return "青色系";
                case 6:
                    return "紫色系";
                default:
                    return null;
            }
        }
    }
}
#endif