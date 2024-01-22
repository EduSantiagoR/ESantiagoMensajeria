using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Contacto
    {
        public Usuario UsuarioRemitente { get; set; }
        public Usuario UsuarioDestinatario { get; set; }
        public List<object> Contactos { get; set; }
    }
}
