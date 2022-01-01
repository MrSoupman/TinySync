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
        private List<Metadata> data;
        private readonly NavStore nav;
        protected override void OnStartup(StartupEventArgs e)
        {
            data = JsonSvc.LoadJson();
            nav.CurrentVM = new FileChooseViewModel(data, new NavigationSvc(nav, CreateHomeViewModel));
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

        private FileChooseViewModel CreateFileChooseViewModel()
        {
            return new FileChooseViewModel(data,new NavigationSvc(nav, CreateHomeViewModel));
        }
        private HomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel(data);
        }

    }
}
