using System.CommandLine;

namespace IalfNET;

class Program
{
    static async Task<int> Main(string[] args)
    {
        /*
            --file-log — путь к файлу с логами
            --file-output — путь к файлу с результатом
            --address-start —  нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса
            --address-mask — маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start
            --time-start —  нижняя граница временного интервала
            --time-end — верхняя граница временного интервала.
        */

        var inputFileOption = new Option<FileInfo?>(
            name: "--file-log",
            description: "The path to the file with logs")
        {
            IsRequired = true,
            Arity = ArgumentArity.ExactlyOne
        };

        var outputFileOption = new Option<FileInfo?>(
            name: "--file-out",
            description: "The path to an output file");

        var addressStartOption = new Option<FileInfo?>(
            name: "--address-start",
            description: "Lower limit of address range. Optional.");

        var addressMask = new Option<FileInfo?>(
           name: "--address-mask",
           description: "Subnet mask. Optional.");

        var rootCommand = new RootCommand("Couning and filtering options for IP access logs");
        rootCommand.AddOption(inputFileOption);

        rootCommand.SetHandler(inputFile => ReadFile(inputFile!), inputFileOption);

        return await rootCommand.InvokeAsync(args);
    }

    static void ReadFile(FileInfo file)
    {
        File.ReadLines(file.FullName).ToList()
            .ForEach(line => Console.WriteLine(line));
    }
}