namespace WebDuLich.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int InfoId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Info Info { get; set; }
    }
}
