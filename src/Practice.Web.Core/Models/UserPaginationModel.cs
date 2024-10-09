namespace Practice.Web.Core.Models;

public class UserPaginationModel
{
    public UserModel[] Data { get; set; }

    public int TotalCount { get; set; }
}