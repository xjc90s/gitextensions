﻿using System.Collections.Generic;
using System.Drawing;

namespace GitUIPluginInterfaces
{
    public interface IGitPlugin
    {
        string? Name { get; }

        string? Description { get; }

        Image? Icon { get; }

        IGitPluginSettingsContainer? SettingsContainer { get; set; }

        bool HasSettings { get; }

        IEnumerable<ISetting> GetSettings();

        void Register(IGitUICommands gitUiCommands);

        void Unregister(IGitUICommands gitUiCommands);

        /// <summary>
        /// Runs the plugin and returns whether the RevisionGrid should be refreshed.
        /// </summary>
        bool Execute(GitUIEventArgs args);
    }
}
