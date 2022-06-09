using CandidateTesting.KailonAlexandre.DTO;
using CandidateTesting.KailonAlexandre.Services;
using CandidateTesting.KailonAlexandre.Services.Interface;
using CandidateTesting.KailonAlexandre.Test.Utils;

namespace CandidateTesting.KailonAlexandre.Test
{
    public class CDNLogTest
    {
        private ILogBuilderService _logBuilderService;

        public CDNLogTest()
        {
            _logBuilderService = new CDNLogBuilderService();
        }

        [Fact]
        public void BuildModel_Should_Return_Ok()
        {
            var defaultModels = BuildDefault();
            var models = _logBuilderService.BuildModel(GetLog());
            Xunit.Assert.NotNull(models);
            foreach (var lists in defaultModels.Zip(models, (d, m) => new { DefaultModel = d, Model = m }))
            {
                Assert.Equal(lists.DefaultModel.Provider, lists.Model.Provider);
                Assert.Equal(lists.DefaultModel.MethodHttp, lists.Model.MethodHttp);
                Assert.Equal(lists.DefaultModel.StatusCode, lists.Model.StatusCode);
                Assert.Equal(lists.DefaultModel.UriPath, lists.Model.UriPath);
                Assert.Equal(lists.DefaultModel.TimeTake, lists.Model.TimeTake);
                Assert.Equal(lists.DefaultModel.ResponseSize, lists.Model.ResponseSize);
                Assert.Equal(lists.DefaultModel.CacheStatus, lists.Model.CacheStatus);
            }

        }
        [Fact]
        public void BuildModel_Should_Not_Return_Ok()
        {
            var models = _logBuilderService.BuildModel(null);
            Assert.Null(models);
        }

        [Fact]
        public void WriteLog_Should_Create_File()
        {
            var result = _logBuilderService.WriteLog(TestConstants._logName, BuildDefault());
            Assert.True(result);
            var fileExist = File.Exists(TestConstants._logName);
            Assert.True(fileExist);
            File.Delete(TestConstants._logName);
        }
        [Fact]
        public void WriteLog_Should_Not_Create_File()
        {
            var result = _logBuilderService.WriteLog(TestConstants._logName, null);
            Assert.False(result);
        }

        private string GetLog()
        {
            var logPath = Path.Combine(TestConstants._jsonPath, "CDNLog.txt");
            return File.ReadAllText(logPath);
        }
        private List<LogModel> BuildDefault()
        {
            List<LogModel> models = new List<LogModel>();
            models.Add(new LogModel()
            {
                Provider = "MINHA CDN",
                MethodHttp = "GET",
                StatusCode = "200",
                UriPath = "/robots.txt",
                TimeTake = 100,
                ResponseSize = "312",
                CacheStatus = "HIT",
            });
            models.Add(new LogModel()
            {
                Provider = "MINHA CDN",
                MethodHttp = "POST",
                StatusCode = "200",
                UriPath = "/myImages",
                TimeTake = 319,
                ResponseSize = "101",
                CacheStatus = "MISS",
            });
            models.Add(new LogModel()
            {
                Provider = "MINHA CDN",
                MethodHttp = "GET",
                StatusCode = "404",
                UriPath = "/not-found",
                TimeTake = 142,
                ResponseSize = "199",
                CacheStatus = "MISS",
            });
            models.Add(new LogModel()
            {
                Provider = "MINHA CDN",
                MethodHttp = "GET",
                StatusCode = "200",
                UriPath = "/robots.txt",
                TimeTake = 245,
                ResponseSize = "312",
                CacheStatus = "REFRESH_HIT",
            });
            return models;
        }
    }
}