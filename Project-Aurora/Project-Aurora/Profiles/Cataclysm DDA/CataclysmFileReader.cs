﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA
{

    public class CataFileReader
    {
        public CataFileReader(string filepath)
        {
            path = filepath;
            fw = new FileSystemWatcher();
            fw.Path = Path.GetDirectoryName(path);
            filename = Path.GetFileName(path);
            fw.Changed += fw_onChange;
            fw.EnableRaisingEvents = true;

            ReadFile();
        }
        private string path;
        private FileSystemWatcher fw;
        private string filename;

        public string fileContent;
        public dynamic fileObject;

        private void fw_onChange(object sender, FileSystemEventArgs e)
        {
            if (e.Name.Equals(filename) && e.ChangeType == WatcherChangeTypes.Changed)
                ReadFile();
        }

        public bool ReadFile()
        {
            try
            {
                if (File.Exists(path))
                {
                    var reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                    string fileContent_t = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();

                    //bindsContent = "{\"catabinds\": " + bindsContent + "}";

                    //bindsObject = JsonConvert.DeserializeObject<CataBindsHolder>(bindsContent);
                    //cataKeybinds.UpdateBinds(bindsObject);
                    if (fileContent_t != fileContent)
                    {
                        fileContent = fileContent_t;
                        return Update();
                    }
                    return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Update()
        {
            return false;
        }

    }
}