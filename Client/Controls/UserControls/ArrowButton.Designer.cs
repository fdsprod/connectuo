namespace ConnectUO.Controls
{
    partial class ArrowButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArrowButton));
            this.pbArrow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // pbArrow
            // 
            this.pbArrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbArrow.BackgroundImage")));
            this.pbArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbArrow.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbArrow.Location = new System.Drawing.Point(0, 0);
            this.pbArrow.Name = "pbArrow";
            this.pbArrow.Size = new System.Drawing.Size(4, 13);
            this.pbArrow.TabIndex = 12;
            this.pbArrow.TabStop = false;
            // 
            // ArrowButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbArrow);
            this.Name = "ArrowButton";
            this.Size = new System.Drawing.Size(125, 13);
            ((System.ComponentModel.ISupportInitialize)(this.pbArrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbArrow;
    }
}
