using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GitCommands.Settings;
using GitExtUtils.GitUI.Theming;
using Color = System.Drawing.Color;

namespace GitUI.Theming
{
    public class DefaultTheme : Theme
    {
        private readonly Dictionary<KnownColor, Color> _sysColors;

        public DefaultTheme() =>
            _sysColors = ReadDefaultColors();

        public override Color GetColor(AppColor name) =>
            AppColorDefaults.GetBy(name);

        private Dictionary<KnownColor, Color> ReadDefaultColors() =>
            SysColors.ToDictionary(name => name, GetFixedColor);

        protected override Color GetSysColor(KnownColor name) =>
            _sysColors.TryGetValue(name, out var result)
                ? result
                : Color.Empty;
    }
}
