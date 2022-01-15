using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TinySync.Model;
using TinySync.Services;
using TinySync.ViewModel;

namespace TinySync.Commands
{
    public class SyncCommand : BaseCommand
    {
        private List<Metadata> metadatas;
        BackgroundWorker worker = new BackgroundWorker();
        private HomeViewModel HVM;
        public override void Execute(object parameter)
        {
            //TODO: Change
            var res = MessageBox.Show("Sync files?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res.Equals(MessageBoxResult.Yes))
            {
                HVM.DisableButtons();
                worker.RunWorkerAsync();
                HVM.Status = "Syncing...";
            }
        }


        public SyncCommand(HomeViewModel HVM)
        {
            this.HVM = HVM;
            
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            HVM.Status = "Finished!";
            HVM.EnableButtons();
            HVM.UpdateMetalist();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            HVM.Progress = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 0, progress = 0;
            metadatas = HVM.GetMetadatas();
            foreach (Metadata metadata in metadatas)
            {
                SyncSvc.Sync(metadata);
                count++;
                progress = Convert.ToInt32(((float)count / (float)metadatas.Count) * 100);
                (sender as BackgroundWorker).ReportProgress(progress);
                Thread.Sleep(1000);
            }
        }
    }
}
