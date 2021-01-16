//using Bipap.DAL.Models;
//using Bipap.Service.Functionality;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UI.Models;

//namespace UI.Functionality
//{
//    public static class HelperMethod
//    {
//        public static string GenerateRandomCode()
//        {
//            Random rnd = new Random();
//            return rnd.Next(1000, 9999).ToString();
//        }
//        public static int GetDayCountFromStepOneModules(List<StepOneModule> stepOneModules)
//        {
//            if (stepOneModules.Count() == 0)
//                return 0;
//            var dayCount = 0;
//            var sessionGroupYear = stepOneModules.GroupBy(x => x.Year).OrderBy(x => x.Key).ToList();
//            foreach (var sesstionYear in sessionGroupYear)
//            {
//                var sessionGroupMonth = sesstionYear.GroupBy(x => x.Month).OrderBy(x => x.Key).ToList();
//                foreach (var sessionMonth in sessionGroupMonth)
//                {
//                    var sessionGroupDay = sessionMonth.GroupBy(x => x.Day).ToList();
//                    //isFirstLabel = true;
//                    foreach (var sessions in sessionGroupDay)
//                    {
//                        dayCount++;
//                    }
//                }
//            }
//            return dayCount;
//        }

//        //public static ModuleStepTwoResult AnalizeStepOneModule(StepOneModule stepOneModule)
//        //{
//        //    var moduleStepTwoResult = stepOneModule.ToModel();

//        //    #region minute_vAndbpm
//        //    var i_sum = 0;
//        //    var e_sum = 0;
//        //    moduleStepTwoResult.minute_v = new int[stepOneModule.Flow.Length / 1200];
//        //    moduleStepTwoResult.bpm = new int[stepOneModule.Flow.Length / 1200];


//        //    int bpm_count = 0;

//        //    for (int i = 0; i < stepOneModule.Flow.Length; i += 1200)
//        //    {

//        //        for (int t = i; t < i + 1200; t++)
//        //        {

//        //            if (t < stepOneModule.Pressure.Length - 1 && stepOneModule.Flow[t] >= 0 && stepOneModule.Pressure[t] % 2 == 0)
//        //                i_sum += stepOneModule.Flow[t];


//        //            if (t < stepOneModule.Pressure.Length - 1 && stepOneModule.Flow[t] < 0 && stepOneModule.Pressure[t] % 2 == 1)
//        //                e_sum += stepOneModule.Flow[t];

//        //            if (t < stepOneModule.Pressure.Length - 1 && stepOneModule.Pressure[t] % 2 == 0 && stepOneModule.Pressure[t + 1] % 2 == 1)
//        //                bpm_count++;
//        //        }

//        //        if (i != 0 && (i / 1200) < moduleStepTwoResult.minute_v.Length)
//        //        {
//        //            moduleStepTwoResult.bpm[i / 1200] = bpm_count;
//        //            moduleStepTwoResult.minute_v[i / 1200] = (int)(((i_sum + (-e_sum)) / 2) * 0.8);
//        //            i_sum = 0;
//        //            e_sum = 0;
//        //            bpm_count = 0;
//        //        }

//        //        if (i == 0)
//        //        {
//        //            moduleStepTwoResult.bpm[0] = bpm_count;
//        //            moduleStepTwoResult.minute_v[0] = (int)(((i_sum + (-e_sum)) / 2) * 0.8);
//        //            i_sum = 0;
//        //            e_sum = 0;
//        //            bpm_count = 0;
//        //        }




//        //    }
//        //    #endregion

//        //    #region tidal_volume

//        //    List<int> tidal_volume = new List<int>();
//        //    i_sum = 0;
//        //    e_sum = 0;


//        //    for (int x = 0; x < stepOneModule.Pressure.Length - 1; x++)
//        //    {
//        //        if (stepOneModule.Flow[x] >= 0 && stepOneModule.Pressure[x] % 2 == 0)
//        //            i_sum += stepOneModule.Flow[x];


//        //        if (stepOneModule.Flow[x] < 0 && stepOneModule.Pressure[x] % 2 == 1)
//        //            e_sum += stepOneModule.Flow[x];


//        //        if (stepOneModule.Pressure[x] % 2 == 1 && stepOneModule.Pressure[x + 1] % 2 == 0 && i_sum == 0)
//        //            e_sum = 0;

//        //        if (stepOneModule.Pressure[x] % 2 == 1 && stepOneModule.Pressure[x + 1] % 2 == 0 && i_sum != 0)
//        //        {
//        //            tidal_volume.Add((int)(((i_sum + (-e_sum)) / 2) * 0.8));
//        //            i_sum = 0;
//        //            e_sum = 0;

//        //        }


//        //    }

//        //    moduleStepTwoResult.tidal_volume = new int[tidal_volume.Count];
//        //    moduleStepTwoResult.tidal_volume = tidal_volume.ToArray();


//        //    #endregion

//        //    #region appnea_indexes

//        //    //int noa = 0;
//        //    int appnea = 0;
//        //    int h_appnea = 0;
//        //    int count = 0;
//        //    int AppneaThreshold = 20;
//        //    List<int> H_appnea_indexes = new List<int>();

//        //    for (int m = 0; m < stepOneModule.Flow.Length - 1; m++)
//        //    {
//        //        if (Math.Abs(stepOneModule.Flow[m]) < AppneaThreshold)
//        //            count++;

//        //        if (Math.Abs(stepOneModule.Flow[m]) < AppneaThreshold && Math.Abs(stepOneModule.Flow[m + 1]) > AppneaThreshold)
//        //        {
//        //            if ((count / 20) > 10 && (count / 20) < 20)
//        //                appnea++;


//        //            if ((count / 20) > 20)
//        //                h_appnea++;

//        //            count = 0;
//        //        }



//        //    }

//        //    //noa = h_appnea +appnea ;
//        //    moduleStepTwoResult.AppneAndHApnne = appnea + h_appnea;
//        //    //moduleStepTwoResult.appnea_indexes = new int[noa];

//        //    #endregion

//        //    #region ScalingPressureData

//        //    for (int w = 0; w < moduleStepTwoResult.Pressure.Length; w++)
//        //        moduleStepTwoResult.Pressure[w] = (Int16)(moduleStepTwoResult.Pressure[w] * 0.002);



//        //    #endregion

