using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Model;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class RemoveFileCommand : BaseCommand
    {
        private readonly HomeViewModel HVM;
        private readonly List<Metadata> data;
        public override void Execute(object parameter)
        {
            data.RemoveAt(HVM.SelectedIndex);
            HVM.UpdateMetalist();
            
        }


        public override bool CanExecute(object parameter)
        {

            if (HVM.SelectedIndex >= 0)
                return true;
            return false;

        }

        public RemoveFileCommand(HomeViewModel HVM, List<Metadata> data)
        {
            this.HVM = HVM;
            this.data = data;
            this.HVM.PropertyChanged += HVM_PropertyChanged;
        }

        private void HVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HomeViewModel.SelectedIndex))
                OnCanExecutedChanged();
        }
    }
}
