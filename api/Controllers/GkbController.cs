using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public abstract class GkbController : ControllerBase
{
    protected IActionResult Status_500_Internal_Server_Error => StatusCode(StatusCodes.Status500InternalServerError);

    protected string GetErrorMessage(string controllerName, string methodName)
    {
        return $"In {controllerName} On {methodName}";
    }
}