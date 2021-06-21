using System.IO;
using allTdL.Services.Interfaces;

namespace allTdL.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service.. Beeyaoth";
        }

        public DriveInfo[] GetAllDriveInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            
            return allDrives;
        }

        public DirectoryInfo GetDirInfo(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo;
        }
    }

}
