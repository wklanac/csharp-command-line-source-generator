using System.CommandLine;
using Newtonsoft.Json;

namespace CommandLineGenerator.Json;

public class ArgumentArityReadonlyConverter: JsonConverter<ArgumentArity>
{
    public override void WriteJson(JsonWriter writer, ArgumentArity value, JsonSerializer serializer)
    {
        throw new NotSupportedException("Serializing ArgumentArity types is not supported.");
    }

    public override ArgumentArity ReadJson(
        JsonReader reader, Type objectType, ArgumentArity existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        if (hasExistingValue)
        {
            throw new NotSupportedException(
                "Use of ArgumentArity converter not permitted when a value already exists.");
        }

        var minimumValues = reader.ReadAsInt32();
        var maximumValues = reader.ReadAsInt32();

        if (!minimumValues.HasValue || !maximumValues.HasValue)
        {
            throw new ArgumentException("ArgumentArity JSON is missing one or both value limits.");
        }

        return new ArgumentArity(minimumValues.Value, maximumValues.Value);
    }
}