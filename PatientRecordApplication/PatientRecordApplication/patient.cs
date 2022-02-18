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
    public class CustomException : Exception//creates a custom exception class used for multiple different custom exceptions(negative/sentinel)
    {
        public CustomException() : base("Illegal operation for a negative number")
        {

        }
        public CustomException(string messageValue) : base(messageValue)
        {

        }
        public CustomException(string messageValue, Exception inner) : base(messageValue, inner)
        {

        }
    }
    public class patient//other classes don't inherit from this class as doing so would mess with initialization and the file
    {
        int ID;
        String name;
        decimal balanceOwed;
        bool patientAdded;//included because if its not here it causes problems in the execution of writing to the file
        public patient()
        {
            bool sentinel;
            int endCheck = 1;
            ID = 0;
            if (File.Exists("PatientRecords.txt"))//if the file that patient information is stored on exists
            {
                
                FileStream outFile = new FileStream("PatientRecords.txt", FileMode.Create, FileAccess.Write);//opens the file to write on
                StreamWriter writer = new StreamWriter(outFile);//creates the writer
                while (endCheck != 2)//while the end check value has yet to be input
                {
                    try//no finally block as execution is dependant on if an exception is thrown
                    {
                        patientAdded = false;//value to see if the patient is successfuly added
                        Console.Write("Enter the patients name >> ");
                        name = Console.ReadLine();
                        Console.Write("Enter how much the patient owes >> ");
                        balanceOwed = decimal.Parse(Console.ReadLine());
                        if(balanceOwed < 0)
                        {
                            throw new CustomException("Amount owed cannot be negative");//if the amount entered is negative throw a custom negative exception
                        }
                        ++ID;
                        writer.WriteLine(ID + "," + name + "," + balanceOwed);
                        patientAdded = true;
                    }
                    catch(FormatException formatException)//if a non decimal is put in the dollar amount throws a format exception
                    {
                        Console.WriteLine(formatException.Message);
                        Console.WriteLine("Amount owed must be a number amount!");
                    }
                    catch(CustomException negativeException)
                    {
                        Console.WriteLine(negativeException.Message);
                    }
                    if (patientAdded == true)//if the patient has been added promt for next patient
                    {
                        sentinel = false;
                        while (sentinel == false)//while a proper input has yet to be entered keep prompting for if they want to enter a new patient
                        {
                            try//finally has no use in this function as whether or not an exception is thrown is critical to execution for all parts
                            {
                                Console.Write("Would you like to enter another patient? (1. Yes/2. No) >> ");
                                endCheck = int.Parse(Console.ReadLine());
                                if (endCheck != 1 && endCheck != 2)
                                {
                                    throw new CustomException("Input must be 1 or 2!");//if input is not 1(yes) or 2(no) throw a sentinel exception and keep prompting
                                }
                                sentinel = true;
                            }
                            catch(FormatException formatException)//if the input is not an int throws a format exception
                            {
                                Console.WriteLine("Invalid input:");
                                Console.WriteLine(formatException.Message);
                                Console.WriteLine("Input must be a 1 or 2!");
                            }
                            catch (CustomException sentinelException)
                            {
                                Console.WriteLine("Invalid input:");
                                Console.WriteLine(sentinelException.Message);
                            }
                            catch(Exception otherException)
                            {
                                Console.WriteLine("Invalid input:");
                                Console.WriteLine(otherException.Message);
                            }
                        }
                    }
                    
                }
                writer.Close();//file is closed at the very end of the run instead of in a finally as closing the file premeturly will cause data loss
                outFile.Close();//closes the file and the writer
            }
        }
    }
}
