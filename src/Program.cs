using System;
using System.IO;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;
using Blast.Encoders;

namespace Blast
{
    class Program
    {
        static void Main(string[] args)
        {
            Command decodeCommand = new("decode", "Decodes encoded file")
            {
                new Option<string>(new[] { "-i" }, "n"),
                new Option<FileInfo>(new[] { "-f", "--file" }, "File to decode").ExistingOnly(),
                new Option<EncoderType>(new[] { "-t", "--type" }, "Type of decoder to use, optional")
            };
            decodeCommand.Handler = CommandHandler.Create<string, FileInfo, EncoderType>((i, file, type) =>
            {
                if (!string.IsNullOrEmpty(i))
                {
                    Console.WriteLine(GetEncoder(type).Decode(i));
                }
                else
                {
                    string outputPath = file.FullName[..^(file.FullName.Split(".").Last().Length + 1)] + ".txt";
                    File.WriteAllText(outputPath, GetEncoder(type).Decode(File.ReadAllText(file.FullName)));
                }
            });

            Command encodeCommand = new("encode", "Encodes file")
            {
                new Option<string>(new[] { "-i" }, "n"),
                new Option<FileInfo>(new[] { "-f", "--file" }, "File to encode").ExistingOnly(),
                new Option<EncoderType>(new[] { "-t", "--type" }, "Type of encoder to use, optional")
            };
            encodeCommand.Handler = CommandHandler.Create<string, FileInfo, EncoderType>((i, file, type) =>
            {
                if (!string.IsNullOrEmpty(i))
                {
                    Console.WriteLine(GetEncoder(type).Encode(i));
                }
                else
                {
                    string outputPath = file.FullName[..^(file.FullName.Split(".").Last().Length + 1)] + ".blst";
                    File.WriteAllText(outputPath, GetEncoder(type).Encode(File.ReadAllText(file.FullName)));
                }
            });

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