using CandidateTesting.KailonAlexandre.DTO;

namespace CandidateTesting.KailonAlexandre.Services.Interface
{
    public interface ILogBuilderService
    {
        public List<LogModel> BuildModel(string response);
        public bool WriteLog(string path, List<LogModel> Logs);
    }
}