//        //    #region MedianAndPresentModel
//        //    moduleStepTwoResult.minute_vMinimum = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Minimum;
//        //    moduleStepTwoResult.minute_vMedian = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Median;
//        //    moduleStepTwoResult.minute_vPresent = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Present;
//        //    moduleStepTwoResult.bpmMedian = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Median;
//        //    moduleStepTwoResult.bpmPresent = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Present;
//        //    moduleStepTwoResult.bpmMinimum = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Minimum;
//        //    moduleStepTwoResult.tidal_volumeMinimum = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Minimum;
//        //    moduleStepTwoResult.tidal_volumeMedian = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Median;
//        //    moduleStepTwoResult.tidal_volumePresent = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Present;
//        //    //moduleStepTwoResult.appnea_indexesMedian = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Median;
//        //    //moduleStepTwoResult.appnea_indexesPresent = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Present;
//        //    //moduleStepTwoResult.appnea_indexesMinimum = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Minimum;

//        //    moduleStepTwoResult.LeakMedian = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Median;
//        //    moduleStepTwoResult.LeakPresent = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Present;
//        //    moduleStepTwoResult.LeakMinimum = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Minimum;

//        //    moduleStepTwoResult.HeaterMedian = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Median;
//        //    moduleStepTwoResult.HeaterPresent = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Present;
//        //    moduleStepTwoResult.HeaterMinimum = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Minimum;
//        //    #endregion

//        //    moduleStepTwoResult.ModeStr = moduleStepTwoResult.Mode.GetModel();
//        //    return moduleStepTwoResult;
//        //}
//        //public static ModuleStepTwoResult NewAnalizeStepOneModule(StepOneModule sessionStepOneModel)
//        //{
//        //    var itemErroe1 = 0;
//        //    var itemError2 = 0;
//        //    var moduleStepTwoResult = sessionStepOneModel.ToModel();
//        //    try
//        //    {

//        //        #region minute_vAndbpm
//        //        var i_sum = 0;
//        //        var e_sum = 0;
//        //        moduleStepTwoResult.minute_v = new int[sessionStepOneModel.Flow.Length / 1200];
//        //        moduleStepTwoResult.bpm = new int[sessionStepOneModel.Flow.Length / 1200];


//        //        int bpm_count = 0;

//        //        for (int i = 0; i < sessionStepOneModel.Flow.Length; i += 1200)
//        //        {

//        //            for (int t = i; t < i + 1200; t++)
//        //            {

//        //                if (t < sessionStepOneModel.Pressure.Length - 1 && sessionStepOneModel.Flow[t] >= 0 && sessionStepOneModel.Pressure[t] % 2 == 0)
//        //                    i_sum += sessionStepOneModel.Flow[t];


//        //                if (t < sessionStepOneModel.Pressure.Length - 1 && sessionStepOneModel.Flow[t] < 0 && sessionStepOneModel.Pressure[t] % 2 == 1)
//        //                    e_sum += sessionStepOneModel.Flow[t];

//        //                if (t < sessionStepOneModel.Pressure.Length - 1 && sessionStepOneModel.Pressure[t] % 2 == 0 && sessionStepOneModel.Pressure[t + 1] % 2 == 1)
//        //                    bpm_count++;
//        //            }

//        //            if (i != 0 && (i / 1200) < moduleStepTwoResult.minute_v.Length)
//        //            {
//        //                moduleStepTwoResult.bpm[i / 1200] = bpm_count;
//        //                moduleStepTwoResult.minute_v[i / 1200] = (int)(((i_sum + (-e_sum)) / 2) * 0.8);
//        //                i_sum = 0;
//        //                e_sum = 0;
//        //                bpm_count = 0;
//        //            }

//        //            if (i == 0)
//        //            {
//        //                moduleStepTwoResult.bpm[0] = bpm_count;
//        //                moduleStepTwoResult.minute_v[0] = (int)(((i_sum + (-e_sum)) / 2) * 0.8);
//        //                i_sum = 0;
//        //                e_sum = 0;
//        //                bpm_count = 0;
//        //            }




//        //        }
//        //        #endregion

//        //        #region tidal_volume

//        //        List<int> tidal_volume = new List<int>();
//        //        i_sum = 0;
//        //        e_sum = 0;


//        //        for (int x = 0; x < sessionStepOneModel.Pressure.Length - 1; x++)
//        //        {
//        //            if (sessionStepOneModel.Flow[x] >= 0 && sessionStepOneModel.Pressure[x] % 2 == 0)
//        //                i_sum += sessionStepOneModel.Flow[x];


//        //            if (sessionStepOneModel.Flow[x] < 0 && sessionStepOneModel.Pressure[x] % 2 == 1)
//        //                e_sum += sessionStepOneModel.Flow[x];


//        //            if (sessionStepOneModel.Pressure[x] % 2 == 1 && sessionStepOneModel.Pressure[x + 1] % 2 == 0 && i_sum == 0)
//        //                e_sum = 0;

//        //            if (sessionStepOneModel.Pressure[x] % 2 == 1 && sessionStepOneModel.Pressure[x + 1] % 2 == 0 && i_sum != 0)
//        //            {
//        //                tidal_volume.Add((int)(((i_sum + (-e_sum)) / 2) * 0.8));
//        //                i_sum = 0;
//        //                e_sum = 0;

//        //            }


//        //        }

//        //        moduleStepTwoResult.tidal_volume = new int[tidal_volume.Count];
//        //        moduleStepTwoResult.tidal_volume = tidal_volume.ToArray();


//        //        #endregion

//        //        #region appnea_indexes

//        //        //int noa = 0;
//        //        int appnea = 0;
//        //        int h_appnea = 0;
//        //        int count = 0;
//        //        int AppneaThreshold = 20;


//        //        for (int m = 0; m < sessionStepOneModel.Flow.Length - 1; m++)
//        //        {
//        //            if (Math.Abs(sessionStepOneModel.Flow[m]) < AppneaThreshold)
//        //                count++;

//        //            if (Math.Abs(sessionStepOneModel.Flow[m]) < AppneaThreshold && Math.Abs(sessionStepOneModel.Flow[m + 1]) > AppneaThreshold)
//        //            {
//        //                if ((count / 20) > 10 && (count / 20) < 20)
//        //                    appnea++;


//        //                if ((count / 20) > 20)
//        //                    h_appnea++;

//        //                count = 0;
//        //            }



//        //        }

//        //        //noa = h_appnea +appnea ;
//        //        moduleStepTwoResult.Appne = appnea;
//        //        moduleStepTwoResult.HApne = h_appnea;
//        //        moduleStepTwoResult.AppneAndHApnne = appnea + h_appnea;
//        //        //moduleStepTwoResult.appnea_indexes = new int[noa];

//        //        #endregion


//        //        #region ScalingPressureData

//        //        for (int w = 0; w < moduleStepTwoResult.Pressure.Length; w++)
//        //            moduleStepTwoResult.Pressure[w] = (Int16)(moduleStepTwoResult.Pressure[w] * 0.002);



