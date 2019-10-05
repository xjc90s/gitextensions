using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitCommands;
using GitExtUtils.GitUI.Theming;

namespace GitUI.Theming
{
    public class ThemeEditor
    {
        private const string Subdirectory = "Themes";
        public const string Extension = ".colors";
        private const string CurrentThemeName = "current";
        private const string SaveDialogTitle = "Save theme";
        private const string LoadDialogTitle = "Load theme";
        private static readonly string Filter = $"GitExtensions theme (*{Extension})|*{Extension}";

        private readonly string _currentThemeFile;
        private readonly string _invariantThemeFile;
        private readonly ThemeController _controller;
        private readonly ThemePersistence _persistence;

        public ThemeEditor(ThemeController controller, ThemePersistence persistence)
        {
            _controller = controller;
            _persistence = persistence;
            UserDirectory = Path.Combine(AppSettings.ApplicationDataPath.Value, Subdirectory);
            AppDirectory = Path.Combine(AppSettings.GetGitExtensionsDirectory(), Subdirectory);
            _currentThemeFile = Path.Combine(UserDirectory, CurrentThemeName + Extension);
            _invariantThemeFile = Path.Combine(AppDirectory, "win10default" + Extension);
        }

        public event ThemeChangedHandler ThemeChanged;

        public string UserDirectory { get; }
        public string AppDirectory { get; }

        public StaticTheme LoadInvariantTheme()
        {
            _persistence.TryLoadFile(_invariantThemeFile, out var appColors, out var sysColors, quiet: true);
            return new StaticTheme(appColors, sysColors);
        }

        public bool IsCurrentThemeFile(string filename) =>
            string.Equals(Path.GetFileNameWithoutExtension(filename), CurrentThemeName,
                StringComparison.InvariantCultureIgnoreCase);

        public IEnumerable<string> GetSavedThemeNames() =>
            Directory
                .GetFiles(UserDirectory, "*" + Extension, SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileNameWithoutExtension)
                .Where(n => !string.Equals(n, CurrentThemeName,
                    StringComparison.InvariantCultureIgnoreCase))
                .OrderByDescending(n =>
                    string.Equals(n, "system default",
                        StringComparison.InvariantCultureIgnoreCase));

        public bool LoadSavedTheme(string name, bool quiet = true)
        {
            string fileName = Path.Combine(UserDirectory, name + Extension);
            if (LoadFromFile(fileName, quiet))
            {
                OnThemeChanged(true, fileName);
                return true;
            }

            return false;
        }

        public void LoadCurrentTheme(string name)
        {
            if (string.IsNullOrEmpty(name) || !LoadSavedTheme(name, quiet: true))
            {
                LoadFromFile(_currentThemeFile, quiet: true);
            }
        }

        public void SaveCurrentTheme()
        {
            string file = _currentThemeFile;
            _controller.Save(out var appColors, out var sysColors);
            _persistence.SaveToFile(appColors, sysColors, file, quiet: true);
        }

        public void SetColor(AppColor name, Color value)
        {
            _controller.SetColor(name, value);
            OnThemeChanged(true, null);
        }

        public void SetColor(KnownColor name, Color value)
        {
            _controller.SetColor(name, value);
            OnThemeChanged(true, null);
        }

        public void Reset(AppColor name)
        {
            _controller.Reset(name);
            OnThemeChanged(true, null);
        }

        public void Reset(KnownColor name)
        {
            _controller.Reset(name);
            OnThemeChanged(true, null);
        }

        public void ResetAllColors()
        {
            _controller.ResetAllColors();
            OnThemeChanged(true, null);
        }

        public void SaveToFileDialog()
        {
            if (!SaveDialog(out var selectedFile))
            {
                return;
            }

            _controller.Save(out var appColors, out var sysColors);
            if (!_persistence.SaveToFile(appColors, sysColors, selectedFile, quiet: false))
            {
                return;
            }

            OnThemeChanged(false, selectedFile);

            bool SaveDialog(out string filename)
            {
                var dlg = new SaveFileDialog
                {
                    DefaultExt = Extension,
                    InitialDirectory = UserDirectory,
                    AddExtension = true,
                    Filter = Filter,
                    Title = SaveDialogTitle,
                    CheckPathExists = true
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    return true;
                }

                filename = null;
                return false;
            }
        }

        public void LoadFromFileDialog()
        {
            if (TrySelectFile(out string selectedFile) && LoadFromFile(selectedFile))
            {
                OnThemeChanged(true, selectedFile);
            }

            bool TrySelectFile(out string result)
            {
                result = null;

                var dlg = new OpenFileDialog
                {
                    DefaultExt = Extension,
                    InitialDirectory = UserDirectory,
                    AddExtension = true,
                    Filter = Filter,
                    Title = LoadDialogTitle,
                    CheckFileExists = true
                };

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                result = dlg.FileName;
                return true;
            }
        }

        private bool LoadFromFile(string file, bool quiet = false)
        {
            if (_persistence.TryLoadFile(file, out var appColors, out var sysColors, quiet))
            {
                _controller.Load(appColors, sysColors);
                return true;
            }

            return false;
        }

        private void OnThemeChanged(bool colorsChanged, string file)
        {
            string schemaName = string.IsNullOrEmpty(file) || file.Equals(_currentThemeFile, StringComparison.OrdinalIgnoreCase)
                ? string.Empty
                : Path.GetFileNameWithoutExtension(file);
            ThemeChanged?.Invoke(colorsChanged, schemaName);
        }
    }
}
