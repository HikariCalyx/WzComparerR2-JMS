﻿using DevComponents.Editors;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WzComparerR2.CharaSim;

namespace WzComparerR2.Avatar.UI
{
    public partial class AvatarAPIForm : DevComponents.DotNetBar.Office2007Form
    {
        public AvatarAPIForm()
        {
            InitializeComponent();
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("MS Gothic"), 9f);
#endif
            cmbRegion.Items.AddRange(new[]
            {
                new ComboItem("KMS"){ Value = 1 },
                new ComboItem("JMS"){ Value = 2 },
                //new ComboItem("CMS"){ Value = 3 },
                new ComboItem("GMS北米"){ Value = 4 },
                new ComboItem("GMS欧州"){ Value = 5 },
                new ComboItem("MSEA"){ Value = 6 },
                new ComboItem("TMS"){ Value = 7 },
                new ComboItem("MSN"){ Value = 8 },
            });
        }

        public string CharaName
        {
            get { return textBoxX1.Text; }
            set { textBoxX1.Text = value; }
        }

        public bool Type1
        {
            get { return checkBoxX1.Checked; }
        }

        public int selectedRegion
        {
            get
            {
                return ((cmbRegion.SelectedItem as ComboItem)?.Value as int?) ?? 0;
            }
            set
            {
                var items = cmbRegion.Items.Cast<ComboItem>();
                var item = items.FirstOrDefault(_item => _item.Value as int? == value)
                    ?? items.Last();
                item.Value = value;
                cmbRegion.SelectedItem = item;
            }
        }

        private bool IsFullWidth(char c)
        {
           return (c >= 0xFF01 && c <= 0xFF5E) || // Full-width ASCII
           (c >= 0x3000 && c <= 0x30FF) || // CJK symbols & kana
           (c >= 0x4E00 && c <= 0x9FFF) || // Common CJK ideographs
           (c >= 0xF900 && c <= 0xFAFF);   // CJK Compatibility
        }

        private bool IsEmoji(char c)
        {
            return char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherSymbol;
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            string text = textBoxX1.Text;
            int fullWidthCount = 0;
            int halfWidthCount = 0;
            string filteredText = "";

            foreach (char c in text)
            {
                if (IsEmoji(c))
                    continue;
                else if (IsFullWidth(c))
                    fullWidthCount++;
                else
                    halfWidthCount++;

                filteredText += c;
            }

            if (fullWidthCount * 2 + halfWidthCount > 12)
            {
                filteredText = filteredText.Substring(0, filteredText.Length - 1);
            }

            if (textBoxX1.Text != filteredText)
            {
                int cursorPos = textBoxX1.SelectionStart;
                textBoxX1.Text = filteredText;
                textBoxX1.SelectionStart = Math.Min(cursorPos, textBoxX1.Text.Length);
            }

            ComboItem selectedItem = (ComboItem)cmbRegion.SelectedItem;
            if (Translator.IsKoreanStringPresent(textBoxX1.Text))
            {
                textBoxX1.Font = new Font("Dotum", 9f);
            }
            else
            {
                textBoxX1.Font = new Font("MS PGothic", 9f);
            }
            buttonX1.Enabled = (textBoxX1.Text.Length > 0 && selectedItem != null);
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonX1.Enabled = textBoxX1.Text.Length > 0;
            ComboItem selectedItem = (ComboItem)cmbRegion.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            switch ((int)selectedItem.Value)
            {
                case 2:
                    labelX1.Text = "結果のアバターは正確ではない可能性があります。";
                    labelX1.Visible = true;
                    break;
                case 4:
                case 5:
                    labelX1.Text = "長年ログインしていないキャラクターは正しく読み\r\n取れない可能性があります。";
                    labelX1.Visible = true;
                    break;
                case 6:
                    labelX1.Text = "見つからない場合はキャラクターにログインして\r\nください。";
                    labelX1.Visible = true;
                    break;
                case 7:
                    labelX1.Text = "ユニオンランキングに掲載されているキャラクター\r\nのみ検索できます。";
                    labelX1.Visible = true;
                    break;
                default:
                    labelX1.Visible = false;
                    break;
            }
        }
    }
}