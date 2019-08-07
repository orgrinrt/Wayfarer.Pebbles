using Godot;
using Godot.Collections;

namespace Wayfarer.Pebbles.Resources
{
    public class PebbleDatabase : Resource
    {
        [Export()] private Array<PebbleMetaData> _all;

        public Array<PebbleMetaData> All => _all;
    }
}