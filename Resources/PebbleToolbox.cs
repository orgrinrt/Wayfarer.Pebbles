using System;
using Godot;
using Wayfarer.Core.Utils.Debug;
using Array = Godot.Collections.Array;

namespace Wayfarer.Core.Plugin.Resources
{
    public class PebbleToolbox : Resource
    {
        [Export()] private string _description;
        [Export()] private string _version;
        [Export()] private DateTime _created;
        [Export()] private DateTime _lastModified;
        [Export()] private Array _pebbles;

        public string Name => GetName();
        public string Description => _description;
        public string Version => _version;
        public DateTime Created => _created;
        public DateTime LastModified => _lastModified;
        public Array Pebbles => _pebbles;

        public void AddPebble(PebbleMetaData pebbleData)
        {
            _pebbles.Add(pebbleData);
        }
        
        public void AddPebble(PebbleMetaData pebbleData, int index)
        {
            _pebbles.Insert(index, pebbleData);
        }

        public void Remove(string name)
        {
            foreach (object item in _pebbles)
            {
                if (item is PebbleMetaData pebble)
                {
                    if (pebble.Name == name)
                    {
                        _pebbles.Remove(pebble);
                        return;
                    }
                }
            }
            
        }

        public void SetDescription(string desc)
        {
            _description = desc;
        }

        public void SetVersion(string ver)
        {
            _version = ver;
        }

        public void SetCreatedTime(DateTime created)
        {
            _created = created;
        }

        public void SetLastModifiedTime(DateTime modified)
        {
            _lastModified = modified;
        }
    }
}