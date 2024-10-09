namespace Practice.Web.App.Interfaces;

public interface IAllowlistManager
{
    void AddAllowlist(string userName);

    void RemoveAllowlist(string userName);
    
    bool Authorize(string userName);
}