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
    class EmergencyCall : CommunalAttributeData
    {
        //构造函数
        public EmergencyCall(double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel,int isXueZhu)
            : base(startX_R_Tunnel, startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel, startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel, isXueZhu)
        {

        }

        /*
       * 如果进入停车带，
       */
        public bool isInEmerg(int insert_X, double lenth_R_Emergency, int[] EmergencyStopCar, int[] flag)
        {
            int HalfOfEmergenceStopCar = DoutoInt(lenth_R_Emergency / 2);
            for (int i = 0; i < EmergencyStopCar.Length; i++)
            {
                if (EmergencyStopCar[i] - HalfOfEmergenceStopCar < insert_X && EmergencyStopCar[i] + HalfOfEmergenceStopCar > insert_X)
                {
                    flag[i] = 1;
                    return true;
                }

            }
            return false;

        }
        /*
      * 是否位于紧急停车带范围内
      */
        public bool isInEmerg(int insert_X, double lenth_R_Emergency, int[] EmergencyStopCar)
        {
            int HalfOfEmergenceStopCar = DoutoInt(lenth_R_Emergency / 2);
            for (int i = 0; i < EmergencyStopCar.Length; i++)
            {
                if (EmergencyStopCar[i] - HalfOfEmergenceStopCar < insert_X && EmergencyStopCar[i] + HalfOfEmergenceStopCar > insert_X)
                {
                    return true;
                }

            }
            return false;

        }

       
         int ShiftAll(int tmp, int gap, int begin_gap_broadcast, int broadcast_size, int[] collisionState,int endX_Tunnel, int []shiftAll)
         {
             int remain = (int)endX_Tunnel - begin_gap_broadcast - tmp;
            System.Console.WriteLine("remian in shift all  {0}",remain);
             if (remain >= 3 * gap)
             {
                 if (remain-3*gap >20)
                 {

                     shiftAll[0] = 4 * gap - remain;
                     return 3;
                     
                 }
                 else
                 {
                     
                     shiftAll[0] = 10;
                     return 2;
                 }
             }
             if (remain >= 2 * gap && remain < 3 * gap)
             {
                 if ( remain-2*gap >= 20)
                 {
                     shiftAll[0] = 3 * gap - remain;
                     return 2;
                 }
                 else
                 {
                     shiftAll[0] = 10;
                     return 1;
                 }
             }
             if (remain >=  gap && remain < 2 * gap)
             {
                 if (remain-gap >20)
                 {
                     shiftAll[0] = 2 * gap - remain;
                     return 1;
                 }
                 else
                 {
                     shiftAll[0] = 10;
                     return 0;
                 }
             }
             if (remain < gap) {
                 if (remain > 20){
                     shiftAll[0] = 25;
                     return 0 ;
                 }
                 else {
                     shiftAll[0] = 30;
                     return 0;
                 }
             }
             return -1;
         }
      /*
      * 寻找理论末尾
       */
         public int FindLocation(int loc, int gap, int broadcast_size, int begin_gap_broadcast, int begin_gap_call, int startX_Tunnel,int endX_Tunnel, int shift, int[] collisionState)
         {
             int loc_tmp = loc;
             int tmp = 0;
             int tmp2 = 0;
             int tmp1 = 0;
             while (loc_tmp + gap < endX_Tunnel - begin_gap_broadcast)
             {

                 tmp = getRightLocation(loc_tmp, broadcast_size, -(shift), endX_Tunnel, collisionState);
                 tmp1 = getRightLocation(tmp + gap, broadcast_size, (shift), endX_Tunnel, collisionState);
                 tmp2 = getRightLocation(tmp - gap, broadcast_size, -(shift), endX_Tunnel, collisionState);
                 loc_tmp = 3 * gap + tmp;
                
             }
             
             return tmp1;
          
         }

       //取实体对称位置
         public double getOppositeLocation(double insert_X, double startX_L_Tunnel, double endX_L_Tunnel)
         {
             return endX_L_Tunnel - insert_X + startX_L_Tunnel;
         }
      
        
         public void setLayer(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1)
         {
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //创建一个图层,名为"EmergencyCallLayer"
            mxCt1.axMxDrawX1.AddLayer("EmergencyCallLayer");
            //设置当前图层为"EmergencyCallLayer"
            mxCt1.axMxDrawX1.LayerName = ("EmergencyCallLayer");
        }
        
        
        
        //放置洞外紧急电话
         public void setOutEmergencyCall(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_Tunnel, double laneWidth, double laneNum, double startY_Tunnel, double endX_Tunnel)
         {
             //设置洞外小桩紧急电话
             mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCall.dwg", "outCall");
             mxCt1.axMxDrawX1.DrawBlockReference(startX_Tunnel , startY_Tunnel, "outCall", 1, 0);
             DrawEquipmentLocation(mxCt1, axMxDrawX1, startX_Tunnel, startY_Tunnel,1);

             //设置洞外大桩紧急电话
             mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCall.dwg", "outCall");
             mxCt1.axMxDrawX1.DrawBlockReference(endX_Tunnel, startY_Tunnel , "outCall", 1, 0);
             DrawEquipmentLocation(mxCt1, axMxDrawX1, endX_Tunnel, startY_Tunnel, 1);
         }

      
       //放置洞外的广播
         public void setOutBroadcast(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_Tunnel, double startY_Tunnel, double endX_Tunnel, double out_broadcast, double laneWidth, double laneNum, int isHaveCommuLoad)
         {
             if (isHaveCommuLoad == 0)
             {
                 mxCt1.axMxDrawX1.InsertBlock("sourceblock/outBroadcast.dwg", "outBroadcast");
                 mxCt1.axMxDrawX1.DrawBlockReference(out_broadcast, startY_Tunnel + laneWidth *laneNum, "outBroadcast", 1, 0);
                 DrawEquipmentLocation(mxCt1, axMxDrawX1, out_broadcast, startY_Tunnel ,0);
             }
        }


        //获取每个紧急车道初始位置
        public void getEmergencyLocation(ArrayList EmergeStopWay, int[] EmergencyStopCar) {

            int m = 0;
            foreach (int insert in EmergeStopWay)
            {
                EmergencyStopCar[m++] = insert;
            }

        }


        //画出首个广播
        public double  drawFirstBroadcast(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_R_Tunnel, double startY_Tunnel, double endX_Tunnel, double i_broadcast, double call_size, int shift, double laneWidth, double laneNum,int[] collisionState)
        {
           
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
            int tmp = getRightLocation(i_broadcast, call_size, shift, endX_Tunnel, collisionState);
            setCollosion(tmp, call_size, collisionState);
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, startY_Tunnel + laneWidth*laneNum, "Broadcast", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, startY_Tunnel , 0);
            return tmp;
        }

        //画出最后一个广播
        public void drawLastBroadcast(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double startX_R_Tunnel, double startY_Tunnel, double endX_Tunnel, double i_broadcast, double call_size, int shift, double laneWidth, int[] collisionState)
        {
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
            int tmp = getRightLocation(i_broadcast, call_size, shift, endX_Tunnel, collisionState);
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, startY_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, startY_Tunnel, 1);
            setCollosion(i_broadcast, call_size, collisionState);
        }


