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
    class minBalance
    {
        int ID;
        string name;
        decimal cost;
        public minBalance()
        {
            decimal compareCost;
            string readIn;
            string[] fields;
            bool sentinel = false;
            while (sentinel == false)
            {
                FileStream inFile = new FileStream("PatientRecords.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader("PatientRecords.txt");
                try
                {
                    Console.Write("Enter the min balance due >> ");
                    compareCost = decimal.Parse(Console.ReadLine());
                    if (compareCost < 0)
                    {
                        throw new CustomException("Balance due cannot be negative!");
                    }
                    readIn = reader.ReadLine();
                    while (readIn != null)
                    {
                        fields = readIn.Split(',');
                        ID = Convert.ToInt32(fields[0]);
                        name = fields[1];
                        cost = Convert.ToDecimal(fields[2]);
                        if (cost >= compareCost)
                        {
                            Console.WriteLine(ID + ": " + name + " $" + cost);
                        }
                        readIn = reader.ReadLine();
                    }
                    sentinel = true;
                }
                catch (FormatException formatException)//catches a formating error
                {
                    Console.WriteLine("Invalid Balance input:");
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("The Balance Due must be in decimal format!");
                }
                catch (CustomException customException)//catches a custom negative error
                {
                    Console.WriteLine("Invalid Balance input:");
                    Console.WriteLine(customException.Message);
                }
                catch (Exception otherException)//catches any other error that might occur
                {
                    Console.WriteLine("Invalid Balance input:");
                    Console.WriteLine(otherException.Message);
                }
                finally
                {
                    reader.Close();
                    inFile.Close();
                }
            }
        }
    }
}