//        //        #endregion

//        //        #region MedianAndPresentModel
//        //        moduleStepTwoResult.minute_vMinimum = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Minimum;
//        //        moduleStepTwoResult.minute_vMedian = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.minute_vPresent = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Present;
//        //        moduleStepTwoResult.bpmMedian = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.bpmPresent = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Present;
//        //        moduleStepTwoResult.bpmMinimum = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Minimum;
//        //        moduleStepTwoResult.tidal_volumeMinimum = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Minimum;
//        //        moduleStepTwoResult.tidal_volumeMedian = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.tidal_volumePresent = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Present;
//        //        //moduleStepTwoResult.appnea_indexesMedian = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Median;
//        //        //moduleStepTwoResult.appnea_indexesPresent = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Present;
//        //        //moduleStepTwoResult.appnea_indexesMinimum = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Minimum;

//        //        moduleStepTwoResult.LeakMedian = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.LeakPresent = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Present;
//        //        moduleStepTwoResult.LeakMinimum = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Minimum;

//        //        moduleStepTwoResult.HeaterMedian = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.HeaterPresent = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Present;
//        //        moduleStepTwoResult.HeaterMinimum = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Minimum;
//        //        #endregion

//        //        #region IEration
//        //        int i_time = 0;
//        //        int e_time = 0;
//        //        List<int> i_ratio = new List<int>();
//        //        List<int> e_ratio = new List<int>();
//        //        int[] I_ratio;
//        //        int[] E_ratio;



//        //        for (int y = 0; y < sessionStepOneModel.Pressure.Length - 1; y++)
//        //        {
//        //            if (sessionStepOneModel.Pressure[y] % 2 == 0)
//        //                i_time++;
//        //            if (sessionStepOneModel.Pressure[y] % 2 == 1)
//        //                e_time++;
//        //            if (sessionStepOneModel.Pressure[y] % 2 == 1 && sessionStepOneModel.Pressure[y + 1] % 2 == 0 && i_time == 0)
//        //                e_time = 0;

//        //            if (sessionStepOneModel.Pressure[y] % 2 == 1 && sessionStepOneModel.Pressure[y + 1] % 2 == 0 && i_time != 0)
//        //            {
//        //                i_ratio.Add((i_time * 100) / (e_time + i_time));
//        //                e_ratio.Add((e_time * 100) / (e_time + i_time));
//        //                i_time = 0;
//        //                e_time = 0;

//        //            }
//        //        }


//        //        I_ratio = new int[i_ratio.Count];
//        //        E_ratio = new int[e_ratio.Count];
//        //        I_ratio = i_ratio.ToArray();
//        //        E_ratio = e_ratio.ToArray();
//        //        moduleStepTwoResult.I_ratioMedian = I_ratio.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.I_ratioMinimum = I_ratio.ToMedianAndPresentModel().Minimum;
//        //        moduleStepTwoResult.I_ratioPresent = I_ratio.ToMedianAndPresentModel().Present;

//        //        moduleStepTwoResult.E_ratioMedian = E_ratio.ToMedianAndPresentModel().Median;
//        //        moduleStepTwoResult.E_ratioMinimum = E_ratio.ToMedianAndPresentModel().Minimum;
//        //        moduleStepTwoResult.E_ratioPresent = E_ratio.ToMedianAndPresentModel().Present;
//        //        #endregion

