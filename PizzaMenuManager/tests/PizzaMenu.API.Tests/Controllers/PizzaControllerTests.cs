using Newtonsoft.Json;
using PizzaMenu.API.ResponseModels;
using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using PizzaMenu.Fixtures;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaMenu.API.Tests.Controllers
{
  public class PizzaControllerTests : IClassFixture<InMemoryApplicationFactory<Startup>>
  {
    private readonly InMemoryApplicationFactory<Startup> _factory;

    public PizzaControllerTests(InMemoryApplicationFactory<Startup> factory)
    {
      _factory = factory ?? throw new ArgumentNullException( nameof( factory ) );
    }

    [Theory]
    [InlineData( "/api/pizzas/?pageSize=1&pageIndex=0", 1, 0 )]
    [InlineData( "/api/pizzas/?pageSize=2&pageIndex=0", 2, 0 )]
    [InlineData( "/api/pizzas/?pageSize=1&pageIndex=1", 1, 1 )]
    public async Task Get_Should_Return_Paginated_Data( string url, int pageSize, int pageIndex )
    {
      HttpClient client = _factory.CreateClient();
      HttpResponseMessage response = await client.GetAsync( url );

      response.EnsureSuccessStatusCode();

      string responseContent = await response.Content.ReadAsStringAsync();
      PaginatedPizzaResponseModel<PizzaResponse> responseEntity = JsonConvert.DeserializeObject<PaginatedPizzaResponseModel<PizzaResponse>>( responseContent );

      responseEntity.PageIndex.ShouldBe( pageIndex );
      responseEntity.PageSize.ShouldBe( pageSize );
      responseEntity.Data.Count().ShouldBe( pageSize );
    }

    [Fact]
    public async Task Get_By_Id_Should_Return_Pizza_Data()
    {
      const string guid = "3eb00b42-a9f0-4012-841d-70ebf3ab7474";
      HttpClient client = _factory.CreateClient();
      HttpResponseMessage response = await client.GetAsync( $"/api/pizzas/{guid}" );

      response.EnsureSuccessStatusCode();
      string responseContent = await response.Content.ReadAsStringAsync();
      Pizza responseEntity = JsonConvert.DeserializeObject<Pizza>( responseContent );

      responseEntity.ShouldNotBeNull();
    }

    [Theory]
    [LoadData( "pizza" )]
    public async Task Add_Should_Create_New_Record_Loaded( AddPizzaRequest request )
    {
      HttpClient client = _factory.CreateClient();

      StringContent httpContent = new StringContent(
        JsonConvert.SerializeObject( request ), Encoding.UTF8, "application/json" );

      HttpResponseMessage response = await client.PostAsync( $"/api/pizzas", httpContent );

      response.EnsureSuccessStatusCode();
      response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task Add_Should_Create_New_Record()
    {
      AddPizzaRequest request = new AddPizzaRequest
      {
        Name = "New Test Pizza"
      };

      HttpClient client = _factory.CreateClient();

      StringContent httpContent = new StringContent( 
        JsonConvert.SerializeObject( request ), Encoding.UTF8, "application/json" );
      HttpResponseMessage response = await client.PostAsync( $"/api/pizzas", httpContent );

      response.EnsureSuccessStatusCode();
      response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task Update_Should_Modify_Existing_Pizza()
    {
      HttpClient client = _factory.CreateClient();
      EditPizzaRequest request = new EditPizzaRequest
      {
        Guid = new Guid( "3eb00b42-a9f0-4012-841d-70ebf3ab7474" ),
        Name = "Updated Pizza Name"
      };

      StringContent httpContent = new StringContent(
        JsonConvert.SerializeObject( request ), Encoding.UTF8, "application/json" );
      HttpResponseMessage response = await client.PutAsync( $"/api/pizzas/{request.Guid}", httpContent );

      response.EnsureSuccessStatusCode();

      string responseContent = await response.Content.ReadAsStringAsync();
      Pizza responseEntity = JsonConvert.DeserializeObject<Pizza>( responseContent );

      responseEntity.Name.ShouldBe( request.Name );
    }

    [Theory]
    [LoadData("pizza")]
    public async Task Delete_Should_Return_No_Content_When_Called_With_Right_Guid(DeletePizzaRequest request )
    {
      HttpClient client = _factory.CreateClient();
      HttpResponseMessage response = await client.DeleteAsync( $"/api/pizzas/{request.Guid}" );
      response.StatusCode.ShouldBe( HttpStatusCode.NotFound );
    }

    [Fact]
    public async Task Delete_Should_Return_Not_Found_When_Called_With_No_ExistingGuid()
    {
      HttpClient client = _factory.CreateClient();
      HttpResponseMessage response = await client.DeleteAsync( $"/api/pizzas/{Guid.NewGuid()}" );
      response.StatusCode.ShouldBe( HttpStatusCode.NotFound );
    }
  }
}
