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
    
    public partial class Vehiculos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculos()
        {
            this.ReportesVehiculos = new HashSet<ReportesVehiculos>();
        }
    
        public int idVehiculo { get; set; }
        public string tipoPropiedad { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int año { get; set; }
        public string color { get; set; }
        public string nombreAseguradora { get; set; }
        public string idPoliza { get; set; }
        public string placa { get; set; }
        public int idDueño { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportesVehiculos> ReportesVehiculos { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}