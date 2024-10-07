namespace Omniway.Web.Core.Models;

public class ChangePasswordModel
{
    public string UserName { get; set; }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}