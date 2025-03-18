namespace book
{
    partial class Deleted
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
            Danhsachdaxoa = new DataGridView();
            btnKhoiphuc = new Button();
            ((System.ComponentModel.ISupportInitialize)Danhsachdaxoa).BeginInit();
            SuspendLayout();
            // 
            // Danhsachdaxoa
            // 
            Danhsachdaxoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Danhsachdaxoa.Location = new Point(21, 33);
            Danhsachdaxoa.Name = "Danhsachdaxoa";
            Danhsachdaxoa.RowHeadersWidth = 51;
            Danhsachdaxoa.Size = new Size(938, 218);
            Danhsachdaxoa.TabIndex = 0;
            // 
            // btnKhoiphuc
            // 
            btnKhoiphuc.Location = new Point(393, 333);
            btnKhoiphuc.Name = "btnKhoiphuc";
            btnKhoiphuc.Size = new Size(94, 29);
            btnKhoiphuc.TabIndex = 1;
            btnKhoiphuc.Text = "Khôi phục";
            btnKhoiphuc.UseVisualStyleBackColor = true;
            btnKhoiphuc.Click += btnKhoiphuc_Click;
            // 
            // Deleted
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 383);
            Controls.Add(btnKhoiphuc);
            Controls.Add(Danhsachdaxoa);
            Name = "Deleted";
            Text = "Xoa";
            ((System.ComponentModel.ISupportInitialize)Danhsachdaxoa).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView Danhsachdaxoa;
        private Button btnKhoiphuc;
    }
}