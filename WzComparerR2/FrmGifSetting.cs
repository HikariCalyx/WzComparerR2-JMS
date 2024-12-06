using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using DevComponents.DotNetBar;
using WzComparerR2.Config;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace WzComparerR2
{
    public partial class FrmGifSetting : DevComponents.DotNetBar.Office2007Form
    {

        public FrmGifSetting()
        {
            InitializeComponent();
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("MS PGothic"), 9f);
#endif
            initSelection();
        }

        private void initSelection()
        {
            comboBoxEx1.SelectedIndex = 0;
            comboBoxEx2.SelectedIndex = 0;
        }

        public bool SavePngFramesEnabled
        {
            get { return checkBoxX2.Checked; }
            set { checkBoxX2.Checked = value; }
        }

        public int GifEncoder
        {
            get { return comboBoxEx1.SelectedIndex; }
            set { comboBoxEx1.SelectedIndex = MathHelper.Clamp(value, 0, comboBoxEx1.Items.Count - 1); }
        }

        public ImageNameMethod ImageNameMethod
        {
            get { return (ImageNameMethod)comboBoxEx2.SelectedIndex; }
            set { comboBoxEx2.SelectedIndex = MathHelper.Clamp((int)value, 0, comboBoxEx2.Items.Count - 1); }
        }

        public ImageBackgroundType BackgroundType
        {
            get
            {
                if (rdoColor.Checked)
                {
                    return checkBoxX1.Checked ? ImageBackgroundType.Transparent : ImageBackgroundType.Color;
                }
                else if (rdoMosaic.Checked)
                {
                    return ImageBackgroundType.Mosaic;
                }
                else //默认
                {
                    return ImageBackgroundType.Transparent;
                }
            }
            set
            {
                switch (value)
                {
                    default:
                    case ImageBackgroundType.Transparent:
                        rdoColor.Checked = true;
                        checkBoxX1.Checked = true;
                        break;

                    case ImageBackgroundType.Color:
                        rdoColor.Checked = true;
                        checkBoxX1.Checked = false;
                        break;

                    case ImageBackgroundType.Mosaic:
                        rdoMosaic.Checked = true;
                        break;
                }
            }
        }

        public Color BackgroundColor
        {
            get { return colorPickerButton1.SelectedColor; }
            set { colorPickerButton1.SelectedColor = value; }
        }

        public int MinMixedAlpha
        {
            get { return slider1.Value; }
            set { slider1.Value = MathHelper.Clamp(value, slider1.Minimum, slider1.Maximum); }
        }

        public int MinDelay
        {
            get { return integerInput1.Value; }
            set { integerInput1.Value = MathHelper.Clamp(value, integerInput1.MinValue, integerInput1.MaxValue); }
        }

        public Color MosaicColor0
        {
            get { return colorPickerButton2.SelectedColor; }
            set { colorPickerButton2.SelectedColor = value; }
        }

        public Color MosaicColor1
        {
            get { return colorPickerButton3.SelectedColor; }
            set { colorPickerButton3.SelectedColor = value; }
        }

        public int MosaicBlockSize
        {
            get { return slider2.Value; }
            set { slider2.Value = MathHelper.Clamp(value, slider2.Minimum, slider2.Maximum); }
        }

        public bool PaletteOptimized
        {
            get { return checkBoxX3.Checked; }
            set { checkBoxX3.Checked = value; }
        }

        public string ffmpegPath { get; set; }

        public void Load(ImageHandlerConfig config)
        {
            this.SavePngFramesEnabled = config.SavePngFramesEnabled;
            this.GifEncoder = config.GifEncoder;
            this.ImageNameMethod = config.ImageNameMethod;
            this.BackgroundType = config.BackgroundType;
            this.BackgroundColor = config.BackgroundColor;
            this.MinMixedAlpha = config.MinMixedAlpha;
            this.MinDelay = config.MinDelay;

            this.MosaicColor0 = config.MosaicInfo.Color0;
            this.MosaicColor1 = config.MosaicInfo.Color1;
            this.MosaicBlockSize = config.MosaicInfo.BlockSize;

            this.PaletteOptimized = config.PaletteOptimized;
            this.ffmpegPath = config.ffmpegPath;
        }

        public void Save(ImageHandlerConfig config)
        {
            config.SavePngFramesEnabled = this.SavePngFramesEnabled;
            config.GifEncoder = this.GifEncoder;
            config.ImageNameMethod = this.ImageNameMethod;
            config.BackgroundType = this.BackgroundType;
            config.BackgroundColor = this.BackgroundColor;
            config.MinMixedAlpha = this.MinMixedAlpha;
            config.MinDelay = this.MinDelay;

            config.MosaicInfo.Color0 = this.MosaicColor0;
            config.MosaicInfo.Color1 = this.MosaicColor1;
            config.MosaicInfo.BlockSize = this.MosaicBlockSize;

            config.PaletteOptimized = this.PaletteOptimized;
            config.ffmpegPath = this.ffmpegPath;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedIndex == 3 && !File.Exists(this.ffmpegPath) && String.IsNullOrEmpty(this.ffmpegPath))
            {
                DialogResult dialogResult = MessageBoxEx.Show("OKをクリックする前にFFMPEG.exeを指定する必要があります。\r\n今すぐFFMPEG.exeの場所を指定しますか?", "注意", MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        buttonX3_Click(sender, e);
                        if (!File.Exists(this.ffmpegPath) && String.IsNullOrEmpty(this.ffmpegPath)) return;
                        break;
                    case DialogResult.No:
                    default:
                        return;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "FFMPEGの選択";
            dlg.Filter = "FFMPEG (ffmpeg.exe)|ffmpeg.exe";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.ffmpegPath = dlg.FileName;
            }
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            var slider = sender as DevComponents.DotNetBar.Controls.Slider;
            slider.Text = slider.Value.ToString();
        }

        private void rdoColor_CheckedChanged(object sender, EventArgs e)
        {
            panelExColor.Enabled = rdoColor.Checked;
        }

        private void rdoMosaic_CheckedChanged(object sender, EventArgs e)
        {
            panelExMosaic.Enabled = rdoMosaic.Checked;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs args)
        {
            if (comboBoxEx1.SelectedIndex == 3) // x264 Encoder is selected
            {
                checkBoxX1.Checked = false;
                checkBoxX1.Enabled = false;
                slider1.Enabled = false;
                buttonX3.Enabled = true;
            }
            else
            {
                checkBoxX1.Enabled = true;
                slider1.Enabled = true;
                buttonX3.Enabled = false;
            }
        }

    }
}