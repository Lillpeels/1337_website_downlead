using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Threading;

namespace _1337_website_download
{
    class Program
    {
        static void Main()
        {
            // gets the path to the map user wanna save in
            Console.WriteLine("Type save file path: ");
            string saveFilePath = Console.ReadLine();

            // counter for the loading
            int loading = 0;


            using (var client = new WebClient())
            {
                // gets the html from tretton37
                var htmlSource = client.DownloadString("https://tretton37.com");

                // loops through all the href links
                foreach (var item in GetLinksFromWebsite(htmlSource))
                {
                    // checks that the link is a link to a webpage and not anything else
                    if (item.StartsWith("/"))
                    {
                        
                        Webpage webPage = new Webpage();
                        
                        webPage.url = item;



                        webPage.url = item;

                        // creates the url of the subwebpages and downloads them
                        Uri uri = new Uri("https://tretton37.com" + item);
                        webPage.content = client.DownloadString(uri);

                        // change the / to a - so it can be saved as the file name
                        if (webPage.url.Contains("/"))
                        {
                            webPage.url = webPage.url.Replace("/", "-");
                        }

                        // create a thread for the creation of the file
                        Thread thread1 = new Thread(() => createTxtFile(saveFilePath, webPage.url, webPage.content));
                        thread1.Start();

                        // the loading counter
                        loading++;
                        Console.Write("Loading: " + loading);

                        Console.SetCursorPosition(0, Console.CursorTop);



                    }



                }


                Console.WriteLine("Download completed!");
            }

        }



        public static List<String> GetLinksFromWebsite(string htmlSource)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlSource);
            // gets the href links on the page
            return doc
                .DocumentNode
                .SelectNodes("//a[@href]")
                .Select(node => node.Attributes["href"].Value)
                .ToList();

            
        }

        public static void createTxtFile(string saveFilePath, string url, string content) {
            string fileName = saveFilePath + @"\" + url + ".txt"; 
            FileInfo fi = new FileInfo(fileName);

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (fi.Exists)
                {
                    fi.Delete();
                }

                // Create a new file     
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(content);
                    
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }



    }
    
}
