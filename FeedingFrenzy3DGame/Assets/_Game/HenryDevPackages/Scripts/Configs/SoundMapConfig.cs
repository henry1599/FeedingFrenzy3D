using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;
using HenryDev;

namespace AudioPlayer
{
    [CreateAssetMenu(menuName = "Sound Configs/Sound Map Config")]
    public class SoundMapConfig : ScriptableObject
    {
        public string SoundMapsPath;
        [ReorderableList]
        public List<SoundMapPath> MapPath;

        [System.Serializable]
        public class SoundMapPath
        {
            public SoundType Type;
            public string Path;
        }
        [Button]
        public void LoadSoundMaps()
        {
            MapPath = new List<SoundMapPath>();
            var files = Directory.GetFiles(SoundMapsPath);
            foreach (var file in files)
            {
                if (file.EndsWith(".meta"))
                    continue;
                var nonResourcesPath = file.Split("Resources/")[^1];
                var finalPath = nonResourcesPath.Split(".asset")[0];
                var soundMap = Resources.Load<SoundMap>(finalPath);
                if (soundMap == null)
                    continue;
                var type = soundMap.soundType;
                MapPath.Add(new SoundMapPath() { Type = type, Path = finalPath });
            }
        }
    }
}
