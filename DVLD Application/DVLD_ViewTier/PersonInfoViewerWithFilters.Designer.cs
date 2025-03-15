using System;

namespace DVLD_ViewTier
{
    partial class PersonInfoViewerWithFilters
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilters = new System.Windows.Forms.ComboBox();
            this.tbFilterValue = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.personInfoViewer1 = new DVLD_ViewTier.PersonInfoViewer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(26, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter By :";
            // 
            // cmbFilters
            // 
            this.cmbFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilters.FormattingEnabled = true;
            this.cmbFilters.Items.AddRange(new object[] {
            "National Number",
            "Person ID"});
            this.cmbFilters.Location = new System.Drawing.Point(96, 13);
            this.cmbFilters.Name = "cmbFilters";
            this.cmbFilters.Size = new System.Drawing.Size(150, 21);
            this.cmbFilters.TabIndex = 2;
            // 
            // tbFilterValue
            // 
            this.tbFilterValue.Location = new System.Drawing.Point(254, 14);
            this.tbFilterValue.Name = "tbFilterValue";
            this.tbFilterValue.Size = new System.Drawing.Size(150, 20);
            this.tbFilterValue.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(411, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(456, 6);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(35, 35);
            this.btnAddPerson.TabIndex = 5;
            this.btnAddPerson.Text = "button1";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // personInfoViewer1
            // 
            this.personInfoViewer1.Address = "[????]";
            this.personInfoViewer1.Created_by = null;
            this.personInfoViewer1.DateOfBirth = "[????]";
            this.personInfoViewer1.Email = "[????]";
            this.personInfoViewer1.FirstName = null;
            this.personInfoViewer1.Gender = "[????]";
            this.personInfoViewer1.Id = "";
            this.personInfoViewer1.ImagePath = null;
            this.personInfoViewer1.LastName = null;
            this.personInfoViewer1.Location = new System.Drawing.Point(4, 41);
            this.personInfoViewer1.Name = "personInfoViewer1";
            this.personInfoViewer1.NationalityCountry = "[????]";
            this.personInfoViewer1.NationalNumber = "[????]";
            this.personInfoViewer1.Phone = "[????]";
            this.personInfoViewer1.SecondName = null;
            this.personInfoViewer1.Size = new System.Drawing.Size(529, 182);
            this.personInfoViewer1.TabIndex = 0;
            this.personInfoViewer1.ThirdName = null;
            // 
            // PersonInfoViewerWithFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbFilterValue);
            this.Controls.Add(this.cmbFilters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.personInfoViewer1);
            this.Name = "PersonInfoViewerWithFilters";
            this.Size = new System.Drawing.Size(538, 228);
            this.Load += new System.EventHandler(this.PersonInfoViewerWithFilters_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PersonInfoViewer personInfoViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilters;
        private System.Windows.Forms.TextBox tbFilterValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAddPerson;
    }
}
