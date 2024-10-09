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
            // Open folder browser dialog
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Set the selected folder path in the TextBox
                FolderPathTextBox.Text = folderDialog.SelectedPath;
                PopulateSubfolders(folderDialog.SelectedPath);
            }
        }

        private void PopulateSubfolders(string folderPath)
        {
            // Clear existing items in ComboBox
            SubfolderComboBox.Items.Clear();

            // Get subdirectories
            var subfolders = Directory.GetDirectories(folderPath);
            foreach (var subfolder in subfolders)
            {
                SubfolderComboBox.Items.Add(Path.GetFileName(subfolder));
            }
        }

        private void SubfolderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Get selected subfolder
            if (SubfolderComboBox.SelectedItem != null)
            {
                string selectedSubfolder = SubfolderComboBox.SelectedItem.ToString();
                string folderPath = FolderPathTextBox.Text;

                // Initiate a separate process using the selected subfolder name
                StartProcess(selectedSubfolder, folderPath);
            }
        }

        private void StartProcess(string subfolderName, string parentFolderPath)
        {
            string subfolderPath = Path.Combine(parentFolderPath, subfolderName);

            if (Directory.Exists(subfolderPath))
            {
                // Start a process (e.g., open the subfolder in File Explorer)
                Process.Start("explorer.exe", subfolderPath);
            }
            else
            {
                MessageBox.Show("The selected subfolder does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
