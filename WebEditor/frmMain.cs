using EsseivaN.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WebsiteEditor
{
    public partial class frmMain : Form
    {
        private List<string> variables = new List<string>();
        private List<string> htmlFiles = new List<string>();
        private Dictionary<string, string> htmlFilesOutput = new Dictionary<string, string>();
        private List<string> contentFiles = new List<string>();
        private List<Parameters> parameters = new List<Parameters>();
        private string OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WebsiteEditor";

        public frmMain()
        {
            InitializeComponent();
            txtOutput.Text = OutputPath;
        }

        private void btnVar_Click(object sender, EventArgs e)
        {
            if (txtVar.Text != string.Empty)
            {
                string data = $"{{{txtVar.Text}}}";
                if (!variables.Contains(data))
                {
                    variables.Add(data);
                    listVar.Items.Add(data);
                }
                txtVar.Focus();
                txtVar.SelectAll();
            }
        }

        private void btnHtml_Click(object sender, EventArgs e)
        {
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                htmlFiles.AddRange(openDialog.FileNames.Where(x => !htmlFiles.Contains(x)));
                FillHtmlOutput();
                listHtml.Items.Clear();
                listHtml.Items.AddRange(htmlFiles.ToArray());
                listHtml2.Items.Clear();
                listHtml2.Items.AddRange(htmlFilesOutput.Values.ToArray());
            }
        }

        private void FillHtmlOutput()
        {
            foreach (string htmlFile in htmlFiles)
            {
                if (!htmlFilesOutput.ContainsKey(htmlFile))
                    htmlFilesOutput.Add(htmlFile, htmlFile);
            }
        }

        private void btnContent_Click(object sender, EventArgs e)
        {
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                contentFiles.AddRange(openDialog.FileNames.Where(x => !contentFiles.Contains(x)));
                listContent.Items.Clear();
                listContent.Items.AddRange(contentFiles.ToArray());
            }
        }

        private void listContent_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < listContent.Items.Count; ++i)
                if (i != e.Index) listContent.SetItemChecked(i, false);
        }

        private void txtVar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnVar.PerformClick();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (listParam.CheckedItems.Count != 0)
            {
                foreach (Parameters parameter in listParam.CheckedItems)
                {
                    ExecuteScripts(parameter);
                }
            }
            else
                statusStripText.Text = "No script selected";
        }

        public void ImportExecuteScript(string path)
        {
            ImportFile(path);
            foreach (var parameter in parameters)
            {
                ExecuteScripts(parameter);
            }
        }

        public void ExecuteScripts(Parameters parameter)
        {
            statusStripText.Text = "Replacing";

            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);

            // Delete old file
            foreach (string htmlFile in parameter.HtmlFiles)
            {
                File.Delete(OutputPath + "\\" + Path.GetFileName(htmlFile));
            }
            for (short i = 0; i < parameter.HtmlFiles.Count; i++)
            {
                string htmlFile = parameter.HtmlFiles[i];

                // Read base text or already changed text
                string html = File.Exists(OutputPath + "\\" + Path.GetFileName(htmlFile)) ? File.ReadAllText(OutputPath + "\\" + Path.GetFileName(htmlFile)) : File.ReadAllText(htmlFile);

                foreach (string contentfile in parameter.ContentFile)
                {
                    string content = File.ReadAllText(contentfile);

                    if (parameter.VarInFile)
                    {
                        string[] variablesContent = content.Replace("###END VARIABLE###\r\n", ((char)0x1e).ToString()).Replace("###END VARIABLE###", string.Empty).Split((char)0x1e).Where(x => (x != string.Empty) && (x.Contains("###BEGIN VARIABLE###\r\n"))).ToArray();
                        foreach (string variableContent in variablesContent)
                        {
                            var temp = variableContent.Replace("###BEGIN VARIABLE###\r\n", string.Empty).Split(new string[] { "#\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (temp.Length != 2)
                                break;

                            string name = $"{{{temp[0]}}}";
                            if (parameter.Vars.Contains(name))
                            {
                                string text = temp[1];
                                html = html.Replace(name, text);
                            }
                        }
                    }
                    else
                    {
                        foreach (string variable in parameter.Vars)
                        {
                            html = html.Replace(variable, content);
                        }
                    }

                    string path = (parameter.HtmlFilesOutput[i] == string.Empty) ? (Path.GetFullPath(parameter.OutputDir) + "\\" + Path.GetFileName(htmlFile)) : parameter.HtmlFilesOutput[i];
                    File.WriteAllText(path, html);
                }
            }
            statusStripText.Text = "Replacement done";
        }

        private void btnParam_Click(object sender, EventArgs e)
        {
            if (listVar.CheckedItems.Count != 0 && listHtml.CheckedItems.Count != 0 && (((listContent.CheckedItems.Count != 0) && boxContent.Checked) || ((listContentVar.CheckedItems.Count != 0) && !boxContent.Checked)))
            {
                Parameters param = new Parameters(listVar, listHtml, boxContent.Checked ? listContent : listContentVar, listHtml2)
                {
                    VarInFile = boxVars.Checked,
                    OutputDir = OutputPath
                };
                if (!parameters.Contains(param))
                {
                    parameters.Add(param);
                    listParam.Items.Clear();
                    listParam.Items.AddRange(parameters.ToArray());
                }
            }
            else
                statusStripText.Text = "Parameters not selected";
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == DialogResult.OK)
            {
                ExportFile(saveDataDialog.FileName);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == DialogResult.OK)
            {
                ImportFile(openDataDialog.FileName);
            }
        }

        public void ImportFile(string FileName)
        {
            // Read file and remove \r caracters
            string content = File.ReadAllText(FileName).Replace("\r","");

            // Set the end caracter as 0x1e
            content = content.Replace("###END PARAMETER###\n", ((char)0x1e).ToString());
            string[] importedParams = content.Split((char)0x1e).Where(x => (x != string.Empty) && (x.Contains("###BEGIN PARAMETER###\n"))).ToArray();

            if (importedParams.Length != 0)
            {
                foreach (string importedParm in importedParams)
                {
                    Parameters newParameters = new Parameters();
                    string[] Values = importedParm.Replace("###BEGIN PARAMETER###\n", string.Empty).Replace("\n", ((char)0x1e).ToString()).Split((char)0x1e).Where(x => x != String.Empty).ToArray();

                    short HtmlCount = 0;

                    foreach (string Value in Values)
                    {
                        var temp = Value.Split(new char[] { '#' }, 2);
                        if (temp.Length != 2)
                            break;

                        string index = temp[0];
                        string val = temp[1];

                        if (index == "0")
                            newParameters.OutputDir = val;
                        else if (index == "1")
                            newParameters.Vars.Add(val);
                        else if (index == "2")
                        {
                            newParameters.HtmlFiles.Add(val);
                            newParameters.HtmlFilesOutput.Add(string.Empty);
                            HtmlCount++;
                        }
                        else if (index == "3")
                            newParameters.ContentFile.Add(val);
                        else if (index == "4")
                            newParameters.VarInFile = val.ToUpper() == bool.TrueString.ToUpper();
                        else if (index == "5")
                            newParameters.HtmlFilesOutput[(HtmlCount - 1)] = val;
                    }
                    parameters.Add(newParameters);
                    listParam.Items.Clear();
                    listParam.Items.AddRange(parameters.ToArray());
                }

                statusStripText.Text = "Successfully imported";
            }
            else
            {
                statusStripText.Text = "Invalid config file";
            }
        }

        private void ExportFile(string FileName)
        {
            string content = "";

            foreach (Parameters parameter in parameters)
            {
                content += "###BEGIN PARAMETER###\r\n";
                content += $"0#{parameter.OutputDir}\r\n";

                foreach (string var in parameter.Vars)
                {
                    content += $"1#{var}\r\n";
                }
                for (short i = 0; i < parameter.HtmlFiles.Count; i++)
                {
                    content += $"2#{parameter.HtmlFiles[i]}\r\n";
                    if (parameter.HtmlFilesOutput[i] != string.Empty)
                        content += $"5#{parameter.HtmlFilesOutput[i]}\r\n";
                }
                for (short i = 0; i < parameter.ContentFile.Count; i++)
                {
                    content += $"3#{parameter.ContentFile[i]}\r\n";
                }
                content += $"4#{parameter.VarInFile.ToString()}\r\n";
                content += "###END PARAMETER###\r\n";
            }

            File.WriteAllText(FileName, content);
            statusStripText.Text = "Successfully exported";
        }

        private void listHtml_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Check file exists
                    if (File.Exists(fileLoc) && !htmlFiles.Contains(fileLoc))
                    {
                        htmlFiles.Add(fileLoc);
                        FillHtmlOutput();
                        listHtml.Items.Clear();
                        listHtml.Items.AddRange(htmlFiles.ToArray());
                        listHtml2.Items.Clear();
                        listHtml2.Items.AddRange(htmlFilesOutput.Values.ToArray());
                    }
                }
            }
        }

        private void listContent_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Check file exists
                    if (File.Exists(fileLoc) && !contentFiles.Contains(fileLoc))
                    {
                        contentFiles.Add(fileLoc);
                        listContent.Items.Clear();
                        listContent.Items.AddRange(contentFiles.ToArray());
                        listContentVar.Items.Clear();
                        listContentVar.Items.AddRange(contentFiles.ToArray());
                    }

                }
            }
        }

        private void listParam_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Check file exists
                    if (File.Exists(fileLoc) && !contentFiles.Contains(fileLoc))
                    {
                        ImportFile(fileLoc);
                    }
                }
            }
        }

        private void list_DragEnter(object sender, DragEventArgs e)
        {
            // If file is dragged, show cursor "Drop allowed"
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            folderDialog.SelectedPath = OutputPath;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                OutputPath = folderDialog.SelectedPath;
                txtOutput.Text = OutputPath;
            }
        }

        private void boxVars_CheckedChanged(object sender, EventArgs e)
        {
            boxContent.Checked = !boxVars.Checked;
        }

        private void boxContent_CheckedChanged(object sender, EventArgs e)
        {
            boxVars.Checked = !boxContent.Checked;
            groupContent.Visible = boxContent.Checked;
            groupContentVar.Visible = !boxContent.Checked;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class Parameters
        {
            public List<string> Vars { get; set; } = new List<string>();
            public List<string> HtmlFiles { get; set; } = new List<string>();
            public List<string> HtmlFilesOutput { get; set; } = new List<string>();
            public List<string> ContentFile { get; set; } = new List<string>();
            public bool VarInFile { get; set; }
            public string OutputDir { get; set; }


            public Parameters()
            {

            }

            public Parameters(CheckedListBox Vars, CheckedListBox HtmlFiles, CheckedListBox ContentFile, CheckedListBox HtmlFilesOutput)
            {
                for (short i = 0; i < Vars.CheckedItems.Count; i++)
                {
                    this.Vars.Add(Vars.CheckedItems[i] as string);
                }

                for (short i = 0; i < HtmlFiles.CheckedItems.Count; i++)
                {
                    this.HtmlFiles.Add(HtmlFiles.CheckedItems[i] as string);
                }

                for (short i = 0; i < ContentFile.CheckedItems.Count; i++)
                {
                    this.ContentFile.Add(ContentFile.CheckedItems[i] as string);
                }

                for (short i = 0; i < HtmlFilesOutput.Items.Count; i++)
                {
                    if (HtmlFilesOutput.GetItemChecked(i))
                    {
                        if (this.HtmlFiles[i] != HtmlFilesOutput.Items[i] as string)
                            this.HtmlFilesOutput.Add(HtmlFilesOutput.Items[i] as string);
                        else
                            this.HtmlFilesOutput.Add(string.Empty);
                    }
                    else
                        this.HtmlFilesOutput.Add(string.Empty);
                }
            }

            public override string ToString()
            {
                return $"Replace {string.Join(";", Vars)} in {string.Join(" ; ", HtmlFiles)} {(VarInFile ? "by variables in" : "by content of")} {string.Join(" ; ", ContentFile)} - Output to {OutputDir}";
            }
        }

        private void btnContentVar_Click(object sender, EventArgs e)
        {
            openDialog.FilterIndex = 2;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                contentFiles.AddRange(openDialog.FileNames.Where(x => !contentFiles.Contains(x)));
                listContentVar.Items.Clear();
                listContentVar.Items.AddRange(contentFiles.ToArray());
                listContent.Items.Clear();
                listContent.Items.AddRange(contentFiles.ToArray());
            }
        }

        private void listContentVar_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Check file exists
                    if (File.Exists(fileLoc) && !contentFiles.Contains(fileLoc))
                    {
                        contentFiles.Add(fileLoc);
                        listContent.Items.Clear();
                        listContent.Items.AddRange(contentFiles.ToArray());
                        listContentVar.Items.Clear();
                        listContentVar.Items.AddRange(contentFiles.ToArray());
                    }
                }
            }
        }

        private void boxRename_CheckedChanged(object sender, EventArgs e)
        {
            groupName.Enabled = boxRename.Checked;
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            if (listHtml2.CheckedItems.Count == 0)
                statusStripText.Text = "No name selected";
            else
            {
                for (short i = 0; i < htmlFilesOutput.Count; i++)
                {
                    if (listHtml2.GetItemChecked(i))
                    {
                        Dialog.DialogConfig dialogConfig = new Dialog.DialogConfig()
                        {
                            Message = "Enter a new name for : " + htmlFilesOutput.Values.ElementAt(i),
                            Title = "Enter filename",
                            DefaultInput = "",
                            Button1 = Dialog.ButtonType.Skip,
                            Button2 = Dialog.ButtonType.OK,
                            Button3 = Dialog.ButtonType.Cancel,
                        };
                        var result = Dialog.ShowDialog(dialogConfig);

                        if (result.DialogResult == Dialog.DialogResult.OK)
                        {
                            htmlFilesOutput[htmlFilesOutput.ElementAt(i).Key] = result.UserInput;
                        }
                        else if (result.DialogResult == Dialog.DialogResult.Cancel)
                            i = (short)htmlFilesOutput.Count;
                    }
                }

                listHtml2.Items.Clear();
                listHtml2.Items.AddRange(htmlFilesOutput.Values.ToArray());
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                updateChecker.FileUrl = "http://www.esseivan.ch/files/softwares/webeditor/version.xml";
                updateChecker.CheckUpdates();
                var result = updateChecker.Result;
                if (result.NeedUpdate)
                    if (System.Windows.Forms.MessageBox.Show($"Update is available, do you want to visit the website ?\nCurrent : {result.CurrentVersion}\nLast : {result.LastVersion}", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        result.OpenUpdateWebsite();
                    }
            }
            catch
            {

            }
        }
    }
}
