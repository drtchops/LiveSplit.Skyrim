using System;
using System.Diagnostics;
using System.Reflection;

namespace LiveSplit.Skyrim
{
    public class SkyrimData
    {
        public SkyrimData(Process game = null)
        {
            //this avoids the ultimate pain in the ass that is to initialize every data item manually
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(GameDataItem<>))
                    p.SetValue(this, Activator.CreateInstance(p.PropertyType, game), null);
            }
        }

        public GameDataItem<bool> IsLoading { get; private set; }
        public GameDataItem<bool> IsLoadingScreen { get; private set; }
        public GameDataItem<bool> IsInFadeOut { get; private set; }
        public GameDataItem<int> LocationID { get; private set; }
        public GameDataItem<int> WorldX { get; private set; }
        public GameDataItem<int> WorldY { get; private set; }
        public Location Location => new Location(LocationID.Current, WorldX.Current, WorldY.Current);
        public Location PreviousLocation => new Location(LocationID.Previous, WorldX.Previous, WorldY.Previous);
        public GameDataItem<bool> IsAlduin2Defeated { get; private set; }
        public GameDataItem<int> QuestlinesCompleted { get; private set; }
        public GameDataItem<int> CollegeOfWinterholdQuestsCompleted { get; private set; }
        public GameDataItem<int> CompanionsQuestsCompleted { get; private set; }
        public GameDataItem<int> DarkBrotherhoodQuestsCompleted { get; private set; }
        public GameDataItem<int> ThievesGuildQuestsCompleted { get; private set; }
        public GameDataItem<bool> IsInEscapeMenu { get; private set; }
        public GameDataItem<int> MainQuestsCompleted { get; private set; }
        public GameDataItem<int> WordsOfPowerLearned { get; private set; }
        public GameDataItem<float> Alduin1Health { get; private set; }
        public GameDataItem<int> LocationsDiscovered { get; private set; }
        public GameDataItem<bool> ArePlayerControlsDisabled { get; private set; }
        public GameDataItem<float> BearCartHealth { get; private set; }

        public bool loadingScreenStarted = false;
        public bool loadScreenFadeoutStarted = false;
        public bool isLoadingSaveFromMenu = false;
        public Location loadScreenStartLocation;
        public bool isWaitingLocationUpdate = false;
        public bool isWaitingLocationIDUpdate = false;
        public SplitArea lastQuestCompleted = SplitArea.None;
        public uint lastQuestframeCounter = 0;
        public bool isSkyHavenTempleVisited = false;
        public bool isAlduin1Defeated = false;
        public int leaveSleepingGiantInnCounter = 0;
        public bool isCouncilDone = false;
    }

    public struct Location
    {
        public readonly int? ID;
        public readonly int? worldX;
        public readonly int? worldY;

        public static readonly Location[] ThroatOfTheWorld = new Location[]
        {
            new Location(Locations.Tamriel, 14, -12),
            new Location(Locations.Tamriel, 14, -13),
            new Location(Locations.Tamriel, 13, -12),
            new Location(Locations.Tamriel, 13, -13)
        };

        public Location(int? locationID, int? cellX = null, int? cellY = null)
        {
            ID = locationID;
            worldX = cellX;
            worldY = cellY;
        }

        public Location(Locations locationID, int? cellX = null, int? cellY = null)
        {
            ID = (int)locationID;
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
            foreach (var location in array)
            {
                if (location == x)
                    return true;
            }
            return false;
        }

        public static bool  operator ==(Location[] array, Location x)
        {
            return x == array;
        }

        public override bool Equals(Object obj)
        {
            return obj is Location && this == (Location)obj;
        }

        public static bool operator !=(Location x, Location y)
        {
            return !(x == y);
        }
        
        public static bool operator !=(Location x, Location[] array)
        {
            return !(x == array);
        }

        public static bool operator !=(Location[] array, Location x)
        {
            return !(x == array);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ worldX.GetHashCode() ^ worldY.GetHashCode();
        }
    }

    public enum SplitArea : int
    {
        None,
        Helgen,
        Whiterun,
        ThalmorEmbassy,
        Esbern,
        Riverwood,
        Karthspire,
        Septimus,
        TheWall,
        MzarkTower,
        ClearSky,
        HorseClimb,
        CutsceneStart,
        CutsceneEnd,
        Alduin1,
        HighHrothgar,
        Solitude,
        Windhelm,
        Council,
        Odahviing,
        EnterSovngarde,
        CollegeOfWinterholdQuestlineCompleted,
        CompanionsQuestlineCompleted,
        DarkBrotherhoodQuestlineCompleted,
        ThievesGuildQuestlineCompleted,
        AlduinDefeated
    }

    public enum Locations
    {
        Tamriel = 0x0000003C,
        Sovngarde = 0x0002EE41,
        HelgenKeep01 = 0x0005DE24,
        WhiterunWorld = 0x0001A26F,
        ThalmorEmbassy02 = 0x0007DCFC,
        WhiterunDragonsreach = 0x000165A3,
        RiftenWorld = 0x00016BB4,
        RiftenRatway01 = 0x0003B698,
        RiverwoodSleepingGiantInn = 0x000133C6,
        KarthspireRedoubtWorld = 0x00035699,
        SkyHavenTemple = 0x000161EB,
        SeptimusSignusOutpost = 0x0002D4E4,
        TowerOfMzark = 0x0002D4E3,
        HighHrothgar = 0x00087764,
        SolitudeWorld = 0x00037EDF,
        SolitudeCastleDour = 0x000213A0,
        WindhelmWorld = 0x0001691D,
        WindhelmPalaceoftheKings = 0x0001677C,
        SkuldafnWorld = 0x000278DD,
    }
}
