﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataBaseEntities : DbContext
    {
        public DataBaseEntities()
            : base("name=DataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Delegaciones> Delegaciones { get; set; }
        public virtual DbSet<Dictamenes> Dictamenes { get; set; }
        public virtual DbSet<Reportes> Reportes { get; set; }
        public virtual DbSet<ReportesFotografias> ReportesFotografias { get; set; }
        public virtual DbSet<ReportesUsuarios> ReportesUsuarios { get; set; }
        public virtual DbSet<ReportesVehiculos> ReportesVehiculos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
    }
}
