 /*
  * author:zjx
  * date;2016.09.18
  * funtion:分别从单端、双端插入风机;监测器以及配电箱
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDrawCAD.Objects
{
    class VentilationFacilities : CommunalAttributeData
    {
        private double laneNum;//车道数
        public double LaneNum
        {
            get { return laneNum; }
            set { laneNum = value; }
        }
        private double laneWidth;//车道宽度
        public double LaneWidth
        {
            get { return laneWidth; }
            set { laneWidth = value; }
        }

        //构造函数
        public VentilationFacilities(double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel, double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel, double laneNum, double laneWidth, int isXueZhu)
            : base(startX_R_Tunnel, startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel, startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel, isXueZhu)
        {
            this.laneNum = laneNum;          //隧道每幅车道数
            this.laneWidth = laneWidth;        //隧道每幅车道宽
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mxCt1"></param>
        /// <param name="axMxDrawX1"></param>
        /// <param name="ventilation_number">风机数</param>
        /// <param name="layout_type">布置类型</param>
        /// <param name="number">每个段面布置风机台数</param>
        /// <param name="gap">两个风机之间的距离</param>
        /// <param name="size">大小</param>
        /// <param name="shift">移动距离</param>
        /// <param name="collisionState">中间碰撞数组</param>
        /// <param name="collisionStates">右幅上端碰撞数组</param>
        /// <param name="flag">为0表示从隧道入口端开始布置，为1从出口端开始布置</param>
        public void Fan_R(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double ventilation_number, string layout_type, int number, double gap, double size, double shift, int[] collisionState_R_C, int[] collisionState_R_H, int flag)
        {
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"VentilationFacilities"
            mxCt1.axMxDrawX1.AddLayer("VentilationFacilities");
            //设置当前图层为"VentilationFacilities"
            mxCt1.axMxDrawX1.LayerName = ("VentilationFacilities");
            //选择布置方式
            if (layout_type.Equals("单端"))
            {
                SinglePort_R(mxCt1, axMxDrawX1, ventilation_number, number, gap, size, shift, collisionState_R_C, collisionState_R_H, flag);
               
            }
            else if (layout_type.Equals("双端"))
            {
                DoublePort_R(mxCt1, axMxDrawX1, ventilation_number, number, gap, size, shift, collisionState_R_C, collisionState_R_H, flag);
               
            }
        }
        public void Fan_L(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double ventilation_number, string layout_type, int number, double gap, double size, double shift, int[] collisionState_L_C, int[] collisionState_L_H, int flag)
        {
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"VentilationFacilities"
            mxCt1.axMxDrawX1.AddLayer("VentilationFacilities");
            //设置当前图层为"VentilationFacilities"
            mxCt1.axMxDrawX1.LayerName = ("VentilationFacilities");
            //选择布置方式
            if (layout_type.Equals("单端"))
            {
                
                SinglePort_L(mxCt1, axMxDrawX1, ventilation_number, number, gap, size, shift, collisionState_L_C, collisionState_L_H, flag);
            }
            else if (layout_type.Equals("双端"))
            {
               
                DoublePort_L(mxCt1, axMxDrawX1, ventilation_number, number, gap, size, shift, collisionState_L_C, collisionState_L_H, flag);
            }
        }
        //右幅单端循环插入函数
        public void SinglePort_R(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double ventilation_number, int number, double gap, double size, double shift, int[] collisionState_R_C, int[] collisionStates_R_H, int flag)
        {
            //当风机数为小数时，向上取整
            int ven = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ventilation_number)).ToString());
            double i = StartX_R_Tunnel;
            int j = 0;
            float remainder = (float)ven / number;
            int times = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(remainder)).ToString());
            while (i < EndX_R_Tunnel && j < times)
            {
                double tmp = InsertBlock_Single_R(mxCt1, axMxDrawX1, i, shift, gap, number, size, collisionState_R_C, collisionStates_R_H, flag);
                i = tmp + gap;
                j++;
            }
        }
        //左幅单端循环插入函数
        public void SinglePort_L(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double ventilation_number, int number, double gap, double size, double shift, int[] collisionState_L_C, int[] collisionState_L_H, int flag)
        {
            //当风机数为小数时，向上取整
            int ven = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ventilation_number)).ToString());
            double i = StartX_L_Tunnel;
            int j = 0;
            float remainder = (float)ven / number;
            int times = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(remainder)).ToString());
            while (i < EndX_L_Tunnel && j < times)
            {
                double tmp = InsertBlock_Single_L(mxCt1, axMxDrawX1, i, shift, gap, number, size,  collisionState_L_C, collisionState_L_H, flag);
                i = tmp + gap;
                j++;
            }
        }
        //右幅双端循环插入函数
        public void DoublePort_R(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double ventilation_number, int number, double gap, double size, double shift, int[] collisionState_R_C, int[] collisionState_R_H, int flag)
        {
            double i = StartX_R_Tunnel;
            int j = 0;
            int times;//次数
            double remainder;//剩余台数
            int ven = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ventilation_number)).ToString()); ;//当风机数为小数时，向上取整
            //当输入风机台数为奇数时
            if (ven % 2 == 1)
            {
                times = (Convert.ToInt32(Math.Floor(Convert.ToDouble((ven / number / 2))).ToString()));
                remainder = ven - number * times * 2;
                while (i < EndX_R_Tunnel && j < times)
                {
                    //两边同时插入
                    double tmp = InsertBlock_Double_R(mxCt1, axMxDrawX1, i, shift, gap, number, size, collisionState_R_C, collisionState_R_H);
                    i = tmp + gap;
                    j++;
                }
                // 对于双端情况，将多余的风机插入到变电所一端
                while (remainder != 0)
                {
                    InsertBlock_Single_R(mxCt1, axMxDrawX1, i, shift, gap, number, size, collisionState_R_C, collisionState_R_H, flag);
                    break;
                }

            }
            else if (ven % 2 == 0)
            {
                times = Convert.ToInt32(Math.Floor(Convert.ToDouble((ven / number / 2))).ToString());
                remainder = ven - number * times * 2;
                while (i < EndX_R_Tunnel && j < times)
                {
                    double tmp = InsertBlock_Double_R(mxCt1, axMxDrawX1, i, shift, gap, number, size, collisionState_R_C, collisionState_R_H);
                    i = tmp + gap;
                    j++;
                }
                while (remainder != 0)
                {
                    InsertBlock_Single_R(mxCt1, axMxDrawX1, i, shift, gap, number, size, collisionState_R_C, collisionState_R_H,flag);
                    break;
                }
            }
        }
        //左幅双端循环插入函数
        public void DoublePort_L(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double ventilation_number, int number, double gap, double size, double shift, int[] collisionState_L_C, int[] collisionState_L_H, int flag)
        {
            double i = StartX_L_Tunnel;
            int j = 0;
            int times;//次数
            double remainder;//剩余台数
            int ven = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ventilation_number)).ToString()); ;//当风机数为小数时，向上取整
            //当输入风机台数为奇数时
            if (ven % 2 == 1)
            {
                times = (Convert.ToInt32(Math.Floor(Convert.ToDouble((ven / number / 2))).ToString()));
                remainder = ven - number * times * 2;
                while (i < EndX_L_Tunnel && j < times)
                {
                    //两边同时插入
                    double tmp = InsertBlock_Double_L(mxCt1, axMxDrawX1, i, shift, gap, number, size,  collisionState_L_C, collisionState_L_H);
                    i = tmp + gap;
                    j++;
                }
                // 对于双端情况，将多余的风机插入到变电所一端
                while (remainder != 0)
                {
                    InsertBlock_Single_L(mxCt1, axMxDrawX1, i, shift, gap, number, size, collisionState_L_C, collisionState_L_H, flag);
                    break;
                }

            }
            else if (ven % 2 == 0)
            {
                times = Convert.ToInt32(Math.Floor(Convert.ToDouble((ven / number / 2))).ToString());
                remainder = ven - number * times * 2;
                while (i < EndX_L_Tunnel && j < times)
                {
                    double tmp = InsertBlock_Double_L(mxCt1, axMxDrawX1, i, shift, gap, number, size,  collisionState_L_C, collisionState_L_H);
                    i = tmp + gap;
                    j++;
                }
                while (remainder != 0)
                {
                    InsertBlock_Single_L(mxCt1, axMxDrawX1, i, shift, gap, number, size,  collisionState_L_C, collisionState_L_H, flag);
                    break;
                }
            }
        }
        //右幅单端画图函数
        public double InsertBlock_Single_R(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double location, double shift, double gap, int number, double size, int[] collisionState_R_C, int[] collisionState_R_H, int flag)
        {
            double tmp = 0;
            double loc_tmp = EndX_R_Tunnel - location + StartX_R_Tunnel - gap;
            tmp = getRightLocation(location, size, shift, EndX_R_Tunnel, collisionState_R_C, collisionState_R_H);
            if (flag == 0)
            {
                DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_R_Tunnel + (laneWidth * laneNum), 0);
                //单台
                if (number == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState_R_C, collisionState_R_H);
                }
                //两台
                else if (number == 2)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    tmp = getRightLocation(location, size, shift, EndX_R_Tunnel, collisionState_R_C, collisionState_R_H);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState_R_C, collisionState_R_H);
                }
                //三台
                else if (number == 3)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState_R_C, collisionState_R_H);
                }
            }
            //出口端
            else if (flag == 1)
            {
                DrawEquipmentLocation(mxCt1, axMxDrawX1, loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum), 0);
                if (number == 1)
                {
                    //右端
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    //画右端配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState_R_C, collisionState_R_H);
                }
                else if (number == 2)
                {
                    //定义右端设备位置
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(location, size, collisionState_R_C, collisionState_R_H);
                }
                else if (number == 3)
                {
                    //右端
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(location, size, collisionState_R_C, collisionState_R_H);
                }
            }
            return tmp;
        }
        //左幅单端画图函数
        public double InsertBlock_Single_L(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double location, double shift, double gap, int number, double size,int[] collisionState_L_C, int[] collisionState_L_H, int flag)
        {
            double tmp = 0;
            double loc_tmp = EndX_L_Tunnel - location + StartX_L_Tunnel - gap;
            tmp = getRightLocation(location, size, shift, EndX_R_Tunnel,  collisionState_L_C, collisionState_L_H);
            //入口段
            if (flag == 1)
            {
                //单台
                if (number == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel+2, "peidianxiang", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_L_Tunnel, 1);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState_L_C, collisionState_L_H);
                }
                //两台
                else if (number == 2)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel +2, "peidianxiang", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_L_Tunnel, 1);
                    //加入碰撞检测
                    setCollosion(tmp, size,  collisionState_L_C, collisionState_L_H);
                }
                //三台
                else if (number == 3)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel+2, "peidianxiang", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_L_Tunnel, 1);
                    //加入碰撞检测
                    setCollosion(tmp, size,  collisionState_L_C, collisionState_L_H);
                }
            }
            //出口段
            else if (flag == 0)
            {
                DrawEquipmentLocation(mxCt1, axMxDrawX1, loc_tmp, StartY_L_Tunnel, 1);
                if (number == 1)
                {
                    //右端
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    //画右端配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel+2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState_L_C, collisionState_L_H);
                }
                else if (number == 2)
                {
                    //定义右端设备位置
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(location, size,  collisionState_L_C, collisionState_L_H);
                }
                else if (number == 3)
                {
                    //右端
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                    //画配电箱
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                    mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel  + 2, "peidianxiang", 1, 0);
                    //加入碰撞检测
                    setCollosion(location, size, collisionState_L_C, collisionState_L_H);
                }
            }
            return tmp;
        }
        //右幅双端画图函数
        public double InsertBlock_Double_R(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double location, double shift, double gap, int number, double size, int[] collisionState_R_C, int[] collisionState_R_H)
        {
            double tmp = 0;
            //从出口端插入的坐标
            double loc_tmp = EndX_R_Tunnel - location + StartX_R_Tunnel - gap;
            tmp = getRightLocation(location, size, shift, EndX_R_Tunnel, collisionState_R_C, collisionState_R_H);
            if (number == 1)
            {
                //右端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                //画右端配电箱
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum), 0);

                //左端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                //画左端配电箱
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_R_Tunnel + (laneWidth * laneNum), 0);


                //加入碰撞检测
                setCollosion(tmp, size, collisionState_R_C, collisionState_R_H);
            }
            else if (number == 2)
            {
                //定义右端设备位置
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                //画配电箱
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum), 0);

                //左端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                //画配电箱
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_R_Tunnel + (laneWidth * laneNum), 0);
                //加入碰撞检测
                setCollosion(location, size, collisionState_R_C, collisionState_R_H);
            }
            else if (number == 3)
            {
                //右端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                //画配电箱
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, loc_tmp, StartY_R_Tunnel + (laneWidth * laneNum), 0);
                //左端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                //画配电箱
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_R_Tunnel + (laneWidth * laneNum) - 2, "peidianxiang", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_R_Tunnel + (laneWidth * laneNum), 0);
                //加入碰撞检测
                setCollosion(location, size, collisionState_R_C, collisionState_R_H);
            }
            return tmp;
        }
        //左幅双端画图函数
        public double InsertBlock_Double_L(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double location, double shift, double gap, int number, double size, int[] collisionState_L_C, int[] collisionState_L_H)
        {
            double tmp = 0;
            //从出口端插入的坐标
            double loc_tmp = EndX_L_Tunnel - location + StartX_L_Tunnel - gap;
            tmp = getRightLocation(location, size, shift, EndX_L_Tunnel,  collisionState_L_C, collisionState_L_H);
            //画右端配电箱
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/peidianxiang.dwg", "peidianxiang");
            mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel+2, "peidianxiang", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, loc_tmp, StartY_L_Tunnel, 1);
            //入口端
            mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel+2, "peidianxiang", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, location + gap, StartY_L_Tunnel, 1);
            if (number == 1)
            {
                //右端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                //左端
               // mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                //加入碰撞检测
                setCollosion(tmp, size, collisionState_L_C, collisionState_L_H);
            }
            else if (number == 2)
            {
                //右端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                //左端
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 3, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) * 2 / 3, "fan", 1, 0);
                setCollosion(location, size, collisionState_L_C, collisionState_L_H);
            }
            else if (number == 3)
            {
                //右端
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/fan.dwg", "fan");
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(loc_tmp, StartY_L_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);
                //左端

                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 4, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) / 2, "fan", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(location + gap, StartY_L_Tunnel + (laneWidth * laneNum) * 3 / 4, "fan", 1, 0);

                //加入碰撞检测
                setCollosion(location, size,  collisionState_L_C, collisionState_L_H);
            }
            return tmp;
        }
        //右幅插入风速风向、空气质量监测器
        public void Detector_R(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double shift, double size, int[] collisionState)
        {
            double tmp = 0;
            tmp = getRightLocation(StartY_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) / 2, size, shift, EndX_R_Tunnel, collisionState);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/fengjijianceqi.dwg", "fengjijianceqi");
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/kongqijianceqi.dwg", "kongqijianceqi");
            //在隧道1/2,
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) / 2, StartY_R_Tunnel + 10, "fengjijianceqi", 1, 0);
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) / 2, StartY_R_Tunnel + 5, "kongqijianceqi", 1, 0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) / 2, StartY_R_Tunnel, 1);
            //隧道3/4处放置
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) * 3 / 4, StartY_R_Tunnel + 10, "fengjijianceqi", 1, 0);
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) * 3 / 4, StartY_R_Tunnel + 5, "kongqijianceqi", 1, 0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_R_Tunnel + (EndX_R_Tunnel - StartX_R_Tunnel) * 3 / 4, StartY_R_Tunnel, 1);

            //加入碰撞检测
            setCollosion(tmp, size, collisionState);

        }
        //左幅插入风速风向、空气质量监测器
        public void Detector_L(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double shift, double size, int[] collisionState)
        {
            double tmp = 0;
            tmp = getRightLocation(StartY_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) / 2, size, shift, EndX_R_Tunnel, collisionState);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/kongqijianceqi.dwg", "kongqijianceqi");
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/fengjijianceqi.dwg", "fengjijianceqi");
            //在隧道1/2,
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) / 2, StartY_L_Tunnel + (laneNum * laneWidth) -5, "kongqijianceqi", 1, 0);
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) / 2, StartY_L_Tunnel + (laneNum * laneWidth) -10, "fengjijianceqi", 1, 0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) / 2, StartY_L_Tunnel + (laneNum * laneWidth), 0);
            //隧道3/4处放置
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) * 3 / 4, StartY_L_Tunnel + (laneNum * laneWidth) - 10, "fengjijianceqi", 1, 0);
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) * 3 / 4, StartY_L_Tunnel + (laneNum * laneWidth) - 5, "kongqijianceqi", 1, 0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) * 3 / 4, StartY_L_Tunnel + (laneNum * laneWidth), 0);
            //加入碰撞检测
            setCollosion(tmp, size, collisionState);
        }
    }
}
