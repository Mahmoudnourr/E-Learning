using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser :IdentityUser<Guid>
    {
        //public Guid DomainUserId { get; set; }
    }
}