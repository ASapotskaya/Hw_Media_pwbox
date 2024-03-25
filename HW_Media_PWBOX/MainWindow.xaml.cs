using Microsoft.Win32;
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
using System.Windows.Threading;

namespace HW_Media_PWBOX
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

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.Filter = "Media Files (*.*)|*.*";
            ofd.ShowDialog();
            try
            {
                if (ofd.ShowDialog(this) == true)
                {
                    foreach (String file in ofd.FileNames)
                    {
                        mediaelement.Source = new Uri(ofd.FileName);
                        LV1.Items.Add(mediaelement.Source);
                    }
                }

            }
            catch (Exception)
            {
                new NullReferenceException("Error");
            }
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += new EventHandler(timer_Tick);
            dt.Interval = new TimeSpan(0, 0, 1); //часы минуты секунды 
            dt.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            slider_video.Value = mediaelement.Position.TotalSeconds;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaelement.Pause();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
           
                mediaelement.Source = new Uri(LV1.SelectedItem.ToString());
                mediaelement.Play();

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaelement.Stop();
        }

        private void slider_video_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            mediaelement.Position = ts;
        }

        private void slider_audio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaelement.Volume = slider_audio.Value;
        }

        private void mediaelement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaelement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(mediaelement.NaturalDuration.TimeSpan.TotalMilliseconds);
                slider_video.Maximum = ts.TotalSeconds;
            }
        }

        private void bdelete_Click(object sender, RoutedEventArgs e)
        {
            LV1.Items.RemoveAt(LV1.SelectedIndex);
        }
    }
}

