using System.Net.Sockets;
using System.Net;
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
using CarInfoClient.Model;
using Newtonsoft.Json;

// OpenCV 사용을 위한 using
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.Windows.Threading;
using System.IO;

namespace CarInfoClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : System.Windows.Window
{
    //통신
    Socket client;
    Member mem = new Member();

    //웹캠
    VideoCapture cam;
    Mat frame;
    DispatcherTimer timer;
    bool is_initCam, is_initTimer;
    int num = 0;

    public MainWindow()
    {
        InitializeComponent();
        Start();
        Photo.Visibility = Visibility.Hidden;
        count.Visibility = Visibility.Hidden;
    }
    public void Start()
    {
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999));
    }

    public static void Send_data(object obj, Member mem)
    {
        Socket client = (Socket)obj;
        
        string datas = JsonConvert.SerializeObject(mem);
        byte[] jsonByte = Encoding.UTF8.GetBytes(datas);
        client.Send(jsonByte);
        byte[] responseBuffer = new byte[1024];
    }
    public static Member Receive_data(object obj, Member mem)
    {
        Socket client = (Socket)obj;
        
        byte[] data = new byte[1024];

        int byteRead = client.Receive(data, data.Length, SocketFlags.None);
        string datas = Encoding.UTF8.GetString(data, 0, byteRead);
        mem = JsonConvert.DeserializeObject<Member>(datas);
        return mem;

    }


    private void Windows_load(object sender, RoutedEventArgs e)
    {
        is_initCam = init_camera();
        is_initTimer = init_Timer(0.01);

        if (is_initTimer && is_initCam) timer.Start();
        
    }
    private bool init_camera()
    {
        try
        {
            // 0번 카메라로 VideoCapture 생성 (카메라가 없으면 안됨)
            cam = new VideoCapture(0);
            cam.Set(VideoCaptureProperties.FrameHeight, 400);
            cam.Set(VideoCaptureProperties.FrameWidth, 350);

            // 카메라 영상을 담을 Mat 변수 생성
            frame = new Mat();

            return true;
        }
        catch
        {
            return false;
        }
    }
    private bool init_Timer(double interval_ms)
    {
        try
        {
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(interval_ms);
            timer.Tick += new EventHandler(timer_tick);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void Register_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("얼굴을 6번 찍어주세요");
        Photo.Visibility = Visibility.Visible;
        count.Visibility = Visibility.Visible;
    }

    private void Photo_Click(object sender, RoutedEventArgs e)
    {
        if (!frame.Empty())
        {
            
            lbl_num.Content = ++num;
            if (num == 6)
            {
                MessageBox.Show("사용자 등록이 완료되었습니다.");
                Photo.Visibility = Visibility.Hidden;
                lbl_num.Visibility = Visibility.Hidden;
                count.Visibility = Visibility.Hidden;
            }

            string filePath = "C:\\Users\\lms5\\source\\repos\\CarInfoClient\\CarInfoClient\\save.jpg";
            Cv2.ImWrite(filePath, frame); // 정상적으로 저장

            mem.type = "File";

            using (FileStream filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(filestream))
            mem.file = reader.ReadBytes((int)filestream.Length); //전송용

            Send_data(client, mem);


            
        }
        //mem = Receive_data(client, mem);
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        if (!frame.Empty())
        {

            string filePath = "C:\\Users\\lms5\\source\\repos\\CarInfoClient\\CarInfoClient\\save.jpg";
            Cv2.ImWrite(filePath, frame); // 정상적으로 저장

            mem.type = "Login";
            

            using (FileStream filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(filestream))
            mem.file = reader.ReadBytes((int)filestream.Length); //전송용

            Send_data(client, mem);

            mem = Receive_data(client, mem);

            if (mem.Num == 1)
            {
                MessageBox.Show("엔진이 켜졌습니다.");
                MyPage mp = new MyPage(client, mem);
                Close();
                mp.Show();
            }
            else
            {
                MessageBox.Show("얼굴이 맞지 않습니다.");

            }

        }

        
    }

    private void timer_tick(object sender, EventArgs e)
    {
        // 0번 장비로 생성된 VideoCapture 객체에서 frame을 읽어옴
        cam.Read(frame);
        Cv2.Resize(frame, frame, new OpenCvSharp.Size(360, 350));

        // 읽어온 Mat 데이터를 Bitmap 데이터로 변경 후 컨트롤에 그려줌
        Dispatcher.Invoke(() =>
        {
            Cam_1.Source = WriteableBitmapConverter.ToWriteableBitmap(frame);

        });

    }


}