using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TinySync.Services;
using TinySync.Commands;

namespace TinySync.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {

        /// <summary>
        /// data is used to store the directory/file to be synced, and its metadata. 
        /// </summary>
        public ObservableCollection<MetadataViewModel> data;
        public IEnumerable<MetadataViewModel> MetaList => data;
        private List<Metadata> dataList;
        
        
        private bool _ButtonsEnabled = true;
        public bool ButtonsEnabled
        {
            get
            {
                return _ButtonsEnabled;
            }
            set
            {
                _ButtonsEnabled = value;
                OnPropertyChanged(nameof(ButtonsEnabled));
            }
        }


        private int _SelectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                _SelectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        private string _Status;
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private int _Progress;
        public int Progress
        {
            get
            {
                return _Progress;
            }
            set
            {
                _Progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }


        public ICommand AddFileMenu { get; }
        public ICommand AddFolderMenu { get; }
        public ICommand Remove { get; }
        public ICommand ModifyMenu { get; }
        public ICommand Sync { get; }

        public HomeViewModel(List<Metadata> dataList, NavigationSvc FileChooseViewNavSvc, NavigationSvc DirectoryChooseViewNavSvc)
        {
            _SelectedIndex = -1;
            Progress = 0;
            Status = "Waiting...";
            data = new ObservableCollection<MetadataViewModel>();
            AddFileMenu = new NavigateCommand(FileChooseViewNavSvc);
            AddFolderMenu = new NavigateCommand(DirectoryChooseViewNavSvc);
            Remove = new RemoveFileCommand(this, dataList);
            ModifyMenu = new NavigateModifyCommand(this, dataList,FileChooseViewNavSvc.GetNavStore());
            Sync = new SyncCommand(this);
            this.dataList = dataList;
            UpdateMetalist();
        }

        public void UpdateMetalist()
        {
            data.Clear();
            foreach (Metadata meta in dataList)
                data.Add(new MetadataViewModel(meta));
            JsonSvc.SaveJson(dataList);
        }

        public List<Metadata> GetMetadatas()
        {
            return dataList;
        }

        public void DisableButtons()
        {
            ButtonsEnabled = false;
        }
        public void EnableButtons()
        {
            ButtonsEnabled = true;
        }

    }
}