//        //        moduleStepTwoResult.ModeStr = moduleStepTwoResult.Mode.GetModel();
//        //        return moduleStepTwoResult;
//        //    }
//        //    catch(Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public static ModuleStepTwoResult ToModel(this StepOneModule model)
//        //{
//        //    if (model == null)
//        //        return null;
//        //    //return new ModuleStepTwoResult
//        //    //{
//        //    //    FlagStart = (Int16)model.Parameters[0],
//        //    //    Initial_CPAP = (Int16)model.Parameters[1],
//        //    //    Pressure_CPAP = (Int16)(model.Parameters[2] / 10),
//        //    //    Start_Ramp = (Int16)(model.Parameters[3] / 10),
//        //    //    Flag_Epr = (Int16)model.Parameters[4],
//        //    //    EPR = (Int16)(model.Parameters[5] / 10),
//        //    //    EPR_Ramp = (Int16)(model.Parameters[6] / 60000),
//        //    //    Mode = (Int16)model.Parameters[7],
//        //    //    Delta_T_P = (Int16)(model.Parameters[8] / 10),
//        //    //    Delta_T_N = (Int16)(model.Parameters[9] / 10),
//        //    //    Delta_P = (Int16)(model.Parameters[10] / 10),
//        //    //    Apap_Min = (Int16)(model.Parameters[11] / 10),
//        //    //    Apap_Max = (Int16)(model.Parameters[12] / 10),
//        //    //    Flag_Apnea = (Int16)model.Parameters[13],
//        //    //    IPAP = (Int16)(model.Parameters[14] / 10),
//        //    //    EPAP = (Int16)(model.Parameters[15] / 10),
//        //    //    SENS_I = (Int16)model.Parameters[16],
//        //    //    SENS_E = (Int16)model.Parameters[17],
//        //    //    Slope = (Int16)model.Parameters[18],
//        //    //    ARAMP = (Int16)model.Parameters[19],
//        //    //    Bti = (Int16)(model.Parameters[20] / 10),
//        //    //    Bstart_Delay = (Int16)(model.Parameters[21] / 10),
//        //    //    RR = (Int16)model.Parameters[22],
//        //    //    Ti = (Int16)(model.Parameters[23] / 10),
//        //    //    Ti_Min = (Int16)(model.Parameters[24] / 10),
//        //    //    Ti_Max = (Int16)(model.Parameters[25] / 10),
//        //    //    IPAP_Min = (Int16)(model.Parameters[26] / 10),
//        //    //    IPAP_Max = (Int16)(model.Parameters[27] / 10),
//        //    //    VT = (Int16)model.Parameters[28],
//        //    //    IE = (Int16)model.Parameters[29],
//        //    //    ST = (Int16)model.Parameters[30],
//        //    //    VT_Only_Read = (Int16)model.Parameters[31],
//        //    //    Toatal_Leak = (Int16)(model.Parameters[32] * 0.02),
//        //    //    Mask_leak = (Int16)(model.Parameters[33] * 0.02),
//        //    //    Minute_Ventilation = (Int16)(model.Parameters[34] / 10),
//        //    //    LastTI = (Int16)(model.Parameters[35] / 100),
//        //    //    LastTE = (Int16)(model.Parameters[36] / 100),
//        //    //    HeaterSetPoint = (Int16)(model.Parameters[37] / 100),
//        //    //    HeaterTemp = (Int16)(model.Parameters[38] / 100),
//        //    //    Motor = (Int16)model.Parameters[39],
//        //    //    Comfort = (Int16)model.Parameters[40],
//        //    //    UID0 = (Int16)model.Parameters[41],
//        //    //    UID1 = (Int16)model.Parameters[42],
//        //    //    UID2 = (Int16)model.Parameters[43],
//        //    //    UID3 = (Int16)model.Parameters[44],
//        //    //    UID4 = (Int16)model.Parameters[45],
//        //    //    UID5 = (Int16)model.Parameters[46],
//        //    //    Version = (Int16)model.Parameters[47],
//        //    //    UUID0 = (Int16)model.Parameters[48],
//        //    //    UUID1 = (Int16)model.Parameters[49],
//        //    //    UUID2 = (Int16)model.Parameters[50],
//        //    //    UUID3 = (Int16)model.Parameters[51],
//        //    //    UUID4 = (Int16)model.Parameters[52],
//        //    //    UUID5 = (Int16)model.Parameters[53],
//        //    //    UVesrion = (Int16)model.Parameters[54],
//        //    //    RTCH = (Int16)model.Parameters[55],
//        //    //    RTCL = (Int16)model.Parameters[56],
//        //    //    RTC_OFS_GRE_H = (Int16)model.Parameters[57],
//        //    //    RTC_OFS_GRE_L = (Int16)model.Parameters[58],
//        //    //    Pressure = model.Pressure.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),// Convert.ToInt16(model.Pressure.Split(',')),
//        //    //    Flow = model.Flow.Split(',').Select(x => Convert.ToInt16(x)).ToArray(), // Convert.ToInt16(model.Flow),
//        //    //    Heater = model.Heater.Split(',').Select(x => Convert.ToInt16(x)).ToArray(), // Convert.ToInt16(model.Heater),
//        //    //    Leak = model.Leak.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
//        //    //    Year = model.Year,
//        //    //    Month = model.Month,
//        //    //    Day = model.Day,
//        //    //    StrarTime = model.StrarTime,
//        //    //    EndTime = model.EndTime,
//        //    //    Length = model.Length
//        //    //};
//        //    return new ModuleStepTwoResult
//        //    {
//        //        FlagStart = (Int16)model.Parameters[0],
//        //        Initial_CPAP = (Int16)model.Parameters[1],
//        //        Pressure_CPAP = (Int16)(model.Parameters[2] / 10),
//        //        Start_Ramp = (Int16)(model.Parameters[3] / 10),
//        //        Flag_Epr = (Int16)model.Parameters[4],
//        //        EPR = (Int16)(model.Parameters[5] / 10),
//        //        EPR_Ramp = (Int16)(model.Parameters[6] / 60000),
//        //        Mode = (Int16)model.Parameters[7],
//        //        Delta_T_P = (Int16)(model.Parameters[8] / 10),
//        //        Delta_T_N = (Int16)(model.Parameters[9] / 10),
//        //        Delta_P = (Int16)(model.Parameters[10] / 10),
//        //        Apap_Min = (Int16)(model.Parameters[11] / 10),
//        //        Apap_Max = (Int16)(model.Parameters[12] / 10),
//        //        Flag_Apnea = (Int16)model.Parameters[13],
//        //        IPAP = (Int16)(model.Parameters[14] / 10),
//        //        EPAP = (Int16)(model.Parameters[15] / 10),
//        //        SENS_I = (Int16)model.Parameters[16],
//        //        SENS_E = (Int16)model.Parameters[17],
//        //        Slope = (Int16)model.Parameters[18],
//        //        ARAMP = (Int16)model.Parameters[19],
//        //        Bti = (Int16)(model.Parameters[20] / 10),
//        //        Bstart_Delay = (Int16)(model.Parameters[21] / 10),
//        //        RR = (Int16)model.Parameters[22],
//        //        Ti = (Int16)(model.Parameters[23] / 10),
//        //        Ti_Min = (Int16)(model.Parameters[24] / 10),
//        //        Ti_Max = (Int16)(model.Parameters[25] / 10),
//        //        IPAP_Min = (Int16)(model.Parameters[26] / 10),
//        //        IPAP_Max = (Int16)(model.Parameters[27] / 10),
//        //        VT = (Int16)model.Parameters[28],
//        //        IE = (Int16)model.Parameters[29],
//        //        ST = (Int16)model.Parameters[30],
//        //        VT_Only_Read = (Int16)model.Parameters[31],
//        //        Toatal_Leak = (Int16)(model.Parameters[32] * 0.02),
//        //        Mask_leak = (Int16)(model.Parameters[33] * 0.02),
//        //        Minute_Ventilation = (Int16)(model.Parameters[34] / 10),
//        //        LastTI = (Int16)(model.Parameters[35] / 100),
//        //        LastTE = (Int16)(model.Parameters[36] / 100),
//        //        HeaterSetPoint = (Int16)(model.Parameters[37] / 100),
//        //        HeaterTemp = (Int16)(model.Parameters[38] / 100),
//        //        Motor = (Int16)model.Parameters[39],
//        //        Comfort = (Int16)model.Parameters[40],
//        //        UID0 = (Int16)model.Parameters[41],
//        //        UID1 = (Int16)model.Parameters[42],
//        //        UID2 = (Int16)model.Parameters[43],
//        //        UID3 = (Int16)model.Parameters[44],
//        //        UID4 = (Int16)model.Parameters[45],
//        //        UID5 = (Int16)model.Parameters[46],
//        //        Version = (Int16)model.Parameters[47],
//        //        UUID0 = (Int16)model.Parameters[48],
//        //        UUID1 = (Int16)model.Parameters[49],
//        //        UUID2 = (Int16)model.Parameters[50],
//        //        UUID3 = (Int16)model.Parameters[51],
//        //        UUID4 = (Int16)model.Parameters[52],
//        //        UUID5 = (Int16)model.Parameters[53],
//        //        UVesrion = (Int16)model.Parameters[54],
//        //        RTCH = (Int16)model.Parameters[55],
//        //        RTCL = (Int16)model.Parameters[56],
//        //        RTC_OFS_GRE_H = (Int16)model.Parameters[57],
//        //        RTC_OFS_GRE_L = (Int16)model.Parameters[58],
//        //        Pressure = model.Pressure.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),// model.Pressure,
//        //        Flow = model.Flow.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
//        //        Heater = model.Heater.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
//        //        Leak = model.Leak.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
//        //        Year = model.Year,
//        //        Month = model.Month,
//        //        Day = model.Day,
//        //        StrarTime = model.StrarTime,
//        //        EndTime = model.EndTime,
//        //        Length = model.Length
//        //    };
//        //}
//        public static MedianAndPresentModel ToMedianAndPresentModel(this int[] array)
//        {
//            Array.Sort(array);

