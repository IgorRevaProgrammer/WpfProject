namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DICT_EXECUTORS:BaseViewModel
    {
        [Key]
        public int id_exec { get; set; }

        [Required]
        [StringLength(255)]
        //private string _exec_full_name;
        public string exec_full_name { get; set; }
        //{
        //    get { return _exec_full_name; }
        //    set
        //    {
        //        _exec_full_name = value;
        //        OnPropertyChanged("exec_full_name");
        //    }
        //}
        [Required]
        [StringLength(100)]
        public string exec_short_name { get; set; }

        public int? id_user { get; set; }
    }
}
