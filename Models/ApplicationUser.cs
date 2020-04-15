using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MemoPlus.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Memos = new List<Memo>();
            IsDarkMode = false;
        }
        public ICollection<Memo> Memos { get; set; }
        public bool IsDarkMode { get; set; }
    }
}
