﻿namespace VebtechTest.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
}
