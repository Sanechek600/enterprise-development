using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace BikeRental.Generator.Kafka.Host.Serialization;

/// <summary>
/// JSON сериализатор для значений сообщений Kafka
/// </summary>
/// <typeparam name="TValue">Тип значения</typeparam>
public class JsonValueSerializer<TValue> : ISerializer<TValue>
{
    public byte[] Serialize(TValue data, SerializationContext context)
    {
        if (data == null)
            return [];

        var json = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json);
    }
}