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
using System.Diagnostics;

namespace Rewind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }       

        private void Start_Rewind(object sender, RoutedEventArgs e)
        {
            TextOutput.Text += String.Format("Starting countermeasures ...\n");

            ComboBoxItem typeItem = (ComboBoxItem)ComboTimeFrame.SelectedItem;
            var TimeFrame = typeItem.Content.ToString();
            TextOutput.Text += String.Format("Rewinding the last {0} minutes ...\n", TimeFrame[0]);
            int NumMinutes = Int32.Parse(TimeFrame[0].ToString());

            // ##################################################################################################################
            // Process Checks and Kills -----------------------------------------------------------------------------------------
            TextOutput.Text += String.Format("=== Process Checks\n");
            // Exceptions
            List<string> exceptions = new List<string>(new string[] { "Rewind", "msvsmon", "ScriptedSandbox64" });
            var allProcesses = Process.GetProcesses();
            var numberKilled = 0;
            foreach (Process proc in allProcesses)
            {
                try
                {
                    //TextOutput.Text += String.Format("Process: {0} ID: {1} Started: {2}\n", theprocess.ProcessName, theprocess.Id, theprocess.StartTime);
                    // Kill all processes younger than 5 minutes and accessible in this user context
                    if (proc.StartTime.AddMinutes(NumMinutes) > DateTime.Now)
                    {
                        if (!exceptions.Any(proc.ProcessName.Contains)) {
                            proc.Kill(true);
                            if (proc.HasExited)
                            {
                                TextOutput.Text += String.Format("Killing process: {0} ID: {1} Started: {2}\n", proc.ProcessName, proc.Id, proc.StartTime);
                            } else
                            {
                                TextOutput.Text += String.Format("Killing process FAILED: {0} ID: {1} Started: {2}\n", proc.ProcessName, proc.Id, proc.StartTime);
                            }
                            numberKilled += 1;
                        }
                    }
                } 
                catch (System.ComponentModel.Win32Exception)
                {
                    // pass
                }
            }
            // not a single process killed
            if (numberKilled == 0)
            {
                TextOutput.Text += String.Format("Not a single process has been killed by Rewind\n");
            }


        }
    }
}
