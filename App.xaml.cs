using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TinySync.Model;
using TinySync.Services;
using TinySync.Stores;
using TinySync.ViewModel;

namespace TinySync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<Metadata> data; //May want to change this so we aren't just being redundant
        private readonly NavStore nav;
        protected override void OnStartup(StartupEventArgs e)
        {
            data = JsonSvc.LoadJson();
            nav.CurrentVM = new HomeViewModel(data, new NavigationSvc(nav, CreateFileChooseViewModel), new NavigationSvc(nav, CreateDirectoryChooseViewModel));
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(nav)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        public App()
        {
            nav = new NavStore();
        }

        private DirectoryChooseViewModel CreateDirectoryChooseViewModel()
        {
            return new DirectoryChooseViewModel(data, new NavigationSvc(nav, CreateHomeViewModel));
        }

        private FileChooseViewModel CreateFileChooseViewModel()
        {
            return new FileChooseViewModel(data,new NavigationSvc(nav, CreateHomeViewModel));
        }
        private HomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel(data, new NavigationSvc(nav, CreateFileChooseViewModel), new NavigationSvc(nav, CreateDirectoryChooseViewModel));
        }

    }
}
