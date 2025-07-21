using Microsoft.AspNetCore.Mvc;

namespace Somadhan.API.Controllers;

public class BaseController<TEntity> : ControllerBase where TEntity : class
{
    public BaseController()
    {
        // Base controller constructor logic can go here if needed
    }

    [HttpGet]
    public virtual IActionResult GetAll()
    {
        var collections = new List<TEntity>();
        // This method should be overridden in derived controllers to provide specific functionality
        return Ok(collections);
    }
}

