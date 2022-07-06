using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServerApp.WebApp.Base.Extensions;

public static class ApiCollectionExtensions
{
    /// <summary>
    /// Added all errors from <see cref="IdentityResult"/>
    /// </summary>
    /// <param name="modelStateDictionary"><see cref="ModelStateDictionary"/></param>
    /// <param name="result"><see cref="IdentityResult"/></param>
    /// <returns><see cref="ModelStateDictionary"/></returns>
    public static ModelStateDictionary AddErrors(this ModelStateDictionary modelStateDictionary, IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return modelStateDictionary;
    }
}