//            decimal[] median_95percent = new decimal[2];

//            if (array.Length > 20)
//            {
//                if (array.Length % 2 == 1)
//                    median_95percent[0] = (decimal)array[array.Length / 2];


//                else
//                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
//                int index = (int)Math.Round((double)(array.Length * 0.95));
//                median_95percent[1] = array[index - 1];
//            }


//            return new MedianAndPresentModel
//            {
//                Median = median_95percent[0],
//                Present = median_95percent[1]
//            };
//        }
//        public static MedianAndPresentModel ToMedianAndPresentModel(this Int16[] array)
//        {
//            Array.Sort(array);

//            decimal[] median_95percent = new decimal[2];

//            if (array.Length > 20)
//            {
//                if (array.Length % 2 == 1)
//                    median_95percent[0] = (decimal)array[array.Length / 2];


//                else
//                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
//                int index = (int)Math.Round((double)(array.Length * 0.95));
//                median_95percent[1] = array[index - 1];
//            }


//            return new MedianAndPresentModel
//            {
//                Median = median_95percent[0],
//                Present = median_95percent[1],
//                Minimum = array.Min()
//            };
//        }
//        public static MedianAndPresentModel ToMedianAndPresentModel(this decimal[] array)
//        {
//            Array.Sort(array);

//            decimal[] median_95percent = new decimal[2];

//            if (array.Length > 20)
//            {
//                if (array.Length % 2 == 1)
//                    median_95percent[0] = (decimal)array[array.Length / 2];


//                else
//                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
//                int index = (int)Math.Round((double)(array.Length * 0.95));
//                median_95percent[1] = array[index - 1];
//            }


//            return new MedianAndPresentModel
//            {
//                Median = median_95percent[0],
//                Present = median_95percent[1],
//                Minimum = array.Min()
//            };
//        }
//        private static string GetTime(DateTime dateTime)
//        {
//            return dateTime.ToString("hh:mm tt");
//        }
//        private static string ToStringModel(List<decimal> model)
//        {
//            var res = "";
//            foreach (var item in model)
//            {
//                res = res + item + ",";
//            }
//            return res.Remove(res.Length - 1);
//        }
//        private static string GetModel(this short inputModel)
//        {
//            if (inputModel == 6)
//                return "Avaps";
//            if (inputModel == 5)
//                return "Bipap ST";
//            if (inputModel == 4)
//                return "Bipap T";
//            if (inputModel == 3)
//                return "Bipap S";
//            if (inputModel == 2)
//                return "Apap";
//            if (inputModel == 1)
//                return "Cpap";
//            return "";
//        }
//        private static int GetMinutes(string time)
//        {
//            var ts = new TimeSpan(Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), 0);
//            return Convert.ToInt32(ts.TotalMinutes);
//        }
//        private static int GetCount(List<ModuleStepTwoResult> model, string date)
//        {
//            var count = 0;
//            var face = 0;
//            var director = 0;
//            foreach (var item in model)
//            {
//                if (item.Year + "/" + item.Month + "/" + item.Day == date)
//                {
//                    var size = GetMinutes(item.EndTime) - GetMinutes(item.StrarTime);
//                    var ahi = 0;
//                    for (int i = 0; i < item.appnea_indexes.Length; i++)
//                    {
//                        ahi += item.appnea_indexes[i];
//                    }
//                    face += ahi * size;
//                    director += size;
//                }
//                count = face / director;
//            }
//            return count;
//        }
//        private static string ToAhiModel(List<ModuleStepTwoResult> model)
//        {
//            var res = "";
//            var ahiList = new List<AhiModel>();
//            foreach (var item in model)
//            {
//                if (!ahiList.Any(x => x.Date == item.Year + "/" + item.Month + "/" + item.Day))
//                {
//                    var date = item.Year + "/" + item.Month + "/" + item.Day;
//                    res += date + "," + GetCount(model, date) + ";";
//                }
//            }
//            return res.Remove(res.Length - 1);

//        }
//        private static string ToStingModel(List<TotalUsage> model)
//        {
//            var res = "";
//            foreach (var item in model)
//            {
//                res = res + item.Date + "," + item.Count + ";";
//            }
//            return res.Remove(res.Length - 1);
//        }
//        private static List<TotalUsage> GetTottalUsage(List<ModuleStepTwoResult> model)
//        {
//            var res = new List<TotalUsage>();
//            var dateList = new List<string>();
//            foreach (var item in model)
//            {
//                if (!dateList.Any(x => x == item.Year + "/" + item.Month + "/" + item.Day))
//                {
//                    var newDate = item.Year + "/" + item.Month + "/" + item.Day;
//                    dateList.Add(newDate);

//                    res.Add(new TotalUsage
//                    {
//                        Date = newDate,
//                        Count = Convert.ToDecimal(Convert.ToDecimal(model.Where(x => x.Year + "/" + x.Month + "/" + x.Day == newDate).Sum(y => y.Length)) / Convert.ToDecimal(288000))
//                    });

//                }
//            }
//            return res;
//        }
//        private static List<Int16[]> GetListInDay(List<ModuleStepTwoResult> model, string selectItem)
//        {
//            var res = new List<Int16[]>();
//            var dateList = new List<string>();
//            foreach (var item in model)
//            {
//                if (!dateList.Any(x => x == item.Year + "/" + item.Month + "/" + item.Day))
//                {
//                    var newDate = item.Year + "/" + item.Month + "/" + item.Day;
//                    dateList.Add(newDate);
//                    if (selectItem == "Ipap")
//                    {
//                        var ipapArray = model.Where(x => x.Year + "/" + x.Month + "/" + x.Day == newDate).Select(y => y.IPAP).ToArray();
//                        res.Add(ipapArray);
//                    }
//                    else
//                    {
//                        var ipapArray = model.Where(x => x.Year + "/" + x.Month + "/" + x.Day == newDate).Select(y => y.EPAP).ToArray();
//                        res.Add(ipapArray);
//                    }

//                }
//            }
//            return res;
//        }
//        //public static ReportModel ToReportModel(this List<ModuleStepTwoResult> stepTwoResult, Patient patient)
//        //{
//        //    if (stepTwoResult.Count() == 0)
//        //        return null;
//        //    var ipapArray = GetListInDay(stepTwoResult, "Ipap");
//        //    var epapArray = GetListInDay(stepTwoResult, "Epap");

