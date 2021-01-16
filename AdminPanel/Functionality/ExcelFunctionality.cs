using AdminPanel.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Functionality
{
    public class ExcelFunctionality
    {
        public List<DeviceExcelModel> GetDataFromCSVFile(Stream stream)
        {
            var empList = new List<DeviceExcelModel>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names  
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            empList.Add(new DeviceExcelModel()
                            {
                                SerialNumber = objDataRow["SerialNumber"].ToString(),
                                RegistrationDate = objDataRow["RegistrationDate"].ToString(),
                                DeviceTypeId = Convert.ToInt32(objDataRow["DeviceTypeId"].ToString()),
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return empList;
        }
    }
}
