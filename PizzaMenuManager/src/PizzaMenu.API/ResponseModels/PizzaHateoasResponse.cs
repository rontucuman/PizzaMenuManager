using PizzaMenu.Domain.Responses.Pizza;
using RiskFirst.Hateoas.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PizzaMenu.API.ResponseModels
{
  public class PizzaHateoasResponse : ILinkContainer
  {
    private PizzaResponse _data;
    private Dictionary<string, Link> _links;

    [JsonPropertyName( "_data" )]
    public PizzaResponse Data
    {
      get => _data ??= new PizzaResponse();
      set => _data = value;
    }

    [JsonPropertyName( "_links" )]
    public Dictionary<string, Link> Links
    {
      get => _links ??= new Dictionary<string, Link>();
      set => _links = value;
    }

    public void AddLink( string id, Link link )
    {
      Links.Add( id, link );
    }
  }
}
