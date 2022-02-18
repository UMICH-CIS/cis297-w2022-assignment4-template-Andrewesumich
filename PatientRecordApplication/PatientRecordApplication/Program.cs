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
    class Program
    {
        static void Main(string[] args)
        {
            //used to test each of the class functions
            patient input = new patient();
            readRecords readOne = new readRecords();
            readID readTwo = new readID();
            minBalance readThree = new minBalance();
            Console.WriteLine("Enter any value to end");
            Console.ReadLine();//keeps the program running at the end so the user can look at the data properly before the program ends
        }
    }
}
