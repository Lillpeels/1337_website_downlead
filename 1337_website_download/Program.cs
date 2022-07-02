using System;
using System.Net;
using System.Threading.Tasks;

namespace _1337_website_download
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean programRunning = true;

            while (programRunning) {

                Console.WriteLine("Type URL address: ");
                String url = Console.ReadLine();

                Console.WriteLine("Type save file path: ");
                String sfp = Console.ReadLine();
                
                try 
                { 
                    DownloadFile(url, sfp);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
        }
        
        
        public static void DownloadFile(string remoteFilename, string localFilename)
        {
            
            WebClient client = new WebClient();
            //client.DownloadFile(remoteFilename, localFilename);
            Uri uri = new Uri(remoteFilename);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

            client.DownloadFileAsync(uri, localFilename);


        }

        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }

    }
}
