using IPlusApp.Models;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IPlusApp.Functionality
{
    public class SessionReportFunctionality
    {
        #region ReadSelectedSessions
        private readonly CommonFunctionality _commonFunctionality = new CommonFunctionality();
        private string selectedSessionsPath = System.IO.Path.Combine(
   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
   "SelectedSession.txt");
        public List<ModuleStepTwoResult> AnalizeSelectedSessions()
        {
            var mouleStepTwoResultList = new List<ModuleStepTwoResult>();
            var sessionstepOneModelList = _commonFunctionality.ReadSessions(selectedSessionsPath);
            foreach (var sessionstepOneMode in sessionstepOneModelList)
            {
                mouleStepTwoResultList.Add(ModeuleStepTwoAnalize.Analize(sessionstepOneMode));
            }
            return mouleStepTwoResultList;
        }
        #endregion

        #region DrawChart
        public void DrawChart(System.Windows.Forms.Panel panelChart, Int16[] array, string chartName, int locationY, int width)
        {
            var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.Series.Clear();
            var CA = new ChartArea(chartName);
            //ChartArea CA = chart.ChartAreas[0];  // quick reference
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;
            chart.ChartAreas.Add(CA);
            chart.Size = new Size(width - 30, 200);
            chart.Location = new Point(8, locationY);
            chart.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var series = new Series
            {
                Name = chartName,
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line
            };

            chart.Series.Add(series);

            for (int i = 1; i < array.Length; i++)
            {
                series.Points.AddXY(i, array[i]);
            }
            chart.Invalidate();
            panelChart.Controls.Add(chart);
        }
        public void DrawChart(System.Windows.Forms.Panel panelChart, int[] array, string chartName, int locationY, int width)
        {
            var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.Titles.Add(chartName);
            chart.Series.Clear();
            var CA = new ChartArea(chartName); // quick reference
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;
            chart.ChartAreas.Add(CA);
            chart.Size = new Size(width - 30, 200);
            chart.Location = new Point(8, locationY);
            chart.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var series = new Series
            {
                Name = chartName,
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line
            };

            chart.Series.Add(series);

            for (int i = 1; i < array.Length; i++)
            {
                series.Points.AddXY(i, array[i]);
            }
            chart.Invalidate();
            panelChart.Controls.Add(chart);
        }
        public void DrawChart(System.Windows.Forms.Panel panelChart, int[] array, int[] array2, string chartName, int locationY)
        {
            var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.Series.Clear();
            ChartArea CA = chart.ChartAreas[0];  // quick reference
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;
            chart.Size = new Size(632, 193);
            chart.Location = new Point(8, locationY);
            var series1 = new Series
            {
                Name = chartName,
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line
            };

            chart.Series.Add(series1);

            for (int i = 1; i < array.Length; i++)
            {
                series1.Points.AddXY(i, array[i]);
            }
            var series2 = new Series
            {
                Name = chartName,
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point
            };

            chart.Series.Add(series2);

            for (int i = 1; i < array2.Length; i++)
            {
                series2.Points.AddXY(i, array[i]);
            }
            chart.Invalidate();
            panelChart.Controls.Add(chart);
        }
        #endregion
    }

    #region StepTwo
    public static class ModeuleStepTwoAnalize
    {
        public static ModuleStepTwoResult Analize(SessionStepOneModel sessionStepOneModel)
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

            return moduleStepTwoResult;

        }
    }


    public class MedianAndPresentModel
    {
        public decimal Median { get; set; }
        public decimal Present { get; set; }
        public decimal Minimum { get; set; }

    }

    public static class ExtentionModel
    {
        public static ModuleStepTwoResult ToModel(this SessionStepOneModel model)
        {
            if (model == null)
                return null;
            return new ModuleStepTwoResult
            {
                FlagStart = (Int16)model.Parameters[0],
                Initial_CPAP = (Int16)model.Parameters[1],
                Pressure_CPAP = (Int16)(model.Parameters[2] / 10),
                Start_Ramp = (Int16)(model.Parameters[3] / 10),
                Flag_Epr = (Int16)model.Parameters[4],
                EPR = (Int16)(model.Parameters[5] / 10),
                EPR_Ramp = (Int16)(model.Parameters[6] / 60000),
                Mode = (Int16)model.Parameters[7],
                Delta_T_P = (Int16)(model.Parameters[8] / 10),
                Delta_T_N = (Int16)(model.Parameters[9] / 10),
                Delta_P = (Int16)(model.Parameters[10] / 10),
                Apap_Min = (Int16)(model.Parameters[11] / 10),
                Apap_Max = (Int16)(model.Parameters[12] / 10),
                Flag_Apnea = (Int16)model.Parameters[13],
                IPAP = (Int16)(model.Parameters[14] / 10),
                EPAP = (Int16)(model.Parameters[15] / 10),
                SENS_I = (Int16)model.Parameters[16],
                SENS_E = (Int16)model.Parameters[17],
                Slope = (Int16)model.Parameters[18],
                ARAMP = (Int16)model.Parameters[19],
                Bti = (Int16)(model.Parameters[20] / 10),
                Bstart_Delay = (Int16)(model.Parameters[21] / 10),
                RR = (Int16)model.Parameters[22],
                Ti = (Int16)(model.Parameters[23] / 10),
                Ti_Min = (Int16)(model.Parameters[24] / 10),
                Ti_Max = (Int16)(model.Parameters[25] / 10),
                IPAP_Min = (Int16)(model.Parameters[26] / 10),
                IPAP_Max = (Int16)(model.Parameters[27] / 10),
                VT = (Int16)model.Parameters[28],
                IE = (Int16)model.Parameters[29],
                ST = (Int16)model.Parameters[30],
                VT_Only_Read = (Int16)model.Parameters[31],
                Toatal_Leak = (Int16)(model.Parameters[32] * 0.02),
                Mask_leak = (Int16)(model.Parameters[33] * 0.02),
                Minute_Ventilation = (Int16)(model.Parameters[34] / 10),
                LastTI = (Int16)(model.Parameters[35] / 100),
                LastTE = (Int16)(model.Parameters[36] / 100),
                HeaterSetPoint = (Int16)(model.Parameters[37] / 100),
                HeaterTemp = (Int16)(model.Parameters[38] / 100),
                Motor = (Int16)model.Parameters[39],
                Comfort = (Int16)model.Parameters[40],
                UID0 = (Int16)model.Parameters[41],
                UID1 = (Int16)model.Parameters[42],
                UID2 = (Int16)model.Parameters[43],
                UID3 = (Int16)model.Parameters[44],
                UID4 = (Int16)model.Parameters[45],
                UID5 = (Int16)model.Parameters[46],
                Version = (Int16)model.Parameters[47],
                UUID0 = (Int16)model.Parameters[48],
                UUID1 = (Int16)model.Parameters[49],
                UUID2 = (Int16)model.Parameters[50],
                UUID3 = (Int16)model.Parameters[51],
                UUID4 = (Int16)model.Parameters[52],
                UUID5 = (Int16)model.Parameters[53],
                UVesrion = (Int16)model.Parameters[54],
                RTCH = (Int16)model.Parameters[55],
                RTCL = (Int16)model.Parameters[56],
                RTC_OFS_GRE_H = (Int16)model.Parameters[57],
                RTC_OFS_GRE_L = (Int16)model.Parameters[58],
                Pressure = model.Pressure,
                Flow = model.Flow,
                Heater = model.Heater,
                Leak = model.Leak,
                Year = model.Year,
                Month = model.Month,
                Day = model.Day,
                StrarTime = model.StrarTime,
                EndTime = model.EndTime,
                Length = model.Length,

            };
        }
        public static MedianAndPresentModel ToMedianAndPresentModel(this int[] array)
        {
            Array.Sort(array);

            decimal[] median_95percent = new decimal[2];

            if (array.Length >= 1)
            {
                if (array.Length % 2 == 1)
                    median_95percent[0] = (decimal)array[array.Length / 2];


                else
                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
                int index = (int)Math.Round((double)(array.Length * 0.95));
                median_95percent[1] = array[index - 1];
            }


            return new MedianAndPresentModel
            {
                Median = median_95percent[0],
                Present = median_95percent[1]
            };
        }
        public static MedianAndPresentModel ToMedianAndPresentModel(this Int16[] array)
        {
            Array.Sort(array);

            decimal[] median_95percent = new decimal[2];

            if (array.Length >=1)
            {
                if (array.Length % 2 == 1)
                    median_95percent[0] = (decimal)array[array.Length / 2];


                else
                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
                int index = (int)Math.Round((double)(array.Length * 0.95));
                median_95percent[1] = array[index - 1];
            }


            return new MedianAndPresentModel
            {
                Median = median_95percent[0],
                Present = median_95percent[1],
                Minimum = array.Min()
            };
        }
        public static MedianAndPresentModel ToMedianAndPresentModel(this decimal[] array)
        {
            Array.Sort(array);

            decimal[] median_95percent = new decimal[2];

            if (array.Length >= 1)
            {
                if (array.Length % 2 == 1)
                    median_95percent[0] = (decimal)array[array.Length / 2];


                else
                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
                int index = (int)Math.Round((double)(array.Length * 0.95));
                median_95percent[1] = array[index - 1];
            }


            return new MedianAndPresentModel
            {
                Median = median_95percent[0],
                Present = median_95percent[1],
                Minimum = array.Min()
            };
        }
    }
    #endregion



}
