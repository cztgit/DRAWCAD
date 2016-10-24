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
    class FireFighting : CommunalAttributeData
    {
        
        //构造函数
        public FireFighting( double startX_R_Tunnel, double startY_R_Tunnel, double endX_R_Tunnel, double endY_R_Tunnel,
            double startX_L_Tunnel, double startY_L_Tunnel, double endX_L_Tunnel, double endY_L_Tunnel, int isXueZhu)
            : base(startX_R_Tunnel, startY_R_Tunnel, endX_R_Tunnel, endY_R_Tunnel, startX_L_Tunnel, startY_L_Tunnel, endX_L_Tunnel, endY_L_Tunnel, isXueZhu)
        {
            
        }



        /*
         * Jia_or_Yi 0甲型消防洞，1乙型消防洞
         * isHave_Shengguang 是否有声光报警器 1有，0无
         * isHave_Shoudong 是否有手动报警按钮  1有，0无
         * EmergencyStopCar 紧急停车带中心桩号
         * collisionState_R_L 右幅隧道下壁碰撞数组
         * collisionState_R_H 右幅隧道上壁碰撞数组
         * isHave_R_H 右幅隧道上面是否也有消防洞 1有，0无
         * lenth_R_Emergency, double width_R_Emergency应急车道长度和宽度
         */
        // 画右幅消防图
        public void InsertBlock_R_Click(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double gap, double size, double shift, double begin_gap, double laneNum, double laneWidth, double lenth_R_Emergency, double width_R_Emergency, ArrayList EmergencyStopCar, int[] collisionState_R_L, int[] collisionState_R_H, int Jia_or_Yi, int isHave_Shengguang, int isHave_Shoudong, int isHave_R_H)
        {
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;

            //创建一个图层,名为"FireFightingLayer"
            mxCt1.axMxDrawX1.AddLayer("FireFightingLayer");

            //设置当前图层为"FireFightingLayer"
            mxCt1.axMxDrawX1.LayerName = ("FireFightingLayer");
            //判断最后是否补加一个，如果补加了，整体向前移动，也就是改变beigingap；
            begin_gap = 25;
            double ceshi = StartX_R_Tunnel + begin_gap;
            while (ceshi < EndX_R_Tunnel)
            {
                double tmp = getRightLocation(ceshi, size, shift, EndX_R_Tunnel, collisionState_R_L);

                ceshi = tmp + gap;

                if (ceshi < EndX_R_Tunnel && EndX_R_Tunnel - ceshi < gap && EndX_R_Tunnel - ceshi > begin_gap)
                {

                    if (EndX_R_Tunnel - ceshi > begin_gap)
                    {
                        begin_gap = 15;
                    }
                    break;
                }
            }

            ////////////////////////////////////////////////////////////
            double i = StartX_R_Tunnel + begin_gap;
            while (i < EndX_R_Tunnel)
            {
                double tmp = InsertBlock_R_L(mxCt1, i, shift, gap, Jia_or_Yi, size,  lenth_R_Emergency,  width_R_Emergency,EmergencyStopCar,collisionState_R_L, isHave_Shengguang, isHave_Shoudong);

                i = tmp + gap;

                if (i < EndX_R_Tunnel && EndX_R_Tunnel - i < gap && EndX_R_Tunnel - i > begin_gap)
                {
                    InsertBlock_R_L(mxCt1, i, shift, gap, Jia_or_Yi, size, lenth_R_Emergency, width_R_Emergency, EmergencyStopCar, collisionState_R_L, isHave_Shengguang, isHave_Shoudong);
                    if (EndX_R_Tunnel - i > begin_gap)
                    {
                        InsertBlock_R_L(mxCt1, EndX_R_Tunnel - begin_gap, shift, gap, Jia_or_Yi, size, lenth_R_Emergency, width_R_Emergency, EmergencyStopCar, collisionState_R_L, isHave_Shengguang, isHave_Shoudong);
                    }
                    break;
                }
            }
            //判断右幅上册是否有消防洞
            if (isHave_R_H == 1)
            {
                double j = StartX_R_Tunnel + begin_gap;
                while (j < EndX_R_Tunnel)
                {
                    double tmp = InsertBlock_R_H(mxCt1, j, shift, gap, Jia_or_Yi, size,  laneNum,  laneWidth, collisionState_R_H, isHave_Shengguang, isHave_Shoudong);

                    j = tmp + gap;

                    if (i < EndX_R_Tunnel && EndX_R_Tunnel - j < gap && EndX_R_Tunnel - j > begin_gap)
                    {
                        InsertBlock_R_H(mxCt1, j, shift, gap, Jia_or_Yi, size,  laneNum,  laneWidth,collisionState_R_H, isHave_Shengguang, isHave_Shoudong);
                        if (EndX_R_Tunnel - j > begin_gap)
                        {
                            InsertBlock_R_H(mxCt1, EndX_R_Tunnel - begin_gap, shift, gap, Jia_or_Yi, size, laneNum,  laneWidth,  collisionState_R_H, isHave_Shengguang, isHave_Shoudong);
                        }
                        break;
                    }
                }
 
            }
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }

        /*
         * 插入右幅下侧消防洞
         * 插入单位为米
         */
        public int InsertBlock_R_L(MxDrawControl mxCt1, double location, double shift, double gap, int Jia_or_Yi, double size, double lenth_R_Emergency, double width_R_Emergency, ArrayList EmergencyStopCar, int[] collisionState, int isHave_Shengguang, int isHave_Shoudong)
        {
            mxCt1.axMxDrawX1.LayerName = ("FireFightingLayer");
            int tmp = 0;
            tmp = getRightLocation(location, size, shift, EndX_R_Tunnel, collisionState);
            if (isInEmerg(tmp, lenth_R_Emergency, EmergencyStopCar))
            {
                if (Jia_or_Yi == 0)
                {
                    //给图块写扩展数据
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaxingxiaofangshebeidong.dwg", "xiaofangshebeidong");
                    Int64 BlockId = mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 3.8-width_R_Emergency, "xiaofangshebeidong", 1.5, 0);
                    MxDrawEntity Block = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(BlockId);
                    MxDrawResbuf xData = new MxDrawResbuf();
                    xData.AddStringEx("Jiaxingxiaofang", 1);
                    Block.SetXData(xData);

                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);

                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2.5, StartY_R_Tunnel + 6.5 - width_R_Emergency, "shengguang", 1, 0);

                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2, StartY_R_Tunnel + 1.5 - width_R_Emergency, "shoudong", 1, 0);
                    }
                }
                else if (Jia_or_Yi == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/yixingxiaofangshebeidong.dwg", "xiaofangshebeidong");
                    Int64 BlockId = mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 2.5 - width_R_Emergency, "xiaofangshebeidong", 1.5, 0);
                    
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);
                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 1, StartY_R_Tunnel + 6.2 - width_R_Emergency, "shengguang", 1, 0);
                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp - 2, StartY_R_Tunnel + 6 - width_R_Emergency, "shoudong", 0.5, 0);
                    }
                }

                //插入紧急疏散标志
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjishusanzhishibiaozhi.dwg", "jinjishusan");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel - 2 - width_R_Emergency, "jinjishusan", 1.5, 0);

                //标注插入以米为单位插入
                mxCt1.axMxDrawX1.LineType = "";
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel - width_R_Emergency, 1);
            }
            else 
            {
                if (Jia_or_Yi == 0)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaxingxiaofangshebeidong.dwg", "xiaofangshebeidong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 3.8, "xiaofangshebeidong", 1.5, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);

                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2.5, StartY_R_Tunnel + 6.5, "shengguang", 1, 0);

                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2, StartY_R_Tunnel + 1.5, "shoudong", 1, 0);
                    }
                }
                else if (Jia_or_Yi == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/yixingxiaofangshebeidong.dwg", "xiaofangshebeidong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + 2.5, "xiaofangshebeidong", 1.5, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);
                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 1, StartY_R_Tunnel + 6.2, "shengguang", 1, 0);
                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp - 2, StartY_R_Tunnel + 6, "shoudong", 0.5, 0);
                    }
                }

                //插入紧急疏散标志
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjishusanzhishibiaozhi.dwg", "jinjishusan");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel - 2, "jinjishusan", 1.5, 0);

                //标注插入以米为单位插入
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel, 1);
            }
            
            return tmp ;
        }

        /*
        * 插入右幅上侧消防洞
        * 插入单位为米
        */
        public int InsertBlock_R_H(MxDrawControl mxCt1, double location, double shift, double gap, int Jia_or_Yi, double size, double laneNum, double laneWidth, int[] collisionState, int isHave_Shengguang, int isHave_Shoudong)
        {
            mxCt1.axMxDrawX1.LayerName = ("FireFightingLayer");
            int tmp = 0;
            tmp = getRightLocation(location, size, shift, EndX_R_Tunnel, collisionState);
            if (Jia_or_Yi == 0)
            {
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaxingxiaofangshebeidong.dwg", "xiaofangshebeidong");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel +laneNum*laneWidth- 3.8, "xiaofangshebeidong", 1.5, 0);
                //加入碰撞检测
                setCollosion(tmp, size, collisionState);

                //插入声光报警器
                if (isHave_Shengguang == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2.5, StartY_R_Tunnel + laneNum * laneWidth - 6.5, "shengguang", 1, 0);

                }
                //插入手动报警按钮
                if (isHave_Shoudong == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2, StartY_R_Tunnel + laneNum * laneWidth - 1.5, "shoudong", 1, 0);
                }
            }
            else if (Jia_or_Yi == 1)
            {
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/yixingxiaofangshebeidong.dwg", "xiaofangshebeidong");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneNum * laneWidth - 2.5, "xiaofangshebeidong", 1.5, 0);
                //加入碰撞检测
                setCollosion(tmp, size, collisionState);
                //插入声光报警器
                if (isHave_Shengguang == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp + 1, StartY_R_Tunnel + laneNum * laneWidth - 6.2, "shengguang", 1, 0);
                }
                //插入手动报警按钮
                if (isHave_Shoudong == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp - 2, StartY_R_Tunnel + laneNum * laneWidth - 6, "shoudong", 0.5, 0);
                }
            }

            //插入紧急疏散标志
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjishusanzhishibiaozhi.dwg", "jinjishusan");
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_R_Tunnel + laneNum * laneWidth +2, "jinjishusan", 1.5, 0);

            //标注插入以米为单位插入
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_R_Tunnel + laneNum * laneWidth ,0);
            

            return tmp;
        }
        /*
         * 是否位于紧急停车带范围内
         * EmergencyStopCar 里面为整形
         */
        public bool isInEmerg(int insert_X,double   lenth_R_Emergency,ArrayList EmergencyStopCar)
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


        /*
         * ****************************************************************************画左幅消防洞******************************************************************************
         */
        /*
        * Jia_or_Yi 0甲型消防洞，1乙型消防洞
        * isHave_Shengguang 是否有声光报警器 1有，0无
        * isHave_Shoudong 是否有手动报警按钮  1有，0无
        * EmergencyStopCar 紧急停车带中心桩号
        * collisionState_R_L 右幅隧道下壁碰撞数组
        * collisionState_R_H 右幅隧道上壁碰撞数组
        * isHave_R_H 右幅隧道上面是否也有消防洞 1有，0无
        * lenth_R_Emergency, double width_R_Emergency应急车道长度和宽度
        */
        // 画左幅消防图
        public void InsertBlock_L_Click(MxDrawControl mxCt1, AxMxDrawXLib.AxMxDrawX axMxDrawX1, double gap, double size, double shift, double begin_gap, double laneNum, double laneWidth, double lenth_R_Emergency, double width_R_Emergency, ArrayList EmergencyStopCar, int[] collisionState_L_L, int[] collisionState_L_H, int Jia_or_Yi, int isHave_Shengguang, int isHave_Shoudong, int isHave_L_L)
        {
            
            //把颜色改回黑白色
            mxCt1.axMxDrawX1.DrawCADColorIndex = 0;

            //设置当前图层为"FireFightingLayer"
            mxCt1.axMxDrawX1.LayerName = ("FireFightingLayer");

            //判断最后是否补加一个，如果补加了，整体向前移动，也就是改变beigingap；
            begin_gap = 25;
            double ceshi = EndX_L_Tunnel - begin_gap;
            while (ceshi > StartX_L_Tunnel)
            {
                double tmp = getRightLocation(ceshi, size, shift, EndX_L_Tunnel, collisionState_L_H);
                ceshi = tmp - gap;
                if (ceshi > StartX_L_Tunnel && ceshi - StartX_L_Tunnel < gap && ceshi - StartX_L_Tunnel > begin_gap)
                {
                    if (ceshi - StartX_L_Tunnel > begin_gap)
                    {
                        begin_gap = 15;
                    }
                    break;
                }
            }

            ////////////////////////////////////////////////////////////

            double i = EndX_L_Tunnel - begin_gap;
            while (i > StartX_L_Tunnel)
            {
                double tmp = InsertBlock_L_H(mxCt1, i, shift, gap, Jia_or_Yi, size, lenth_R_Emergency, width_R_Emergency, EmergencyStopCar, collisionState_L_H, isHave_Shengguang, isHave_Shoudong, laneNum, laneWidth);

                i = tmp - gap;

                if (i > StartX_L_Tunnel && i - StartX_L_Tunnel < gap && i - StartX_L_Tunnel > begin_gap)
                {
                    InsertBlock_L_H(mxCt1, i, shift, gap, Jia_or_Yi, size, lenth_R_Emergency, width_R_Emergency, EmergencyStopCar, collisionState_L_H, isHave_Shengguang, isHave_Shoudong, laneNum, laneWidth);
                    if (i - StartX_L_Tunnel > begin_gap)
                    {
                        InsertBlock_L_H(mxCt1, StartX_L_Tunnel + begin_gap, shift, gap, Jia_or_Yi, size, lenth_R_Emergency, width_R_Emergency, EmergencyStopCar, collisionState_L_H, isHave_Shengguang, isHave_Shoudong, laneNum, laneWidth);
                    }
                    break;
                }
            }
            //判断左幅下册是否有消防洞
            if (isHave_L_L == 1)
            {
                double j = EndX_L_Tunnel - begin_gap;
                while (j > StartX_L_Tunnel)
                {
                    double tmp = InsertBlock_L_L(mxCt1, j, shift, gap, Jia_or_Yi, size, laneNum, laneWidth, collisionState_L_L, isHave_Shengguang, isHave_Shoudong);

                    j = tmp - gap;

                    if (j > StartX_L_Tunnel && j - StartX_L_Tunnel < gap && j - StartX_L_Tunnel > begin_gap)
                    {
                        InsertBlock_L_L(mxCt1, j, shift, gap, Jia_or_Yi, size, laneNum, laneWidth, collisionState_L_L, isHave_Shengguang, isHave_Shoudong);
                        if (i - StartX_L_Tunnel > begin_gap)
                        {
                            InsertBlock_L_L(mxCt1, StartX_L_Tunnel + begin_gap, shift, gap, Jia_or_Yi, size, laneNum, laneWidth, collisionState_L_L, isHave_Shengguang, isHave_Shoudong);
                        }
                        break;
                    }
                }

            }
            mxCt1.axMxDrawX1.ZoomAll();
            mxCt1.axMxDrawX1.UpdateDisplay();
        }

        /*
         * 插入左幅上侧消防洞
         * 插入单位为米
         */
        public int InsertBlock_L_H(MxDrawControl mxCt1, double location, double shift, double gap, int Jia_or_Yi, double size, double lenth_R_Emergency, double width_R_Emergency, ArrayList EmergencyStopCar, int[] collisionState, int isHave_Shengguang, int isHave_Shoudong,double laneNum, double laneWidth )
        {
          
            int tmp = 0;
            tmp = getRightLocation(location, size, shift, EndX_L_Tunnel, collisionState);
            if (isInEmerg(tmp, lenth_R_Emergency, EmergencyStopCar))
            {
                if (Jia_or_Yi == 0)
                {
                    
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaxingxiaofangshebeidong.dwg", "jiaxingxiaofangshebeidong");
                    Int64 BlockId = mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth - 4.6 + width_R_Emergency, "jiaxingxiaofangshebeidong", 1.5, 0);
                    MxDrawEntity Block = (MxDrawEntity)mxCt1.axMxDrawX1.ObjectIdToObject(BlockId);
                    MxDrawResbuf xData = new MxDrawResbuf();
                    xData.AddStringEx("Jiaxingxiaofang", 1);
                    Block.SetXData(xData);

                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);

                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2.5, StartY_L_Tunnel + laneNum * laneWidth - 6.5 + width_R_Emergency, "shengguang", 1, 0);

                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2, StartY_L_Tunnel + laneNum * laneWidth - 1.5 + width_R_Emergency, "shoudong", 1, 0);
                    }

                }
                else if (Jia_or_Yi == 1)
                {
                   
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/yixingxiaofangshebeidong.dwg", "yixingxiaofangshebeidong");
                    Int64 BlockId = mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth - 4.7 + width_R_Emergency, "yixingxiaofangshebeidong", 1.5, 0);

                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);
                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 1, StartY_L_Tunnel + laneNum * laneWidth - 1.1 + width_R_Emergency, "shengguang", 1, 0);
                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp - 2, StartY_L_Tunnel + laneNum * laneWidth - 1.3 + width_R_Emergency, "shoudong", 0.5, 0);
                    }

                }

                //插入紧急疏散标志
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjishusanzhishibiaozhi.dwg", "jinjishusan");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth  + 2 + width_R_Emergency, "jinjishusan", 1.5, 0);

                //标注插入以米为单位插入
                DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_L_Tunnel + laneNum * laneWidth  + width_R_Emergency,0);
            }
            else
            {
                if (Jia_or_Yi == 0)
                {
                   
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaxingxiaofangshebeidong.dwg", "jiaxingxiaofangshebeidong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth - 4.6, "jiaxingxiaofangshebeidong", 1.5, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);

                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2.5, StartY_L_Tunnel + laneNum * laneWidth - 6.5, "shengguang", 1, 0);

                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2, StartY_L_Tunnel + laneNum * laneWidth - 1.5, "shoudong", 1, 0);
                    }
                }
                else if (Jia_or_Yi == 1)
                {
                   
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/yixingxiaofangshebeidong.dwg", "yixingxiaofangshebeidong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth - 4.7, "yixingxiaofangshebeidong", 1.5, 0);
                    //加入碰撞检测
                    setCollosion(tmp, size, collisionState);
                    //插入声光报警器
                    if (isHave_Shengguang == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp + 1, StartY_L_Tunnel + laneNum * laneWidth-1.1, "shengguang", 1, 0);
                    }
                    //插入手动报警按钮
                    if (isHave_Shoudong == 1)
                    {
                        mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                        mxCt1.axMxDrawX1.DrawBlockReference(tmp - 2, StartY_L_Tunnel + laneNum * laneWidth - 1.3, "shoudong", 0.5, 0);
                    }
                }

                //插入紧急疏散标志
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjishusanzhishibiaozhi.dwg", "jinjishusan");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel + laneNum * laneWidth + 2, "jinjishusan", 1.5, 0);

                //标注插入以米为单位插入
               DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp , StartY_L_Tunnel + laneNum * laneWidth,0);
            }

            return tmp;
        }

        /*
        * 插入左幅幅下侧消防洞
        * 插入单位为米
        */
        public int InsertBlock_L_L(MxDrawControl mxCt1, double location, double shift, double gap, int Jia_or_Yi, double size, double laneNum, double laneWidth, int[] collisionState, int isHave_Shengguang, int isHave_Shoudong)
        {
           
            int tmp = 0;
            tmp = getRightLocation(location, size, shift, EndX_L_Tunnel, collisionState);
            if (Jia_or_Yi == 0)
            {
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/jiaxingxiaofangshebeidong.dwg", "jiaxingxiaofangshebeidong");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel - 4.6, "jiaxingxiaofangshebeidong", 1.5, 0);
                //加入碰撞检测
                setCollosion(tmp, size, collisionState);

                //插入声光报警器
                if (isHave_Shengguang == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2.5, StartY_L_Tunnel  - 6.5, "shengguang", 1, 0);

                }
                //插入手动报警按钮
                if (isHave_Shoudong == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp + 2, StartY_L_Tunnel  - 1.5, "shoudong", 1, 0);
                }
            }
            else if (Jia_or_Yi == 1)
            {
                mxCt1.axMxDrawX1.InsertBlock("sourceblock/yixingxiaofangshebeidong.dwg", "yixingxiaofangshebeidong");
                mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel - 4.7, "yixingxiaofangshebeidong", 1.5, 0);
                //加入碰撞检测
                setCollosion(tmp, size, collisionState);
                //插入声光报警器
                if (isHave_Shengguang == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shengguangbaojingqi.dwg", "shengguang");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp + 1, StartY_L_Tunnel  - 1.1, "shengguang", 1, 0);
                }
                //插入手动报警按钮
                if (isHave_Shoudong == 1)
                {
                    mxCt1.axMxDrawX1.InsertBlock("sourceblock/shoudongbaojinganniu.dwg", "shoudong");
                    mxCt1.axMxDrawX1.DrawBlockReference(tmp - 2, StartY_L_Tunnel  -1.3, "shoudong", 0.5, 0);
                }
            }

            //插入紧急疏散标志
            mxCt1.axMxDrawX1.InsertBlock("sourceblock/jinjishusanzhishibiaozhi.dwg", "jinjishusan");
            mxCt1.axMxDrawX1.DrawBlockReference(tmp, StartY_L_Tunnel  + 2, "jinjishusan", 1.5, 0);

            //标注插入以米为单位插入
            DrawEquipmentLocation(mxCt1, mxCt1.axMxDrawX1, tmp, StartY_L_Tunnel, 1);


            return tmp;
        }
       
    }
}
