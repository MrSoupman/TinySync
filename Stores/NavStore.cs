using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.ViewModel;

namespace TinySync.Stores
{
    public class NavStore
    {
        private BaseViewModel _CurrentVM;
        public BaseViewModel CurrentVM
        {
            get => _CurrentVM;
            set
            {
                _CurrentVM = value;
                OnCurrentViewModelChanged();
            }

        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;
    }
}
