using QuoteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteFinder.Logic;

namespace QuoteFinder.UI
{
    public  class UserInteraction : IUserInterface
    {
        private QueryAPI queryAPI = new QueryAPI();
        public void AskForData()
        {
            List<string> results = new List<string> ();

            Console.WriteLine("What word are you looking for?");
            string wordToLook = Console.ReadLine();
            wordToLook = ValidateIsAValidWord(wordToLook);

            Console.WriteLine("How many pages do you want to read?");
            string pages = Console.ReadLine();
            int numOfPages = ValidateIsAValidNum(pages);

            Console.WriteLine("How many quotes per page?");
            string quotes = Console.ReadLine();
            int numOfQuotes = ValidateIsAValidNum(quotes);

            results.Add(wordToLook);
            results.Add(numOfPages.ToString());
            results.Add(numOfQuotes.ToString());

            Console.WriteLine("Shall process pages in parallel? ('y' for 'yes', anything else for 'no')");
            string answer = Console.ReadLine();
            if(answer == "y")
            {
                queryAPI.QueryDataMultiThreaded(results);
            }
            else
            {
                queryAPI.QueryDataSingleThreaded(results);
            }
                

        }

        public int ValidateIsAValidNum(string input)
        {
            bool IsInputANum = int.TryParse(input, out int num);
            while (IsInputANum == false)
            {
                Console.WriteLine("The number of pages must be a number, please enter a valid number.");
                input = Console.ReadLine();
                IsInputANum = int.TryParse(input, out num);
            }
            
            return num;
        }

        public string ValidateIsAValidWord(string input)
        {
            while(string.IsNullOrWhiteSpace(input) || int.TryParse(input, out int n) == true)
            {
                Console.WriteLine("The word cannot be null, empty or a number, please enter a valid word.");
                input = Console.ReadLine();
            }

            string inputTrimmed = input.Trim();
            string word = "";
            foreach(char c in inputTrimmed)
            {
                if (char.IsLetter(c) == false)
                {
                    continue;
                }
                word += c;
            }

            return word;

        }
    }
}
