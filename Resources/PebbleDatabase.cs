using Godot;
using Godot.Collections;
using Wayfarer.Core.Plugin.Resources;

namespace Wayfarer.Core.Plugin.Resources
{
    public class PebbleDatabase : Resource
    {
        [Export()] private Array<PebbleMetaData> _all;

        public Array<PebbleMetaData> All => _all;
    }
}