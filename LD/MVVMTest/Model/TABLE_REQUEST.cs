namespace MVVMTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TABLE_REQUEST:BaseViewModel
    {
        public TABLE_REQUEST()
        {
            TABLE_REQUEST_NOMENCL = new ObservableCollection<TABLE_REQUEST_NOMENCL>();
            TABLE_STAGE_HISTORY = new ObservableCollection<TABLE_STAGE_HISTORY>();
        }

        [Key]
        public int id_request { get; set; }

        [Required]
        [StringLength(255)]
        public string request_number { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime? delete_at { get; set; }

        public int id_division { get; set; }

        public int id_service { get; set; }

        public int id_status { get; set; }

        public int id_stage { get; set; }

        public int id_user { get; set; }

        public int? id_exec { get; set; }
        
        public DateTime? accept_at { get; set; }

        public virtual DICT_DIVISIONS DICT_DIVISIONS { get; set; }

        public virtual DICT_SERVICES DICT_SERVICES { get; set; }

        public  DICT_STAGES _DICT_STAGES;
        public virtual DICT_STAGES DICT_STAGES
        {
            get
            {
                return _DICT_STAGES;
            }
            set
            {
                _DICT_STAGES = value;
                OnPropertyChanged("DICT_STAGES");
            }
        }

        public  DICT_STATUSES _DICT_STATUSES;

        public virtual DICT_STATUSES DICT_STATUSES
        {
            get
            {
                return _DICT_STATUSES;
            }
            set
            {
                _DICT_STATUSES = value;
                OnPropertyChanged("DICT_STATUSES");
            }
        }

        public virtual DICT_USERS DICT_USERS { get; set; }

        [ForeignKey("id_exec")]
        private DICT_EXECUTORS _DICT_EXECUTORS;
        [ForeignKey("id_exec")]
        public DICT_EXECUTORS DICT_EXECUTORS
        {
            get { return _DICT_EXECUTORS; }
            set
            {
                _DICT_EXECUTORS = value;
                OnPropertyChanged("DICT_EXECUTORS");
            }
        }
        public virtual ICollection<TABLE_REQUEST_NOMENCL> TABLE_REQUEST_NOMENCL { get; set; }
        public virtual ICollection<TABLE_STAGE_HISTORY> TABLE_STAGE_HISTORY { get; set; }
    }
}
