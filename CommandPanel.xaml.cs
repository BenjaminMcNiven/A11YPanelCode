﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using ReportGenerators;
using System.Management.Automation;
using Microsoft.WindowsAPICodePack.Dialogs;
using My.AsyncObservCollect;
using My.WPFControlWriter;
using My.CanvasApi;

namespace WPFCommandPanel
{
    /// <summary>
    /// Interaction logic for CommandPanel.xaml
    /// </summary>
    public partial class CommandPanel : Page
    {
        //Will watch the folder that will contain the reports
        public FileSystemWatcher FileWatcher;
        //Collection of FileDisplay objects that will be displayed in the panel
        public ObservableCollection<FileDisplay> file_paths = new AsyncObservableCollection<FileDisplay>();
        //Allows the program to control a single browser through multiple events and commands
        //public ChromeDriver chrome;
        public FirefoxDriver chrome;
        public WebDriverWait wait;
        //Flag to quit a given opperation. Should add checks for it in various places so it can jsut end the event or funciton.
        public bool QuitThread = false;

        public class PageReviewer
        {   //Object to review the current webpage for the user and hold the data. Is reset whenever they click the CreateReport button.
            public PageReviewer()
            {
                A11yReviewer = new A11yParser();
                MediaReviewer = new MediaParser();
                LinkReviewer = new LinkParser("None");
                DocumentReviewer = new DocumentParser();
            }
            public A11yParser A11yReviewer { get; set; }
            public MediaParser MediaReviewer { get; set; }
            public LinkParser LinkReviewer { get; set; }
            public DocumentParser DocumentReviewer { get; set; }
        }
        public PageReviewer PageParser;
        //Class to work best with the Listbox and FileSystemWatcher together.


        //Container for file info to be displayed
        public class FileDisplay
        {
            public FileDisplay(string path)
            {
                DisplayName = path.Split('\\').Last();
                FullName = path;
            }
            public string DisplayName { get; set; }
            public string FullName { get; set; }

        }

        //Init
        public CommandPanel()
        {
            InitializeComponent();
            //Set all console output to our own writer
            //Console.SetOut(new ControlWriter(TerminalOutput));
            FileWatcher = new FileSystemWatcher(MainWindow.panelOptions.ReportPath);
            //Setup the events for the filewatcher
            this.FileWatcher.EnableRaisingEvents = true;
            this.FileWatcher.Created += new System.IO.FileSystemEventHandler(this.FileWatcher_Created);
            this.FileWatcher.Deleted += new System.IO.FileSystemEventHandler(this.FileWatcher_Deleted);
            this.FileWatcher.Renamed += new System.IO.RenamedEventHandler(this.FileWatcher_Renamed);
            //Init the listbox / file_paths container
            foreach (var d in new DirectoryInfo(FileWatcher.Path).GetFiles("*.xlsx"))
            {
                file_paths.Add(new FileDisplay(d.FullName));
            }
            //Setup the listbox
            ReportList.ItemsSource = file_paths;
            ReportList.DisplayMemberPath = "DisplayName";
            ReportList.SelectedValuePath = "FullName";
            //try
            //{
            //    HighScoreBox.Text = "HighScore: " + File.ReadAllText(MainWindow.panelOptions.HighScorePath);
            //}
            //catch
            //{
            //    Dispatcher.Invoke(() =>
            //    {
            //        Run run = new Run("Error loading HighScore ...\n")
            //        {
            //            Foreground = System.Windows.Media.Brushes.Red
            //        };
            //        TerminalOutput.Inlines.Add(run);
            //    });
            //}
            //PageParser = new PageReviewer();

        }

        private void ChangeDomain(object sender, SelectionChangedEventArgs e)
        {
            if (Domain.SelectedItem != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)Domain.SelectedItem;
                string selectedText = cbi.Content.ToString();
                CanvasApi.ChangeDomain(selectedText);
            }
        }


