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

namespace CarInfoClient
{
    /// <summary>
    /// Route_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Route_Page : Window
    {
        Socket client;
        Member mem = new Member();
        public Route_Page(Socket obj, Member obj2)
        {
            client = obj;
            mem = obj2;
            InitializeComponent();
            webView.Source = new Uri("https://map.naver.com/p/");
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Func_Fage ff = new Func_Fage(client, mem);
            Close();
            ff.Show();
        }
    }
}
