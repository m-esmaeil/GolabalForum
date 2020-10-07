namespace GolabalForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Topics
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topics()
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(150)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int TotalViews { get; set; }

        public int CreatedBy { get; set; }

        public virtual Categories Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        public virtual Users Auther { get; set; }
    }
}
