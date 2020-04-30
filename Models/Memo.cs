using System;

namespace MemoPlus.Models
{
    public class Memo
    {
        public Memo()
        {
            CreationDate = DateTime.Now;
            IsFixed = false;
        }
        public int MemoId { get; set; }
        public string MemoTitle { get; set; }
        public string MemoText { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsFixed { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}