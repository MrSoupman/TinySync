using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Model;
using TinySync.Services;
using TinySync.Stores;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class NavigateModifyCommand : BaseCommand
    {
        private readonly HomeViewModel HVM;
        private NavStore nav;
        private List<Metadata> data;
        public override void Execute(object parameter)
        {
            nav.CurrentVM = new ModifyViewModel(HVM.SelectedIndex, data, new NavigationSvc(nav, CreateHomeViewModel)); //changes the view(model) to the modify view
        }

        public override bool CanExecute(object parameter)
        {
            return HVM.SelectedIndex > -1; //checks if user has selected an item
        }

        public NavigateModifyCommand(HomeViewModel HVM, List<Metadata> data, NavStore nav)
        {
            this.HVM = HVM;
            this.data = data;
            this.nav = nav;
            this.HVM.PropertyChanged += HVM_PropertyChanged;
        }

        private void HVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HomeViewModel.SelectedIndex))
                OnCanExecutedChanged();
        }

        private FileChooseViewModel CreateFileChooseViewModel()
        {
            return new FileChooseViewModel(data, new NavigationSvc(nav, CreateHomeViewModel));
        }
        private HomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel(data, new NavigationSvc(nav, CreateFileChooseViewModel));
        }
    }
}
