using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EsercizioWpf
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private CancellationTokenSource token = new CancellationTokenSource();
        private CancellationTokenSource token2 = new CancellationTokenSource();
        private CancellationTokenSource token3 = new CancellationTokenSource();

        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {
            if (token == null)
                token = new CancellationTokenSource();

            Task.Factory.StartNew(() => Conteggio(token, 10000, lblContenuto1));
        }

        private void Conteggio(CancellationTokenSource token,int max, Label lbl)
        {
            for (int i=0;i<=max;i++)
            {
                Dispatcher.Invoke(()=>AggiornaUI(i,lbl));
                Thread.Sleep(1000);
                Dispatcher.Invoke(()=>AggiornaUI1(lbl));
                Thread.Sleep(1000);
                if (token.Token.IsCancellationRequested)
                    break;

            }
            Dispatcher.Invoke(() => AggiornaUI2(lbl));
        }
        private void AggiornaUI(int i, Label lbl)
        {
            lbl.Content = $"Sto contando caro...{ i.ToString()}";
        }
        private void AggiornaUI1( Label lbl)
        {
            lbl.Content = $"Attendere...";
        }
        private void AggiornaUI2(Label lbl)
        {
            lbl.Content = $"Ho finito fanciullo";
        }

        private void btnStop1_Click(object sender, RoutedEventArgs e)
        {
            if(token!=null)
            {
                token.Cancel();
                token = null;
            }
        }
        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
            int max = Convert.ToInt32(txtMax.Text);
            if (token2 == null)
                token2 = new CancellationTokenSource();

            Task.Factory.StartNew(() => Conteggio(token2, max, lblContenuto2));
        }

        private void btnStop2_Click(object sender, RoutedEventArgs e)
        {
            if (token2 != null)
            {
                token2.Cancel();
                token2 = null;
            }
        }

        private void btnStart3_Click(object sender, RoutedEventArgs e)
        {
            int max = Convert.ToInt32(txtMax2.Text);
            int delay = Convert.ToInt32(txtDelay.Text);
            if (token3 == null)
                token3 = new CancellationTokenSource();

            Task.Factory.StartNew(() => ConteggioDelay(token3, max,delay, lblContenuto3));
        }

        private void ConteggioDelay(CancellationTokenSource token, int max, int delay, Label lbl)
        {
            for (int i = 0; i <= max; i++)
            {
                Dispatcher.Invoke(() => AggiornaUI(i, lbl));
                Thread.Sleep(delay);
                Dispatcher.Invoke(() => AggiornaUI1(lbl));
                Thread.Sleep(delay);
                if (token.Token.IsCancellationRequested)
                    break;

            }
            Dispatcher.Invoke(() => AggiornaUI2(lbl));
        }

        private void btnStop3_Click(object sender, RoutedEventArgs e)
        {
            if (token3 != null)
            {
                token3.Cancel();
                token3 = null;
            }
        }

        private void btnStopTutti_Click(object sender, RoutedEventArgs e)
        {
            if (token != null && token2 != null && token3 != null)
            {
                token.Cancel();
                token = null;
                token2.Cancel();
                token2 = null;
                token3.Cancel();
                token3 = null;
            }
        }
    }
}
