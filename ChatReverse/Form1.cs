using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatReverse
{
    public partial class Form1 : Form
    {

        string input_path = "";
        string output_path = "";
        List<string> lista_piattaforme = new List<string>() {"","Instagram" };
        public Form1()
        {
            InitializeComponent();
            prog_bar_reverse.Visible = false;
            lbl_perc.Text = "";
            //combo_piattaforma.DataSource = lista_piattaforme;
        }

        private void btn_select_input_path_Click(object sender, EventArgs e)
        {
            dialog_input_fldr.ShowDialog();
            input_path = dialog_input_fldr.SelectedPath;
            txt_input_path.AppendText(dialog_input_fldr.SelectedPath);


        }

        private void btn_select_output_path_Click(object sender, EventArgs e)
        {
             
                dialog_output_fldr.ShowDialog();
                string[] files = Directory.GetFiles(dialog_output_fldr.SelectedPath, "*.*", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                DialogResult dialogResult = MessageBox.Show("La cartella di destinazione selezionata contiene già dei file, continuare comunque?", "Struttura cartella già esistente", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    output_path = dialog_output_fldr.SelectedPath;
                    txt_output_path.AppendText(dialog_output_fldr.SelectedPath);
                }

                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                output_path = dialog_output_fldr.SelectedPath;
                txt_output_path.AppendText(dialog_output_fldr.SelectedPath);
            }
            
        }

        private void btn_revert_Click(object sender, EventArgs e)
        {
            if (txt_input_path.Text == "" || txt_output_path.Text == "")
            {
                MessageBox.Show("Selezionare entrambi i percorsi prima di procedere.");
                return;
            }
            if(!worker_reverse.IsBusy)
            {
                prog_bar_reverse.Visible = true;
                worker_reverse.RunWorkerAsync();

            }
        }

        private void worker_reverse_DoWork(object sender, DoWorkEventArgs e)
        {
            int currLine = 1;
            int totLines = 1;
            foreach (string s in Directory.GetDirectories(input_path, "*", SearchOption.AllDirectories))
            {
                totLines += 1;
            }
            foreach (string s in Directory.GetFiles(input_path, "*.*", SearchOption.AllDirectories))
            {
                totLines += 1;
            }
            foreach (string s in Directory.GetFiles(input_path, "*.html", SearchOption.AllDirectories))
            {
                if (s.Contains("message_"))
                {
                    totLines += 1;
                }
            }

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(input_path, "*", SearchOption.AllDirectories))
            {
                
                Directory.CreateDirectory(dirPath.Replace(input_path, output_path));

                worker_reverse.ReportProgress(currLine++ * 100 / totLines);
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(input_path, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(input_path, output_path), true);

                worker_reverse.ReportProgress(currLine++ * 100 / totLines);
            }


            string[] htmlFiles = Directory.GetFiles(output_path, "*.html", SearchOption.AllDirectories);
            foreach (string s in htmlFiles)
            {
                if (s.Contains("message_"))
                {
                    try
                    {
                        string readText = File.ReadAllText(s);
                        readText = readText.Replace("._4t5n{", "._4t5n{display:flex;flex-direction:column-reverse;");
                        File.WriteAllText(s, string.Empty);
                        File.WriteAllText(s, readText);
                    }
                    catch(Exception Read_Write_exception)
                    {
                        MessageBox.Show("Errore nella modifica dei file HTML: " + Environment.NewLine + Read_Write_exception);

                    }

                    worker_reverse.ReportProgress(currLine++ * 100 / totLines);
                }
            }
            worker_reverse.ReportProgress(currLine++ * 100 / totLines);
        }

        private void worker_reverse_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prog_bar_reverse.Value = e.ProgressPercentage;
            lbl_perc.Text = string.Format("{0}%", e.ProgressPercentage);
            prog_bar_reverse.Update();
        }

        private void worker_reverse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Inversione chat completata, output disponibile in:" + Environment.NewLine + output_path);
            lbl_perc.Invoke((MethodInvoker)delegate { lbl_perc.Text = ""; });
            lbl_perc.Invoke((MethodInvoker)delegate { prog_bar_reverse.Visible = false; });
            if (Directory.Exists(output_path))
            {
                Process.Start(output_path);
            }
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            
        }

    
    }
}
