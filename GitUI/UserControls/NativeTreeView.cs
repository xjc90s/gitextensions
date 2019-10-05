using GitCommands;

namespace GitUI.UserControls
{
    public class NativeTreeView : System.Windows.Forms.TreeView
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            if (AppSettings.UseSystemVisualStyle)
            {
                NativeMethods.SetWindowTheme(Handle, "explorer", null);
            }
        }
    }
}
