using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace FocusingLensAligner
{
    class GeneralFunction
    {
        //获取指定配置文件名所对应的文件全路径名称
        public static string GetConfigFilePath(string filename)
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            if (!Directory.Exists(Path.GetDirectoryName(path) + "\\Config\\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path) + "\\Config\\");
            }
            string FilePath = Path.GetDirectoryName(path) + "\\Config\\" + filename;
            return FilePath;
        }

        //获取指定产品配置文件名所对应的文件全路径名称
        public static string GetProductConfigFilePath(string filename)
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            if (!Directory.Exists(Path.GetDirectoryName(path) + "\\Config\\ProductConfig\\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path) + "\\Config\\ProductConfig\\");
            }
            string FilePath = Path.GetDirectoryName(path) + "\\Config\\ProductConfig\\" + filename;
            return FilePath;
        }

        //获取指定文件名所对应的文件全路径名称
        public static string GetApplicationFilePath(string filename)
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string FilePath = Path.GetDirectoryName(path) + "\\" + filename;
            return FilePath;
        }

        //搜索指定文件夹中指定类型文件的所有相关文件名
        public static List<FileInfo> GetFileInfos(string path, string extName)
        {
            List<FileInfo> fileinfolist = new List<FileInfo>();
            string[] dir = Directory.GetDirectories(path);
            DirectoryInfo fdir = new DirectoryInfo(path);
            FileInfo[] fileinfos = fdir.GetFiles();
            if (fileinfos.Length != 0 || dir.Length != 0)
            {
                foreach (FileInfo fileinfo in fileinfos)
                {
                    if (extName.ToLower().IndexOf(fileinfo.Extension.ToLower()) >= 0)
                    {
                        fileinfolist.Add(fileinfo);
                    }
                }
            }
            return fileinfolist;
        }

        //延时（毫秒）
        public static void Delay(int milliSecond)
        {
            System.Threading.Thread.Sleep(milliSecond);
        }

        //更新XML配置文件中指定元素项
        public static void UpdateXmlFileElement(string filepath, string[] elementstr, string newValue, ref string errMsg)
        {

            if (!File.Exists(filepath))
            {
                errMsg = "File is not found.";
                return;
            }

            if (elementstr[0].Length == 0)
            {
                errMsg = "Node is null, please set the node name.";
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode tempNode = null;
            XmlNodeList nodeList = null;
            XmlNode firstNode = null;
            XmlNode secondNode = null;
            XmlNode thirdNode = null;
            xmlDoc.Load(filepath);
            if (elementstr[0].Length > 0)
            {
                nodeList = xmlDoc.GetElementsByTagName(elementstr[0]);
                if (nodeList.Count == 0)
                {
                    errMsg = "Xml file does't contain the first node. ";
                    return;
                }
                else
                {
                    firstNode = nodeList.Item(0);
                    tempNode = firstNode;
                }
            }
            if (elementstr[1].Length > 0)
            {
                secondNode = firstNode.SelectSingleNode(elementstr[1]);
                if (secondNode != null)
                {
                    tempNode = secondNode;
                }
                else
                {
                    errMsg = "Xml file does't contain the second node. ";
                    return;
                }
            }
            if (elementstr[2].Length > 0)
            {
                thirdNode = secondNode.SelectSingleNode(elementstr[2]);
                if (thirdNode != null)
                {
                    tempNode = thirdNode;
                }
                else
                {
                    errMsg = "Xml file does't contain the third node. ";
                    return;
                }
            }

            if (tempNode != null)
            {
                tempNode.InnerText = newValue;
                xmlDoc.Save(filepath);
            }
        }

        //设置传感器状态指示灯颜色
        public static void SetIOPictureBoxImage(PictureBox pbx, bool on, int colormode)
        {
            switch (colormode)
            {
                case 0:
                    if (on) pbx.BackgroundImage = Properties.Resources.Greenled;
                    else pbx.BackgroundImage = Properties.Resources.Grayled;
                    break;
                case 1:
                    if (on) pbx.BackgroundImage = Properties.Resources.Grayled;
                    else pbx.BackgroundImage = Properties.Resources.Greenled;
                    break;
                case 2:
                    if (on) pbx.BackgroundImage = Properties.Resources.Redled;
                    else pbx.BackgroundImage = Properties.Resources.Grayled;
                    break;
                case 3:
                    if (on) pbx.BackgroundImage = Properties.Resources.Grayled;
                    else pbx.BackgroundImage = Properties.Resources.Redled;
                    break;
            }
        }

        //设置机台运行状态指示灯颜色
        public static void SetStatusIndicateLedPictureBoxImage(PictureBox pbx, bool on, int colormode)
        {
            switch (colormode)
            {
                case 0:
                    if (on) pbx.BackgroundImage = Properties.Resources.Greenled;
                    else pbx.BackgroundImage = Properties.Resources.Grayled;
                    break;
                case 1:
                    if (on) pbx.BackgroundImage = Properties.Resources.Grayled;
                    else pbx.BackgroundImage = Properties.Resources.Greenled;
                    break;
                case 2:
                    if (on) pbx.BackgroundImage = Properties.Resources.Redled;
                    else pbx.BackgroundImage = Properties.Resources.Grayled;
                    break;
                case 3:
                    if (on) pbx.BackgroundImage = Properties.Resources.Grayled;
                    else pbx.BackgroundImage = Properties.Resources.Redled;
                    break;
            }
        }

        //保存系统配置文件
        public static bool SaveSystemConfig()
        {
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("SystemConfig.xml");
            res = STD_IGeneralTool.GeneralTool.TryToSave(GlobalParameters.systemconfig, filepath);
            return res;
        }

        //调取系统配置文件
        public static bool LoadSystemConfig()
        {
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("SystemConfig.xml");
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<SystemConfig>(ref GlobalParameters.systemconfig, filepath);
            return res;
        }

        //根据产品名称调取产品配置文件
        public static bool LoadProductConfig(string productName)
        {
            bool res = false;

            string filepath = GeneralFunction.GetConfigFilePath("ProductConfig\\" + productName);
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<ProductConfig>(ref GlobalParameters.productconfig, filepath);
            return res;
        }

        //调取MES配置参数
        public static bool LoadMESConfig()
        {
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("Camstar\\" + "CamstarConfig.xml");
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<MESConfig>(ref GlobalParameters.MESConfig, filepath);
            return res;
        }

        //获取单步流程名称
        public static string GetWorkflowName(string workflow)
        {
            string str;
            switch (workflow.ToString())
            {
                case "UPCAMVIEW_BOX":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "上相机识别产品盒子";
                    else
                        str = "Up Camera View Box";
                    break;

                case "PICK_BOX":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "从料盘夹取产品盒子";
                    else
                        str = "Pick Box From Tray";
                    break;

                case "BOX_TOSCANNER":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "产品盒子移动到扫码器";
                    else
                        str = "Move Box To Scanner";
                    break;

                case "BOX_TONEST":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "产品盒子放入Nest";
                    else
                        str = "Place Box Into Nest";
                    break;

                case "DNCAMVIEW_WINDOW":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "下相机识别盒子窗口";
                    else
                        str = "Down Camera View Box Window";
                    break;

                case "GET_LASERSPOTPOS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "获取光斑位置";
                    else
                        str = "Get Laser Spot Position";
                    break;

                case "POWER_ON":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "产品加电";
                    else
                        str = "Laser on";
                    break;

                case "POWER_OFF":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "产品下电";
                    else
                        str = "Laser off";
                    break;

                case "GET_BOXSN":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "获取产品SN信息";
                    else
                        str = "Get Box SN";
                    break;

                case "PICK_BOXFROMNEST":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "从Nest中取出产品盒子";
                    else
                        str = "Pick Box from Nest";
                    break;

                case "BOX_TOTRAY":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "产品盒子放回料盘";
                    else
                        str = "Place Box Into Tray";
                    break;

                case "UPCAMVIEW_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "上相机识别Lens";
                    else
                        str = "Up Camera View Lens";
                    break;

                case "PICK_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "从料盘夹取Lens";
                    else
                        str = "Pick Lens From Tray";
                    break;

                case "LENS_TOBOX":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "Lens放入产品盒子";
                    else
                        str = "Place Lens Into Box";
                    break;

                case "ALIGN_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "Lens耦合";
                    else
                        str = "Align Lens";
                    break;

                case "ATTACH_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "贴装Lens";
                    else
                        str = "Attach Lens";
                    break;

                case "UV_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "UV固化Lens";
                    else
                        str = "UV Lens";
                    break;

                case "RELEASE_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "释放Lens";
                    else
                        str = "Release Lens";
                    break;

                case "DISCARD_LENS":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "Lens抛料";
                    else
                        str = "Discard Lens";
                    break;

                case "DIP_EPOXY":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "蘸胶";
                    else
                        str = "Dip Epoxy";
                    break;

                case "EPOXY_BOX":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "点胶到产品盒子";
                    else
                        str = "Epoxy Box";
                    break;

                case "EPOXY_WIPE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "擦胶";
                    else
                        str = "Epoxy Wipe";
                    break;

                case "UPCAMVIEW_EPOXY":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "上相机看胶斑";
                    else
                        str = "Up Camera View Epoxy Spot";
                    break;

                case "AUTO_REMOVE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "自动拆料";
                    else
                        str = "Auto Remove";
                    break;

                case "COMPONENT_ISSUE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "自动发料";
                    else
                        str = "Component Issue";
                    break;

                case "CHECK_BOMLIST":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "检查产品BOM";
                    else
                        str = "Check BOM List";
                    break;

                case "AUTO_MOVEOUT":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "自动过站";
                    else
                        str = "Auto Move Out";
                    break;

                case "AUTO_HOLD":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "自动Hold";
                    else
                        str = "Auto Hold";
                    break;

                case "CHECKSPOT_BEFOREALIGN":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "耦合前光斑确认";
                    else
                        str = "Check Spot Before Align";
                    break;

                case "CHECKLENS_BEFOREUVCURE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "UV固化前Lens中心确认";
                    else
                        str = "Check Lens Center Before UV Cure";
                    break;

                case "CHECKSPOT_BEFOREUVCURE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "UV固化前光斑确认";
                    else
                        str = "Check Spot Before UV Cure";
                    break;

                case "CHECKLENS_AFTERUVCURE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "UV固化后Lens中心确认";
                    else
                        str = "Check Lens Center After UV Cure";
                    break;

                case "CHECKSPOT_AFTERUVCURE":
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "UV固化后光斑确认";
                    else
                        str = "Check Spot After UV Cure";
                    break;

                default:
                    if (GlobalParameters.flagassembly.chineseflag)
                        str = "未命名";
                    else
                        str = "Unnamed";
                    break;
            }

            return str;
        }

        //获取单步流程代号
        public static int GetWorkflowNum(string workflowname)
        {
            string str;
            switch (workflowname.ToString())
            {
                case "上相机识别产品盒子":
                case "Up Camera View Box":
                    str = "UPCAMVIEW_BOX";
                    break;

                case "从料盘夹取产品盒子":
                case "Pick Box From Tray":
                    str = "PICK_BOX";
                    break;

                case "产品盒子移动到扫码器":
                case "Move Box To Scanner":
                    str = "BOX_TOSCANNER";
                    break;

                case "产品盒子放入Nest":
                case "Place Box Into Nest":
                    str = "BOX_TONEST";
                    break;

                case "下相机识别盒子窗口":
                case "Down Camera View Box Window":
                    str = "DNCAMVIEW_WINDOW";
                    break;

                case "获取光斑位置":
                case "Get Laser Spot Position":
                    str = "GET_LASERSPOTPOS";
                    break;

                case "产品加电":
                case "Laser on":
                    str = "POWER_ON";
                    break;

                case "产品下电":
                case "Laser off":
                    str = "POWER_OFF";
                    break;

                case "获取产品SN信息":
                case "Get Box SN":
                    str = "GET_BOXSN";
                    break;

                case "从Nest中取出产品盒子":
                case "Pick Box from Nest":
                    str = "PICK_BOXFROMNEST";
                    break;

                case "产品盒子放回料盘":
                case "Place Box Into Tray":
                    str = "BOX_TOTRAY";
                    break;

                case "上相机识别Lens":
                case "Up Camera View Lens":
                    str = "UPCAMVIEW_LENS";
                    break;

                case "从料盘夹取Lens":
                case "Pick Lens From Tray":
                    str = "PICK_LENS";
                    break;

                case "Lens放入产品盒子":
                case "Place Lens Into Box":
                    str = "LENS_TOBOX";
                    break;

                case "Lens耦合":
                case "Align Lens":
                    str = "ALIGN_LENS";
                    break;

                case "贴装Lens":
                case "Attach Lens":
                    str = "ATTACH_LENS";
                    break;

                case "UV固化Lens":
                case "UV Lens":
                    str = "UV_LENS";
                    break;

                case "释放Lens":
                case "Release Lens":
                    str = "RELEASE_LENS";
                    break;

                case "Lens抛料":
                case "Discard Lens":
                    str = "DISCARD_LENS";
                    break;

                case "蘸胶":
                case "Dip Epoxy":
                    str = "DIP_EPOXY";
                    break;

                case "点胶到产品盒子":
                case "Epoxy Box":
                    str = "EPOXY_BOX";
                    break;

                case "擦胶":
                case "Epoxy Wipe":
                    str = "EPOXY_WIPE";
                    break;

                case "上相机看胶斑":
                case "Up Camera View Epoxy Spot":
                    str = "UPCAMVIEW_EPOXY";
                    break;

                case "自动拆料":
                case "Auto Remove":
                    str = "AUTO_REMOVE";
                    break;

                case "自动发料":
                case "Component Issue":
                    str = "COMPONENT_ISSUE";
                    break;

                case "检查产品BOM":
                case "Check BOM List":
                    str = "CHECK_BOMLIST";
                    break;

                case "自动过站":
                case "Auto Move Out":
                    str = "AUTO_MOVEOUT";
                    break;

                case "自动Hold":
                case "Auto Hold":
                    str = "AUTO_HOLD";
                    break;

                case "耦合前光斑确认":
                case "Check Spot Before Align":
                    str = "CHECKSPOT_BEFOREALIGN";
                    break;

                case "UV固化前Lens中心确认":
                case "Check Lens Center Before UV Cure":
                    str = "CHECKLENS_BEFOREUVCURE";
                    break;

                case "UV固化前光斑确认":
                case "Check Spot Before UV Cure":
                    str = "CHECKSPOT_BEFOREUVCURE";
                    break;

                case "UV固化后Lens中心确认":
                case "Check Lens Center After UV Cure":
                    str = "CHECKLENS_AFTERUVCURE";
                    break;

                case "UV固化后光斑确认":
                case "Check Spot After UV Cure":
                    str = "CHECKSPOT_AFTERUVCURE";
                    break;

                default:
                    str = "";
                    break;
            }

            int Num = (int)Enum.Parse(typeof(Enum_WorkFlowList), str);
            return Num;
        }
    }
}
