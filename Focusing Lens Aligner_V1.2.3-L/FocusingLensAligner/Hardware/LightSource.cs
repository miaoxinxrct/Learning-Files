using STD_ILightSource;

namespace FocusingLensAligner
{
    //光源控制器底层功能
    class cLightSourcesTools
    {
        public cLightTool LightTool = new cLightTool();
        public LightSourceConfigurationCollection lightcollection = new LightSourceConfigurationCollection();

        public bool LightSource_Initial()
        {
            bool res = false;

            res = LightTool.InitialAllLight();
            return res;
        }

        public bool LightSource_OpenSetupPanel()
        {
            bool res = true;

            LightTool.OpenConfigurationFrm();
            return res;
        }

        public bool LightSource_Open(string LightSourceName, double LightValue)
        {
            bool res = false;

            res = LightTool.LightControl_OpenChannel(LightSourceName, LightValue);
            return res;
        }

        public bool LightSource_Close(string LightSourceName)
        {
            bool res = false;

            res = LightTool.LightControl_CloseChannel(LightSourceName);
            return res;
        }

        public bool LightSource_CloseAll()
        {
            bool res = true;

            LightTool.CloseAllLight();
            return res;
        }

        public bool LightSource_SetMaxCurrent(string LightSourceName, int MaxCurrentValue)
        {
            bool res = false;

            res = LightTool.LightControl_SetCurrentMax(LightSourceName, MaxCurrentValue);
            return res;
        }

        public bool LightSource_SetWorkMode(string LightSourceName, WorkMode Mode)
        {
            bool res = false;

            res = LightTool.LightControl_SetChannelMode(LightSourceName, Mode);
            return res;
        }

        public bool LightSource_OpenUpRing(double value)
        {
            bool res = false;
            string lightname = "UpRing";
            res = LightSource_Open(lightname, value);
            return res;
        }
        public bool LightSource_CloseUpRing()
        {
            bool res = false;
            string lightname = "UpRing";
            res = LightSource_Close(lightname);
            return res;
        }
        public bool LightSource_OpenDnRing(double value)
        {
            bool res = false;
            string lightname = "DnRing";
            res = LightSource_Open(lightname, value);
            return res;
        }
        public bool LightSource_CloseDnRing()
        {
            bool res = false;
            string lightname = "DnRing";
            res = LightSource_Close(lightname);
            return res;
        }
        public bool LightSource_OpenUpSpot(double value)
        {
            bool res = false;
            string lightname = "UpSpot";
            res = LightSource_Open(lightname, value);
            return res;
        }
        public bool LightSource_CloseUpSpot()
        {
            bool res = false;
            string lightname = "UpSpot";
            res = LightSource_Close(lightname);
            return res;
        }
        public bool LightSource_OpenDnSpot(double value)
        {
            bool res = false;
            string lightname = "DnSpot";
            res = LightSource_Open(lightname, value);
            return res;
        }
        public bool LightSource_CloseDnSpot()
        {
            bool res = false;
            string lightname = "DnSpot";
            res = LightSource_Close(lightname);
            return res;
        }
    }
}
