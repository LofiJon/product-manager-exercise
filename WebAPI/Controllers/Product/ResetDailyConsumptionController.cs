using Application.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers.Product;

[ApiController]
[Route(PathMapping.Api + PathMapping.Version + PathMapping.Product)]
[Produces("application/json")]
public class ResetDailyConsumptionController : ControllerBase
{
    private readonly IResetDailyConsumption _resetDailyConsumption;

    public ResetDailyConsumptionController(IResetDailyConsumption resetDailyConsumption)
    {
        _resetDailyConsumption = resetDailyConsumption;
    }
    
    /// <summary>
    /// Reset daily consumption
    /// </summary>
    [HttpPut("reset-daily-consumption/{id}")]
    public async Task Handle(Guid id)
    {
        await _resetDailyConsumption.Execute(id);
    }
}