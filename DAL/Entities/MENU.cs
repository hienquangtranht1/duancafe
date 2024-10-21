namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MENU")]
    public partial class MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MENU()
        {
            BILLINFOes = new HashSet<BILLINFO>();
            DISCOUNTMENUs = new HashSet<DISCOUNTMENU>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDMENU { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        public int IDTYPE { get; set; }

        public double PRICE { get; set; }

        [StringLength(100)]
        public string AVATARMENU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILLINFO> BILLINFOes { get; set; }

        public virtual COFFEETYPE COFFEETYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCOUNTMENU> DISCOUNTMENUs { get; set; }
    }
}
