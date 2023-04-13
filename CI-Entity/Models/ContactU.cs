using System;
using System.Collections.Generic;

namespace CI_Entity.Models;

public partial class ContactU
{
    public long ContactId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }
}
