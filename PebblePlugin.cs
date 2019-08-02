#if TOOLS

using System;
using Godot;
using Wayfarer.Core.Systems.Managers;
using Wayfarer.Core.Utils.Attributes;
using Wayfarer.Core.Utils.Debug;
using Wayfarer.Core.Utils.Helpers;

namespace Wayfarer.Pebbles
{
    [Tool]
    public class PebblePlugin : EditorPlugin
    {
        private PebbleManager _manager;
        private EditorMenuBar _editorMenuBar;

        public PebbleManager Manager => _manager;
        public EditorInterface EditorInterface => GetEditorInterface();
        
        public override void _EnterTree()
        {
            _manager = new PebbleManager { Name = "Manager" };

            AddChild(_manager);
            try
            {
                AddCustomControlsToEditor();
            }
            catch (Exception e)
            {
                Log.Wf.EditorError("Couldn't add custom controls in Pebble, resetting may work", e, true);
            }
            
            AddCustomResources();
        }

        public override void _ExitTree()
        {
            RemoveCustomControlsFromEditor();
            RemoveCustomResources();
            try
            {
                RemoveOldEditorMenubar();
            }
            catch (Exception e)
            {
                Log.Wf.EditorError("Couldn't remove the OLD EditorMenuBar", e, true);
            }
        }
        
        public override void DisablePlugin()
        {
            base.DisablePlugin();

            
        }
        
        private void AddCustomControlsToEditor()
        {
            PackedScene toolbarScene = GD.Load<PackedScene>("res://Addons/Wayfarer.Pebbles/Assets/Scenes/EditorMenuBar.tscn");
            _editorMenuBar = (EditorMenuBar)toolbarScene.Instance();
            _editorMenuBar.SetEditorInterface(GetEditorInterface());
            AddControlToContainer(CustomControlContainer.CanvasEditorMenu, _editorMenuBar);
        }
        
        private void AddCustomResources()
        {
            Script nodeScript = GD.Load<Script>("res://Addons/Wayfarer.Pebbles/Resources/PebbleMetaData.cs");
            Texture nodeIcon = GD.Load<Texture>("res://Addons/Wayfarer.Core/Assets/Icons/resource.png");
            AddCustomType("PebbleMetaData", "Resource", nodeScript, nodeIcon);

            Script nodeListScript = GD.Load<Script>("res://Addons/Wayfarer.Pebbles/Resources/PebbleDatabase.cs");
            Texture nodeListIcon = GD.Load<Texture>("res://Addons/Wayfarer.Core/Assets/Icons/wayfarerNodes.png");
            AddCustomType("PebbleDatabase", "Resource", nodeListScript, nodeListIcon);
        }
        
        private void RemoveCustomControlsFromEditor()
        {
            try
            {
                RemoveControlFromContainer(CustomControlContainer.CanvasEditorMenu, _editorMenuBar);

                _editorMenuBar.QueueFree();
            }
            catch (Exception e)
            {
                Log.Wf.EditorError("Couldn't remove the EditorMenuBar", e, true);
                
                if (e.Message.StartsWith("Cannot access"))
                {
                    Log.Wf.Simple("This is because it is already removed - no action needed, not a bug!", true);
                }
            }
        }
        
        private void RemoveCustomResources()
        {
            RemoveCustomType("WayfarerNodeData");
            RemoveCustomType("WayfarerNodeDatabase");
        }
        
        private void RemoveOldEditorMenubar()
        {
            Node[] editorNodes = EditorInterface.GetBaseControl().GetChildrenRecursive();

            foreach (Node node in editorNodes)
            {
                if (node is EditorMenuBar)
                {
                    try
                    {
                        RemoveControlFromContainer(CustomControlContainer.CanvasEditorMenu, node as Control);
                    }
                    catch (Exception e)
                    {
                        Log.Wf.EditorError("Tried to remove EditorMenuBar from Toolbar, but couldn't", e, true);
                    }
                    try
                    {
                        node.QueueFree();
                        Log.Wf.Editor("Removed old EditorMenuBar (QueueFree)", true);
                    }
                    catch (Exception e)
                    {
                        Log.Wf.EditorError("Tried to QueueFree() EditorMenuBar from Toolbar, but couldn't", e, true);
                    }
                    return;
                }
            }
        }
    }
}

#endif