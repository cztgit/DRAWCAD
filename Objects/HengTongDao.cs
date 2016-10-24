using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDrawCAD.Objects
{
    class HengTongDao
    {
        //0----人行  1===车行
        private int hengtongdao_man;
        public int HengTongDao_Man
        {
            get { return hengtongdao_man; }
            set { hengtongdao_man = value; }
        }
       //横通道右幅插入点桩号
        private double hengtongdao_r;
        public double HengTongDao_R
        {
            get { return hengtongdao_r; }
            set { hengtongdao_r = value; }
        }
        //横通道左幅插入点桩号
        private double hengtongdao_l;
        public double HengTongDao_L
        {
            get { return hengtongdao_l; }
            set { hengtongdao_l = value; }
        }
        //横通道长度
        private double hengtongdao_length;
        public double HengTongDao_Length
        {
            get { return hengtongdao_length; }
            set { hengtongdao_length = value; }
        }
        public HengTongDao(int hengtongdao_man, double hengtongdao_r, double hengtongdao_l, double hengtongdao_length)
        {
            this.hengtongdao_man = hengtongdao_man;
            this.hengtongdao_r = hengtongdao_r;
            this.hengtongdao_l = hengtongdao_l;
            this.hengtongdao_length = hengtongdao_length;
        }
    }
}
