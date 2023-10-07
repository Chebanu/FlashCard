﻿using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO;

public class Login
{
	[Required(ErrorMessage = "UserName is required")]
	public string UserName { get; set; }

	[Required(ErrorMessage = "Password is required")]
	public string Password { get; set; }
}
