using CandidateTesting.KailonAlexandre.DTO;
using CandidateTesting.KailonAlexandre.Services.Interface;

namespace CandidateTesting.KailonAlexandre.Services
{
    public class CDNLogBuilderService : ILogBuilderService
    {
        public List<LogModel> BuildModel(string response)
        {
            if (string.IsNullOrEmpty(response))
                return null;
            var CDNResponse = response.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            List<LogModel> logModels = new List<LogModel>();

            foreach (var line in CDNResponse)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                LogModel model = new LogModel();

                var CDNFields = line.Split('|');
                var HttpFields = CDNFields[3].Replace("\"", "").Split(' ');

                model.Provider = "MINHA CDN";
                model.MethodHttp = HttpFields[0];
                model.StatusCode = CDNFields[1];
                model.UriPath = HttpFields[1];
                model.TimeTake = Convert.ToInt32(CDNFields[4].Split('.')[0]);
                model.ResponseSize = CDNFields[0];
                if (CDNFields[2] == "INVALIDATE")
                    model.CacheStatus = "REFRESH_HIT";
                else
                    model.CacheStatus = CDNFields[2];
                logModels.Add(model);
            }

            return logModels;
        }

        public bool WriteLog(string path, List<LogModel> Logs)
        {
            if (Logs == null || Logs.Count == 0)
                return false;

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (var line in Logs)
                {
                    writer.Write($"{line.ResponseSize} |");
                    writer.Write($" {line.StatusCode} | ");
                    writer.Write($"{line.CacheStatus} |");
                    writer.Write($"\"{line.MethodHttp} ");
                    writer.Write($"{line.UriPath} \" |");
                    writer.Write($"{line.TimeTake} ");
                }
            }
            return true;
        }
    }
}
