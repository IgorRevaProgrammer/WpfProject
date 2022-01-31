namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_USER_DIVISION
    {
        [Key]
        public int id_ud { get; set; }

        public int id_user { get; set; }

        public int id_division { get; set; }

        public virtual DICT_DIVISIONS DICT_DIVISIONS { get; set; }

        public virtual DICT_USERS DICT_USERS { get; set; }
    }
}
