namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TABLE_HISTORY
    {
        [Key]
        public int id_history { get; set; }

        [Required]
        [StringLength(500)]
        public string old_request { get; set; }

        [Required]
        [StringLength(500)]
        public string new_request { get; set; }
    }
}
