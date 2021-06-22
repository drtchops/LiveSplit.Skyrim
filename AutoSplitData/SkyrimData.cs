using LiveSplit.AutoSplitting;
using LiveSplit.ComponentUtil;
using LiveSplit.Skyrim.AutoSplitData;

[assembly: GameEventRegister(typeof(SkyrimEvent))]

namespace LiveSplit.Skyrim.AutoSplitData
{
	public sealed class SkyrimData : GameData
	{
		// Loads
		public MemoryWatcher<bool> IsQuickSaving { get; } = new MemoryWatcher<bool>(new DeepPointer(0x17337CC)); // == 1 if a load is happening (any except loading screens in Helgen for some reason)
		public MemoryWatcher<bool> IsLoadingScreen { get; } = new MemoryWatcher<bool>(new DeepPointer(0xEE3561)); // == 1 if in a loading screen
		public MemoryWatcher<bool> IsInFadeOut { get; } = new MemoryWatcher<bool>(new DeepPointer(0x172EE2E)); // == 1 when in a fadeout, it goes back to 0 once control is gained
		public FakeMemoryWatcher<bool> IsLoading { get; } = new FakeMemoryWatcher<bool>();

		// Position
		public MemoryWatcher<int> WorldID { get; } = new MemoryWatcher<int>(new DeepPointer(0x01738308, 0x4, 0x78, 0x670, 0xEC)); // ID of the current world (or cell for interiors) (see http://steamcommunity.com/sharedfiles/filedetails/?id=148834641 or http://www.skyrimsearch.com/cells.php)
		public MemoryWatcher<int> CellX { get; } = new MemoryWatcher<int>(new DeepPointer(0x0172E864, 0x64)); // X cell coord, often unreliable in interiors
		public MemoryWatcher<int> CellY { get; } = new MemoryWatcher<int>(new DeepPointer(0x0172E864, 0x68)); // Y cell coord), often unreliable in interiors
		public MemoryWatcher<Vector3f> Position { get; } = new MemoryWatcher<Vector3f>(new DeepPointer(0xF1063C, 0x20));

		// Game state
		public MemoryWatcher<bool> IsAlduin2Defeated { get; } = new MemoryWatcher<bool>(new DeepPointer(0x1711608)); // == 1 when last blow is struck on alduin
		public MemoryWatcher<bool> IsInEscapeMenu { get; } = new MemoryWatcher<bool>(new DeepPointer(0x172E85E)); // == 1 when in the pause menu or level up menu
		public MemoryWatcher<bool> ArePlayerControlsDisabled { get; } = new MemoryWatcher<bool>(new DeepPointer(0x172EF30, 0xf)); // == 1 when player controls have been disabled (not necessarily all controls)

