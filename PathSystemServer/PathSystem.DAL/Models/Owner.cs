﻿using System.Collections.Generic;

namespace PathSystem.DAL.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<OwnerRefreshToken> RefreshTokens { get; set; }
        public ICollection<Route> Routes { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        User = 1,
        Admin = 2
    }
}