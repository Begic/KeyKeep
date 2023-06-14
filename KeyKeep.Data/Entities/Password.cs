﻿using System.ComponentModel.DataAnnotations;

namespace KeyKeep.Data.Entities;

public class Password
{
    public int Id { get; set; }
    [MaxLength(200)] public string? Title { get; set; }
    [MaxLength(200)] public string? Description { get; set; }
    [MaxLength(200)] public byte[] UserPassword { get; set; }
    [MaxLength(200)] public byte[] UserName { get; set; }
    [MaxLength(200)] public byte[]? URL { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    public bool IsDeleted { get; set; }

    public List<Key> Keys { get; set; } = new();
}