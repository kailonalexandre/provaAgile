using CandidateTesting.KailonAlexandre.DTO;
using CandidateTesting.KailonAlexandre.Services.Interface;


namespace CandidateTesting.KailonAlexandre.Services
{
    public class AgoraLogBuilderService : ILogBuilderService
    {
        public List<LogModel> BuildModel(string response)
        {
            //TODO: CONVERT RESPONSE AGORALOG TO MODEL
            throw new NotImplementedException(); ;
        }

        public bool WriteLog(string path, List<LogModel> Logs)
        {
            if (Logs == null || Logs.Count == 0)
                return false;
            if (path.StartsWith('.'))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + path.Remove(0, 1);
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            if (!System.IO.Directory.Exists(Path.GetDirectoryName(path)))
                return false;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("#Version: 1.0 ");
                writer.WriteLine($"#Date:{DateTime.UtcNow} ");
                writer.WriteLine("#Fields: provider  http-method  status-code  uri-path  time-taken  response-size  cache-status ");

                foreach (var line in Logs)
                {
                    writer.Write($"\"{line.Provider}\" ");
                    writer.Write($"{line.StatusCode} ");
                    writer.Write($"{line.CacheStatus} ");
                    writer.Write($"{line.MethodHttp} ");
                    writer.Write($"{line.UriPath} ");
                    writer.Write($"{line.ResponseSize} ");
                    writer.Write($"{line.CacheStatus} \r \n");
                }
            }
            return true;
        }
    }
}
