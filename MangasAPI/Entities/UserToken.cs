﻿namespace MangasAPI.Entities
{
    public class UserToken
    {
        public string? Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
