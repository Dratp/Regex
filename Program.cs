using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Regexp
{
    class Program
    {
        // Learning Regular Expressions
        //https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=netcore-3.1
        //https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

        static void Main(string[] args)
        {
                    
            //Console.WriteLine("Hello World!");
            // @ before "" = able to make \ as a ignore next charater called a literal as a special charater
            // ^ = Start of String
            // . = wildcard
            // * = any number of previous charater
            // $ = end of String
            // [abc] = match the char to only a,b, or c
            // [^abc] = match the char to any except a,b or c

            
            string choice;

            string[] testdata =
            {
                "",
                "Chuck@TechU..om",
                "David@Quick@enLoans.com",
                "Jeff@Grandcircus.com",
                "madeup@someplace.ca",
                "fake%new.com",
                "Jim@Desktop.org", 
                "@place.com", 
                "Bob@Burgerscom", 
                "Keith@Voltron.defender",
                "David",
                "Abcdefghijklmn Opqrstuvwxyzextrastuff",
                "That Guy",
                "N0t Name",
                "1/12/2020",
                "12/31/1999",
                "15/12/1986",
                "(1d6)-456-7890",
                "(123)-456-7890",
                "1-1-1",
                "(1234)56-7890",
                "1&3-456-7890",
                "9999999999",
                "123.456.7890",
                "123-456-7890"
            };
            

            choice = Welcome();
            switch (choice)
            {
                case "N":
                    NameVaildation(testdata);
                    break;
                case "E":
                    EmailValidation(testdata);
                    break;
                case "P":
                    PhoneValidation(testdata);
                    break;
                case "D":
                    DateValidation(testdata);
                    break;
                default:
                    Console.WriteLine("Something went wrong: A charater other than N, E, P, or D was passed to the switch sstatement");
                    break;
            }

                               

            

        }

        static string Welcome()
        {
            string Entry;
            bool EntryValid;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Data Validator!");
                Console.WriteLine();
                Console.WriteLine("Type N for Name");
                Console.WriteLine("Type E for E-mail");
                Console.WriteLine("Type P for Phone Number");
                Console.WriteLine("Type D for Date");
                Console.Write("What kind of Data would you like to validate?: ");
                Entry = Console.ReadLine();
                Entry = Entry.ToUpper();
                if (Entry != "N" && Entry != "E" && Entry != "P" && Entry != "D")
                {
                    EntryValid = false;
                }
                else
                {
                    EntryValid = true;
                }
// Debugging Code checking variables before moving on
//                Console.WriteLine("EntryValid = " + EntryValid);
//                Console.WriteLine("Entry =" + Entry);
//                Console.Write("Press Enter to continue");
//                Console.ReadLine();
            } while (!EntryValid);
            return Entry;
            
        }

        static void EmailValidation(string[] data)
        {
            Console.Clear();
            string input;
            bool doesItMatch;
            Regex reg = new Regex(@"^\w{5,30}@\w{5,15}\.\w{2,3}$", RegexOptions.IgnoreCase);
            // Changed the brief becuase @QuickenLoans is 12 charaters 
            // brief said more than 10 in this area is not a valid e-mail address changed to 15
            
            Console.Write("Please Enter an E-mail Address: ");
            input = Console.ReadLine();

            if (input == "test")
            {
                Test(data, reg);
            }
            else
            {
                doesItMatch = reg.IsMatch(input);
                if (doesItMatch)
                {
                    Console.WriteLine("That is a Valid E-mail address");
                }
                else
                {
                    Console.WriteLine("That is not an E-mail address");
                }
            }
        }

        static void NameVaildation(string[] data)
        {
            Console.Clear();
            string input;
            Regex single = new Regex(@"^[A-Z][a-z]\w+$");
            Regex firstlast = new Regex(@"^[A-Z][a-z]\w+\s[A-Z][a-z]\w+$");

            Console.Write("Please Enter a Name: ");
            input = Console.ReadLine();

            if (input == "test")
            {
                Test(data, single, firstlast);
            }
            else
            {
                if (input.Length > 30)
                {
                    Console.WriteLine("That is too long to be a name");
                }
                else if (firstlast.IsMatch(input))
                {
                    Console.WriteLine("That is a First and Last Name");
                }
                else if (single.IsMatch(input))
                {
                    Console.WriteLine("That is a single Name");
                }
                else
                {
                    Console.WriteLine("That is not a name");
                }

            }

                
        }

        static void PhoneValidation(string[] data)
        {
            Console.Clear();
            bool doesItMatch;
            Regex phoneparadash = new Regex(@"^\([0-9]{3}\)-[0-9]{3}-[0-9]{4}$");
//            Regex phonedash = new Regex(@"^([0-9]){3}-([0-9]){3}-([0-9]){4}$");
            Regex phonedot = new Regex(@"^([0-9]){3}[\.,-]([0-9]){3}[\.,-]([0-9]){4}$");

            for (int i = 0; i < data.Length; i++)
            {
                if (phoneparadash.IsMatch(data[i]))
                {
                    doesItMatch = true;
                }
//                else if (phonedash.IsMatch(data[i]))
//                {
//                    doesItMatch = true;
//                }
                else if (phonedot.IsMatch(data[i]))
                {
                    doesItMatch = true;
                }
                else
                {
                    doesItMatch = false;
                }
                Console.Write("{0,30} is a phone number?: ", data[i]);
                Console.WriteLine(doesItMatch);
            }
        }

        static void DateValidation(string[] data)
        {
            Console.Clear();
            bool doesItMatch;
            Regex Date = new Regex(@"^[0-1][0-9]/[0-3][0-9]/[0-1][0-9][0-9][0-9]$");

            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch = Date.IsMatch(data[i]);
                Console.Write("{0,30} is a Date? (dd/mm/yyyy format): ", data[i]);
                Console.WriteLine(doesItMatch);
            }
            
        }

        static void Test(string[] data, Regex test1)
        {
            Console.Clear();
            bool doesItMatch;
            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch = test1.IsMatch(data[i]);
                Console.Write("{0,30}: ", data[i]);
                Console.WriteLine(doesItMatch);
            }
        }

        static void Test(string[] data, Regex test1, Regex test2)
        {
            Console.Clear();
            bool[,] doesItMatch = new bool[2,data.Length];
            Console.WriteLine("Test 1");
            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch[0,i] = test1.IsMatch(data[i]);
            }
            Console.WriteLine("Test 2");
            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch[1,i] = test2.IsMatch(data[i]);
            }
            Console.WriteLine("{0,40}: {1,7} {2,7}", "data", "Test 1", "Test 2");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("{0,40}: {1,7} {2,7}",data[i], doesItMatch[0,i], doesItMatch[1,i]);
            }
        }

    }
}
