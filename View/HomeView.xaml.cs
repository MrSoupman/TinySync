using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;


namespace TinySync.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            if (open.ShowDialog() == true)
            {
                FileTextBox.Text = open.FileName.ToString();
            }
        }
    }
}
