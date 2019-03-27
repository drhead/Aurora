using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA.FileReaders
{
    class ColorFileReader : CataFileReader
    {
        public ColorFileReader(string filepath) : base(filepath) { }

        public override bool Update()
        {
            fileContent = "{\"catabinds\": " + fileContent + "}";
            fileObject = JsonConvert.DeserializeObject<CataStateHolder>(fileContent);
            return true;
        }
    }
}
