namespace book
{
    partial class DauSach
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
            dataGridViewDauSach = new DataGridView();
            btnThemDauSach = new Button();
            btnSuaDauSach = new Button();
            btnXoaDauSach = new Button();
            btnDanhSachDaXoaDauSach = new Button();
            btnLamMoiDauSach = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDauSach).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewDauSach
            // 
            dataGridViewDauSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDauSach.BackgroundColor = Color.Silver;
            dataGridViewDauSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDauSach.Location = new Point(12, 22);
            dataGridViewDauSach.Name = "dataGridViewDauSach";
            dataGridViewDauSach.RowHeadersWidth = 51;
            dataGridViewDauSach.Size = new Size(947, 164);
            dataGridViewDauSach.TabIndex = 0;
            // 
            // btnThemDauSach
            // 
            btnThemDauSach.Location = new Point(122, 330);
            btnThemDauSach.Name = "btnThemDauSach";
            btnThemDauSach.Size = new Size(94, 29);
            btnThemDauSach.TabIndex = 1;
            btnThemDauSach.Text = "Thêm";
            btnThemDauSach.UseVisualStyleBackColor = true;
            btnThemDauSach.Click += btnThemDauSach_Click;
            // 
            // btnSuaDauSach
            // 
            btnSuaDauSach.Location = new Point(248, 330);
            btnSuaDauSach.Name = "btnSuaDauSach";
            btnSuaDauSach.Size = new Size(94, 29);
            btnSuaDauSach.TabIndex = 2;
            btnSuaDauSach.Text = "Sửa";
            btnSuaDauSach.UseVisualStyleBackColor = true;
            btnSuaDauSach.Click += btnSuaDauSach_Click;
            // 
            // btnXoaDauSach
            // 
            btnXoaDauSach.Location = new Point(389, 304);
            btnXoaDauSach.Name = "btnXoaDauSach";
            btnXoaDauSach.Size = new Size(94, 29);
            btnXoaDauSach.TabIndex = 3;
            btnXoaDauSach.Text = "Xoá";
            btnXoaDauSach.UseVisualStyleBackColor = true;
            btnXoaDauSach.Click += btnXoaDauSach_Click;
            // 
            // btnDanhSachDaXoaDauSach
            // 
            btnDanhSachDaXoaDauSach.Location = new Point(369, 342);
            btnDanhSachDaXoaDauSach.Name = "btnDanhSachDaXoaDauSach";
            btnDanhSachDaXoaDauSach.Size = new Size(142, 29);
            btnDanhSachDaXoaDauSach.TabIndex = 4;
            btnDanhSachDaXoaDauSach.Text = "Danh sách đã xoá";
            btnDanhSachDaXoaDauSach.UseVisualStyleBackColor = true;
            btnDanhSachDaXoaDauSach.Click += btnDanhSachDaXoaDauSach_Click;
            // 
            // btnLamMoiDauSach
            // 
            btnLamMoiDauSach.Location = new Point(540, 330);
            btnLamMoiDauSach.Name = "btnLamMoiDauSach";
            btnLamMoiDauSach.Size = new Size(94, 29);
            btnLamMoiDauSach.TabIndex = 5;
            btnLamMoiDauSach.Text = "Làm mới";
            btnLamMoiDauSach.UseVisualStyleBackColor = true;
            btnLamMoiDauSach.Click += btnLamMoiDauSach_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(65, 211);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 6;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(287, 211);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 7;
            // 
            // DauSach
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 383);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(btnLamMoiDauSach);
            Controls.Add(btnDanhSachDaXoaDauSach);
            Controls.Add(btnXoaDauSach);
            Controls.Add(btnSuaDauSach);
            Controls.Add(btnThemDauSach);
            Controls.Add(dataGridViewDauSach);
            Name = "DauSach";
            Text = "DauSach";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDauSach).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewDauSach;
        private Button btnThemDauSach;
        private Button btnSuaDauSach;
        private Button btnXoaDauSach;
        private Button btnDanhSachDaXoaDauSach;
        private Button btnLamMoiDauSach;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
    }
}