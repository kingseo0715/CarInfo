using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO.Ports;
using CarInfoClient.Model;
using System.Net.Sockets;

namespace CarInfoClient
{
    /// <summary>
    /// Func_Fage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Func_Fage : Window
    {
        Socket client;
        Member mem = new Member();
        //SerialPort port = new SerialPort("COM4", 9600); // 포트 및 속도 설정
        int num = 1;
        public Func_Fage(Socket obj, Member obj2)
        {
            client = obj;
            mem = obj2;

            InitializeComponent();


           // port.Open();
            this.Closed += Func_Fage_Closed;
            
        }

        private void headLight_Click(object sender, RoutedEventArgs e)
        {
            num++;
            //port.Write("1");
            //if (num == 3)
            //{
            //    port.Write("2");
            //    num--;
            //}



        }
        private void Func_Fage_Closed(object? sender, EventArgs e)
        {
            //if (port.IsOpen)
            //{
            //    port.Write("3");
            //    port.Close(); // 시리얼 포트 닫기
            //}
        }

        private void Media_btn_Click(object sender, RoutedEventArgs e)
        {
            Music_Page mp = new Music_Page(client, mem);
            Close();
            mp.Show();
        }

        private void Map_btn_Click(object sender, RoutedEventArgs e)
        {
            Map_Page mp = new Map_Page(client, mem);
            Close();
            mp.Show();

        }

        private void air_btn_Click(object sender, RoutedEventArgs e)
        {
            AirCon_Page ap = new AirCon_Page(client, mem);
            Close();
            ap.Show();
        }

        private void route_btn_Click(object sender, RoutedEventArgs e)
        {
            Route_Page rp = new Route_Page(client, mem);
            Close();
            rp.Show();
        }

        private void temperature_btn_Click(object sender, RoutedEventArgs e)
        {
            Temperature_Page tp = new Temperature_Page(client, mem);
            Close();
            tp.Show();
        }
    }
}
