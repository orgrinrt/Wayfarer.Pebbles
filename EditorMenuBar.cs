#if TOOLS

using Godot;
using Wayfarer;
using Wayfarer.Core.Utils.Attributes;
using Wayfarer.Core.Utils.Debug;
using Wayfarer.Core.Utils.Helpers;
using Array = Godot.Collections.Array;

namespace Wayfarer.Pebbles
{
    [Tool]
    public class EditorMenuBar : Control
    {
        [Get("HBox/AddNodeMenu/Button")] private Button _addNodeButton;
    
        private AddNodeDialog _addNodeDialog;
        private EditorInterface _editorInterface;
        private PebblePlugin _plugin;

        public AddNodeDialog AddNodeDialog => _addNodeDialog;
    
        public override void _Ready()
        {
            this.SetupWayfarer();

            _addNodeButton.Connect("button_up", this, nameof(OnButtonPressed));

            PackedScene addNodeDialogScene = GD.Load<PackedScene>("res://Addons/Wayfarer.Pebbles/Assets/Scenes/AddNodeDialog.tscn");
            _addNodeDialog = (AddNodeDialog)addNodeDialogScene.Instance();
            AddChild(_addNodeDialog);
            
        }
        

        public override void _ExitTree()
        {
            Log.Wf.Editor("Going to QueueFree() the AddNodeDialog", true);
            AddNodeDialog dialog = this.GetChildOfType<AddNodeDialog>();
            dialog.QueueFree();
            Log.Crash("Test", true);
        }
        
        private void OnButtonPressed()
        {
            EditorSelection selection = _editorInterface.GetSelection();
            Array selectedNodes = selection.GetSelectedNodes();

            _addNodeDialog.SetSelectedNodes(selectedNodes);
            _addNodeDialog.Popup_();
        
            foreach (Node node in selectedNodes)
            {
                // pass in the selectedNodes to the popup
            }
            // open pop-up to select node to create
        }
    
        public void SetEditorInterface(EditorInterface editor)
        {
            _editorInterface = editor;
        }
        
        public void SetPlugin(PebblePlugin plugin)
        {
            _plugin = plugin;
        }
    }
}

#endif