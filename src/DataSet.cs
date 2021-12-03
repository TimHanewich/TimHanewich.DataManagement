using System;
using TimHanewich.Csv;
using System.Collections.Generic;


namespace TimHanewich.DataSetManagement
{
    public class DataSet
            {
                public List<DataRecord> Records { get; set; }

                public DataSet()
                {
                    Records = new List<DataRecord>();
                }

                public static DataSet CreateFromCsvFileContent(string content)
                {
                    DataSet csvds = new DataSet();
                    CsvFile csv = CsvFile.CreateFromCsvFileContent(content);

                    List<DataRecord> Records = new List<DataRecord>();
                    int t = 0;
                    for (t = 1; t <= csv.Rows.Count - 1; t++)
                    {
                        DataRecord dr = new DataRecord();

                        List<DataAttribute> Attributes = new List<DataAttribute>();
                        int r = 0;
                        for (r = 0; r <= csv.Rows[0].Values.Count - 1; r++)
                        {
                            DataAttribute da = new DataAttribute();
                            da.Name = csv.Rows[0].Values[r];

                            //Get the value
                            string val = csv.Rows[t].Values[r];
                            if (val == "")
                            {
                                da.Value = null; //If it is blank, set it to null
                            }
                            else
                            {
                                da.Value = csv.Rows[t].Values[r];
                            }

                            
                            Attributes.Add(da);
                        }

                        dr.Attributes = Attributes;
                        Records.Add(dr);
                    }


                    csvds.Records = Records;
                    return csvds;
                }

                public string PrintToCsv()
                {
                    //Get all attribute names
                    List<string> AttributeNames = new List<string>();
                    foreach (DataRecord dr in Records)
                    {
                        foreach (DataAttribute da in dr.Attributes)
                        {
                            if (AttributeNames.Contains(da.Name) == false)
                            {
                                AttributeNames.Add(da.Name);
                            }
                        }
                    }

                    //Make the csv file
                    CsvFile csv = new CsvFile();


                    //Write the headers
                    DataRow header_row = csv.AddNewRow();
                    foreach (string s in AttributeNames)
                    {
                        header_row.Values.Add(s);
                    }

                    //Write the records
                    foreach (DataRecord dr in Records)
                    {
                        DataRow csv_row = csv.AddNewRow();
                        foreach (string s in AttributeNames)
                        {
                            try
                            {
                                DataAttribute da = dr.GetDataAttribute(s);
                                csv_row.Values.Add(da.Value);
                            }
                            catch
                            {
                                csv_row.Values.Add("");
                            }
                        }
                    }

                    return csv.GenerateAsCsvFileContent();

                }

            }
}