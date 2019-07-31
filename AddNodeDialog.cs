#if TOOLS

using Godot;
using Wayfarer.Core.Utils.Debug;

namespace Wayfarer.Pebbles
{
    [Tool]
    public class AddNodeDialog : WindowDialog
    {
        [Export()] private string _test;
        
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