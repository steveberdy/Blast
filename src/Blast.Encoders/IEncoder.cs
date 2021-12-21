namespace Blast.Encoders
{
    public interface IEncoder
    {
        IEncoder Child { get; set; }
        IEncoder Parent { get; set; }
        string Encode(string text);
        string Decode(string text);
    }
}