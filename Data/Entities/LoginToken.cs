#nullable disable
namespace StudyMate.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LoginToken
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Guid Token { get; set; }
	[ForeignKey("UserNavigation")]
	public string Username { get; set; }
	public DateTime Created { get; set; }

	public virtual User UserNavigation { get; set; }
}