//        //    return new ReportModel
//        //    {
//        //        ChIPAP = true,
//        //        ChEPAP = true,
//        //        ChLEAK = true,
//        //        ChTidalVolume = true,
//        //        ChMinuteVentilation = true,
//        //        ChRespiratoryRate = true,
//        //        ChAHIAI = true,
//        //        ChUsage = true,
//        //        ChFlow = true,
//        //        ChPressure = true,
//        //        PatientName = patient.FullName,
//        //        PatientId = patient.PersonalId,
//        //        DateOfBirth = patient.DateOfBirth.ToString(),
//        //        Gender = "" /*patient.Gender?.Title*/,
//        //        Address = patient.Address,
//        //        Age = patient.Age.ToString(),
//        //        TelephoneNumbers = patient.Mobile,
//        //        Email = patient.Email,
//        //        InsuranceCarrier = "",
//        //        IPAPMedian = stepTwoResult.Select(x => x.IPAP).ToArray().ToMedianAndPresentModel().Median.ToString(),
//        //        IPAPMinimum = stepTwoResult.Select(x => x.IPAP).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
//        //        IPAPPercentile = stepTwoResult.Select(x => x.IPAP).ToArray().ToMedianAndPresentModel().Present.ToString(),
//        //        LeakMedian = stepTwoResult.Select(x => x.Leak).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
//        //        LeakMinimum = stepTwoResult.Select(x => x.Leak).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
//        //        LeakPercentile = stepTwoResult.Select(x => x.Leak).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
//        //        TidalVolumeMedian = stepTwoResult.Select(x => x.tidal_volume).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
//        //        TidalVolumeMinimum = stepTwoResult.Select(x => x.tidal_volume).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
//        //        TidalVolumePercentile = stepTwoResult.Select(x => x.tidal_volume).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
//        //        MinuteVentilationMedian = stepTwoResult.Select(x => x.minute_v).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
//        //        MinuteVentilationPercentile = stepTwoResult.Select(x => x.minute_v).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
//        //        MinuteVentilationMinimum = stepTwoResult.Select(x => x.minute_v).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
//        //        RespiratoryRateMedian = stepTwoResult.Select(x => x.bpm).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
//        //        RespiratoryRateMinimum = stepTwoResult.Select(x => x.bpm).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
//        //        RespiratoryRatePercentile = stepTwoResult.Select(x => x.bpm).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
//        //        ApneaIndex = "",
//        //        HypopneaIndex = "",
//        //        AHI = "",
//        //        IERatioMedian = "",
//        //        IERatioMinimum = "",
//        //        IERatioPercentile = "",
//        //        InspiratoryTimeMedian = "",
//        //        InspiratoryTimeMinimum = "",
//        //        InspiratoryTimePercentile = "",
//        //        AlveolarVentilationMedian = "",
//        //        AlveolarVentilationMinimum = "",
//        //        AlveolarVentilationPercentile = "",
//        //        IpapMedianArray = ToStringModel(ipapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Median).ToList()),
//        //        IpapPrecentileArray = ToStringModel(ipapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Present).ToList()),
//        //        IpapMinimumArray = ToStringModel(ipapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Minimum).ToList()),
//        //        EpapMedianArray = ToStringModel(epapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Median).ToList()),
//        //        EpapPrecentileArray = ToStringModel(epapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Present).ToList()),
//        //        EpapMinimumArray = ToStringModel(epapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Minimum).ToList()),
//        //        ReportedBy = "",
//        //        Time = GetTime(DateTime.Now),
//        //        LeakMinimumArray = ToStringModel(stepTwoResult.Select(x => x.Leak.ToMedianAndPresentModel()).Select(x => x.Minimum).ToList()),
//        //        LeakMedianArray = ToStringModel(stepTwoResult.Select(x => x.Leak.ToMedianAndPresentModel()).Select(x => x.Median).ToList()),
//        //        LeakPrecentileArray = ToStringModel(stepTwoResult.Select(x => x.Leak.ToMedianAndPresentModel()).Select(x => x.Present).ToList()),
//        //        TidalVolumeMinimumArray = ToStringModel(stepTwoResult.Select(x => x.tidal_volume.ToMedianAndPresentModel()).Select(x => x.Minimum).ToList()),
//        //        TidalVolumeMedianArray = ToStringModel(stepTwoResult.Select(x => x.tidal_volume.ToMedianAndPresentModel()).Select(x => x.Median).ToList()),
//        //        TidalVolumePrecentileArray = ToStringModel(stepTwoResult.Select(x => x.tidal_volume.ToMedianAndPresentModel()).Select(x => x.Present).ToList()),
//        //        MinuteVentilationMinimumArray = ToStringModel(stepTwoResult.Select(x => x.minute_vMinimum).ToList()),
//        //        MinuteVentilationMedianArray = ToStringModel(stepTwoResult.Select(x => x.minute_vMedian).ToList()),
//        //        MinuteVentilationPrecentileArray = ToStringModel(stepTwoResult.Select(x => x.minute_vPresent).ToList()),
//        //        RespiratoryRateMinimumArray = ToStringModel(stepTwoResult.Select(x => x.bpmMinimum).ToList()),
//        //        RespiratoryRateMedianArray = ToStringModel(stepTwoResult.Select(x => x.bpmMedian).ToList()),
//        //        RespiratoryRatePrecentileArray = ToStringModel(stepTwoResult.Select(x => x.bpmPresent).ToList()),
//        //        Ahi = ToAhiModel(stepTwoResult),
//        //        IsAvaps = stepTwoResult.Last().Mode == 6,
//        //        TotalUsage = ToStingModel(GetTottalUsage(stepTwoResult)),
//        //        TotalDays = GetTottalUsage(stepTwoResult).Count().ToString(),// stepTwoResult.Count().ToString(),
//        //        TotalHoursUsed = (Convert.ToDecimal(stepTwoResult.Sum(x => x.Length)) / Convert.ToDecimal(288000)).ToString("f2"),
//        //        AverageDailyUsed = ((Convert.ToDecimal(stepTwoResult.Sum(x => x.Length)) / Convert.ToDecimal(288000)) / Convert.ToDecimal(GetTottalUsage(stepTwoResult).Count())).ToString("F2")


//        //    };
//        //}
//        public static string GenerateHtml(this ReportModel model)
//        {
//            if (model == null)
//                return null;
//            var html = GetHeader(model) + GetPatientProfile(model)
//                + GetBreakPage() + GetBreakPage() + GetHeader(model) + GetStatistics(model) + GetBreakPage();

