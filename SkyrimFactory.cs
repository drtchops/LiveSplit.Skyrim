using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

namespace LiveSplit.Skyrim
{
	public class SkyrimFactory : IComponentFactory
    {
        public string ComponentName => "Skyrim";

        public string Description => "Automates splitting and load removal for Skyrim.";

        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state) => new SkyrimComponent(state);

        public string UpdateName => ComponentName;

        public string UpdateURL => "https://raw.githubusercontent.com/drtchops/LiveSplit.Skyrim/master/";

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public string XMLURL => UpdateURL + "Components/update.LiveSplit.Skyrim.xml";
    }
}
