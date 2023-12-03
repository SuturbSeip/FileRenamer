using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace FileRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        List<string> files = new List<string>();
        private ObservableCollection<FileInfo> fileNames = new ObservableCollection<FileInfo>();

        public ObservableCollection<FileInfo> FileNames
        {
            get { return fileNames; }
            set
            {
                if (fileNames != value)
                {
                    fileNames = value;
                    OnPropertyChanged(nameof(FileNames));
                }
            }
        }

        private ObservableCollection<ExtensionFilters> extensions = new ObservableCollection<ExtensionFilters>()
        {
            new ExtensionFilters(".xlsx"),
            new ExtensionFilters(".test"),
        };

        public ObservableCollection<ExtensionFilters> Extensions
        {
            get { return extensions; }
            set
            {

            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            DataContext = this;
            folderPath.Text = "C:\\Users\\adria\\Desktop\\RenamingProgramTester";
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            filesList.ItemsSource = fileNames;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            files.Clear();
            fileNames.Clear();
            var fileArray = Directory.GetFiles(folderPath.Text.ToString());
            foreach (var file in fileArray) { files.Add(file); }
            foreach (var file in files)
            {
                var path = System.IO.Path.GetDirectoryName(file);
                var fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                var fileExtension = System.IO.Path.GetExtension(file);
                fileNames.Add(new FileInfo(path, fileName, fileExtension));
            }
        }

        private void newNames_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {

            var test = e.Cancel;
            var editedTextBox = e.EditingElement as TextBox;
            var editedItemIndex = e.Row.GetIndex();
            string editedValue = editedTextBox.Text;

            fileNames[editedItemIndex].NewName = editedValue;

        }

        private void copyNamesButton_Click(object sender, RoutedEventArgs e)
        {
            var itemSource = filesList.ItemsSource as IEnumerable<FileInfo>;
            if (itemSource != null)
            {
                foreach (var item in itemSource)
                {
                    item.NewName = item.FileName;
                }
                //FileNames = new ObservableCollection<FileInfo>(itemSource);
                filesList.Items.Refresh();
            }
        }

        private void renameButton_Click(object sender, RoutedEventArgs e)
        {
            var itemSource = filesList.ItemsSource as IEnumerable<FileInfo>;
            foreach (var item in itemSource)
            {
                if (item.Selected && !string.IsNullOrEmpty(item.NewName.ToString()))
                {
                    var path = System.IO.Path.GetDirectoryName(files[0]);
                    File.Move($"{item.Path}\\{item.FileName}{item.Extension}", $"{item.Path}\\{item.NewName}{item.Extension}");
                    //File.Move(files[i], $"{path}//{fileNames[i].NewName}{fileNames[i].Extension}");
                }
            }

        }

        //private void isSelectedBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    CheckBox? checkBox = sender as CheckBox;
        //    if (checkBox.IsChecked==true)
        //    {

        //    }
        //}
    }
}
