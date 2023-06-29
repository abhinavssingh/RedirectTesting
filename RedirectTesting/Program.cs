using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RedirectTesting
{
    public class Program
    {

        static Task Main()
        {
            string strFilePath = @"C:\RedirectTesting\RedirectTest.csv";
            List<string> outputLlist = new List<string>();

            using (StreamReader reader = new StreamReader(strFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var response = RedirectUrlAsync(line);
                    if (response != null)
                    {
                        Console.WriteLine($"Requested url is {line},status code = {(int)response.Result.StatusCode},Redirected url is {response.Result.RequestMessage.RequestUri}");
                        outputLlist.Add($"Requested url is {line},status code = {(int)response.Result.StatusCode},Redirected url is {response.Result.RequestMessage.RequestUri}");
                    }
                }
            }

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "ResponseRedirect.txt")))
            {
                foreach (string output in outputLlist)
                {
                    outputFile.WriteLine(output);
                }

            }

            return Task.CompletedTask;
        }

        static async Task<HttpResponseMessage> RedirectUrlAsync(string line)
        {
            var client = new HttpClient();
            var requestUri = new Uri(line);
            var response = await client.GetAsync(requestUri);
            return response;
        }
    }
}
