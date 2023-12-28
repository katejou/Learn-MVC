using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChattingBoard.Models;
using System.IO;

namespace ChattingBoard.Controllers
{

    public class ChattingBoardController : Controller
    {
        ChattingBoardContext context = new ChattingBoardContext();

        /// <summary>
        /// 首頁
        /// </summary>
        public ActionResult Index()
        {
            // 取電腦登入者的資訊
            ViewBag.Name = Environment.UserName;
            return View();
        }

        /// <summary>
        /// 網頁維護者
        /// </summary>
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// 發起活動
        /// </summary>
        public ActionResult StartEvent()
        {
            return View();
        }

        public string MakeSecret_Code()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            int passwordLength = 8;//密碼長度
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                //allowedChars -> 這個String ，隨機取得一個字，丟給chars[i]
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            var code = DateTime.Now.ToString("yyMMdd") + new string(chars) + DateTime.Now.ToString("HHmmss");

            return code; // 20碼
        }

        /// <summary>
        /// 發起活動(回傳)
        /// </summary>
        [HttpPost]
        public ActionResult StartEvent(Event thisEvent)
        {
            // 資料夾不許存取。

            if (ModelState.IsValid)
            {

                // 存到資料庫
                thisEvent.Picture = thisEvent.ImageFile == null ? "" : Path.GetFileName(thisEvent.ImageFile.FileName);
                if (thisEvent.Public_Event == false) thisEvent.Secret_Code = MakeSecret_Code();

                thisEvent.StartBy_Who = Environment.UserName;
                thisEvent.Event_Status = true;
                thisEvent.Time = DateTime.Now;
        
                context.Events.Add(thisEvent);
                context.SaveChanges();

                // 檔案上傳到這個路徑  (SaveChanges後才拿到這個 Event 的 Id)
                if (thisEvent.Picture != "")
                {
                    var path = Path.Combine(ControllerContext.HttpContext.Server.MapPath("~/PhotoFile"), thisEvent.Id.ToString());
                    Directory.CreateDirectory(path); // 建資料夾
                    path = Path.Combine(path, thisEvent.Picture);
                    thisEvent.ImageFile.SaveAs(path); // 上傳圖片
                }

                //前往活動頁
                if (thisEvent.Public_Event)
                    return Redirect($"~/ChattingBoard/Event/{thisEvent.Id}");
                else
                    return Redirect($"~/ChattingBoard/Event/{thisEvent.Id}/{thisEvent.Secret_Code}");
            }

            return View();

        }

        /// <summary>
        /// 查詢所有活動紀錄
        /// </summary>
        public ActionResult QueryEvent()
        {
            List<Event> events = context.Events.Where(n => n.Public_Event).ToList();
            // events.Public_Event == false
            events.Reverse();
            return View(events);
        }

        /// <summary>
        /// 查看Event頁
        /// </summary>
        public ActionResult Event(int? Id, string code = null)
        {
            if (Id == null)
                return Content("請輸入活動編號");

            var events = context.Events.Find(Id);

            if (events == null)
                return Content("找不到這個活動，可能已被發起起刪除。");

            // 還有這個Event的所有留言？如何做到聊天室一樣，會有即時顯示別人留言的效果？
            // 如果進入者為發起者的話，給予她標這留言的權限(在留言旁展示多一個按鈕)
            if (events.Public_Event == false && code == null)
                return Content("這個活動不是公開的，請由正確網址進入");

            ViewBag.Id = events.Id;
            ViewBag.Picture = events.Picture;
            ViewBag.Event_Status = events.Event_Status;

            //ViewBag.Comments = context.Comments.Where(n => n.Event)



            return View(events);
        }

        /// <summary>
        /// 查看我所發起的活動，可連去該Event頁。
        /// </summary>
        public ActionResult QueryMyEvent()
        {
            List<Event> events = context.Events.Where(n => n.StartBy_Who == Environment.UserName).ToList();
            events.Reverse();
            return View(events);
        }

        [HttpPost]
        public string ChangeEventStatus(int? Id)
        {
            if (Id == null)
                return "沒輸入活動編號";

            var thisEvent = context.Events.Find(Id);

            if (thisEvent == null)
                return "找不到這個活動";

            if (thisEvent.Event_Status)
                thisEvent.Event_Status = false;
            else
                thisEvent.Event_Status = true;

            context.SaveChanges();

            return "修改成功";
        }

        /// <summary>
        /// 顯示我的留言，可連去該Event頁，或刪除自己的留言
        /// </summary>
        public ActionResult QueryMyComment()
        {
            List<Comment> comments = context.Comments.Where(n => n.By_Who == Environment.UserName).OrderByDescending(n => n.Time).ToList();
            
            // 這個View 可能用反向查詢還可以找到 Event_Name 和 Evemt_Id
            return View(comments);
        }

        public ActionResult DelMyComment(int? Id)
        {
            if (Id == null)
                return Content("沒輸入要刪除的留言編號");

            var comment = context.Comments.Find(Id);

            if (comment == null)
                return Content("找不到留言");

            context.Comments.Remove(comment);

            context.SaveChanges();
            return Redirect($"~/ChattingBoard/QueryMyComment");
        }

        /// <summary>
        /// 刪除我的活動
        /// </summary>
        public ActionResult DelMyEvent(int? Id)
        {
            if (Id == null)
                return Content("沒輸入活動編號");

            var thisEvent = context.Events.Find(Id);

            if (thisEvent == null)
                return Content("找不到這個活動");

            context.Events.Remove(thisEvent);
            context.SaveChanges();

            //不刪除留言和子留言，但如果留言者想要打開這個活動的話，就告訴他們活動已刪除。

            return Redirect($"~/ChattingBoard/QueryMyEvent");
        }


    }
}