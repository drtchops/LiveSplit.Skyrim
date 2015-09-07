using LiveSplit.ComponentUtil;
using System;
using System.Reflection;

namespace LiveSplit.Skyrim
{
    public class SkyrimData : MemoryWatcherList
    {
        // Loads
        public MemoryWatcher<bool> IsQuickLoading { get; } = new MemoryWatcher<bool>(new DeepPointer(0x17337CC)); // == 1 if a load is happening (any except loading screens in Helgen for some reason)
        public MemoryWatcher<bool> IsLoadingScreen { get; } = new MemoryWatcher<bool>(new DeepPointer(0xEE3561)); // == 1 if in a loading screen
        public MemoryWatcher<bool> IsInFadeOut { get; } = new MemoryWatcher<bool>(new DeepPointer(0x172EE2E)); // == 1 when in a fadeout, it goes back to 0 once control is gained
        public FakeMemoryWatcher<bool> IsLoading => new FakeMemoryWatcher<bool>(
            IsQuickLoading.Old || IsLoadingScreen.Old,
            IsQuickLoading.Current || IsLoadingScreen.Current);

        // Position
        public MemoryWatcher<int> LocationID { get; } = new MemoryWatcher<int>(new DeepPointer(0x01738308, 0x4, 0x78, 0x670, 0xEC)); // ID of the current location (see http://steamcommunity.com/sharedfiles/filedetails/?id=148834641 or http://www.skyrimsearch.com/cells.php)
        public MemoryWatcher<int> WorldX { get; } = new MemoryWatcher<int>(new DeepPointer(0x0172E864, 0x64)); // X world position (cell)
        public MemoryWatcher<int> WorldY { get; } = new MemoryWatcher<int>(new DeepPointer(0x0172E864, 0x68)); // Y world position (cell)
        public FakeMemoryWatcher<Location> Location => new FakeMemoryWatcher<Location>(
            new Location(LocationID.Old, WorldX.Old, WorldY.Old),
            new Location(LocationID.Current, WorldX.Current, WorldY.Current));

        // Game state
        public MemoryWatcher<bool> IsAlduin2Defeated { get; } = new MemoryWatcher<bool>(new DeepPointer(0x1711608)); // == 1 when last blow is struck on alduin
        public MemoryWatcher<int> QuestlinesCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3F0)); // number of questlines completed (from ingame stats)
        public MemoryWatcher<int> CollegeOfWinterholdQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x38c)); // number of college of winterhold quests completed (from ingame stats)
        public MemoryWatcher<int> CompanionsQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x378)); // number of companions quests completed (from ingame stats)
        public MemoryWatcher<int> DarkBrotherhoodQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3b4)); // number of dark brotherhood quests completed (from ingame stats)
        public MemoryWatcher<int> ThievesGuildQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3a0)); // number of thieves guild quests completed (from ingame stats)
        public MemoryWatcher<bool> IsInEscapeMenu { get; } = new MemoryWatcher<bool>(new DeepPointer(0x172E85E)); // == 1 when in the pause menu or level up menu
        public MemoryWatcher<int> MainQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x350)); // number of main quests completed (from ingame stats)
        public MemoryWatcher<int> WordsOfPowerLearned { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x558)); // "Words Of Power Learned" from ingame stats
        public MemoryWatcher<float> Alduin1Health { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F41764, 0x74, 0x30, 0x30, 0x1c)); // Alduin 1's health (if it's at 0 it's 99% of the time because it can't be found
        public MemoryWatcher<int> LocationsDiscovered { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x170)); // number of locations discovered (from ingame stats)
        public MemoryWatcher<bool> ArePlayerControlsDisabled { get; } = new MemoryWatcher<bool>(new DeepPointer(0x172EF30, 0xf)); // == 1 when player controls have been disabled (not necessarily all controls)
        public MemoryWatcher<float> BearCartHealth { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F354DC, 0x74, 0x30, 0x30, 0x1C));

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

        public SkyrimData()
        {
            //add all memory watchers to the list
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(MemoryWatcher<>))
                    this.Add(p.GetValue(this, null) as MemoryWatcher);
            }
        }
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

    public class FakeMemoryWatcher<T>
    {
        public T Current { get; }
        public T Old { get; }
        public bool Changed { get; }

        public FakeMemoryWatcher(T old, T current)
        {
            this.Old = old;
            this.Current = current;
            this.Changed = !current.Equals(old);
        }
    }
}
