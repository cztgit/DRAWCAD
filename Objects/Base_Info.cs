using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using WPFDrawCAD.Objects;
using MxDrawXLib;
using System.Windows.Forms;


namespace WPFDrawCAD.Objects
{
    class Base_Info : CommunalAttributeData
    {
        private Int64 Line_R_Id;//右幅下线id
        private Int64 Line_L_Id;//左幅上线id
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
        private double startX_R_Emergency;//隧道右幅应急车道起始位置
        public double StartX_R_Emergency
        {
            get { return startX_R_Emergency; }
            set { startX_R_Emergency = value; }
        }
        private double lenth_R_Emergency;//隧道右幅应急车道长度
        public double Lenth_R_Emergency
        {
            get { return lenth_R_Emergency; }
            set { lenth_R_Emergency = value; }
        }
        private double width_R_Emergency;//隧道右幅应急车道宽度
        public double Width_R_Emergency
        {
            get { return width_R_Emergency; }
            set { width_R_Emergency = value; }
        }
        private double startX_R_Footwalk; //隧道右幅人行道起点
        public double StartX_R_Footwalk
        {
            get { return startX_R_Footwalk; }
            set { startX_R_Footwalk = value; }
        }
        private double width_R_Footwalk; //隧道右幅人行道宽度
        public double Width_R_Footwalk
        {
            get { return width_R_Footwalk; }
            set { width_R_Footwalk = value; }
        }
        private double startX_L_Footwalk; //隧道左幅人行道起点

        public double StartX_L_Footwalk
        {
            get { return startX_L_Footwalk; }
            set { startX_L_Footwalk = value; }
        }
 

        private double startX_R_Roadway;//隧道右幅车行道起点
        private double width_R_Roadway;//隧道右幅车行道宽度
        private double startX_L_Roadway;//隧道左幅车行道起点

        //如下联络道所需参数 
        private double Road_S_Lenth;//隧道入口长度
        private double Road_E_Lenth;//隧道出口长度
        private double xzhd_xzh_R_CommuRoad;//小桩号端-小桩号-右幅-联络道
        private double xzhd_dzh_R_CommuRoad;//小桩号端-大桩号-右幅-联络道
        private double xzhd_xzh_L_CommuRoad;//小桩号端-小桩号-左幅-联络道
        private double xzhd_dzh_L_CommuRoad;//小桩号端-大桩号-左幅-联络道
        private double dzhd_xzh_L_CommuRoad;//大桩号端-小桩号-左幅-联络道
        private double dzhd_dzh_L_CommuRoad;//大桩号端-大桩号-左幅-联络道
        private double dzhd_xzh_R_CommuRoad;//大桩号端-小桩号-右幅-联络道
        private double dzhd_dzh_R_CommuRoad;//大桩号端-大桩号-右幅-联络道
        private double width_CommuRoad;     //联络道宽度
        //土建基础资料赋值过程
        //参数：右幅起点桩号（X,Y）、右幅终点桩号（X，Y）、左幅起点桩号（X,Y）、左幅终点桩号（X，Y）、车道数、车道宽度、应急车道起始位置、应急车道长度、宽度、人行横道起始位置、宽度
        public Base_Info(double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel,
            double laneNum, double laneWidth,
            double startX_R_Emergency, double lenth_R_Emergency, double width_R_Emergency,            
            double startX_R_Footwalk,double startX_L_Footwalk,double width_R_Footwalk,double Road_S_Lenth,double Road_E_Lenth,
            double xzhd_xzh_R_CommuRoad, double xzhd_dzh_R_CommuRoad, double xzhd_xzh_L_CommuRoad, double xzhd_dzh_L_CommuRoad, double dzhd_xzh_L_CommuRoad, double dzhd_dzh_L_CommuRoad, double dzhd_xzh_R_CommuRoad, double dzhd_dzh_R_CommuRoad
            ,double width_CommuRoad,int isXueZhu) :base(startX_R_Tunnel,startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel,startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel,isXueZhu)
        {
            this.LaneNum = laneNum;
            this.LaneWidth = laneWidth;

            this.StartX_R_Emergency = startX_R_Emergency;
            this.Lenth_R_Emergency = lenth_R_Emergency;
            this.Width_R_Emergency = width_R_Emergency;

            this.StartX_R_Footwalk = startX_R_Footwalk;
            this.StartX_L_Footwalk = startX_L_Footwalk;
            this.Width_R_Footwalk = width_R_Footwalk;
            this.Road_S_Lenth = Road_S_Lenth;
            this.Road_E_Lenth = Road_S_Lenth;

            this.xzhd_xzh_R_CommuRoad = xzhd_xzh_R_CommuRoad;
            this.xzhd_xzh_L_CommuRoad = xzhd_xzh_L_CommuRoad;
            this.xzhd_dzh_L_CommuRoad = xzhd_dzh_L_CommuRoad;
            this.xzhd_dzh_R_CommuRoad = xzhd_dzh_R_CommuRoad;

            this.dzhd_xzh_L_CommuRoad = dzhd_xzh_L_CommuRoad;
            this.dzhd_dzh_L_CommuRoad = dzhd_dzh_L_CommuRoad;
            this.dzhd_xzh_R_CommuRoad = dzhd_xzh_R_CommuRoad;
            this.dzhd_dzh_R_CommuRoad = dzhd_dzh_R_CommuRoad;
            this.width_CommuRoad = width_CommuRoad;

            if (isXueZhu == 1) {
                this.StartX_L_Tunnel -= 10;
                this.StartX_R_Tunnel -= 10;
                this.EndX_L_Tunnel += 10;
                this.EndX_R_Tunnel += 10;
            }
        }



