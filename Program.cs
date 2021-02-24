using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace ZendeskUserRemoval
{
    class Program
    {
        static void Main(string[] args)
        {
            Prompt();
            Thread.Sleep(3000000);
        }

        public static void Prompt()
        {
            try
            {
                Console.WriteLine("What would you like to do...?");

                Console.WriteLine("Select a number... ");
                Console.WriteLine("1. Download Zendesk Users ");
                Console.WriteLine("2. Disable Zendesk Users");
                string prompt = Console.ReadLine();

                if (prompt.Equals("1"))
                {
                    GetUsers();

                } else if (prompt.Equals("2"))
                {
                    DisableUsers();
                } else
                {
                    throw new Exception();
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Invalid Input - " + ex + "\n\n");
                
                Prompt();
            }



        }

        public static async void DisableUsers()
        {
            string filePath = ConfigurationManager.AppSettings["FilePath"];
            Console.WriteLine(filePath);

            string password = ConfigurationManager.AppSettings["password"];
            string userName = ConfigurationManager.AppSettings["username"];

            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new
            System.Net.NetworkCredential(userName, password)
            };


            using (var client = new HttpClient(handler))
            {

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = null;

                var data = new StringContent("{user\":{suspended\":false}}", Encoding.UTF8, "application/json");
                try
                {
                    string urls = System.IO.File.ReadAllText(@filePath + "\\disable.txt");
                    Console.WriteLine(urls);
                    List<double> urlList = urls.Split(',').Select(double.Parse).ToList();

                    foreach (var s in urlList)
                    {
                        Console.WriteLine("parsing list");
                        response = await client.DeleteAsync($"/api/v2/users/" + s + ".json");
                        Console.WriteLine("Deleted: " + response.StatusCode);
                        if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                        {
                            Console.WriteLine("Failed to write delete: " + s);
                            ExportLog(s.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed Query: " +  ex.Message);
                }
            }
        }

        public static async void GetUsers()
        {
            //string accessToken = "ZKErxL4WJe1kq1Do3sDikFZ7DVG8inVemf3Xud6J";
            string password = ConfigurationManager.AppSettings["password"];

            string userName = ConfigurationManager.AppSettings["username"];

            List<string> collectionJson = new List<string>();
            List<string> collectionCsv = new List<string>();
            string headers = "id|name|email|active|last_login_at|suspended";
            collectionCsv.Add(headers);


            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new
            System.Net.NetworkCredential(userName, password)
            };


            using (var client = new HttpClient(handler))
            {

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = null;
                
                object returnType = null;

                int counter = 0;
                string nextUrl = "";
                bool loopCounter = true;


                try
                {
                    while (loopCounter)
                    {
                        if (counter == 0)
                        {
                            response = await client.GetAsync($"/api/v2/users/search.json");
                            Console.WriteLine(client.BaseAddress.ToString());
                            counter += 1;
                        }
                        else
                        {
                            string url = ConfigurationManager.AppSettings["baseUrl"];
                            nextUrl = nextUrl.Replace(url, "");
                            counter += 1;
                            response = await client.GetAsync(nextUrl);
                            Console.WriteLine(client.BaseAddress.ToString());


                        }
                        

                        string jsonString = await response.Content.ReadAsStringAsync();
                        UserAPI responseData = JsonConvert.DeserializeObject<UserAPI>(jsonString);
                        try
                        {
                            string url = ConfigurationManager.AppSettings["baseUrl"];
                            nextUrl = responseData.next_page;
                            nextUrl = nextUrl.Replace(url, "");
                            Console.WriteLine(nextUrl);
                            Users tmpList = new Users();
                            List<String> tmp = tmpList.BuildUsers(responseData.users);

                            foreach (string s in tmp)
                            {
                                
                               
                                collectionCsv.Add(s);
                            }
                        } catch (Exception ex)
                        {
                            ExportResults(collectionCsv);
                            Console.WriteLine("Not an issue... The NextUrl was null : " +ex);
                            
                        }
                        
                    }
                    ExportResults(collectionCsv);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
           
        }

        public static void ExportLog(string txt)
        {
            try
            {
                System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["filePath"]);
            }
            catch
            {
                //Console.WriteLine("Folder already exists");
            }

            try
            {

                string path = ConfigurationManager.AppSettings["filePath"];
                Console.WriteLine(path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\log.txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.   
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(DateTime.Now + " : " + txt);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(DateTime.Now + " : " + txt);
                    }
                }
            }
            catch
            {
                //Console.WriteLine("Unable to write file or it already exists");
            }

        }

        public static void ExportResults(List<string> inCollection)
        {
            try
            {
                System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["filePath"]);
            } catch
            {
                Console.WriteLine("Folder already exists");
            }

            try
            {
                if(inCollection.Count <= 1)
                {
                    Console.WriteLine("nothing to save...");

                } else
                {

                    
                    using (var file = File.CreateText(ConfigurationManager.AppSettings["filePath"].ToString() + "\\" + "zendesk-users.csv")){
                        foreach (var arr in inCollection)
                        {
                            Console.WriteLine(arr.ToString());
                            file.WriteLine(arr.ToString());
                           
                        }
                    }
                }
            } catch (UnauthorizedAccessException uax)
            {
                Console.WriteLine("Unauthorized access to file to write.");
            } catch (Exception ex)
            {
                Console.WriteLine("Unable to write/create file. " + ex);
            }
        }
        
        
    }
}
