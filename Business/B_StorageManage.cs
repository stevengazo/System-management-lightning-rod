﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using FluentFTP;
using System.Reflection;

namespace Business
{
    public static class B_StorageManage
    {
        private static readonly string NetworkStoragePath = $@"192.168.1.5";
        private static readonly string userName = "AppUser";
        private static readonly string UserPassword = "ApUser2020$";
        public static readonly string basePath = "array1/Dinnteco";
       
        /// <summary>
        /// Get an specific file from the FTP Server
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="LocalPath">Local file</param>
        /// <param name="NetworkPath">Path where is located the file</param>
        /// <returns>Array of bytes</returns>
        public static byte[] DownloadFile(string fileName = "", string LocalPath = "", string NetworkPath = "")
        {
            try
            {
                var FlagConexion = CheckConnection();
                if (FlagConexion)
                {
                    FtpClient ftpClient = new FtpClient(NetworkStoragePath, userName, UserPassword);
                    ftpClient.Connect();
                    // ftpClient.DownloadFile($"{LocalPath}/{fileName}", $"{basePath}/{NetworkPath}/{fileName}", FtpLocalExists.Overwrite, FtpVerify.None);
                    ftpClient.Download(out byte[] data, $"{basePath}/{NetworkPath}/{fileName}", 0);
                    ftpClient.Disconnect();
                    return data;
                }
                else
                {
                    return null ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
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


        /// <summary>
        /// Return the base path of the FTP Server
        /// </summary>
        /// <returns></returns>
        public static string getBasePath()
        {
            return $"{NetworkStoragePath}/{basePath}"  ;
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
     
        /// <summary>
        /// Delete an existent file in the NAS
        /// </summary>
        /// <param name="NetWorkPath">route of the file</param>
        /// <param name="fileName">name of the file to delete</param>
        /// <returns>True if the file was delete, false if present errors</returns>
       public static bool DeleteFile(string NetWorkPath = "", string fileName = "")
        {
            try
            {
                if (NetWorkPath.Equals("") || fileName.Equals("") || !CheckConnection() )
                {
                    return false;
                }
                else
                {
                    FtpClient ftpClient = new FtpClient(NetworkStoragePath, userName, UserPassword);
                    var filePath = $"{basePath}/{NetWorkPath}/{fileName}";
                    ftpClient.Connect();
                    ftpClient.DeleteFile(filePath);
                    ftpClient.Disconnect();

                    return true;
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
                return false;   
            }


        }


        /// <summary>
        /// Get a list of files in a especific path
        /// </summary>
        /// <param name="path">relative path to consult. Dinnteco/<Path></param>
        /// <returns></returns>
        public static async Task< List<Dictionary<string,string>> >GetListOfFiles(string RelativePath = "")
        {
            try
            {
                string fullPath = $"{basePath}/{RelativePath}";
                // list to return
                var list = new List<Dictionary<string,string>>();
                // variable to save the FTPClient
                FtpClient clientFTP = new FtpClient(NetworkStoragePath, userName, UserPassword);
                await clientFTP.ConnectAsync();
                var resultFlag = await clientFTP.DirectoryExistsAsync(fullPath);
                if (resultFlag)
                {
                    var results = clientFTP.GetListing(fullPath);
                    if (results.Length>0)
                    {
                        foreach (var item in results)
                        {
                            var tmpDict = new Dictionary<string, string>();
                            tmpDict.Add("Name", item.Name.ToString());
                            tmpDict.Add("Type", item.Type.ToString());
                            tmpDict.Add("Size", item.Size.ToString());
                            tmpDict.Add("Location", item.FullName.ToString());
                            list.Add(tmpDict);
                        }
                    }
                }
                await clientFTP.DisconnectAsync();
                return list;
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

        /// <summary>
        /// Check if the folder exist and create a new directory
        /// </summary>
        /// <param name="RelativePath">Relative Path in the nas /{DeviceId}/{Year-Type}</param>
        /// <param name="DirectoryName">Name of the folder to create sample {DirectoryName}</param>
        /// <returns></returns>
        public static async Task<bool> createFolderAsync(string RelativePath = "", string DirectoryName= "Sample")
        {
            try
            {
                var pathString = $"{basePath}{RelativePath}/{DirectoryName}";
                FtpClient ClientFTP = new FtpClient(NetworkStoragePath, userName, UserPassword);
                await ClientFTP.ConnectAsync();
                var ExistsFlag = await ClientFTP.DirectoryExistsAsync(pathString);
                if (!ExistsFlag)
                {
                    await ClientFTP.CreateDirectoryAsync(pathString, true);
                    await ClientFTP.DisconnectAsync();
                    return true; 
                }
                return false;
            }
            catch (Exception e ) 
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
