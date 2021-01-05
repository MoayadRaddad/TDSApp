
namespace TSDApp
{
    partial class TSD
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
            this.BankNamePanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblInformation = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.lblGVTitle = new System.Windows.Forms.Label();
            this.btnDeleteScreen = new System.Windows.Forms.Button();
            this.btnEditScreen = new System.Windows.Forms.Button();
            this.gvScreens = new System.Windows.Forms.DataGridView();
            this.btnAddScreen = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.BankNamePanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvScreens)).BeginInit();
            this.SuspendLayout();
            // 
            // BankNamePanel
            // 
            this.BankNamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BankNamePanel.Controls.Add(this.btnCancel);
            this.BankNamePanel.Controls.Add(this.btnEnter);
            this.BankNamePanel.Controls.Add(this.txtBankName);
            this.BankNamePanel.Controls.Add(this.lblInformation);
            this.BankNamePanel.Location = new System.Drawing.Point(135, 110);
            this.BankNamePanel.Name = "BankNamePanel";
            this.BankNamePanel.Size = new System.Drawing.Size(496, 323);
            this.BankNamePanel.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::TSDApp.Properties.Resources.left_arrow;
            this.btnCancel.Location = new System.Drawing.Point(136, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Exit";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btnEnter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.Image = global::TSDApp.Properties.Resources.check;
            this.btnEnter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnter.Location = new System.Drawing.Point(268, 175);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(94, 40);
            this.btnEnter.TabIndex = 3;
            this.btnEnter.Text = "Confirm";
            this.btnEnter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtBankName
            // 
            this.txtBankName.BackColor = System.Drawing.Color.White;
            this.txtBankName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankName.Location = new System.Drawing.Point(136, 126);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(221, 23);
            this.txtBankName.TabIndex = 1;
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(131, 81);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(226, 25);
            this.lblInformation.TabIndex = 0;
            this.lblInformation.Text = "Enter the bank Name :";
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.lblGVTitle);
            this.MainPanel.Controls.Add(this.btnDeleteScreen);
            this.MainPanel.Controls.Add(this.btnEditScreen);
            this.MainPanel.Controls.Add(this.gvScreens);
            this.MainPanel.Controls.Add(this.btnAddScreen);
            this.MainPanel.Controls.Add(this.lblTitle);
            this.MainPanel.Location = new System.Drawing.Point(12, 12);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(728, 539);
            this.MainPanel.TabIndex = 1;
            // 
            // lblGVTitle
            // 
            this.lblGVTitle.AutoSize = true;
            this.lblGVTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGVTitle.Location = new System.Drawing.Point(269, 92);
            this.lblGVTitle.Name = "lblGVTitle";
            this.lblGVTitle.Size = new System.Drawing.Size(85, 25);
            this.lblGVTitle.TabIndex = 5;
            this.lblGVTitle.Text = "Screens";
            // 
            // btnDeleteScreen
            // 
            this.btnDeleteScreen.BackColor = System.Drawing.Color.White;
            this.btnDeleteScreen.BackgroundImage = global::TSDApp.Properties.Resources.trash_1_;
            this.btnDeleteScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDeleteScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteScreen.FlatAppearance.BorderSize = 0;
            this.btnDeleteScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeleteScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeleteScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteScreen.Location = new System.Drawing.Point(608, 88);
            this.btnDeleteScreen.Name = "btnDeleteScreen";
            this.btnDeleteScreen.Size = new System.Drawing.Size(35, 40);
            this.btnDeleteScreen.TabIndex = 4;
            this.btnDeleteScreen.UseVisualStyleBackColor = false;
            this.btnDeleteScreen.Click += new System.EventHandler(this.btnDeleteScreen_Click);
            // 
            // btnEditScreen
            // 
            this.btnEditScreen.BackgroundImage = global::TSDApp.Properties.Resources.draw;
            this.btnEditScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditScreen.FlatAppearance.BorderSize = 0;
            this.btnEditScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEditScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEditScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditScreen.Location = new System.Drawing.Point(649, 88);
            this.btnEditScreen.Name = "btnEditScreen";
            this.btnEditScreen.Size = new System.Drawing.Size(35, 40);
            this.btnEditScreen.TabIndex = 3;
            this.btnEditScreen.UseVisualStyleBackColor = true;
            this.btnEditScreen.Click += new System.EventHandler(this.btnEditScreen_Click);
            // 
            // gvScreens
            // 
            this.gvScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvScreens.BackgroundColor = System.Drawing.Color.White;
            this.gvScreens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvScreens.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gvScreens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvScreens.GridColor = System.Drawing.Color.White;
            this.gvScreens.Location = new System.Drawing.Point(3, 139);
            this.gvScreens.Name = "gvScreens";
            this.gvScreens.Size = new System.Drawing.Size(725, 397);
            this.gvScreens.TabIndex = 2;
            // 
            // btnAddScreen
            // 
            this.btnAddScreen.BackgroundImage = global::TSDApp.Properties.Resources.plus;
            this.btnAddScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddScreen.FlatAppearance.BorderSize = 0;
            this.btnAddScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddScreen.Location = new System.Drawing.Point(690, 88);
            this.btnAddScreen.Name = "btnAddScreen";
            this.btnAddScreen.Size = new System.Drawing.Size(35, 40);
            this.btnAddScreen.TabIndex = 1;
            this.btnAddScreen.UseVisualStyleBackColor = true;
            this.btnAddScreen.Click += new System.EventHandler(this.btnAddScreen_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(269, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(52, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // TSD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(752, 563);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.BankNamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(772, 606);
            this.MinimumSize = new System.Drawing.Size(772, 606);
            this.Name = "TSD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TSDApp";
            this.Load += new System.EventHandler(this.TSD_Load);
            this.BankNamePanel.ResumeLayout(false);
            this.BankNamePanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvScreens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BankNamePanel;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView gvScreens;
        private System.Windows.Forms.Button btnAddScreen;
        private System.Windows.Forms.Button btnDeleteScreen;
        private System.Windows.Forms.Button btnEditScreen;
        private System.Windows.Forms.Label lblGVTitle;
    }
}

