using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TinySync.Model;
using TinySync.Services;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class AddDirectoryCommand : BaseCommand
    {
        private readonly DirectoryChooseViewModel DCVM;
        private readonly List<Metadata> data;
        private readonly NavigationSvc homeViewNavigationSvc;

        public override bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(DCVM.Origin) && !string.IsNullOrEmpty(DCVM.Remote))
                return true;
            return false;

        }

        public override void Execute(object parameter)
        {
            if (!DCVM.Origin.Equals(DCVM.Remote))
            {
                DirectoryMetadata DirectoryMeta = new DirectoryMetadata(DCVM.Origin, DCVM.Remote);
                if (!data.Contains(DirectoryMeta))
                {
                    data.Add(DirectoryMeta);
                    MessageBox.Show("Successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    homeViewNavigationSvc.Navigate();
                }
                else
                {
                    MessageBox.Show("Directory already added for syncing to this directory", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Directories are the same.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public AddDirectoryCommand(DirectoryChooseViewModel DCVM, List<Metadata> data, NavigationSvc homeViewNavSvc)
        {
            homeViewNavigationSvc = homeViewNavSvc;
            this.DCVM = DCVM;
            this.data = data;
            this.DCVM.PropertyChanged += DCVM_PropertyChanged;
        }

        private void DCVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DirectoryChooseViewModel.Origin) || e.PropertyName == nameof(DirectoryChooseViewModel.Remote))
                OnCanExecutedChanged();
        }

    }
}
