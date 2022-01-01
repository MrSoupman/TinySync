using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Services;

namespace TinySync.Commands
{
    public class NavigateCommand : BaseCommand
    {
        private readonly NavigationSvc NavSvc;

        public override void Execute(object parameter)
        {
            NavSvc.Navigate();
        }

        public NavigateCommand(NavigationSvc NavSvc)
        {
            this.NavSvc = NavSvc;
        }
    }
}
