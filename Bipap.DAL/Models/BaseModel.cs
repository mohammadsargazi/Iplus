using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bipap.DAL.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Visible = true;
            CreateDate = DateTime.Now;
        }
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotListed]
        public int Id { get; set; }
        [NeverUpdate]
        [NotListed]
        public DateTime CreateDate { get; set; }
        [NeverUpdate]
        [NotListed]
        public bool Visible { get; set; }
    }
}
