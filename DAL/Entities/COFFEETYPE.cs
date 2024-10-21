namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COFFEETYPE")]
    public partial class COFFEETYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COFFEETYPE()
        {
            INVENTORies = new HashSet<INVENTORY>();
            MENUs = new HashSet<MENU>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTYPE { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NSX { get; set; }

        [StringLength(100)]
        public string ORIGIN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVENTORY> INVENTORies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MENU> MENUs { get; set; }
    }
}
