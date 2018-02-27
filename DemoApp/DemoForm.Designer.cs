namespace DemoApp
{
    partial class DemoForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.typeSelection = new System.Windows.Forms.ComboBox();
            this.goButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 39);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(610, 476);
            this.dataGridView.TabIndex = 0;
            // 
            // typeSelection
            // 
            this.typeSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSelection.FormattingEnabled = true;
            this.typeSelection.Items.AddRange(new object[] {
            "Ripe",
            "Geonames",
            "Wikipedia - HAP",
            "Datahub"});
            this.typeSelection.Location = new System.Drawing.Point(12, 12);
            this.typeSelection.Name = "typeSelection";
            this.typeSelection.Size = new System.Drawing.Size(275, 21);
            this.typeSelection.TabIndex = 1;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(293, 10);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 527);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.typeSelection);
            this.Controls.Add(this.dataGridView);
            this.MinimumSize = new System.Drawing.Size(395, 248);
            this.Name = "DemoForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox typeSelection;
        private System.Windows.Forms.Button goButton;
    }
}

