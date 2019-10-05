using System.IO;
using System.Linq;

namespace GitUI.Theming
{
    internal class ThemeDeployment
    {
        private readonly ThemeEditor _editor;

        public ThemeDeployment(ThemeEditor editor)
        {
            _editor = editor;
        }

        public void DeployThemesToUserDirectory()
        {
            string deployedSaveDirectory = _editor.AppDirectory;
            if (!Directory.Exists(deployedSaveDirectory))
            {
                return;
            }

            string saveDirectory = _editor.UserDirectory;
            Directory.CreateDirectory(saveDirectory);

            var sourceFiles = Directory.EnumerateFiles(deployedSaveDirectory,
                    "*" + ThemeEditor.Extension,
                    SearchOption.TopDirectoryOnly)
                .Where(file => !_editor.IsCurrentThemeFile(file))
                .ToArray();

            // to prevent permanent loss of user customizations
            BackupExistingFiles(sourceFiles, saveDirectory);
            foreach (string file in sourceFiles)
            {
                File.Copy(file, file.InDirectory(saveDirectory), overwrite: true);
            }
        }

        private void BackupExistingFiles(string[] sourceFiles, string saveDirectory)
        {
            var targetFiles = sourceFiles
                .Select(file => file.InDirectory(saveDirectory))
                .ToArray();

            if (!targetFiles.Any(File.Exists))
            {
                return;
            }

            var modified = Enumerable.Range(0, sourceFiles.Length)
                .Where(i =>
                    File.Exists(targetFiles[i]) &&

                    // prevent accidentally reading > 1MB files
                    new FileInfo(targetFiles[i]).Length < (1 << 20) &&
                    File.ReadAllText(targetFiles[i]) != File.ReadAllText(sourceFiles[i]))
                .Select(i => targetFiles[i])
                .ToArray();

            if (modified.Length != 0)
            {
                var backupDirectory = CreateBackupDirectory(saveDirectory);
                foreach (string file in modified)
                {
                    File.Move(file, file.InDirectory(backupDirectory));
                }
            }
        }

        private string CreateBackupDirectory(string saveDirectory)
        {
            var backupDirectory = Path.Combine(saveDirectory, "backup");
            Directory.CreateDirectory(backupDirectory);
            var oldBackups = Directory.GetDirectories(backupDirectory);
            var number = 1 + oldBackups
                .Select(Path.GetFileName)
                .Where(_ => _.All(char.IsDigit))
                .Select(int.Parse)
                .DefaultIfEmpty(0)
                .Max();
            var path = Path.Combine(backupDirectory, number.ToString());
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
