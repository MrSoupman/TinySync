using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            //TODO: Change
            var res = MessageBox.Show("Are you sure you want to remove this file from the list?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (res.Equals(MessageBoxResult.OK))
            {
                data.RemoveAt(HVM.SelectedIndex);
                HVM.UpdateMetalist();
            }
            
            
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
