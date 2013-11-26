using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MockDataDebugVisualizer
{
    public class CfgLoader
    {
        private const string SkipsPath = "Skip.cfg";

        public static List<Type> GetSkipTypes()
        {
            if (File.Exists(SkipsPath))
            {
                var skips = File.ReadAllLines("Skip.cfg");

                return skips.Select(Type.GetType).Where(type => type != null).ToList();    
            }
            
            return new List<Type>();
        }

        public static List<Type> GetHardCodedSkipTypes()
        {
            return new List<Type>
                {
                    Type.GetType("System.IO.FileSystemInfo"),
                    Type.GetType("System.IO.DirectoryInfo"),
                };
        }
    }
}