//            if (model.IsAvaps || true)
//            {
//                html = html + GetColumnChartContainer(model.IpapMinimumArray, model.IpapMedianArray, model.IpapPrecentileArray, "IpapContainer", "Ipap");
//                html = html + GetColumnChartContainer(model.EpapMinimumArray, model.EpapMedianArray, model.EpapPrecentileArray, "EpapContainer", "Epap");
//            }
//            if (model.ChLEAK || true)
//                html = html + GetColumnChartContainer(model.LeakMinimumArray, model.LeakMedianArray, model.LeakPrecentileArray, "leakContainer", "leak");
//            if (model.ChLEAK || true)
//                html = html + GetColumnChartContainer(model.TidalVolumeMinimumArray, model.TidalVolumeMedianArray, model.TidalVolumePrecentileArray, "TidalVolumeContainer", "Tidal Volume");
//            if (model.ChTidalVolume || true)
//                html = html + GetColumnChartContainer(model.TidalVolumeMinimumArray, model.TidalVolumeMedianArray, model.TidalVolumePrecentileArray, "TidalVolumeContainer", "Tidal Volume");
//            if (model.ChMinuteVentilation || true)
//                html = html + GetColumnChartContainer(model.MinuteVentilationMinimumArray, model.MinuteVentilationMedianArray, model.MinuteVentilationPrecentileArray, "MinuteVentilationContainer", "Minute Ventilation");
//            if (model.ChRespiratoryRate || true)
//                html = html + GetColumnChartContainer(model.RespiratoryRateMinimumArray, model.RespiratoryRateMedianArray, model.RespiratoryRatePrecentileArray, "RespiratoryRateContainer", "Respiratory Rate");
//            if (model.ChAHIAI || true)
//                html = html + GetColumnChartContainer(model.Ahi, "AhiContainer", "Ahi");
//            html = html + GetColumnChartContainer(model.TotalUsage, "TotalCotainer", "Total Usage");
//            return html;
//        }
//        private static string GetHeader(ReportModel model)
//        {
//            return $"<div style='width: 100%;float: left;border-bottom: 1px solid #dee2e6'><div style='width: 100%;float: left'><div style='width: 100%;float: left; margin-bottom: .25rem;margin-top: .25rem'><div style='display: flex; align-items: center'> <img src='data:image/jpeg;base64,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' style='width: 150px;float: left;'><div style='border-left: 1px solid #ccc;float: left;width: calc(100% - 150px);padding-left: 15px;'><ul style='float: left;width: calc(100% - 150px);padding-left: 15px;list-style: none'><li style='font-weight: bold; color: #545454;'>Name : <span style='color: #545454;font-weight: normal;'> {model.PatientName}</span></li><li style='font-weight: bold; color: #545454;'>patient ID : <span style='color: #545454;font-weight: normal;'>{model.PatientId}</span></li><li style='font-weight: bold; color: #545454;'>Date of birth : <span style='color: #545454;font-weight: normal;'>{model.DateOfBirth}</span></li><li style='font-weight: bold; color: #545454;'>Report prepared by : <span style='color: #545454;font-weight: normal;'>{model.ReportedBy}</span> on <span style='color: #545454;font-weight: normal;'>{model.Time}</span></li></ul></div></div></div></div> </div>";
//        }
//        private static string GetPatientProfile(ReportModel model)
//        {
//            return $"<div style='width: 100%;float: left'><h5 style='width: 100%;float: left;margin-top: 1rem;margin-bottom: 1rem;font-size: 1.5rem'>Patient Profile</h5><div style='width: 100%;float: left'><ul style='width: 100%;float: left;margin-bottom: 1.5rem;list-style: none;padding: 0'><li style='font-size: 1rem;font-weight: bold;color: #17a2b8'>Patient Information</li><li style='font-weight: bold'>patient Name : <span style='font-weight: normal'> {model.PatientName}</span></li><li style='font-weight: bold;'>patient ID : <span style='font-weight: normal'>{model.PatientId}</span></li><li style='font-weight: bold;display: inline'>Date of birth : <span style='font-weight: normal'>{model.DateOfBirth}</span></li><li style='font-weight: bold;display: inline;margin-left: 2rem'> Age: <span style='font-weight: normal'>{model.Age}</span></li><li style='font-weight: bold;display: inline;margin-left: 2rem'> Gender: <span style='font-weight: normal'>{model.Gender}</span></li></ul><ul style='width: 100%;float: left; margin-bottom: 4px;list-style: none;padding: 0'><li style='font-size: 1rem;font-weight: bold;color: #17a2b8'>Contact Details</li><li style='font-weight: bold;'>Address : <span style='font-weight: normal'> {model.Address}</span></li><li style='font-weight: bold;'>Telephone Numbers : <span style='font-weight: normal'>{model.TelephoneNumbers}</span></li><li style='font-weight: bold;'>E-Mail : <span style='font-weight: normal'>{model.Email}</span></li></ul><ul style='width: 100%;float: left; margin-bottom: 4px;list-style: none; padding: 0'><li style='font-size: 1rem;font-weight: bold;color: #17a2b8'>Insurance</li><li style='font-weight: bold;display: inline;'>Insurance Carrier : <span style='font-weight: normal'>{model.InsuranceCarrier}</span></li><li style='font-weight: bold;display: inline;margin-left: 5rem'>Membership Number : <span style='font-weight: normal'>{model.MembershipNumber}</span></li><li style='font-weight: bold;display: inline;margin-left: 5rem'>Member Since : <span style='font-weight: normal'>{model.MemberSince}</span></li></ul><ul style='width: 100%;float: left; margin-bottom: 4px;list-style: none; padding: 0'><li style='font-size: 1rem;font-weight: bold;color: #17a2b8'>Doctor</li><li style='font-weight: bold;display: inline;width: 50%;float: left'>Treating Doctor : <span style='font-weight: normal'>{model.TreatingDoctor}</span></li><li style='font-weight: bold;display: inline;width: 50%;float: left'>Clinic: <span style='font-weight: normal'>{model.ReferingDoctor}</span></li><li style='font-weight: bold;display: inline;width: 50%;float: left'>Referring Doctor: <span style='font-weight: normal'>{model.ReferingDoctor}</span></li><li style='font-weight: bold;display: inline;width: 50%;float: left'>Clinic: <span style='font-weight: normal'></span></li></ul><ul style='width: 100%;float: left; margin-bottom: 4px;list-style: none; padding: 0'><li style='font-size: 1rem;font-weight: bold;color: #17a2b8'>Equipment Information</li><li style='font-weight: bold;display: inline;width: 33%;float: left'>Flow Generator: <span style='font-weight: normal'>{model.FlowGenerator}</span></li><li style='font-weight: bold;display: inline;width: 40%;float: left'>Flow Generator Serial Number: <span style='font-weight: normal'>{model.FlowGeneratorSerialNumber}</span></li><li style='font-weight: bold;display: inline;width: 25%;float: left'>Owner: <span style='font-weight: normal'>Yes</span></li><li style='font-weight: bold;display: inline;width: 33%;float: left'> <span style='font-weight: normal'> </span></li><li style='font-weight: bold;display: inline;width: 40%;float: left'>Flow Generator Software Version: <span style='font-weight: normal'>{model.FlowGeneratorSoftwareVersion}</span></li><li style='font-weight: bold;display: inline;width: 25%;float: left'> <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 33%;float: left'>Humidifier: <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 40%;float: left'>Humidifier Serial Number: <span style='font-weight: normal'>{model.HumidifierSerialNumber}</span></li><li style='font-weight: bold;display: inline;width: 25%;float: left'>Owner: <span style='font-weight: normal'>Yes</span></li><li style='font-weight: bold;display: inline;width: 33%;float: left'>Data Module: <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 40%;float: left'>Data Module Serial Number: <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 25%;float: left'>Owner: <span style='font-weight: normal'>Yes</span></li><li style='font-weight: bold;display: inline;width: 33%;float: left'> <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 25%;float: left'>Flow Generator Software Version: <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 25%;float: left'> <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 100%;float: left'>Mask: <span style='font-weight: normal'></span></li><li style='font-weight: bold;display: inline;width: 100%;float: left'>Data Card: <span style='font-weight: normal'></span></li></ul></div></div>";
//        }
//        private static string GetStatistics(ReportModel model)
//        {
//            return $"<div style='width: 100%;float: left'><h5 style='width: 100%;float: left;margin-top: 1rem;margin-bottom: 1rem;font-size: 1.5rem'>Statistics</h5><div style='width: 100%;float: left; border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Device Setting</h3><ul style='list-style:none;padding:0;margin:0;;width: 33%;float:left;font-size: 0.8rem'><li> Therapy mode : <span><b>{model.TherapyMode}</b></span></li><li>Ramp Down Enable : <span><b>{model.RampDownEnable}</b></span></li><li>Ti Min : <span><b> {model.TiMin}</b></span></li><li>Target Vendilation : <span><b>{model.TargetAlveolarVentilation}</b></span></li><li>EPAP : <span><b>{model.EPAP}</b></span></li></ul><ul style='list-style:none;padding:0;margin:0;;width: 33%;float:left;font-size: 0.8rem'><li> Ramp Enable : <span><b>{model.RampEnable}</b></span></li><li>Essentials : <span><b>{model.Essentials}</b></span></li><li>Max PS : <span><b> {model.MaxPS}</b></span></li><li>Height : <span><b>{model.Height}</b></span></li><li>Start EPAP : <span><b>{model.StartEPAP}</b></span></li></ul><ul style='list-style:none;padding:0;margin:0;;width: 33%;float:left;font-size: 0.8rem'><li> Ramp Time : <span><b>{model.RampTime}</b></span></li><li>Ti Max : <span><b>{model.TiMax}</b></span></li><li>Min PS : <span><b> {model.MinPS}</b></span></li><li>Target Patient Rate : <span><b>{model.TargetPatientRate}</b></span></li><li>ODI Threshold : <span><b>{model.ODIThreshold}%</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>IPAP - cmH2O</h3><ul style='list-style:none; padding:0;margin:0;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.IPAPMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.IPAPPercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b> {model.IPAPMinimum}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Leak - L/min</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.LeakMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.LeakPercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b>{model.LeakMinimum}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Tidal Volume - mL</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.TidalVolumeMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.TidalVolumePercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b>{model.TidalVolumeMinimum}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Minute Ventilation - L/min</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.MinuteVentilationMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.MinuteVentilationPercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b> {model.MinuteVentilationMinimum}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Respiratory Rate - breaths/min</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.RespiratoryRateMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.RespiratoryRatePercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b> {model.RespiratoryRateMinimum}</b></span></li><li style='width:33%;float:left'>% Spontaneous triggered breaths : <span><b>91</b></span></li><li style='width:33%;float:left'>% Spontaneous cycled breaths : <span><b> 10</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Respiratory Indices - events/hr</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Apnea Index : <span><b>{model.ApneaIndex}</b></span></li><li style='width:33%;float:left'>Hypopnea Index : <span><b>{model.HypopneaIndex}</b></span></li><li style='width:33%;float:left'>AHI : <span><b>{model.AHI}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Total Usage</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'>Total days : <span><b>{model.TotalDays}</b></span></li><li style='width:33%;float:left'>Total hours used : <span><b> {model.TotalHoursUsed}</b></span></li><li style='width:33%;float:left'>Average daily usage : <span><b> {model.AverageDailyUsed}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>I:E Ratio</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.IERatioMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.IERatioPercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b> {model.IERatioMinimum}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Inspiratory Time - seconds</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.InspiratoryTimeMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.InspiratoryTimePercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b> {model.InspiratoryTimeMinimum}</b></span></li></ul></div><div style='width: 100%;float: left;border-bottom: 1px solid #dddddd'><h3 style='width: 100%;float: left; color: #17a2b8'>Alveolar Ventilation - L/min</h3><ul style='list-style:none;padding:0;margin:0;;width:100%;float:left;font-size: 0.8rem'><li style='width:33%;float:left'> Median : <span><b>{model.AlveolarVentilationMedian}</b></span></li><li style='width:33%;float:left'>95th Percentile : <span><b>{model.AlveolarVentilationPercentile}</b></span></li><li style='width:33%;float:left'>Minimum : <span><b>{model.AlveolarVentilationMinimum}</b></span></li></ul></div></div>";
//        }
//        private static string GetBreakPage()
//        {
//            return "<div style='display:block;page-break-before:always;'></div>";
//        }
//        private static string GetColumnChartContainer(string minimum, string median, string precintail, string containerId, string title)
//        {
//            return "<div style='display: block;'><figure style='display: block;margin-block-start: 1em;margin-block-end: 1em;margin-inline-start: 40px;margin-inline-end: 40px;'>" +
//             $"<div style='overflow: visible !important' id='{containerId}'></div>" +
//             "<script>$(function () { " +
//             $"columnChart('{containerId}',[{minimum}],[{median}],[{precintail}],'{title}') " +
//             "})</script>" +
//             "</figure></div>";
//        }
//        private static string GetColumnChartContainer(string model, string containerId, string title)
//        {
//            return $"<div style='max-height:250px !important;overflow: visible !important' id='{containerId}'></div><script>OneColumnChart('{containerId}','{model}','{title}')</script>";
//        }

//    }
//}
