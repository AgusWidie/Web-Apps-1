﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace APIRetail.Models.Database;

public partial class SendEmailFail
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string Message { get; set; }

    public string ErrorDescription { get; set; }

    public string CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}