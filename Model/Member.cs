using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfoClient.Model
{
    public class Member 
    {
        private int _Num;
        public int Num
        {
            get { return _Num; }
            set { _Num = value; }
        }
        private string _type = string.Empty;
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        private Byte[] _file;
        public Byte[] file
        {
            get { return _file; }
            set { _file = value; }
        }
        private string _user_name = string.Empty;
        public string user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }

        
    }
}
