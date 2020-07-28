using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PizzaMenu.Fixtures
{
  public class LoadDataConverter : JsonConverter
  {
    private readonly Dictionary<string, string> _propertyMappings = new Dictionary<string, string>()
    {
      {"RowGuid", "Guid"}
    };

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      object instance = Activator.CreateInstance(objectType);
      List<PropertyInfo> props = objectType.GetTypeInfo().DeclaredProperties.ToList();

      JObject jObject = JObject.Load(reader);

      foreach (JProperty jProperty in jObject.Properties())
      {
        if (!_propertyMappings.TryGetValue(jProperty.Name, out string name))
        {
          name = jProperty.Name;
        }

        PropertyInfo prop = props.FirstOrDefault(pi =>
          pi.CanWrite && pi.Name == name);

        prop?.SetValue(instance, jProperty.Value.ToObject(prop.PropertyType, JsonSerializer.CreateDefault()));
      }

      return instance;
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType.GetTypeInfo().IsClass;
    }
  }
}