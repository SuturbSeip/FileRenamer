using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileRenamer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private string _folderPath = "C:\\Users\\adria\\Desktop\\RenamingProgramTester";
        private List<string> _files = new List<string>();
        private ObservableCollection<FileInfo> _fileNames = new ObservableCollection<FileInfo>();
        private ObservableCollection<ExtensionFilters> _extensions = new ObservableCollection<ExtensionFilters>();
        #endregion

        #region Public Properties
        public string FolderPath
        {
            get { return _folderPath; }
            set { _folderPath = value; }
        }
        
        public ObservableCollection<FileInfo> FileNames
        {
            get { return _fileNames; }
            set
            {
                if (_fileNames != value)
                {
                    _fileNames = value;
                    OnPropertyChanged(nameof(FileNames));
                }
            }
        }

        public ObservableCollection<ExtensionFilters> Extensions
        {
            get{ return _extensions; }
            set
            {
                if (_extensions != value)
                {
                    _extensions = value;
                    OnPropertyChanged(nameof(Extensions));
                    UpdateFilesNamesList();
                }
            }
        }

        #endregion

        #region Private Helper Methods
        private void UpdateFilesNamesList()
        {
            _fileNames.Clear();
            foreach (var file in _files)
            {
                var path = System.IO.Path.GetDirectoryName(file);
                var fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                var fileExtension = System.IO.Path.GetExtension(file);
                //Find a better way to do it.
                foreach (var extension in Extensions)
                {
                    if (extension.ExtensionOfFile.Equals(fileExtension) && extension.IsActive)
                        _fileNames.Add(new FileInfo(path, fileName, fileExtension));
                }
            }
        }

        private void LoadFilesAndExtensionsList()
        {
            _files.Clear();
            var fileArray = Directory.GetFiles(_folderPath);
            foreach (var file in fileArray) { _files.Add(file); }

            foreach (var file in _files)
            {
                var fileExtension = System.IO.Path.GetExtension(file);
                var extensionFilter = new ExtensionFilters(fileExtension);
                if (!Extensions.Any(element => element.ExtensionOfFile == extensionFilter.ExtensionOfFile))
                {
                    Extensions.Add(extensionFilter);
                }
            }
            UpdateFilesNamesList();
        }



        #endregion

        #region Commands

        private ICommand? browseCommand;

        public ICommand Browse
        {
            get
            {
                if (browseCommand == null) browseCommand = new RelayCommand(
                    argument => { LoadFilesAndExtensionsList(); });
                return browseCommand;
            }
        }
            #endregion

    }
}
