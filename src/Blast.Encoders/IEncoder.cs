namespace Blast.Encoders
{
    public interface IEncoder
    {
        string Encode(string text);
        string Decode(string text);
    }
}