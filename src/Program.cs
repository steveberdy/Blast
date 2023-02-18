using System;
using System.IO;
using System.CommandLine;
using Blast.Encoders;

namespace Blast
{
    class Program
    {
        private static readonly Option<string> rawInputOption = new(new[] { "-i", "--input" }, "Raw input, optional");
        private static readonly Option<FileInfo> fileInputOption = new Option<FileInfo>(new[] { "-f", "--file" }, "File to decode").ExistingOnly();
        private static readonly Option<EncoderType> typeOption = new(new[] { "-t", "--type" }, "Type of decoder to use, optional");

        static void Main(string[] args)
        {
            Command decodeCommand = new("decode", "Decodes encoded file")
            {
                rawInputOption,
                fileInputOption,
                typeOption
            };
            decodeCommand.SetHandler((i, file, type) =>
            {
                if (!string.IsNullOrEmpty(i))
                {
                    Console.WriteLine(GetEncoder(type).Decode(i));
                }
                else
                {
                    string outputPath = Path.GetFileNameWithoutExtension(file.FullName) + ".txt";
                    File.WriteAllText(outputPath, GetEncoder(type).Decode(File.ReadAllText(file.FullName)));
                }
            }, rawInputOption, fileInputOption, typeOption);

            Command encodeCommand = new("encode", "Encodes file")
            {
                rawInputOption,
                fileInputOption,
                typeOption
            };
            encodeCommand.SetHandler((i, file, type) =>
            {
                if (!string.IsNullOrEmpty(i))
                {
                    Console.WriteLine(GetEncoder(type).Encode(i));
                }
                else
                {
                    string outputPath = Path.GetFileNameWithoutExtension(file.FullName) + ".blst";
                    File.WriteAllText(outputPath, GetEncoder(type).Encode(File.ReadAllText(file.FullName)));
                }
            }, rawInputOption, fileInputOption, typeOption);

            RootCommand rootCommand = new()
            {
                decodeCommand,
                encodeCommand
            };

            rootCommand.Invoke(args);
        }

        static IEncoder GetEncoder(EncoderType type)
        {
            return type switch
            {
                EncoderType.C => new CompassEncoder(),
                EncoderType.CDeep => new CompassDeepEncoder(),
                EncoderType.Insert => new InsertEncoder(),
                _ => new CompassDeepEncoder()
            };
        }
    }

    enum EncoderType
    {
        C = 0,
        CDeep = 1,
        Insert = 2
    }
}