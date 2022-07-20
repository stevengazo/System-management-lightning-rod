using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using FluentFTP;

namespace Business
{
    public static class B_StorageManage
    {
        private static readonly string NetworkStoragePath = $@"192.168.1.5";
        private static readonly string userName = "Sgazo";
        private static readonly string UserPassword = "Stega.26";
        private static readonly string basePath = "array1/Sgazo/Dinnteco";


        public static string getBasePath()
        {
            return basePath;
        }
        public static Array[] sample(string path = "")
        {
            List<Array> tmp= new List<Array>();
            try
            {
                var fullpath = basePath + "/"+ path;
                

                FtpClient ftpClient = new FtpClient(NetworkStoragePath, userName, UserPassword);
                
                ftpClient.Connect();
                var tmpResults = ftpClient.GetListing(fullpath);
                ftpClient.Disconnect();
                if (tmpResults.Length > 0)
                {
                    var tmpArray = new string[] { };
                    var counter = 0;
                    foreach (FtpListItem item in tmpResults)
                    {
                        tmpArray[0] = item.Name.ToString();
                        tmpArray[1] = item.Type.ToString();
                        tmpArray[2] = item.FullName.ToString();
                    }
                    tmp[counter] = tmpArray;
                    counter++;
                }
                return tmp.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;


            }


        }


        /// <summary>
        /// Create a new folder in the NAS Storage 
        /// </summary>
        /// <param name="relativePath">Path where the directory need to be create</param>
        /// <param name="namePath">name of the directory</param>
        public static async Task createFolder(string relativePath = "/", string namePath = "Sample")
        {
            try
            {
                FtpClient client = new FtpClient(NetworkStoragePath,userName,UserPassword);            
                if(relativePath == "/")
                {
                    var flag =  client.DirectoryExists(namePath);
                    if(!flag)
                    {
                        await client.CreateDirectoryAsync($"{basePath}/{namePath}");
                    }

                }
                else
                {
                    await client.CreateDirectoryAsync($"{basePath}/{relativePath}/{namePath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        


    }
}
