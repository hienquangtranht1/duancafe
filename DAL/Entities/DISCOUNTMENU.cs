namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DISCOUNTMENU")]
    public partial class DISCOUNTMENU
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDIS_MENU { get; set; }

        public int IDDISCOUNT { get; set; }

        public int IDMENU { get; set; }

        [StringLength(100)]
        public string STATUS { get; set; }

        public virtual DISCOUNT DISCOUNT { get; set; }

        public virtual MENU MENU { get; set; }
    }
}
