﻿namespace WzComparerR2
{
    partial class FrmGMSDownloader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGMSDownloader));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.lblLatestVer = new DevComponents.DotNetBar.LabelX();
            this.lblUpdateDate = new DevComponents.DotNetBar.LabelX();
            this.lblUpdateContent = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.richTextBoxEx1 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 9);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 16);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "最新バージョン:";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(11, 33);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(81, 16);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "配信日時:";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(11, 57);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(50, 16);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "バージョン情報:";
            // 
            // lblLatestVer
            // 
            this.lblLatestVer.AutoSize = true;
            // 
            // 
            // 
            this.lblLatestVer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLatestVer.Location = new System.Drawing.Point(110, 9);
            this.lblLatestVer.Name = "lblLatestVer";
            this.lblLatestVer.Size = new System.Drawing.Size(13, 16);
            this.lblLatestVer.TabIndex = 4;
            this.lblLatestVer.Text = "-";
            // 
            // lblUpdateDate
            // 
            this.lblUpdateDate.AutoSize = true;
            // 
            // 
            // 
            this.lblUpdateDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUpdateDate.Location = new System.Drawing.Point(110, 33);
            this.lblUpdateDate.Name = "lblUpdateDate";
            this.lblUpdateDate.Size = new System.Drawing.Size(13, 16);
            this.lblUpdateDate.TabIndex = 5;
            this.lblUpdateDate.Text = "-";
            // 
            // lblUpdateContent
            // 
            this.lblUpdateContent.AutoSize = true;
            // 
            // 
            // 
            this.lblUpdateContent.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUpdateContent.Location = new System.Drawing.Point(110, 57);
            this.lblUpdateContent.Name = "lblUpdateContent";
            this.lblUpdateContent.Size = new System.Drawing.Size(13, 16);
            this.lblUpdateContent.TabIndex = 6;
            this.lblUpdateContent.Text = LocalizedString_JP.FRMUPDATER_UPDATE_CHECKING;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            // this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX1.Location = new System.Drawing.Point(110, 187);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(85, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 8;
            this.buttonX1.Enabled = false;
            this.buttonX1.Text = "ダウンロード";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // richTextBoxEx1
            // 
            // 
            // 
            // 
            this.richTextBoxEx1.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx1.Location = new System.Drawing.Point(12, 81);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "129 \\\'b5\\\'b8\\\'bf\\\'f2;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
            this.richTextBoxEx1.Size = new System.Drawing.Size(280, 100);
            this.richTextBoxEx1.ReadOnly = true;
            this.richTextBoxEx1.TabIndex = 9;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // FrmGMSDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 221);
            this.Controls.Add(this.richTextBoxEx1);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.lblUpdateContent);
            this.Controls.Add(this.lblUpdateDate);
            this.Controls.Add(this.lblLatestVer);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font(LocalizedString_JP.DEFAULT_MONOSPACE_FONT, 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGMSDownloader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GMSダウンローダー";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX lblLatestVer;
        private DevComponents.DotNetBar.LabelX lblUpdateDate;
        private DevComponents.DotNetBar.LabelX lblUpdateContent;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}