namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QLCF")]
    public partial class QLCF
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDBILL { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTABLE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string NAME { get; set; }

        [Key]
        [Column(Order = 3)]
        public double PRICE { get; set; }

        [Key]
        [Column(Order = 4)]
        public double COUNT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateCheckIn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateCheckOut { get; set; }
    }
}
