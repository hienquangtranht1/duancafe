namespace Cafe
{
    partial class Menu
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
            this.btnranh = new System.Windows.Forms.Button();
            this.btnban = new System.Windows.Forms.Button();
            this.cmbloaisp = new System.Windows.Forms.ComboBox();
            this.lblkhoa = new System.Windows.Forms.Label();
            this.dgvsv = new System.Windows.Forms.DataGridView();
            this.dgvmssp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtensp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbltensp = new System.Windows.Forms.Label();
            this.txttensp = new System.Windows.Forms.TextBox();
            this.txtsl = new System.Windows.Forms.TextBox();
            this.lblsl = new System.Windows.Forms.Label();
            this.txtsb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtgt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txttn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btntt = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvgiatien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvsl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnsua = new System.Windows.Forms.Button();
            this.btnthem = new System.Windows.Forms.Button();
            this.btnxoa = new System.Windows.Forms.Button();
            this.txttt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtkhuyenmai = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.TenBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trangthaib = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnranh
            // 
            this.btnranh.BackColor = System.Drawing.Color.ForestGreen;
            this.btnranh.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnranh.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnranh.Location = new System.Drawing.Point(53, 385);
            this.btnranh.Name = "btnranh";
            this.btnranh.Size = new System.Drawing.Size(139, 50);
            this.btnranh.TabIndex = 14;
            this.btnranh.Text = "Rãnh";
            this.btnranh.UseVisualStyleBackColor = false;
            // 
            // btnban
            // 
            this.btnban.BackColor = System.Drawing.Color.Red;
            this.btnban.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnban.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnban.Location = new System.Drawing.Point(198, 385);
            this.btnban.Name = "btnban";
            this.btnban.Size = new System.Drawing.Size(139, 50);
            this.btnban.TabIndex = 15;
            this.btnban.Text = "Bận";
            this.btnban.UseVisualStyleBackColor = false;
            // 
            // cmbloaisp
            // 
            this.cmbloaisp.FormattingEnabled = true;
            this.cmbloaisp.Location = new System.Drawing.Point(521, 76);
            this.cmbloaisp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbloaisp.Name = "cmbloaisp";
            this.cmbloaisp.Size = new System.Drawing.Size(200, 24);
            this.cmbloaisp.TabIndex = 28;
            // 
            // lblkhoa
            // 
            this.lblkhoa.AutoSize = true;
            this.lblkhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblkhoa.Location = new System.Drawing.Point(429, 78);
            this.lblkhoa.Name = "lblkhoa";
            this.lblkhoa.Size = new System.Drawing.Size(86, 22);
            this.lblkhoa.TabIndex = 27;
            this.lblkhoa.Text = "Loại SP:";
            // 
            // dgvsv
            // 
            this.dgvsv.AllowUserToAddRows = false;
            this.dgvsv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvsv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvmssp,
            this.dgvtensp});
            this.dgvsv.Location = new System.Drawing.Point(433, 126);
            this.dgvsv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvsv.Name = "dgvsv";
            this.dgvsv.RowHeadersWidth = 51;
            this.dgvsv.RowTemplate.Height = 24;
            this.dgvsv.Size = new System.Drawing.Size(288, 123);
            this.dgvsv.TabIndex = 29;
            // 
            // dgvmssp
            // 
            this.dgvmssp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvmssp.HeaderText = "MSSP";
            this.dgvmssp.MinimumWidth = 6;
            this.dgvmssp.Name = "dgvmssp";
            // 
            // dgvtensp
            // 
            this.dgvtensp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvtensp.HeaderText = "Tên SP";
            this.dgvtensp.MinimumWidth = 6;
            this.dgvtensp.Name = "dgvtensp";
            // 
            // lbltensp
            // 
            this.lbltensp.AutoSize = true;
            this.lbltensp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltensp.Location = new System.Drawing.Point(768, 128);
            this.lbltensp.Name = "lbltensp";
            this.lbltensp.Size = new System.Drawing.Size(76, 20);
            this.lbltensp.TabIndex = 30;
            this.lbltensp.Text = "Tên SP:";
            // 
            // txttensp
            // 
            this.txttensp.Enabled = false;
            this.txttensp.Location = new System.Drawing.Point(860, 126);
            this.txttensp.Name = "txttensp";
            this.txttensp.Size = new System.Drawing.Size(152, 22);
            this.txttensp.TabIndex = 31;
            // 
            // txtsl
            // 
            this.txtsl.Location = new System.Drawing.Point(860, 257);
            this.txtsl.Name = "txtsl";
            this.txtsl.Size = new System.Drawing.Size(152, 22);
            this.txtsl.TabIndex = 33;
            // 
            // lblsl
            // 
            this.lblsl.AutoSize = true;
            this.lblsl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsl.Location = new System.Drawing.Point(756, 257);
            this.lblsl.Name = "lblsl";
            this.lblsl.Size = new System.Drawing.Size(88, 20);
            this.lblsl.TabIndex = 32;
            this.lblsl.Text = "Số lượng:";
            // 
            // txtsb
            // 
            this.txtsb.Location = new System.Drawing.Point(860, 211);
            this.txtsb.Name = "txtsb";
            this.txtsb.Size = new System.Drawing.Size(152, 22);
            this.txtsb.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(771, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "Số bàn:";
            // 
            // txtgt
            // 
            this.txtgt.Enabled = false;
            this.txtgt.Location = new System.Drawing.Point(860, 169);
            this.txtgt.Name = "txtgt";
            this.txtgt.Size = new System.Drawing.Size(152, 22);
            this.txtgt.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(763, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "Giá tiền:";
            // 
            // txttn
            // 
            this.txttn.Enabled = false;
            this.txttn.Location = new System.Drawing.Point(860, 339);
            this.txttn.Name = "txttn";
            this.txttn.Size = new System.Drawing.Size(152, 22);
            this.txttn.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(751, 339);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Tổng tiền:";
            // 
            // btntt
            // 
            this.btntt.BackColor = System.Drawing.SystemColors.Highlight;
            this.btntt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btntt.Location = new System.Drawing.Point(1032, 342);
            this.btntt.Name = "btntt";
            this.btntt.Size = new System.Drawing.Size(182, 45);
            this.btntt.TabIndex = 40;
            this.btntt.Text = "Thanh toán:";
            this.btntt.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dgvgiatien,
            this.dgvsl,
            this.dgvtt});
            this.dataGridView1.Location = new System.Drawing.Point(1032, 126);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(566, 203);
            this.dataGridView1.TabIndex = 41;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "MSSP";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên SP";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dgvgiatien
            // 
            this.dgvgiatien.HeaderText = "Giá tiền";
            this.dgvgiatien.MinimumWidth = 6;
            this.dgvgiatien.Name = "dgvgiatien";
            // 
            // dgvsl
            // 
            this.dgvsl.HeaderText = "Số lượng";
            this.dgvsl.MinimumWidth = 6;
            this.dgvsl.Name = "dgvsl";
            // 
            // dgvtt
            // 
            this.dgvtt.HeaderText = "Tổng tiền";
            this.dgvtt.MinimumWidth = 6;
            this.dgvtt.Name = "dgvtt";
            // 
            // btnsua
            // 
            this.btnsua.Location = new System.Drawing.Point(841, 385);
            this.btnsua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(75, 23);
            this.btnsua.TabIndex = 43;
            this.btnsua.Text = "Sửa";
            this.btnsua.UseVisualStyleBackColor = true;
            // 
            // btnthem
            // 
            this.btnthem.Location = new System.Drawing.Point(745, 385);
            this.btnthem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(75, 23);
            this.btnthem.TabIndex = 42;
            this.btnthem.Text = "Thêm";
            this.btnthem.UseVisualStyleBackColor = true;
            // 
            // btnxoa
            // 
            this.btnxoa.Location = new System.Drawing.Point(937, 385);
            this.btnxoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(75, 23);
            this.btnxoa.TabIndex = 44;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.UseVisualStyleBackColor = true;
            // 
            // txttt
            // 
            this.txttt.Enabled = false;
            this.txttt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttt.Location = new System.Drawing.Point(1220, 349);
            this.txttt.Name = "txttt";
            this.txttt.Size = new System.Drawing.Size(152, 30);
            this.txttt.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(1025, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 38);
            this.label1.TabIndex = 26;
            this.label1.Text = "Danh mục chọn món";
            // 
            // txtkhuyenmai
            // 
            this.txtkhuyenmai.Enabled = false;
            this.txtkhuyenmai.Location = new System.Drawing.Point(860, 297);
            this.txtkhuyenmai.Name = "txtkhuyenmai";
            this.txtkhuyenmai.Size = new System.Drawing.Size(152, 22);
            this.txtkhuyenmai.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(732, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 46;
            this.label5.Text = "Khuyến mãi:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenBan,
            this.Trangthaib});
            this.dataGridView2.Location = new System.Drawing.Point(53, 126);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(284, 253);
            this.dataGridView2.TabIndex = 48;
            // 
            // TenBan
            // 
            this.TenBan.HeaderText = "Tên Bàn";
            this.TenBan.MinimumWidth = 6;
            this.TenBan.Name = "TenBan";
            this.TenBan.ReadOnly = true;
            this.TenBan.Width = 125;
            // 
            // Trangthaib
            // 
            this.Trangthaib.HeaderText = "Trạng thái";
            this.Trangthaib.MinimumWidth = 6;
            this.Trangthaib.Name = "Trangthaib";
            this.Trangthaib.ReadOnly = true;
            this.Trangthaib.Width = 125;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 22);
            this.label7.TabIndex = 50;
            this.label7.Text = "Thông tin chọn bàn:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1906, 28);
            this.menuStrip1.TabIndex = 51;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýToolStripMenuItem,
            this.thoátToolStripMenuItem});
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            this.quảnLýToolStripMenuItem.Click += new System.EventHandler(this.quảnLýToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(433, 254);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 154);
            this.pictureBox1.TabIndex = 52;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1906, 555);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.txtkhuyenmai);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txttt);
            this.Controls.Add(this.btnsua);
            this.Controls.Add(this.btnthem);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btntt);
            this.Controls.Add(this.txttn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtgt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtsb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtsl);
            this.Controls.Add(this.lblsl);
            this.Controls.Add(this.txttensp);
            this.Controls.Add(this.lbltensp);
            this.Controls.Add(this.dgvsv);
            this.Controls.Add(this.cmbloaisp);
            this.Controls.Add(this.lblkhoa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnban);
            this.Controls.Add(this.btnranh);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Menu";
            this.Text = "Menu_Nhân viên";
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnranh;
        private System.Windows.Forms.Button btnban;
        private System.Windows.Forms.ComboBox cmbloaisp;
        private System.Windows.Forms.Label lblkhoa;
        private System.Windows.Forms.DataGridView dgvsv;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvmssp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtensp;
        private System.Windows.Forms.Label lbltensp;
        private System.Windows.Forms.TextBox txttensp;
        private System.Windows.Forms.TextBox txtsl;
        private System.Windows.Forms.Label lblsl;
        private System.Windows.Forms.TextBox txtsb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtgt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btntt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnsua;
        private System.Windows.Forms.Button btnthem;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.TextBox txttt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvgiatien;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvsl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtt;
        private System.Windows.Forms.TextBox txtkhuyenmai;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trangthaib;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}