using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using STD_ISourceMeter;

namespace FocusingLensAligner
{
    //加电源表底层功能

    #region Keilthly_2401 & Keysight_364x(Agilent_364x)
    class cSourceMeterTools
    {
        public static Dictionary<string, ISourceMeter> sourcemeterhandles = new Dictionary<string, ISourceMeter>();
        public static cSourceMeterAPI csourcemetertool = new cSourceMeterAPI();

        public bool InitialSourceMeter()
        {
            bool res = false;
            res = csourcemetertool.InitDevice(ref sourcemeterhandles);
            return res;
        }

        //add by Stella
        public bool InitialSourceMeter(ref Dictionary<string, ISourceMeter> Sourcemeterhandles)
        {
            bool res = false;
            res = csourcemetertool.InitDevice(ref Sourcemeterhandles);
            sourcemeterhandles = Sourcemeterhandles;
            return res;
        }

        public bool OpenSourceMeterSetForm()
        {
            csourcemetertool.ShowDeviceSettingForm();
            return true;
        }

        public bool CloseSourceMeter()
        {
            bool res = false;
            foreach (string key in sourcemeterhandles.Keys)
            {
                res = sourcemeterhandles[key].SourceMeter_CloseDevice();
                if (res == false)
                {
                    return res;
                }
            }
            return res;
        }

        public bool Keilthly240x_SetSourceTerminal(string sourcename, bool front)
        {
            bool res = false;
            Hashtable ht = new Hashtable();
            ht.Add("FuncName", "SourceMeter_SetFrontRearPanel");
            ht.Add("isFront", front);
            res = sourcemeterhandles[sourcename].SourceMeter_ExecuteFunction(ref ht);
            return res;
        }

        public bool Keilthly240x_SetVoltageValue(string sourcename, double value)
        {
            bool res = false;
            Hashtable ht = new Hashtable();
            ht.Add("FuncName", "SourceMeter_SetSourceType");
            ht.Add("funcType", SourceFuncType.VOLTAGE);
            ht.Add("modeType", SourceModeType.FIX);
            res = sourcemeterhandles[sourcename].SourceMeter_ExecuteFunction(ref ht);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            ht.Clear();
            ht.Add("FuncName", "SourceMeter_SetMeasureType");
            ht.Add("funcType", SourceFuncType.CURRENT);
            res = sourcemeterhandles[sourcename].SourceMeter_ExecuteFunction(ref ht);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            string cmd = ":SENS:CURR:PROT " + GlobalParameters.productconfig.processConfig.currentCompliance.ToString();
            //string cmd = ":SENS:CURR:PROT 1.05";
            string refdata = "";
            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            string rangecmd = ":SENS:CURR:RANG " + "0.5";
            string refrangedata = "";
            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(rangecmd, ref refrangedata);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            res = sourcemeterhandles[sourcename].SourceMeter_SetMeasureRange(SourceFuncType.CURRENT, 6);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            res = sourcemeterhandles[sourcename].SourceMeter_SetSourceVoltage(value);
            if (res == false)
            {
                return res;
            }
            return res;
        }

        public bool Keilthly240x_SetCurrentValue(string sourcename, double value)
        {
            bool res = false;
            Hashtable ht = new Hashtable();
            ht.Add("FuncName", "SourceMeter_SetSourceType");
            ht.Add("funcType", SourceFuncType.CURRENT);
            ht.Add("modeType", SourceModeType.FIX);
            res = sourcemeterhandles[sourcename].SourceMeter_ExecuteFunction(ref ht);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            ht.Clear();
            ht.Add("FuncName", "SourceMeter_SetMeasureType");
            ht.Add("funcType", SourceFuncType.VOLTAGE);
            res = sourcemeterhandles[sourcename].SourceMeter_ExecuteFunction(ref ht);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            res = sourcemeterhandles[sourcename].SourceMeter_SetSourceRange(SourceFuncType.CURRENT, 5);
            if (res == false)
            {
                return res;
            }
            //GeneralFunction.Delay(100);
            res = sourcemeterhandles[sourcename].SourceMeter_SetSourceCurrent(value);
            if (res == false)
            {
                return res;
            }
            return res;
        }

        public bool Keilthly240x_TurnOnKeithley(string sourcename, bool on)
        {
            bool res = false;
            res = sourcemeterhandles[sourcename].SourceMeter_SetSourceOnOff(on);
            return res;
        }

        public bool Keilthly240x_ReadKeithleyValue(string sourcename, SourceFuncType meartype, ref double readvalue)
        {
            bool res = false;
            //GlobalParameters.csourcemetertool.Keilthly240x_SetVoltageValue("K2400-1", GlobalParameters.productconfig.processConfig.ramppoweronenable ? 0.0 : GlobalParameters.productconfig.processConfig.voltageval);

            res = sourcemeterhandles[sourcename].SourceMeter_MeasureOneChannel(meartype, ref readvalue);
            return res;
        }

        public bool Keilthly240x_QuerySourceState(string sourcename, ref bool state)
        {
            bool res = false;
            string cmd = "OUTP:STAT?";
            string refdata = "";
            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
            if (refdata == "1")
            {
                state = true;
            }
            else
            {
                state = false;
            }
            return res;
        }

        public bool Keilthly240x_QuerySourceValue(string sourcename, SourceFuncType sourceFuncType, ref double value)
        {
            bool res = false;
            string cmd = "";
            string refdata = "";
            if (sourceFuncType == SourceFuncType.VOLTAGE)
            {
                cmd = ":SOUR:VOLT:LEV:IMM:AMPL?";
            }
            else
            {
                cmd = ":SOUR:CURR:LEV:IMM:AMPL?";
            }

            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
            if (res)
                value = double.Parse(refdata);
            return res;
        }

