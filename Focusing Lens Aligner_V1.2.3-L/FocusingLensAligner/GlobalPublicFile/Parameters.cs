using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace FocusingLensAligner
{
    #region 公共枚举变量申明

    public enum Enum_MachineStatus
    {
        //机台状态枚举
        NORMAL = 0,
        ERROR,
        REMINDER
    }

    public enum Enum_RunStatus
    {
        //机台运行状态枚举
        RUN_STOP = 0,
        RUN_READYRUN,
        RUN_ING,
        RUN_ERR,
        RUN_IDLE,
        RUN_DOWN,
        RUN_WARNING,
        RUN_SUSPEND,
        RUN_PROCESS,
    }

    public enum Enum_UPDATEMODE
    {
        UPDATE_INFO = 0,
        UPDATE_PINSTATUS,
    }

    public enum Enum_WorkFlowList
    {
        //流程类型枚举
        //----与Box相关----
        UPCAMVIEW_BOX = 0,              //上相机识别产品Box
        PICK_BOX,                       //从料盘夹取产品Box
        BOX_TOSCANNER,                  //产品Box移动到扫码器
        BOX_TONEST,                     //产品Box放入Nest(包含锁定)
        DNCAMVIEW_WINDOW,               //下相机识别产品Box窗口
        GET_LASERSPOTPOS,               //获取光斑位置
        POWER_ON,                       //产品加电
        POWER_OFF,                      //产品下电
        GET_BOXSN,                      //获取产品SN信息(通信方式)
        PICK_BOXFROMNEST,               //从Nest中取出产品Box(包含解锁)
        BOX_TOTRAY,                     //将产品Box放回料盘(包含夹爪释放)

        //----与Lens相关----
        UPCAMVIEW_LENS,                 //上相机识别Lens
        PICK_LENS,                      //从料盘夹取Lens
        LENS_TOBOX,                     //将Lens放入产品Box
        ALIGN_LENS,                     //Lens耦合
        ATTACH_LENS,                    //贴装Lens
        UV_LENS,                        //UV固化Lens
        RELEASE_LENS,                   //夹爪释放Lens
        DISCARD_LENS,                   //Lens抛料

        //----胶水相关----
        DIP_EPOXY,                      //蘸胶
        EPOXY_BOX,                      //点胶到产品Box
        EPOXY_WIPE,                     //擦胶
        UPCAMVIEW_EPOXY,                //上相机看胶斑

        //----MES功能相关----
        AUTO_REMOVE,                    //自动拆料
        COMPONENT_ISSUE,                //自动发料
        CHECK_BOMLIST,                  //检查产品BOM
        AUTO_MOVEOUT,                   //自动过站
        AUTO_HOLD,                      //自动Hold

        //----光斑位置确认----
        CHECKSPOT_BEFOREALIGN,          //耦合前光斑确认
        CHECKSPOT_BEFOREUVCURE,         //UV固化前光斑确认
        CHECKSPOT_AFTERUVCURE,          //UV固化后光斑确认

        //----Lens中心位置确认----
        CHECKLENS_BEFOREUVCURE,         //UV固化前Lens中心确认
        CHECKLENS_AFTERUVCURE,          //UV固化后Lens中心确认
    }

    public enum Enum_AxisName
    {
        //轴名称枚举
        X1,                             //龙门伺服X轴
        Y1,                             //龙门伺服Y轴
        Z1,                             //龙门伺服Z轴
        X2,                             //步进X轴
        Y2,                             //步进Y轴
        Z2,                             //步进Z轴
        U1,                             //步进旋转U轴
        PogoPin                         //SMC卡片电机加电Pin针轴
    }

    public enum Enum_PowerWay
    {
        //产品加电方式枚举
        VOLTAGE_I2C = 0,                //I2C通信模块
        VOLTAGE_FI2CUSB = 1,            //FI2CUSB模块
        CURRENT_IO = 2,                 //IO点切换直接加电
        CURRENT_NO = 3,                 //源表直接加电
        DRIVERBOARD = 4,                //S4500驱动板
        EUIBOARD = 5                    //EUI模块加电
    }

    public enum Enum_AlignmentWay
    {
        //Lens耦合方式枚举
        WINDOW_LENS = 0,                //Lens中心和产品Box窗口中心对位
        LASER_LENS = 1,                 //Lens中心和多通道光斑平衡中心对位
        LASER_LASER = 2                 //Lens中心和光斑中心对位
    }

    public enum Enum_LaserSpotPosBalanceMode
    {
        //多通道光斑中心位置平衡方式枚举
        MEAN = 0,                       //光斑中心位置平均值
        MIDDLE = 1                      //光斑中心位置中间值
    }

    public enum Enum_PogoPinPosition
    {
        //加电Pin针作用方式枚举
        SIDE_PIN = 0,                   //在产品侧边合上加电Pin针
        TOP_PIN = 1,                    //在产品顶部合上加电Pin针
        SIDETOP_PIN = 2,                //在产品顶部和侧边同时合上加电Pin针
        PCBA_PIN = 3                    //PCBA类产品加电Pin针
    }

    public enum Enum_PressLensProtectMode
    {
        //LENS夹爪压力保护场合枚举
        NONE = 0,                       //无保护
        PICK_ONLY = 1,                  //在夹取LENS时有保护
        PLACE_ONLY = 2,                 //在夹取LENS放入产品Box内部时有保护
        PICK_PLACE = 3                  //在夹取LENS和将LENS放入产品Box内部时都有保护
    }

    public enum Enum_SignalTowerLamp
    {
        //信号塔三色灯标识枚举
        RED = 0,                        //红灯
        YELLOW = 1,                     //黄灯
        GREEN = 2,                      //绿灯
        AllTurnOff = 3                  //全灭
    }

    public enum Enum_DnCameraViewObject
    {
        //下相机拍摄的目标
        Window = 0,                     //产品Box窗口
        LaserSpot = 1,                  //光斑
        Lens = 2                        //产品Box中的Lens
    }

    public enum Enum_FinalResult
    {
        //产品Box耦合组装最终结果枚举
        PASS = 0,                       //耦合贴装成功
        FAIL = 1,                       //耦合贴装失败
        MESFAIL = 2,                    //耦合贴装成功但MES功能操作失败
    }

    public enum Enum_VisionProcessType
    {
        DownCameraRecognizeWindow = 0,          //下相机识别产品Box窗口中心位置并测量窗口直径
        DownCameraRecognizeLens = 1,            //下相机识别产品Box中的Lens中心位置
        DownCameraRecognizeLaserSpot = 2,       //下相机识别光斑中心位置
        UpCameraRecognizeBoxCorner = 3,         //上相机识别产品Box左上角位置
        UpCameraRecognizeEpoxySpot = 4,         //上相机识别胶斑中心位置并测量胶斑直径
        UpCameraRecognizeLens = 5,              //上相机识别Lens中心位置(识别Lens反射光斑中心位置)
        UpCameraRecognizeGripper = 6,           //上相机识别Lens夹爪中心位置(识别Lens夹爪上所夹持的Lens反射光斑中心位置)
    }

    #endregion

    #region 公共参数申明

    public class SystemOperationInfo
    {
        //机台操作相关信息
        public string machineid;        //机台编号    
        public string useraccount;      //用户Camstar帐号
        public string userpassword;     //用户Camstar密码
        public string producttype;      //当前产品类型
        public string swversion;        //软件版本
    }

    public class FlagAssembly
    {
        //各种标志枚举
        public bool chineseflag;                //中文模式
        public bool homeflag;                   //已执行过轴复位操作
        public bool stopflag;                   //机台停止自动运行
        public bool estopflag;                  //急停按钮导致的急停状态
        public bool singlestepflag;             //单步运行模式
        public bool continueflag;               //自动连续运行模式
        public bool showpressureforceflag;      //实时显示压力传感器读数
        public bool befirstproductflag;         //自动运行模式下的第一个产品标记
        public bool checkepoxyonboxflag;        //自动运行模式下检查产品窗口上点胶状态标记
        public bool epoxypininboxflag;          //点胶针还处在产品Box内部
        public bool lensongripperflag;          //有Lens残留在Lens夹爪上
        public bool lensgripperinboxflag;       //Lens夹爪还处在产品Box内部
        public bool boxongripperflag;           //产品Box被Box夹爪夹持
        public bool boxinnestflag;              //产品Box在Nest中
        public bool poweronflag;                //产品已加电
        public bool lenstouchedepoxyflag;       //当前Lens已接触并沾上胶水
        public bool upcameraliveonflag;         //上相机处于实时模式
        public bool downcameraliveonflag;       //下相机处于实时模式
        public bool pressdownlensflag;          //下压Lens过程中
        public bool picklensthreadaliveflag;    //夹取Lens线程正在运行中
        public bool epoxydipthreadaliveflag;    //蘸胶线程正在运行中
        public bool epoxywipethreadaliveflag;   //擦胶线程正在运行中
        public bool mainthreadaliveflag;        //主线程正在运行中
    }

    public class RampPara
    {
        //源表阶梯加电参数
        public double startval;         //起步值
        public double endval;           //终止值
        public double range;            //幅度
        public double step;             //步距
        public double interval;         //每步之间的延时时间
    }

    public class AxisPosition
    {
        //各轴当前位置
        public double X1;               //伺服X1轴坐标    
        public double Y1;               //伺服Y1轴坐标
        public double Z1;               //伺服Z1轴坐标
        public double X2;               //步进X2轴坐标
        public double Y2;               //步进Y2轴坐标
        public double Z2;               //步进Z2轴坐标
        public double U1;               //步进U1轴坐标
        public double PogoPin;          //PogoPin_SMC卡片电机轴坐标
    }

    public class DataBaseInfo
    {
        public string datasource;       //数据库链接信息
        public string userid;           //用户名
        public string password;         //访问密码
    }

    public class TrayStruct
    {
        //物料盘结构信息
        public int rowcount;            //总行数
        public int colcount;            //总列数
        public double rowspace;         //行间距
        public double colspace;         //列间距
    }

    public struct ObjectInTray
    {
        //料盘中物料状态信息
        public int index;                       //物料索引号
        public int row;                         //在物料盘中所在行号
        public int col;                         //在物料盘中所在列号
        public bool exists;                     //物料是否存在(勾选状态)
        public double posx;                     //夹爪夹取的X方向坐标
        public double posy;                     //夹爪夹取的Y方向坐标
        public bool bechecked;                  //是否经过上相机识别检查过
        public Enum_FinalResult finalresult;    //最终耦合贴装结果
    }

    public struct LaserIntensityInfo
    {
        //激光强度信息
        public int channelID;                   //通道号
        public bool outputEnable;               //通道是否使能
        public int currentDAC;                  //电流强度值
    }

    public class BoxWindowRecognize
    {
        //产品Box窗口识别结果
        public double epoxydipboxwindowposx1;   //点胶针在产品Box内部点胶X1轴位置
        public double epoxydipboxwindowposy1;   //点胶针在产品Box内部点胶Y1轴位置
        public double lensgripperintoboxposx2;  //夹爪进入产品Box内部X2轴位置
        public double lensgripperintoboxposy2;  //夹爪进入产品Box内部Y2轴位置
        public double windowcenterposx1;        //产品Box窗口中心换算到龙门轴坐标系统X1轴位置
        public double windowcenterposy1;        //产品Box窗口中心换算到龙门轴坐标系统Y1轴位置
        public double windowcenterposx2;        //产品Box窗口中心换算到步进轴坐标系统X2轴位置
        public double windowcenterposy2;        //产品Box窗口中心换算到步进轴坐标系统Y2轴位置
        public double diameter;                 //产品Box窗口直径
    }

    public struct LaserSpotRecognize
    {
        //光斑中心识别结果
        public double posx2;                    //光斑中心换算到步进轴坐标系统X2轴位置
        public double posy2;                    //光斑中心换算到步进轴坐标系统Y2轴位置
        public double centeroffsetx;            //光斑中心与产品Box中心X方向偏移量
        public double centeroffsety;            //光斑中心与产品Box中心Y方向偏移量
    }

    public class LensAttachPosition
    {
        //Lens贴装目标位置
        public double posx2;                    //Lens中心贴装目标位置换算到步进轴坐标系统X2轴位置
        public double posy2;                    //Lens中心贴装目标位置换算到步进轴坐标系统Y2轴位置
        public double posz2;                    //Lens贴装目标位置换算到步进轴坐标系统Z2轴位置
    }

    public class LensCenterRecognize
    {        
        //下相机识别Lens中心结果
        public double posx2;                    //Lens中心换算到步进轴坐标系统X2轴位置
        public double posy2;                    //Lens中心换算到步进轴坐标系统Y2轴位置
        public double centeroffsetx;            //Lens中心与产品Box中心X方向偏移量
        public double centeroffsety;            //Lens中心与产品Box中心Y方向偏移量
    }

    public class ThreadInfo
    {
        //线程信息
        public string threadflow;
        public bool threadenale;
    }

    #endregion

    #region 系统配置参数

    //硬件初始化状态标志
    public class HardwareInitialStatus
    {        
        public bool MotionCard_InitialStatus { get; set; }          //运动控制卡已初始化成功标志
        public bool IOCard_InitialStatus { get; set; }              //IO卡已初始化成功标志
        public bool LightSources_InitialStatus { get; set; }        //光源控制器已初始化成功标志
        public bool Camera_InitialStatus { get; set; }              //相机系统已初始化成功标志
        public bool ForceSensor_InitialStatus { get; set; }         //压力传感器已初始化成功标志
        public bool LensGripper_InitialStatus { get; set; }         //Lens夹爪控制器已初始化成功标志
        public bool BoxGripper_InitialStatus { get; set; }          //Box夹爪控制器已初始化成功标志
        public bool UVController_InitialStatus { get; set; }        //UV控制器已初始化成功标志
        public bool FI2CUSB_InitialStatus { get; set; }             //FI2CUSB通信设备已初始化成功标志
        public bool QRCodeScanner_InitialStatus { get; set; }       //二维码扫码器已初始化成功标志
        public bool SMCCardMotion_InitialStatus { get; set; }       //SMC卡片电机轴已初始化成功标志
        public bool Keithley2401_1_InitialStatus { get; set; }      //Keithley2401_#1加电源表已初始化成功标志
        public bool Keithley2401_2_InitialStatus { get; set; }      //Keithley2401_#2加电源表已初始化成功标志
        public bool Keithley2401_3_InitialStatus { get; set; }      //Keithley2401_#3加电源表已初始化成功标志
        public bool Keithley2401_4_InitialStatus { get; set; }      //Keithley2401_#4加电源表已初始化成功标志
        public bool KeySightE364x_InitialStatus { get; set; }       //KeySightE364x加电源表已初始化成功标志
        public bool GhoptoIRCamera_InitialStatus { get; set; }      //国惠宽波段红外相机已初始化成功标志
        public bool ImageProcessTools_InitialStatus { get; set; }   //Cognex视觉工具已初始化成功标志
        public bool MESFunctionTools_InitialStatus { get; set; }    //MES系统工具已初始化成功标志
    }

    //仪器设备配置项
    public class InstrumentConfig
    {        
        public bool Keithley2401_1_Valid { get; set; }      //Keithley2401_#1加电源表启用标志
        public bool Keithley2401_2_Valid { get; set; }      //Keithley2401_#2加电源表启用标志
        public bool Keithley2401_3_Valid { get; set; }      //Keithley2401_#3加电源表启用标志
        public bool Keithley2401_4_Valid { get; set; }      //Keithley2401_#4加电源表启用标志
        public bool Keithley2510_1_Valid { get; set; }      //Keithley2510_#1温控仪启用标志
        public bool Keithley2510_2_Valid { get; set; }      //Keithley2510_#2温控仪启用标志
        public bool KeySightE364x_Valid { get; set; }       //KeySightE364x加电源表启用标志
        public bool I2C_Valid { get; set; }                 //I2C通信设备启用标志
        public bool FI2CUSB_Valid { get; set; }             //FI2CUSB通信设备启用标志
        public bool DriverBoard_Valid { get; set; }         //s4500_DriverBoard驱动板启用标志
        public bool ForceSensor_Valid { get; set; }         //压力传感器启用标志
        public bool IAIBoxGripper_Valid { get; set; }       //Box夹爪启用标志
        public bool IAILensGripper_Valid { get; set; }      //Lens夹爪启用标志
        public bool UVController_Valid { get; set; }        //UV控制器启用标志
        public bool QRCodeScanner_Valid { get; set; }       //二维码扫码器启用标志
        public bool SMCCardMotion_Valid { get; set; }       //SMC卡片电机轴启用标志
        public bool GhoptoIRCamera_Vaild { get; set; }      //国惠宽波段红外相机启用标志
        public int GripperMotionDelay { get; set; }      //夹爪动作延时等待时间
        public int LightSourceDelay { get; set; }        //光源打开后延时等待时间
    }

    //轴安全位置信息
    public class AxisSafetyPositionInformation
    {        
        public string AxisName;             //轴名称
        public double SafetyPosition;       //轴的安全位置
    }

    //轴安全位置配置
    public class AxisSafetyPositionConfig
    {        
        public double AxisSpeedPercent { get; set; }
        public AxisSafetyPositionInformation Axis_0 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_1 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_2 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_3 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_4 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_5 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_6 { get; set; } = new AxisSafetyPositionInformation();
        public AxisSafetyPositionInformation Axis_7 { get; set; } = new AxisSafetyPositionInformation();
    }

    //系统操作选项配置
    public class SystemOperationConfig
    {        
        public bool doorsafemode { get; set; }                  //UV防护门是否启用
        public bool paralellprocess { get; set; }               //是否采用并行方式
        public bool recordlogfile { get; set; }                 //是否后台记录Log文档
        public bool grippersecuritydetection { get; set; }      //夹爪动作安全检测功能是否启用
    }

    //相机系统校准配置
    public class CameraCalibrateConfig
    {
        public AxisPosition CameraPos { get; set; } = new AxisPosition();
        public int upringlightval { get; set; }
        public int upspotlightval { get; set; }
        public int dnringlightval { get; set; }
        public int dnspotlightval { get; set; }
        public double xscale { get; set; }
        public double yscale { get; set; }
        public double gridstep { get; set; }
    }

    //红外相机采集卡参数配置
    public class IRCameraCaptureCardConfig
    {
        //识别产品Box窗口中心        
        public double ViewBoxWindowBrightness { get; set; }
        public double ViewBoxWindowContrast { get; set; }
        public double ViewBoxWindowDelay { get; set; }

        //识别产品通道光斑中心
        public double ViewLaserSpotBrightness { get; set; }
        public double ViewLaserSpotContrast { get; set; }
        public double ViewLaserSpotDelay { get; set; }

        //识别产品Box中的Lens中心
        public double ViewLensInsideBoxBrightness { get; set; }
        public double ViewLensInsideBoxContrast { get; set; }
        public double ViewLensInsideBoxDelay { get; set; }
    }

    //国惠宽波段(400-1700nm)红外相机(型号：GH-SWU2-VS15)配置
    public class GhoptoIRCameraConfig
    {
        //识别产品Box窗口中心
        public uint ViewBoxWindowIntegration { get; set; }
        public uint ViewBoxWindowGain { get; set; }
        public uint ViewBoxWindowBias { get; set; }
        
        //识别产品通道光斑中心
        public uint ViewLaserSpotIntegration { get; set; }
        public uint ViewLaserSpotGain { get; set; }
        public uint ViewLaserSpotBias { get; set; }

        //识别产品Box中的Lens中心
        public uint ViewLensInsideBoxIntegration { get; set; }
        public uint ViewLensInsideBoxGain { get; set; }
        public uint ViewLensInsideBoxBias { get; set; }
    }

    //产品Box夹爪配置
    public class BoxGripperConfig
    {
        public AxisPosition GripperBlockPos { get; set; } = new AxisPosition();
        public AxisPosition UpCameraViewBlockPos { get; set; } = new AxisPosition();
        public int upringlightval { get; set; }
        public int upspotlightval { get; set; }
        public double offsetx { get; set; }
        public double offsety { get; set; }
    }

    //Lens夹爪配置
    public class LensGripperConfig
    {
        public AxisPosition GripperLensPos { get; set; } = new AxisPosition();
        public AxisPosition UpCameraViewLensPos { get; set; } = new AxisPosition();
        public int upringlightval { get; set; }
        public int upspotlightval { get; set; }
        public double offsetx { get; set; }
        public double offsety { get; set; }
    }

    //蘸胶针配置
    public class DispenserConfig
    {
        public AxisPosition DispenserPinToEpoxyPos { get; set; } = new AxisPosition();
        public AxisPosition UpCameraViewEpoxyPos { get; set; } = new AxisPosition();
        public int upringlightval { get; set; }
        public int upspotlightval { get; set; }
        public double offsetx { get; set; }
        public double offsety { get; set; }
        public AxisPosition epoxyWipePos { get; set; } = new AxisPosition();
        public AxisPosition epoxyDipPos { get; set; } = new AxisPosition();
        public int epoxytestrowcount { get; set; }
        public double epoxytestrowspace { get; set; }
        public int epoxytestcolumncount { get; set; }
        public double epoxytestcolumnspace { get; set; }
        public double epoxytestdiptime { get; set; }
        public bool autoEpoxyDipHeightCompensation { get; set; }
        public int autoEpoxyDipCount { get; set; }
        public double autoEpoxyDipCompensationHeight { get; set; }
        public double autoEpoxyDipZ1HeightLimit { get; set; }
    }

    //产品配置管理
    public class ManageProductConfig
    {
        public List<String> productlist { get; set; }
        public string currentproduct { get; set; }
    }

    //系统配置信息集合
    [Serializable]
    [System.Xml.Serialization.XmlRoot("System Config")]

    public class SystemConfig
    {
        public InstrumentConfig InstrumentConfig { get; set; } = new InstrumentConfig();
        public AxisSafetyPositionConfig AxisSafetyPosConfig { get; set; } = new AxisSafetyPositionConfig();
        public SystemOperationConfig SystemOperationConfig { get; set; } = new SystemOperationConfig();
        public CameraCalibrateConfig UpCameraCalibrateConfig { get; set; } = new CameraCalibrateConfig();
        public CameraCalibrateConfig DownCameraCalibrateConfig { get; set; } = new CameraCalibrateConfig();
        public IRCameraCaptureCardConfig IRCameraCaptureCardConfig { get; set; } = new IRCameraCaptureCardConfig();
        public GhoptoIRCameraConfig GhoptoIRCameraConfig { get; set; } = new GhoptoIRCameraConfig();
        public BoxGripperConfig BoxGripperConfig { get; set; } = new BoxGripperConfig();
        public LensGripperConfig LensGripperConfig { get; set; } = new LensGripperConfig();
        public DispenserConfig DispenserConfig { get; set; } = new DispenserConfig();
        public ManageProductConfig ManageProductConfig { get; set; } = new ManageProductConfig();
    }

    #endregion

    #region 产品参数配置
    public class ProcessConfig
    {
        //Workflow Setting
        public int[] workflowArray = new int[50];
        public int workflowStepNum { get; set; }

        //Thread Setting
        public ThreadInfo thread1 { get; set; } = new ThreadInfo();
        public ThreadInfo thread2 { get; set; } = new ThreadInfo();
        public ThreadInfo thread3 { get; set; } = new ThreadInfo();

        //Power Config
        public Enum_PowerWay powerway { get; set; }
        public double voltageval { get; set; }
        public int i2cval { get; set; }
        public double iocurrentval { get; set; }
        public double currentval { get; set; }
        public double temperatureval { get; set; }

        //Source Meter Setting
        public double currentCompliance { get; set; }
        public bool rampPowerOnEnable { get; set; }
        public double rampDelay { get; set; }
        public int rampSteps { get; set; }

        //PogoPin Position Setting
        public Enum_PogoPinPosition pogopinPosition { get; set; }

        //PogoPin Lifetime Setting
        public int pogopinUsedCount { get; set; }
        public int pogopinLifetime { get; set; }

        //Epoxy Spot Setting
        public double minEpoxyDia { get; set; }
        public double maxEpoxyDia { get; set; }
        public int remindTime { get; set; }
        public bool manualPreDispensing { get; set; }

        //UV Setting
        public int uvPower { get; set; }
        public int uvTime { get; set; }
        public int uvCount { get; set; }

        //Laser Intensity Setting 
        public int channelCount { get; set; }
        public LaserIntensityInfo[] laserIntensity { get; set; } = new LaserIntensityInfo[8];
        public Enum_LaserSpotPosBalanceMode laserSpotPosBalanceMode { get; set; }

        //Alignment Way Setting
        public Enum_AlignmentWay alignmentWay { get; set; }

        //Press Lens Setting
        public int pressLensSpeedPercent { get; set; }
        public int pressLensTime { get; set; }
        public double pressLensTouchStep { get; set; }
        public int pressLensTouchForce { get; set; }
        public double pressLensStep { get; set; }
        public int pressLensForce { get; set; }
        public double pressLensProtectStep { get; set; }
        public int pressLensProtectForce { get; set; }
        public int pressLensAlarmForce { get; set; }
        public double pressLensEpoxyThickness { get; set; }
        public Enum_PressLensProtectMode pressLensProtectMode { get; set; }

        //Process Option Setting
        public bool checkLaserPositionBeforeLens2 { get; set; }
        public bool compensateLaserWindowOffset { get; set; }
        public double laserSpec1 { get; set; }
        public double laserSpec2 { get; set; }
        public bool checkLaserPositionAfterLens2 { get; set; }
        public bool recordAllLaserOffsetBeforeLens2 { get; set; }
        public bool recordAllLaserOffsetAfterLens2 { get; set; }
        public bool processDataUploadIntoDatabase { get; set; }

        //MES Option Setting
        public bool mesEnable { get; set; }
        public bool checkSN { get; set; }
        public bool moveoutEnable { get; set; }
        public bool autoHold { get; set; }
        public bool readSN { get; set; }
        public int dcCount { get; set; }
    }

    public class BoxLensConfig
    {
        //Pick Box Offset
        public double pickBoxOffsetX { get; set; }
        public double pickBoxOffsetY { get; set; }

        //Box Tray Config
        public TrayStruct boxTray { get; set; } = new TrayStruct();
        public double boxTrayMiddleSpace { get; set; }

        //Downward View Box Parameters
        public int UpCameraViewBoxUpSpot { get; set; }
        public int UpCameraViewBoxUpRing { get; set; }
        public bool UpCameraViewAllBox { get; set; }

        //Upward View Box Window Parameters
        public int DnCameraViewBoxWindowDnSpot { get; set; }
        public int DnCameraViewBoxWindowDnRing { get; set; }
        public double BoxWindowCentreOffsetLimit { get; set; }
        public bool CheckBoxWindowCentreOffset { get; set; }        

        //Upward View Laser Spot Parameters
        public int DnCameraViewLaserSpotMaxThreshold { get; set; }
        public int DnCameraViewLaserSpotMinThreshold { get; set; }

        //Lens Tray Config
        public TrayStruct lensTray { get; set; } = new TrayStruct();

        //Downward View Lens Parameters
        public int UpCameraViewLensUpSpot { get; set; }
        public int UpCameraViewLensUpRing { get; set; }
        public bool UpCameraViewAllLens { get; set; }

        //Upward View Lens Parameters
        public int DnCameraViewLensDnSpot { get; set; }
        public int DnCameraViewLensDnRing { get; set; }
        public bool DnCameraViewLensPatMatch { get; set; }

        //Upward View Epoxy Parameters
        public int DnCameraViewEpoxyDnSpot { get; set; }
        public int DnCameraViewEpoxyDnRing { get; set; }
    }

    public class PositionConfig
    {
        #region Box相关位置参数项

        //View First Box In Tray Position
        public AxisPosition UpCameraViewBoxInTrayPosition { get; set; } = new AxisPosition();

        //Box Pick From & Place To Tray Z Height
        public double pickBoxZ1Height { get; set; }

        //Box To Safe Position
        public AxisPosition boxSafePosition { get; set; } = new AxisPosition();

        //Box Place To Nest Position
        public AxisPosition boxInNestPosition { get; set; } = new AxisPosition();
        public int boxIntoNestSpeedPercent { get; set; }

        //Box Pick From Nest Position
        public AxisPosition pickBoxFromNestPosition { get; set; } = new AxisPosition();

        //Box Place To Scanner Position
        public AxisPosition boxScanPosition { get; set; } = new AxisPosition();

        #endregion

        #region Lens相关位置参数项

        //View First Lens In Tray Position
        public AxisPosition UpCameraViewLensInTrayPosition { get; set; } = new AxisPosition();

        //Lens Pick From & Place To Tray Z Height
        public double pickLensZ2Height { get; set; }

        //Lens To Safe Position
        public AxisPosition lensSafePosition { get; set; } = new AxisPosition();

        //First Step Lens Into Box Position Offset
        public double step1LensPlaceOffsetX { get; set; }
        public double step1LensPlaceOffsetY { get; set; }

        //Second Step Lens Into Box Position Offset
        public double step2LensPlaceOffsetX { get; set; }
        public double step2LensPlaceOffsetY { get; set; }
        public double step2LensPlaceOffsetZ { get; set; }

        //Final Step Lens Into Box Position Offset
        public double finalLensPlaceOffsetX { get; set; }
        public double finalLensPlaceOffsetY { get; set; }

        //Lens Place Into Box Protection
        public double pressLensDistanceLimit { get; set; }
        public int moveLensIntoBoxSpeedPercent { get; set; }

        //Lens Discard Position
        public AxisPosition cleanLensDicardPosition { get; set; } = new AxisPosition();
        public AxisPosition dirtyLensDicardPosition { get; set; } = new AxisPosition();

        #endregion

        #region Dispenser相关位置参数项

        //Dispenser To Box Z Height
        public double dispenserBoxZ1Height { get; set; }

        //Dispenser To Box Offset
        public double dispenserToBoxOffsetX { get; set; }
        public double dispenserToBoxOffsetY { get; set; }

        //Dispenser To Safe Position
        public AxisPosition dispenserSafePosition { get; set; } = new AxisPosition();

        //Dispenser Box Protection
        public int dispenserToBoxSpeedPercent { get; set; }
        public double dispenserBoxZ1Up { get; set; }

        #endregion
    }

    public class DataLogConfig
    {
        //数据库上传配置
        public bool databaseenable { get; set; }
        public DataBaseInfo database { get; set; } = new DataBaseInfo();
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("Product Config")]
    public class ProductConfig
    {
        public ProcessConfig processConfig { get; set; } = new ProcessConfig();
        public BoxLensConfig boxlensConfig { get; set; } = new BoxLensConfig();
        public PositionConfig positionConfig { get; set; } = new PositionConfig();
        public DataLogConfig datalogConfig { get; set; } = new DataLogConfig();
    }

    #endregion

    #region MES配置

    public class SingleStep
    {
        public string stepname { get; set; }
        public bool CheckMoveIn { get; set; }
    }

    public class MultiStep
    {
        public string productname { get; set; }
        public string stepname { get; set; }
        public bool CheckMoveIn { get; set; }
    }

    public class EnvrInfo
    {
        public bool productEnvrFlag { get; set; }
        public bool testEnvrFlag { get; set; }
        public bool defineEnvrFlag { get; set; }
        public string serverName { get; set; }
        public string databaseName { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
    }

    public class DownloadInfo
    {
        public bool autoDownload { get; set; }
        public string serverPath { get; set; }
    }

    public class MESConfig
    {
        public bool singleStepFlag { get; set; }

        SingleStep singleStep = new SingleStep();
        public SingleStep singlestep
        {
            get
            {
                return singleStep;
            }
            set
            {
                singleStep = value;
            }
        }

        public bool multiStepFlag { get; set; }

        List<MultiStep> multiStep = new List<MultiStep>();
        public List<MultiStep> multistep
        {
            get
            {
                return multiStep;
            }
            set
            {
                multiStep = value;
            }
        }

        EnvrInfo envrInfo = new EnvrInfo();
        public EnvrInfo envrinfo
        {
            get
            {
                return envrInfo;
            }
            set
            {
                envrInfo = value;
            }
        }

        DownloadInfo downLoadInfo = new DownloadInfo();
        public DownloadInfo downloadinfo
        {
            get
            {
                return downLoadInfo;
            }
            set
            {
                downLoadInfo = value;
            }
        }

        public bool InitialMES { get; set; }
    }

    public class DCInfo
    {
        public string dcNum { get; set; }
        public string dcPN { get; set; }
        public string validTime { get; set; }
    }

    public class DCList
    {
        List<DCInfo> dcInfo = new List<DCInfo>();
        public List<DCInfo> dcinfolist
        {
            get
            {
                return dcInfo;
            }
            set
            {
                dcInfo = value;
            }
        }
    }

    #endregion

    #region 工艺过程参数集合
    public class ProcessData
    {
        public string startTime;                                                                //产品组装开始时间
        public string endTime;                                                                  //产品组装结束时间
        public ObjectInTray[,] boxload = new ObjectInTray[4, 3];                                //产品Box发料盘中Box状态信息
        public ObjectInTray[,] boxunload = new ObjectInTray[4, 3];                              //产品Box收料盘中Box已收料状态信息
        public ObjectInTray[] lensload = new ObjectInTray[12];                                  //产品Lens物料盘中Lens状态信息
        public string currentStepFlowName;                                                      //当前单步骤的名称
        public string currentProductSN;                                                         //当前产品SN信息
        public int currentChannel;                                                              //当前通道号
        public int currentBoxIndex;                                                             //当前Box索引号
        public int currentLensIndex;                                                            //当前Lens索引号
        public bool pickLensThreadResult;                                                       //自动夹取Lens线程运行结果
        public bool epoxyDipThreadResult;                                                       //自动蘸胶线程运行结果
        public bool epoxyWipeThreadResult;                                                      //自动擦胶线程运行结果
        public BoxWindowRecognize boxWindowRecognize = new BoxWindowRecognize();                //产品Box窗口识别结果
        public LaserSpotRecognize[] laserSpotRecognize = new LaserSpotRecognize[8];             //产品各通道光斑识别结果
        public LaserSpotRecognize[] beforelenslaserSpotRecognize = new LaserSpotRecognize[8];   //产品Lens贴装前UV前产品各通道光斑识别结果
        public LaserSpotRecognize[] afterlenslaserSpotRecognize = new LaserSpotRecognize[8];    //产品Lens贴装后UV后产品各通道光斑识别结果
        public string laserSpotPosBalanceMode;                                                  //产品多通道光斑中心位置平衡方式
        public double lensCenterAttachOffsetx;                                                  //产品Lens中心贴装目标位置与产品Box窗口中心在X方向的偏移
        public double lensCenterAttachOffsety;                                                  //产品Lens中心贴装目标位置与产品Box窗口中心在Y方向的偏移
        public double spotCenterMaxOffsetx;                                                     //产品各通道光斑中心与产品Box窗口中心在X方向的最大偏移值减去最小偏移值
        public double spotCenterMaxOffsety;                                                     //产品各通道光斑中心与产品Box窗口中心在Y方向的最大偏移值减去最小偏移值
        public LensCenterRecognize lensCenterRecognize = new LensCenterRecognize();             //下相机识别Lens中心结果
        public LensCenterRecognize beforeUVlensCenterRecognize = new LensCenterRecognize();     //产品在UV前下相机识别Lens中心结果
        public LensCenterRecognize afterUVlensCenterRecognize = new LensCenterRecognize();      //产品在UV后下相机识别Lens中心结果
        public LensAttachPosition lensCenterAttachPosition = new LensAttachPosition();          //产品Lens中心需要贴装的目标位置
        public LensAttachPosition lensGripperAttachPosition = new LensAttachPosition();         //Lens夹爪夹持Lens最终停留在产品Box内部贴装的位置
        public bool checkSpotBeforeAlignResult;                                                 //产品耦合贴装前光斑检查结果
        public string finalResult;                                                              //产品最终耦合组装结果
        public string failReason;                                                               //产品耦合贴装失败原因 
        public string mesHoldReason;                                                            //产品被MES_Hold的原因
        public double realtimeEpoxyDipHeight;                                                   //蘸胶Z1轴实时高度值 
        public int realtimeEpoxyDipCount;                                                       //蘸胶实时累计次数
        public double realtimeLensPressureForce;                                                //Lens下压实时压力传感器反馈值
    }
    #endregion

    #region 全局参数集合
    class GlobalParameters
    {
        //全局标志
        public static FlagAssembly flagassembly = new FlagAssembly();

        //硬件初始化状态
        public static HardwareInitialStatus HardwareInitialStatus = new HardwareInitialStatus();

        //系统参数配置
        public static SystemConfig systemconfig = new SystemConfig();

        //产品参数配置
        public static ProductConfig productconfig = new ProductConfig();

        //全局图像
        public static Bitmap globitmap = null;

        //机台操作信息
        public static SystemOperationInfo systemOperationInfo = new SystemOperationInfo();

        //产品工艺参数
        public static ProcessData processdata = new ProcessData();

        //MES配置参数
        public static MESConfig MESConfig = new MESConfig();   

        //线程参数
        public static Thread mainThread = null;         //主线程
        public static Thread picklensThread = null;     //从料盘夹取Lens线程
        public static Thread epoxydipThread = null;     //蘸胶线程
        public static Thread epoxywipeThread = null;    //擦胶线程 

        //国惠宽波段红外相机设备操作接口句柄
        public static int GhoptoIRCameraHandle;

        //国惠宽波段红外相机相机图像预处理视觉工具接口
        public static CogToolBlock cogtoolblock_GhoptoIRCamera = new CogToolBlock();
    }
    #endregion

    #region 全局功能集合
    class GlobalFunction
    {
        //主界面信息显示窗口记录刷新委托事件接口
        public delegate void UpdateStatusDelegate(string str, Enum_MachineStatus status = Enum_MachineStatus.NORMAL, Enum_UPDATEMODE updatemode = Enum_UPDATEMODE.UPDATE_INFO);
        public static UpdateStatusDelegate updateStatusDelegate;

        //主界面产品Box发料盘状态刷新委托事件接口
        public delegate void UpdateBoxLoadTrayStatusDelegate(int row, int col, bool exist);
        public static UpdateBoxLoadTrayStatusDelegate updateBoxLoadTrayStatusDelegate;

        //主界面产品Box收料盘状态刷新委托事件接口
        public delegate void UpdateBoxUnloadTrayStatusDelegate(int row, int col, bool exist, Enum_FinalResult finalresult);
        public static UpdateBoxUnloadTrayStatusDelegate updateBoxUnloadTrayStatusDelegate;

        //主界面Lens料盘状态刷新委托事件接口
        public delegate void UpdateLensLoadTrayStatusDelegate(int row, bool exist);
        public static UpdateLensLoadTrayStatusDelegate updateLensLoadTrayStatusDelegate;

        //主界面加电源表窗口电压读数刷新委托事件接口
        public delegate void UpdateSourceMeterVoltageReadingDelegate();
        public static UpdateSourceMeterVoltageReadingDelegate updateSourceMeterVoltageReadingDelegate;

        //主界面加电源表窗口电流读数刷新委托事件接口
        public delegate void UpdateSourceMeterCurrentReadingDelegate();
        public static UpdateSourceMeterCurrentReadingDelegate updateSourceMeterCurrentReadingDelegate;

        //主界面产品SN信息刷新委托事件接口
        public delegate void UpdateProdutSNDelegate(string productSN);
        public static UpdateProdutSNDelegate updateProdutSNDelegate;

        //主界面DC物料实时状态获取委托事件接口
        public delegate Color GetDCTreeStatusDelegate(int DCIndex);
        public static GetDCTreeStatusDelegate getDCTreeStatusDelegate;

        //主界面压力传感器读值刷新委托事件接口
        public delegate void UpdatePressureForceReadingDelegate(double pressureforce);
        public static UpdatePressureForceReadingDelegate updatePressureForceReadingDelegate;

        //主界面机台状态指示Lamp刷新委托事件接口
        public delegate void UpdateRunStatusLampDelegate(Enum_RunStatus runstatus);
        public static UpdateRunStatusLampDelegate updateRunStatusLampDelegate;

        //Cognex图像控件对象
        public static Cognex.VisionPro.CogRecordDisplay CogRecordDisplay = new Cognex.VisionPro.CogRecordDisplay();

        //运动控制对象
        public static cMotionTools MotionTools = new cMotionTools();

        //IO控制对象
        public static cIOControlTools IOControlTools = new cIOControlTools();

        //相机控制对象
        public static cCameraTools CameraTools = new cCameraTools();

        //国惠宽波段红外相机控制对象
        public static cGhoptoIRCameraTools GhoptoIRCameraTools = new cGhoptoIRCameraTools();

        //视觉检测对象
        public static cImageProcessTool ImageProcessTools = new cImageProcessTool();

        //光源控制器对象
        public static cLightSourcesTools LightSourcesTools = new cLightSourcesTools();

        //加电源表控制对象
        public static cSourceMeterTools SourceMetertools = new cSourceMeterTools();

        //UV控制器对象
        public static cUVControllerTools UVControllerTools = new cUVControllerTools();

        //IAI夹爪控制对象
        //public static cIAIGripperTools IAIGripperTools = new cIAIGripperTools();
        public static cElectricGripperTools IAIGripperTools = new cElectricGripperTools();

        //压力传感器控制对象
        public static cForceSensorTools ForceSensorTools = new cForceSensorTools();

        //SMC卡片电机轴控制对象
        public static cSMCCardMotionTools SMCCardMotionTools = new cSMCCardMotionTools();

        //FI2CUSB通信模块控制对象
        public static cFI2CUSBTools FI2CUSBTools = new cFI2CUSBTools();

        //二维码扫码器控制对象
        public static cQRCodeScannerTools QRCodeScannerTools = new cQRCodeScannerTools();

        //MES对象
        public static cMESTools MESTools = new cMESTools();        

        //工艺流程功能对象
        public static ProcessFlow ProcessFlow = new ProcessFlow();
    }
    #endregion
}