using Bogus;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Framework;
namespace Test;

public class GETBooks
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task GetAllBooksTest()
    {
        Helper.TriggerRebuild();
        var expected = new List<Object>();
        for (var i = 1; i < 10; i++)
        {
            var book = new Book()
            {
                BookId = i,
                Title = new Faker().Random.Word(),
                Publisher = new Faker().Random.Word(),
                CoverImgUrl = new Faker().Random.Word(),
            };
            expected.Add(book);
            var sql = $@"INSERT INTO library.books (title, publisher, coverimgurl) 
                        VALUES (@title, @publisher, @coverImgUrl);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, book);
            }
        }

        var response = await _httpClient.GetAsync("http://localhost:5000/api/books");
        var content = await response.Content.ReadAsStringAsync();
        var actualBook = JsonConvert.DeserializeObject<IEnumerable < Book >> (content)!;

        using (new AssertionScope())
        {
            actualBook.Should().BeEquivalentTo(expected, Helper.MyBecause(actualBook, expected));
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

}