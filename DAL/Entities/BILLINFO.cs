namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BILLINFO")]
    public partial class BILLINFO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDINFO { get; set; }

        public int IDBILL { get; set; }

        public int? IDEMPLOYEE { get; set; }

        public int IDMENU { get; set; }

        public double COUNT { get; set; }

        public virtual BILL BILL { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }

        public virtual MENU MENU { get; set; }
    }
}
