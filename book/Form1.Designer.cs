namespace book
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            add = new Button();
            TenSach = new TextBox();
            TacGia = new TextBox();
            TheLoai = new TextBox();
            NamXuatBan = new TextBox();
            NhaXuatBan = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            ThoiGian = new TextBox();
            SuspendLayout();
            // 
            // add
            // 
            add.Location = new Point(694, 60);
            add.Name = "add";
            add.Size = new Size(94, 29);
            add.TabIndex = 0;
            add.Text = "Thêm";
            add.UseVisualStyleBackColor = true;
            add.Click += add_Click;
            // 
            // TenSach
            // 
            TenSach.Location = new Point(49, 47);
            TenSach.Name = "TenSach";
            TenSach.Size = new Size(125, 27);
            TenSach.TabIndex = 3;
            // 
            // TacGia
            // 
            TacGia.Location = new Point(49, 109);
            TacGia.Name = "TacGia";
            TacGia.Size = new Size(125, 27);
            TacGia.TabIndex = 4;
            // 
            // TheLoai
            // 
            TheLoai.Location = new Point(220, 47);
            TheLoai.Name = "TheLoai";
            TheLoai.Size = new Size(125, 27);
            TheLoai.TabIndex = 5;
            // 
            // NamXuatBan
            // 
            NamXuatBan.Location = new Point(220, 109);
            NamXuatBan.Name = "NamXuatBan";
            NamXuatBan.Size = new Size(125, 27);
            NamXuatBan.TabIndex = 6;
            // 
            // NhaXuatBan
            // 
            NhaXuatBan.Location = new Point(384, 47);
            NhaXuatBan.Name = "NhaXuatBan";
            NhaXuatBan.Size = new Size(125, 27);
            NhaXuatBan.TabIndex = 7;
            NhaXuatBan.TextChanged += textBox5_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 24);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 8;
            label1.Text = "Tên sách";
            label1.Click += label1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(384, 86);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 10;
            label3.Text = "Thời gian";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(384, 24);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 11;
            label4.Text = "Nhà xuất bản";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(220, 86);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 12;
            label5.Text = "Năm xuất bản";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(220, 24);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 13;
            label6.Text = "Thể loại";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(49, 86);
            label7.Name = "label7";
            label7.Size = new Size(55, 20);
            label7.TabIndex = 14;
            label7.Text = "Tác giả";
            label7.Click += label7_Click;
            // 
            // ThoiGian
            // 
            ThoiGian.Location = new Point(384, 109);
            ThoiGian.Name = "ThoiGian";
            ThoiGian.Size = new Size(125, 27);
            ThoiGian.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ThoiGian);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(NhaXuatBan);
            Controls.Add(NamXuatBan);
            Controls.Add(TheLoai);
            Controls.Add(TacGia);
            Controls.Add(TenSach);
            Controls.Add(add);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button add;
        private TextBox TenSach;
        private TextBox TacGia;
        private TextBox TheLoai;
        private TextBox NamXuatBan;
        private TextBox NhaXuatBan;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox ThoiGian;
    }
}
