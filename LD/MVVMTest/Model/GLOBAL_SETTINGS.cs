namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GLOBAL_SETTINGS
    {
        [Key]
        public int Id_Settings { get; set; }

        [StringLength(50)]
        public string LDAP { get; set; }
    }
}
