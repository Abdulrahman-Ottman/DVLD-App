namespace DVLD_ViewTier.Licenses.LocalLicense
{
    partial class AddLocalLicensApplication
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
            this.personInfoViewerWithFilters1 = new DVLD_ViewTier.PersonInfoViewerWithFilters();
            this.SuspendLayout();
            // 
            // personInfoViewerWithFilters1
            // 
            this.personInfoViewerWithFilters1.Location = new System.Drawing.Point(3, 113);
            this.personInfoViewerWithFilters1.Name = "personInfoViewerWithFilters1";
            this.personInfoViewerWithFilters1.Size = new System.Drawing.Size(538, 228);
            this.personInfoViewerWithFilters1.TabIndex = 0;
            // 
            // AddLocalLicensApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 343);
            this.Controls.Add(this.personInfoViewerWithFilters1);
            this.Name = "AddLocalLicensApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Local License Application";
            this.ResumeLayout(false);

        }

        #endregion

        private PersonInfoViewerWithFilters personInfoViewerWithFilters1;
    }
}