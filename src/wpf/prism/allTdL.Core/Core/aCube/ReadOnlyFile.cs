using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;

namespace allTdL.Core.aCube
{
    public class ReadOnlyFile : DynamicObject
    {
        // Store the path to the file and the initial line count value.
        private string p_filePath;

        // Public constructor. Verify that file exists and store the path in
        // the private variable.
        public ReadOnlyFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("File path does not exist.");
            }

            p_filePath = filePath;
        }

        // Implement the TryInvokeMember method of the DynamicObject class for
// dynamic member calls that have arguments.
        public override bool TryInvokeMember(InvokeMemberBinder binder,
            object[] args,
            out object result)
        {
            StringSearchOption StringSearchOption = StringSearchOption.StartsWith;
            bool trimSpaces = true;

            try
            {
                if (args.Length > 0) { StringSearchOption = (StringSearchOption)args[0]; }
            }
            catch
            {
                throw new ArgumentException("StringSearchOption argument must be a StringSearchOption enum value.");
            }

            try
            {
                if (args.Length > 1) { trimSpaces = (bool)args[1]; }
            }
            catch
            {
                throw new ArgumentException("trimSpaces argument must be a Boolean value.");
            }

            result = GetPropertyValue(binder.Name, StringSearchOption, trimSpaces);

            return result == null ? false : true;
        }

        // Implement the TryGetMember method of the DynamicObject class for dynamic member calls.
        public override bool TryGetMember(GetMemberBinder binder,
            out object result)
        {
            result = GetPropertyValue(binder.Name);
            return result == null ? false : true;
        }

        public List<string> GetPropertyValue(string propertyName,
            StringSearchOption StringSearchOption = StringSearchOption.StartsWith,
            bool trimSpaces = true)
        {
            
            
            var results = new List<string>();
            
            try
            {  
                using var sr = new StreamReader(p_filePath);

                while (!sr.EndOfStream)
                {
                    var line = sr?.ReadLine();
                    // Perform a case-insensitive search by using the specified search options.
                    var testLine = line?.ToUpper();
                    if (trimSpaces)
                    {
                        testLine = testLine?.Trim();
                    }
                    Debug.Assert(testLine != null, nameof(testLine) + " is null");
                    switch (StringSearchOption)
                    {
                        case StringSearchOption.StartsWith:
                            if (testLine.StartsWith(propertyName.ToUpper())) { results.Add(line); }
                            break;
                        case StringSearchOption.Contains:
                            if (testLine.Contains(propertyName.ToUpper())) { results.Add(line); }
                            break;
                        case StringSearchOption.EndsWith:
                            if (testLine.EndsWith(propertyName.ToUpper())) { results.Add(line); }
                            break;
                    }
                }
            }
            catch
            {
                // Trap any exception that occurs in reading the file and return null.
                results = null;
            }

            return results;
        }
    }
}