using Bipap.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bipap.Service.Functionality
{
    public class Module
    {
        public SessionStepOneModel GetSessionStepOneModel(string fileName)
        {

            #region StepOne
            //Int16[] pressure = new Int16[1000000];
            //Int16[] flow = new Int16[1000000];
            //Int16[] heater = new Int16[1000000];
            //Int16[] leak = new Int16[1000000];
            int offset = 59;
            float[] parameters = new float[offset];
            int copy_pressure = 0;
            int copy_flow = 0;
            int copy_heater = 0;
            int copy_leak = 0;
            var len = new System.IO.FileInfo(fileName).Length;
            var br = new BinaryReader(System.IO.File.Open(fileName, FileMode.Open));
            var data_8bit = new byte[len];
            Int16[] data_16bit = new Int16[data_8bit.Length / 2];
            data_8bit = br.ReadBytes(int.Parse(len.ToString()));
            br.Close();
            var flow = new Int16[len / 4];
            var pressure = new Int16[len / 4];
            var leak = new Int16[len / 2000];
            var heater = new Int16[len / 2000];


            Buffer.BlockCopy(data_8bit, 0, data_16bit, 0, data_8bit.Length);

            var flowCount = 0;

            for (int i = 0; i < data_16bit.Length; i++)
            {

                if (data_16bit[i] == -21555 && data_16bit[i + 3 + offset] == 32767)
                    Array.Copy(data_16bit, i + 3, parameters, 0, offset);


                if ((data_16bit[i] == 32765 && data_16bit[i - 251] == 32766) || (data_16bit[i] == 32763 && data_16bit[i - 251] == 32764))
                {

                    Array.Copy(data_16bit, i + 1, pressure, copy_pressure, 250);
                    Array.Copy(data_16bit, i - 250, flow, copy_flow, 250);
                    copy_flow += 250;
                    copy_pressure += 250;
                    flowCount++;


                }


                if (i > 500 && data_16bit[i - 251] == 32765 && data_16bit[i - 502] == 32766)
                {
                    Array.Copy(data_16bit, i, heater, copy_leak, 1);
                    copy_heater++;

                }


                if (i > 500 && data_16bit[i - 251] == 32763 && data_16bit[i - 502] == 32764)
                {
                    Array.Copy(data_16bit, i, leak, copy_leak, 1);
                    copy_leak++;
                }

            }

            #region Scaling data
            for (int w = 0; w < flow.Length; w++)
                flow[w] = (Int16)(flow[w] * 0.02);

            for (int w = 0; w < leak.Length; w++)
                leak[w] = (Int16)(leak[w] * 0.02);

            for (int w = 0; w < heater.Length; w++)
                heater[w] = (Int16)(heater[w] * 0.01);
            #endregion



            #endregion

            #region StepTwo

            UInt16 T1 = (UInt16)parameters[58];
            UInt16 T2 = (UInt16)parameters[57];
            UInt16 T3 = (UInt16)parameters[56];
            UInt16 T4 = (UInt16)parameters[55];

            UInt32 CH1 = (UInt32)T1;
            UInt32 CH2 = (UInt32)T2;
            UInt32 CH3 = (UInt32)T3;
            UInt32 CH4 = (UInt32)T4;

            UInt32 RES1 = (CH2 << 16) | CH1;
            UInt32 RES2 = (CH4 << 16) | CH3;

            UInt64 startDate = RES1 + RES2;
            UInt64 days_elapsed = startDate / 86400;
            UInt64 RemindSecound = startDate % 86400;
            UInt64 hour = RemindSecound / 3600;
            RemindSecound = Convert.ToUInt32(RemindSecound % 3600);
            UInt64 minutes = (RemindSecound / 60);
            UInt64 seconds = (RemindSecound % 60);
            UInt64 LoopYear = days_elapsed / 1460;
            UInt64 year = (days_elapsed - LoopYear) / 365;
            UInt64 Mounth = 0;
            UInt64 Day = 0;
            UInt64 RemindYearDay = days_elapsed - (year * 365) - (year / 4);
            if (RemindYearDay > 186)
            {
                Mounth = ((RemindYearDay - 186) / 30) + 6;
                Day = (RemindYearDay - 186) % 30;
            }
            else
            {
                Mounth = RemindYearDay / 31;
                Day = RemindYearDay % 31;
            }

            UInt64 currentYear = (year + 1396);
            UInt64 currentMonth = (Mounth + 1);
            UInt64 currentDaye = (Day + 1);
            #endregion

            var ddd = ((pressure.Length / 2400 / 60));
            var res = new SessionStepOneModel();

            res.Parameters = parameters;
            res.Length = len;
            res.Pressure = pressure;
            res.Flow = flow;
            res.Heater = heater;
            res.Leak = leak;
            res.Day = currentDaye.ToString();
            res.Year = currentYear.ToString();
            res.Month = currentMonth.ToString();
            res.Hour = hour.ToString();
            res.Minutes = minutes.ToString();
            res.StrarTime = hour.ToString() + ":" + minutes.ToString();
            res.EndTime = (flowCount * 250) / 72000 + ":" + ((flowCount * 250) % 72000) / 1200;
            res.Name = currentYear.ToString() + "/" + currentMonth.ToString() + "/" + currentDaye.ToString() + "/" + hour.ToString() + "/" + minutes.ToString();
            return res;

        }

        public StepOneModule GetStepOneModule(byte[] fileBytes, long fileLength, int fileId)
        {
            if (fileBytes == null)
                return null;
            #region StepOne
            //Int16[] pressure = new Int16[1000000];
            //Int16[] flow = new Int16[1000000];
            //Int16[] heater = new Int16[1000000];
            //Int16[] leak = new Int16[1000000];
            int offset = 59;
            float[] parameters = new float[offset];
            int copy_pressure = 0;
            int copy_flow = 0;
            int copy_heater = 0;
            int copy_leak = 0;
            var len = fileLength;
            var br = fileBytes;
            var data_8bit = new byte[len];
            Int16[] data_16bit = new Int16[data_8bit.Length / 2];
            data_8bit = fileBytes;
            var flow = new Int16[len / 4];
            var pressure = new Int16[len / 4];
            var leak = new Int16[len / 2000];
            var heater = new Int16[len / 2000];


            Buffer.BlockCopy(data_8bit, 0, data_16bit, 0, data_8bit.Length);

            var flowCount = 0;

            for (int i = 0; i < data_16bit.Length; i++)
            {

                if (data_16bit[i] == -21555 && data_16bit[i + 3 + offset] == 32767)
                    Array.Copy(data_16bit, i + 3, parameters, 0, offset);


                if ((data_16bit[i] == 32765 && data_16bit[i - 251] == 32766) || (data_16bit[i] == 32763 && data_16bit[i - 251] == 32764))
                {

                    Array.Copy(data_16bit, i + 1, pressure, copy_pressure, 250);
                    Array.Copy(data_16bit, i - 250, flow, copy_flow, 250);
                    copy_flow += 250;
                    copy_pressure += 250;
                    flowCount++;


                }


                if (i > 500 && data_16bit[i - 251] == 32765 && data_16bit[i - 502] == 32766)
                {
                    Array.Copy(data_16bit, i, heater, copy_leak, 1);
                    copy_heater++;

                }


                if (i > 500 && data_16bit[i - 251] == 32763 && data_16bit[i - 502] == 32764)
                {
                    Array.Copy(data_16bit, i, leak, copy_leak, 1);
                    copy_leak++;
                }

            }

            #region Scaling data
            for (int w = 0; w < flow.Length; w++)
                flow[w] = (Int16)(flow[w] * 0.02);

            for (int w = 0; w < leak.Length; w++)
                leak[w] = (Int16)(leak[w] * 0.02);

            for (int w = 0; w < heater.Length; w++)
                heater[w] = (Int16)(heater[w] * 0.01);
            #endregion



            #endregion



            #region StepTwo

            UInt16 T1 = (UInt16)parameters[58];
            UInt16 T2 = (UInt16)parameters[57];
            UInt16 T3 = (UInt16)parameters[56];
            UInt16 T4 = (UInt16)parameters[55];

            UInt32 CH1 = (UInt32)T1;
            UInt32 CH2 = (UInt32)T2;
            UInt32 CH3 = (UInt32)T3;
            UInt32 CH4 = (UInt32)T4;

            UInt32 RES1 = (CH2 << 16) | CH1;
            UInt32 RES2 = (CH4 << 16) | CH3;

            UInt64 startDate = RES1 + RES2;
            UInt64 days_elapsed = startDate / 86400;
            UInt64 RemindSecound = startDate % 86400;
            UInt64 hour = RemindSecound / 3600;
            RemindSecound = Convert.ToUInt32(RemindSecound % 3600);
            UInt64 minutes = (RemindSecound / 60);
            UInt64 seconds = (RemindSecound % 60);
            UInt64 LoopYear = days_elapsed / 1460;
            UInt64 year = (days_elapsed - LoopYear) / 365;
            UInt64 Mounth = 0;
            UInt64 Day = 0;
            UInt64 RemindYearDay = days_elapsed - (year * 365) - (year / 4);
            if (RemindYearDay > 186)
            {
                Mounth = ((RemindYearDay - 186) / 30) + 6;
                Day = (RemindYearDay - 186) % 30;
            }
            else
            {
                Mounth = RemindYearDay / 31;
                Day = RemindYearDay % 31;
            }

            UInt64 currentYear = (year + 1396);
            UInt64 currentMonth = (Mounth + 1);
            UInt64 currentDaye = (Day + 1);
            #endregion

            var ddd = ((pressure.Length / 2400 / 60));
            var res = new StepOneModule();

            res.Parameters = string.Join(",", parameters);
            res.Length = len;
            res.Pressure = string.Join(",", pressure.ToArray());
            res.Flow = string.Join(",", flow.ToArray());
            res.Heater = string.Join(",", heater.ToArray());
            res.Leak = string.Join(",", leak.ToArray());
            res.Day = currentDaye.ToString();
            res.Year = currentYear.ToString();
            res.Month = currentMonth.ToString();
            res.Hour = hour.ToString();
            res.Minutes = minutes.ToString();
            res.StrarTime = hour.ToString() + ":" + minutes.ToString();
            res.EndTime = (flowCount * 250) / 72000 + ":" + ((flowCount * 250) % 72000) / 1200;
            res.Name = currentYear.ToString() + "/" + currentMonth.ToString() + "/" + currentDaye.ToString() + "/" + hour.ToString() + "/" + minutes.ToString();
            res.FileId = fileId;
            return res;
        }
    }
}
