//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gestor_de_Siniestros.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.ReportesUsuarios = new HashSet<ReportesUsuarios>();
            this.Vehiculos = new HashSet<Vehiculos>();
        }
    
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string aPaterno { get; set; }
        public string aMaterno { get; set; }
        public int tipoUsuario { get; set; }
        public System.DateTime fechaNacimiento { get; set; }
        public string idLicencia { get; set; }
        public string email { get; set; }
        public Nullable<long> celular { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public Nullable<int> delegacion { get; set; }
    
        public virtual Delegaciones Delegaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportesUsuarios> ReportesUsuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
