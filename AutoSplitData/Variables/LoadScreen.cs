using LiveSplit.AutoSplitting;
using LiveSplit.AutoSplitting.Variables;
using LiveSplit.Skyrim.AutoSplitData.Editors;
using LiveSplit.Skyrim.AutoSplitData.Variables;
using System.Linq;
using System.Xml;

[assembly: VariableRegister(typeof(LoadScreen), typeof(LoadScreenEditor))]

namespace LiveSplit.Skyrim.AutoSplitData.Variables
{
	[VariableDescriptor(AllowMultiple = false, Description = "Triggers on a loadscreen distinguished by its start and end location.")]
	public sealed class LoadScreen : Variable
	{
		public Location[] StartLocation { get; set; }

		public Location[] EndLocation { get; set; }

		public bool EqualsStartLocation { get; set; }

		public bool EqualsEndLocation { get; set; }

		public override GameEvent[] RestrictedEvents => GetRestrictedEvents();

		public LoadScreen(Location[] startLocation, bool equalsStartLocation, Location[] endLocation, bool equalsEndLocation)
		{
			StartLocation = startLocation;
			EndLocation = endLocation;
			EqualsStartLocation = equalsStartLocation;
			EqualsEndLocation = equalsEndLocation;
		}

		public LoadScreen(Location[] startLocation, Location[] endLocation) : this(startLocation, true, endLocation, true) { }

		public LoadScreen(Location startLocation, Location endLocation) : this((Location[])startLocation, (Location[])endLocation) { }

		public LoadScreen(Location startLocation, bool equalsStartLocation, Location endLocation, bool equalsEndLocation)
			: this((Location[])startLocation, equalsStartLocation, (Location[])endLocation, equalsEndLocation)
		{ }

		public LoadScreen(XmlElement elem)
		{
			var startElem = elem["StartLocation"];
			var endElem = elem["EndLocation"];

			StartLocation = Location.ArrayFromXml(startElem);
			EqualsStartLocation = bool.Parse(startElem.GetAttribute("equals"));

			EndLocation = Location.ArrayFromXml(endElem);
			EqualsEndLocation = bool.Parse(endElem.GetAttribute("equals"));
		}

		public override bool Verify(GameData gData)
		{
			var data = (SkyrimData)gData;
			return (StartLocation == null || data.LoadScreenStartLocation == StartLocation == EqualsStartLocation)
				&& (EndLocation == null || data.Location.Current == EndLocation == EqualsEndLocation);
		}

		GameEvent[] GetRestrictedEvents()
		{
			if (!Location.IsNullOrAnywhere(EndLocation) && EndLocation.Any(end => end.ID != null))
				return new GameEvent[] { SkyrimEvent.LoadScreenEnd };
			else if (!Location.IsNullOrAnywhere(EndLocation))
				return new GameEvent[] { SkyrimEvent.LoadScreenLoadEnd, SkyrimEvent.LoadScreenEnd };
			else
				return new GameEvent[] { SkyrimEvent.LoadScreenStart, SkyrimEvent.LoadScreenLoadEnd,
					SkyrimEvent.LoadScreenEnd };
		}

		public override XmlElement ToXml(XmlDocument doc)
		{
			var root = base.ToXml(doc);

			var startLocation = StartLocation.ToXml(doc, "StartLocation");
			startLocation.SetAttribute("equals", EqualsStartLocation.ToString());
			root.AppendChild(startLocation);

			var endLocation = EndLocation.ToXml(doc, "EndLocation");
			endLocation.SetAttribute("equals", EqualsEndLocation.ToString());
			root.AppendChild(endLocation);

			return root;
		}
	}
}
