using Microsoft.AspNetCore.Mvc;
using UnitOfWorkExample.Entities;
using UnitOfWorkExample.UnitOfWork;

namespace UnitOfWorkExample.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AddressesController : Controller
{
    private readonly ILogger<AddressesController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public AddressesController(
        ILogger<AddressesController> logger, 
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    
    public async Task<IActionResult> CreateAsync(Address address)
    {
        return Ok();
    }
}