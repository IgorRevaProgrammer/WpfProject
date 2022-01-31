namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_USERS
    {
        [Key]
        public int id_user { get; set; }

        public string user_login { get; set; }

        public string user_full_name { get; set; }

        public bool type_auth { get; set; }

        public string user_password { get; set; }

        public bool user_lock { get; set; }

        public int id_service { get; set; }

        public int id_division { get; set; }

        [ForeignKey("id_service")]
        public DICT_SERVICES DICT_SERVICES { get; set; }

        [ForeignKey("id_division")]
        public DICT_DIVISIONS DICT_DIVISIONS { get; set; }

    }
}
