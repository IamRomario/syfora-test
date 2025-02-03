using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace syfora_test_API.Utils.config
{
    internal class ApiService
    {
        public bool Online { get; set; }
        public string Host { get; set; }
        public string OfflinePath { get; set; }
    }
}
