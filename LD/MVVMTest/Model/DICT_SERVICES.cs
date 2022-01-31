namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_SERVICES
    {
        [Key]
        public int id_service { get; set; }

        [Required]
        [StringLength(100)]
        public string serv_name { get; set; }

        [Required]
        [StringLength(255)]
        public string serv_full_name { get; set; }
    }
}
