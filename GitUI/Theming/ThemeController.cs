using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GitCommands;
using GitExtUtils.GitUI.Theming;

namespace GitUI.Theming
{
    public class ThemeController : Theme
    {
        private readonly Theme _defaultTheme;
        private readonly FieldInfo _colorTableField;
        private readonly PropertyInfo _threadDataProperty;
        private readonly object _systemBrushesKey;
        private readonly object _systemPensKey;
        private readonly Dictionary<KnownColor, Color> _sysColors;

        public ThemeController(Theme defaultTheme)
        {
            _defaultTheme = defaultTheme;
            _sysColors = new Dictionary<KnownColor, Color>();

            var systemDrawingAssembly = typeof(Color).Assembly;

            _colorTableField = systemDrawingAssembly.GetType("System.Drawing.KnownColorTable")
                .GetField("colorTable", BindingFlags.Static | BindingFlags.NonPublic);

            _threadDataProperty = systemDrawingAssembly.GetType("System.Drawing.SafeNativeMethods")
                .GetNestedType("Gdip", BindingFlags.NonPublic)
                .GetProperty("ThreadData", BindingFlags.Static | BindingFlags.NonPublic);

            _systemBrushesKey = typeof(SystemBrushes)
                .GetField("SystemBrushesKey", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(null);

            _systemPensKey = typeof(SystemPens)
                .GetField("SystemPensKey", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(null);
        }

        public event Action Changed;

        private IDictionary ThreadData =>
            (IDictionary)_threadDataProperty.GetValue(null, null);

        public void Load(
            IReadOnlyDictionary<AppColor, Color> appColors,
            IReadOnlyDictionary<KnownColor, Color> systemColors)
        {
            _sysColors.Clear();
            foreach (var (name, value) in systemColors)
            {
                _sysColors.Add(name, value);
            }

            foreach (var (name, value) in appColors)
            {
                AppSettings.SetColor(name, value);
            }

            ResetGdiCaches();
            Changed?.Invoke();
        }

        public void Save(
            out IReadOnlyDictionary<AppColor, Color> appColors,
            out IReadOnlyDictionary<KnownColor, Color> sysColors)
        {
            appColors = AppColors.ToDictionary(c => c, GetColor);
            sysColors = SysColors.ToDictionary(c => c, GetSysColor);
        }

        public void ResetAllColors()
        {
            SysColors.ForEach(Reset);
            AppColors.ForEach(ResetInternal);

            ResetGdiCaches();
            Changed?.Invoke();
        }

        public void Reset(KnownColor name)
        {
            _sysColors.Remove(name);

            ResetGdiCaches();
            Changed?.Invoke();
        }

        public void Reset(AppColor name)
        {
            ResetInternal(name);
            Changed?.Invoke();
        }

        public override Color GetColor(AppColor name) =>
            AppSettings.GetColor(name);

        public void SetColor(AppColor name, Color color)
        {
            AppSettings.SetColor(name, color);
            Changed?.Invoke();
        }

        public void SetColor(KnownColor name, Color value)
        {
            if (!IsSystemColor(name))
            {
                throw new ArgumentException($"{name} is not system color");
            }

            _sysColors[name] = value;
            ResetGdiCaches();
            Changed?.Invoke();
        }

        protected override Color GetSysColor(KnownColor name) =>
            _sysColors.TryGetValue(name, out var result)
                ? result
                : _defaultTheme.GetColor(name);

        private void ResetInternal(AppColor name) =>
            AppSettings.SetColor(name, _defaultTheme.GetColor(name));

        private void ResetGdiCaches()
        {
            _colorTableField.SetValue(null, null);
            ThreadData[_systemBrushesKey] = null;
            ThreadData[_systemPensKey] = null;

            foreach (Form form in Application.OpenForms)
            {
                NativeMethods.SendMessageInt(form.Handle, NativeConstants.WM_SYSCOLORCHANGE,
                    IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
