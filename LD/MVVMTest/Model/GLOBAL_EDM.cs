namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GLOBAL_EDM
    {
        [Key]
        public int Id_Edm { get; set; }

        [Required]
        [StringLength(50)]
        public string Edm { get; set; }

        [Required]
        [StringLength(250)]
        public string FullName { get; set; }
    }
}
