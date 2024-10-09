namespace Practice.Web.Mvc.Models;

public class UsersViewModel
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalPage { get; set; }
    
    public int TotalCount { get; set; }

    public UserViewModel[] Data { get; set; }
}