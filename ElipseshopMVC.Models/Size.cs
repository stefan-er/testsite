namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Size : AuditInfo, IDeletableEntity
    {
        private ICollection<PCSQuantity> pcsQuantities;

        public Size()
        {
            this.pcsQuantities = new HashSet<PCSQuantity>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<PCSQuantity> PCSQuantities
        {
            get { return this.pcsQuantities; }
            set { this.pcsQuantities = value; }
        }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
