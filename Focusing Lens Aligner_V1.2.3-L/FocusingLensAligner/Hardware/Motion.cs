using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using STD_IMotion;

namespace FocusingLensAligner
{
    public class AxisConfig
    {
        public string AxisName { get; set; }
        public string AxisType { get; set; }
        public int AxisID { get; set; }
        public STD_IMotion.HOME_MODE HomeMode { set; get; }
        public STD_IMotion.HOME_DIR HomeDir { set; get; }
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("Axes Config")]
    public class AxisConfigCollection
    {
        List<AxisConfig> axisconfigs = new List<AxisConfig>();
        public List<AxisConfig> AxisConfigs
        {
            get
            {
                return axisconfigs;
            }
            set
            {
                axisconfigs = value;
            }
        }
    }

    //运动轴控制底层功能

    class cMotionTools
    {
        #region 轴运动底层驱动

        public Dictionary<string, IMotion> MotionTool = new Dictionary<string, IMotion>();
        private AxisConfigCollection axisconfigcollection = new AxisConfigCollection();

        private String ControllerName = string.Empty;
        public bool ControllerInitialized = false;

        private AxisConfig GetAxisConfig(string AxisName)
        {
            AxisConfig tmp = axisconfigcollection.AxisConfigs.Find(x => x.AxisName == AxisName);
            return tmp;
        }

        public bool Motion_Initial()
        {
            bool res = false;

            res = STD_IGeneralTool.GeneralTool.ToTryLoad<AxisConfigCollection>(ref axisconfigcollection, GeneralFunction.GetConfigFilePath("AxisConfig.xml"));
            if (res == false)
            {
                MessageBox.Show("Load Motion Config Fail!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return res;
            }

            res = CMotion.Initial(ref MotionTool);
            if (res == true && MotionTool.Count > 0)
            {
                //获取接口中控制器名称
                KeyValuePair<string, IMotion> KVP = MotionTool.First();
                ControllerName = KVP.Key;

                res = MotionTool[ControllerName].Motion_Initial();
                if (res == false)
                {
                    ControllerInitialized = false;
                    return false;
                }
                else
                {
                    ControllerInitialized = true;
                }

                res = MotionTool[ControllerName].Motion_AllServoOn(true);
                if (res == false)
                {
                    MessageBox.Show("Axes Servo on Fail!", "Axis Servo on error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MotionTool.Clear();
                }
            }
            else
            {
                MotionTool.Clear();
            }

            return res;
        }

        public bool Motion_UnInitial()
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }

            if (ControllerInitialized == false)
            {
                return true;
            }

            res = MotionTool[ControllerName].Motion_AllServoOn(false);
            if (res == true)
            {
                res = MotionTool[ControllerName].Motion_Close();
            }
            else
            {
                MessageBox.Show("Axes Servo off Fail!", "Axis Servo off error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MotionTool.Clear();
            }

            if (res == true)
            {
                ControllerInitialized = false;
            }

            return res;
        }

        public bool Motion_OpenMotionSetupPanel()
        {
            bool res = false;

            res = CMotion.OpenSetupPanel();
            return res;
        }

        public void Motion_OpenAxisConfigPanel()
        {
            AxisConfigForm axisConfigFrm = new AxisConfigForm();
            axisConfigFrm.TopLevel = true;
            axisConfigFrm.StartPosition = FormStartPosition.CenterScreen;
            axisConfigFrm.ShowDialog();
        }

        public bool Motion_ResetController()
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_ResetController();
            return res;
        }

        public bool Motion_ClearError()
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_ClearError();
            return res;
        }