        private void OpenReportList_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MainWindow.panelOptions.ReportPath);
        }


        private void ReportList_DoubleClick(object sender, EventArgs e)
        {
            if (new FileInfo(ReportList.SelectedValue.ToString()).Exists)
            {
                System.Diagnostics.Process.Start(ReportList.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("File no longer exists");
            }
        }

        private void mReportsButton_Click(object sender, EventArgs e)
        {
            //Insert code for mReports. May be easiest to create a POSH terminal and just copy paste the script over
            using (PowerShell posh = PowerShell.Create())
            {
                //I was to lazy to rewrite the function into c# and just import the POSH script.
                string script = File.ReadAllText(MainWindow.panelOptions.PowershellScriptDir + "\\MoveReports.ps1");
                script = "param($ReportPath, $BaseDestination, $N_DriveBase, $email)process{\n" + script + "\n}";
                posh.AddScript(script)
                    .AddArgument(MainWindow.panelOptions.ReportPath)
                    .AddArgument(MainWindow.panelOptions.BaseExcelArchive)
                    .AddArgument(MainWindow.panelOptions.BaseMoveReportsDir)
                    .AddArgument(MainWindow.panelOptions.A11yEmail);
                Collection<PSObject> results = posh.Invoke();
                foreach (var obj in results)
                {
                    Run run = new Run("Report:\n")
                    {
                        Foreground = System.Windows.Media.Brushes.Cyan
                    };
                    //TerminalOutput.Inlines.Add(run);
                    //TerminalOutput.Inlines.Add(obj.ToString().Remove(0,2).Replace("; ", "\n").Replace("=", ": ").Replace("}", "") + "\n");
                }
            }
        }

        private void FindReplace_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog prompt = new CommonOpenFileDialog();

            prompt.IsFolderPicker = true;
            var file_path = "";
            if (prompt.ShowDialog() == CommonFileDialogResult.Ok)
            {
                file_path = prompt.FileName;
            }
            else
            {
                return;
            }
            var script = File.ReadAllText(MainWindow.panelOptions.PowershellScriptDir + @"\FindReplace.ps1");
            script = "param($path)process{\n" + script + "\n}";
            var posh = PowerShell.Create();
            posh.AddScript(script).AddArgument(file_path).AddArgument(MainWindow.panelOptions.CourseBackupDir);
            posh.Invoke();

            Dispatcher.Invoke(() =>
            {
                Run run = new Run($"Find Replace on {file_path} finished.\nBack up can be found at {MainWindow.panelOptions.CourseBackupDir}")
                {
                    Foreground = System.Windows.Media.Brushes.Cyan
                };
                //TerminalOutput.Inlines.Add(run);
            });
        }

        private void Archive_Reports_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo reports = new DirectoryInfo(MainWindow.panelOptions.ReportPath);
            string currentDate =
                DateTime.Now.Year.ToString() +
                "-" +
                DateTime.Now.Month.ToString() +
                "-" +
                DateTime.Now.Day.ToString();
            string newDirPath = $"{MainWindow.panelOptions.ReportPath}\\{currentDate}";
            DirectoryInfo newDirectory;
            if (!Directory.Exists(newDirPath))
            {
                newDirectory = Directory.CreateDirectory(newDirPath);
            }
            else
            {
                newDirectory = new DirectoryInfo(newDirPath);
            }
            FileInfo[] reportFiles = reports.GetFiles();
            foreach (FileInfo file in reportFiles)
            {
                string newFileName = Path.GetFileNameWithoutExtension(file.Name);
                string baseFileName = newFileName;
                bool fileHasNotMoved = true;
                int i = 2;
                while (fileHasNotMoved)
                {
                    try
                    {
                        file.MoveTo($"{newDirPath}\\{newFileName}.xlsx");
                        fileHasNotMoved = false;
                    }
                    catch
                    {
                        newFileName = baseFileName + "_V" + i;
                        i++;
                    }
                }
            }
        }

        private void Accessibility_Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://byuceaccessibility.github.io/");
        }

        private void UpdateCurrentProgress_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://byu.app.box.com/file/854963586136?s=jnyv07ojiqb234l3ywxxhs7r7m5nk2nf&utm_campaign=collab%20auto%20accept%20user&utm_medium=email&utm_source=trans");
        }
    }
}
