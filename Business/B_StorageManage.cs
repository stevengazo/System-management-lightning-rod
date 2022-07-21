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

        private static bool CheckConnection()
        {
            try
            {
                FtpClient ftpClient = new FtpClient(NetworkStoragePath, userName, UserPassword);
                ftpClient.Connect();
                ftpClient.Disconnect();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public static string getBasePath()
        {

            return basePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="storagePath">ruta para almacenar el archivo</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static void uploadFIle(string localpath = "", string storagePath ="")
        {

            try
            {
                var tmp = $"{basePath}/{storagePath}/";

                FtpClient ftpClient = new FtpClient(NetworkStoragePath, userName, UserPassword);
                ftpClient.Connect();             
                ftpClient.UploadFile(localpath,tmp,FtpRemoteExists.Overwrite,true,FtpVerify.None);
                ftpClient.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);              
            }
           
         }

        public static async Task downloadFile(string relativePath = "", string FileName = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a list of files in a especific path
        /// </summary>
        /// <param name="path">relative path to consult. Dinnteco/<Path></param>
        /// <returns></returns>
        public static async Task< List<Array>> GetListOfFiles(string path = "")
        {
            List<Array> tmp = new List<Array>();
            try
            {
                if (CheckConnection())
                {
                    var fullpath = basePath + "/" + path;
                    FtpClient ftpClient = new FtpClient(NetworkStoragePath, userName, UserPassword);
                    ftpClient.Connect();
                    var tmpResults = ftpClient.GetListing(fullpath);
                    ftpClient.Disconnect();                  
                    if(tmpResults != null)
                    {                        
                        foreach(var item in tmpResults)
                        {
                            List<string> tmplist = new List<string>();
                            tmplist.Add(item.Name);
                            tmplist.Add(item.Type.ToString());
                            tmplist.Add(item.FullName.ToString());
                            var arraytoPush = tmplist.ToArray();
                           tmp.Add(arraytoPush);
                        }                    
                    }
                }
                return tmp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public static async Task< bool> DeleteFile(string relativePath = "", string namePath = "")
        {
           throw new NotImplementedException();
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
                if (CheckConnection())
                {
                    FtpClient client = new FtpClient(NetworkStoragePath, userName, UserPassword);
                    if (relativePath == "/")
                    {
                        var flag = client.DirectoryExists(namePath);
                        if (!flag)
                        {
                            await client.CreateDirectoryAsync($"{basePath}/{namePath}");
                        }

                    }
                    else
                    {
                        await client.CreateDirectoryAsync($"{basePath}/{relativePath}/{namePath}");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }



    }
}
