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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTK { get; set; }

        [StringLength(100)]
        public string USERNAME { get; set; }

        [StringLength(100)]
        public string DISPLAYNAME { get; set; }

        [Required]
        [StringLength(100)]
        public string PASSWORD { get; set; }
    }
}
