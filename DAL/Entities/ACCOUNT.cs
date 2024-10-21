namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACCOUNT")]
    public partial class ACCOUNT
    {
        [Key]
        [StringLength(100)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(100)]
        public string PASSWORD { get; set; }

        public int? IDEMPLOYEE { get; set; }

        public int? IDTYPETK { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }

        public virtual TYPEACCOUNT TYPEACCOUNT { get; set; }
    }
}
