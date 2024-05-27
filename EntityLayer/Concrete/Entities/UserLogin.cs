using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class UserLogin
    {
     
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Description { get; set; } = null!;

        public DateTime Date { get; init; } = DateTime.Now;

        public LoginType Type { get; init; }

        public AppUser User { get; init; }
    }

    public enum LoginType
    {

        Login,
        Logout
    }
}
