using QuoteFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuoteFinder.Logic
{
    public class QueryAPI
    {
        private MockQuotesApiDataReader mockQuotesApiDataReader = new MockQuotesApiDataReader();
        public void QueryDataSingleThreaded (List<string> DataToQueryAPI)
        {
            string word = DataToQueryAPI[0];
            int numOfPages = int.Parse(DataToQueryAPI[1]);
            int numOfQuotes = int.Parse(DataToQueryAPI[2]);

            for (int i = 1; i<= numOfPages; i++)
            {
                ProcessAPIData(word, numOfPages, numOfQuotes, i);
            }

        }

        public async void QueryDataMultiThreaded(List<string> DataToQueryAPI)
        {
            string word = DataToQueryAPI[0];
            int numOfPages = int.Parse(DataToQueryAPI[1]);
            int numOfQuotes = int.Parse(DataToQueryAPI[2]);
            var tasks = new List<Task>();
            for (int i = 1; i <= numOfPages; i++)
            {
                Task task = Task.Run(() => ProcessAPIData(word, numOfPages, numOfQuotes, i));
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

        }

        public async Task ProcessAPIData (string word, int numOfPages,int numOfQuotes, int i)
        {
            var tasks = new List<Task<string>>();
            var fetchDataTask = mockQuotesApiDataReader.ReadAsync(i, numOfQuotes);
            tasks.Add(fetchDataTask);
            List <string> dataFetched= (await Task.WhenAll(tasks)).ToList();
            foreach (var data in dataFetched) 
            {
                var root = JsonSerializer.Deserialize<Root>(data);
                var quotes = root.data;
                List<string> quotesList = new List<string>();
                foreach (var quote in quotes)
                {
                    var quoteText = quote.quoteText;
                    if (quoteText.Contains(word))
                    {
                        var wordsFromQuote = quoteText.Split(" ");
                        foreach (string w in wordsFromQuote)
                        {
                            if (w.Equals(word))
                            {
                                quotesList.Add(quoteText);
                            }
                        }
                    }
                }
                if (quotesList.Count > 0)
                {
                    int quoteLength = quotesList[0].Length;
                    string quoteWithShortestLength = quotesList[0];

                    for (int j = 1; j < quotesList.Count; j++)
                    {
                        if (quoteLength > quotesList[j].Length)
                        {
                            quoteLength = quotesList[j].Length;
                            quoteWithShortestLength = quotesList[j];
                        }
                    }
                    Console.WriteLine(quoteWithShortestLength + " | " + "Thread's ID: " + Thread.CurrentThread.ManagedThreadId);
                }
                else
                {
                    Console.WriteLine("No quotes found on this page. | " + "Thread's ID: " + Thread.CurrentThread.ManagedThreadId);
                }
            }
            

        }
    }
}
