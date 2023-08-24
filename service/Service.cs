using Infrastructure;
using Infrastructure.models;

namespace service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }
    public IEnumerable<Book> getALlBooks()
    {
        try
        {
            return _repository.getALlBooks();
        }
        catch (Exception e)
        {
            throw new Exception("Could not get books");
        }
    }
}