namespace ScaffoldUI.Plugin
{

    /// <summary>
    /// A lightweight lifecycle contract for plugin editor-only features (like setting menus, toolbars etc.)
    /// used in the main <see cref="ScaffoldUIPlugin"/>. It nicely seperates different code parts of the plugin.
    /// This ensures that added functionality is properly lifecycle-managed to prevent memory leaks or bugs.
    /// </summary>
    public interface IScaffoldEditorFeature
    {
        /// <summary>
        /// Called when the plugin is enabled
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called when the plugin is disabled (Cleanup)
        /// </summary>
        void Deinitialize();
    }
}
