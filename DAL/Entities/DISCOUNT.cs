namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DISCOUNT")]
    public partial class DISCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISCOUNT()
        {
            DISCOUNTMENU = new HashSet<DISCOUNTMENU>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDIS { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        public double DISCOUNT_PERCENTAGE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_START { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_FINISH { get; set; }

        [StringLength(100)]
        public string STATUS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCOUNTMENU> DISCOUNTMENU { get; set; }
    }
}
