using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class DebugCommand : BaseCommand
    {
        private HomeViewModel HVM;
        public override void Execute(object parameter)
        {
            HVM.ButtonsEnabled = !HVM.ButtonsEnabled;
        }
        public DebugCommand(HomeViewModel HVM)
        {
            this.HVM = HVM;
        }
    }
}
