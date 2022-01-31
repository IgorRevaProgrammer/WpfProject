namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_DIVISIONS
    {

        [Key]
        public int id_division { get; set; }

        [Required]
        [StringLength(255)]
        public string division_name { get; set; }

        public virtual ICollection<DICT_USERS> DICT_USERS { get; set; }
    }
}
