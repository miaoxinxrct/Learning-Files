using System;
using System.Collections.Generic;
using STD_ICureControl;

namespace FocusingLensAligner
{
    //UV控制器底层功能
    class cUVControllerTools
    {
        public cControlTool controlTool = new STD_ICureControl.cControlTool();
        Dictionary<string, ICureControl> UVDeviceHandles = null;

        public enum Enum_ChannelName
        {
            Channel1 = 1,
            Channel2 = 2,
            Channel3 = 3,
            Channel4 = 4
        }

        public bool UVController_Initial()
        {
            bool res = false;

            res = controlTool.InitDevice(ref UVDeviceHandles);
            return res;
        }

        public bool UVController_UnInitial()
        {
            bool res = false;

            if (UVDeviceHandles != null)
            {
                foreach (string UVControllerName in UVDeviceHandles.Keys)
                {
                    res = UVDeviceHandles[UVControllerName].CureControl_Close();
                }
            }
            return res;
        }

        public bool UVController_OpenSetupPanel()
        {
            bool res = true;

            CureControlSetting UVControlSetting = new CureControlSetting();
            UVControlSetting.Show();
            return res;
        }

        public bool UVController_SetUVParametes(string UVControllerName, string ChannelName, double Power, double Time)
        {
            bool res = false;
            int Channel = Convert.ToInt32(Enum.Parse(typeof(Enum_ChannelName), ChannelName));

            if (UVDeviceHandles != null)
            {
                res = UVDeviceHandles[UVControllerName].CureControl_SetType0PWRTime(Channel, Power, Time);
            }
            return res;
        }

        public bool UVController_StartSingleChannelUV(string UVControllerName, string ChannelName)
        {
            bool res = false;
            int Channel = Convert.ToInt32(Enum.Parse(typeof(Enum_ChannelName), ChannelName));

            if (UVDeviceHandles != null)
            {
                res = UVDeviceHandles[UVControllerName].CureControl_StartUV(Channel);
            }
            return res;
        }

        public bool UVController_StopSingleChannelUV(string UVControllerName, string ChannelName)
        {
            bool res = false;
            int Channel = Convert.ToInt32(Enum.Parse(typeof(Enum_ChannelName), ChannelName));

            if (UVDeviceHandles != null)
            {
                res = UVDeviceHandles[UVControllerName].CureControl_StopUV(Channel);
            }
            return res;
        }

        public bool UVController_StartBothChannelUV(string UVControllerName, string ChannelName1, string ChannelName2)
        {
            bool res = false;

            if (UVController_StartSingleChannelUV(UVControllerName, ChannelName1) && UVController_StartSingleChannelUV(UVControllerName, ChannelName2))
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        public bool UVController_StopBothChannelUV(string UVControllerName, string ChannelName1, string ChannelName2)
        {
            bool res = false;

            if (UVController_StopSingleChannelUV(UVControllerName, ChannelName1) && UVController_StopSingleChannelUV(UVControllerName, ChannelName2))
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
    }
}