namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_NOMENCLATURE
    {
        [Key]
        public int Id_Mater { get; set; }

        public int Id_DK { get; set; }

        public int Id_Edm { get; set; }

        [Required]
        [StringLength(350)]
        public string Name_Mater { get; set; }

        [StringLength(1000)]
        public string cDescription { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateModify { get; set; }

        public int Id_User { get; set; }

        public bool bDel { get; set; }

        public virtual GLOBAL_DK GLOBAL_DK { get; set; }

        public virtual GLOBAL_EDM GLOBAL_EDM { get; set; }

        public virtual DICT_USERS DICT_USERS { get; set; }
        public virtual ICollection<TABLE_REQUEST_NOMENCL> TABLE_REQUEST_NOMENCL { get; set; }
    }
}
