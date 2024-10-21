namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("INVENTORY")]
    public partial class INVENTORY
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDINVENTORY { get; set; }

        public int IDCOFFEE { get; set; }

        public double QUANTITY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_RECEIVED { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_EXPIRED { get; set; }

        [Required]
        [StringLength(50)]
        public string UNIT { get; set; }

        public virtual COFFEETYPE COFFEETYPE { get; set; }
    }
}
