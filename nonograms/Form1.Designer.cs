namespace nonograms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.bOpen = new System.Windows.Forms.Button();
            this.bResolution = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(12, 12);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 0;
            this.bOpen.Text = "Открыть";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bResolution
            // 
            this.bResolution.Location = new System.Drawing.Point(93, 12);
            this.bResolution.Name = "bResolution";
            this.bResolution.Size = new System.Drawing.Size(75, 23);
            this.bResolution.TabIndex = 1;
            this.bResolution.Text = "Решить";
            this.bResolution.UseVisualStyleBackColor = true;
            this.bResolution.Click += new System.EventHandler(this.bResolution_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 343);
            this.Controls.Add(this.bResolution);
            this.Controls.Add(this.bOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Nonograms";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bResolution;
    }
}

