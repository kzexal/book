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
            btnadd = new Button();
            textboxTenSach = new TextBox();
            textboxTacGia = new TextBox();
            textboxTheLoai = new TextBox();
            textboxNamXuatBan = new TextBox();
            textboxNhaXuatBan = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            soluong = new Label();
            textboxSoLuong = new TextBox();
            pickThoiGianNhap = new DateTimePicker();
            SuspendLayout();
            // 
            // btnadd
            // 
            btnadd.Location = new Point(367, 409);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(94, 29);
            btnadd.TabIndex = 0;
            btnadd.Text = "Thêm";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // textboxTenSach
            // 
            textboxTenSach.Location = new Point(25, 51);
            textboxTenSach.Name = "textboxTenSach";
            textboxTenSach.Size = new Size(125, 27);
            textboxTenSach.TabIndex = 3;
            // 
            // textboxTacGia
            // 
            textboxTacGia.Location = new Point(173, 51);
            textboxTacGia.Name = "textboxTacGia";
            textboxTacGia.Size = new Size(125, 27);
            textboxTacGia.TabIndex = 4;
            // 
            // textboxTheLoai
            // 
            textboxTheLoai.Location = new Point(313, 51);
            textboxTheLoai.Name = "textboxTheLoai";
            textboxTheLoai.Size = new Size(125, 27);
            textboxTheLoai.TabIndex = 5;
            // 
            // textboxNamXuatBan
            // 
            textboxNamXuatBan.Location = new Point(454, 51);
            textboxNamXuatBan.Name = "textboxNamXuatBan";
            textboxNamXuatBan.Size = new Size(125, 27);
            textboxNamXuatBan.TabIndex = 6;
            // 
            // textboxNhaXuatBan
            // 
            textboxNhaXuatBan.Location = new Point(25, 124);
            textboxNhaXuatBan.Name = "textboxNhaXuatBan";
            textboxNhaXuatBan.Size = new Size(125, 27);
            textboxNhaXuatBan.TabIndex = 7;
            textboxNhaXuatBan.TextChanged += textBox5_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 28);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 8;
            label1.Text = "Tên sách";
            label1.Click += label1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(313, 101);
            label3.Name = "label3";
            label3.Size = new Size(108, 20);
            label3.TabIndex = 10;
            label3.Text = "Thời gian nhập";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 101);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 11;
            label4.Text = "Nhà xuất bản";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(454, 28);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 12;
            label5.Text = "Năm xuất bản";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(313, 28);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 13;
            label6.Text = "Thể loại";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(173, 28);
            label7.Name = "label7";
            label7.Size = new Size(55, 20);
            label7.TabIndex = 14;
            label7.Text = "Tác giả";
            label7.Click += label7_Click;
            // 
            // soluong
            // 
            soluong.AutoSize = true;
            soluong.Location = new Point(175, 103);
            soluong.Name = "soluong";
            soluong.Size = new Size(69, 20);
            soluong.TabIndex = 16;
            soluong.Text = "Số lượng";
            // 
            // textboxSoLuong
            // 
            textboxSoLuong.Location = new Point(173, 126);
            textboxSoLuong.Name = "textboxSoLuong";
            textboxSoLuong.Size = new Size(125, 27);
            textboxSoLuong.TabIndex = 17;
            // 
            // pickThoiGianNhap
            // 
            pickThoiGianNhap.Location = new Point(313, 126);
            pickThoiGianNhap.Name = "pickThoiGianNhap";
            pickThoiGianNhap.Size = new Size(250, 27);
            pickThoiGianNhap.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pickThoiGianNhap);
            Controls.Add(textboxSoLuong);
            Controls.Add(soluong);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(textboxNhaXuatBan);
            Controls.Add(textboxNamXuatBan);
            Controls.Add(textboxTheLoai);
            Controls.Add(textboxTacGia);
            Controls.Add(textboxTenSach);
            Controls.Add(btnadd);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnadd;
        private TextBox textboxTenSach;
        private TextBox textboxTacGia;
        private TextBox textboxTheLoai;
        private TextBox textboxNamXuatBan;
        private TextBox textboxNhaXuatBan;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label soluong;
        private TextBox textboxSoLuong;
        private DateTimePicker pickThoiGianNhap;
    }
}
