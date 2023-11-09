using Microsoft.AspNetCore.Mvc;
using UnitOfWorkExample.Entities;
using UnitOfWorkExample.UnitOfWork;

namespace UnitOfWorkExample.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(
        ILogger<UsersController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _unitOfWork.Users.All();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        var item = await _unitOfWork.Users.GetById(id);

        if(item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            user.Id = Guid.NewGuid();

            try
            {
                await _unitOfWork.StartTransaction();
                _logger.LogInformation("Transaction Beginned.");
                
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.SaveChanges();
               
                await _unitOfWork.CommitTransaction();
                _logger.LogInformation("Transaction Commited.");
                return CreatedAtAction("GetItem", new { user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Transaction Rolled back.");
                await _unitOfWork.RollBackTransaction(); 
                return new JsonResult("Something Went Wrong: " + ex.Message) { StatusCode = 500 };
            }

        }

        return new JsonResult("Somethign Went wrong") {StatusCode = 500};
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(Guid id, User user)
    {
        if(id != user.Id)
            return BadRequest();

        await _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        var item = await _unitOfWork.Users.GetById(id);

        if(item == null)
            return NotFound();

        var result = await _unitOfWork.Users.Delete(id);
        await _unitOfWork.SaveChanges();

        return Ok(result);
    }
}