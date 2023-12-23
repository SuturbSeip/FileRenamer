namespace FileRenamer
{
    public class ExtensionFilters
    {
        public string ExtensionOfFile { get; set; }
        public bool IsActive { get; set; }

        public ExtensionFilters(string extensionOfFile, bool isAvite=true)
        {
            ExtensionOfFile = extensionOfFile;
            IsActive = isAvite;
        }
    }
}
