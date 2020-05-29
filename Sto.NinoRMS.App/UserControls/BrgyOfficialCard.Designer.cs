namespace Sto.NinoRMS.App.UserControls
{
    partial class BrgyOfficialCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imgPic = new System.Windows.Forms.PictureBox();
            this.lblFullName = new DevComponents.DotNetBar.LabelX();
            this.lblPosition = new DevComponents.DotNetBar.LabelX();
            this.lblDesignation = new DevComponents.DotNetBar.LabelX();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgPic)).BeginInit();
            this.SuspendLayout();
            // 
            // imgPic
            // 
            this.imgPic.Image = global::Sto.NinoRMS.App.Properties.Resources.logo1;
            this.imgPic.Location = new System.Drawing.Point(10, 11);
            this.imgPic.Name = "imgPic";
            this.imgPic.Size = new System.Drawing.Size(72, 72);
            this.imgPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgPic.TabIndex = 2;
            this.imgPic.TabStop = false;
            // 
            // lblFullName
            // 
            // 
            // 
            // 
            this.lblFullName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(26)))), ((int)(((byte)(34)))));
            this.lblFullName.Location = new System.Drawing.Point(87, 6);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.PaddingTop = 5;
            this.lblFullName.SingleLineColor = System.Drawing.Color.Silver;
            this.lblFullName.Size = new System.Drawing.Size(223, 28);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "FullName";
            this.lblFullName.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblFullName.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.lblFullName.WordWrap = true;
            // 
            // lblPosition
            // 
            this.lblPosition.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // 
            // 
            this.lblPosition.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPosition.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblPosition.Location = new System.Drawing.Point(87, 34);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.SingleLineColor = System.Drawing.Color.Silver;
            this.lblPosition.Size = new System.Drawing.Size(223, 25);
            this.lblPosition.TabIndex = 4;
            this.lblPosition.Text = "Position";
            this.lblPosition.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPosition.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.lblPosition.WordWrap = true;
            // 
            // lblDesignation
            // 
            // 
            // 
            // 
            this.lblDesignation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDesignation.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDesignation.Location = new System.Drawing.Point(86, 57);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.SingleLineColor = System.Drawing.Color.Silver;
            this.lblDesignation.Size = new System.Drawing.Size(224, 40);
            this.lblDesignation.TabIndex = 5;
            this.lblDesignation.Text = "Designation";
            this.lblDesignation.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblDesignation.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.lblDesignation.WordWrap = true;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // BrgyOfficialCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.lblDesignation);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.imgPic);
            this.Name = "BrgyOfficialCard";
            this.Size = new System.Drawing.Size(315, 100);
            ((System.ComponentModel.ISupportInitialize)(this.imgPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgPic;
        private DevComponents.DotNetBar.LabelX lblFullName;
        private DevComponents.DotNetBar.LabelX lblPosition;
        private DevComponents.DotNetBar.LabelX lblDesignation;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;

    }
}
