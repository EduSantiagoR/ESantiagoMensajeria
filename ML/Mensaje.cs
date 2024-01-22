using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }
        public int IdUsuarioRemitente { get; set; }
        public int IdUsuarioDestinatario { get; set; }
        public DateTime Fecha { get; set; }
        public string Message { get; set; }
        public List<object> Mensajes { get; set; }
    }
}
