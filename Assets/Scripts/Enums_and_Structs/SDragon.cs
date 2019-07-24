using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[System.Serializable]
public struct SDragon
{
    public string objectID;
    public int spikesNumber;

    [JsonConverter(typeof(StringEnumConverter))]
    public EElement element;
    [JsonConverter(typeof(StringEnumConverter))]
    public ESex sex;
    [JsonConverter(typeof(StringEnumConverter))]
    public ESkin skin;
}