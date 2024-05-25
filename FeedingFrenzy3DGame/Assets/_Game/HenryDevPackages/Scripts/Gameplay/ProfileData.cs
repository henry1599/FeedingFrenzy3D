using System.Collections.Generic;

namespace HenryDev.Gameplay
{
    [System.Serializable]
    public class ProfileData
    {
        public List<SaveData> UnlockedLevels;
        public ProfileData()
        {
            UnlockedLevels = new List<SaveData>()
            {
                new SaveData(1, 1)
            };
        }
        public SaveData GetLevelSaveData(int chapter, int level)
        {
            foreach (var unlockedLevel in UnlockedLevels)
            {
                if (unlockedLevel.ChapterID == chapter && unlockedLevel.LevelID == level)
                    return unlockedLevel;
            }
            return null;
        }
    }
    [System.Serializable]
    public class SaveData
    {
        public int ChapterID;
        public int LevelID;
        public int CollectedObjectsRequirement;
        public int CollectedObjectsCount;
        public SaveData() {}
        public SaveData(int chapter, int level)
        {
            ChapterID = chapter;
            LevelID = level;
        }
        public override bool Equals(object obj)
        {
            return ChapterID == (obj as SaveData).ChapterID && LevelID == (obj as SaveData).LevelID;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
