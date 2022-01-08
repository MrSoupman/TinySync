using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TinySync.View
{
    /// <summary>
    /// Interaction logic for ModifyView.xaml
    /// </summary>
    public partial class ModifyView : UserControl
    {
        public ModifyView()
        {
            InitializeComponent();
        }

        private void AddDirectory(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult res = dialog.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK && dialog.SelectedPath.Length > 0)
                {
                    _Remote.Text = dialog.SelectedPath;
                }

            }

        }
    }
}
