using System;
using System.Collections.Generic;
using System.Windows.Forms;
using STD_IElectricGripper;
using System.Threading;

namespace FocusingLensAligner
{
    //夹爪控制器底层功能
    class cElectricGripperTools
    {
        public cElectricGripperTool ElectricGripperTool = new cElectricGripperTool();
        public ElectricGripperConfigCollection ElectricGripperConfigCollection = new ElectricGripperConfigCollection();
        //public int retErrorCode;

        public bool ElectricalGripper_Initial(ref int retErrorCode)
        {
            bool res = false;

            res = ElectricGripperTool.ElectricGripperControl_InitialAllDevice(out retErrorCode);
            return res;
        }

        public bool ElectricalGripper_UnInitial(ref int retErrorCode)
        {
            bool res = false;

            res = ElectricGripperTool.ElectricGripperControl_ReleaseAllDevice(out retErrorCode);
            return res;
        }

        public bool ElectricalGripper_OpenSetupPanel()
        {
            bool res = true;

            ElectricGripperTool.OpenConfigurationFrm();
            return res;
        }

        public bool ElectricalGripper_OpenDevice(string GripperName, ref int retErrorCode)
        {
            bool res = true;

            res = ElectricGripperTool.ElectricGripperControl_OpenDevice(GripperName, out retErrorCode);
            return res;
        }

        public bool ElectricalGripper_CloseDevice(string GripperName, ref int retErrorCode)
        {
            bool res = true;

            res = ElectricGripperTool.ElectricGripperControl_CloseDevice(GripperName, out retErrorCode);
            return res;
        }

        public bool ElectricalGripper_SetServoOnSwitch(string GripperName, int ServoOnSwitch, ref int retErrorCode)
        {
            bool res = false;
            bool servoOn = false;

            switch (ServoOnSwitch)
            {
                case 1:
                    servoOn = true;
                    break;

                case 0:
                    servoOn = false;
                    break;
            }
            res = ElectricGripperTool.ElectricGripperControl_ServoOn(GripperName, servoOn, out retErrorCode);
            if (res == true)
            {
                Thread.Sleep(1000);
                res = ElectricGripperTool.ElectricGripperControl_IsServoOn(GripperName, out servoOn, out retErrorCode);
                if (res == true)
                {
                    switch (ServoOnSwitch)
                    {
                        case 1:
                            if (servoOn == false)
                            {
                                return false;
                            }
                            break;

                        case 0:
                            if (servoOn == true)
                            {
                                return false;
                            }
                            break;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool ElectricalGripper_GetServoOnStatus(string GripperName, ref bool ServoOnStatus, ref int retErrorCode)
        {
            bool res = false;

            res = ElectricGripperTool.ElectricGripperControl_IsServoOn(GripperName, out ServoOnStatus, out retErrorCode);
            return res;
        }

        public bool ElectricalGripper_ClearAlarmSignal(string GripperName, ref int retErrorCode)
        {
            bool res = false;

            res = ElectricGripperTool.ElectricGripperControl_ClearAlarmSignal(GripperName, out retErrorCode);
            return res;
        }

        public bool ElectricalGripper_PositionMove(string GripperName, string PositionName, ref int retErrorCode)
        {
            bool res = false;
            bool pend = false;
            int count = 0;

            res = ElectricGripperTool.ElectricGripperControl_PositionMove(GripperName, PositionName, out retErrorCode);
            if (res == true)
            {
                while (pend == false)
                {
                    Thread.Sleep(100);
                    res = ElectricGripperTool.ElectricGripperControl_IsPositionMoveCompleted(GripperName, out pend, out retErrorCode);
                    if (res == false)
                    {
                        return false;
                    }
                    if (pend == false)
                    {
                        if (count >= 30)
                        {
                            return false;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool ElectricalGripper_HomeMove(string GripperName, ref int retErrorCode)
        {
            bool res = false;
            bool homed = false;

            res = ElectricGripperTool.ElectricGripperControl_HomeMove(GripperName, out retErrorCode);
            if (res == true)
            {
                Thread.Sleep(3000);
                res = ElectricGripperTool.ElectricGripperControl_IsHomeCompleted(GripperName, out homed, out retErrorCode);
                if (res == true)
                {
                    if (homed == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ElectricGripper_GetMotorCurrentAmpere(string GripperName, ref uint CurrentAmpere, ref int retErrorCode)
        {
            bool res = false;

            res = ElectricGripperTool.ElectricGripperControl_GetMotorCurrentAmpere(GripperName, out CurrentAmpere, out retErrorCode);
            return res;
        }

        public bool ElectricGripper_IsPositionMoveCompleted(string GripperName, ref bool Pend, ref int retErrorCode)
        {
            bool res = false;

            res = ElectricGripperTool.ElectricGripperControl_IsPositionMoveCompleted(GripperName, out Pend, out retErrorCode);
            return res;
        }

        public void ElectricalGripper_ReturnErrorMessage(int retErrorCode, ref string ErrorMessage)
        {
            ElectricGripperTool.ElectricGripperControl_GetErrorMessage(retErrorCode, out ErrorMessage);
        }
    }
}
