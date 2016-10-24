using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MxDrawXLib;


namespace WPFDrawCAD.Objects
{
    class CommunalAttributeData
    {

        private List<int> collisionList_L_L = new List<int>();
        private List<int> collisionList_L_H = new List<int>();
        private List<int> collisionList_R_L = new List<int>();
        private List<int> collisionList_R_H = new List<int>();
        private double startX_R_Tunnel;//隧道右幅起点桩号X坐标
        public double StartX_R_Tunnel
        {
            get { return startX_R_Tunnel; }
            set { startX_R_Tunnel = value; }
        }
        private double startY_R_Tunnel;//隧道右幅起点桩号Y坐标
        public double StartY_R_Tunnel
        {
            get { return startY_R_Tunnel; }
            set { startY_R_Tunnel = value; }
        }
        private double endX_R_Tunnel;//隧道右幅终点桩号X坐标
        public double EndX_R_Tunnel
        {
            get { return endX_R_Tunnel; }
            set { endX_R_Tunnel = value; }
        }
        private double endY_R_Tunnel;//隧道右幅终点桩号Y坐标
        public double EndY_R_Tunnel
        {
            get { return endY_R_Tunnel; }
            set { endY_R_Tunnel = value; }
        }
        private double startX_L_Tunnel;//隧道左幅起点桩号X坐标
        public double StartX_L_Tunnel
        {
            get { return startX_L_Tunnel; }
            set { startX_L_Tunnel = value; }
        }
        private double startY_L_Tunnel;//隧道左幅起点桩号Y坐标
        public double StartY_L_Tunnel
        {
            get { return startY_L_Tunnel; }
            set { startY_L_Tunnel = value; }
        }
        private double endX_L_Tunnel;//隧道左幅终点桩号X坐标
        public double EndX_L_Tunnel
        {
            get { return endX_L_Tunnel; }
            set { endX_L_Tunnel = value; }
        }
        private double endY_L_Tunnel;//隧道左幅终点桩号Y坐标
        public double EndY_L_Tunnel
        {
            get { return endY_L_Tunnel; }
            set { endY_L_Tunnel = value; }
        }

        private int isXueZhu; //是否是削竹型隧道
        public int IsXueZhu
        {
            get { return isXueZhu; }
            set { isXueZhu = value; }
        }

        //无参构造函数
        public CommunalAttributeData()
        {

        }
 
        //参数：右幅起点桩号（X,Y）、右幅终点桩号（X，Y）、左幅起点桩号（X,Y）、左幅终点桩号（X，Y）
        public CommunalAttributeData(double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel,int isXueZhu)
        {
            this.StartX_R_Tunnel = startX_R_Tunnel;
            this.StartY_R_Tunnel = startY_R_Tunnel;
            this.EndX_R_Tunnel = endX_R_Tunnel;
            this.EndY_R_Tunnel = endY_R_Tunnel;

            this.StartX_L_Tunnel = startX_L_Tunnel;
            this.StartY_L_Tunnel = startY_L_Tunnel;
            this.EndX_L_Tunnel = endX_L_Tunnel;
            this.EndY_L_Tunnel = endY_L_Tunnel;

            this.isXueZhu = isXueZhu;
        }

        /**
       * 碰撞检测函数之判断位置是否能插入
         * by Jun
       * */
        public bool isOK(double insert_X, double insert_size, int[] collisionState, int[] collisionStates)
        {
            int max = DoutoInt(insert_X + insert_size / 2);
            int min = DoutoInt(insert_X - insert_size / 2);
            for (int i = min; i <= max; i++)
            {
                if (collisionState[i] == 1 && collisionStates[i] == 1)
                {
                    return false;
                }

            }
            return true;

        }

        /**
        * 碰撞检测函数之判断位置是否能插入
        * */
        public bool isOK(double insert_X, double insert_size, int[] collisionState)
        {
            int max = DoutoInt(insert_X + insert_size / 2);
            int min = DoutoInt(insert_X - insert_size / 2);
                for (int i = min; i <= max; i++)
                {
                    if (collisionState[i] == 1)
                    {
                        return false;
                    }

                }
                return true;
           
        }

