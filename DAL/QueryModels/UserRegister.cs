﻿using university.DAL.Enums;

namespace university.DAL.QueryModels;

public class UserRegister
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}