/*************************************************************************************************************************************/
        /*
         * By Yorn 画出左幅通道
         */
        public void InsertBlock_ClickL(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, int gap, double broadcast_size, double call_size, int shift, int begin_gap_call, double begin_gap_broadcast, int[] collisionState, ArrayList EmergeStopWay, double Width_L_Emergency, double Lenth_L_Emergency, double laneWidth, double laneNum,double out_Broadcast, int isHaveCommuLoad)
        {

            setLayer(mxCt1, axMxDrawX1);
            double tmp = 0;
            //设置洞外紧急电话
            {
                               
                //setOutBroadcast(mxCt1, axMxDrawX1, StartX_L_Tunnel, StartY_L_Tunnel, EndX_L_Tunnel, getOppositeLocation(out_broadcast, StartX_L_Tunnel, EndX_L_Tunnel), laneWidth, laneNum, isHaveCommuLoad);
                //设置洞外小桩紧急电话
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCall.dwg", "outCall");
                mxCt1.axMxDrawX1.DrawBlockReference(StartX_L_Tunnel-  out_Broadcast, StartY_L_Tunnel + laneWidth * laneNum, "outCall", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, StartX_L_Tunnel - out_Broadcast, StartY_L_Tunnel + laneWidth * laneNum , 0);

                //设置洞外大桩紧急电话
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCall.dwg", "outCall");
                mxCt1.axMxDrawX1.DrawBlockReference(EndX_L_Tunnel+out_Broadcast, StartY_L_Tunnel + laneWidth * laneNum, "outCall", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, EndX_L_Tunnel + out_Broadcast, StartY_L_Tunnel + laneWidth * laneNum , 0);
            
            
            
            }
           

            //画出洞内广播
            {
                double i_broadcast = EndX_L_Tunnel -begin_gap_broadcast;
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                tmp = getRightLocation(i_broadcast, call_size, -shift, EndX_L_Tunnel, collisionState);
                setCollosion(tmp, call_size, collisionState);
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3, "Broadcast", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel + laneWidth * laneNum , 0);
               
                tmp = getRightLocation(begin_gap_broadcast+StartX_L_Tunnel, call_size, -shift, EndX_L_Tunnel, collisionState);
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel+laneWidth * laneNum*2/3, "Broadcast", 1, 0);
                DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel + laneWidth * laneNum , 0);
                setCollosion(tmp, call_size, collisionState);
            }


            //初始化flag数组
            int[] EmergencyStopCar = new int[EmergeStopWay.Count];
            getEmergencyLocation(EmergeStopWay, EmergencyStopCar);
            int[] flag = new int[EmergencyStopCar.Length];
            for (int i = 0; i < EmergencyStopCar.Length; i++)
            {
                flag[i] = 0;

            }
           
            
            //获取平移数值
            int loc = (int)EndX_L_Tunnel -begin_gap_call;
            int[] a = new int[1];
            double lastLoc = FindLocation(((int)StartX_L_Tunnel + (int)begin_gap_call), gap, (int)broadcast_size, (int)begin_gap_broadcast, begin_gap_call, (int)StartX_L_Tunnel, (int)EndX_L_Tunnel, shift, collisionState);
                     
            int num = ShiftAll((int) lastLoc, gap, (int)begin_gap_broadcast, (int)broadcast_size, collisionState,(int)EndX_L_Tunnel, a);
            int pingyi = a[0];
            System.Console.WriteLine("the shift in L {0}",pingyi);
            int tmp2 = 0;
            int tmp1 = 0;
            int tmp3 = 0;

            System.Console.WriteLine("loop");
            while (loc -gap >StartX_L_Tunnel +begin_gap_broadcast)
            {

                tmp = getRightLocation(loc, call_size, (shift), EndX_R_Tunnel, collisionState);
                tmp3 = getRightLocation(tmp+pingyi, call_size, (shift), EndX_R_Tunnel, collisionState);

                //若放置电话位于紧急车道，flag置1
                isInEmerg(tmp3, Lenth_L_Emergency, EmergencyStopCar, flag);

                //画图
                if (!isInEmerg(tmp3, Lenth_L_Emergency, EmergencyStopCar))
                {

                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_L_Tunnel + laneWidth * laneNum-1, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_L_Tunnel+ laneWidth * laneNum*2 /3, "Broadcast", 1, 0);
                    setCollosion(tmp3, call_size, collisionState);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp3, StartY_L_Tunnel + laneWidth * laneNum , 0);
                }
                else
                {

                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_L_Tunnel + laneWidth * laneNum + Width_L_Emergency - 1, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3 + Width_L_Emergency, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp3, StartY_L_Tunnel  + laneWidth * laneNum + Width_L_Emergency, 0);
                    setCollosion(tmp3, call_size, collisionState);
                }
                //画各个广播
                {
                    //画出实际左广播
                    tmp1 = getRightLocation(tmp3 +gap, broadcast_size, -(shift), EndX_R_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp1, StartY_L_Tunnel + laneWidth * laneNum * 2 /3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp1, StartY_L_Tunnel + laneWidth * laneNum,0);
                    setCollosion(tmp1, call_size, collisionState);
                    //画出实际右广播
                    tmp2 = getRightLocation(tmp3-gap, broadcast_size, +(shift), EndX_R_Tunnel, collisionState);
                    // tmp3 = getRightLocation(tmp2, broadcast_size, (shift), EndX_R_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp2, StartY_L_Tunnel + laneWidth * laneNum*2/3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp2, StartY_L_Tunnel + laneWidth * laneNum ,0);
                    setCollosion(tmp2, call_size, collisionState);
                }
                //计算理论位置（未平移）
                loc = (int)(-3 * gap + tmp);

            }
            for (int n = 0; n < flag.Length; n++)
            {
                if (flag[n] == 0)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(getRightLocation(EmergencyStopCar[n]-10, call_size, shift, EndX_L_Tunnel, collisionState), StartY_L_Tunnel + laneWidth * laneNum + Width_L_Emergency - 1, "EmergencyCall", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, getRightLocation(EmergencyStopCar[n] -Width_L_Emergency/2, call_size, shift, EndX_R_Tunnel, collisionState), StartY_L_Tunnel + laneWidth * laneNum + Width_L_Emergency ,0);
                    setCollosion(getRightLocation(EmergencyStopCar[n] - 10, call_size, shift, EndX_L_Tunnel, collisionState), call_size, collisionState);
                }

            }
            System.Console.WriteLine("the num is{0}",num);
            switch (num)
            {
                case 1:
                    {

                        tmp = getRightLocation(tmp2 - gap, broadcast_size, +shift, EndX_R_Tunnel, collisionState);
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum- 1, "EmergencyCall", 1, 0);
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3, "Broadcast", 1, 0);
                        DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel + laneWidth * laneNum , 0);
                        setCollosion(tmp, call_size, collisionState);
                    }

                    break;
                case 2:

                    tmp = getRightLocation(tmp2 - gap * 2, broadcast_size, shift, EndX_L_Tunnel, collisionState);
                    System.Console.WriteLine("the resule 6 tmp is{0}", tmp);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum - 1, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel+laneWidth * laneNum , 0);
                    setCollosion(tmp, call_size, collisionState);
                    //画右广播
                    tmp = getRightLocation(tmp + gap, broadcast_size, (-shift), EndX_L_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel + laneWidth * laneNum , 0);
                    setCollosion(tmp, call_size, collisionState);

                    break;

                case 3:

                    tmp = getRightLocation(tmp2 - gap * 2, broadcast_size, shift, EndX_L_Tunnel, collisionState);
                    System.Console.WriteLine("tmpA{0}", tmp);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum - 1, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_L_Tunnel + laneWidth * laneNum ,0);
                    setCollosion(tmp, call_size, collisionState);
                    //画左广播
                    tmp1 = getRightLocation(tmp - gap, broadcast_size, (shift), EndX_L_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp1, StartY_L_Tunnel + laneWidth * laneNum * 2 / 3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp1, StartY_L_Tunnel + laneWidth * laneNum ,0);
                    setCollosion(tmp1, call_size, collisionState);
                    //画右广播

                    tmp2 = getRightLocation(tmp + gap, broadcast_size, (-shift), EndX_L_Tunnel, collisionState);

                    mxCt1.axMxDrawX1.DrawBlockReference(tmp2, StartY_L_Tunnel + laneWidth * laneNum * 2 /3, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp2, StartY_L_Tunnel + laneWidth * laneNum , 0);
                    setCollosion(tmp2, call_size, collisionState);

                    break;
            }




        }






        /*
         * By Yorn 画出右幅通道
         */
         public void InsertBlock_ClickR(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, int gap, double broadcast_size, double call_size, int shift, int begin_gap_call, double begin_gap_broadcast, int[] collisionState, ArrayList EmergeStopWay, double Width_R_Emergency, double Lenth_R_Emergency, double laneWidth, double laneNum,double out_gap_camara, int isHaveCommuLoad)
        {
            double  tmp = 0;
            setLayer(mxCt1, axMxDrawX1);

            //设置洞外紧急电话
            {
                double startX_R_Tunnel = StartX_R_Tunnel - 5;
                double endX_R_Tunnel = EndX_R_Tunnel + 5;
                setOutEmergencyCall(mxCt1, axMxDrawX1, startX_R_Tunnel, laneWidth,  laneNum,StartY_R_Tunnel, endX_R_Tunnel);
            
            }

            {
                double out_broadcast = StartX_R_Tunnel - out_gap_camara;
                setOutBroadcast(mxCt1, axMxDrawX1, StartX_R_Tunnel, StartY_R_Tunnel, EndX_R_Tunnel, out_broadcast, laneWidth, laneNum, isHaveCommuLoad);
            
            }

         


            //设置洞内紧急电话


            //画出首个广播

            double i_broadcast = StartX_R_Tunnel + begin_gap_broadcast;
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
             tmp = getRightLocation(i_broadcast, call_size, -(shift), EndX_R_Tunnel, collisionState);

            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneWidth/2, "Broadcast", 1, 0);
            DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel, 1);
            
           


            //画出末尾广播
            {
                i_broadcast = EndX_R_Tunnel - begin_gap_broadcast;
                drawLastBroadcast(mxCt1, axMxDrawX1, StartX_R_Tunnel, StartY_R_Tunnel, EndX_R_Tunnel, i_broadcast, call_size, shift, laneWidth, collisionState);
      
            }
           
           




            //每隔150米放置一个紧急电话
            int loc = (int)StartX_R_Tunnel + begin_gap_call;
            int[] a = new int[1];
            int lastLoc = FindLocation(((int)StartX_R_Tunnel + (int)begin_gap_call), gap, (int)broadcast_size, (int)begin_gap_broadcast, begin_gap_call, (int)StartX_R_Tunnel, (int)EndX_R_Tunnel, shift, collisionState);
            int num = ShiftAll(lastLoc, gap, (int)begin_gap_broadcast, (int)broadcast_size, collisionState,(int)EndX_R_Tunnel,a);
            int pingyi = a[0];
            System.Console.WriteLine("the painyi  is{0}", pingyi);

            //获取每个紧急停车道的初始位置&&flag初始化
            
                int[] EmergencyStopCar = new int[EmergeStopWay.Count];
                getEmergencyLocation(EmergeStopWay, EmergencyStopCar);
                int[] flag = new int[EmergencyStopCar.Length];
                for (int i = 0; i < EmergencyStopCar.Length; i++)
                {
                    flag[i] = 0;

                }
  

            int tmp2 = 0;
            int tmp1 = 0;
            int tmp3 = 0;
            while (loc + gap < EndX_R_Tunnel - begin_gap_broadcast)
            {


                tmp = getRightLocation(loc, call_size, (-shift), EndX_R_Tunnel, collisionState);
                tmp3 = getRightLocation(tmp - pingyi, call_size, (-shift), EndX_R_Tunnel, collisionState);

                //若放置电话位于紧急车道，flag置1
                isInEmerg(tmp3, Lenth_R_Emergency, EmergencyStopCar, flag);


                //画图
                if (!isInEmerg(tmp3, Lenth_R_Emergency, EmergencyStopCar))
                {

                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_R_Tunnel + 2, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);

                    setCollosion(tmp3, call_size, collisionState);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp3, StartY_R_Tunnel, 1);
                }
                else
                {
                    // System.Console.WriteLine("draw loc is{0}", tmp3);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_R_Tunnel - Width_R_Emergency + broadcast_size, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp3, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp3, StartY_R_Tunnel - Width_R_Emergency, 1);

                    setCollosion(tmp3, call_size, collisionState);
                }
                //画各个广播
                {
                    //画出实际左广播
                    tmp1 = getRightLocation(tmp3 + gap, broadcast_size, -(shift), EndX_R_Tunnel, collisionState);
                    // tmp3 = getRightLocation(tmp1, broadcast_size, -(shift), EndX_R_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp1, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp1, StartY_R_Tunnel, 1);
                    setCollosion(tmp1, call_size, collisionState);
                    //画出实际右广播
                    tmp2 = getRightLocation(tmp3 - gap, broadcast_size, +(shift), EndX_R_Tunnel, collisionState);
                    // tmp3 = getRightLocation(tmp2, broadcast_size, (shift), EndX_R_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp2, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp2, StartY_R_Tunnel, 1);
                    setCollosion(tmp2, call_size, collisionState);
                }
                //计算理论位置（未平移）
                loc = (int)(3 * gap + tmp);


            }
            //判断最后剩余多少距离

            switch (num)
            {
                case 1:
                    {

                        tmp = getRightLocation(tmp1 + gap, broadcast_size, -shift, EndX_R_Tunnel, collisionState);
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel - Width_R_Emergency + broadcast_size, "EmergencyCall", 1, 0);
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                        DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel - Width_R_Emergency, 1);
                        setCollosion(tmp, call_size, collisionState);
                    }

                    break;
                case 2:

                    tmp = getRightLocation(tmp1 + gap * 2, broadcast_size, -shift, EndX_R_Tunnel, collisionState);
                    System.Console.WriteLine("the resule 6 tmp is{0}", tmp);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + broadcast_size, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel - Width_R_Emergency, 1);
                    setCollosion(tmp, call_size, collisionState);
                    //画左广播
                    tmp = getRightLocation(tmp - gap, broadcast_size, (shift), EndX_R_Tunnel, collisionState);
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel, 1);
                    setCollosion(tmp, call_size, collisionState);

                    break;

                case 3:

                    tmp = getRightLocation(tmp1 + gap * 2, broadcast_size, -shift, EndX_R_Tunnel, collisionState);
                    System.Console.WriteLine("tmpA{0}", tmp);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + broadcast_size, "EmergencyCall", 1, 0);
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/broadcast.dwg", "Broadcast");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp, StartY_R_Tunnel - Width_R_Emergency, 1);
                    setCollosion(tmp, call_size, collisionState);
                    //画左广播
                    tmp1 = getRightLocation(tmp - gap, broadcast_size, (shift), EndX_R_Tunnel, collisionState);

                    mxCt1.axMxDrawX1.DrawBlockReference(tmp1, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp1, StartY_R_Tunnel, 1);
                    setCollosion(tmp1, call_size, collisionState);
                    //画右广播

                    tmp2 = getRightLocation(tmp + gap, broadcast_size, (-shift), EndX_R_Tunnel, collisionState);

                    mxCt1.axMxDrawX1.DrawBlockReference(tmp2, StartY_R_Tunnel + laneWidth / 2, "Broadcast", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, tmp2, StartY_R_Tunnel, 1);
                    setCollosion(tmp2, call_size, collisionState);

                    break;
            }


            for (int n = 0; n < flag.Length; n++)
            {
                if (flag[n] == 0)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/emergencycall.dwg", "EmergencyCall");
                    mxCt1.axMxDrawX1.DrawBlockReference(getRightLocation(EmergencyStopCar[n] - 10, call_size, shift, EndX_R_Tunnel, collisionState), StartY_R_Tunnel - Width_R_Emergency + broadcast_size, "EmergencyCall", 1, 0);
                    DrawEquipmentLocation(mxCt1, axMxDrawX1, getRightLocation(EmergencyStopCar[n] - 10, call_size, shift, EndX_R_Tunnel, collisionState), StartY_R_Tunnel - Width_R_Emergency + broadcast_size, 1);
                    setCollosion(getRightLocation(EmergencyStopCar[n] - 10, call_size, shift, EndX_R_Tunnel, collisionState), call_size, collisionState);
                }

            }
            System.Console.WriteLine("out of R");
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }



    }
}

