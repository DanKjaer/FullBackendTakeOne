using Infrastructure.models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Api.Controllers;

[ApiController]
public class BookController : ControllerBase
{
    private readonly Service _service;

    public BookController(Service service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("/api/books")]
    public IEnumerable<Book> getALlBooks()
    {
        return _service.getALlBooks();
    }
}