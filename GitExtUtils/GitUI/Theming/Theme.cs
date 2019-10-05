using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GitExtUtils.GitUI.Theming
{
    public abstract class Theme
    {
        private static readonly IReadOnlyDictionary<KnownColor, KnownColor> Duplicates =
                    new Dictionary<KnownColor, KnownColor>
                    {
                        [KnownColor.ButtonFace] = KnownColor.Control,
                        [KnownColor.ButtonShadow] = KnownColor.ControlDark,
                        [KnownColor.ButtonHighlight] = KnownColor.ControlLight
                    };

        public static HashSet<AppColor> AppColors { get; } =
            new HashSet<AppColor>(Enum.GetValues(typeof(AppColor)).Cast<AppColor>());

        public static HashSet<KnownColor> SysColors { get; } =
            new HashSet<KnownColor>(
                Enum.GetValues(typeof(KnownColor))
                    .Cast<KnownColor>()
                    .Where(c => IsSystemColor(c) && !Duplicates.ContainsKey(c)));

        public abstract Color GetColor(AppColor name);

        public Color GetColor(KnownColor name)
        {
            if (!IsSystemColor(name))
            {
                return Color.FromKnownColor(name);
            }

            var actualName = Duplicates.TryGetValue(name, out var duplicate)
                ? duplicate
                : name;

            return GetSysColor(actualName);
        }

        public static bool IsSystemColor(Color c) =>
            c.IsKnownColor && IsSystemColor(c.ToKnownColor());

        protected abstract Color GetSysColor(KnownColor name);

        protected static Color GetFixedColor(KnownColor systemColor) =>
            Color.FromArgb(Color.FromKnownColor(systemColor).ToArgb());

        protected static bool IsSystemColor(KnownColor name) =>
            name < KnownColor.Transparent || name > KnownColor.YellowGreen;
    }
}
