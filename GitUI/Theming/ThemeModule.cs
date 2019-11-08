using System;
using System.Diagnostics;
using System.Windows.Forms;
using GitCommands;
using GitExtUtils.GitUI.Theming;
using Control = System.Windows.Forms.Control;

namespace GitUI.Theming
{
    public static class ThemeModule
    {
        private static readonly Lazy<ThemeDeployment> DeploymentLazy =
            new Lazy<ThemeDeployment>(() => new ThemeDeployment(Editor));

        private static readonly Lazy<ThemeEditor> EditorLazy =
            new Lazy<ThemeEditor>(CreateEditor);

        private static readonly Lazy<FormThemeEditor> FormEditorLazy =
            new Lazy<FormThemeEditor>(() => new FormThemeEditor(Controller, DefaultTheme, Editor));

        private static ThemePersistence Persistence { get; } = new ThemePersistence();

        private static readonly Lazy<Theme> DefaultThemeLazy =
            new Lazy<Theme>(() => new DefaultTheme());

        private static readonly Lazy<ThemeController> ControllerLazy =
            new Lazy<ThemeController>(() => new ThemeController(DefaultTheme));

        private static ThemeDeployment Deployment =>
            DeploymentLazy.Value;

        public static ThemeEditor Editor =>
            EditorLazy.Value;

        private static FormThemeEditor FormEditor =>
            FormEditorLazy.Value;

        private static Theme DefaultTheme =>
            DefaultThemeLazy.Value;

        private static ThemeController Controller =>
            ControllerLazy.Value;

        public static void Load()
        {
            ApplyCurrentTheme();
            PreventMessageBoxTheming();
            ColorHelper.SetUITheme(Controller, Editor.LoadInvariantTheme());
        }

        public static void Unload()
        {
            Win32ThemingHooks.Uninstall();
            Win32ThemingHooks.WindowCreated -= Handle_WindowCreated;
        }

        public static void ShowEditor()
        {
            FormEditor.Show();
            FormEditor.BringToFront();
        }

        private static ThemeEditor CreateEditor()
        {
            var editor = new ThemeEditor(Controller, Persistence);
            AppSettings.Saved += editor.SaveCurrentTheme;
            return editor;
        }

        private static void ApplyCurrentTheme()
        {
            try
            {
                Deployment.DeployThemesToUserDirectory();
            }
            catch (Exception ex)
            {
                // non mission-critical, do not crash
                Trace.WriteLine($"Failed to deploy schemes to user directory: {ex}");
            }

            try
            {
                Win32ThemingHooks.InstallColorHooks(Controller);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Failed to install color hooks: {ex}");
                return;
            }

            try
            {
                Win32ThemingHooks.InstallCreateWindowHook();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Failed to install create window hook: {ex}");
                return;
            }

            Win32ThemingHooks.WindowCreated += Handle_WindowCreated;
            Editor.LoadCurrentTheme(AppSettings.UITheme);
        }

        private static void PreventMessageBoxTheming()
        {
            try
            {
                Win32ThemingHooks.InstallMessageBoxHooks();
            }
            catch (Exception ex)
            {
                // non mission-critical, do not crash
                Trace.WriteLine($"Failed to install MessageBox hooks: {ex}");
            }
        }

        private static void Handle_WindowCreated(IntPtr hwnd)
        {
            switch (Control.FromHandle(hwnd))
            {
                case Form form:
                    form.Load += (s, e) =>
                    {
                        if (!AppSettings.UseSystemVisualStyle)
                        {
                            ((Form)s).FixVisualStyle();
                        }
                    };
                    break;
            }
        }
    }
}
