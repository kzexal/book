namespace book
{
    partial class begin
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
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            dataGridViewBooks = new DataGridView();
            btnLamMoi = new Button();
            btnDanhsachdaxoa = new Button();
            btnDauSach = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            SuspendLayout();
            // 
            // btnThem
            // 
            btnThem.Location = new Point(73, 321);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 0;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(214, 321);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(363, 294);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xoá";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBooks.BackgroundColor = Color.Gainsboro;
            dataGridViewBooks.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Location = new Point(12, 49);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.RowHeadersWidth = 51;
            dataGridViewBooks.Size = new Size(929, 230);
            dataGridViewBooks.TabIndex = 3;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(498, 321);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 4;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnDanhsachdaxoa
            // 
            btnDanhsachdaxoa.Location = new Point(330, 342);
            btnDanhsachdaxoa.Name = "btnDanhsachdaxoa";
            btnDanhsachdaxoa.Size = new Size(149, 29);
            btnDanhsachdaxoa.TabIndex = 5;
            btnDanhsachdaxoa.Text = "Danh sách đã xoá";
            btnDanhsachdaxoa.UseVisualStyleBackColor = true;
            btnDanhsachdaxoa.Click += btnDanhsachdaxoa_Click;
            // 
            // btnDauSach
            // 
            btnDauSach.Location = new Point(628, 321);
            btnDauSach.Name = "btnDauSach";
            btnDauSach.Size = new Size(94, 29);
            btnDauSach.TabIndex = 6;
            btnDauSach.Text = "Đầu sách";
            btnDauSach.UseVisualStyleBackColor = true;
            btnDauSach.Click += btnDauSach_Click;
            // 
            // begin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 383);
            Controls.Add(btnDauSach);
            Controls.Add(btnDanhsachdaxoa);
            Controls.Add(btnLamMoi);
            Controls.Add(dataGridViewBooks);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Name = "begin";
            Text = "begin";
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private DataGridView dataGridViewBooks;
        private Button btnLamMoi;
        private Button btnDanhsachdaxoa;
        private Button btnDauSach;
    }
}