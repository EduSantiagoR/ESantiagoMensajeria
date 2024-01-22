using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string Token { get; set; }
        public List<object> Usuarios { get; set; }
        public Usuario() { }
        public Usuario(string username, string email, byte[] password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
