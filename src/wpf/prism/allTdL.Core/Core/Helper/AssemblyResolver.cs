using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace allTdL.Core.Helper
{
    public static class AssemblyResolver
    {
        static AssemblyResolver()
        {

        }

        public static void Init()
        {
            ErrorLogging.ClearPreviousLogs();
            ErrorLogging.LogError("ErrorLogging Initialized");
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (!args.Name.Contains(".resources"))
            {
                if (args.Name.ToLower().StartsWith("allTdL"))
                {
                    var assemblylocation = ConfigurationManager.AppSettings["assemblylocation"];
                    if (!string.IsNullOrEmpty(assemblylocation))
                    {
                        string assemblyName;
                        if (args.Name.Contains(","))
                        {
                            assemblyName = args.Name.Substring(0, args.Name.IndexOf(","));
                        }
                        else
                        {
                            assemblyName = args.Name;
                        }
                        assemblyName = assemblylocation + assemblyName + ".dll";
                        if (File.Exists(assemblyName))
                            return Assembly.LoadFrom(assemblyName);
                        ErrorLogging.LogError("Could not locate the assembly " + assemblyName + ", Please contact AllTdL support.");
                    }
                    else
                    {
                        ErrorLogging.LogError("Assembly location details missing in app.config file, Please contact AllTdL support." + args.Name);
                    }
                }
            }
            return null;
        }
    }

    public static class LicenseKeyLocator
    {
        public static void FindandRegisterLicenseKey()
        {
            //SyncfusionLicenseProvider.RegisterLicense(FindLicenseKey());
        }

        /// <summary>
        /// Helper method to find a syncfusion license key.
        /// </summary>
        /// <returns>License Key</returns>
        private static string FindLicenseKey()
        {
            int levelsToCheck = 12;
            string filePath = @"allTdLLicenseKey.txt";

            string rootPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace(@"file:///", ""));

            if(string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            for (int n = 0; n < levelsToCheck; n++)
            {
                string fileDataPath = System.IO.Path.Combine(rootPath, filePath);
                if (System.IO.File.Exists(fileDataPath))
                    return File.ReadAllText(fileDataPath, Encoding.UTF8);
                DirectoryInfo rootDirectory = Directory.GetParent(rootPath);
                if (rootDirectory == null)
                    break;
                rootPath = rootDirectory.FullName;
            }
            return string.Empty;
        }
    }

    /// <summary>
    /// Logs the errors in ErrorLog.txt
    /// </summary>
    public static class ErrorLogging
    {
        /// <summary>
        /// Method to Clear previous logs
        /// </summary>
        internal static void ClearPreviousLogs()
        {
            string errorPath = Directory.GetCurrentDirectory() + @"\Errorlog.txt";
            if (File.Exists(errorPath))
                File.Delete(errorPath);
#if DEBUG
            string bindingErrorPath = Directory.GetCurrentDirectory() + @"\BindingError.txt";
            string livedemosPath = Directory.GetCurrentDirectory() + @"\LiveDemos.txt";
            if (File.Exists(bindingErrorPath))
                File.Delete(bindingErrorPath);
            if (File.Exists(livedemosPath))
                File.Delete(livedemosPath);
#endif
        }

        /// <summary>
        /// Method to take care of error logging operations
        /// </summary>
        /// <param name="error"></param>
        public static void LogError(object error)
        {
            string path = Directory.GetCurrentDirectory() + @"\Errorlog.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter fileWriter = File.AppendText(path))
            {
                fileWriter.WriteLine($"{ DateTime.Now.ToLongTimeString()} :");
                fileWriter.Write(error.ToString() + "\n");
                fileWriter.Close();
            }
        }

#if DEBUG
        /// <summary>
        /// Helps to log binding error
        /// </summary>
        /// <param name="error"></param>
        public static void LogBindingError(object error)
        {
            string path = Directory.GetCurrentDirectory() + @"\BindingError.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter fileWriter = File.AppendText(path))
            {
                fileWriter.Write(error.ToString() + "\n");
                fileWriter.Close();
            }
        }

        /// <summary>
        /// Helps to live demos information
        /// </summary>
        public static void LogLiveDemos(object demo)
        {
            string path = Directory.GetCurrentDirectory() + @"\LiveDemos.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter fileWriter = File.AppendText(path))
            {
                fileWriter.Write(demo.ToString() + "\n");
                fileWriter.Close();
            }
        }
#endif
    }
}
