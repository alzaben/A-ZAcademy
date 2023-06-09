using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string? RoleName { get; set; }
	}
}
