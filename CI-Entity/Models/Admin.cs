﻿using System;
using System.Collections.Generic;

namespace CI_Entity.Models;

public partial class Admin
{
    public long AdminId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public long? RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Role? Role { get; set; }
}