        public bool Motion_GetFeedbackPosition(string AxisName, ref double Position)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_GetFeedbackPosition(tmp.AxisID, ref Position);
            return res;
        }

        public bool Motion_GetPosition(string AxisName, ref double Position)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_GetPosition(tmp.AxisID, ref Position);
            return res;
        }

        public bool Motion_SetSpeed(string AxisName, double Speed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (Speed <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetSpeed(tmp.AxisID, Speed);
            return res;
        }

        public bool Motion_GetSpeed(string AxisName, ref double Speed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_GetSpeed(tmp.AxisID, ref Speed);
            return res;
        }

        public bool Motion_SetSpeedPercent(string AxisName, double SpeedPercent)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (SpeedPercent <= 0 || SpeedPercent > 100)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetSpeedPercent(tmp.AxisID, SpeedPercent);
            if (res == false)
            {
                return false;
            }
            return res;
        }

        public bool Motion_SetJogSpeed(string AxisName, double JogSpeed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (JogSpeed <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetJogSpeed(tmp.AxisID, JogSpeed);
            return res;
        }

        public bool Motion_GetJogSpeed(string AxisName, ref double JogSpeed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_GetJogSpeed(tmp.AxisID, ref JogSpeed);
            return res;
        }

        public bool Motion_SetAccSpeed(string AxisName, double AccSpeed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (AccSpeed <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetAcc(tmp.AxisID, AccSpeed);
            return res;
        }

        public bool Motion_SetAccSpeedPercent(string AxisName, double AccSpeedPercent)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (AccSpeedPercent <= 0 || AccSpeedPercent > 100)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetAccPercent(tmp.AxisID, AccSpeedPercent);
            return res;
        }

        public bool Motion_SetDecSpeed(string AxisName, double DecSpeed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (DecSpeed <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetDec(tmp.AxisID, DecSpeed);
            return res;
        }

        public bool Motion_SetDecSpeedPercent(string AxisName, double DecSpeedPercent)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (DecSpeedPercent <= 0 || DecSpeedPercent > 100)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetDecPercent(tmp.AxisID, DecSpeedPercent);
            return res;
        }

        public bool Motion_SetStopDecSpeed(string AxisName, double StopDecSpeed)
        {

            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            if (StopDecSpeed <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_SetStopDec(tmp.AxisID, StopDecSpeed);

            return res;
        }

        public bool Motion_EnableSoftwareLimit(string AxisName, bool Enable)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }
            res = MotionTool[ControllerName].Motion_EnableSWLimit(tmp.AxisID, Enable);
            return res;
        }

        public bool Motion_SetSoftwareLimit(string AxisName, double LeftLimitPosition, double RightLimitPosition)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            if (RightLimitPosition > LeftLimitPosition)
            {
                double[] LimitPosition = new double[2];

                LimitPosition[0] = LeftLimitPosition;
                LimitPosition[1] = RightLimitPosition;
                res = MotionTool[ControllerName].Motion_SetSWLimit(tmp.AxisID, LimitPosition);
            }
            return res;
        }

        public bool Motion_ServoOn(string AxisName, bool ServoOn)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_ServoOn(tmp.AxisID, ServoOn);
            return res;
        }

        public bool Motion_IsServoOn(string AxisName, ref bool ServoOnStatus)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_IsServoOn(tmp.AxisID, ref ServoOnStatus);
            return res;
        }

        public bool Motion_AllServoOn(bool ServoOn)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_AllServoOn(ServoOn);
            return res;
        }

        public bool Motion_HomeMove(string AxisName, bool Wait)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, Wait);
            return res;
        }

        public bool Motion_IsHomed(string AxisName, ref bool Homed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_IsHomed(tmp.AxisID, ref Homed);
            return res;
        }

        public bool Motion_IsMoveCompleted(string AxisName, ref bool Completed)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_IsMoveCompleted(tmp.AxisID, ref Completed);
            return res;
        }

        public bool Motion_WaitMoveCompleted(string AxisName, double Timeout)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_WaitMoveCompleted(tmp.AxisID, Timeout);
            return res;
        }

        public bool Motion_Stop(string AxisName)
        {
            bool res = false;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            res = MotionTool[ControllerName].Motion_Stop(tmp.AxisID);
            return res;
        }

        public bool Motion_MoveDistance(string AxisName, double Distance, bool Wait)
        {
            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            return MotionTool[ControllerName].Motion_MoveDistance(tmp.AxisID, Distance, Wait);
        }

        public bool Motion_MoveDistance(string AxisName, double Distance, double Speed, bool Wait)
        {
            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            return MotionTool[ControllerName].Motion_MoveDistance(tmp.AxisID, Distance, Speed, Wait);
        }

        public bool Motion_MoveDistance(string[] AxisNames, double[] Distances, bool Wait)
        {
            string AxisType = String.Empty;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            List<int> Axis = new List<int>();
            Axis.Clear();
            for (int i = 0; i < AxisNames.Length; i++)
            {
                AxisConfig tmp = GetAxisConfig(AxisNames[i]);
                if (tmp == null)
                {
                    return false;
                }
                Axis.Add(tmp.AxisID);
                AxisType = tmp.AxisType;
            }

            bool res = MotionTool[ControllerName].Motion_MoveDistance(Axis.ToArray(), Distances, Wait);
            return res;
        }

        public bool Motion_MoveDistance(string[] AxisNames, double[] Distances, double Speed, bool Wait)
        {
            string AxisType = String.Empty;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            List<int> Axis = new List<int>();
            Axis.Clear();
            for (int i = 0; i < AxisNames.Length; i++)
            {
                AxisConfig tmp = GetAxisConfig(AxisNames[i]);
                if (tmp == null)
                {
                    return false;
                }
                Axis.Add(tmp.AxisID);
                AxisType = tmp.AxisType;
            }

            bool res = MotionTool[ControllerName].Motion_MoveDistance(Axis.ToArray(), Distances, Speed, Wait);
            return res;
        }

        public bool Motion_MoveToLocation(string AxisName, double Location, bool Wait)
        {
            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            bool res = MotionTool[ControllerName].Motion_MovetoLocation(tmp.AxisID, Location, Wait);
            return res;
        }

        public bool Motion_MoveToLocation(string AxisName, double Location, double Speed, bool Wait)
        {
            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            bool res = MotionTool[ControllerName].Motion_MovetoLocation(tmp.AxisID, Location, Speed, Wait);
            return res;
        }

        public bool Motion_MoveToLocation(string[] AxisNames, double[] Locations, bool Wait)
        {
            string AxisType = String.Empty;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            List<int> Axis = new List<int>();
            Axis.Clear();
            for (int i = 0; i < AxisNames.Length; i++)
            {
                AxisConfig tmp = GetAxisConfig(AxisNames[i]);
                if (tmp == null)
                {
                    return false;
                }
                Axis.Add(tmp.AxisID);
                AxisType = tmp.AxisType;
            }

            bool res = MotionTool[ControllerName].Motion_MovetoLocation(Axis.ToArray(), Locations, Wait);
            return res;
        }

        public bool Motion_MoveToLocation(string[] AxisNames, double[] Locations, double Speed, bool Wait)
        {
            string AxisType = String.Empty;

            if (MotionTool.Count <= 0)
            {
                return false;
            }
            List<int> Axis = new List<int>();
            Axis.Clear();
            for (int i = 0; i < AxisNames.Length; i++)
            {
                AxisConfig tmp = GetAxisConfig(AxisNames[i]);
                if (tmp == null)
                {
                    return false;
                }
                Axis.Add(tmp.AxisID);
                AxisType = tmp.AxisType;
            }

            bool res = MotionTool[ControllerName].Motion_MovetoLocation(Axis.ToArray(), Locations, Speed, Wait);
            return res;
        }

        public bool Motion_GetAxisID(string AxisName, ref int AxisID)
        {
            if (MotionTool.Count <= 0)
            {
                return false;
            }
            AxisConfig tmp = GetAxisConfig(AxisName);
            if (tmp == null)
            {
                return false;
            }

            AxisID = tmp.AxisID;

            return true;
        }

        #endregion

        #region 设置轴运动速度百分比

        public bool Motion_SetX1SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "X1";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetY1SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "Y1";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetZ1SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "Z1";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetX2SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "X2";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetY2SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "Y2";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetZ2SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "Z2";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetU1SpeedPercent(double percent)
        {
            bool res = false;
            string axisname = "U1";
            if (percent < 0 || percent > 100)
            {
                return false;
            }
            res = Motion_SetSpeedPercent(axisname, percent);
            return res;
        }

        public bool Motion_SetAllAxisSpeedPercent(double percent)
        {
            //设置所有轴的运动速度百分比
            bool res = false;

            if (percent < 0 || percent > 100)
            {
                return false;
            }

            res = Motion_SetX1SpeedPercent(percent);
            if (res == true)
            {
                res = Motion_SetY1SpeedPercent(percent);
                if (res == true)
                {
                    res = Motion_SetZ1SpeedPercent(percent);
                    if (res == true)
                    {
                        res = Motion_SetX2SpeedPercent(percent);
                        if (res == true)
                        {
                            res = Motion_SetY2SpeedPercent(percent);
                            if (res == true)
                            {
                                res = Motion_SetZ2SpeedPercent(percent);
                                if (res == true)
                                {
                                    res = Motion_SetU1SpeedPercent(percent);
                                }
                            }
                        }
                    }
                }
            }

            return res;
        }

        #endregion

        #region 设置轴运动速度

        public bool Motion_SetX1Speed(double speed)
        {
            bool res = Motion_SetSpeed("X1", speed);
            return res;
        }

        public bool Motion_SetY1Speed(double speed)
        {
            bool res = Motion_SetSpeed("Y1", speed);
            return res;
        }

        public bool Motion_SetZ1Speed(double speed)
        {
            bool res = Motion_SetSpeed("Z1", speed);
            return res;
        }

        public bool Motion_SetU1Speed(double speed)
        {
            bool res = Motion_SetSpeed("U1", speed);
            return res;
        }

        public bool Motion_SetX2Speed(double speed)
        {
            bool res = Motion_SetSpeed("X2", speed);
            return res;
        }

        public bool Motion_SetY2Speed(double speed)
        {
            bool res = Motion_SetSpeed("Y2", speed);
            return res;
        }

        public bool Motion_SetZ2Speed(double speed)
        {
            bool res = Motion_SetSpeed("Z2", speed);
            return res;
        }

        #endregion

        #region 获取轴运动速度

        public double Motion_GetX1Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("X1", ref speed);
            return speed;
        }

        public double Motion_GetY1Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("Y1", ref speed);
            return speed;
        }

        public double Motion_GetZ1Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("Z1", ref speed);
            return speed;
        }

        public double Motion_GetU1Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("U1", ref speed);
            return speed;
        }

        public double Motion_GetX2Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("X2", ref speed);
            return speed;
        }

        public double Motion_GetY2Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("Y2", ref speed);
            return speed;
        }

        public double Motion_GetZ2Speed()
        {
            double speed = 0;
            bool res = Motion_GetSpeed("Z2", ref speed);
            return speed;
        }

        #endregion

        #region 获取轴当前位置

        public double Motion_GetX1Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("X1", ref pos);
            return pos;
        }

        public double Motion_GetY1Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("Y1", ref pos);
            return pos;
        }

        public double Motion_GetZ1Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("Z1", ref pos);
            return pos;
        }

        public double Motion_GetU1Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("U1", ref pos);
            return pos;
        }

        public double Motion_GetX2Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("X2", ref pos);
            return pos;
        }

        public double Motion_GetY2Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("Y2", ref pos);
            return pos;
        }

        public double Motion_GetZ2Pos()
        {
            double pos = 0;
            bool res = Motion_GetFeedbackPosition("Z2", ref pos);
            return pos;
        }

        #endregion

        #region 轴运动到安全位置

        public bool Motion_X1MoveToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_0.SafetyPosition;
            bool res = Motion_MoveToLocation("X1", pos, true);
            return res;
        }

        public bool Motion_Y1MoveToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_1.SafetyPosition;
            bool res = Motion_MoveToLocation("Y1", pos, true);
            return res;
        }

        public bool Motion_Z1UpToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition;
            bool res = Motion_MoveToLocation("Z1", pos, true);
            return res;
        }

        public bool Motion_X2MoveToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_3.SafetyPosition;
            bool res = Motion_MoveToLocation("X2", pos, true);
            return res;
        }

        public bool Motion_Y2MoveToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_4.SafetyPosition;
            bool res = Motion_MoveToLocation("Y2", pos, true);
            return res;
        }

        public bool Motion_Z2UpToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition;
            bool res = Motion_MoveToLocation("Z2", pos, true);
            return res;
        }

        public bool Motion_U1MoveToSafe()
        {
            double pos = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_6.SafetyPosition;
            bool res = Motion_MoveToLocation("U1", pos, true);
            return res;
        }

        public bool Motion_PogoPinMoveToSafe()
        {
            return true;
        }

        public bool Motion_AllMoveToSafe()
        {
            //所有轴运动到安全位置
            bool res = false;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.MotionCard_InitialStatus == true)
            {
                res = Motion_SetAllAxisSpeedPercent(GlobalParameters.systemconfig.AxisSafetyPosConfig.AxisSpeedPercent);
                if (res == true)
                {
                    //伺服Z1轴安全上抬
                    res = Motion_Z1UpToSafe();
                    if (res == true)
                    {
                        //步进U1轴旋转至安全角度
                        res = Motion_U1MoveToSafe();
                        if (res == true)
                        {
                            //伺服X1_Y1轴运动至安全位置
                            double posx = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_0.SafetyPosition;
                            double posy = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_1.SafetyPosition;
                            res = Motion_MoveX1Y1ToLocation(posx, posy, false, true);
                            if (res == true)
                            {
                                GlobalParameters.flagassembly.epoxypininboxflag = false;

                                //步进X2轴运动至安全位置
                                res = Motion_X2MoveToSafe();
                                if (res == true)
                                {
                                    GlobalParameters.flagassembly.lensgripperinboxflag = false;
                                    
                                    //步进Y2轴运动至安全位置
                                    Motion_Y2MoveToSafe();
                                    if (res == true)
                                    {
                                        //步进Z2轴上抬至安全位置
                                        res = Motion_Z2UpToSafe();
                                        if (res == true)
                                        {
                                            //PogoPin轴运动至安全位置
                                            if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true && GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus == true)
                                            {
                                                res = Motion_PogoPinMoveToSafe();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Motion_SetAllAxisSpeedPercent(100);
                }
            }
            else
            {
                str = "运动轴卡未初始化";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                res = false;
            }

            if (res == true)
            {
                str = "运动轴系统回安全位成功";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
            }
            else
            {
                str = "运动轴系统回安全位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
            }

            return res;
        }

        #endregion

        #region 轴相对位移指定距离

        public bool Motion_MoveX1Distance(double dis, bool z1up, bool z2up, bool wait)
        {
            bool res = false;
            string axisname = "X1";

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            res = Motion_MoveDistance(axisname, dis, wait);
            return res;
        }

        public bool Motion_MoveY1Distance(double dis, bool z1up, bool z2up, bool wait)
        {
            bool res = false;
            string axisname = "Y1";

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            res = Motion_MoveDistance(axisname, dis, wait);
            return res;
        }

        public bool Motion_MoveZ1Distance(double dis, bool wait)
        {
            bool res = false;
            string axisname = "Z1";

            res = Motion_MoveDistance(axisname, dis, wait);
            return res;
        }

        public bool Motion_MoveX2Distance(double dis, bool z1up, bool z2up, bool wait)
        {
            bool res = false;
            string axisname = "X2";

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            res = Motion_MoveDistance(axisname, dis, wait);
            return res;
        }

        public bool Motion_MoveY2Distance(double dis, bool z1up, bool z2up, bool wait)
        {
            bool res = false;
            string axisname = "Y2";

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            res = Motion_MoveDistance(axisname, dis, wait);
            return res;
        }

        public bool Motion_MoveZ2Distance(double dis, bool wait)
        {
            bool res = false;
            string axisname = "Z2";

            res = Motion_MoveDistance(axisname, dis, wait);
            return res;
        }

        #endregion

        #region 轴运动到绝对位置

        public bool Motion_MoveX1ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("X1", pos, wait);
            return res;
        }

        public bool Motion_MoveY1ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("Y1", pos, wait);
            return res;
        }

        public bool Motion_MoveZ1ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("Z1", pos, wait);
            return res;
        }

        public bool Motion_MoveU1ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("U1", pos, wait);
            return res;
        }

        public bool Motion_MoveX2ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("X2", pos, wait);
            return res;
        }

        public bool Motion_MoveY2ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("Y2", pos, wait);
            return res;
        }

        public bool Motion_MoveZ2ToLocation(double pos, bool wait)
        {
            bool res = Motion_MoveToLocation("Z2", pos, wait);
            return res;
        }

        public bool Motion_MoveX1Y1ToLocation(double x1pos, double y1pos, bool z1up, bool wait)
        {
            bool res = false;

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            string[] axisname = { "X1", "Y1" };
            double[] location = { x1pos, y1pos };
            res = Motion_MoveToLocation(axisname, location, wait);
            return res;
        }

        public bool Motion_MoveX1Y1Z1ToLocation(double x1pos, double y1pos, double z1pos, bool z1up, bool wait)
        {
            bool res = false;

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            string[] axisname = { "X1", "Y1" };
            double[] location = { x1pos, y1pos };
            res = Motion_MoveToLocation(axisname, location, wait);
            if (res == true)
            {
                res = Motion_MoveToLocation("Z1", z1pos, wait);
            }
            return res;
        }

        public bool Motion_MoveX1Y1U1ToLocation(double x1pos, double y1pos, double u1pos, bool z1up, bool wait)
        {
            bool res = false;

            if (z1up)
            {
                res = Motion_Z1UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            string[] axisname = { "X1", "Y1", "U1" };
            double[] location = { x1pos, y1pos, u1pos };
            res = Motion_MoveToLocation(axisname, location, wait);
            return res;
        }

        public bool Motion_MoveX2Y2ToLocation(double x2pos, double y2pos, bool z2up, bool wait)
        {
            bool res = false;

            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            string[] axisname = { "X2", "Y2" };
            double[] location = { x2pos, y2pos };
            res = Motion_MoveToLocation(axisname, location, wait);
            return res;
        }

        public bool Motion_MoveX2Y2Z2ToLocation(double x2pos, double y2pos, double z2pos, bool z2up, bool wait)
        {
            bool res = false;

            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            string[] axisname = { "X2", "Y2"};
            double[] location = { x2pos, y2pos };
            res = Motion_MoveToLocation(axisname, location, wait);
            if (res == true)
            {
                res = Motion_MoveToLocation("Z2", z2pos, wait);
            }
            return res;
        }

        public bool Motion_MoveY2Z2ToLocation(double y2pos, double z2pos, bool z2up, bool wait)
        {
            bool res = false;

            if (z2up)
            {
                res = Motion_Z2UpToSafe();
                if (res == false)
                {
                    return res;
                }
            }
            string[] axisname = { "Y2", "Z2" };
            double[] location = { y2pos, z2pos };
            res = Motion_MoveToLocation(axisname, location, wait);
            return res;
        }

        #endregion

        #region 运动轴和夹爪复位

        public bool Motion_HomeAll()
        {
            //所有轴做Home归零运动
            bool res = false;
            bool result = true;
            bool wait = true;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.MotionCard_InitialStatus == true)
            {
                AxisConfig tmp = GetAxisConfig("Z1");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "Z1轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "Z1轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }

                tmp = GetAxisConfig("X1");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "X1轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "X1轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }

                tmp = GetAxisConfig("Y1");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "Y1轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "Y1轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }

                tmp = GetAxisConfig("X2");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "X2轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "X2轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }

                tmp = GetAxisConfig("Y2");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "Y2轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "Y2轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }

                tmp = GetAxisConfig("Z2");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "Z2轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "Z2轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }

                tmp = GetAxisConfig("U1");
                res = MotionTool[ControllerName].Motion_HomeMove(tmp.AxisID, tmp.HomeMode, tmp.HomeDir, wait);
                if (res == true)
                {
                    str = "U1轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "U1轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }
            }
            else
            {
                str = "运动轴卡未初始化，轴系统复位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return false;
            }

            return result;
        }

        public bool Motion_BoxGripperHome()
        {
            bool res = false;
            bool result = false;
            int errorCode = 0;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == true)
            {
                res = GlobalFunction.IAIGripperTools.ElectricalGripper_HomeMove("BoxGripper", ref errorCode);
                if (res == false)
                {
                    str = "Box夹爪复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }
                else
                {
                    Thread.Sleep(1000);
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("BoxGripper", "BoxGripperOpen", ref errorCode);
                    if (res == true)
                    {
                        str = "Box夹爪复位成功";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                        result = true;
                    }
                    else
                    {
                        str = "Box夹爪复位失败";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                        result = false;
                    }
                }
            }
            else
            {
                str = "Box夹爪控制器未初始化，Box夹爪复位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                result = false;
            }

            return result;
        }

        public bool Motion_LensGripperHome()
        {
            bool res = false;
            bool result = false;
            int errorCode = 0;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == true)
            {
                res = GlobalFunction.IAIGripperTools.ElectricalGripper_HomeMove("LensGripper", ref errorCode);
                if (res == false)
                {
                    str = "Lens夹爪复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }
                else
                {
                    Thread.Sleep(1000);
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("LensGripper", "LensGripperOpen", ref errorCode);
                    if (res == true)
                    {
                        str = "Lens夹爪复位成功";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                        result = true;
                    }
                    else
                    {
                        str = "Lens夹爪复位失败";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                        result = false;
                    }
                }
            }
            else
            {
                str = "Lens夹爪控制器未初始化，Lens夹爪复位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                result = false;
            }

            return result;
        }

        public bool Motion_SMCCardMotionHome()
        {
            bool res = false;
            bool result = false;
            int errorCode = 0;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus == true)
            {
                res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_HomeMove("PogoPin", true, ref errorCode);
                if (res == false)
                {
                    str = "SMC卡片电机轴复位失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    result = false;
                }
                else
                {
                    str = "SMC卡片电机轴复位成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                    result = true;
                }
            }
            else
            {
                str = "SMC卡片电机轴控制器未初始化，轴复位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                result = false;
            }

            return result;
        }

        #endregion
    }
}