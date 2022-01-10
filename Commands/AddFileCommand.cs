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
    public class AddFileCommand : BaseCommand
    {
        private readonly FileChooseViewModel FCVM;
        private readonly List<Metadata> data;
        private readonly NavigationSvc homeViewNavigationSvc;

        public override bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(FCVM.Origin) && !string.IsNullOrEmpty(FCVM.Remote) && base.CanExecute(parameter))
                return true;
            return false;

        }

        public override void Execute(object parameter)
        {
            Regex reg = new Regex("[^\"]+[^\", ]"); //We add files in the format of '"file.ext",'. This regex extracts the string between the quotes and comma
            //List<string> files = new List<string>();
            string param = FCVM.Origin;
            var matches = reg.Matches(param);
            if (Directory.Exists(FCVM.Remote)) //checks on the off chance the remote directory doesn't exist at this point
            {
                foreach (var match in matches)
                {
                    string file = match.ToString();
                    if (File.Exists(file))
                    {
                        string remote = FCVM.Remote + "\\" + Path.GetFileName(file);
                        if (!file.Equals(remote))
                        {
                            Metadata temp = new Metadata(file, remote);
                            if (!data.Contains(temp)) //check that we're not already set to sync to this directory
                            {
                                data.Add(temp);
                                //TODO: Replace
                                MessageBox.Show("Successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                homeViewNavigationSvc.Navigate();
                            }
                            else
                            {
                                //TODO: Replace
                                MessageBox.Show("File already added for syncing to this directory", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            //TODO: Replace
                            MessageBox.Show("Original path and sync path are the same.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        //TODO: Replace
                        MessageBox.Show(file + " does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                //TODO: Replace
                MessageBox.Show(FCVM.Remote + " does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }

        public AddFileCommand(FileChooseViewModel FCVM, List<Metadata> data, NavigationSvc homeViewNavSvc)
        {
            homeViewNavigationSvc = homeViewNavSvc;
            this.FCVM = FCVM;
            this.data = data;
            this.FCVM.PropertyChanged += FCVM_PropertyChanged;
        }

        private void FCVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FileChooseViewModel.Origin) || e.PropertyName == nameof(FileChooseViewModel.Remote))
                OnCanExecutedChanged();
        }

    }
}
