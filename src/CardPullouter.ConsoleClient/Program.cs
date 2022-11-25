using CardPullouter.Core.Services;
using System.Text;
using CardPullouter.ConsoleClient;
using CardPullouter.Core;

Console.OutputEncoding = Encoding.UTF8;

var fileService = new FileService();
var htmlService = new HtmlService();
var parserService = new ParserService();
var excelService = new ExcelService();
var consoleOutputService = new ConsoleOutputService();

var pullouter = new Pullouter(excelService, fileService, htmlService, parserService, consoleOutputService);

await pullouter.StartAsync();

