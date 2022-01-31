using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Model
{
    public class dbContext : DbContext 
    {
        public dbContext(string connectionString) : base(connectionString) { Configuration.LazyLoadingEnabled = false; }


        public virtual DbSet<DICT_DIVISIONS> DICT_DIVISIONS { get; set; }
        public virtual DbSet<DICT_EXECUTORS> DICT_EXECUTORS { get; set; }
        public virtual DbSet<DICT_NOMENCLATURE> DICT_NOMENCLATURE { get; set; }
        public virtual DbSet<DICT_SERVICES> DICT_SERVICES { get; set; }
        public virtual DbSet<DICT_STAGES> DICT_STAGES { get; set; }
        public virtual DbSet<DICT_STATUSES> DICT_STATUSES { get; set; }
        public virtual DbSet<DICT_USER_DIVISION> DICT_USER_DIVISION { get; set; }
        public virtual DbSet<DICT_USER_SERVICE> DICT_USER_SERVICE { get; set; }
        public virtual DbSet<DICT_USERS> DICT_USERS { get; set; }
        public virtual DbSet<GLOBAL_DK> GLOBAL_DK { get; set; }
        public virtual DbSet<GLOBAL_EDM> GLOBAL_EDM { get; set; }
        public virtual DbSet<GLOBAL_SETTINGS> GLOBAL_SETTINGS { get; set; }
        public virtual DbSet<GLOBAL_VIEW> GLOBAL_VIEW { get; set; }
        public virtual DbSet<TABLE_HISTORY> TABLE_HISTORY { get; set; }
        public virtual DbSet<TABLE_REQUEST> TABLE_REQUEST { get; set; }
        public virtual DbSet<TABLE_REQUEST_NOMENCL> TABLE_REQUEST_NOMENCL { get; set; }
        public virtual DbSet<TABLE_STAGE_HISTORY> TABLE_STAGE_HISTORY { get; set; }
   
    }
}
