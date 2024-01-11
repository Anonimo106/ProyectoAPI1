using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAPI1
{
    class Cliente
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }

        public int Citaid { get; set; }
        public Agendar_Cita Cita { get; set; }
    }
}
