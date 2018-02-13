namespace IDEX
{
    partial class IDEX
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
            this.components = new System.ComponentModel.Container();
            this.ContentStatusdataGridView = new System.Windows.Forms.DataGridView();
            this.PendingServiceContentStatus = new System.Windows.Forms.Label();
            this.DisplayContentStatTimer = new System.Windows.Forms.Timer(this.components);
            this.PendingCheckAlertTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ContentStatusdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentStatusdataGridView
            // 
            this.ContentStatusdataGridView.AllowUserToAddRows = false;
            this.ContentStatusdataGridView.AllowUserToDeleteRows = false;
            this.ContentStatusdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ContentStatusdataGridView.Location = new System.Drawing.Point(12, 57);
            this.ContentStatusdataGridView.Name = "ContentStatusdataGridView";
            this.ContentStatusdataGridView.ReadOnly = true;
            this.ContentStatusdataGridView.Size = new System.Drawing.Size(616, 150);
            this.ContentStatusdataGridView.TabIndex = 0;
            // 
            // PendingServiceContentStatus
            // 
            this.PendingServiceContentStatus.AutoSize = true;
            this.PendingServiceContentStatus.Location = new System.Drawing.Point(13, 22);
            this.PendingServiceContentStatus.Name = "PendingServiceContentStatus";
            this.PendingServiceContentStatus.Size = new System.Drawing.Size(137, 13);
            this.PendingServiceContentStatus.TabIndex = 1;
            this.PendingServiceContentStatus.Text = "Pending Service Content = ";
            this.PendingServiceContentStatus.Click += new System.EventHandler(this.PendingServiceContentStatus_Click);
            // 
            // IDEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 313);
            this.Controls.Add(this.PendingServiceContentStatus);
            this.Controls.Add(this.ContentStatusdataGridView);
            this.Name = "IDEX";
            this.Text = "IDEX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IDEX_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IDEX_FormClosed);
            this.Load += new System.EventHandler(this.IDEX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ContentStatusdataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ContentStatusdataGridView;
        private System.Windows.Forms.Label PendingServiceContentStatus;
        private System.Windows.Forms.Timer DisplayContentStatTimer;
        private System.Windows.Forms.Timer PendingCheckAlertTimer;
    }
}

