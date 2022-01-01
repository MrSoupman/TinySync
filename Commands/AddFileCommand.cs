using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TinySync.Model;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class AddFileCommand : BaseCommand
    {
        private readonly FileChooseViewModel FCVM;
        private readonly List<Metadata> data;
        public override bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(FCVM.Origin) && !string.IsNullOrEmpty(FCVM.Remote) && base.CanExecute(parameter))
                return true;
            return false;

        }

        public override void Execute(object parameter)
        {
            Regex reg = new Regex("[^\"]+[^\", ]");
            //List<string> files = new List<string>();
            string param = FCVM.Origin;
            var matches = reg.Matches(param);
            if (Directory.Exists(FCVM.Remote))
            {
                foreach (var match in matches)
                {
                    string file = match.ToString();
                    if (File.Exists(file))
                    {
                        Metadata temp = new Metadata(file, FCVM.Remote);
                        if (!data.Contains(temp))
                        {
                            data.Add(temp);
                            //TODO: Replace
                            MessageBox.Show("Successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public AddFileCommand(FileChooseViewModel FCVM, List<Metadata> data)
        {
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
