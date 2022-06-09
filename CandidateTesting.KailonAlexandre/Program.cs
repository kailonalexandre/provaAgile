using CandidateTesting.KailonAlexandre.Services;
using CandidateTesting.KailonAlexandre.Services.Interface;

namespace CandidateTesting.KailonAlexandre
{
    public class Program
    {
        public delegate string cmdEvent(params object[] args);

        static Dictionary<string, cmdEvent> cmd = new Dictionary<string, cmdEvent>();

        private static ILogBuilderService? logService;
        static void Main(string[] args)
        {
            Console.WriteLine("Type \"help\" to see avaliable commands. ");
            cmd.Add("convert", cmd_Convert);
            cmd.Add("help", cmd_Help);
            cmd.Add("exit", cmd_Exit);

            do
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;
                var command = input.Split(" ");
                if (cmd.ContainsKey(command[0]))
                {
                    Console.WriteLine(cmd[command[0]](command));
                }
            }
            while (true);

        }
        static string cmd_Convert(params object[] args)
        {
            string content;

            if (args.Length != 3)
            {
                return "Invalid Command";
            }
            string? URL = args[1].ToString();
            string? path = args[2].ToString();

            if (string.IsNullOrEmpty(URL) || string.IsNullOrEmpty(path))
                return "Invalid Command";

            HttpClient client = new HttpClient();
            using (HttpResponseMessage response = client.GetAsync(URL).Result)
            using (HttpContent responseContent = response.Content)
            {
                content = responseContent.ReadAsStringAsync().Result;
                logService = new CDNLogBuilderService();
                var CDNLog = logService.BuildModel(content);

                if (CDNLog is not null)
                {
                    logService = new AgoraLogBuilderService();
                    var result = logService.WriteLog(path, CDNLog);
                    if (!result)
                        return "Error!";
                }
                else
                    return "File conversion error";
                return "Success";
            }

        }
        static string cmd_Exit(params object[] args)
        {
            Environment.Exit(0);
            return "";
        }

        static string cmd_Help(params object[] args)
        {
            return @"Available Commands: 
                     convert <URL> <PATH + FILENAME>
                     help
                     exit";
        }
    }
}