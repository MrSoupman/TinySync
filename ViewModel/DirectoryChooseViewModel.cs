using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TinySync.Commands;
using TinySync.Model;
using TinySync.Services;

namespace TinySync.ViewModel
{
    public class DirectoryChooseViewModel : BaseViewModel
    {
        private string _origin;
        public string Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
                OnPropertyChanged(nameof(Origin));
            }
        }

        private string _remote;
        public string Remote
        {
            get
            {
                return _remote;
            }
            set
            {
                _remote = value;
                OnPropertyChanged(nameof(Remote));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }
        public DirectoryChooseViewModel(List<Metadata> data, NavigationSvc homeViewNavSvc)
        {
            AddCommand = new AddDirectoryCommand(this, data, homeViewNavSvc);
            CancelCommand = new NavigateCommand(homeViewNavSvc);
        }
    }
}
