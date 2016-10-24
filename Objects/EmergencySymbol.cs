using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MxDrawXLib;
using WPFDrawCAD.Objects;
using System.Collections;


namespace WPFDrawCAD.Objects
{
    class EmergencySymbol : CommunalAttributeData
    {

        public EmergencySymbol(double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel,int isXueZhu)
            : base(startX_R_Tunnel, startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel, startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel, isXueZhu)
        {

        }

        //画左幅
        public void InsertBlockL(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, ArrayList EmergeStopWay, double Width_L_Emergency, double Lenth_L_Emergency, double LaneWidth, double LaneNum, int flag, int[] collisionState)
        {
            int[] EmergencyStopCar = new int[EmergeStopWay.Count];
            int m = 0;
            foreach (int insert in EmergeStopWay)
            {
                EmergencyStopCar[m++] = insert;
            }

            int l = 0;
            int tmp;
            switch (flag)
            {
                case 0:
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("EmergencySymbol");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("EmergencySymbol");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencySymbol.dwg", "EmergencySymbol");
                        tmp = getRightLocation(EmergencyStopCar[l] + Lenth_L_Emergency / 2 +5, 2, 1, EndX_L_Tunnel, collisionState);
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + LaneWidth * LaneNum -1, "EmergencySymbol", 1, 0);
                        DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel + LaneWidth * LaneNum, 0);
                        setCollisionList(tmp, 2,0);
                        setCollosion(tmp, 2, collisionState);
                        l++;
                    }
                    //把颜色改回黑白色
                    break;
                case 1:
                    //把颜色改回黑白色
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("jinjitingchetishi");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("jinjitingchetishi");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjitingchetishi.dwg", "jinjitingchetishi");
                        mxCt1.axMxDrawX1.DrawBlockReference(EmergencyStopCar[l++], StartY_L_Tunnel + LaneWidth * LaneNum + Width_L_Emergency + 7.5, "jinjitingchetishi", 1, 0);
                    }
                    break;
                case 2:
                    //把颜色改回黑白色
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("notice");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("notice");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/notice.dwg", "notice");
                        mxCt1.axMxDrawX1.DrawBlockReference(EmergencyStopCar[l++] - Lenth_L_Emergency / 2, StartY_L_Tunnel + LaneWidth * LaneNum - 2.5, "notice", 1, 0);
                    }
                    break;
                case 3:
                    //把颜色改回黑白色
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("gonggao");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("gonggao");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/gonggao.dwg", "gonggao");
                        mxCt1.axMxDrawX1.DrawBlockReference(EmergencyStopCar[l++] - 5, StartY_L_Tunnel + LaneWidth * LaneNum + Width_L_Emergency + 7.5, "gonggao", 1, 0);
                    }
                    break;
                default: Console.WriteLine(); break;

            }
      

           
        }










        //画右幅
        public void InsertBlockR(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, ArrayList EmergeStopWay, double Width_R_Emergency, double Lenth_R_Emergency, double LaneWidth, int flag, int[] collisionState)
        {

            int[] EmergencyStopCar = new int[EmergeStopWay.Count];
            System.Console.WriteLine("EmergeStopWay.Count is {0}", EmergeStopWay.Count);
            int m = 0;
            foreach (int insert in EmergeStopWay)
            {
                EmergencyStopCar[m] = insert;
                m++;
            }

            int l = 0;

             int tmp;
            switch (flag)
            {
                case 0:
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("EmergencySymbol");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("EmergencySymbol");
             
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencySymbol.dwg", "EmergencySymbol");
                        tmp = getRightLocation(EmergencyStopCar[l] - Lenth_R_Emergency / 2 - 5, 2, -1, EndX_R_Tunnel,collisionState);
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel+1, "EmergencySymbol", 1, 0);
                        DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel , 1);
                        setCollisionList(tmp, 2, 1);
                        setCollosion(tmp, 2, collisionState);
                        l++;
                    }
                    //把颜色改回黑白色
                    break;
                case 1:
                    //把颜色改回黑白色
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("jinjitingchetishi");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("jinjitingchetishi");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjitingchetishi.dwg", "jinjitingchetishi");
                        mxCt1.axMxDrawX1.DrawBlockReference(EmergencyStopCar[l++], StartY_R_Tunnel - Width_R_Emergency - 6, "jinjitingchetishi", 1, 0);
                    }
                    break;
                case 2:
                    //把颜色改回黑白色
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("notice");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("notice");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/notice.dwg", "notice");
                        mxCt1.axMxDrawX1.DrawBlockReference(EmergencyStopCar[l++] + Lenth_R_Emergency / 2, StartY_R_Tunnel - Width_R_Emergency - 2.5, "notice", 1, 0);
                    }
                    break;
                case 3:
                    //把颜色改回黑白色
                    mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                    //创建一个图层,名为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.AddLayer("gonggao");
                    //设置当前图层为"begin_tranportationLayer"
                    mxCt1.axMxDrawX1.LayerName = ("gonggao");
                    while (l < EmergencyStopCar.Length)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/gonggao.dwg", "gonggao");
                        mxCt1.axMxDrawX1.DrawBlockReference(EmergencyStopCar[l++] + 5, StartY_R_Tunnel - Width_R_Emergency - 5, "gonggao", 1, 0);
                    }
                    break;
                default: Console.WriteLine(); break;

            }

      
        }
    }

}



