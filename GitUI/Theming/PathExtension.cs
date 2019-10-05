using System.IO;

namespace GitUI.Theming
{
    internal static class PathExtension
    {
        public static string InDirectory(this string file, string targetDirectory) =>
            Path.Combine(targetDirectory, Path.GetFileName(file));
    }
}
