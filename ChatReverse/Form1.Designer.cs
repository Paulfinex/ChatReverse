
namespace ChatReverse
{
    partial class Form1
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
            this.btn_select_input_path = new System.Windows.Forms.Button();
            this.dialog_input_fldr = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_revert = new System.Windows.Forms.Button();
            this.btn_select_output_path = new System.Windows.Forms.Button();
            this.dialog_output_fldr = new System.Windows.Forms.FolderBrowserDialog();
            this.worker_reverse = new System.ComponentModel.BackgroundWorker();
            this.prog_bar_reverse = new System.Windows.Forms.ProgressBar();
            this.lbl_perc = new System.Windows.Forms.Label();
            this.txt_input_path = new System.Windows.Forms.TextBox();
            this.txt_output_path = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_select_input_path
            // 
            this.btn_select_input_path.Location = new System.Drawing.Point(10, 25);
            this.btn_select_input_path.Name = "btn_select_input_path";
            this.btn_select_input_path.Size = new System.Drawing.Size(117, 24);
            this.btn_select_input_path.TabIndex = 2;
            this.btn_select_input_path.Text = "Cartella Origine";
            this.btn_select_input_path.UseVisualStyleBackColor = true;
            this.btn_select_input_path.Click += new System.EventHandler(this.btn_select_input_path_Click);
            // 
            // btn_revert
            // 
            this.btn_revert.Location = new System.Drawing.Point(10, 83);
            this.btn_revert.Name = "btn_revert";
            this.btn_revert.Size = new System.Drawing.Size(117, 23);
            this.btn_revert.TabIndex = 5;
            this.btn_revert.Text = "Inverti Chat";
            this.btn_revert.UseVisualStyleBackColor = true;
            this.btn_revert.Click += new System.EventHandler(this.btn_revert_Click);
            // 
            // btn_select_output_path
            // 
            this.btn_select_output_path.Location = new System.Drawing.Point(10, 55);
            this.btn_select_output_path.Name = "btn_select_output_path";
            this.btn_select_output_path.Size = new System.Drawing.Size(117, 24);
            this.btn_select_output_path.TabIndex = 6;
            this.btn_select_output_path.Text = "Cartella Destinazione";
            this.btn_select_output_path.UseVisualStyleBackColor = true;
            this.btn_select_output_path.Click += new System.EventHandler(this.btn_select_output_path_Click);
            // 
            // worker_reverse
            // 
            this.worker_reverse.WorkerReportsProgress = true;
            this.worker_reverse.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_reverse_DoWork);
            this.worker_reverse.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.worker_reverse_ProgressChanged);
            this.worker_reverse.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_reverse_RunWorkerCompleted);
            // 
            // prog_bar_reverse
            // 
            this.prog_bar_reverse.Location = new System.Drawing.Point(133, 83);
            this.prog_bar_reverse.Name = "prog_bar_reverse";
            this.prog_bar_reverse.Size = new System.Drawing.Size(186, 23);
            this.prog_bar_reverse.TabIndex = 8;
            // 
            // lbl_perc
            // 
            this.lbl_perc.AutoSize = true;
            this.lbl_perc.Location = new System.Drawing.Point(142, 110);
            this.lbl_perc.Name = "lbl_perc";
            this.lbl_perc.Size = new System.Drawing.Size(0, 13);
            this.lbl_perc.TabIndex = 9;
            // 
            // txt_input_path
            // 
            this.txt_input_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_input_path.Location = new System.Drawing.Point(133, 28);
            this.txt_input_path.Name = "txt_input_path";
            this.txt_input_path.ReadOnly = true;
            this.txt_input_path.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_input_path.Size = new System.Drawing.Size(272, 19);
            this.txt_input_path.TabIndex = 10;
            // 
            // txt_output_path
            // 
            this.txt_output_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_output_path.Location = new System.Drawing.Point(133, 58);
            this.txt_output_path.Name = "txt_output_path";
            this.txt_output_path.ReadOnly = true;
            this.txt_output_path.Size = new System.Drawing.Size(272, 19);
            this.txt_output_path.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 150);
            this.Controls.Add(this.txt_output_path);
            this.Controls.Add(this.txt_input_path);
            this.Controls.Add(this.lbl_perc);
            this.Controls.Add(this.prog_bar_reverse);
            this.Controls.Add(this.btn_select_output_path);
            this.Controls.Add(this.btn_revert);
            this.Controls.Add(this.btn_select_input_path);
            this.Name = "Form1";
            this.Text = "Inverti Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_select_input_path;
        private System.Windows.Forms.FolderBrowserDialog dialog_input_fldr;
        private System.Windows.Forms.Button btn_revert;
        private System.Windows.Forms.Button btn_select_output_path;
        private System.Windows.Forms.FolderBrowserDialog dialog_output_fldr;
        private System.ComponentModel.BackgroundWorker worker_reverse;
        private System.Windows.Forms.ProgressBar prog_bar_reverse;
        private System.Windows.Forms.Label lbl_perc;
        private System.Windows.Forms.TextBox txt_input_path;
        private System.Windows.Forms.TextBox txt_output_path;
    }
}

