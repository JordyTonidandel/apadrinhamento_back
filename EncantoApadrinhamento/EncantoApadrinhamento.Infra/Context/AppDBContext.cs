﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EncantoApadrinhamento.Infra.Context
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }
    }
}