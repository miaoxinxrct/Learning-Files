using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using STD_IFrameWork;
using MESdll;

namespace FocusingLensAligner
{
    //MES底层功能
    class cMESTools
    {
        bool mesinitflag = false;       //MES功能初始化结果标志
        public MESMain mesmain = new MESMain();

        /// <summary>
        /// MES Initialize
        /// </summary>
        /// <returns>res</returns>
        public bool MES_Initial()
        {
            //MES功能初始化
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("Camstar\\" + "MESConfig.ini");
            string resource = cFrameWork.cFrameWorkParameter.RigName;

            res = mesmain.Init(filepath, resource);
            mesinitflag = res;
            return res;
        }

        /// <summary>
        /// Get MES Function Initialize Status
        /// </summary>
        /// <returns>res</returns>
        public bool MES_GetInitialStatus()
        {
            //返回MES功能初始化结果
            //true - 已初始化成功；false - 初始化失败或者没有进行过初始化
            return mesinitflag;
        }

        /// <summary>
        /// Verify User Name and Passward
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="passWord">Password</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_VerifyEmployee(string userName, string passWord, ref string errMsg)
        {
            //核查员工身份信息
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;

            res = mesmain.VerifyEmployeeName(userName, passWord, ref errMsg);
            return res;
        }

        /// <summary>
        /// Get PN by DC Number 
        /// </summary>
        /// <param name="dcStr">DC Number</param>
        /// <param name="pnStr">PN Number</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_GetPNByDC(string dcStr, ref string pnStr, ref string errMsg)
        {
            //根据物料DC信息查询对应的PN信息
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] sConditionColumns = new string[] { dcStr, "productName" };//containerName,description,productName,ExpirationDate
            string[] sReturnValues = null;
            int iNumberOfColumnReturns = 0;

            res = mesmain.GetGenericData_BySP("GetContainerInfoByColumn", ref sConditionColumns, ref sReturnValues, ref iNumberOfColumnReturns, ref errMsg);
            if (res && sReturnValues.Length > 0)
            {
                pnStr = sReturnValues[0];
            }
            return res;
        }

        /// <summary>
        /// Get Valid Time by DC Number
        /// </summary>
        /// <param name="dcStr">DC Number</param>
        /// <param name="validTime">Valid Time</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_GetValidTimeByDC(string dcStr, ref string validTime, ref string errMsg)
        {
            //根据物料DC信息核查有效期信息
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] sConditionColumns = new string[] { dcStr, "ExpirationTime" };  //containerName,description,productName,ExpirationDate
            string[] sReturnValues = null;
            int iNumberOfColumnReturns = 0;

