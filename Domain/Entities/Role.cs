﻿namespace VebtechTest.Domain.Entities;

public class Role : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<User>? Users { get; set; }
}
