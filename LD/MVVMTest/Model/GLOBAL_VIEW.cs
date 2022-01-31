namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GLOBAL_VIEW
    {
        [Key]
        public int Id_View { get; set; }

        [Required]
        [StringLength(30)]
        public string Name_View { get; set; }

        [StringLength(100)]
        public string cTitle { get; set; }

        [StringLength(200)]
        public string cDescription { get; set; }

        [StringLength(300)]
        public string ImageSource { get; set; }

        public bool bDel { get; set; }
    }
}
