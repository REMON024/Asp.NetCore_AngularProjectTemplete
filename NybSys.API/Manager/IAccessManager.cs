using Microsoft.AspNetCore.Mvc;
using NybSys.Models.ViewModels;
using System.Threading.Tasks;

namespace NybSys.API.Manager
{
    public interface IAccessManager
    {
        Task<IActionResult> GetAllAccessList(ApiCommonMessage message);
        Task<IActionResult> SaveUpdateController(ApiCommonMessage message);
        Task<IActionResult> GetAllController(ApiCommonMessage message);






        Task<IActionResult> SaveUpdateAction(ApiCommonMessage message);
        Task<IActionResult> GetAllAction(ApiCommonMessage message);


        Task<IActionResult> SaveOrUpdateAccessControl(ApiCommonMessage message);
        Task<IActionResult> GetAccessControlByRole(ApiCommonMessage message);
        Task<bool> VerifyAccess(string roleName, string controllerName, string actionName);
        Task<bool> VerifyAccessbyRole(string roleName, string controllerName, string actionName);

    }
}
