namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GLOBAL_DK
    {
        [Key]
        public int Id_DK { get; set; }

        [Required]
        [StringLength(30)]
        public string Code { get; set; }

        [Required]
        [StringLength(300)]
        public string Name_DK { get; set; }

        public int? Id_Parent { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateModify { get; set; }
    }
}
