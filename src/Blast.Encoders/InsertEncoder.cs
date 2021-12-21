using System;
using System.Linq;
using System.Collections.Generic;

namespace Blast.Encoders
{
    public partial class InsertEncoder : BlastEncoder
    {
        public override string Encode(string text)
        {
            text = text.Replace(",", "").Replace("!", "").Replace(".", "");
            IEnumerable<string> chunks = text.Split(" ");
            List<string> outChunks = new();

            foreach (string chunk in chunks)
            {
                outChunks.Add(chunk);
                for (int j = 0; j < chunk.Length; j++)
                {
                    outChunks.Add(words[random.Next(words.Length)]);
                }
            }

            return base.Encode(string.Join(" ", outChunks));
        }

        public override string Decode(string text)
        {
            IEnumerable<string> chunks = text.Split(" ");
            List<string> outChunks = new();

            while (chunks.Count() > 0)
            {
                string chunk = chunks.First();
                outChunks.Add(chunk);
                chunks = chunks.Skip(chunk.Length + 1);
            }

            return base.Decode(string.Join(" ", outChunks));
        }
    }
}