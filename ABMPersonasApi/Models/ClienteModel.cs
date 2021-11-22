using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMPersonasApi.Models
{
    public class ClienteModel
    {
        public int id { set; get; }
        public int ci { set; get; }

        public string nombre { set; get; }

        public string fechanac { set; get; }

        public string email { set; get; }

        public string telefono { set; get; }

    }
}
