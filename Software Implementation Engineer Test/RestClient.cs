using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Software_Implementation_Engineer_Test
{
    public enum httpVerbs
    {
        GET,
        POST,
        DELETE
    }
    internal class RestClient
    {
        public string endPoint { get; set; }
        public httpVerbs httpMethod { get; set; }
        public string sCode { get; set; }
        public string postJSON { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerbs.GET;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();
            


            if (request.Method == "POST" && postJSON != string.Empty)
            {
                
                try
                {
                    request.ContentType = "application/json";

                    using (StreamWriter swJson = new StreamWriter(request.GetRequestStream()))
                    {
                        swJson.Write(postJSON);
                        swJson.Close();
                    }
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in post");
                }
                
            }

            else
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (response)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Error: " + response.StatusCode);
                    }

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                                sCode = response.StatusCode.ToString();
                            }
                        }
                    }
                }

            }
            
                


            return strResponseValue;
        }
    }
}
