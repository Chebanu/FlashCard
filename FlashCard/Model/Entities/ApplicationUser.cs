﻿using FlashCard.Model.Domain;
using Microsoft.AspNetCore.Identity;

namespace FlashCard.Model.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