		//NPC
		public MemoryWatcher<float> Alduin1Health { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F41764, 0x74, 0x30, 0x30, 0x1c)); // Alduin 1's health (if it's at 0 it's 99% of the time because it can't be found
		public MemoryWatcher<float> BearCartHealth { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F354DC, 0x74, 0x30, 0x30, 0x1C));
		public MemoryWatcher<float> LordHarkonHealth { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F64184, 0x74, 0x30, 0x30, 0x1C));
		public MemoryWatcher<float> AncanoHealth { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F2DC4C, 0x74, 0x30, 0x30, 0x1C));
		public MemoryWatcher<float> MiraakHealth { get; } = new MemoryWatcher<float>(new DeepPointer(0x00F814FC, 0x74, 0x30, 0x30, 0x1C));

		//Stats
		public MemoryWatcher<int> LocationsDiscovered { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x170)); // number of locations discovered (from ingame stats)
		public MemoryWatcher<int> WordsOfPowerLearned { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x558)); // "Words Of Power Learned" from ingame stats
		public MemoryWatcher<int> MiscObjectivesCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x33C)); // number of misc objectives completed (from ingame stats)
		public MemoryWatcher<int> MainQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x350)); // number of main quests completed (from ingame stats)
		public MemoryWatcher<int> QuestlinesCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3F0)); // number of questlines completed (from ingame stats)
		public MemoryWatcher<int> CollegeOfWinterholdQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x38c)); // number of college of winterhold quests completed (from ingame stats)
		public MemoryWatcher<int> CompanionsQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x378)); // number of companions quests completed (from ingame stats)
		public MemoryWatcher<int> DarkBrotherhoodQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3b4)); // number of dark brotherhood quests completed (from ingame stats)
		public MemoryWatcher<int> ThievesGuildQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3a0)); // number of thieves guild quests completed (from ingame stats)
		public MemoryWatcher<int> DaedricQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3dc)); // number of daedric quests completed (from ingame stats)
		public MemoryWatcher<int> CivilWarQuestsCompleted { get; } = new MemoryWatcher<int>(new DeepPointer(0x00EE6C34, 0x3c8)); // number of Civil War quests completed (from ingame stats)
		//Fake
		public FakeMemoryWatcher<Location> Location { get; } = new FakeMemoryWatcher<Location>();
		public FakeMemoryWatcher<bool> IsCouncilDone { get; } = new FakeMemoryWatcher<bool>();
		public FakeMemoryWatcher<bool> IsAlduin1Defeated { get; } = new FakeMemoryWatcher<bool>();
		public FakeMemoryWatcher<bool> IsSkyHavenTempleVisited { get; } = new FakeMemoryWatcher<bool>();
		public FakeMemoryWatcher<int> LeaveSleepingGiantInnCounter { get; } = new FakeMemoryWatcher<int>();

		public static readonly Vector3f StartPosition = new Vector3f(15586.7f, -81242f, 8203.3f);

		public SkyrimData()
		{
			IsLoading.OnUpdate = () => IsQuickSaving.Current || IsLoadingScreen.Current;
			Location.OnUpdate = () => new Location(WorldID.Current, CellX.Current, CellY.Current);
		}

		public bool LoadingScreenStarted { get; set; }
		public bool LoadScreenFadeoutStarted { get; set; }
		public bool QuickLoadFadeoutStarted { get; set; }
		public bool IsLoadingSaveFromMenu { get; set; }
		public Location LoadScreenStartLocation { get; set; }
		public bool IsWaitingLocationIDUpdate { get; set; }
		public Questlines LastQuestCompleted { get; set; }
		public uint LastQuestframeCounter { get; set; }
		public uint FrameCounter { get; set; }

		public override void Reset()
		{
			base.Reset();
			IsSkyHavenTempleVisited.Current = false;
			IsAlduin1Defeated.Current = false;
			LeaveSleepingGiantInnCounter.Current = 0;
			IsCouncilDone.Current = false;
		}
	}

	public sealed class SkyrimEvent : GameEvent
	{
		public static readonly SkyrimEvent QuickSave = new SkyrimEvent(0);
		public static readonly SkyrimEvent QuickLoad = new SkyrimEvent(1);
		public static readonly SkyrimEvent LoadScreenStart = new SkyrimEvent(2);
		public static readonly SkyrimEvent LoadScreenLoadEnd = new SkyrimEvent(3, true);
		public static readonly SkyrimEvent LoadScreenEnd = new SkyrimEvent(4);

		SkyrimEvent(int value, bool hidden = false) : base(value, hidden) { }
	}

	/// <summary>
	/// Enumeration of world IDs (cell ID for interiors).
	/// </summary>
	public enum Worlds
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
		WindhelmPalaceOfTheKings = 0x0001677C,
		SkuldafnWorld = 0x000278DD,
		TempleOfMara = 0x00016BD7
	}

	public enum Questlines
	{
		None,
		CollegeOfWinterhold,
		Companions,
		DarkBrotherhood,
		ThievesGuild,
	}
}
