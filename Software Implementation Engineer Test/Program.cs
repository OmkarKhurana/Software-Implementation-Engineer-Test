using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Software_Implementation_Engineer_Test
{
    internal class Program
    {
        RestClient restClient = new RestClient();
        static void Main(string[] args)
        {
            Program program = new Program();
            program.getAllItems();
            program.getParticularItem();
            program.postItem();
            Console.ReadLine();
        }

        private void postItem()
        {
            restClient.endPoint = "https://jsonplaceholder.typicode.com/posts";
            restClient.httpMethod = httpVerbs.POST;
            restClient.postJSON = "{ \"id\": '101'\"title\": 'foo',\"body\": 'bar',\"userId\": 1,}";
            string response = string.Empty;
            response = restClient.makeRequest();


        }

        private void getParticularItem()
        {
            restClient.endPoint = "https://jsonplaceholder.typicode.com/posts/1";
            string response = string.Empty;
            response = restClient.makeRequest();

            try
            {
                Items items = JsonConvert.DeserializeObject<Items>(response);
                
                Console.WriteLine(items.ToString());

                Console.WriteLine("Success: " + restClient.sCode);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void getAllItems()
        {
            restClient.endPoint = "https://jsonplaceholder.typicode.com/posts";
            string response = string.Empty;

            try
            {
                response = restClient.makeRequest();

                List<Items> items = JsonConvert.DeserializeObject<List<Items>>(response);
                foreach (var item in items)
                {
                    //Console.WriteLine(item.ToString());
                    writeToFile(item.ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Success: All Items have been written to a txt file.");
            }
            
        }

        public void writeToFile(string str)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("ItemsList.txt", true);
                //Write a line of text
                sw.WriteLine(str);
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }

    class Items
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int userId { get; set; }

        public string ToString()
        {
            return string.Format(id + "\n" + title + "\n" + body + "\n" + userId + "\n");
        }
       
    }

}
