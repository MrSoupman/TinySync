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
    public class ModifyViewModel : BaseViewModel
    {
        private string _Meta;
        public string Meta
        {
            get
            {
                return _Meta;
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
        public ICommand CancelCommand { get; }
        public ICommand Modify { get; }
        public ModifyViewModel(int index, List<Metadata> metadata, NavigationSvc homeViewNavSvc)
        {
            CancelCommand = new NavigateCommand(homeViewNavSvc);
            _Meta = metadata[index].Origin;
        }
    }
}
