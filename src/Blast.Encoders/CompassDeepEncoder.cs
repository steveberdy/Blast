using System;
using System.Linq;
using System.Collections.Generic;

namespace Blast.Encoders
{
    public class CompassDeepEncoder : BlastEncoder
    {
        private static readonly string[] outCombos = { "!!", "!@", "!#", "@!", "@@",
                    "@#", "#!", "#@", "##", ".@", ".#", "!.@", "!.#", "@.@",
                    "@.#", "#.@", "#.#", "!", "@", "#", "|" };
        private const string outChars = "0123456789abcdefghijk";
        private readonly static CompassEncoder subEncoder = new();

        /// <summary>
        /// Encodes text.
        /// </summary>
        /// <returns>
        /// The encoded text.
        /// </returns>
        /// <param name="text">Text to encode</param>
        public override string Encode(string text)
        {
            text = subEncoder.Encode(text);
            string enc = string.Empty;

            while (text.Length > 0)
            {
                for (int c = 0; c < outCombos.Length; c++)
                {
                    if (text.Length >= outCombos[c].Length && text[..outCombos[c].Length] == outCombos[c])
                    {
                        text = text[outCombos[c].Length..];
                        enc += outChars[c];
                    }
                }
            }

            return base.Encode(enc);
        }

        /// <summary>
        /// Decodes text. The text must have been
        /// encoded through this encoder, or the
        /// decoded text will be corrupted, or an
        /// error can be thrown.
        /// </summary>
        /// <returns>
        /// The decoded text.
        /// </returns>
        /// <param name="raw">Raw encoding</param>
        public override string Decode(string raw)
        {
            string dec = string.Empty;

            for (int i = 0; i < raw.Length; i++)
            {
                dec += outCombos[outChars.IndexOf(raw[i])];
            }

            return base.Decode(subEncoder.Decode(dec));
        }
    }
}