using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
    /// <summary>
    /// <para>Resumo: Enum responsible for define systen user types</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        NORMAL,
        ADMINISTRATOR
    }
}

    
