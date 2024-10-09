using System.Text.Json;
using Practice.Web.Api.Interfaces;

namespace Practice.Web.Api.Managers;

public class AllowlistManager : IAllowlistManager
{
    private const string Allowlist = "Allowlist";
    private readonly ISession _session;

    public AllowlistManager(IHttpContextAccessor httpContextAccessor)
    {
        _session = httpContextAccessor.HttpContext!.Session;
    }

    public bool Authorize(string userName)
    {
        var allowList = GetAllowlist();

        return allowList.Contains(userName);
    }

    public void AddAllowlist(string userName)
    {
        var allowList = GetAllowlist();

        allowList.Add(userName);

        UpdateAllowlist(allowList);
    }

    public void RemoveAllowlist(string userName)
    {
        var allowList = GetAllowlist();

        allowList.Remove(userName);

        UpdateAllowlist(allowList);
    }

    private void UpdateAllowlist(List<string> allowList)
    {
        _session.SetString(Allowlist, JsonSerializer.Serialize(allowList.Distinct()));
    }

    private List<string> GetAllowlist()
    {
        var allowlistJson = _session.GetString(Allowlist);

        var allowList = string.IsNullOrEmpty(allowlistJson)
            ? []
            : JsonSerializer.Deserialize<List<string>>(allowlistJson) ?? [];
        return allowList;
    }
}