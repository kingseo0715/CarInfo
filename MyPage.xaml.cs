using CarInfoClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



//라이브차트를 사용하기 위한 using
using LiveCharts;
using LiveCharts.Wpf;
using System.Net;
using System.IO;
using System.Windows.Threading;
using LiveCharts.Defaults;
using LiveCharts.Configurations;


namespace CarInfoClient
{
    /// <summary>
    /// Interaction logic for MyPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyPage : Window
    {
        Socket client;
        Member mem = new Member();
        public MyPage(Socket obj, Member obj2)
        {
            client = obj;
            mem = obj2;
            InitializeComponent();

            Info_Car ic = new Info_Car();

            var rand = new Random();
            
            ic.Oil = rand.Next(50, 100);
            ic.Engine = rand.Next(50, 100);

            Gauge.Value = ic.Oil;
            Gauge_1.Value = ic.Engine;
        }

        private void btn_func_Click(object sender, RoutedEventArgs e)
        {
            Func_Fage ff = new Func_Fage(client, mem);
            Close();
            ff.Show();
        }
    }
}