        //土建基础资料绘制过程
        public void BaseLine_Draw(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, int[] collisionState, int[] collision_R_H, int[] collision_L_L  )
        {
            //先画右幅道路****************************************************************
            //   mxCt1.axMxDrawX1.DoCommand(1);
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //把线型改成实线
            mxCt1.axMxDrawX1.LineType = "";
            //设置线宽
            mxCt1.axMxDrawX1.LineWidth = 0;
            //创建一个图层,名为"BaseLayer"
            mxCt1.axMxDrawX1.AddLayer("BaseLayer");
            //设置当前图层为"BaseLayer"
            mxCt1.axMxDrawX1.LayerName = "BaseLayer";
            // 直接绘制一个实线
            ////将线给为红色
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
           
            Line_R_Id = mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel, EndX_R_Tunnel, EndY_R_Tunnel);
            //标注插入以米为单位插入
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_R_Tunnel, StartY_R_Tunnel ,1);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, EndX_R_Tunnel, StartY_R_Tunnel, 1);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * laneNum ,0);
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, EndX_L_Tunnel, StartY_L_Tunnel+laneWidth * laneNum ,0);
            //《---------------------------------------》
            //绘制一个虚线 虚线类型为“center”
            mxCt1.axMxDrawX1.LineType = "CENTER";
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            // 绘制一个虚线
            for (int i = 1; i < laneNum; i++)
            {
                mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * i, EndX_R_Tunnel, EndY_R_Tunnel + laneWidth * i);
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * i, EndX_R_Tunnel, EndY_R_Tunnel + laneWidth * i));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 10;

            }
            //把线型改成实线
            mxCt1.axMxDrawX1.LineType = "";
            //将线给为红色
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * laneNum, EndX_R_Tunnel, EndY_R_Tunnel + laneWidth * laneNum);//右幅最上面一根线

            //画四个角上斜线
            double xiexian = Math.Sin(Math.PI / 4);
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel, StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * laneNum); //左竖线
            mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel, EndY_R_Tunnel, EndX_R_Tunnel, EndY_R_Tunnel + laneWidth * laneNum);        //右竖线
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel, StartX_R_Tunnel - 5 * xiexian, StartY_R_Tunnel - 5 * xiexian);//右幅左下斜线
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * laneNum, StartX_R_Tunnel - 5 * xiexian, StartY_R_Tunnel + laneWidth * laneNum + 5 * xiexian);//右幅左上斜线
            mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel, EndY_R_Tunnel, EndX_R_Tunnel + 5 * xiexian, EndY_R_Tunnel - 5 * xiexian);//右幅右下斜线
            mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel, EndY_R_Tunnel + laneWidth * laneNum, EndX_R_Tunnel + 5 * xiexian, EndY_R_Tunnel + laneWidth * laneNum + 5 * xiexian);//右幅右上斜线


            //****************************************************************
            //再画左幅道路
            long rxhd_l_id = mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel,   EndX_L_Tunnel , EndY_L_Tunnel);//左幅最下面一根线
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            mxCt1.axMxDrawX1.LineType = "CENTER";
            // 绘制一个虚线
            for (int i = 1; i < laneNum; i++)
            {
                mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * i, EndX_L_Tunnel, EndY_L_Tunnel + laneWidth * i);
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * i, EndX_L_Tunnel, EndY_L_Tunnel + laneWidth * i));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 10;
            }
            //将线给为红色
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            //把线型改成实线
            mxCt1.axMxDrawX1.LineType = "";
            //左幅上线
            Line_L_Id = mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * laneNum,  EndX_L_Tunnel , EndY_L_Tunnel + laneWidth * laneNum);
            //左幅左竖线
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel , StartY_L_Tunnel, StartX_L_Tunnel , StartY_L_Tunnel + laneWidth * laneNum);
            //左幅右竖线
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel), EndY_L_Tunnel, StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel), EndY_L_Tunnel + laneWidth * laneNum);
            //下面四行代码分别为左幅的四个角的斜线
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel, StartX_L_Tunnel - 5 * xiexian, StartY_L_Tunnel - 5 * xiexian);
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * laneNum, StartX_L_Tunnel - 5 * xiexian, StartY_L_Tunnel + laneWidth * laneNum + 5 * xiexian);
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel), EndY_L_Tunnel, StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) + 5 * xiexian, EndY_L_Tunnel - 5 * xiexian);
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel), EndY_L_Tunnel + laneWidth * laneNum, StartX_L_Tunnel + (EndX_L_Tunnel - StartX_L_Tunnel) + 5 * xiexian, EndY_L_Tunnel + laneWidth * laneNum + 5 * xiexian);
            //画进出口道路
            InOutRoad_Draw(mxCt1,1,1);
            //画洞口

            
            //把所有的实体都放到当前显示视区
            mxCt1.axMxDrawX1.ZoomAll();
            //更新视区显示
            mxCt1.axMxDrawX1.UpdateDisplay();
        }

        // 遍历数据库的实体.
        public void GetAllEntity(MxDrawControl mxCt1)
        {
            try
            {

                MxDrawApplication app = new MxDrawApplication();
                MxDrawUtility mxUtility = new MxDrawUtility();

                // 得到当前图纸空间
                MxDrawBlockTableRecord blkRec = app.WorkingDatabase().CurrentSpace();

                // 创建一个用于遍历当前图纸空间的遍历器
                MxDrawBlockTableRecordIterator iter = blkRec.NewIterator();
                if (iter == null)
                    return;

                // 所有实体的id数组。
                List<Int64> aryId = new List<Int64>();

                int iLineNum = 0;
                // 循环得到所有实体
                for (; !iter.Done(); iter.Step(true, false))
                {
                    // 得到遍历器当前的实体
                    MxDrawEntity ent = iter.GetEntity();
                    if (ent == null)
                        continue;

                    // 得到实体的id
                    aryId.Add(ent.ObjectID);
                   
                    if (ent is MxDrawLine)
                    {
                        // 当前实体是一个直线
                        MxDrawLine line = (MxDrawLine)ent;
                        iLineNum++;
                    }
                    else if (ent is MxDrawBlockReference)
                    {
                        // 当前实体是一个块引用
                        MxDrawBlockReference blkRef = (MxDrawBlockReference)ent;
                        for (int j = 0; j < blkRef.AttributeCount; j++)
                        {
                            // 得到块引用中所有的属性
                            MxDrawAttribute attrib = blkRef.AttributeItem(j);
                            mxUtility.Prompt("\n Tag: " + attrib.Tag + "Text:" + attrib.TextString);
                        }

                    }
                    else if (ent is MxDrawText)
                    {
                        MxDrawText text = (MxDrawText)ent;

                        // 是个文字实体，text.TextString是文字内容
                        //text.TextString;
                    }
                    else if (ent is MxDrawMText)
                    {
                        MxDrawMText text = (MxDrawMText)ent;

                        // 是个多行文字实体，text.Contents是文字内容
                        //text.Contents
                    }
                    // else if (ent is 其它类型)
                    //{
                    //  ... ....
                    //}
                }

                String sT;
                sT = "发现" + aryId.Count + "个实体,其中有" + iLineNum + "个直线";
                MessageBox.Show(sT);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        /*
         * 打断直线函数
         * 第一个参数是Mxdraw控件，相当于那个画板
         * 第二个参数是你想要打断的直线的id
         * 后面四个参数是你要打断的位置坐标
         * 返回切割后，后面线段的id
         */
        public long  DoSplitCurves(MxDrawControl mxCt1,long line_id ,double psX,double psY,double peX,double peY)
        {
            mxCt1.axMxDrawX1.LayerName = "BaseLayer";
            ////将线给为红色
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            MxDrawEntity ent_line = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(line_id);
           //创建点对象
            MxDrawPoint pos = new MxDrawPoint();
            pos.x = psX;
            pos.y = psY;
            MxDrawPoint poe = new MxDrawPoint();
            poe.x = peX;
            poe.y = peY;

            MxDrawEntity ent = ent_line;
            if (ent == null)
            {
                return 0;
            }
            MxDrawCurve curve;
            if (ent is MxDrawCurve)
            {
                curve = (MxDrawCurve)ent;
            }
            else
            {
                MessageBox.Show("实体类型不对");
                return 0;
            }

           //传参，参数是点，传两个点就分成3条直线
            MxDrawResbuf param = new MxDrawResbuf();
            param.AddPoint(pos);
            param.AddPoint(poe);

            MxDrawResbuf objId;
            if (curve.SplitCurves(param , out objId))
            {
                MxDrawResbuf rbId = (MxDrawResbuf)objId;
                // MessageBox.Show("打断成功，生成的曲线个数:" + rbId.Count);
                //删除打断后的第二条直线
                mxCt1.axMxDrawX1.Erase(rbId.AtLong(1));
                // 把以前的删除掉。
                curve.Erase();
                //更改颜色
                MxDrawEntity ent_line0 = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(rbId.AtLong(0));
                ent_line0.colorIndex = MCAD_COLOR.mcRed;

                MxDrawEntity ent_line2 = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(rbId.AtLong(2));
                ent_line2.colorIndex = MCAD_COLOR.mcRed;

                return rbId.AtLong(2);
            }
            else
            {
                MessageBox.Show("打断失败");
                return -1;
            }
        }
        /*
         * 画人行通道和车行通道
         * flag=0 人行
         * flag=1 车行
         */
        public void drawPassWay(MxDrawControl mxCt1, double RID, double LID, int flag, int[] collision_R_H, int[] collision_L_L)
        {
            int manroad_width = 8;
            int carroad_width = 10;
            mxCt1.axMxDrawX1.LayerName = "BaseLayer";

            if (flag == 0)
            {
                //将线给为红色
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                mxCt1.axMxDrawX1.LineType = "";
                mxCt1.axMxDrawX1.DrawLine(RID - manroad_width / 2, StartY_R_Tunnel + laneWidth * laneNum, LID - manroad_width / 2, StartY_L_Tunnel);//人行横道左线
                mxCt1.axMxDrawX1.DrawLine(RID + manroad_width / 2, StartY_R_Tunnel + laneWidth * laneNum, LID + manroad_width / 2, StartY_L_Tunnel);//人行横道右线
                ////设计当前线型为"MyLineType"
                //mxCt1.axMxDrawX1.LineType = "CENTER";
                //mxCt1.axMxDrawX1.DrawLine(RID, StartY_R_Tunnel + laneWidth * laneNum, LID, StartY_L_Tunnel);//人行横道虚线
                //插入人行横通道防火门
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/renxingfanghuomen.dwg", "renxingfanghuomen");
                mxCt1.axMxDrawX1.DrawBlockReference(RID, StartY_R_Tunnel + laneWidth * laneNum +2, "renxingfanghuomen", 0.5, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(LID, StartY_L_Tunnel - 2, "renxingfanghuomen", 0.5, 0);
                setCollosion(RID, manroad_width, collision_R_H);
                setCollosion(LID, manroad_width, collision_L_L);
            }
            else if (flag == 1)
            {

                //将线给为红色
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                //把线型改成实线
                mxCt1.axMxDrawX1.LineType = "";
                mxCt1.axMxDrawX1.DrawLine(RID - carroad_width / 2, StartY_R_Tunnel + laneWidth * laneNum, LID - carroad_width / 2, StartY_L_Tunnel);//人行横道左线
                mxCt1.axMxDrawX1.DrawLine(RID + carroad_width / 2, StartY_R_Tunnel + laneWidth * laneNum, LID + carroad_width / 2, StartY_L_Tunnel);//人行横道右线
                ////设计当前线型为"MyLineType"
                //mxCt1.axMxDrawX1.LineType = "CENTER";
                //mxCt1.axMxDrawX1.DrawLine(RID, StartY_R_Tunnel + laneWidth * laneNum, LID , StartY_L_Tunnel);//人行横道虚线

                //插入车行横通道防火门
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/chexingfanghuomen.dwg", "chexingfanghuomen");
                mxCt1.axMxDrawX1.DrawBlockReference(RID, StartY_R_Tunnel + laneWidth * laneNum+1.3 , "chexingfanghuomen", 1, 0);
                mxCt1.axMxDrawX1.DrawBlockReference(LID, StartY_L_Tunnel - 1.2, "chexingfanghuomen", 1, 0);
                setCollosion(RID, manroad_width, collision_R_H);
                setCollosion(LID, manroad_width, collision_L_L);
            }

        }
        /*
         * 画人行车行横通道
         */
        public void drawPassWay(MxDrawControl mxCt1,  int[] collision_R_H, int[] collision_L_L,List<HengTongDao> Location)
        {
           
           foreach(HengTongDao htd in Location )
           {
               drawPassWay( mxCt1, htd.HengTongDao_R,htd.HengTongDao_L, htd.HengTongDao_Man,collision_R_H,collision_L_L);
           }
           
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }

       


       /*
        * 画车道分割线 粗体虚线
        * mxCt1 梦想空间对象
        * Start 开始点坐标
        * End   结束点坐标
        *
        */
        public void XuLine_Draw(MxDrawControl mxCt1,double StartX,double StartY,double EndX,double EndY)
        {
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //设置线宽
            mxCt1.axMxDrawX1.LineWidth =1.25;
            //创建一个图层,名为"BaseLayer"
            mxCt1.axMxDrawX1.AddLayer("BaseLayer");
            //设置当前图层为"BaseLayer"
            mxCt1.axMxDrawX1.LayerName = "BaseLayer";
            // mxCt1.axMxDrawX1.
            //《---------------------------------------》
            //绘制一个虚线
            //定义虚线数据据,"MyLineType"是线型名，"6,-8"是虚线的一个单位定义，6是实线长，-8是空格长。
            //设计当前线型为"MyLineType"
            mxCt1.axMxDrawX1.LineType = "CENTER";
            mxCt1.axMxDrawX1.DrawLine(StartX, StartY, EndX, EndY);
            mxCt1.axMxDrawX1.LineWidth = 0;
            mxCt1.axMxDrawX1.LineType = "";
        }


        /*
         * 隧道出入口画图
         * isHave_S_Commuroad  进口是否有连通道 1有 0无
         * isHave_E_Commuroad  出口是否有联通道 1有 0无
         */
        public void InOutRoad_Draw(MxDrawControl mxCt1,int isHave_S_Commuroad,int isHave_E_Commuroad)
        {
            double End_Abs = System.Math.Abs(EndX_R_Tunnel - EndX_L_Tunnel);
            double Road_E_Lenth_R = Road_E_Lenth;
            double Road_E_Lenth_L = Road_E_Lenth;
            if (EndX_R_Tunnel < EndX_L_Tunnel)
            {
                Road_E_Lenth_R = Road_E_Lenth_R + End_Abs;
            }
            else {
                Road_E_Lenth_L = Road_E_Lenth_L + End_Abs;
            }
            double Start_Abs = System.Math.Abs(StartX_R_Tunnel - StartX_L_Tunnel);
            double Road_S_Lenth_R = Road_S_Lenth;
            double Road_S_Lenth_L = Road_S_Lenth;
            if (StartX_R_Tunnel < StartX_L_Tunnel)
            {
                Road_S_Lenth_L = Road_S_Lenth_L + Start_Abs;
            }
            else {
                Road_S_Lenth_R = Road_S_Lenth_R + Start_Abs;
            }
            //右幅出口红线
            mxCt1.axMxDrawX1.LayerName = "BaseLayer";
            mxCt1.axMxDrawX1.LineType = "";
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;//将线给为红色
            mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel + 2, EndY_R_Tunnel - 2, EndX_R_Tunnel + Road_E_Lenth_R, StartY_R_Tunnel - 2);//右幅出口下面一条

            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;// //把颜色改回黑白色
            //设计当前线型为"CENTER"
            mxCt1.axMxDrawX1.LineType = "CENTER";
            // 绘制一个虚线
            for (int i = 1; i < laneNum; i++)
            {
                mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel + 2, StartY_R_Tunnel + laneWidth * i, EndX_R_Tunnel + Road_E_Lenth_R, EndY_R_Tunnel + laneWidth * i);
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel + 2, StartY_R_Tunnel + laneWidth * i, EndX_R_Tunnel + Road_E_Lenth_R, EndY_R_Tunnel + laneWidth * i));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 10;
            }

            mxCt1.axMxDrawX1.LineType = "";
            //把线型改成实线
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(EndX_R_Tunnel + 2, EndY_R_Tunnel + laneWidth * laneNum + 2, EndX_R_Tunnel + Road_E_Lenth_R, StartY_R_Tunnel + laneWidth * laneNum + 2);//右幅出口上面一条
           

            //左幅出口红线
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(EndX_L_Tunnel + 2, EndY_L_Tunnel - 2, EndX_L_Tunnel + Road_E_Lenth, EndY_L_Tunnel - 2);//左幅出口下面一条
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;

            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;// //把颜色改回黑白色
           
           
            //设计当前线型为"MyLineType"
            mxCt1.axMxDrawX1.LineType = "CENTER";
            // 绘制一个虚线
            for (int i = 1; i < laneNum; i++)
            {
                mxCt1.axMxDrawX1.DrawLine(EndX_L_Tunnel, StartY_L_Tunnel + laneWidth * i, EndX_L_Tunnel + Road_E_Lenth_L, EndY_L_Tunnel + laneWidth * i);
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(EndX_L_Tunnel, StartY_L_Tunnel + laneWidth * i, EndX_L_Tunnel + Road_E_Lenth_L, EndY_L_Tunnel + laneWidth * i));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 10;
            }
            mxCt1.axMxDrawX1.LineType = "";
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(EndX_L_Tunnel+2, EndY_L_Tunnel + laneWidth * laneNum + 2, EndX_L_Tunnel + Road_E_Lenth_L, StartY_L_Tunnel + laneWidth * laneNum + 2);//左幅出口上面一条
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //最右边竖线
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(EndX_L_Tunnel + Road_E_Lenth_L, StartY_L_Tunnel + laneWidth * laneNum + 10, EndX_L_Tunnel + Road_E_Lenth_L, StartY_R_Tunnel - 10);



            //右幅入口红线
            mxCt1.axMxDrawX1.LineType = "";
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;//将线给为红色
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel - 2, StartY_R_Tunnel - 2, StartX_R_Tunnel - Road_S_Lenth_R, StartY_R_Tunnel - 2);//右幅入口下面一条

            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;// //把颜色改回黑白色
         
            //设计当前线型为"MyLineType"
            mxCt1.axMxDrawX1.LineType = "CENTER";
            // 绘制一个虚线
            for (int i = 1; i < laneNum; i++)
            {
                mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * i, StartX_R_Tunnel - Road_S_Lenth_R, StartY_R_Tunnel + laneWidth * i);
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel, StartY_R_Tunnel + laneWidth * i, StartX_R_Tunnel - Road_S_Lenth_R, StartY_R_Tunnel + laneWidth * i));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 10;
            }

            mxCt1.axMxDrawX1.LineType = "";
            //把线型改成实线
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel - 2, EndY_R_Tunnel + laneWidth * laneNum + 2, StartX_R_Tunnel - Road_S_Lenth_R, StartY_R_Tunnel + laneWidth * laneNum + 2);//右幅入口上面一条


            //左幅入口红线
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel - 2, StartY_L_Tunnel - 2, StartX_L_Tunnel - Road_S_Lenth_L, StartY_L_Tunnel - 2);//左幅入口下面一条
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;

            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;// //把颜色改回黑白色
           
           
            mxCt1.axMxDrawX1.LineType = "CENTER";
            // 绘制一个虚线
            for (int i = 1; i < laneNum; i++)
            {
                mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * i, StartX_L_Tunnel - Road_S_Lenth_L, StartY_L_Tunnel + laneWidth * i);
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel, StartY_L_Tunnel + laneWidth * i, StartX_L_Tunnel - Road_S_Lenth_L, StartY_L_Tunnel + laneWidth * i));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 10;
            }
            mxCt1.axMxDrawX1.LineType = "";
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(StartX_L_Tunnel - 2, StartY_L_Tunnel + laneWidth * laneNum + 2, StartX_L_Tunnel - Road_S_Lenth_L, StartY_L_Tunnel + laneWidth * laneNum + 2);//左幅出口上面一条
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
            //最左边竖线
            mxCt1.axMxDrawX1.DrawCADColor = 16711680;
            mxCt1.axMxDrawX1.DrawLine(StartX_R_Tunnel - Road_S_Lenth_R, StartY_L_Tunnel + laneWidth * laneNum + 10, StartX_R_Tunnel - Road_S_Lenth_R, StartY_R_Tunnel - 10);
           
            
            

            //入口联络道
            if (isHave_S_Commuroad == 1)
            {
                
                Int64 xdz_L_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_R_CommuRoad-width_CommuRoad/2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(xzhd_dzh_L_CommuRoad-width_CommuRoad/2), DoutoInt(StartY_L_Tunnel - 2));
                mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                mxCt1.axMxDrawX1.LineType = "CENTER";
                mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(xzhd_dzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2));

                //int id_I = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(xzhd_dzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2)));
                //MxDrawDatabase database_I = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent_I = (MxDrawEntity)database_I.ObjectIdToObject(id_I);
                //ent_I.LinetypeScale = 1;

                mxCt1.axMxDrawX1.LineType = "";
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 xdz_R_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_R_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(xzhd_dzh_L_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2));

                Int64 ddz_L_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_L_CommuRoad - width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(xzhd_dzh_R_CommuRoad - width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2));
                mxCt1.axMxDrawX1.LineType = "CENTER";
                mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(xzhd_dzh_R_CommuRoad),DoutoInt( StartY_R_Tunnel + laneWidth * laneNum + 2));
                //int id_I_1 = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(xzhd_dzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2)));
                //MxDrawDatabase database_I_1 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent_I_1 = (MxDrawEntity)database_I_1.ObjectIdToObject(id_I_1);
                //ent_I_1.LinetypeScale = 1;

                mxCt1.axMxDrawX1.LineType = "";
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 ddz_R_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(xzhd_xzh_L_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(xzhd_dzh_R_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2));


                MxDrawPoint points1 = IntersectPoint(mxCt1, xdz_L_ID, ddz_L_ID);
                MxDrawPoint points2 = IntersectPoint(mxCt1, xdz_L_ID, ddz_R_ID);
                MxDrawPoint points3 = IntersectPoint(mxCt1, xdz_R_ID, ddz_L_ID);
                MxDrawPoint points4 = IntersectPoint(mxCt1, xdz_R_ID, ddz_R_ID);
                DoSplitCurves( mxCt1, xdz_L_ID ,points1.x,points1.y,points2.x,points2.y);
                DoSplitCurves(mxCt1, xdz_R_ID, points3.x, points3.y, points4.x, points4.y);
                DoSplitCurves(mxCt1, ddz_L_ID, points1.x, points1.y, points3.x, points3.y);
                DoSplitCurves(mxCt1, ddz_R_ID, points4.x, points4.y, points2.x, points2.y);
                //插入洞外监控
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCamara.dwg", "outCamara");
                mxCt1.axMxDrawX1.DrawBlockReference(xzhd_xzh_R_CommuRoad-10, points1.y, "outCamara", 1, 0);
                //洞外广播
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outBroadcast.dwg", "outBroadcast");
                mxCt1.axMxDrawX1.DrawBlockReference(xzhd_xzh_R_CommuRoad - 10, points1.y + 5, "outBroadcast", 1, 0);
                //洞外亮度检测器
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/monitor.dwg", "Monitor");
                mxCt1.axMxDrawX1.DrawBlockReference(xzhd_xzh_R_CommuRoad - 10, StartY_R_Tunnel +laneNum*laneWidth, "Monitor", 1, 0);

                //标注插入以米为单位插入
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, xzhd_xzh_R_CommuRoad, StartY_R_Tunnel, 1);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, xzhd_dzh_R_CommuRoad, StartY_R_Tunnel, 1);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, xzhd_xzh_L_CommuRoad, StartY_L_Tunnel + laneWidth * laneNum, 0);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, xzhd_dzh_L_CommuRoad, StartY_L_Tunnel + laneWidth * laneNum, 0);
            }
            //出口联络道
            if (isHave_E_Commuroad == 1)
            {
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 xdz_L_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_R_CommuRoad - width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(dzhd_dzh_L_CommuRoad - width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2));
                mxCt1.axMxDrawX1.LineType = "CENTER";
                mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(dzhd_dzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2));
                //int id = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(dzhd_dzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2)));
                //MxDrawDatabase database2 = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent2 = (MxDrawEntity)database2.ObjectIdToObject(id);
                //ent2.LinetypeScale = 1;

                mxCt1.axMxDrawX1.LineType = "";
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 xdz_R_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_R_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2), DoutoInt(dzhd_dzh_L_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2));

                Int64 ddz_L_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_L_CommuRoad - width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(dzhd_dzh_R_CommuRoad - width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2));
                mxCt1.axMxDrawX1.LineType = "CENTER";
                mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(dzhd_dzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2));
                //int id_O = Convert.ToInt32(mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_L_CommuRoad), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(dzhd_dzh_R_CommuRoad), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2)));
                //MxDrawDatabase database_O = (MxDrawDatabase)mxCt1.axMxDrawX1.GetDatabase();
                //MxDrawEntity ent_O = (MxDrawEntity)database_O.ObjectIdToObject(id_O);
                //ent2.LinetypeScale = 1;

                mxCt1.axMxDrawX1.LineType = "";
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 ddz_R_ID = mxCt1.axMxDrawX1.DrawLine(DoutoInt(dzhd_xzh_L_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_L_Tunnel - 2), DoutoInt(dzhd_dzh_R_CommuRoad + width_CommuRoad / 2), DoutoInt(StartY_R_Tunnel + laneWidth * laneNum + 2));


                MxDrawPoint points1 = IntersectPoint(mxCt1, xdz_L_ID, ddz_L_ID);
                MxDrawPoint points2 = IntersectPoint(mxCt1, xdz_L_ID, ddz_R_ID);
                MxDrawPoint points3 = IntersectPoint(mxCt1, xdz_R_ID, ddz_L_ID);
                MxDrawPoint points4 = IntersectPoint(mxCt1, xdz_R_ID, ddz_R_ID);
                DoSplitCurves(mxCt1, xdz_L_ID, points1.x, points1.y, points2.x, points2.y);
                DoSplitCurves(mxCt1, xdz_R_ID, points3.x, points3.y, points4.x, points4.y);
                DoSplitCurves(mxCt1, ddz_L_ID, points1.x, points1.y, points3.x, points3.y);
                DoSplitCurves(mxCt1, ddz_R_ID, points4.x, points4.y, points2.x, points2.y);
                //插入洞外监控
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outCamara.dwg", "outCamara");
                mxCt1.axMxDrawX1.DrawBlockReference(dzhd_dzh_L_CommuRoad+10, points1.y, "outCamara", 1, 0);
                //洞外广播
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/outBroadcast.dwg", "outBroadcast");
                mxCt1.axMxDrawX1.DrawBlockReference(dzhd_dzh_L_CommuRoad + 10, points1.y+5, "outBroadcast", 1, 0);
                //洞外亮度检测器
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/monitor.dwg", "Monitor");
                mxCt1.axMxDrawX1.DrawBlockReference(dzhd_dzh_L_CommuRoad + 10, StartY_L_Tunnel, "Monitor", 1, 0);
                //标注插入以米为单位插入
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, dzhd_xzh_R_CommuRoad, StartY_R_Tunnel, 1);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, dzhd_dzh_R_CommuRoad, StartY_R_Tunnel, 1);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, dzhd_xzh_L_CommuRoad, StartY_L_Tunnel + laneWidth * laneNum, 0);
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, dzhd_dzh_L_CommuRoad, StartY_L_Tunnel + laneWidth * laneNum, 0);
            }
        }

        /*
         * 返回直线交点
         * 传入两条直线的id
         * 返回交点对象
         */
        public MxDrawPoint IntersectPoint(MxDrawControl mxCt1,Int64 Line1,Int64 Line2)
        {
            MxDrawEntity ent_line1 = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(Line1);
            MxDrawEntity ent_line2 = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(Line2);
            MxDrawPoints points = (MxDrawPoints)ent_line1.IntersectWith(ent_line2, MCAD_McExtendOption.mcExtendNone);

            if (points.Count == 0)
            {
                MessageBox.Show("没有求到交点");
            }
            else
            {
                MxDrawPoint pt = points.Item(0);

                //MessageBox.Show("交点为:" + pt.x + "," + pt.y);
                return pt;

            }
            return null;
        }


        /*
         * 画应急车道
         * L_OR_R  ---------------0左幅，1右幅
         * collisionState_R_L-------------------隧道左幅上侧碰撞检测数组
         * collisionState_L_H-------------------隧道右幅下册碰撞检测数组
         * insertLocation ----------------------插入位置链表
         * 
         */
        public void DrawEmergStopWay(MxDrawControl mxCt1,  int L_OR_R, int[] collisionState_R_L, int[] collisionState_L_H, ArrayList insertLocation)
        {
            if (L_OR_R == 1)
            {
                //将线给为红色
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 R_ID;
                foreach (int insert in insertLocation)
                {
                    R_ID = DrawEmergStopWay(mxCt1, Line_R_Id, insert, L_OR_R, collisionState_R_L);
                    Line_R_Id = R_ID;
                }
            }
            else if (L_OR_R == 0)
            {
                //将线给为红色
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                Int64 L_ID;
                foreach (int insert in insertLocation)
                {
                    L_ID = DrawEmergStopWay(mxCt1, Line_L_Id, insert, L_OR_R, collisionState_L_H);
                    Line_L_Id = L_ID;
                }
 
            }

            //把所有的实体都放到当前显示视区
            mxCt1.axMxDrawX1.ZoomAll();
            //更新视区显示
            mxCt1.axMxDrawX1.UpdateDisplay();
        }
        /*
        * 画应急车道
         * L_OR_R,0-画左幅，1-画右幅
         * 返回切割线第三段的id用于后续切割
         * CenterEmergy-插入地点
         * collisionState-----碰撞数组
        */
        public Int64 DrawEmergStopWay(MxDrawControl mxCt1, Int64 LineId, double CenterEmergy, int L_OR_R, int[] collisionState)
        {
            //设置当前图层为"BaseLayer"
            mxCt1.axMxDrawX1.LayerName = "BaseLayer";
            //画应急车道
            if (L_OR_R == 1) 
            {
                ////将线给为红色
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                mxCt1.axMxDrawX1.DrawLine(CenterEmergy - lenth_R_Emergency * 0.5 + 5, StartY_R_Tunnel - width_R_Emergency, CenterEmergy + lenth_R_Emergency * 0.5 - 5, StartY_R_Tunnel - width_R_Emergency);
                mxCt1.axMxDrawX1.DrawLine(CenterEmergy - lenth_R_Emergency * 0.5 + 5, StartY_R_Tunnel - width_R_Emergency, CenterEmergy - lenth_R_Emergency * 0.5, StartY_R_Tunnel);
                mxCt1.axMxDrawX1.DrawLine(CenterEmergy + lenth_R_Emergency * 0.5 - 5, StartY_R_Tunnel - width_R_Emergency, CenterEmergy + lenth_R_Emergency * 0.5, StartY_R_Tunnel);
                //切割应急车道出口
                Int64 After_R_Id = DoSplitCurves(mxCt1, LineId, CenterEmergy - lenth_R_Emergency * 0.5, StartY_R_Tunnel, CenterEmergy + lenth_R_Emergency * 0.5, StartY_R_Tunnel);

                //画洞内云台摄像机

                mxCt1.axMxDrawX1.InsertBlock("sourceblock/dongneiyuntaishexiangji.dwg", "dongneiyuntaishexiangji");
                mxCt1.axMxDrawX1.DrawBlockReference(CenterEmergy - (lenth_R_Emergency / 2) + 2.5, StartY_R_Tunnel - width_R_Emergency / 2, "dongneiyuntaishexiangji", 1, 0);

                //将应急车道加入碰撞状态数组
                setCollosion(CenterEmergy - (lenth_R_Emergency / 2) + 2.5, 5.0, collisionState);
                setCollosion(CenterEmergy + (lenth_R_Emergency / 2) - 2.5, 5, collisionState);
                //把颜色改回黑白色
                mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                //标注应急车道距离
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, CenterEmergy - lenth_R_Emergency * 0.5, StartY_R_Tunnel, 1); //左端点
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, CenterEmergy + lenth_R_Emergency * 0.5, StartY_R_Tunnel, 1); //右端点
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, CenterEmergy - (lenth_R_Emergency / 2) + 2.5, StartY_R_Tunnel - width_R_Emergency, 1); //洞外云台摄像机
                mxCt1.axMxDrawX1.ZoomAll();
                mxCt1.axMxDrawX1.UpdateDisplay();
                return After_R_Id;
            }
            if (L_OR_R == 0)
            {
                ////将线给为红色
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;
                mxCt1.axMxDrawX1.DrawLine(CenterEmergy - lenth_R_Emergency * 0.5 + 5, StartY_L_Tunnel + laneNum * laneWidth + width_R_Emergency, CenterEmergy + lenth_R_Emergency * 0.5 - 5, StartY_L_Tunnel + width_R_Emergency + laneNum * laneWidth);
                mxCt1.axMxDrawX1.DrawLine(CenterEmergy - lenth_R_Emergency * 0.5 + 5, StartY_L_Tunnel + laneNum * laneWidth + width_R_Emergency, CenterEmergy - lenth_R_Emergency * 0.5, StartY_L_Tunnel + laneNum * laneWidth);
                mxCt1.axMxDrawX1.DrawLine(CenterEmergy + lenth_R_Emergency * 0.5 - 5, StartY_L_Tunnel + laneNum * laneWidth + width_R_Emergency, CenterEmergy + lenth_R_Emergency * 0.5, StartY_L_Tunnel + laneNum * laneWidth);
                //切割应急车道出口
                 Int64 After_L_Id  = DoSplitCurves(mxCt1, LineId, CenterEmergy - lenth_R_Emergency * 0.5, StartY_L_Tunnel, CenterEmergy + lenth_R_Emergency * 0.5, StartY_L_Tunnel);
                //画洞内云台摄像机

                 mxCt1.axMxDrawX1.InsertBlock("sourceblock/dongneiyuntaishexiangji.dwg", "dongneiyuntaishexiangji");
                 mxCt1.axMxDrawX1.DrawBlockReference(CenterEmergy + (lenth_R_Emergency / 2) - 2.5, StartY_L_Tunnel + laneNum * laneWidth + width_R_Emergency / 2, "dongneiyuntaishexiangji", 1, 180);
                //将应急车道加入碰撞状态数组
                setCollosion(CenterEmergy - (lenth_R_Emergency / 2) + 2.5, 5.0, collisionState);
                setCollosion(CenterEmergy + (lenth_R_Emergency / 2) - 2.5, 5, collisionState);
                //标注应急车道距离
                //把颜色改回黑白色
                mxCt1.axMxDrawX1.DrawCADColorIndex = 0;
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, CenterEmergy - lenth_R_Emergency * 0.5, StartY_L_Tunnel + laneNum * laneWidth , 0); //左端点
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, CenterEmergy + lenth_R_Emergency * 0.5, StartY_L_Tunnel + laneNum * laneWidth ,0); //右端点
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, CenterEmergy + (lenth_R_Emergency / 2) - 2.5, StartY_L_Tunnel + laneNum * laneWidth + width_R_Emergency, 0); //洞外云台摄像机
                mxCt1.axMxDrawX1.ZoomAll();
                mxCt1.axMxDrawX1.UpdateDisplay();
                return After_L_Id;
            }
            return -1;

        }
        /*
         * 画洞口圆弧
         */
        public void DrawEntrance(MxDrawControl mxCt1)
        {
            
                mxCt1.axMxDrawX1.DrawCADColor = 16711680;//将线给为红色
                //画右幅起点
                mxCt1.axMxDrawX1.DrawArc3(StartX_R_Tunnel, StartY_R_Tunnel, StartX_R_Tunnel, StartY_R_Tunnel + laneNum * laneWidth, 0.3);
                //画右幅终点
                mxCt1.axMxDrawX1.DrawArc3(EndX_R_Tunnel, StartY_R_Tunnel, EndX_R_Tunnel , StartY_R_Tunnel + laneNum * laneWidth, -0.3);
               //画左幅起点
                mxCt1.axMxDrawX1.DrawArc3(EndX_L_Tunnel , StartY_L_Tunnel, EndX_L_Tunnel , StartY_L_Tunnel + laneNum * laneWidth, -0.3);
                //画左幅终点
                mxCt1.axMxDrawX1.DrawArc3(StartX_L_Tunnel, StartY_L_Tunnel, StartX_L_Tunnel, StartY_L_Tunnel + laneNum * laneWidth, 0.3);
                mxCt1.axMxDrawX1.DrawCADColor = 0;//将线给为白色
                mxCt1.axMxDrawX1.ZoomAll();
                mxCt1.axMxDrawX1.UpdateDisplay();
        }
    }
}
