namespace WzComparerR2
{
    partial class FrmSetupWizard
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
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackButtonText = "戻る(&B)";
            this.wizard1.CancelButtonText = "キャンセル";
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FinishButtonText = "完了(&F)";
            // 
            // 
            // 
            this.wizard1.FooterStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard1.HeaderCaptionFont = new System.Drawing.Font("MS Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.HeaderDescriptionFont = new System.Drawing.Font("MS Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wizard1.HeaderDescriptionIndent = 16;
            // 
            // 
            // 
            this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Etched;
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.wizard1.HeaderStyle.BorderRightWidth = 1;
            this.wizard1.HeaderStyle.BorderTopWidth = 1;
            this.wizard1.HeaderStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard1.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.HelpButtonVisible = false;
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "Wizard";
            this.wizard1.NextButtonText = "次へ(&N)";
            this.wizard1.Size = new System.Drawing.Size(548, 375);
            this.wizard1.TabIndex = 0;
            // 
            // FrmSetupWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 370);
            this.Controls.Add(this.wizard1);
            this.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetupWizard";
            this.Text = "初回実行ウィザード";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmSetupWizard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard1;
    }
}