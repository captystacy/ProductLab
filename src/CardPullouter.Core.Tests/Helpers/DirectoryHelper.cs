namespace CardPullouter.Core.Tests.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetCurrentDirectory()
        {
            var binDirectory = Directory.GetCurrentDirectory();
            return Directory.GetParent(binDirectory)!.Parent!.Parent!.FullName;
        }
    }
}
