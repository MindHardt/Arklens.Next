using System.Text.Json;
using System.Text.Json.Serialization;
using Arklens.Next.Core;

namespace Arklens.Next.Extra;

public class AlidEntityJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsAssignableTo(typeof(AlidEntity));

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(AlidEntityConverter<>).MakeGenericType(typeToConvert);
        return Activator.CreateInstance(converterType) as JsonConverter;
    }

    private class AlidEntityConverter<TEntity> : JsonConverter<TEntity> where TEntity: AlidEntity
    {
        public override TEntity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetString() is string alid
                ? AlidEntity.GetRequired<TEntity>(alid)
                : null;

        public override void Write(Utf8JsonWriter writer, TEntity value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.Alid.Text);
    }
}