
namespace TSDApp.Fomrs
{
    partial class AddEditScreen
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ddlActive = new System.Windows.Forms.ComboBox();
            this.gvButtons = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDeleteButton = new System.Windows.Forms.Button();
            this.btnEditButton = new System.Windows.Forms.Button();
            this.btnAddButton = new System.Windows.Forms.Button();
            this.lblGVTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvButtons)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(306, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Screen";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(13, 49);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 23);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name *";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActive.Location = new System.Drawing.Point(12, 79);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(75, 23);
            this.lblActive.TabIndex = 2;
            this.lblActive.Text = "Active *";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(132, 53);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 23);
            this.txtName.TabIndex = 3;
            // 
            // ddlActive
            // 
            this.ddlActive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlActive.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlActive.FormattingEnabled = true;
            this.ddlActive.Items.AddRange(new object[] {
            "- Select -",
            "True",
            "False"});
            this.ddlActive.Location = new System.Drawing.Point(132, 83);
            this.ddlActive.Name = "ddlActive";
            this.ddlActive.Size = new System.Drawing.Size(191, 24);
            this.ddlActive.TabIndex = 4;
            // 
            // gvButtons
            // 
            this.gvButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvButtons.BackgroundColor = System.Drawing.Color.White;
            this.gvButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvButtons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gvButtons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvButtons.GridColor = System.Drawing.Color.White;
            this.gvButtons.Location = new System.Drawing.Point(15, 183);
            this.gvButtons.Name = "gvButtons";
            this.gvButtons.Size = new System.Drawing.Size(725, 340);
            this.gvButtons.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::TSDApp.Properties.Resources.left_arrow;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(15, 525);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Back";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::TSDApp.Properties.Resources.check;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(631, 525);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Add";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteButton
            // 
            this.btnDeleteButton.BackgroundImage = global::TSDApp.Properties.Resources.trash_1_;
            this.btnDeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDeleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteButton.FlatAppearance.BorderSize = 0;
            this.btnDeleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteButton.Location = new System.Drawing.Point(623, 137);
            this.btnDeleteButton.Name = "btnDeleteButton";
            this.btnDeleteButton.Size = new System.Drawing.Size(35, 40);
            this.btnDeleteButton.TabIndex = 7;
            this.btnDeleteButton.UseVisualStyleBackColor = true;
            this.btnDeleteButton.Click += new System.EventHandler(this.btnDeleteButton_Click);
            // 
            // btnEditButton
            // 
            this.btnEditButton.BackgroundImage = global::TSDApp.Properties.Resources.draw;
            this.btnEditButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditButton.FlatAppearance.BorderSize = 0;
            this.btnEditButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEditButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditButton.Location = new System.Drawing.Point(664, 137);
            this.btnEditButton.Name = "btnEditButton";
            this.btnEditButton.Size = new System.Drawing.Size(35, 40);
            this.btnEditButton.TabIndex = 6;
            this.btnEditButton.UseVisualStyleBackColor = true;
            this.btnEditButton.Click += new System.EventHandler(this.btnEditButton_Click);
            // 
            // btnAddButton
            // 
            this.btnAddButton.BackgroundImage = global::TSDApp.Properties.Resources.plus;
            this.btnAddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddButton.FlatAppearance.BorderSize = 0;
            this.btnAddButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddButton.Location = new System.Drawing.Point(705, 137);
            this.btnAddButton.Name = "btnAddButton";
            this.btnAddButton.Size = new System.Drawing.Size(35, 40);
            this.btnAddButton.TabIndex = 5;
            this.btnAddButton.UseVisualStyleBackColor = true;
            this.btnAddButton.Click += new System.EventHandler(this.btnAddButton_Click);
            // 
            // lblGVTitle
            // 
            this.lblGVTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGVTitle.AutoSize = true;
            this.lblGVTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGVTitle.Location = new System.Drawing.Point(306, 141);
            this.lblGVTitle.Name = "lblGVTitle";
            this.lblGVTitle.Size = new System.Drawing.Size(83, 25);
            this.lblGVTitle.TabIndex = 11;
            this.lblGVTitle.Text = "Buttons";
            // 
            // AddEditScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(752, 567);
            this.Controls.Add(this.lblGVTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gvButtons);
            this.Controls.Add(this.btnDeleteButton);
            this.Controls.Add(this.btnEditButton);
            this.Controls.Add(this.btnAddButton);
            this.Controls.Add(this.ddlActive);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(772, 610);
            this.MinimumSize = new System.Drawing.Size(772, 610);
            this.Name = "AddEditScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Screen";
            this.Load += new System.EventHandler(this.AddEditScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvButtons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox ddlActive;
        private System.Windows.Forms.Button btnDeleteButton;
        private System.Windows.Forms.Button btnEditButton;
        private System.Windows.Forms.Button btnAddButton;
        private System.Windows.Forms.DataGridView gvButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblGVTitle;
    }
}