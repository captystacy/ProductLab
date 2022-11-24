namespace CardPullouter.Core.Tests.Helpers
{
    public static class DirectoryHelper
    {
        public const string SourceFolderName = "Source";

        public static string GetCurrentDirectory()
        {
            var binDirectory = Directory.GetCurrentDirectory();
            return Directory.GetParent(binDirectory)!.Parent!.Parent!.FullName;
        }

        public static string GetPathToFileFromSourceFolder(string fileName)
        {
            var currentDirectory = GetCurrentDirectory();
            return Path.Combine(currentDirectory, SourceFolderName, fileName);
        }
    }
}
