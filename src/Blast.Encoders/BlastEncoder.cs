using System;

namespace Blast.Encoders
{
    /// <summary>
    /// Base class for encoders
    /// </summary>
    public abstract class BlastEncoder : IEncoder
    {
        public IEncoder Parent { get; set; } = null;
        public IEncoder Child { get; set; } = null;
        protected Random random = new();

        public virtual string Encode(string text)
        {
            if (Child is null)
            {
                return text;
            }
            return Child.Encode(text);
        }

        public virtual string Decode(string text)
        {
            if (Parent is null)
            {
                return text;
            }
            return Parent.Decode(text);
        }
    }
}