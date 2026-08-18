using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WinformClient.Model;
using static System.Net.Mime.MediaTypeNames;

namespace WLD.SimpleMes.WinformClient.HostObject
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class TSCHostObject
    {
        /// <summary>
        /// 如果连接本地打印机，例：TSC CLEVER TTP-243
        /// 如果连接网络打印机，例： \\SERVER\TTP243
        /// </summary>
        private static string PringName = SettingModel.Instance.PringName;
        /// <summary>
        /// 标签宽度
        /// </summary>
        private static string LableWidth = SettingModel.Instance.LableWidth.ToString();
        /// <summary>
        /// 标签高度
        /// </summary>
        private static string LableHeight = SettingModel.Instance.LableHeight;
        /// <summary>
        /// 打印机类型 （对应列印速度）
        /// </summary>
        private static string PrintType = SettingModel.Instance.PrintType;

        /// <summary>
        /// 打印入库单批次号
        /// </summary>
        /// <param name="message"></param>
        /// <param name="counts"></param>
        public async Task printBatchByInStockInfo(string message, int counts)
        {
            try
            {
                JObject json = JObject.Parse(message);
                TSCLIB_DLL.openport(PringName);
                TSCLIB_DLL.setup(LableWidth, LableHeight, PrintType, "5", "0", "2", "");
                TSCLIB_DLL.clearbuffer();
                TSCLIB_DLL.windowsfont(40, 20, 30, 0, 2, 0, "Arial", "物料编码：");
                TSCLIB_DLL.windowsfont(170, 20, 30, 0, 0, 0, "Arial", json["materialNumber"].ToString());
                TSCLIB_DLL.windowsfont(40, 60, 30, 0, 2, 0, "Arial", "物料名称：");
                TSCLIB_DLL.windowsfont(170, 60, 30, 0, 0, 0, "Arial", json["materialName"].ToString());
                TSCLIB_DLL.windowsfont(40, 100, 30, 0, 2, 0, "Arial", "批 次 号 ：");
                TSCLIB_DLL.windowsfont(170, 100, 30, 0, 0, 0, "Arial", json["batchNo"].ToString());
                TSCLIB_DLL.windowsfont(40, 140, 30, 0, 2, 0, "Arial", "采购单号：");
                TSCLIB_DLL.windowsfont(170, 140, 30, 0, 0, 0, "Arial", json["fSourceBillNo"].ToString());
                TSCLIB_DLL.windowsfont(40, 180, 30, 0, 2, 0, "Arial", "入库单号：");
                TSCLIB_DLL.windowsfont(170, 180, 30, 0, 0, 0, "Arial", json["warehousingNumber"].ToString());
                TSCLIB_DLL.windowsfont(40, 220, 30, 0, 2, 0, "Arial", "入库时间：");
                TSCLIB_DLL.windowsfont(170, 220, 30, 0, 0, 0, "Arial", json["warehousingTime"].ToString().Substring(0, 10));
                TSCLIB_DLL.windowsfont(40, 260, 30, 0, 2, 0, "Arial", "入库数量：");
                TSCLIB_DLL.windowsfont(170, 260, 30, 0, 0, 0, "Arial", json["receiptQuantity"].ToString());
                string QRCODEString = "QRCODE 370,135,H,6,A,0,M2,S7,\"" + json["batchNo"].ToString() + "\"";
                TSCLIB_DLL.sendcommand(QRCODEString);
                TSCLIB_DLL.printlabel("1", counts.ToString());
                TSCLIB_DLL.closeport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await Task.CompletedTask;

        }

        /// <summary>
        /// 打印成品编号
        /// </summary>
        /// <param name="message"></param>
        /// <param name="counts"></param>
        public async Task printManufacturedNumber(string message)
        {
            try
            {
                JObject json = JObject.Parse(message);
                TSCLIB_DLL.openport(PringName);
                TSCLIB_DLL.setup(LableWidth, LableHeight, PrintType, "5", "0", "2", "");
                TSCLIB_DLL.clearbuffer();
                TSCLIB_DLL.windowsfont(60, 20, 30, 0, 2, 0, "Arial", "产品名称：");
                TSCLIB_DLL.windowsfont(190, 20, 30, 0, 0, 0, "Arial", json["materialName"].ToString());
                TSCLIB_DLL.windowsfont(60, 60, 30, 0, 2, 0, "Arial", "物料编码 ：");
                TSCLIB_DLL.windowsfont(190, 60, 30, 0, 0, 0, "Arial", json["materialNumber"].ToString());
                TSCLIB_DLL.windowsfont(60, 100, 30, 0, 2, 0, "Arial", "序列号：");
                TSCLIB_DLL.windowsfont(190, 100, 30, 0, 0, 0, "Arial", json["batchNumber"].ToString());
                TSCLIB_DLL.windowsfont(60, 140, 30, 0, 2, 0, "Arial", "工单号：");
                TSCLIB_DLL.windowsfont(190, 140, 30, 0, 0, 0, "Arial", json["fromOrderNumber"].ToString());
                string QRCODEString = "QRCODE 380,155,H,6,A,0,M2,S7,\"" + json["batchNumber"].ToString() + "\"";
                TSCLIB_DLL.sendcommand(QRCODEString);
                TSCLIB_DLL.printlabel("1", "1");
                TSCLIB_DLL.closeport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 打印小批次号（物料）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="counts"></param>
        public async Task printManufacturedNumberToSplitAsync(string message)
        {
            try
            {
                JObject json = JObject.Parse(message);
                TSCLIB_DLL.openport(PringName);
                TSCLIB_DLL.setup(LableWidth, LableHeight, PrintType, "5", "0", "2", "");
                TSCLIB_DLL.clearbuffer();
                TSCLIB_DLL.windowsfont(20, 20, 30, 0, 2, 0, "Arial", "物料编码：");
                TSCLIB_DLL.windowsfont(150, 20, 30, 0, 0, 0, "Arial", json["materialNumber"].ToString());
                TSCLIB_DLL.windowsfont(20, 60, 30, 0, 0, 0, "Arial", "物料名称");
                TSCLIB_DLL.windowsfont(150, 60, 30, 0, 0, 0, "Arial", json["materialName"].ToString());
                TSCLIB_DLL.windowsfont(20, 100, 30, 0, 2, 0, "Arial", "批次号：");
                TSCLIB_DLL.windowsfont(150, 100, 30, 0, 0, 0, "Arial", json["batchNumber"].ToString());
                TSCLIB_DLL.windowsfont(20, 140, 30, 0, 0, 0, "Arial", "源批次号：");
                TSCLIB_DLL.windowsfont(150, 140, 30, 0, 0, 0, "Arial", json["fromErpBatchNumber"].ToString());
                TSCLIB_DLL.windowsfont(20, 180, 30, 0, 2, 0, "Arial", "批次数量：");
                TSCLIB_DLL.windowsfont(150, 180, 30, 0, 0, 0, "Arial", json["matrialCount"].ToString());
                TSCLIB_DLL.windowsfont(20, 220, 30, 0, 2, 0, "Arial", "工单号：");
                TSCLIB_DLL.windowsfont(150, 220, 30, 0, 0, 0, "Arial", json["fromOrderNumber"].ToString());
                string QRCODEString = "QRCODE 380,180,H,5,A,0,M2,S7,\"" + json["batchNumber"].ToString() + "\"";

                TSCLIB_DLL.sendcommand(QRCODEString);
                TSCLIB_DLL.printlabel("1", "1");
                TSCLIB_DLL.closeport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 打印铭牌
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task printNameplate(string message, string ddcc, string ddzl)
        {
            JObject json = JObject.Parse(message);
            TSCLIB_DLL.openport(PringName);
            StringBuilder commandString = new StringBuilder();
            var prodcueTime = DateTime.Parse(json["produceDateTime"].Value<string>()).ToString("yyyy年MM月dd日");

            commandString.AppendLine("SIZE 70 mm,45 mm");
            commandString.AppendLine("DIRECTION 1");
            commandString.AppendLine("CLS");
            commandString.AppendLine("BOX 16,16,546,344,3");
            commandString.AppendLine("BAR 16,99,530,3");
            commandString.AppendLine("PUTBMP 16,24,\"vlogo200\",8");
            commandString.AppendLine($"TEXT 32,112,\"FONT001\",0,1,1,1,\"产品名称: {json["matreialName"]}\"");
            commandString.AppendLine("TEXT 290,112,\"FONT001\",0,1,1,1,\"运行环境温度: -10~40℃\"");
            commandString.AppendLine($"TEXT 32,152,\"FONT001\",0,1,1,1,\"出厂编号: {json["belongMaterialBatchNumber"]}\"");
            commandString.AppendLine("TEXT 290,152,\"FONT001\",0,1,1,1,\"运行环境湿度: 5%~95%\"");
            commandString.AppendLine($"TEXT 32,192,\"FONT001\",0,1,1,1,\"电堆内阻: {json["internalResistance"]}Ω.cm²\"");
            commandString.AppendLine($"TEXT 32,232,\"FONT001\",0,1,1,1,\"电堆重量: {ddzl}\"");
            commandString.AppendLine($"TEXT 32,272,\"FONT001\",0,1,1,1,\"生产日期: {prodcueTime}\"");
            commandString.AppendLine($"TEXT 32,312,\"FONT001\",0,1,1,1,\"电堆尺寸: {ddcc}\"");
            commandString.AppendLine($"QRCODE 343,202,H,5,A,0,M2,S7,\"{json["belongMaterialBatchNumber"]}\"");
            commandString.AppendLine("PRINT 1,1");
            TSCLIB_DLL.sendcommand(commandString.ToString());
            TSCLIB_DLL.closeport();
        }


        /// <summary>
        /// 打印无Logo的铭牌
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ddcc"></param>
        /// <param name="ddzl"></param>
        /// <returns></returns>
        public async Task printNameplateNoLogo(string message, string ddcc, string ddzl)
        {
            JObject json = JObject.Parse(message);
            TSCLIB_DLL.openport(PringName);
            StringBuilder commandString = new StringBuilder();
            var prodcueTime = DateTime.Parse(json["produceDateTime"].Value<string>()).ToString("yyyy年MM月dd日");

            commandString.AppendLine("SIZE 70 mm,45 mm");
            commandString.AppendLine("DIRECTION 1");
            commandString.AppendLine("CLS");
            commandString.AppendLine("BOX 16,16,546,344,3");
            commandString.AppendLine($"TEXT 32,88,\"FONT001\",0,1,1,1,\"产品名称: {json["matreialName"]}\"");
            commandString.AppendLine("TEXT 290,88,\"FONT001\",0,1,1,1,\"运行环境温度: -10~40℃\"");
            commandString.AppendLine($"TEXT 32,128,\"FONT001\",0,1,1,1,\"出厂编号: {json["belongMaterialBatchNumber"]}\"");
            commandString.AppendLine("TEXT 290,128,\"FONT001\",0,1,1,1,\"运行环境湿度: 5%~95%\"");
            commandString.AppendLine($"TEXT 32,168,\"FONT001\",0,1,1,1,\"电堆内阻: {json["internalResistance"]}Ω.cm²\"");
            commandString.AppendLine($"TEXT 32,208,\"FONT001\",0,1,1,1,\"电堆重量: {ddzl}\"");
            commandString.AppendLine($"TEXT 32,248,\"FONT001\",0,1,1,1,\"生产日期: {prodcueTime}\"");
            commandString.AppendLine($"TEXT 32,388,\"FONT001\",0,1,1,1,\"电堆尺寸: {ddcc}\"");
            commandString.AppendLine($"QRCODE 343,178,H,5,A,0,M2,S7,\"{json["belongMaterialBatchNumber"]}\"");
            commandString.AppendLine("PRINT 1,1");
            TSCLIB_DLL.sendcommand(commandString.ToString());
            TSCLIB_DLL.closeport();
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        public void CloseWinform()
        {
            System.Environment.Exit(0);
        }
    }
}

