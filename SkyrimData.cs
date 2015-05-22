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
    }
}
