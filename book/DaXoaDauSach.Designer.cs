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
            btnRestore = new Button();
            datagridviewDauSachDaXoa = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)datagridviewDauSachDaXoa).BeginInit();
            SuspendLayout();
            // 
            // btnRestore
            // 
            btnRestore.Location = new Point(384, 342);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(94, 29);
            btnRestore.TabIndex = 3;
            btnRestore.Text = "Khôi phục";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += btnRestore_Click;
            // 
            // datagridviewDauSachDaXoa
            // 
            datagridviewDauSachDaXoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridviewDauSachDaXoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridviewDauSachDaXoa.Location = new Point(12, 42);
            datagridviewDauSachDaXoa.Name = "datagridviewDauSachDaXoa";
            datagridviewDauSachDaXoa.RowHeadersWidth = 51;
            datagridviewDauSachDaXoa.Size = new Size(938, 218);
            datagridviewDauSachDaXoa.TabIndex = 2;
            // 
            // DaXoaDauSach
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 383);
            Controls.Add(btnRestore);
            Controls.Add(datagridviewDauSachDaXoa);
            Name = "DaXoaDauSach";
            Text = "DaXoaDauSach";
            ((System.ComponentModel.ISupportInitialize)datagridviewDauSachDaXoa).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnRestore;
        private DataGridView datagridviewDauSachDaXoa;
    }
}