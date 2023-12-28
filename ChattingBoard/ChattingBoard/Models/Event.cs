using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingBoard.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "發起時間")]
        public DateTime Time { get; set; }

        [Display(Name = "發起者")]
        public string StartBy_Who { get; set; }

        [Display(Name = "公開活動")]
        public bool Public_Event { get; set; }

        [Display(Name = "活動代碼")]
        public string Secret_Code { get; set; }

        [Display(Name = "活動狀態")]
        public bool Event_Status { get; set; }

        [StringLength(100, ErrorMessage = "限100字")]
        [Required(ErrorMessage = "活動名稱必填")]
        [Display(Name = "活動名稱")]
        public string Event_Name { get; set; }

        [StringLength(50, ErrorMessage = "檔名限50字")]
        [Display(Name = "上傳圖片")]
        public string Picture { get; set; }

        [StringLength(1000, ErrorMessage = "限1000字")]
        [Display(Name = "活動內容")]
        public string Description { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        // 這個是一對多的，「一」的那一方去加的連接，用於「反向查詢」
        // 在 Comment 的表格上，會自動多了一欄 Event_Id
        public virtual ICollection<Comment> Comments { get; set; }
    }
}