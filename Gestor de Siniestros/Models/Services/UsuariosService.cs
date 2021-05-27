using Gestor_de_Siniestros.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    public class UsuariosService
    {
        private static List<Usuarios> ObjUserList;
        private DataBaseEntities DataBase;

        public UsuariosService()
        {
            DataBase = new DataBaseEntities();
            ObjUserList = new List<Usuarios>();
            using (DataBaseEntities database = new DataBaseEntities())
            {
                var lst = database.Usuarios;
                foreach (var OUsuario in lst)
                {
                    ObjUserList.Add(OUsuario);
                }
            }
        }

        public List<Usuarios> GetAll()
        {
            return ObjUserList;
        }

        public bool Add(Usuarios ObjNewUsuario)
        {
            validateUser(ObjNewUsuario);
            DataBase.Usuarios.Add(ObjNewUsuario);
            DataBase.SaveChanges();
            return true;
        }

        public void Update(Usuarios ObjUsuarioToUpdate)
        {
            validateUser(ObjUsuarioToUpdate);
            if (ObjUsuarioToUpdate.idUsuario > 0)
            {
                Usuarios selectedUser = DataBase.Usuarios.First(usr => usr.idUsuario == ObjUsuarioToUpdate.idUsuario);
                selectedUser.nombre = ObjUsuarioToUpdate.nombre;
                selectedUser.aPaterno = ObjUsuarioToUpdate.aPaterno;
                selectedUser.email = ObjUsuarioToUpdate.email;
                selectedUser.celular = ObjUsuarioToUpdate.celular;
                selectedUser.contraseña = ObjUsuarioToUpdate.contraseña;
                
            }
            else
            {
                DataBase.Usuarios.Add(ObjUsuarioToUpdate);
            }
            DataBase.SaveChanges();
        }

        public void Delete(Usuarios User)
        {
            DataBase.Usuarios.Remove(User);
            DataBase.SaveChanges();
        }

        public Usuarios Search(int id)
        {
            Usuarios UserToReturn;
            UserToReturn = DataBase.Usuarios.Find(id);
            return UserToReturn;
        }

        private void validateUser(Usuarios user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Usuario", "Error en Update");
            }

            if (string.IsNullOrEmpty(user.nombre))
            {
                throw new ArgumentNullException("Nombre", "Porfavor ingresa un nombre");
            }
            else if (string.IsNullOrEmpty(user.aPaterno))
            {
                throw new ArgumentNullException("Apellido Paterno", "Porfavor ingresa un Apellido Paterno");
            }
            else if (string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentNullException("Email", "Porfavor ingresa un Apellido Paterno");
            }
            else if (user.celular.HasValue == false)
            {
                throw new ArgumentNullException("Celular", "Porfavor ingresa un Numero de celular");
            }
            else if (string.IsNullOrEmpty(user.contraseña))
            {
                throw new ArgumentNullException("Contraseña", "Porfavor ingresa una contraseña");
            }
        }
    }
}