            res = mesmain.GetGenericData_BySP("GetContainerInfoByColumn", ref sConditionColumns, ref sReturnValues, ref iNumberOfColumnReturns, ref errMsg);
            if (res && sReturnValues.Length > 0)
            {
                validTime = sReturnValues[0];
            }
            return res;
        }

        /// <summary>
        /// Check Product Step Name by Single Step
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_CheckSN(string prodSN, ref string errMsg)
        {
            //核查产品SN站点信息（单站点配置模式）
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] sReturns = null;

            res = mesmain.GetContainerInfo(prodSN, ref sReturns);
            if (res)
            {
                if (String.Compare(GlobalParameters.MESConfig.singlestep.stepname, sReturns[5])!=0)
                {
                    errMsg = "SN current step:" + sReturns[5] + "does't match the config step name.";
                    res = false;
                }
            }
            return res;
        }

        /// <summary>
        /// Check Product Step Name by Multi Step
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="prodName">Product Name</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_CheckSNWithMultiStep(string prodSN, string prodName, ref string errMsg)
        {
            //核查产品SN站点信息（多站点配置模式）
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] sReturns = null;

            res = mesmain.GetContainerInfo(prodSN, ref sReturns);
            if (res)
            {
                MultiStep step = GlobalParameters.MESConfig.multistep.Find(X => X.productname == prodName);
                try
                {
                    if (String.Compare(step.stepname, sReturns[5]) != 0)
                    {
                        errMsg = "SN current step:" + sReturns[5] + "does't match the config step name.";
                        res = false;
                    }
                }
                catch { }               
            }
            return res;
        }

        /// <summary>
        /// Check Bom List
        /// </summary>
        /// <param name="count">DC Count in MainForm</param>
        /// <param name="dcCount">DC Count in Config File</param>
        /// <param name="listPN">PN List Getted by DC number </param>
        /// <param name="prodSN">Product Number</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_CheckBomList(int count, int dcCount, List<string> listPN, string prodSN, ref string errMsg)
        {
            //检查产品SN和D/C物料BOM的捆绑关系并检查D/C规格和数量是否正常
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string str = "";
            string[] sConditionColumns = new string[2];
            string[] sReturnValues = null;
            int iNumberOfColumnReturns = 0;
            List<int> listIndex = new List<int>();

            if (listPN.Count == 0)
            {
                errMsg = "PN number is null.";
                return false;
            }

            if (count != dcCount)
            {
                errMsg = "DC number is wrong.";
                return false;
            }
            else
            {
                sConditionColumns[0] = prodSN;
                sConditionColumns[1] = "ProductName,SubstitutionProduct";
                res = mesmain.GetGenericData_BySP("GetBomListBySN", ref sConditionColumns, ref sReturnValues, ref iNumberOfColumnReturns, ref errMsg);
                if (!res || iNumberOfColumnReturns < 1)
                {
                    str = errMsg;
                }
                else
                {
                    for (int k = 0; k < dcCount; k++)//* iNumberOfColumnReturns
                    {
                        for (int i = 0; i < sReturnValues.Length; i++)
                        {
                            if (sReturnValues[i].Contains(listPN[k]))
                            {
                                listIndex.Add(i);
                                break;
                            }
                        }
                    }
                    if (listIndex.Count < dcCount)
                    {
                        errMsg = "DC number is not in the bom list.";
                        res = false;
                    }
                    else
                    {
                        if (listIndex.GroupBy(n => n).Any(c => c.Count() > 1))
                        {
                            errMsg = "There are same PN number in the PN list.";
                            res = false;
                        }
                        else
                        {
                            listIndex.Sort();
                            int firstIndex = listIndex[0];
                            for (int i = 1; i < listIndex.Count; i++)
                            {
                                if ((firstIndex % 2 == 1 && firstIndex - listIndex[i] == 1) || (firstIndex % 2 == 0 && listIndex[i] - firstIndex == 1))
                                {
                                    errMsg = "There are same type PN number in the PN list";
                                    res = false;
                                }
                                else
                                {
                                    res = true;
                                }
                            }
                        }
                    }
                }
            }
            return res;

        }

        /// <summary>
        /// Auto Component Issue
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="dcCount">DC Count</param>
        /// <param name="dcList">DC Number List</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_AutoComponentIssue(string prodSN, int dcCount, List<string> dcList, ref string errMsg)
        {
            //自动发料
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] dcLot = new string[dcCount];

            for (int i=0; i<dcCount;i++)
            {
                dcLot[i] = dcList[i];
            }
            res = mesmain.ComponentIssue(prodSN, ref dcLot, ref errMsg);
            return res;
        }

        /// <summary>
        /// Auto Move Station Out
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_AutoMoveOut(string prodSN, ref string errMsg)
        {
            //自动过站
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] sConditionColumns = new string[] { prodSN };
            string[] sReturnValues = new string[0];
            int iNumberOfColumnReturns = 0;

            res = mesmain.GetGenericData_BySP("CheckCompIssue", ref sConditionColumns, ref sReturnValues, ref iNumberOfColumnReturns, ref errMsg);
            if (res && sReturnValues[0]=="Yes")
            {
                res = mesmain.MoveStandard(prodSN,"",ref errMsg);
            }
            else if(sReturnValues[0]=="No")
            {
                errMsg = "Component issue verification is failed.";
            }
            return res;
        }

        /// <summary>
        /// Auto jump into specified station
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="workFlowName">Specified work flow name</param>
        /// <param name="workFlowStep">Specified Work flow step</param>
        /// <returns>res</returns>
        public bool MES_AutoMoveOutNonStd(string prodSN, string workFlowName, string workFlowStep, ref string errMsg)
        {
            //自动跳转至指定站点
            //针对ICTROSA产品：workFlowName = "OSA_IC TROSA_ITTRx 64S"; workFlowStep = "Rework start"
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;

            if (prodSN == "" || workFlowName == "" || workFlowStep == "")
            {
                return false;
            }

            res = mesmain.MoveNonStd(prodSN, workFlowName, workFlowStep, false, ref errMsg, "");
            return res;
        }

        /// <summary>
        /// Auto Hold
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="holdReason">Hold Product Reason</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_AutoHold(string prodSN, string holdReason, ref string errMsg)
        {
            //自动OnHold产品在当前站点
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;

            res = mesmain.Hold(prodSN, holdReason, ref errMsg);
            return res;
        }

        /// <summary>
        /// Auto remove component issue
        /// </summary>
        /// <param name="prodSN">Product SN</param>
        /// <param name="chlInfo">Channel Config Info in Camstar</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>res</returns>
        public bool MES_AutoRemove(string prodSN, ref string errMsg)
        {
            //自动拆料
            if (!mesinitflag)
            {
                errMsg = "MES is not initialized";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            string[] SPColumn = new string[5];
            string[] CompIssueData = new string[5];
            for (int i = 0; i < SPColumn.Length; i++)
            {
                SPColumn[i] = "";
            }
            for (int i = 0; i < CompIssueData.Length; i++)
            {
                CompIssueData[i] = "";
            }
            string[] sReturnValues = null;
            int iNumberOfColumnReturns = 0;

            //获取当前产品SN对应的站点名称，以字符串数组方式返回至sReturnValues
            res = mesmain.GetContainerInfo(prodSN, ref sReturnValues);
            if (res == false)
            {
                errMsg = "MES failed to obtain the step name corresponding to the SN.";
                return false;
            }

            //获取产品SN与物料捆绑的信息集，以字符串数组方式返回至sReturnValues
            SPColumn[0] = prodSN;               //产品SN信息
            SPColumn[3] = sReturnValues[2];     //站点名
            res = mesmain.GetGenericData_BySP("GetRemovedContainer", ref SPColumn, ref sReturnValues, ref iNumberOfColumnReturns, ref errMsg);
            if (res == false)
            {
                return false;
            }

            //对LENS2机台来说，拆料主要针对LENS(例如：R20110201106-04)，胶水(例如：A2011137902)不涉及
            string Epoxy_LotNo = sReturnValues[3];  //胶水的Lot号
            string Lens_LotNo = sReturnValues[14];  //Lens的Lot号            
            res = mesmain.ComponentRemove(prodSN, Lens_LotNo, "", "", "", "", CompIssueData, ref errMsg);
            
            return res;
        }

        public void MES_SetIssueQtyAndRefDesignator(int qty, string designator)
        {
            mesmain.ComponentIssueQty = 1;
            mesmain.CompIssueRefDesignator = designator;
        }

        public bool MES_LoadDCList(ref DCList dcCollection)
        {
            //调取本地DC配置文件
            bool res = false;
            string filename = "DCList.xml";
            string filepath = GeneralFunction.GetConfigFilePath(filename);

            res = STD_IGeneralTool.GeneralTool.ToTryLoad<DCList>(ref dcCollection, filepath);
            if(res == false)
            {
                string errMsg = "Load local DCList config file failed!";
                GlobalFunction.updateStatusDelegate(errMsg, Enum_MachineStatus.ERROR);
                MessageBox.Show(errMsg, "Load Config Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return res;
        }

        public bool MES_SaveDCList(DCList dcCollection)
        {
            //保存本地DC配置文件
            bool res = false;
            string filename = "DCList.xml";
            string filepath = GeneralFunction.GetConfigFilePath(filename);

            res = STD_IGeneralTool.GeneralTool.TryToSave<DCList>(dcCollection, filepath);
            return res;
        }
    }
}
