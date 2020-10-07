namespace GolabalForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public int CreatedBy { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Topics Topics { get; set; }

        public virtual Users Users { get; set; }
    }
}
