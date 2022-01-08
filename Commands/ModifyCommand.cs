using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinySync.Model;
using TinySync.Services;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class ModifyCommand : BaseCommand
    {
        private ModifyViewModel MVM;
        private NavigationSvc homeViewNavSvc;
        private int index;
        private List<Metadata> metadata;

        public override void Execute(object parameter)
        {
            metadata[index].Remote = MVM.Remote + "\\" + Path.GetFileName(metadata[index].Origin);
            //TODO: Change
            MessageBox.Show("File modified.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            homeViewNavSvc.Navigate();
        }

        public override bool CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(MVM.Remote))
                return false;
            return true;
        }

        public ModifyCommand(ModifyViewModel MVM, int index, List<Metadata> metadata, NavigationSvc homeViewNavSvc)
        {
            this.MVM = MVM;
            this.homeViewNavSvc = homeViewNavSvc;
            this.index = index;
            this.metadata = metadata;
            this.MVM.PropertyChanged += MVM_PropertyChanged;
        }

        private void MVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ModifyViewModel.Remote))
                OnCanExecutedChanged();
        }
    }
}
