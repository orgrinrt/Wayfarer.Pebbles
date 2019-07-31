using Godot;
using Godot.Collections;
using Wayfarer.Core.Utils.Debug;

namespace Wayfarer.Core.Plugin.Resources
{
    public class PebbleMetaData : Resource
    {
        [Export()] private string _name;
        [Export()] private string _parentName;

        [Export()] private bool _isScene;
        [Export()] private Script _script;
        [Export()] private PackedScene _scene;
        
        [Export()] private string _description;
        [Export()] private Texture _icon;

        public string Name => _name;
        public string ParentName => _parentName;
        public PackedScene Scene => _scene;
        public Texture Icon => _icon;

        public override void _Init()
        {
            base._Init();
            
            Log.Wayfarer.Print("NodeData " + _name + " was initialized", true);
        }

        public override Array _GetPropertyList()
        {
            Array source = base._GetPropertyList();

            foreach (Dictionary property in source)
            {
                if (property.TryGetValue("name", out object value))
                {
                    if (value is string s)
                    {
                        if (s == "_script")
                        {
                            if (_isScene)
                            {
                                Log.Wayfarer.Print("Removed property " + s, true);
                                source.Remove(property);
                            } 
                        }
                        else if (s == "_scene")
                        {
                            if (!_isScene)
                            {
                                Log.Wayfarer.Print("Removed property " + s, true);
                                source.Remove(property);
                            }
                        }
                        else
                        {
                            Log.Wayfarer.Error("There was no property named _script or _scene (current: " + s, true);
                        }
                    }
                }
                else
                {
                    Log.Wayfarer.Error("A property didn't have a name", true);
                }
            }

            foreach (Dictionary property in source)
            {
                if (property.TryGetValue("name", out object value))
                {
                    Log.Wayfarer.Error("Property: " + value, true);
                }
            }
            return source;
        }
    }
}