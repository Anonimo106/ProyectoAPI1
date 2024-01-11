using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAPI1
{
    class Agendar_Cita
    {
        public int id { get; set; }
        public string fecha { get; set; }

        public List<Cliente> cliente { get; set; }
    }
}
