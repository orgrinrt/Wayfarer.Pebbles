#if TOOLS

using Godot;
using Wayfarer.Utils.Debug;

namespace Wayfarer.Pebbles
{
    [Tool]
    public class AddNodeDialog : WindowDialog
    {
        private Godot.Collections.Array _selectedNodes;
    
        public override void _Ready()
        {
            Log.Wf.Editor("AddNodeDialog Ready");
        }

        public void SetSelectedNodes(Godot.Collections.Array selectedNodes)
        {
            _selectedNodes = selectedNodes;
        }
    }
}

#endif