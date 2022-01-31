namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TABLE_REQUEST_NOMENCL
    {
        [Key]
        public int id_reqnom { get; set; }

        public int id_request { get; set; }

        public int Id_Mater { get; set; }

        public int quantity { get; set; }

        [StringLength(255)]
        public string user_description { get; set; }

        [ForeignKey("Id_Mater")]
        public virtual DICT_NOMENCLATURE DICT_NOMENCLATURE { get; set; }
    }
}
