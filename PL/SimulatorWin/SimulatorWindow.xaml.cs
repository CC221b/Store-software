using BlApi;
using SimulatorLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PL.SimulatorWin
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker Worker;
        private Stopwatch stopWatch;
        private bool isTimerRun;
        bool flagClose = true;
        BO.Order? order;

        public SimulatorWindow()
        {
            InitializeComponent();
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.RunWorkerAsync();
            stopWatch = new Stopwatch();
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;
            }
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (!Worker.CancellationPending)
            {
                Simulator.SimulatorStart();
                Simulator.StatusChangedEvent += StatusChanged;
                Simulator.EndSimulatorEvent += EndSimulator;
                if (!Dispatcher.Thread.IsAlive) { e.Cancel = false; }
                while (isTimerRun)
                {
                    Worker.ReportProgress(1);
                    Thread.Sleep(1000);
                }
            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            timerTextBlock.Text = timerText;
            if (order is null)
                lblStatus.Content = "Loading order for update...";
            else
                lblStatus.Content = "The order currently being processed: " + order.ID.ToString();
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.StatusChangedEvent -= StatusChanged;
            Simulator.EndSimulatorEvent -= EndSimulator;
            isTimerRun = false;
        }

        private void StopSimulator_Click(object? sender = null, RoutedEventArgs? e = null)
        {
            Simulator.SimulatorStop();
            flagClose = false;
            Worker.CancelAsync();
            Close();
        }

        public void EndSimulator(DateTime end, string reasonStop)
        {
            Dispatcher.Invoke(() =>
            {
                if (reasonStop != "")
                {
                    MessageBox.Show("Finishing the simulator: " + end.ToString() + "\n" + "Becouse: " + reasonStop);
                    StopSimulator_Click();
                }
            });
        }

        public void StatusChanged(BO.Order? order1, string newStatus, DateTime startChangeAt, DateTime endChangeAt)
        {
            order = order1;
            Dispatcher.Invoke(() =>
            {
                txtSimulator.Text = $"The result for this order: " + order?.ID.ToString() + "\n" +
                $"Previous status: " + order?.Status.ToString() + "\n" +
                $"Current status: " + newStatus + "\n" +
                    $"begin to change at: " + startChangeAt + "\n" +
                    $"end to change at: " + endChangeAt;
            });
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = flagClose;
        }
    }
}
