using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Stores;
using TinySync.ViewModel;

namespace TinySync.Services
{
    public class NavigationSvc
    {
        private readonly NavStore nav;
        private readonly Func<BaseViewModel> createViewModel;
        public void Navigate()
        {
            nav.CurrentVM = createViewModel();
        }
        public NavigationSvc(NavStore nav, Func<BaseViewModel> createViewModel)
        {
            this.nav = nav;
            this.createViewModel = createViewModel;
        }
    }
}
