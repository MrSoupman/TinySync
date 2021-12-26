using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinySync.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentVM { get; }

        public MainViewModel()
        {
            CurrentVM = new HomeViewModel();
        }
    }
}
