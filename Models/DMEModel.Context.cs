﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMEWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DMEEntities : DbContext
    {
        //This class is the DMEEntities partial class
        //The name=DMEEntities basically creating the instance of the application entity context and the specifying the connnection string to the database
        public DMEEntities()
            : base("name=DMEEntities")
        {
        }
    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    //Tables found in our database is specified here for the entity context to use in data definition and manipulation
        public virtual DbSet<ReportInsRegLimitValue> ReportInsRegLimitValues { get; set; }
    }
}
