using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient("http://localhost:5024/");

            int current = 1;
            Debug.WriteLine(DateTime.Now.ToString());
            List<Task> tasks = new List<Task>();
            do
            {
                Task one = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Int64 id = new Random().Next(10000, int.MaxValue);
                        decimal score = new Random().Next(-1000, 1000);
                        RestRequest request = new RestRequest($"Customer/{id}/score/{score}", Method.Post);
                        RestResponse res = client.Execute(request);

                        int start = 3;
                        int end = 10;

                        request = new RestRequest($"Leaderboard?start={start}&end={end}", Method.Get);
                        res = client.Execute(request);
                        Debug.WriteLine($"Leaderboard?start={start}&end={end}:" + JsonConvert.DeserializeObject<JArray>(res.Content));


                        int high = 2;
                        int low = 3;

                        request = new RestRequest($"Leaderboard/{id}?low={low}&high={high}", Method.Get);
                        res = client.Execute(request);
                        Debug.WriteLine($"Leaderboard/{id}?low={low}&high={high}:" + JsonConvert.DeserializeObject<JArray>(res.Content));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                });

                tasks.Add(one);
                current++;
            }
            while (current < 20);

            Task.Factory.ContinueWhenAll(tasks.ToArray(), one => { return string.Empty; }).Wait();

            Debug.WriteLine(DateTime.Now.ToString());

            RestRequest requestAll = new RestRequest($"Customer/GetAllTestData", Method.Get);
            RestResponse resAll = client.Execute(requestAll);
            Debug.WriteLine(JsonConvert.DeserializeObject<JArray>(resAll.Content));
            Console.WriteLine("Done");
            Console.ReadKey();
            
        }
    }
}
