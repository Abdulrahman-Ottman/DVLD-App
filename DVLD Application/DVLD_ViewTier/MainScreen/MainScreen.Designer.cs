﻿namespace DVLD_ViewTier.MainScreen
{
    partial class MainScreen
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmApplications,
            this.tsmPeople,
            this.tsmDrivers,
            this.tsmUsers,
            this.tsmAccountSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 6, 0, 6);
            this.menuStrip1.Size = new System.Drawing.Size(1184, 48);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmApplications
            // 
            this.tsmApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.tstToolStripMenuItem});
            this.tsmApplications.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsmApplications.Image = global::DVLD_ViewTier.Properties.Resources.form__1_;
            this.tsmApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmApplications.Name = "tsmApplications";
            this.tsmApplications.Size = new System.Drawing.Size(136, 36);
            this.tsmApplications.Text = "Applications";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.testToolStripMenuItem.Text = "test";
            // 
            // tstToolStripMenuItem
            // 
            this.tstToolStripMenuItem.Name = "tstToolStripMenuItem";
            this.tstToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.tstToolStripMenuItem.Text = "tst";
            // 
            // tsmPeople
            // 
            this.tsmPeople.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsmPeople.Image = global::DVLD_ViewTier.Properties.Resources.group__2_;
            this.tsmPeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmPeople.Name = "tsmPeople";
            this.tsmPeople.Size = new System.Drawing.Size(100, 36);
            this.tsmPeople.Text = "People";
            this.tsmPeople.Click += new System.EventHandler(this.tsmPeople_Click);
            // 
            // tsmDrivers
            // 
            this.tsmDrivers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsmDrivers.Image = global::DVLD_ViewTier.Properties.Resources.driver;
            this.tsmDrivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDrivers.Name = "tsmDrivers";
            this.tsmDrivers.Size = new System.Drawing.Size(93, 36);
            this.tsmDrivers.Text = "Drivers";
            // 
            // tsmUsers
            // 
            this.tsmUsers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsmUsers.Image = global::DVLD_ViewTier.Properties.Resources.user;
            this.tsmUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmUsers.Name = "tsmUsers";
            this.tsmUsers.Size = new System.Drawing.Size(89, 36);
            this.tsmUsers.Text = "Users";
            // 
            // tsmAccountSettings
            // 
            this.tsmAccountSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsmAccountSettings.Image = global::DVLD_ViewTier.Properties.Resources.settings;
            this.tsmAccountSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmAccountSettings.Name = "tsmAccountSettings";
            this.tsmAccountSettings.Size = new System.Drawing.Size(164, 36);
            this.tsmAccountSettings.Text = "Account Settings";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmApplications;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmPeople;
        private System.Windows.Forms.ToolStripMenuItem tsmDrivers;
        private System.Windows.Forms.ToolStripMenuItem tsmUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmAccountSettings;
    }
}