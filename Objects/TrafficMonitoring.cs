using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace WPFDrawCAD.Objects
{
    class TrafficMonitoring: CommunalAttributeData
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
        public TrafficMonitoring(double laneNum, double laneWidth, double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel, int isXueZhu)
            : base(startX_R_Tunnel, startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel, startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel, isXueZhu)
        {
            this.LaneNum = laneNum;
            this.LaneWidth = laneWidth;

        }


        /*
         * 1,三车道指示器 
         * 2，车辆检测器
         * 3，洞外交通信号灯
         */
        public void Laneindicator_Draw_R(MxDrawControl mxCt1, int size, int shift, int[] collisionState, int[] collisionState_R_C)
        {
            
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;

            //创建一个图层,名为"TrafficMonitoring"
            mxCt1.axMxDrawX1.AddLayer("TrafficMonitoring");

            //设置当前图层为"TrafficMonitoring"
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
            int tmps = getRightLocation(StartX_R_Tunnel + 2, size, shift*-1, EndX_R_Tunnel, collisionState);//入口车道指示器
            int tmpscheliangjiance = getRightLocation(tmps+2, size, shift*-1, EndX_R_Tunnel, collisionState);//入口车辆检测器
            int tmpe = getRightLocation(EndX_R_Tunnel - 4, size, shift, EndX_R_Tunnel, collisionState);//出口车道指示器
            int tmpecheliangjiance = getRightLocation(tmpe + 2, size, shift, EndX_R_Tunnel, collisionState);//出口车辆检测器


            //画可变限速标志
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/kebianxiansu.dwg", "kebianxiansu");
            mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel - 150, StartY_R_Tunnel - 5, "kebianxiansu", 1, 0);
            

            for (int i = 0; i < laneNum; i++)
            {
               
                //画三道指示器
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/santaichedaozhishiqi.dwg", "santaichedao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmps, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "santaichedao", 1, 270);
                mxCt1.axMxDrawX1.DrawBlockReference(tmpe, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "santaichedao", 1, 270);
                //画车辆检测器
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/xianquanxingchedao.dwg", "xianquanxingchedao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmpscheliangjiance, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "xianquanxingchedao", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(tmpecheliangjiance, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "xianquanxingchedao", 1, 0);

                //洞外交通信号灯距离出口150米
                if (i == 0&&laneNum==3)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/scdongwaikebian.dwg", "scdongwaikebian");
                    if (IsXueZhu == 1)
                    {
                        mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel-10 - 150, StartY_R_Tunnel + 30, "scdongwaikebian", 1, 270);
                    }
                    else {
                        mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel - 150, StartY_R_Tunnel + 30, "scdongwaikebian", 1, 270);
                    }
                    
                }
                else if (i == 0 && laneNum == 2)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/lcdongwaikebian.dwg", "lcdongwaikebian");
                    mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel - 150, StartY_R_Tunnel + 20, "lcdongwaikebian", 1, 270);

                }
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaotongxinhaodeng.dwg", "jiaotongxinhaodeng");
                mxCt1.axMxDrawX1.DrawBlockReference(StartX_R_Tunnel-155, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "jiaotongxinhaodeng", 1, 270);
                
            }
            //标注右幅入口车道
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmps, StartY_R_Tunnel, 1);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpscheliangjiance, StartY_R_Tunnel, 1);
           //如下两行为车辆指示器和车辆检测器加入碰撞检测
            setCollosion(tmps, size, collisionState);
            setCollosion(tmpscheliangjiance, size, collisionState);
            //首部加入中间碰撞数组
            setCollosion(tmps, size, collisionState_R_C);
            //标注右幅出口车道
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpe, StartY_R_Tunnel, 1);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpecheliangjiance, StartY_R_Tunnel, 1);
            //如下两行为车辆检测器和车辆指示器加入碰撞检测
            setCollosion(tmpe, size, collisionState);
            setCollosion(tmpecheliangjiance, size, collisionState);
            //尾部加入中间碰撞数组
            setCollosion(tmpe, size, collisionState_R_C);

            //标注交通信号灯
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_R_Tunnel - 150, StartY_R_Tunnel, 1);
            
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
          
            
        }


        /*
         * 画人行横道和车行横到指示器
         * gap 为通道之间的间距
         * mandirector 人行横道指示器的大小
         * 画区域控制单元
         */
        public void Laneindicator_In_Draw_R(MxDrawControl mxCt1, double shift, double mandirector, double cardirector, double Two_size, double Three_size, int[] collisionState, int[] collisionState_R_H, List<HengTongDao> Location, int[] collisionState_R_C, int QuyuSize, double lenth_R_Emergency, ArrayList EmergencyStopCar)
        {

            //画入口和出口的区域控制单元
            int quyu_start = getRightLocation(StartX_R_Tunnel + 6, QuyuSize, shift * -1, EndX_R_Tunnel, collisionState);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
            mxCt1.axMxDrawX1.DrawBlockReference(quyu_start, StartY_R_Tunnel+1, "quyukongzhi", 1, 0);
            //区域控制单元入口处加入碰撞检测
            setCollosion(quyu_start, QuyuSize, collisionState);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, quyu_start, StartY_R_Tunnel, 1);

            int quyu_end = getRightLocation(EndX_R_Tunnel - 6, QuyuSize, shift, EndX_R_Tunnel, collisionState);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
            mxCt1.axMxDrawX1.DrawBlockReference(quyu_start, StartY_R_Tunnel+1, "quyukongzhi", 1, 0);
            //区域控制单元出口处加入碰撞检测
            setCollosion(quyu_end, QuyuSize, collisionState);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, quyu_end, StartY_R_Tunnel, 1);

            foreach (HengTongDao htd in Location)
            {
                //画连通道中心桩号区域控制单元
                int Quyu_Hengtongdao_Center = getRightLocation(htd.HengTongDao_R, QuyuSize, shift, EndX_R_Tunnel, collisionState);
                if (isInEmerg(DoutoInt(Quyu_Hengtongdao_Center), lenth_R_Emergency, EmergencyStopCar))
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
                    mxCt1.axMxDrawX1.DrawBlockReference(Quyu_Hengtongdao_Center, StartY_R_Tunnel-5 + 1, "quyukongzhi", 1, 0);
                    //区域控制单元入口处加入碰撞检测
                    setCollosion(Quyu_Hengtongdao_Center, QuyuSize, collisionState);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, Quyu_Hengtongdao_Center, StartY_R_Tunnel-5, 1);
                }
                else {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
                    mxCt1.axMxDrawX1.DrawBlockReference(Quyu_Hengtongdao_Center, StartY_R_Tunnel + 1, "quyukongzhi", 1, 0);
                    //区域控制单元入口处加入碰撞检测
                    setCollosion(Quyu_Hengtongdao_Center, QuyuSize, collisionState);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, Quyu_Hengtongdao_Center, StartY_R_Tunnel, 1);
                }
               

                InsertBlock_R(mxCt1, htd.HengTongDao_R, shift, htd.HengTongDao_Man, mandirector, cardirector, Two_size, Three_size, collisionState, collisionState_R_H, collisionState_R_C);
            }
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }
      

        /*
      * 插入车道指示器
         * flag== 0 为人行
         * flag== 1 为车行
         * Two_size 两车道标志
         * Three_size 三车道标志大小
      * 插入单位为米
      */
        public void InsertBlock_R(MxDrawControl mxCt1, double location, double shift, int flag, double mandirector, double cardirector, double Two_size, double Three_size, int[] collisionState, int[] collisionState_R_H, int[] collisionState_R_C)
        {
           
            //设置当前图层为"TrafficMonitoring"
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
            if (flag == 0)
            {
                int tmpm = 0;
                for (int i = 0; i < laneNum; i++)
                {
                    tmpm = getRightLocation(location-5, Two_size, shift, EndX_R_Tunnel, collisionState);
                   
                    //画两道指示器
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/liangtaichedaozhishiqi.dwg", "liangtaichedaozhishiqi");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmpm, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "liangtaichedaozhishiqi", 0.7, 90);

                }
                //两车道指示器加入中间碰撞检测
                setCollosion(tmpm, Two_size, collisionState_R_C);

                //画人行横道指示器，并且加入碰撞检测，用的碰撞数组为collisionState_R_H
                int tmpmandirector = getRightLocation(location - 10, mandirector, shift, EndX_R_Tunnel, collisionState_R_H);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/renxinghengdao.dwg", "renxinghengdao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmpmandirector, StartY_R_Tunnel + laneWidth * laneNum-2, "renxinghengdao", 1, 0);
                setCollosion(tmpmandirector, mandirector, collisionState_R_H);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpmandirector, StartY_R_Tunnel + laneWidth * laneNum , 0);
                
                //两车道指示器加入右幅下侧碰撞数组
                setCollosion(tmpm, Two_size, collisionState);
                //标注两车道指示器
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpm, StartY_R_Tunnel, 1);
                //标注右幅出口车道
            }
            else if (flag == 1)
            {
                int  tmpc = getRightLocation(location - 10, Three_size, shift, EndX_R_Tunnel, collisionState);
                int tmpccheliangjiance = getRightLocation(tmpc - 5, Three_size, shift, EndX_R_Tunnel, collisionState);

                for (int i = 0; i < laneNum; i++)
                {
                    
                    //画三车道指示器
                    if (i == 0 && laneNum == 2) {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/lcdongneikebian.dwg", "lcdongneikebian");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmpc + 5, StartY_R_Tunnel + 20, "lcdongneikebian", 1.23, 270);
                    }
                    else if (i == 0 && laneNum == 3) {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/scdongneikebian.dwg", "scdongneikebian");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmpc + 5, StartY_R_Tunnel + 30, "scdongneikebian", 1.23, 270);
                    }
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/santaichedaozhishiqi.dwg", "santaichedao");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmpc, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "santaichedao", 1, 270);
                    //车辆检测器
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/xianquanxingchedao.dwg", "xianquanxingchedao");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmpccheliangjiance, StartY_R_Tunnel + laneWidth * i + laneWidth / 2, "xianquanxingchedao", 1, 0);
                }
                //三车道指示器加入中间碰撞检测数组
                setCollosion(tmpc, Three_size, collisionState_R_C);

                //画车行横道指示器，并且加入碰撞检测，用的碰撞数组为collisionState_R_H
                int tmpcardirector = getRightLocation(location - 10, cardirector, shift, EndX_R_Tunnel, collisionState_R_H);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/chexinghengdao.dwg", "chexinghengdao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmpcardirector, StartY_R_Tunnel + laneWidth * laneNum - 2, "chexinghengdao", 1, 0);
                setCollosion(tmpcardirector, cardirector, collisionState_R_H);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpcardirector, StartY_R_Tunnel + laneWidth * laneNum, 0);

                //标注三车道指示器
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpc, StartY_R_Tunnel, 1);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpccheliangjiance, StartY_R_Tunnel, 1);
                setCollosion(tmpc, Three_size, collisionState);//三车道指示器加入碰撞检测
                setCollosion(tmpccheliangjiance, Three_size, collisionState);//车辆检测器加入碰撞检测
            }
        }






        //标注视频监控 by Yorn
        public void InsertCamara_R(MxDrawControl mxCt1, double camara_size, double gap, double shift, double out_gap_camara, double begin_gap, int[] collisionState, int isHaveCommuLoad, double lenth_R_Emergency, ArrayList EmergencyStopCar)
        {
            //设置当前图层为"TrafficMonitoring"
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
            //设置洞外监控
            double tmp = StartX_R_Tunnel - out_gap_camara;
            if (isHaveCommuLoad == 0)
            {
                mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCamara.dwg", "outCamara");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + camara_size / 2, "outCamara", 1, 0);
                mxCt1.axMxDrawX1.LayerName = ("SwitchEquipmentLayer");
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/monitor.dwg", "Monitor");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + camara_size / 2 + 4, "Monitor", 1, 0);
            }
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
            //判断间隔是否输入异常
            if (gap < 100 || gap > 150) throw new Exception("很抱歉，摄像头间隔应该在100-150，您可能输入有误");

            //设置洞内监控
            tmp = begin_gap + StartX_R_Tunnel;
            while (tmp < EndX_R_Tunnel - gap)
            {

                tmp = getRightLocation(tmp, camara_size, shift, EndX_R_Tunnel, collisionState);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/inCamara.dwg", "inCamara");
                if (isInEmerg(DoutoInt(tmp), lenth_R_Emergency, EmergencyStopCar))
                {
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 1.8 - 5, "inCamara", 0.5, 0);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel - 5,1);
                }
                else
                {
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 1.8, "inCamara", 0.5, 0);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel, 1);
                }
                setCollosion(tmp, camara_size, collisionState);
                tmp += gap;
            }

            tmp = getRightLocation(tmp, camara_size, -shift, EndX_R_Tunnel, collisionState);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/inCamara.dwg", "inCamara");
            if (isInEmerg(DoutoInt(tmp), lenth_R_Emergency, EmergencyStopCar))
            {
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 1.8 - 5, "inCamara", 0.5, 0);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel - 5, 1);
            }
            else
            {
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 1.8, "inCamara", 0.5, 0);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel, 1);
            }
            setCollosion(tmp, camara_size, collisionState);
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();

        }
                

                
