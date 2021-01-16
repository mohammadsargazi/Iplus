using Bipap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bipap.Service.Functionality
{
    public class AnalizeModule
    {
        
        public  ModuleStepTwoResult Analize(SessionStepOneModel sessionStepOneModel)
        {
            var moduleStepTwoResult = sessionStepOneModel.ToModel();

            #region minute_vAndbpm
            var i_sum = 0;
            var e_sum = 0;
            moduleStepTwoResult.minute_v = new int[sessionStepOneModel.Flow.Length / 1200];
            moduleStepTwoResult.bpm = new int[sessionStepOneModel.Flow.Length / 1200];


            int bpm_count = 0;

            for (int i = 0; i < sessionStepOneModel.Flow.Length; i += 1200)
            {

                for (int t = i; t < i + 1200; t++)
                {

                    if (t < sessionStepOneModel.Pressure.Length - 1 && sessionStepOneModel.Flow[t] >= 0 && sessionStepOneModel.Pressure[t] % 2 == 0)
                        i_sum += sessionStepOneModel.Flow[t];


                    if (t < sessionStepOneModel.Pressure.Length - 1 && sessionStepOneModel.Flow[t] < 0 && sessionStepOneModel.Pressure[t] % 2 == 1)
                        e_sum += sessionStepOneModel.Flow[t];

                    if (t < sessionStepOneModel.Pressure.Length - 1 && sessionStepOneModel.Pressure[t] % 2 == 0 && sessionStepOneModel.Pressure[t + 1] % 2 == 1)
                        bpm_count++;
                }

                if (i != 0 && (i / 1200) < moduleStepTwoResult.minute_v.Length)
                {
                    moduleStepTwoResult.bpm[i / 1200] = bpm_count;
                    moduleStepTwoResult.minute_v[i / 1200] = (int)(((i_sum + (-e_sum)) / 2) * 0.8);
                    i_sum = 0;
                    e_sum = 0;
                    bpm_count = 0;
                }

                if (i == 0)
                {
                    moduleStepTwoResult.bpm[0] = bpm_count;
                    moduleStepTwoResult.minute_v[0] = (int)(((i_sum + (-e_sum)) / 2) * 0.8);
                    i_sum = 0;
                    e_sum = 0;
                    bpm_count = 0;
                }




            }
            #endregion

            #region tidal_volume

            List<int> tidal_volume = new List<int>();
            i_sum = 0;
            e_sum = 0;


            for (int x = 0; x < sessionStepOneModel.Pressure.Length - 1; x++)
            {
                if (sessionStepOneModel.Flow[x] >= 0 && sessionStepOneModel.Pressure[x] % 2 == 0)
                    i_sum += sessionStepOneModel.Flow[x];


                if (sessionStepOneModel.Flow[x] < 0 && sessionStepOneModel.Pressure[x] % 2 == 1)
                    e_sum += sessionStepOneModel.Flow[x];


                if (sessionStepOneModel.Pressure[x] % 2 == 1 && sessionStepOneModel.Pressure[x + 1] % 2 == 0 && i_sum == 0)
                    e_sum = 0;

                if (sessionStepOneModel.Pressure[x] % 2 == 1 && sessionStepOneModel.Pressure[x + 1] % 2 == 0 && i_sum != 0)
                {
                    tidal_volume.Add((int)(((i_sum + (-e_sum)) / 2) * 0.8));
                    i_sum = 0;
                    e_sum = 0;

                }


            }

            moduleStepTwoResult.tidal_volume = new int[tidal_volume.Count];
            moduleStepTwoResult.tidal_volume = tidal_volume.ToArray();


            #endregion

            #region appnea_indexes

            //int noa = 0;
            int appnea = 0;
            int h_appnea = 0;
            int count = 0;
            int AppneaThreshold = 20;


            for (int m = 0; m < sessionStepOneModel.Flow.Length - 1; m++)
            {
                if (Math.Abs(sessionStepOneModel.Flow[m]) < AppneaThreshold)
                    count++;

                if (Math.Abs(sessionStepOneModel.Flow[m]) < AppneaThreshold && Math.Abs(sessionStepOneModel.Flow[m + 1]) > AppneaThreshold)
                {
                    if ((count / 20) > 10 && (count / 20) < 20)
                        appnea++;


                    if ((count / 20) > 20)
                        h_appnea++;

                    count = 0;
                }



            }

            //noa = h_appnea +appnea ;
            moduleStepTwoResult.Appne = appnea;
            moduleStepTwoResult.HApne = h_appnea;
            moduleStepTwoResult.AppneAndHApnne = appnea + h_appnea;
            //moduleStepTwoResult.appnea_indexes = new int[noa];

            #endregion


            #region ScalingPressureData

            for (int w = 0; w < moduleStepTwoResult.Pressure.Length; w++)
                moduleStepTwoResult.Pressure[w] = (Int16)(moduleStepTwoResult.Pressure[w] * 0.002);



            #endregion

            #region MedianAndPresentModel
            moduleStepTwoResult.minute_vMinimum = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Minimum;
            moduleStepTwoResult.minute_vMedian = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.minute_vPresent = moduleStepTwoResult.minute_v.ToMedianAndPresentModel().Present;
            moduleStepTwoResult.bpmMedian = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.bpmPresent = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Present;
            moduleStepTwoResult.bpmMinimum = moduleStepTwoResult.bpm.ToMedianAndPresentModel().Minimum;
            moduleStepTwoResult.tidal_volumeMinimum = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Minimum;
            moduleStepTwoResult.tidal_volumeMedian = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.tidal_volumePresent = moduleStepTwoResult.tidal_volume.ToMedianAndPresentModel().Present;
            //moduleStepTwoResult.appnea_indexesMedian = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Median;
            //moduleStepTwoResult.appnea_indexesPresent = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Present;
            //moduleStepTwoResult.appnea_indexesMinimum = moduleStepTwoResult.appnea_indexes.ToMedianAndPresentModel().Minimum;

            moduleStepTwoResult.LeakMedian = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.LeakPresent = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Present;
            moduleStepTwoResult.LeakMinimum = moduleStepTwoResult.Leak.ToMedianAndPresentModel().Minimum;

            moduleStepTwoResult.HeaterMedian = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.HeaterPresent = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Present;
            moduleStepTwoResult.HeaterMinimum = moduleStepTwoResult.Heater.ToMedianAndPresentModel().Minimum;
            #endregion

            #region IEration
            int i_time = 0;
            int e_time = 0;
            List<int> i_ratio = new List<int>();
            List<int> e_ratio = new List<int>();
            int[] I_ratio;
            int[] E_ratio;
            int[] InspiratoryArray;

            List<int> InspiratoryTime = new List<int>();
            for (int y = 0; y < sessionStepOneModel.Pressure.Length - 1; y++)
            {
                if (sessionStepOneModel.Pressure[y] % 2 == 0)
                    i_time++;
                if (sessionStepOneModel.Pressure[y] % 2 == 1)
                    e_time++;
                if (sessionStepOneModel.Pressure[y] % 2 == 1 && sessionStepOneModel.Pressure[y + 1] % 2 == 0 && i_time == 0)
                    e_time = 0;

                if (sessionStepOneModel.Pressure[y] % 2 == 1 && sessionStepOneModel.Pressure[y + 1] % 2 == 0 && i_time != 0)
                {
                    InspiratoryTime.Add(i_time);
                    i_ratio.Add((i_time * 100) / (e_time + i_time));
                    e_ratio.Add((e_time * 100) / (e_time + i_time));
                    i_time = 0;
                    e_time = 0;


                }
            }

            InspiratoryArray = new int[InspiratoryTime.Count];
            InspiratoryArray = InspiratoryTime.ToArray();
            I_ratio = new int[i_ratio.Count];
            E_ratio = new int[e_ratio.Count];
            I_ratio = i_ratio.ToArray();
            E_ratio = e_ratio.ToArray();

            moduleStepTwoResult.InspiratoryTimeArray = InspiratoryArray;

            moduleStepTwoResult.I_ratioMedian = I_ratio.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.I_ratioMinimum = I_ratio.ToMedianAndPresentModel().Minimum;
            moduleStepTwoResult.I_ratioPresent = I_ratio.ToMedianAndPresentModel().Present;

            moduleStepTwoResult.E_ratioMedian = E_ratio.ToMedianAndPresentModel().Median;
            moduleStepTwoResult.E_ratioMinimum = E_ratio.ToMedianAndPresentModel().Minimum;
            moduleStepTwoResult.E_ratioPresent = E_ratio.ToMedianAndPresentModel().Present;
            #endregion
            moduleStepTwoResult.ModeStr = GetModel(moduleStepTwoResult.Mode);
            return moduleStepTwoResult;
        }

        private  string GetModel( short inputModel)
        {
            if (inputModel == 6)
                return "Avaps";
            if (inputModel == 5)
                return "Bipap ST";
            if (inputModel == 4)
                return "Bipap T";
            if (inputModel == 3)
                return "Bipap S";
            if (inputModel == 2)
                return "Apap";
            if (inputModel == 1)
                return "Cpap";
            return "";
        }

       
    }
}
