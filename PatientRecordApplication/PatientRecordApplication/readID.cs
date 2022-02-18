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

    class readID
    {
        int ID;
        string name;
        decimal cost;
        public readID() 
        {
            bool checkID = false;
            bool sentinel = false;
            int findID;
            string readIn;
            string[] fields;
            while (sentinel == false)//until a valid Id is input keep trying 
            {
                FileStream inFile = new FileStream("PatientRecords.txt", FileMode.Open, FileAccess.Read);//open the file from the start
                StreamReader reader = new StreamReader("PatientRecords.txt");
                Console.Write("Enter an ID number >> ");//prompt for an ID input
                try
                {
                    findID = int.Parse(Console.ReadLine());
                    if (findID < 0)
                    {
                        throw new CustomException("ID cannot be negative!");//if the input is negative throw an exception
                    }
                    readIn = reader.ReadLine();
                    while (readIn != null && ID != findID)//runs until the ID is found or the end of the file is reached
                    {
                        fields = readIn.Split(',');
                        ID = Convert.ToInt32(fields[0]);
                        name = fields[1];
                        cost = Convert.ToDecimal(fields[2]);
                        if (ID == findID)
                        {
                            checkID = true;//if the ID is found and printed end the file search as there is only one of each ID
                        }
                        readIn = reader.ReadLine();
                    }
                    if (checkID == true)
                    {
                        Console.WriteLine(ID + ": " + name + " $" + cost);//if the ID is found print the patients information
                        sentinel = true;
                    }
                    else
                    {
                        Console.WriteLine("ID not found");//if not print that no patient with said ID was found
                        sentinel = true;
                    }
                }
                catch (FormatException formatException)//catches a formatting error
                {
                    Console.WriteLine("Invalid ID input:");
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("The ID must be in int format!");
                }
                catch (CustomException customException)//catches a negative formating error
                {
                    Console.WriteLine("Invalid ID input:");
                    Console.WriteLine(customException.Message);
                }
                catch (Exception otherException)//catches any other error that might occur
                {
                    Console.WriteLine("Invalid ID input:");
                    Console.WriteLine(otherException.Message);
                }
                finally//wheter or not an exception is found close the file so that if one is found mid read you restart your read from the start and if not you just close the file
                {
                    reader.Close();
                    inFile.Close();
                }
            }
        }
    }
}
