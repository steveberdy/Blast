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
                new Argument<FileInfo>("file", "File to decode").ExistingOnly()
            };
            decodeCommand.Handler = CommandHandler.Create<FileInfo>((file) =>
            {
                string outputPath = file.FullName[..^(file.FullName.Split(".").Last().Length + 1)] + ".txt"; 
                File.WriteAllText(outputPath, new CompassEncoder().Decode(File.ReadAllText(file.FullName)));
            });

            Command encodeCommand = new("encode", "Encodes file")
            {
                new Argument<FileInfo>("file", "File to encode").ExistingOnly()
            };
            encodeCommand.Handler = CommandHandler.Create<FileInfo>((file) =>
            {
                string outputPath = file.FullName[..^(file.FullName.Split(".").Last().Length + 1)] + ".blst";
                File.WriteAllText(outputPath, new CompassEncoder().Encode(File.ReadAllText(file.FullName)));
            });

            RootCommand rootCommand = new()
            {
                decodeCommand,
                encodeCommand
            };

            rootCommand.Invoke(args);
        }
    }
}