using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ChattingBoard.Models
{
    public class ChattingBoardContext :DbContext
    {
        public ChattingBoardContext() : base("name=ChattingBoardConn") { }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}