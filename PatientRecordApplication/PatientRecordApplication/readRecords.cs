using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PatientRecordApplication
{
    public class readRecords
    {
        int ID;
        string name;
        decimal cost;
        public readRecords()
        {
            string readIn;
            string[] fields;
            FileStream inFile = new FileStream("PatientRecords.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader("PatientRecords.txt");
            readIn = reader.ReadLine();
            while (readIn != null)//until no new line is found
            {
                fields = readIn.Split(',');//split the line into three parts based on the format of the file
                ID = Convert.ToInt32(fields[0]);
                name = fields[1];
                cost = Convert.ToDecimal(fields[2]);
                Console.WriteLine(ID + ": " + name + " $" + cost);//print the line of the file reformatted
                readIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();//no try catch block needed as any value testing there could be had has been done while inputing of the file
        }
    }
}
