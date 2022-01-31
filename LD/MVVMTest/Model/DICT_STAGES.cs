namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_STAGES
    {
        [Key]
        public int id_stage { get; set; }

        [Required]
        [StringLength(100)]
        public string stage { get; set; }

        public int stage_order { get; set; }
    }
}
