using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO;

public class UpdatePermissionDto
{
	[Required(ErrorMessage = "UserName is required")]
	public string UserName { get; set; }
}
