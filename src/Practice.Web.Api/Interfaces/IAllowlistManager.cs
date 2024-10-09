namespace Practice.Web.Api.Interfaces;

public interface IAllowlistManager
{
    void AddAllowlist(string userName);

    void RemoveAllowlist(string userName);
    
    bool Authorize(string userName);
}