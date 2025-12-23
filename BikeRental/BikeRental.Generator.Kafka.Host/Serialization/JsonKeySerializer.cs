using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace BikeRental.Generator.Kafka.Host.Serialization;

/// <summary>
/// JSON сериализатор для ключей сообщений Kafka
/// </summary>
/// <typeparam name="TKey">Тип ключа</typeparam>
public class JsonKeySerializer<TKey> : ISerializer<TKey>
{
    public byte[] Serialize(TKey data, SerializationContext context)
    {
        if (data == null)
            return [];

        var json = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json);
    }
}