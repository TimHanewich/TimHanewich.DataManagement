using System;
using TimHanewich.DataSetManagement;
using Newtonsoft.Json;

namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = System.IO.File.ReadAllText(@"C:\Users\tahan\Downloads\data.csv");
            DataSet ds = DataSet.CreateFromCsvFileContent(content);
            foreach (DataRecord dr in ds.Records)
            {
                Console.WriteLine(JsonConvert.SerializeObject(dr));
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
