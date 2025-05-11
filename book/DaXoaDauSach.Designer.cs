namespace book
{
    partial class DaXoaDauSach
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
            btnKhoiphuc = new Button();
            Danhsachdaxoa = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)Danhsachdaxoa).BeginInit();
            SuspendLayout();
            // 
            // btnKhoiphuc
            // 
            btnKhoiphuc.Location = new Point(384, 342);
            btnKhoiphuc.Name = "btnKhoiphuc";
            btnKhoiphuc.Size = new Size(94, 29);
            btnKhoiphuc.TabIndex = 3;
            btnKhoiphuc.Text = "Khôi phục";
            btnKhoiphuc.UseVisualStyleBackColor = true;
            // 
            // Danhsachdaxoa
            // 
            Danhsachdaxoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Danhsachdaxoa.Location = new Point(12, 42);
            Danhsachdaxoa.Name = "Danhsachdaxoa";
            Danhsachdaxoa.RowHeadersWidth = 51;
            Danhsachdaxoa.Size = new Size(938, 218);
            Danhsachdaxoa.TabIndex = 2;
            // 
            // DaXoaDauSach
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 383);
            Controls.Add(btnKhoiphuc);
            Controls.Add(Danhsachdaxoa);
            Name = "DaXoaDauSach";
            Text = "DaXoaDauSach";
            ((System.ComponentModel.ISupportInitialize)Danhsachdaxoa).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnKhoiphuc;
        private DataGridView Danhsachdaxoa;
    }
}