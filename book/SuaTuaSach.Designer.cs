namespace book
{
    partial class SuaTuaSach
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
            btnLuu = new Button();
            btnThemTacGia = new Button();
            btnTacGiaPhu = new Button();
            btnTacGiaChinh = new Button();
            label8 = new Label();
            label2 = new Label();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            pickThoiGianNhap = new DateTimePicker();
            textboxSoLuong = new TextBox();
            soluong = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            textboxNhaXuatBan = new TextBox();
            textboxNamXuatBan = new TextBox();
            textboxTheLoai = new TextBox();
            textboxTacGia = new TextBox();
            textboxTenSach = new TextBox();
            textboxIDTuasach = new TextBox();
            IDTuaSach = new Label();
            SuspendLayout();
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(425, 342);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 29);
            btnLuu.TabIndex = 0;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnThemTacGia
            // 
            btnThemTacGia.Location = new Point(611, 39);
            btnThemTacGia.Name = "btnThemTacGia";
            btnThemTacGia.Size = new Size(119, 37);
            btnThemTacGia.TabIndex = 48;
            btnThemTacGia.Text = "Thêm tác giả";
            btnThemTacGia.UseVisualStyleBackColor = true;
            btnThemTacGia.Click += btnThemTacGia_Click;
            // 
            // btnTacGiaPhu
            // 
            btnTacGiaPhu.Location = new Point(622, 189);
            btnTacGiaPhu.Name = "btnTacGiaPhu";
            btnTacGiaPhu.Size = new Size(94, 37);
            btnTacGiaPhu.TabIndex = 47;
            btnTacGiaPhu.Text = "<";
            btnTacGiaPhu.UseVisualStyleBackColor = true;
            btnTacGiaPhu.Click += btnTacGiaPhu_Click;
            // 
            // btnTacGiaChinh
            // 
            btnTacGiaChinh.Location = new Point(622, 127);
            btnTacGiaChinh.Name = "btnTacGiaChinh";
            btnTacGiaChinh.Size = new Size(94, 37);
            btnTacGiaChinh.TabIndex = 46;
            btnTacGiaChinh.Text = ">";
            btnTacGiaChinh.UseVisualStyleBackColor = true;
            btnTacGiaChinh.Click += btnTacGiaChinh_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(729, 104);
            label8.Name = "label8";
            label8.Size = new Size(94, 20);
            label8.TabIndex = 45;
            label8.Text = "Tác giả chính";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(447, 95);
            label2.Name = "label2";
            label2.Size = new Size(126, 20);
            label2.TabIndex = 44;
            label2.Text = "Danh sách tác giả";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(729, 127);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(167, 124);
            listBox2.TabIndex = 43;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(447, 123);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(157, 124);
            listBox1.TabIndex = 42;
            // 
            // pickThoiGianNhap
            // 
            pickThoiGianNhap.Location = new Point(74, 238);
            pickThoiGianNhap.Name = "pickThoiGianNhap";
            pickThoiGianNhap.Size = new Size(250, 27);
            pickThoiGianNhap.TabIndex = 41;
            // 
            // textboxSoLuong
            // 
            textboxSoLuong.Location = new Point(224, 39);
            textboxSoLuong.Name = "textboxSoLuong";
            textboxSoLuong.Size = new Size(125, 27);
            textboxSoLuong.TabIndex = 40;
            // 
            // soluong
            // 
            soluong.AutoSize = true;
            soluong.Location = new Point(224, 16);
            soluong.Name = "soluong";
            soluong.Size = new Size(69, 20);
            soluong.TabIndex = 39;
            soluong.Text = "Số lượng";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(450, 16);
            label7.Name = "label7";
            label7.Size = new Size(55, 20);
            label7.TabIndex = 38;
            label7.Text = "Tác giả";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(74, 92);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 37;
            label6.Text = "Thể loại";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(224, 94);
            label5.Name = "label5";
            label5.Size = new Size(102, 20);
            label5.TabIndex = 36;
            label5.Text = "Năm xuất bản";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(74, 145);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 35;
            label4.Text = "Nhà xuất bản";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(76, 215);
            label3.Name = "label3";
            label3.Size = new Size(108, 20);
            label3.TabIndex = 34;
            label3.Text = "Thời gian nhập";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 18);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 33;
            label1.Text = "Tên sách";
            // 
            // textboxNhaXuatBan
            // 
            textboxNhaXuatBan.Location = new Point(74, 171);
            textboxNhaXuatBan.Name = "textboxNhaXuatBan";
            textboxNhaXuatBan.Size = new Size(125, 27);
            textboxNhaXuatBan.TabIndex = 32;
            // 
            // textboxNamXuatBan
            // 
            textboxNamXuatBan.Location = new Point(224, 117);
            textboxNamXuatBan.Name = "textboxNamXuatBan";
            textboxNamXuatBan.Size = new Size(125, 27);
            textboxNamXuatBan.TabIndex = 31;
            // 
            // textboxTheLoai
            // 
            textboxTheLoai.Location = new Point(74, 115);
            textboxTheLoai.Name = "textboxTheLoai";
            textboxTheLoai.Size = new Size(125, 27);
            textboxTheLoai.TabIndex = 30;
            // 
            // textboxTacGia
            // 
            textboxTacGia.Location = new Point(450, 39);
            textboxTacGia.Name = "textboxTacGia";
            textboxTacGia.Size = new Size(125, 27);
            textboxTacGia.TabIndex = 29;
            // 
            // textboxTenSach
            // 
            textboxTenSach.Location = new Point(74, 41);
            textboxTenSach.Name = "textboxTenSach";
            textboxTenSach.Size = new Size(125, 27);
            textboxTenSach.TabIndex = 28;
            // 
            // textboxIDTuasach
            // 
            textboxIDTuasach.Location = new Point(74, 302);
            textboxIDTuasach.Name = "textboxIDTuasach";
            textboxIDTuasach.Size = new Size(125, 27);
            textboxIDTuasach.TabIndex = 49;
            // 
            // IDTuaSach
            // 
            IDTuaSach.AutoSize = true;
            IDTuaSach.Location = new Point(78, 279);
            IDTuaSach.Name = "IDTuaSach";
            IDTuaSach.Size = new Size(78, 20);
            IDTuaSach.TabIndex = 50;
            IDTuaSach.Text = "IDTuaSach";
            IDTuaSach.Click += label9_Click;
            // 
            // Sua
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 383);
            Controls.Add(IDTuaSach);
            Controls.Add(textboxIDTuasach);
            Controls.Add(btnThemTacGia);
            Controls.Add(btnTacGiaPhu);
            Controls.Add(btnTacGiaChinh);
            Controls.Add(label8);
            Controls.Add(label2);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
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
            Controls.Add(btnLuu);
            Name = "Sua";
            Text = "Sua";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLuu;
        private Button btnThemTacGia;
        private Button btnTacGiaChinh;
        private Label label8;
        private Label label2;
        private ListBox listBox2;
        private ListBox listBox1;
        private DateTimePicker pickThoiGianNhap;
        private TextBox textboxSoLuong;
        private Label soluong;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private TextBox textboxNhaXuatBan;
        private TextBox textboxNamXuatBan;
        private TextBox textboxTheLoai;
        private TextBox textboxTacGia;
        private TextBox textboxTenSach;
        private TextBox textboxIDTuasach;
        private Button btnTacGiaPhu;
        private Label IDTuaSach;
    }
}