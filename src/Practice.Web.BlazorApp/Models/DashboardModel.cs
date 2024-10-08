namespace Practice.Web.BlazorApp.Models;

public class DashboardModel<T> : ResultModel where T: class
{
    public int TotalCount { get; set; }

    public T[] Data { get; set; } = [];
}