#if TOOLS
using Godot;
using System;

namespace ScaffoldUI.Plugin;

[Tool]
public partial class ScaffoldUIPlugin : EditorPlugin
{
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
	}
}
#endif
