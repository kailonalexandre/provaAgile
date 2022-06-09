namespace CandidateTesting.KailonAlexandre.Test.Utils
{
    public static class TestConstants
    {
        public static readonly string _workDir = Environment.CurrentDirectory;
        public static readonly string _projectDir = Directory.GetParent(_workDir).Parent.Parent.FullName;
        public static readonly string _jsonPath = Path.Combine(_projectDir, "Logs");
        public static readonly string _logName = Path.Combine(_projectDir, "logTest.txt");
    }
}
