using System;
using System.Collections.Generic;

namespace CI_Entity.Models;

public partial class Role
{
    public long RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
