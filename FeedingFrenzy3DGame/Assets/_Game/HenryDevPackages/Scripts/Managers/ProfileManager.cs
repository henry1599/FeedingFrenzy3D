using System.Collections;
using System.Collections.Generic;
using HenryDev.Gameplay;
using HenryDev.Utilities;
using UnityEngine;

namespace HenryDev.Managers
{
    public class ProfileManager : MonoBehaviour
    {
        public static readonly string GLOBAL_SAVE_KEY = "g10b4l_s4v3";
        public static ProfileManager Instance {get; private set;}
        private ProfileData data;
        public ProfileData Data => this.data;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            Load();
        }
        void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
        public void Load()
        {
            string json = PlayerPrefs.GetString(GLOBAL_SAVE_KEY, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                this.data = new ProfileData();
                Debug.Log("Empty data, init new one");
            }
            else
                this.data = json.FromJson<ProfileData>();
        }
        public void Save()
        {
            string json = this.data.ToJson();
            Debug.Log(string.Format("Save string: {0}", json));
            PlayerPrefs.SetString(GLOBAL_SAVE_KEY, json);
        }
        public void UpdateObjectsCountRequirement(int chapter, int level, int objectCountRequirement)
        {
            if (this.data == null)
                Load();
            if (this.data == null)
                return;
            var levelData = this.data.GetLevelSaveData(chapter, level);
            if (levelData == null)
                return;
            levelData.CollectedObjectsRequirement = objectCountRequirement;
            Save();
        }
        public void UpdateObjectsCount(int chapter, int level, int objectCounts)
        {
            if (this.data == null)
                Load();
            if (this.data == null)
                return;
            var levelData = this.data.GetLevelSaveData(chapter, level);
            levelData.CollectedObjectsCount = objectCounts;
            Save();
        }
    }
}
