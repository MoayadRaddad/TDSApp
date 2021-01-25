
namespace TSDApp.Forms
{
    partial class AddEditButton
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
            this.ddlIssueTicket = new System.Windows.Forms.ComboBox();
            this.txtENName = new System.Windows.Forms.TextBox();
            this.lblIssueTicket = new System.Windows.Forms.Label();
            this.lblENName = new System.Windows.Forms.Label();
            this.txtARName = new System.Windows.Forms.TextBox();
            this.lblARName = new System.Windows.Forms.Label();
            this.txtMessageAR = new System.Windows.Forms.TextBox();
            this.lblMessageAR = new System.Windows.Forms.Label();
            this.lblButtonType = new System.Windows.Forms.Label();
            this.txtMessageEN = new System.Windows.Forms.TextBox();
            this.lblMessageEN = new System.Windows.Forms.Label();
            this.rbIssueTicket = new System.Windows.Forms.RadioButton();
            this.rbShowMessage = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(226, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(118, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Button";
            // 
            // ddlIssueTicket
            // 
            this.ddlIssueTicket.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.ddlIssueTicket.FormattingEnabled = true;
            this.ddlIssueTicket.Location = new System.Drawing.Point(261, 195);
            this.ddlIssueTicket.Name = "ddlIssueTicket";
            this.ddlIssueTicket.Size = new System.Drawing.Size(217, 24);
            this.ddlIssueTicket.TabIndex = 8;
            // 
            // txtENName
            // 
            this.txtENName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtENName.Location = new System.Drawing.Point(261, 86);
            this.txtENName.Name = "txtENName";
            this.txtENName.Size = new System.Drawing.Size(217, 23);
            this.txtENName.TabIndex = 7;
            // 
            // lblIssueTicket
            // 
            this.lblIssueTicket.AutoSize = true;
            this.lblIssueTicket.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssueTicket.Location = new System.Drawing.Point(106, 195);
            this.lblIssueTicket.Name = "lblIssueTicket";
            this.lblIssueTicket.Size = new System.Drawing.Size(125, 23);
            this.lblIssueTicket.TabIndex = 6;
            this.lblIssueTicket.Text = "Issue Ticket *";
            // 
            // lblENName
            // 
            this.lblENName.AutoSize = true;
            this.lblENName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblENName.Location = new System.Drawing.Point(107, 86);
            this.lblENName.Name = "lblENName";
            this.lblENName.Size = new System.Drawing.Size(105, 23);
            this.lblENName.TabIndex = 5;
            this.lblENName.Text = "English Name *";
            // 
            // txtARName
            // 
            this.txtARName.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtARName.Location = new System.Drawing.Point(261, 121);
            this.txtARName.Name = "txtARName";
            this.txtARName.Size = new System.Drawing.Size(217, 23);
            this.txtARName.TabIndex = 10;
            // 
            // lblARName
            // 
            this.lblARName.AutoSize = true;
            this.lblARName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblARName.Location = new System.Drawing.Point(106, 116);
            this.lblARName.Name = "lblARName";
            this.lblARName.Size = new System.Drawing.Size(104, 23);
            this.lblARName.TabIndex = 9;
            this.lblARName.Text = "Arabic Name *";
            // 
            // txtMessageAR
            // 
            this.txtMessageAR.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtMessageAR.Location = new System.Drawing.Point(260, 234);
            this.txtMessageAR.Name = "txtMessageAR";
            this.txtMessageAR.Size = new System.Drawing.Size(217, 23);
            this.txtMessageAR.TabIndex = 14;
            // 
            // lblMessageAR
            // 
            this.lblMessageAR.AutoSize = true;
            this.lblMessageAR.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageAR.Location = new System.Drawing.Point(105, 229);
            this.lblMessageAR.Name = "lblMessageAR";
            this.lblMessageAR.Size = new System.Drawing.Size(127, 23);
            this.lblMessageAR.TabIndex = 13;
            this.lblMessageAR.Text = "Message Arabic *";
            // 
            // lblButtonType
            // 
            this.lblButtonType.AutoSize = true;
            this.lblButtonType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblButtonType.Location = new System.Drawing.Point(104, 153);
            this.lblButtonType.Name = "lblButtonType";
            this.lblButtonType.Size = new System.Drawing.Size(128, 23);
            this.lblButtonType.TabIndex = 15;
            this.lblButtonType.Text = "Button Type *";
            // 
            // txtMessageEN
            // 
            this.txtMessageEN.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtMessageEN.Location = new System.Drawing.Point(260, 195);
            this.txtMessageEN.Name = "txtMessageEN";
            this.txtMessageEN.Size = new System.Drawing.Size(217, 23);
            this.txtMessageEN.TabIndex = 17;
            // 
            // lblMessageEN
            // 
            this.lblMessageEN.AutoSize = true;
            this.lblMessageEN.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageEN.Location = new System.Drawing.Point(105, 191);
            this.lblMessageEN.Name = "lblMessageEN";
            this.lblMessageEN.Size = new System.Drawing.Size(128, 23);
            this.lblMessageEN.TabIndex = 16;
            this.lblMessageEN.Text = "Message English *";
            // 
            // rbIssueTicket
            // 
            this.rbIssueTicket.AutoSize = true;
            this.rbIssueTicket.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.rbIssueTicket.Location = new System.Drawing.Point(261, 158);
            this.rbIssueTicket.Name = "rbIssueTicket";
            this.rbIssueTicket.Size = new System.Drawing.Size(94, 20);
            this.rbIssueTicket.TabIndex = 18;
            this.rbIssueTicket.TabStop = true;
            this.rbIssueTicket.Text = "Issue Ticket";
            this.rbIssueTicket.UseVisualStyleBackColor = true;
            this.rbIssueTicket.CheckedChanged += new System.EventHandler(this.rbIssueTicket_CheckedChanged);
            // 
            // rbShowMessage
            // 
            this.rbShowMessage.AutoSize = true;
            this.rbShowMessage.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.rbShowMessage.Location = new System.Drawing.Point(359, 158);
            this.rbShowMessage.Name = "rbShowMessage";
            this.rbShowMessage.Size = new System.Drawing.Size(112, 20);
            this.rbShowMessage.TabIndex = 19;
            this.rbShowMessage.TabStop = true;
            this.rbShowMessage.Text = "Show Message";
            this.rbShowMessage.UseVisualStyleBackColor = true;
            this.rbShowMessage.CheckedChanged += new System.EventHandler(this.rbShowMessage_CheckedChanged);
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
            this.btnCancel.Location = new System.Drawing.Point(75, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 40);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Back";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::TSDApp.Properties.Resources.check;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(443, 296);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 40);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Add";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddEditButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(573, 367);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rbShowMessage);
            this.Controls.Add(this.rbIssueTicket);
            this.Controls.Add(this.txtMessageEN);
            this.Controls.Add(this.lblMessageEN);
            this.Controls.Add(this.lblButtonType);
            this.Controls.Add(this.txtMessageAR);
            this.Controls.Add(this.lblMessageAR);
            this.Controls.Add(this.txtARName);
            this.Controls.Add(this.lblARName);
            this.Controls.Add(this.ddlIssueTicket);
            this.Controls.Add(this.txtENName);
            this.Controls.Add(this.lblIssueTicket);
            this.Controls.Add(this.lblENName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(593, 410);
            this.MinimumSize = new System.Drawing.Size(593, 410);
            this.Name = "AddEditButton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Button";
            this.Load += new System.EventHandler(this.AddEditButton_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox ddlIssueTicket;
        private System.Windows.Forms.TextBox txtENName;
        private System.Windows.Forms.Label lblIssueTicket;
        private System.Windows.Forms.Label lblENName;
        private System.Windows.Forms.TextBox txtARName;
        private System.Windows.Forms.Label lblARName;
        private System.Windows.Forms.TextBox txtMessageAR;
        private System.Windows.Forms.Label lblMessageAR;
        private System.Windows.Forms.Label lblButtonType;
        private System.Windows.Forms.TextBox txtMessageEN;
        private System.Windows.Forms.Label lblMessageEN;
        private System.Windows.Forms.RadioButton rbIssueTicket;
        private System.Windows.Forms.RadioButton rbShowMessage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}