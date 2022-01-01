using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Stores;

namespace TinySync.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavStore nav;
        public BaseViewModel CurrentVM => nav.CurrentVM;

        public MainViewModel(NavStore nav)
        {
            this.nav = nav;
            nav.CurrentViewModelChanged += Nav_CurrentViewModelChanged;
        }

        private void Nav_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
        }
    }
}
