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

        public bool Update(Usuarios ObjUsuarioToUpdate)
        {
            bool IsUpdated = false;
            try
            {
                var ObjUser = DataBase.Usuarios.Find(ObjUsuarioToUpdate.idUsuario);
                ObjUser.celular = ObjUsuarioToUpdate.celular;
                ObjUser.email = ObjUsuarioToUpdate.email;
                ObjUser.delegacion = ObjUsuarioToUpdate.delegacion;
                var NoRowsAffected = DataBase.SaveChanges();
                IsUpdated = NoRowsAffected > 0;
            }
            catch (Exception)
            {

                throw;
            }
            return IsUpdated;
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
