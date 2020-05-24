using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace Films.Website.Domain
{
    [Table("users", Schema = "catalog")]
    public class User : IdentityUser
    {

        public virtual ICollection<Film> Films { get; set; }
    }
}
