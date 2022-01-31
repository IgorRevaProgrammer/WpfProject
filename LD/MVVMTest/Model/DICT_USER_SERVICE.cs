namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_USER_SERVICE
    {
        [Key]
        public int id_us { get; set; }

        public int id_user { get; set; }

        public int id_service { get; set; }

        public virtual DICT_SERVICES DICT_SERVICES { get; set; }

        public virtual DICT_USERS DICT_USERS { get; set; }
    }
}
