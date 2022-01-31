namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_STATUSES
    {
        [Key]
        public int id_status { get; set; }

        [Required]
        [StringLength(100)]
        public string status_name { get; set; }
    }
}
