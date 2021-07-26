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
        public Form1()
        {
            InitializeComponent();
            prog_bar_reverse.Visible = false;
            lbl_perc.Text = "";
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
            string[] files;
            try 
            {
                 files = Directory.GetFiles(dialog_output_fldr.SelectedPath, "*.*", SearchOption.AllDirectories);
            }
            catch(Exception getFileEX)
            {
                MessageBox.Show(getFileEX.Message);
                return;
            }
            if (files.Length > 0)
            {
                DialogResult dialogResult = MessageBox.Show("La cartella di destinazione selezionata contiene già dei file, continuare comunque?", "Struttura cartella già esistente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            string[] files;
            try
            {
                files = Directory.GetFiles(input_path, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception getFileEX)
            {
                MessageBox.Show(getFileEX.Message);
                return;
            }
            if (txt_input_path.Text == "" || txt_output_path.Text == "")
            {
                MessageBox.Show("Selezionare entrambi i percorsi prima di procedere.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (files.Length==0)
            {
                MessageBox.Show("Non sono presenti file da modificare nella cartella Origine selezionata.","Errore",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
            if (!worker_reverse.IsBusy)
            {
                prog_bar_reverse.Visible = true;
                worker_reverse.RunWorkerAsync();
            }
        }

        private void worker_reverse_DoWork(object sender, DoWorkEventArgs e)
        {
            int currLine = 1;
            int totLines = 1;
            // Count iterations for all files and directories, adding to the totLines for the prog_bar
            lbl_perc.Invoke((MethodInvoker)delegate { lbl_curr_op.Text = "Calcolo iterazioni.."; });
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
                    if (check_paths.Checked)
                    {

                        totLines += 2;
                    }
                    else
                    {
                        totLines += 1;

                    }
                }
            }
            //Copy source dir structure to destination
            lbl_perc.Invoke((MethodInvoker)delegate { lbl_curr_op.Text = "Copia struttura cartelle.."; });
            foreach (string dirPath in Directory.GetDirectories(input_path, "*", SearchOption.AllDirectories))
            {

                Directory.CreateDirectory(dirPath.Replace(input_path, output_path));
                worker_reverse.ReportProgress(currLine++ * 100 / totLines);
            }
            //Copy source File structure to destination
            lbl_perc.Invoke((MethodInvoker)delegate { lbl_curr_op.Text = "Copia struttura file.."; });
            foreach (string newPath in Directory.GetFiles(input_path, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(input_path, output_path), true);
                worker_reverse.ReportProgress(currLine++ * 100 / totLines);
            }
            //Modify HTML files in destination dir
            string[] htmlFiles = Directory.GetFiles(output_path, "*.html", SearchOption.AllDirectories);
            //var originalmediapath= Directory.EnumerateFiles(output_path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp3") || s.EndsWith(".jpg");
            var destinationmediapath = Directory.EnumerateFiles(output_path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp4") || s.EndsWith(".png") || s.EndsWith(".gif") || s.EndsWith(".avi") || s.EndsWith(".jpg") || s.EndsWith(".mp3"));
            int currFileCounter = 1;

            int totMediaFiles = destinationmediapath.Count();
            foreach (string s in htmlFiles)
            {
                
                   
               
                if (s.Contains("message_"))
                {
                    
                    try
                    {
                        string readText = File.ReadAllText(s);


                        if (check_paths.Checked)
                        {
                            string rebuildText = "";
                            foreach (string dp in destinationmediapath)
                            {
                                
                                string[] splitText = readText.Split('"');
                                rebuildText = "";
                                foreach (string split in splitText)
                                {
                                    if (split.Contains(Path.GetFileName(dp)))
                                    {
                                        lbl_perc.Invoke((MethodInvoker)delegate { lbl_curr_op.Text = "Cambio Referenze Media: " + currFileCounter + "/" + totMediaFiles.ToString(); });

                                        currFileCounter++;
                                        rebuildText += dp + "\"";
                                    }
                                    else
                                    {
                                        rebuildText += split + "\"";
                                    }
                                }
                                readText = rebuildText;
                            }
                            worker_reverse.ReportProgress(currLine++ * 100 / totLines);
                        }
                        lbl_perc.Invoke((MethodInvoker)delegate { lbl_curr_op.Text = "Inversione chat.."; });
                        readText = readText.Replace("._4t5n{", "._4t5n{display:flex;flex-direction:column-reverse;");
                        File.WriteAllText(s, string.Empty);
                        File.WriteAllText(s, readText);
                    }
                    catch (Exception Read_Write_exception)
                    {
                        MessageBox.Show("Errore nella modifica dei file HTML: " + Environment.NewLine + Read_Write_exception.Message);

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

        private List<string> relativePath(string[] paths)
        {
            List<string> relpath = new List<string>();

            foreach (string s in paths)
            { 
                string[] temp = s.Split('\\');
                string strbuilder = "";
                string formatstr = "";
                foreach (string s2 in temp)
                {
                    if (s2 != "messages")
                    {
                        strbuilder += s2 + @"\";
                    }
                    else
                    {
                        break;
                    }
                }
                formatstr = s.Replace(strbuilder, "");
                relpath.Add(formatstr);
            }

            return relpath;
        
        }

        private void check_paths_CheckedChanged(object sender, EventArgs e)
        {
            if(check_paths.Checked)
            {
                MessageBox.Show("Mantenere i media per le chat aumenterà notevolmente il tempo di esecuzione.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
