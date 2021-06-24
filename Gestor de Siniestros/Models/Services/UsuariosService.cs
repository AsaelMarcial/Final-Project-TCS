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
        private DataBaseEntities DataBase;

        public UsuariosService()
        {
            DataBase = new DataBaseEntities();
            
        }

        public List<Usuarios> GetAll()
        {
            List<Usuarios> ObjUsuarioList = new List<Usuarios>();
            try
            {
                var ObjQuery = from usuarios in DataBase.Usuarios select usuarios;
                foreach (var usuario in ObjQuery)
                {
                    ObjUsuarioList.Add(usuario);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ObjUsuarioList;
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
                ObjUser.nombre = ObjUsuarioToUpdate.nombre;
                ObjUser.aPaterno = ObjUsuarioToUpdate.aPaterno;
                ObjUser.aMaterno = ObjUsuarioToUpdate.aMaterno;
                ObjUser.celular = ObjUsuarioToUpdate.celular;
                ObjUser.email = ObjUsuarioToUpdate.email;
                ObjUser.idLicencia = ObjUsuarioToUpdate.idLicencia;
                ObjUser.tipoUsuario = ObjUsuarioToUpdate.tipoUsuario;
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
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
