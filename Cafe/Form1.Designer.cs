namespace Cafe
{
    partial class Form1
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
            this.txttmk = new System.Windows.Forms.TextBox();
            this.txttdn = new System.Windows.Forms.TextBox();
            this.lblmk = new System.Windows.Forms.Label();
            this.lbltdn = new System.Windows.Forms.Label();
            this.btndn = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txttmk
            // 
            this.txttmk.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttmk.Location = new System.Drawing.Point(315, 89);
            this.txttmk.Name = "txttmk";
            this.txttmk.Size = new System.Drawing.Size(199, 34);
            this.txttmk.TabIndex = 32;
            // 
            // txttdn
            // 
            this.txttdn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttdn.Location = new System.Drawing.Point(315, 44);
            this.txttdn.Name = "txttdn";
            this.txttdn.Size = new System.Drawing.Size(199, 34);
            this.txttdn.TabIndex = 31;
            // 
            // lblmk
            // 
            this.lblmk.AutoSize = true;
            this.lblmk.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmk.Location = new System.Drawing.Point(173, 94);
            this.lblmk.Name = "lblmk";
            this.lblmk.Size = new System.Drawing.Size(124, 29);
            this.lblmk.TabIndex = 30;
            this.lblmk.Text = "Mật khẩu:";
            // 
            // lbltdn
            // 
            this.lbltdn.AutoSize = true;
            this.lbltdn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltdn.Location = new System.Drawing.Point(102, 49);
            this.lbltdn.Name = "lbltdn";
            this.lbltdn.Size = new System.Drawing.Size(195, 29);
            this.lbltdn.TabIndex = 29;
            this.lbltdn.Text = "Tên đăng nhập:";
            // 
            // btndn
            // 
            this.btndn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btndn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btndn.Location = new System.Drawing.Point(128, 143);
            this.btndn.Name = "btndn";
            this.btndn.Size = new System.Drawing.Size(217, 45);
            this.btndn.TabIndex = 27;
            this.btndn.Text = "Đăng nhập:";
            this.btndn.UseVisualStyleBackColor = false;
            this.btndn.Click += new System.EventHandler(this.btndn_Click);
            // 
            // btnexit
            // 
            this.btnexit.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnexit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnexit.Location = new System.Drawing.Point(370, 143);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(217, 45);
            this.btnexit.TabIndex = 33;
            this.btnexit.Text = "EXIT";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(685, 289);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.txttmk);
            this.Controls.Add(this.txttdn);
            this.Controls.Add(this.lblmk);
            this.Controls.Add(this.lbltdn);
            this.Controls.Add(this.btndn);
            this.Name = "Form1";
            this.Text = "Đăng Nhập";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txttmk;
        private System.Windows.Forms.TextBox txttdn;
        private System.Windows.Forms.Label lblmk;
        private System.Windows.Forms.Label lbltdn;
        private System.Windows.Forms.Button btndn;
        private System.Windows.Forms.Button btnexit;
    }
}

