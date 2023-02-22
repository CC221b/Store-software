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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker simulatorWorker;
        BackgroundWorker timerWorker;
        private Stopwatch stopWatch;
        private bool isTimerRun;
        bool flagClose = true;

        public SimulatorWindow()
        {
            InitializeComponent();
            simulatorWorker = new BackgroundWorker();
            timerWorker = new BackgroundWorker();
            simulatorWorker.DoWork += WorkerSimulator_DoWork;
            simulatorWorker.WorkerReportsProgress = true;
            simulatorWorker.RunWorkerAsync();
            stopWatch = new Stopwatch();
            timerWorker.DoWork += WorkerTimer_DoWork;
            timerWorker.ProgressChanged += WorkerTimer_ProgressChangedTimer;
            timerWorker.WorkerReportsProgress = true;
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;
                timerWorker.RunWorkerAsync();
            }
        }

        private void WorkerSimulator_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.SimulatorStart();
            Simulator.StatusChangedEvent += StatusChanged;
            Simulator.EndSimulatorEvent += EndSimulator;
            if (!Dispatcher.Thread.IsAlive) { e.Cancel = false; }
        }
        public void StatusChanged(BO.Order? order, string newStatus, DateTime prev, DateTime next)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtsim.Text = $"The result for this order: " + order?.ID.ToString() + "\n" +
                $"Previous status: " + order?.Status.ToString() + "\n" +
                $"Current status: " + newStatus + "\n" +
                    $"begin to change at: " + $" {prev} \n" +
                    $"now:  {next}  ";
            });
        }
        private void StopSimulator_Click(object sender, RoutedEventArgs e)
        {
            Simulator.SimulatorStop();
            flagClose = false;
            Close();    
        }

        public void EndSimulator(DateTime end, string reasonStop)
        {
            this.Dispatcher.Invoke(() =>
            {
                isTimerRun = false;
                if (reasonStop != "")
                {
                    MessageBox.Show("Finishing the simulator: " + end.ToString() + "\n" + "Becouse: " + reasonStop);
                }       
            }); 
        }

        private void WorkerTimer_ProgressChangedTimer(object? sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
        }
        private void WorkerTimer_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = flagClose;
        }
    }
}
