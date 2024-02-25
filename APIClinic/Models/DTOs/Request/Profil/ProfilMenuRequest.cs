﻿namespace APIClinic.Models.DTOs.Request
{
    public class ProfilMenuRequest
    {
        public long? Id { get; set; }

        public long? ProfilId { get; set; }

        public long? ParentMenuId { get; set; }

        public long? MenuId { get; set; }

        public string CreateBy { get; set; } = "";

        public DateTime? CreateDate { get; set; }

        public string UpdateBy { get; set; } = "";

        public DateTime? UpdateDate { get; set; }
    }
}