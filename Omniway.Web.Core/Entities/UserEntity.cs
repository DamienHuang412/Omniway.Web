using Microsoft.EntityFrameworkCore;

namespace Omniway.Web.Core.Entities;

[PrimaryKey(nameof(Id))]
internal class UserEntity
{
    public int Id { get; set; }
    
    public string UserName { get; set; }

    public string Password { get; set; }
}