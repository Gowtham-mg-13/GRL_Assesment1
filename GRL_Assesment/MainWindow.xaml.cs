using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;


namespace SubfolderSelector
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            
                FolderPathTextBox.Text = folderDialog.SelectedPath;
                PopulateSubfolders(folderDialog.SelectedPath);
            }
        }

        private void PopulateSubfolders(string folderPath)
        {
            
            SubfolderComboBox.Items.Clear();

            
            var subfolders = Directory.GetDirectories(folderPath);
            foreach (var subfolder in subfolders)
            {
                SubfolderComboBox.Items.Add(Path.GetFileName(subfolder));
            }
        }

        private void SubfolderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
    
            if (SubfolderComboBox.SelectedItem != null)
            {
                string selectedSubfolder = SubfolderComboBox.SelectedItem.ToString();
                string folderPath = FolderPathTextBox.Text;

                
                StartProcess(selectedSubfolder, folderPath);
            }
        }

        private void StartProcess(string subfolderName, string parentFolderPath)
        {
            string subfolderPath = Path.Combine(parentFolderPath, subfolderName);

            if (Directory.Exists(subfolderPath))
            {
            
                Process.Start("explorer.exe", subfolderPath);
            }
            else
            {
                MessageBox.Show("The selected subfolder does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
