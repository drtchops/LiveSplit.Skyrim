using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;

namespace LiveSplit.Skyrim.AutoSplitData
{
	public struct Location
	{
		public readonly int? ID;
		public readonly int? worldX;
		public readonly int? worldY;

		public static readonly Location Anywhere = new Location(null, null, null);
		public static readonly Location[] ThroatOfTheWorld = new Location[]
		{
			new Location((int)Worlds.Tamriel, 14, -12),
			new Location((int)Worlds.Tamriel, 14, -13),
			new Location((int)Worlds.Tamriel, 13, -12),
			new Location((int)Worlds.Tamriel, 13, -13)
		};

		public static IReadOnlyDictionary<string, Location[]> StaticLocations { get; } = GetStaticLocations();

		public Location(int? locationID = null, int? cellX = null, int? cellY = null)
		{
			ID = locationID;
			worldX = cellX;
			worldY = cellY;
		}

		public static bool operator ==(Location x, Location y)
		{
			/*
             * Null members are considered as "any".
             * For exemple a location with only null members is "anywhere" and will thus be equal to everything.
            */
			if (x.ID != null && y.ID != null)
			{
				if (x.ID != y.ID)
					return false;
			}

			if (x.worldX != null && y.worldX != null)
			{
				if (x.worldX != y.worldX)
					return false;
			}

			if (x.worldY != null && y.worldY != null)
			{
				if (x.worldY != y.worldY)
					return false;
			}

			return true;
		}

		public static bool operator ==(Location x, Location[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == x)
					return true;
			}
			return false;
		}

		public bool StrictlyEquals(Location[] location)
		{
			return location != null && location.Length == 1 &&
			  location[0].ID == ID && location[0].worldX == worldX && location[0].worldY == worldY;
		}

		public XmlElement ToXml(XmlDocument doc)
		{
			var root = doc.CreateElement("Location");
			root.SetAttribute("cellX", worldX.ToString());
			root.SetAttribute("cellY", worldY.ToString());
			root.SetAttribute("id", ID != null ? ID.Value.ToString("X") : ID.ToString());
			return root;
		}

		public static Location FromXml(XmlElement elem)
		{
			var id = StrToNullableInt(elem.GetAttribute("id"), true);
			var x = StrToNullableInt(elem.GetAttribute("cellX"));
			var y = StrToNullableInt(elem.GetAttribute("cellY"));
			return new Location(id, x, y);
		}

		public static Location[] ArrayFromXml(XmlElement elem)
		{
			if (!elem.HasAttribute("id") || !elem.HasAttribute("cellX") || !elem.HasAttribute("cellY"))
			{
				var list = new List<Location>();
				foreach (XmlElement child in elem.ChildNodes)
					list.Add(FromXml(child));

				return list.ToArray();
			}

			return new Location[] { FromXml(elem) };
		}

		static int? StrToNullableInt(string str, bool hexadecimal = false)
		{
			var style = hexadecimal
				? System.Globalization.NumberStyles.HexNumber
				: System.Globalization.NumberStyles.Number;

			int i;
			if (int.TryParse(str, style, System.Globalization.CultureInfo.InvariantCulture, out i))
				return i;
			return null;
		}

		static IReadOnlyDictionary<string, Location[]> GetStaticLocations()
		{
			var dictionary = new SortedDictionary<string, Location[]>();
			foreach (var f in typeof(Location).GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				if (f.IsInitOnly && f.Name != nameof(Anywhere))
				{
					if (f.FieldType == typeof(Location[]))
						dictionary.Add(f.Name, (Location[])f.GetValue(null));
					else if (f.FieldType == typeof(Location))
						dictionary.Add(f.Name, (Location[])(Location)f.GetValue(null));
				}
			}
			return new ReadOnlyDictionary<string, Location[]>(dictionary);
		}

		public static bool IsNullOrAnywhere(Location location) => Anywhere.StrictlyEquals((Location[])location);

		public static bool IsNullOrAnywhere(Location[] location) => location == null || Anywhere.StrictlyEquals(location);

		public static bool operator ==(Location[] array, Location x) => x == array;

		public override bool Equals(object obj) => obj is Location && this == (Location)obj;

		public static bool operator !=(Location x, Location y) => !(x == y);

		public static bool operator !=(Location x, Location[] array) => !(x == array);

		public static bool operator !=(Location[] array, Location x) => !(x == array);

		public static explicit operator Location[] (Location location) => new Location[] { location };

		public static explicit operator Location(Worlds location) => new Location((int)location);

		public override int GetHashCode() => ID.GetHashCode() ^ worldX.GetHashCode() ^ worldY.GetHashCode();
	}

	static class LocationExtensions
	{
		public static XmlElement ToXml(this IEnumerable<Location> array, XmlDocument doc, string name = "Location")
		{
			var root = doc.CreateElement(name);
			foreach (var loc in array)
				root.AppendChild(loc.ToXml(doc));
			return root;
		}
	}
}
