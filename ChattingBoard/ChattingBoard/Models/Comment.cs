using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChattingBoard.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string By_Who { get; set; }

        [StringLength(1000, ErrorMessage = "限1000字")]
        [Required(ErrorMessage = "必填")]
        public string Text { get; set; }

        public bool Mark { get; set; }

        public virtual Event Event { get; set; }

    }
}