
namespace SendMail
{
    partial class EmailApp
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
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flpAttachments = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddAttachment = new System.Windows.Forms.Button();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioBtnOutlook = new System.Windows.Forms.RadioButton();
            this.radioBtnMicrosoft = new System.Windows.Forms.RadioButton();
            this.radioBtnGmail = new System.Windows.Forms.RadioButton();
            this.radioButtonGmailSMTP = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(524, 460);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(75, 23);
            this.btnSendEmail.TabIndex = 13;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(158, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.btnAddAttachment);
            this.groupBox2.Controls.Add(this.txtTo);
            this.groupBox2.Controls.Add(this.txtSubject);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(97, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 369);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compose";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flpAttachments);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(93, 265);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 88);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attachments";
            // 
            // flpAttachments
            // 
            this.flpAttachments.Location = new System.Drawing.Point(8, 13);
            this.flpAttachments.Name = "flpAttachments";
            this.flpAttachments.Size = new System.Drawing.Size(422, 70);
            this.flpAttachments.TabIndex = 9;
            // 
            // btnAddAttachment
            // 
            this.btnAddAttachment.Image = global::SendMail.Properties.Resources.attachment;
            this.btnAddAttachment.Location = new System.Drawing.Point(543, 278);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(50, 46);
            this.btnAddAttachment.TabIndex = 8;
            this.btnAddAttachment.UseVisualStyleBackColor = true;
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(92, 19);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(394, 22);
            this.txtTo.TabIndex = 7;
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(93, 48);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(394, 22);
            this.txtSubject.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(58, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "To:";
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(93, 74);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(394, 177);
            this.txtMessage.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(36, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Subject:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Message:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonGmailSMTP);
            this.groupBox3.Controls.Add(this.radioBtnOutlook);
            this.groupBox3.Controls.Add(this.radioBtnMicrosoft);
            this.groupBox3.Controls.Add(this.radioBtnGmail);
            this.groupBox3.Location = new System.Drawing.Point(97, 26);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(609, 36);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Platform";
            // 
            // radioBtnOutlook
            // 
            this.radioBtnOutlook.AutoSize = true;
            this.radioBtnOutlook.Location = new System.Drawing.Point(436, 15);
            this.radioBtnOutlook.Name = "radioBtnOutlook";
            this.radioBtnOutlook.Size = new System.Drawing.Size(62, 17);
            this.radioBtnOutlook.TabIndex = 2;
            this.radioBtnOutlook.TabStop = true;
            this.radioBtnOutlook.Text = "Outlook";
            this.radioBtnOutlook.UseVisualStyleBackColor = true;
            // 
            // radioBtnMicrosoft
            // 
            this.radioBtnMicrosoft.AutoSize = true;
            this.radioBtnMicrosoft.Location = new System.Drawing.Point(252, 13);
            this.radioBtnMicrosoft.Name = "radioBtnMicrosoft";
            this.radioBtnMicrosoft.Size = new System.Drawing.Size(68, 17);
            this.radioBtnMicrosoft.TabIndex = 1;
            this.radioBtnMicrosoft.TabStop = true;
            this.radioBtnMicrosoft.Text = "Microsoft";
            this.radioBtnMicrosoft.UseVisualStyleBackColor = true;
            // 
            // radioBtnGmail
            // 
            this.radioBtnGmail.AutoSize = true;
            this.radioBtnGmail.Location = new System.Drawing.Point(61, 15);
            this.radioBtnGmail.Name = "radioBtnGmail";
            this.radioBtnGmail.Size = new System.Drawing.Size(51, 17);
            this.radioBtnGmail.TabIndex = 0;
            this.radioBtnGmail.TabStop = true;
            this.radioBtnGmail.Text = "Gmail";
            this.radioBtnGmail.UseVisualStyleBackColor = true;
            // 
            // radioButtonGmailSMTP
            // 
            this.radioButtonGmailSMTP.AutoSize = true;
            this.radioButtonGmailSMTP.Location = new System.Drawing.Point(133, 15);
            this.radioButtonGmailSMTP.Name = "radioButtonGmailSMTP";
            this.radioButtonGmailSMTP.Size = new System.Drawing.Size(78, 17);
            this.radioButtonGmailSMTP.TabIndex = 3;
            this.radioButtonGmailSMTP.TabStop = true;
            this.radioButtonGmailSMTP.Text = "Gmail Smtp";
            this.radioButtonGmailSMTP.UseVisualStyleBackColor = true;
            // 
            // EmailApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Name = "EmailApp";
            this.Text = "EmailApp";
            this.Load += new System.EventHandler(this.EmailApp_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddAttachment;
        private System.Windows.Forms.FlowLayoutPanel flpAttachments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioBtnMicrosoft;
        private System.Windows.Forms.RadioButton radioBtnGmail;
        private System.Windows.Forms.RadioButton radioBtnOutlook;
        private System.Windows.Forms.RadioButton radioButtonGmailSMTP;
    }
}