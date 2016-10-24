using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace WPFDrawCAD.Objects
{
    class SwitchEquipment : CommunalAttributeData
    {
        //构造函数
        public SwitchEquipment(double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel, int isXueZhu)
            : base(startX_R_Tunnel, startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel, startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel, isXueZhu)
        {

        }


        public void DrawBaseR(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, int lengh, int lengh_ave, int baseswitch_size, int powerswitch_size, int lanewidth, double laneNum, int shift,int remain, bool flag, int[] collisionStateB)
        {
             //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.AddLayer("SwitchEquipmentLayer");
            //设置当前图层为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.LayerName = ("SwitchEquipmentLayer");
            int tmp = 0;
            if (flag)
            {
                int cumulation = remain;
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                tmp = getRightLocation(StartX_R_Tunnel + remain / 2, baseswitch_size, shift, EndX_R_Tunnel, collisionStateB);
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + lanewidth * laneNum - powerswitch_size / 2, "BaseSwitch", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel + lanewidth * laneNum, 0);
                while (cumulation + lengh_ave / 2 <= lengh)
                {
                    tmp = getRightLocation(lengh_ave / 2 + cumulation + StartX_R_Tunnel, baseswitch_size, -shift, EndX_R_Tunnel, collisionStateB);
                    System.Console.WriteLine("RRRRRRRRRRRRRRRRRRRRRR    tmp is {0}",tmp);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + lanewidth * laneNum - powerswitch_size / 2, "BaseSwitch", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel + lanewidth * laneNum, 0);
                    cumulation += lengh_ave;
                }
               
            }
            else
            {
                int cumulation = 0;
                while (cumulation+lengh_ave / 2 <= lengh)
                {
                    tmp = getRightLocation(lengh_ave / 2 + cumulation + StartX_R_Tunnel, baseswitch_size, shift, EndX_R_Tunnel, collisionStateB);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + lanewidth * laneNum - powerswitch_size / 2, "BaseSwitch", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel + lanewidth * laneNum, 0);
                    cumulation += lengh_ave;
                }
            }
          //  mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }

        public void DrawBaseL(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, int lengh, int lengh_ave, int baseswitch_size, int powerswitch_size, int lanewidth, double laneNum, int shift, int remain, bool flag, int[] collisionStateB) {

            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.AddLayer("SwitchEquipmentLayer");
            //设置当前图层为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.LayerName = ("SwitchEquipmentLayer");
            int tmp = 0;
              if (flag)
            {
                int cumulation =remain ;
                tmp = getRightLocation(remain/2 + StartX_R_Tunnel, baseswitch_size, shift, EndX_R_Tunnel, collisionStateB);
              
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel+ powerswitch_size , "BaseSwitch", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel, 1);
                while (cumulation + lengh_ave / 2 <= lengh)
                {
                    tmp = getRightLocation(lengh_ave / 2 + cumulation + StartX_L_Tunnel, baseswitch_size, -shift, EndX_L_Tunnel, collisionStateB);
                    
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + powerswitch_size , "BaseSwitch", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel, 1);
                    cumulation += lengh_ave;
                }
                cumulation = cumulation +lengh_ave;
                
            }
            else
            {
                int cumulation = 0;
                while (cumulation + lengh_ave / 2 <= lengh)
                {
                    tmp = getRightLocation(lengh_ave / 2 + cumulation + StartX_L_Tunnel, baseswitch_size, -shift, EndX_L_Tunnel, collisionStateB);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + powerswitch_size, "BaseSwitch", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel, 1);
                    cumulation += lengh_ave;
                }
            }
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }





        public void InsertBlock_ClickL(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double baseswitch_size, double powerswitch_size, double shift, double begin_powerswitch_gap, double end_powerswitch_gap, double baseswitch_gap, double lanewidth, double laneNum, double out_gap_camara, int[] collisionStateB,int[]collisionStateA)
        {

            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.AddLayer("SwitchEquipmentLayer");
            //设置当前图层为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.LayerName = ("SwitchEquipmentLayer");
            //设置入口处加强配电箱
            double tmp = getRightLocation(-begin_powerswitch_gap + EndX_L_Tunnel, powerswitch_size, (shift), EndX_L_Tunnel, collisionStateB);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/powerswitch.dwg", "PowerSwitch");
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + powerswitch_size, "PowerSwitch", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel, 1);
            setCollosion(tmp, 4, collisionStateB);

            //设置出口处加强配电箱
            tmp = getRightLocation(begin_powerswitch_gap+StartX_L_Tunnel, powerswitch_size, (-shift), EndX_L_Tunnel, collisionStateB);
            System.Console.WriteLine("the powerSwitch loc is{0}",tmp);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/powerswitch.dwg", "PowerSwitch");
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + powerswitch_size, "PowerSwitch", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel, 1);
            setCollosion(tmp, 4, collisionStateB);


            ////设置洞内亮度检测器
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/monitor.dwg", "Monitor");
            tmp = getRightLocation(EndX_L_Tunnel - 10, 4, -shift, EndX_L_Tunnel, collisionStateA);
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel+laneNum*lanewidth-0.5, "Monitor", 0.5, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel+laneNum * lanewidth, 0);
            setCollosion(tmp, 4, collisionStateA);
            Console.WriteLine("++++++++++++++++++++++++++__________________{0}",StartY_L_Tunnel+laneNum * lanewidth==EndY_L_Tunnel);
           


            //判断隧道是否不足500米
            if (EndX_L_Tunnel - StartX_L_Tunnel < 500)
            {

                tmp = getRightLocation((EndX_L_Tunnel + StartX_L_Tunnel) / 2, baseswitch_size, -shift, EndX_L_Tunnel, collisionStateB);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + powerswitch_size, "BaseSwitch", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel, 1);


            }
            /*
             *   放置基础配电箱
             */
            else
            {
                int lengh = (int)EndX_L_Tunnel - (int)StartX_L_Tunnel;
                int lengh_ave = 500;
                bool flag = false;
                int remain = 0;
                while (lengh % lengh_ave != 0)
                {
                    lengh_ave--;
                }
                if (lengh_ave < 400)
                {
                    lengh_ave = 500;
                    remain = lengh % lengh_ave;
                    flag = true;
                }
                DrawBaseL(mxCt1, axMxDrawX1, lengh, lengh_ave, (int)baseswitch_size, (int)powerswitch_size, (int)lanewidth, laneNum, (int)-shift, remain, flag, collisionStateB);
            }

            
        }





        public void InsertBlock_ClickR(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double baseswitch_size, double powerswitch_size, double shift, double begin_powerswitch_gap, double end_powerswitch_gap, double baseswitch_gap, double lanewidth,double  laneNum, double out_gap_camara, int[] collisionStateB,int []collisionStateA)
        {
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.AddLayer("SwitchEquipmentLayer");
            //设置当前图层为"LightingEquipmentLayer"
            mxCt1.axMxDrawX1.LayerName = ("SwitchEquipmentLayer");


            System.Console.WriteLine("the StartX{0}", begin_powerswitch_gap + StartX_R_Tunnel);
            //设置入口处加强配电箱
            double tmp = getRightLocation(begin_powerswitch_gap + StartX_R_Tunnel, powerswitch_size, -(shift), EndX_R_Tunnel, collisionStateB);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/powerswitch.dwg", "PowerSwitch");
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + lanewidth * laneNum, "PowerSwitch", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel + lanewidth * laneNum, 0);
            setCollosion(tmp, 4, collisionStateB);

           


            //设置出口处加强配电箱
            tmp = getRightLocation(EndX_R_Tunnel - end_powerswitch_gap, powerswitch_size, -(shift), EndX_R_Tunnel, collisionStateB);
            System.Console.WriteLine("出口处加强配电箱 tmp is{0}", tmp);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/powerswitch.dwg", "PowerSwitch");
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + lanewidth * laneNum - powerswitch_size / 2, "PowerSwitch", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel + lanewidth * laneNum, 0);
            setCollosion(tmp, 4, collisionStateB);

            //设置亮度检测器

            ////设置洞内亮度检测器
          


            tmp = getRightLocation(StartX_R_Tunnel + 10, 4, shift, EndX_R_Tunnel, collisionStateA);
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel+0.5, "Monitor", 0.5, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel, 1);
            setCollosion(tmp, 4, collisionStateA);

            //设置洞内基本及应急照明配电箱

            //判断隧道是否不足500米
            if (EndX_R_Tunnel - StartX_R_Tunnel < 500)
            {

                tmp = getRightLocation((EndX_R_Tunnel + StartX_R_Tunnel) / 2, baseswitch_size, -shift, EndX_R_Tunnel, collisionStateB);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/baseswitch.dwg", "BaseSwitch");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + lanewidth * laneNum - powerswitch_size / 2, "BaseSwitch", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel + lanewidth * laneNum, 0);


            }
            /*
             *   放置基础配电箱
             */
            else
            {
                int lengh = (int)EndX_L_Tunnel - (int)StartX_L_Tunnel;
                int lengh_ave = 500;
                bool flag = false;
                int remain = 0;
                while (lengh % lengh_ave != 0)
                {
                    lengh_ave--;
                }
                if (lengh_ave < 400)
                {
                    lengh_ave = 500;
                    remain = lengh % lengh_ave;
                    flag = true;
                }
                System.Console.WriteLine("++++++++++++++++++++++++++++++++" + flag);
             DrawBaseR(mxCt1, axMxDrawX1, lengh, lengh_ave, (int)baseswitch_size, (int)powerswitch_size, (int)lanewidth, laneNum, (int)shift, remain, flag, collisionStateB);

            }
        }

    }
}






