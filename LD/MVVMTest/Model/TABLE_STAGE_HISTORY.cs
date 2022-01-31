namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TABLE_STAGE_HISTORY
    {
        [Key]
        public int id_stage_history { get; set; }

        public int id_request { get; set; }

        public int id_stage { get; set; }

        public int id_status { get; set; }
        public DateTime updated_at { get; set; }

        public virtual DICT_STAGES DICT_STAGES { get; set; }
        public virtual DICT_STATUSES DICT_STATUSES { get; set; }

        public virtual TABLE_REQUEST TABLE_REQUEST { get; set; }
    }
}