        /**
        * 碰撞检测函数之给出合理插入位置
         * by Jun
        * */
        public int getRightLocation(double insert_X, double insert_size, double shift, double endX_R_Tunnel, int[] collisionState, int[] collisionStates)
        {
            int tmpshift = DoutoInt(shift);
            for (int j = DoutoInt(insert_X); j < DoutoInt(endX_R_Tunnel); j = j + tmpshift)
            {
                if (isOK(j, insert_size, collisionState, collisionStates))
                {
                    return j;
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }

        /**
         * 碰撞检测函数之给出合理插入位置
         * */
        public int getRightLocation(double insert_X, double insert_size, double shift,double endX_R_Tunnel, int[] collisionState)
        {
            int tmpshift = DoutoInt(shift);
            for (int j = DoutoInt(insert_X); j < DoutoInt(endX_R_Tunnel); j = j + tmpshift)
            {
                if (isOK(j, insert_size, collisionState))
                {
                    return j;
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }

        ///**by czt
        // * 标注设备的位置
        // * */
        //public void DrawEquipmentLocation(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1,double startX_Euip,double startY_Tunnel)
        //{
        //    mxCt1.axMxDrawX1.LineType = "";
        //    mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 2, startX_Euip, startY_Tunnel - 20);
        //   // string position = startX_Euip.ToString();
        //    string position = NumToKString(startX_Euip);
        //    mxCt1.axMxDrawX1.DrawText(startX_Euip+1, startY_Tunnel - 40, position, 2, 90, 0, 1);
        //}
        ///**by czt
        // * 标注设备的位置
        // * flag = 0 上标注
        // * flag = 1  下标注
        // * */
        //public void DrawEquipmentLocation(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_Euip, double startY_Tunnel,int flag)
        //{
        //    mxCt1.axMxDrawX1.LineType = "";
           
        //    if (flag == 1) 
        //    {
        //        mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 2, startX_Euip, startY_Tunnel - 20);
        //        //string position = startX_Euip.ToString();
        //        string position = NumToKString(startX_Euip);
        //        mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 35, position, 2, 90, 0, 1);
        //    }
        //    else if (flag == 0)
        //    {
        //        mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel + 2, startX_Euip, startY_Tunnel + 20);
        //        //string position = startX_Euip.ToString();
        //        string position = NumToKString(startX_Euip);
        //        mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel + 25, position, 2, 90, 0, 1);
        //    }
        //}
        ///**by czt
        //* 标注设备的位置
        //* */
        //public void DrawEquipmentLocation(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_Euip, double startY_Tunnel)
        //{
        //    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
        //    mxCt1.axMxDrawX1.LineType = "";
           
        //    string position = NumToKString(startX_Euip);
        //    if (startY_Tunnel == StartY_L_Tunnel)
        //    {
        //        mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 2, startX_Euip, startY_Tunnel - 5);
        //        mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
        //        mxCt1.axMxDrawX1.TextStyle = "myType";
        //        mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 5, position, 2.5, 90, 2, 1);
        //    }
        //    else {
        //        mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 10, startX_Euip, startY_Tunnel - 13);
        //        mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
        //        mxCt1.axMxDrawX1.TextStyle = "myType";
        //        mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 13, position, 2.5, 90, 2, 1);
        //    }
           
        //}
        /**by czt
         * 标注设备的位置
         * flag = 0 上标注
         * flag = 1  下标注
         * */
        public void DrawEquipmentLocation(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_Euip, double startY_Tunnel, int flag)
        {
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            mxCt1.axMxDrawX1.LineType = "";
            if (startY_Tunnel >= StartY_L_Tunnel )
            {
                if (flag == 1)
                {
                    //mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 2, startX_Euip, startY_Tunnel - 5);
                    //string position = NumToKString(startX_Euip);
                    //mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
                    //mxCt1.axMxDrawX1.TextStyle = "myType";
                    //mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 5, position, 2.5, 90, 2, 1);


                    mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 2, startX_Euip, startY_Tunnel - 5);
                    string position = NumToKString(startX_Euip);
                    mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
                    mxCt1.axMxDrawX1.TextStyle = "myType";
                    MxDrawText txt = (MxDrawText)mxCt1.axMxDrawX1.ObjectIdToObject(mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 5, position, 2.5, 90, 0, 1));
                    MxDrawPoint minpt, maxpt;
                    txt.GetBoundingBox(out minpt, out maxpt);
                    MxDrawPoint newpos = txt.Position;
                    newpos.y -= (maxpt.y - minpt.y);
                    txt.Position = newpos;
                    txt.AlignmentPoint = newpos;


                }
                else if (flag == 0)
                {
                    mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel + 10, startX_Euip, startY_Tunnel + 13);
                    string position = NumToKString(startX_Euip);
                    mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
                    mxCt1.axMxDrawX1.TextStyle = "myType";
                    mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel + 13, position, 2.5, 90, 0, 1);
                }
            }
            else
            {
                if (flag == 1)
                {
                    //mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 10, startX_Euip, startY_Tunnel - 13);
                    //string position = NumToKString(startX_Euip);
                    //mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
                    //mxCt1.axMxDrawX1.TextStyle = "myType";
                    //mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 13, position, 2.5, 90, 2, 1);


                    mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel - 10, startX_Euip, startY_Tunnel - 13);
                    string position = NumToKString(startX_Euip);
                    mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
                    mxCt1.axMxDrawX1.TextStyle = "myType";
                    MxDrawText txt = (MxDrawText)mxCt1.axMxDrawX1.ObjectIdToObject(mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel - 13, position, 2.5, 90, 0, 1));
                    MxDrawPoint minpt, maxpt;
                    txt.GetBoundingBox(out minpt, out maxpt);
                    MxDrawPoint newpos = txt.Position;
                    newpos.y -= (maxpt.y - minpt.y);
                    txt.Position = newpos;
                    txt.AlignmentPoint = newpos;


                }
                else if (flag == 0)
                {
                    mxCt1.axMxDrawX1.DrawLine(startX_Euip, startY_Tunnel + 2, startX_Euip, startY_Tunnel + 5);
                    string position = NumToKString(startX_Euip);
                    mxCt1.axMxDrawX1.AddTextStyle2("myType", "宋体", 0.75);
                    mxCt1.axMxDrawX1.TextStyle = "myType";
                    mxCt1.axMxDrawX1.DrawText(startX_Euip + 1, startY_Tunnel + 5, position, 2.5, 90, 0, 1);
                }

            }
        }
        
        /*
         * 将碰撞数组置一
         * 传入的参数都是以米为单位
         * 
         */
        public void setCollosion(double location, double insertsize, int[] collesion) 
        {
            int X1 = DoutoInt(location - (insertsize / 2)) - 1;// +1 外延伸1米 
            int X2 = DoutoInt(location + (insertsize / 2)) + 1;

            for (int j = X1; j <= X2; j++)
            {
                collesion[j] = 1;
            }
        }

        //获取还原的位置
        public List<int> getCollisionList_L_L()
        {
            return this.collisionList_L_L;
        }
        public List<int> getCollisionList_L_H()
        {
            return this.collisionList_L_H;
        }
        public List<int> getCollisionList_R_L()
        {
            return this.collisionList_R_L;
        }
        public List<int> getCollisionList_R_H()
        {
            return this.collisionList_R_H;
        }


        ////设置还原位置
        public void setCollisionList(double location, double insertSize, int flag)
        {
            int X1 = DoutoInt(location - (insertSize / 2)) - 1;// +1 外延伸1米 
            int X2 = DoutoInt(location + (insertSize / 2)) + 1;
            List<int> collisionList = new List<int>();
            if (flag == 0) collisionList = collisionList_L_H;
            if (flag == 1) collisionList = collisionList_L_L;
            if (flag == 2) collisionList = collisionList_R_H;
            if (flag == 3) collisionList = collisionList_R_L;
            for (int j = X1; j <= X2; j++)
            {
                collisionList.Add(j);
            }
        }


        //风机所用 碰撞函数
        public void setCollosion(double location, double insertsize, int[] collisionState1, int[] collisionState2)
        {
            int X1 = DoutoInt(location - (insertsize / 2)) - 1;// +1 外延伸1米 
            int X2 = DoutoInt(location + (insertsize / 2)) + 1;

            for (int j = X1; j <= X2; j++)
            {
                collisionState1[j] = 1;
                collisionState2[j] = 1;

            }
        }

        /*
         * 将小数向上转换
         * 如果是整数就直接返回
         * 如果是小数就+1向上取整
         */
        public int DoutoInt(double num)
        {
          return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(num)).ToString());
        }


        /*
        * 由数字转换成k值
        *
        */
        public String NumToKString(double num)
        {
            int tmp = DoutoInt(num);
            int YuShu = tmp % 1000;
            int Shang = tmp / 1000;
            return "K" + Shang + "+" + YuShu;
        }

        /*
         * 是否位于紧急停车带范围内
         * EmergencyStopCar 里面为整形
         */
        public bool isInEmerg(int insert_X, double lenth_R_Emergency, ArrayList EmergencyStopCar)
        {
            int HalfOfEmergenceStopCar = DoutoInt(lenth_R_Emergency / 2);
            foreach (int merge in EmergencyStopCar)
            {
                if (merge - HalfOfEmergenceStopCar < insert_X && merge + HalfOfEmergenceStopCar > insert_X)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
