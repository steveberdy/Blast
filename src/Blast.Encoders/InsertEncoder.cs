using System;
using System.Linq;
using System.Collections.Generic;

namespace Blast.Encoders
{
    public partial class InsertEncoder : IEncoder
    {
        private readonly Random random = new();

        public string Encode(string text)
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

            return string.Join(" ", outChunks);
        }

        public string Decode(string text)
        {
            IEnumerable<string> chunks = text.Split(" ");
            List<string> outChunks = new();

            while (chunks.Count() > 0)
            {
                string chunk = chunks.First();
                outChunks.Add(chunk);
                chunks = chunks.Skip(chunk.Length + 1);
            }

            return string.Join(" ", outChunks);
        }
    }
}