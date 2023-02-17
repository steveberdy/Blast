using System;
using System.Linq;

namespace Blast.Encoders
{
    public class CompassEncoder : IEncoder
    {
        private static readonly string characters = " abcdefghijklmnopqrstuvwyz0123456789";
        private static readonly string numericalCharacters = "0123456789";
        private readonly string symbols = "!@#";
        private readonly string primarySeparator = "-";
        private readonly string secondarySeparator = ".";
        private readonly Random random = new();

        /// <summary>
        /// Creates a <see cref="CompassEncoder" />
        /// </summary>
        public CompassEncoder() { }

        /// <summary>
        /// Creates a <see cref="CompassEncoder" /> with a key.
        /// The key must be at least 5 characters in length and can contain
        /// anything but numerical characters.
        /// </summary>
        public CompassEncoder(string key)
        {
            if (key.Length < 5 || key.Distinct().Count() != key.Length || key.Intersect(numericalCharacters).Any())
            {
                throw new ArgumentException("Key must be valid");
            }

            symbols = key[..^2];
            primarySeparator = key[^1].ToString();
            secondarySeparator = key[^2].ToString();
        }

        public string Encode(string text)
        {
            string full = string.Empty;
            string character;
            for (int i = 0; i < text.Length; i++)
            {
                int index = characters.IndexOf(text[i]);
                string next = i == text.Length - 1 ? string.Empty : primarySeparator;
                if (index == -1)
                {
                    if (characters.Contains(text[i].ToString().ToLower()))
                    {
                        // for capital characters
                        index = characters.IndexOf(text[i].ToString().ToLower());
                        character = Transform((index + 1) * 10 + random.Next(10)) + secondarySeparator + symbols[0] + next;
                    }
                    else
                    {
                        // for special characters
                        character = Transform(text[i]) + secondarySeparator + symbols[1] + next;
                    }
                }
                else
                {
                    // for normal characters
                    character = Transform((index + 1) * 10 + random.Next(10)) + next;
                }
                full += character;
            }

            return full;
        }

        public string Decode(string text)
        {
            string[] slices = text.Split(primarySeparator);

            string full = string.Empty;
            for (int i = 0; i < slices.Length; i++)
            {
                int secondarySeparatorIndex = slices[i].IndexOf(secondarySeparator);
                if (secondarySeparatorIndex > 0)
                {
                    // Get what symbol the suffix is
                    switch (symbols.IndexOf(slices[i][secondarySeparatorIndex + 1]))
                    {
                        case 0:
                            // it's a capital letter
                            full += characters[BaseTransformer.StringToInt(slices[i][..^2], symbols) / 10 - 1].ToString().ToUpper();
                            break;
                        case 1:
                            // it's a special character
                            full += (char)BaseTransformer.StringToInt(slices[i][..^2], symbols);
                            break;
                    }
                }
                else
                {
                    full += characters[BaseTransformer.StringToInt(slices[i], symbols) / 10 - 1];
                }
            }

            return full;
        }

        private string Transform(int value)
        {
            string enc = BaseTransformer.IntToString(value, symbols);

            for (int i = 0; i < symbols.Length; i++)
            {
                enc = enc.Replace(i.ToString(), symbols[i].ToString());
            }

            return enc;
        }
    }
}