/*
 * *******************************************************************************画左幅设备************************************************************************************************
 */


        /*
         * 1,三车道指示器 
         * 2，车辆检测器
         * 3，洞外交通信号灯
         */
        public void Laneindicator_Draw_L(MxDrawControl mxCt1, int size, int shift, int[] collisionState, int[] collisionState_L_C)
        {

            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;

            //创建一个图层,名为"TrafficMonitoring"
            mxCt1.axMxDrawX1.AddLayer("TrafficMonitoring");

            //设置当前图层为"TrafficMonitoring"
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");

            //画可变限速标志
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/kebianxiansu.dwg", "kebianxiansu");
            mxCt1.axMxDrawX1.DrawBlockReference(EndX_L_Tunnel + 150, StartY_L_Tunnel+laneWidth*laneNum + 5, "kebianxiansu", 1, 0);

            int tmps = 0;
            int tmpe = 0;
            tmps = getRightLocation(EndX_L_Tunnel - 2, size, shift*-1, EndX_L_Tunnel, collisionState);
            int tmpscheliangjiance = getRightLocation(tmps - 2, size, shift*-1, EndX_L_Tunnel, collisionState);//入口车辆检测器
            tmpe = getRightLocation(StartX_L_Tunnel + 4, size, shift, EndX_L_Tunnel, collisionState);
            int tmpecheliangjiance = getRightLocation(tmpe - 2, size, shift, EndX_L_Tunnel, collisionState);//出口车辆检测器
            for (int i = 0; i < laneNum; i++)
            {
                //画三道指示器
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/santaichedaozhishiqi.dwg", "santaichedao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmps, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "santaichedao", 0.7, 90);
                mxCt1.axMxDrawX1.DrawBlockReference(tmpe, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "santaichedao", 0.7, 90);
                //画车辆检测器
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/xianquanxingchedao.dwg", "xianquanxingchedao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmpscheliangjiance, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "xianquanxingchedao", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(tmpecheliangjiance, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "xianquanxingchedao", 1, 0);

                //洞外交通信号灯距离出口150米
                if (i == 0 && laneNum == 3)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/scdongwaikebian.dwg", "scdongwaikebian");
                    if (IsXueZhu == 1)
                    {
                        mxCt1.axMxDrawX1.DrawBlockReference(EndX_L_Tunnel+10 + 150, StartY_L_Tunnel + 30, "scdongwaikebian", 1, 90);
                    }
                    else {
                        mxCt1.axMxDrawX1.DrawBlockReference(EndX_L_Tunnel + 150, StartY_L_Tunnel + 30, "scdongwaikebian", 1, 90);
                    }
                    
                }
                else if (i == 0 && laneNum == 2)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/lcdongwaikebian.dwg", "lcdongwaikebian");
                    mxCt1.axMxDrawX1.DrawBlockReference(EndX_L_Tunnel + 150, StartY_L_Tunnel + 20, "lcdongwaikebian", 1, 90);

                }
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaotongxinhaodeng.dwg", "jiaotongxinhaodeng");
                mxCt1.axMxDrawX1.DrawBlockReference(EndX_L_Tunnel + 155, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "jiaotongxinhaodeng", 1, 90);

            }
            //标注左幅入口车道
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmps, StartY_L_Tunnel+laneNum*laneWidth ,0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpscheliangjiance, StartY_L_Tunnel + laneNum * laneWidth , 0);
            setCollosion(tmps, size, collisionState);
            setCollosion(tmpscheliangjiance, size, collisionState);
            //首部加入中间碰撞数组
            setCollosion(tmps, size, collisionState_L_C);

            //标注右幅出口车道
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpe, StartY_L_Tunnel + laneNum * laneWidth , 0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpecheliangjiance, StartY_L_Tunnel + laneNum * laneWidth , 0);
            setCollosion(tmpe, size, collisionState);
            setCollosion(tmpecheliangjiance, size, collisionState);//车辆检测器加入碰撞检测
            //尾部加入中间碰撞数组
            setCollosion(tmpe, size, collisionState_L_C);

            //标注交通信号灯
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, EndX_L_Tunnel + 150, StartY_L_Tunnel + laneNum * laneWidth , 0);
            
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();

        }


        /*
         * 画人行横道和车行横到指示器
         * gap 为通道之间的间距
         * mandirector 人行横道指示器的大小
         */
        public void Laneindicator_In_Draw_L(MxDrawControl mxCt1, double shift, double mandirector, double cardirector, double Two_size, double Three_size, int[] collisionState_L_H, int[] collisionState_L_L, List<HengTongDao> Location, int[] collisionState_L_C, int QuyuSize, double lenth_L_Emergency, ArrayList EmergencyStopCar)
        {
            //画入口和出口的区域控制单元
            int quyu_start = getRightLocation(EndX_L_Tunnel - 6, QuyuSize, shift * -1, EndX_L_Tunnel, collisionState_L_H);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
            mxCt1.axMxDrawX1.DrawBlockReference(quyu_start, StartY_L_Tunnel + laneNum*laneWidth-1, "quyukongzhi", 1, 0);
            //区域控制单元入口处加入碰撞检测
            setCollosion(quyu_start, QuyuSize, collisionState_L_H);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, quyu_start, StartY_L_Tunnel + laneNum * laneWidth ,0);

            int quyu_end = getRightLocation(StartX_L_Tunnel + 6, QuyuSize, shift, EndX_L_Tunnel, collisionState_L_H);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
            mxCt1.axMxDrawX1.DrawBlockReference(quyu_start, StartY_L_Tunnel + laneWidth*laneNum-1, "quyukongzhi", 1, 0);
            //区域控制单元出口处加入碰撞检测
            setCollosion(quyu_end, QuyuSize, collisionState_L_H);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, quyu_end, StartY_L_Tunnel + laneWidth * laneNum ,0);

            foreach (HengTongDao htd in Location)
            {
                //画连通道中心桩号区域控制单元
                int Quyu_Hengtongdao_Center = getRightLocation(htd.HengTongDao_L, QuyuSize, shift, EndX_L_Tunnel, collisionState_L_H);
                if (isInEmerg(DoutoInt(Quyu_Hengtongdao_Center), lenth_L_Emergency, EmergencyStopCar))
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
                    mxCt1.axMxDrawX1.DrawBlockReference(Quyu_Hengtongdao_Center, StartY_L_Tunnel +5 - 1+laneNum*laneWidth, "quyukongzhi", 1, 0);
                    //区域控制单元入口处加入碰撞检测
                    setCollosion(Quyu_Hengtongdao_Center, QuyuSize, collisionState_L_H);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, Quyu_Hengtongdao_Center, StartY_L_Tunnel + 5  + laneNum * laneWidth,0);
                }
                else
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/quyukongzhi.dwg", "quyukongzhi");
                    mxCt1.axMxDrawX1.DrawBlockReference(Quyu_Hengtongdao_Center, StartY_L_Tunnel + laneNum * laneWidth-1, "quyukongzhi", 1, 0);
                    //区域控制单元入口处加入碰撞检测
                    setCollosion(Quyu_Hengtongdao_Center, QuyuSize, collisionState_L_H);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, Quyu_Hengtongdao_Center, StartY_L_Tunnel + laneNum * laneWidth,0);
                }

                InsertBlock_L(mxCt1, htd.HengTongDao_L, shift, htd.HengTongDao_Man, mandirector, cardirector, Two_size, Three_size, collisionState_L_H, collisionState_L_L, collisionState_L_C);
            }
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }


        /*
      * 插入车道指示器
         * flag== 0 为人行
         * flag== 1 为车行
         * Two_size 两车道标志
         * Three_size 三车道标志大小
      * 插入单位为米
      */
        public void InsertBlock_L(MxDrawControl mxCt1, double location, double shift, int flag, double mandirector, double cardirector, double Two_size, double Three_size, int[] collisionState_L_H, int[] collisionState_L_L, int[] collisionState_L_C)
        {
            //设置当前图层为"TrafficMonitoring"
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
            if (flag == 0)
            {
                int tmpm = 0;
                for (int i = 0; i < laneNum; i++)
                {
                    tmpm = getRightLocation(location + 5, Two_size, shift, EndX_L_Tunnel, collisionState_L_H);

                    //画两道指示器
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/liangtaichedaozhishiqi.dwg", "liangtaichedaozhishiqi");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmpm, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "liangtaichedaozhishiqi", 0.7, 270);

                }
                //两车道指示器加入中间碰撞检测
                setCollosion(tmpm, Two_size, collisionState_L_C);

                //画人行横道指示器，并且加入碰撞检测，用的碰撞数组为collisionState_L_L
                int tmpmandirector = getRightLocation(location + 10, mandirector, shift, EndX_L_Tunnel, collisionState_L_L);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/renxinghengdao.dwg", "renxinghengdao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmpmandirector, StartY_L_Tunnel +  2, "renxinghengdao", 1, 0);
                setCollosion(tmpmandirector, mandirector, collisionState_L_L);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpmandirector, StartY_L_Tunnel , 1);

                //两车道指示器加入左幅上侧碰撞数组
                setCollosion(tmpm, Two_size, collisionState_L_H);
                //标注两车道指示器
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpm, StartY_L_Tunnel+laneWidth*laneNum,0);
            }
            else if (flag == 1)
            {
                int tmpc = getRightLocation(location + 10, Three_size, shift, EndX_L_Tunnel, collisionState_L_H);
                int tmpccheliangjiance = getRightLocation(tmpc + 5, Three_size, shift, EndX_L_Tunnel, collisionState_L_H);
                for (int i = 0; i < laneNum; i++)
                {
                    
                    //画三车道指示器
                    if (i == 0 && laneNum == 2)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/lcdongneikebian.dwg", "lcdongneikebian");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmpc -5 , StartY_L_Tunnel + 20, "lcdongneikebian", 1.23, 90);
                    }
                    else if (i == 0 && laneNum == 3)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/scdongneikebian.dwg", "scdongneikebian");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmpc - 5, StartY_L_Tunnel + 30, "scdongneikebian", 1.23, 90);
                    }
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/santaichedaozhishiqi.dwg", "santaichedao");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmpc, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "santaichedao",1, 90);
                    //车辆检测器
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/xianquanxingchedao.dwg", "xianquanxingchedao");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmpccheliangjiance, StartY_L_Tunnel + laneWidth * i + laneWidth / 2, "xianquanxingchedao", 1, 0);
                }
                //三车道指示器加入中间碰撞检测数组
                setCollosion(tmpc, Three_size, collisionState_L_C);
                //画车行横道指示器，并且加入碰撞检测，用的碰撞数组为collisionState_L_L
                int tmpcardirector = getRightLocation(location + 10, cardirector, shift, EndX_R_Tunnel, collisionState_L_L);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/chexinghengdao.dwg", "chexinghengdao");
                mxCt1.axMxDrawX1.DrawBlockReference(tmpcardirector, StartY_L_Tunnel + 2, "chexinghengdao", 1, 0);
                setCollosion(tmpcardirector, cardirector, collisionState_L_L);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpcardirector, StartY_L_Tunnel , 1);

                //标注三车道指示器
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpc, StartY_L_Tunnel + laneWidth * laneNum , 0);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmpccheliangjiance, StartY_L_Tunnel + laneWidth * laneNum , 0);
                setCollosion(tmpc, Three_size, collisionState_L_H);//三车道指示器加入左幅上册碰撞检测数组
                setCollosion(tmpccheliangjiance, Three_size, collisionState_L_H);//车辆检测器加入左幅上册碰撞检测数组
            }
        }





        //标注视频监控 by Yorn
        //标注视频监控 by Yorn
        public void InsertCamara_L(MxDrawControl mxCt1, double camara_size, double gap, double shift, double out_gap_camara, double begin_gap, int[] collisionState, int isHaveCommuLoad, double lenth_L_Emergency, ArrayList EmergencyStopCar)
        {
            //设置当前图层为"TrafficMonitoring"
            mxCt1.axMxDrawX1.LayerName = ("TrafficMonitoring");
            //设置洞外监控
            double tmp = EndX_L_Tunnel + out_gap_camara;
            if (isHaveCommuLoad == 0)
            {

                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCamara.dwg", "outCamara_L");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel, "outCamara_L", 1, 180);
                mxCt1.axMxDrawX1.LayerName = ("SwitchEquipmentLayer");
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/monitor.dwg", "MonitorL");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel - 4, "MonitorL", 1, 0);
            }

            //判断间隔是否输入异常
            if (gap < 100 || gap > 150) throw new Exception("很抱歉，摄像头间隔应该在100-150，您可能输入有误");

            //设置洞内监控
            tmp = EndX_L_Tunnel - begin_gap;
            while (tmp > StartX_L_Tunnel + gap)
            {
                tmp = getRightLocation(tmp, camara_size, shift, EndX_L_Tunnel, collisionState);
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/inCamara.dwg", "inCamara_L");
                if (isInEmerg(DoutoInt(tmp), lenth_L_Emergency, EmergencyStopCar))
                {
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth + camara_size + 5 - 6, "inCamara_L", 0.5, 180);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_L_Tunnel + laneNum * laneWidth + 5, 0);
                }
                else
                {
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth + camara_size - 6, "inCamara_L", 0.5, 180);
                    DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_L_Tunnel + laneNum * laneWidth, 0);
                }

                setCollosion(tmp, camara_size, collisionState);
                tmp -= gap;
            }
            tmp = getRightLocation(tmp, camara_size, -shift, EndX_L_Tunnel, collisionState);
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/inCamara.dwg", "inCamara_L");
            if (isInEmerg(DoutoInt(tmp), lenth_L_Emergency, EmergencyStopCar))
            {
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth + camara_size + 5 - 6, "inCamara_L", 0.5, 180);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_L_Tunnel + laneNum * laneWidth + 5, 0);
            }
            else
            {
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth + camara_size - 6, "inCamara_L", 0.5, 180);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_L_Tunnel + laneNum * laneWidth, 0);
            }
            setCollosion(tmp, camara_size, collisionState);
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }


    }
}
