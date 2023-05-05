﻿using System.ComponentModel.DataAnnotations;

namespace KeyKeep.Data.Entities;

public class Passwort
{
    public int Id { get; set; }
    [MaxLength(200)] public string Title { get; set; }
    [MaxLength(200)] public string Description { get; set; }
    [MaxLength(200)] public string Password { get; set; }
    [MaxLength(200)] public string UserName { get; set; }
    [MaxLength(200)] public string URL { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    public bool IsDeleted { get; set; }
}