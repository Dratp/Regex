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
            string choice;
            string cont;

            do
            {
                choice = Welcome();
                switch (choice)
                {
                    case "N":
                        NameVaildation();
                        break;
                    case "E":
                        EmailValidation();
                        break;
                    case "P":
                        PhoneValidation();
                        break;
                    case "D":
                        DateValidation();
                        break;
                    default:
                        Console.WriteLine("Something went wrong: A charater other than N, E, P, or D was passed to the switch sstatement");
                        break;
                }
                Console.WriteLine();
                do
                {

                    Console.Write("Continue y/n: ");
                    cont = Console.ReadLine();
                    cont = cont.ToUpper();
                } while (cont != "Y" && cont != "N");
            } while (cont != "N");
            
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

        static void EmailValidation()
        {
            Console.Clear();
            string input;
            bool doesItMatch;
            Regex reg = new Regex(@"^\w{5,30}@\w{5,15}\.\w{2,3}$", RegexOptions.IgnoreCase);
            // Changed the brief becuase @QuickenLoans is 12 charaters 
            // brief said more than 10 in this area is not a valid e-mail address changed to 15
            // assuming my most likely client would want thier e-mail addresses to validate
            // *** Need to ask the business if this is acceptable ***

            Console.WriteLine("You may enter test to pull test Data");
            Console.Write("Please Enter an E-mail Address: ");
            input = Console.ReadLine();

            if (input == "test")
            {
                Test(reg);
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

        static void NameVaildation()
        {
            Console.Clear();
            string input;
            Regex single = new Regex(@"^[A-Z][a-z]\w{1,30}$");
            Regex firstlast = new Regex(@"^([A-Z][a-z]\w+\s[A-Z][a-z]\w+)\w{1,30}$");

            Console.WriteLine("You may enter test to pull test Data");
            Console.Write("Please Enter a Name: ");
            input = Console.ReadLine();

            if (input == "test")
            {
                Test(single, firstlast);
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

        static void PhoneValidation()
        {
            Console.Clear();
            string input;
            Regex phonepara = new Regex(@"^\([0-9]{3}\)-[0-9]{3}-[0-9]{4}$");
//            Regex phonedash = new Regex(@"^([0-9]){3}-([0-9]){3}-([0-9]){4}$");
            Regex phone = new Regex(@"^([0-9]){3}[\.,-]([0-9]){3}[\.,-]([0-9]){4}$");

            Console.WriteLine("You may enter test to pull test Data");
            Console.Write("Please Enter a Phone number: ");
            input = Console.ReadLine();

            if (input == "test")
            {
                Test(phonepara, phone);
            }
            else if (phonepara.IsMatch(input) || phone.IsMatch(input))
            {
                Console.WriteLine("That is a Valid phone number");
            }
            else
            {
                Console.WriteLine("What ever that is it is not a phone number");
            }
        }
      

        static void DateValidation()
        {
            Console.Clear();
            string input;
            bool doesItMatch;
            Regex Date = new Regex(@"^[0-1][0-9]/[0-3][0-9]/[0-1][0-9][0-9][0-9]$");

            Console.WriteLine("You may enter test to pull test Data");
            Console.Write("Please Enter a Date (mm/dd/yyyy): ");
            input = Console.ReadLine();

            if (input == "test")
            {
                Test(Date);
            }
            else
            {
                doesItMatch = Date.IsMatch(input);
                if (doesItMatch)
                {
                    Console.WriteLine("That is a Valid Date");
                }
                else
                {
                    Console.WriteLine("That is not a Date");
                }
            }


        }

        static void Test(Regex test1)
        {
            Console.Clear();
            string[] data = GetTestData();
            bool doesItMatch;
            Console.WriteLine();
            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch = test1.IsMatch(data[i]);
                Console.WriteLine("{0,50}: {1,7}", data[i], doesItMatch);
            }
        }

        static void Test(Regex test1, Regex test2)
        {
            Console.Clear();
            string[] data = GetTestData();
            bool[,] doesItMatch = new bool[2,data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch[0,i] = test1.IsMatch(data[i]);
            }
            for (int i = 0; i < data.Length; i++)
            {
                doesItMatch[1,i] = test2.IsMatch(data[i]);
            }
            Console.WriteLine("If Test 1 or Test 2 is valid the the data is valid");
            Console.WriteLine("Some additional tests are done in the non test version using if statments");
            Console.WriteLine("{0,40}: {1,7} {2,7}", "data", "Test 1", "Test 2");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("{0,40}: {1,7} {2,7}",data[i], doesItMatch[0,i], doesItMatch[1,i]);
            }
        }

        static string[] GetTestData()
        {
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
            return testdata;
        }

        // @ before "" = able to make \ as a ignore next charater called a literal as a special charater
        // ^ = Start of String
        // . = wildcard
        // * = any number of previous charater
        // $ = end of String
        // [abc] = match the char to only a,b, or c
        // [^abc] = match the char to any except a,b or c
    }
}
