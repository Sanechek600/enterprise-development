using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace BikeRental.Infrastructure.Kafka.Serialization;

/// <summary>
/// JSON десериализатор для значений сообщений Kafka
/// </summary>
/// <typeparam name="TValue">Тип значения</typeparam>
public class JsonValueDeserializer<TValue> : IDeserializer<TValue>
{
    public TValue Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.IsEmpty)
            return default!;

        var json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<TValue>(json)!;
    }
}
