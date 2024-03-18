using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan countdownTime;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            btn_Stop.IsEnabled = false;
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSpan.TryParse(txt_Timer.Text, out TimeSpan timeout) == false)
            {
                lbl_Warning.Content = "Time format must be 'hh:mm:ss'";
                lbl_Warning.Foreground = Brushes.Red;
            }
            else
            {
                lbl_Warning.Content = "";
                txt_Timer.IsEnabled = false;
                btn_Stop.IsEnabled = true;
                btn_Start.IsEnabled = false;
                countdownTime = TimeSpan.Parse(txt_Timer.Text);
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += Timer_Tick;
                timer.Start();
            }

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (countdownTime.TotalSeconds >= 0)
            {
                if (countdownTime.TotalSeconds <= 10)
                {
                    if (countdownTime.TotalSeconds % 2 == 0)
                    {
                        txt_Timer.Foreground = Brushes.Red;
                    }
                    else
                    {
                        txt_Timer.Foreground = Brushes.Black;
                    }
                    txt_Timer.Text = string.Format("{0:hh\\:mm\\:ss}", countdownTime);
                }
                else
                {
                    txt_Timer.Text = string.Format("{0:hh\\:mm\\:ss}", countdownTime);
                }
                countdownTime = countdownTime.Add(new TimeSpan(0, 0, -1));
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Time's up!");
                txt_Timer.IsEnabled = true;
                txt_Timer.Foreground = Brushes.Black;
                btn_Stop.IsEnabled = false;
                btn_Start.IsEnabled = true;
            }
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            txt_Timer.IsEnabled = true;
            txt_Timer.Foreground = Brushes.Black;
            btn_Stop.IsEnabled = false;
            btn_Start.IsEnabled = true;
        }
    }
}