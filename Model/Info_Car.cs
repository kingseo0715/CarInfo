using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfoClient.Model
{
    public class Info_Car : INotifyPropertyChanged
    {
        private int _oil;
        public int Oil
        {
            get { return _oil; }
            set
            {
                if (_oil != value)
                {
                    _oil = value;
                    OnPropertyChanged(nameof(Oil));  // 바뀔 때 UI에 알림
                }
            }
        }

        private int _engine;
        public int Engine
        {
            get { return _engine; }
            set
            {
                if (_engine != value)
                {
                    _engine = value;
                    OnPropertyChanged(nameof(_engine));  // 바뀔 때 UI에 알림
                }
            }
        }

        //livechar 데이터 바인딩을 위한 구현
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