        public bool Keilthly240x_TurnOnKeithleyRamp(string sourcename, SourceFuncType type, bool on, RampPara rampara)
        {
            bool res = false;
            int stepdir = 0;
            double setval = 0;
            double value = 0;
            if (on)
            {
                if (rampara.endval < rampara.startval)
                {
                    return false;
                }
                setval = rampara.startval;
                stepdir = 1;
                if (type == SourceFuncType.VOLTAGE)
                {
                    res = Keilthly240x_SetVoltageValue(sourcename, setval);
                }
                else if (type == SourceFuncType.CURRENT)
                {
                    res = Keilthly240x_SetCurrentValue(sourcename, setval);
                }
                if (res == false)
                {
                    return res;
                }

                res = Keilthly240x_TurnOnKeithley(sourcename, true);
                if (res == false)
                {
                    return res;
                }
                while (setval < rampara.endval)
                {
                    if (type == SourceFuncType.VOLTAGE)
                    {
                        res = Keilthly240x_SetVoltageValue(sourcename, setval);
                    }
                    else if (type == SourceFuncType.CURRENT)
                    {
                        res = Keilthly240x_SetCurrentValue(sourcename, setval);
                    }
                    if (res == false)
                    {
                        return res;
                    }
                    setval += rampara.step * stepdir;
                    Thread.Sleep((int)rampara.interval * 1000);
                }
                res = Keilthly240x_SetVoltageValue(sourcename, rampara.endval);
                if (res == false)
                {
                    return res;
                }
            }
            else
            {
                stepdir = -1;
                if (type == SourceFuncType.VOLTAGE)
                {
                    res = Keilthly240x_QuerySourceValue(sourcename, SourceFuncType.VOLTAGE, ref value);
                    if (res == false || value < rampara.endval)
                    {
                        return false;
                    }
                    else
                    {
                        setval = value;
                    }
                    res = Keilthly240x_SetVoltageValue(sourcename, setval);
                }
                else if (type == SourceFuncType.CURRENT)
                {
                    res = Keilthly240x_QuerySourceValue(sourcename, SourceFuncType.CURRENT, ref value);
                    if (res == false || value < rampara.endval)
                    {
                        return false;
                    }
                    else
                    {
                        setval = value;
                    }
                    res = Keilthly240x_SetCurrentValue(sourcename, setval);
                }
                if (res == false)
                {
                    return res;
                }

                res = Keilthly240x_TurnOnKeithley(sourcename, true);
                if (res == false)
                {
                    return res;
                }
                while (setval < rampara.endval)
                {
                    if (type == SourceFuncType.VOLTAGE)
                    {
                        res = Keilthly240x_SetVoltageValue(sourcename, setval);
                    }
                    else if (type == SourceFuncType.CURRENT)
                    {
                        res = Keilthly240x_SetCurrentValue(sourcename, setval);
                    }
                    if (res == false)
                    {
                        return res;
                    }
                    setval += rampara.step * stepdir;
                    Thread.Sleep((int)rampara.interval * 1000);
                }
                res = Keilthly240x_SetVoltageValue(sourcename, rampara.endval);
                if (res == false)
                {
                    return res;
                }
                if (rampara.endval == 0)
                {
                    res = Keilthly240x_TurnOnKeithley(sourcename, false);
                    if (res == false)
                    {
                        return res;
                    }
                }

            }
            return res;
        }

        public bool Keysight364x_SetVoltageValue(string sourcename, double value)
        {
            bool res = false;
            string cmd = String.Empty;
            string refdata = string.Empty;
            // res = sourcemeterhandles[sourcename].SourceMeter_Reset();//重置
            //或者清空故障
            cmd = "*CLS";
            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
            // string cmd = ":SENS:CURR:PROT " + GlobalParameters.productconfig.processConfig.currentcompliance.ToString();//ClearErrors
            if (res)
            {
                //设定电压值
                res = sourcemeterhandles[sourcename].SourceMeter_SetSourceVoltage(value);
            }
            return res;
        }

        public bool Keysight364x_ReadE364XACurrent(string sourcename, ref double readvalue)
        {
            bool res = false;
            string cmd = String.Empty;
            string refdata = string.Empty;
            // res = sourcemeterhandles[sourcename].SourceMeter_Reset();//重置
            //或者清空故障
            cmd = "*CLS";
            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
            // string cmd = ":SENS:CURR:PROT " + GlobalParameters.productconfig.processConfig.currentcompliance.ToString();//ClearErrors
            if (res)
            {
                //读取电流
                cmd = "MEAS:CURR?";
                res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
                readvalue = Convert.ToDouble(refdata);
                //res = sourcemeterhandles[sourcename].SourceMeter_MeasureOneChannel(SourceFuncType.CURRENT, ref readvalue);
            }
            return res;
        }

        public bool Keysight364x_TurnE364XAOnOff(string sourcename, bool on)
        {
            bool res = false;
            res = sourcemeterhandles[sourcename].SourceMeter_SetSourceOnOff(on);
            return res;
        }

        public bool Keysight364x_GetOutputStatus(string sourcename)
        {
            bool res = false;
            string cmd = String.Empty;
            string refdata = string.Empty;

            cmd = "OUTP?";
            res = sourcemeterhandles[sourcename].SourceMeter_SendCmd(cmd, ref refdata);
            //// string cmd = ":SENS:CURR:PROT " + GlobalParameters.productconfig.processConfig.currentcompliance.ToString();//ClearErrors
            //if (res)
            //{
            //    //读取电流
            //    res = sourcemeterhandles[sourcename].SourceMeter_MeasureOneChannel(SourceFuncType.CURRENT, ref readvalue);
            //}
            return res;
        }
    }
    #endregion


}