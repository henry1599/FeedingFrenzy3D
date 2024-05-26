using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using NaughtyAttributes;
using HenryDev;
using Pooling;
using Unity.VisualScripting;
using HenryDev.Utilities;

namespace FeedingFrenzy
{
    public class FishSpawner : MonoBehaviour
    {
        public FishSpawnInfoDict fishSpawnInfoDict;
        protected FishSpawnInfo currentInfo;
        protected float timeBtwSpawn = 0;
        protected bool isInitialized;
        protected Level level;
        void Awake()
        {
            this.isInitialized = false;
            Fish.OnSizeUpdated += HandleFishUpdateSize;
        }
        void OnDestroy()
        {
            Fish.OnSizeUpdated -= HandleFishUpdateSize;
        }
        void Update()
        {
            if (!this.isInitialized)
                return;
            Spawn();
        }
        public void Setup(Level level)
        {
            this.level = level;
            this.isInitialized = true;
        }
        private void HandleFishUpdateSize(int size)
        {
            if (!this.fishSpawnInfoDict.ContainsKey(size))   
                return;
            this.currentInfo = this.fishSpawnInfoDict[size];
            ResetSpawnTimer();
        }
        protected void Spawn()
        {
            if (this.timeBtwSpawn > 0)
                this.timeBtwSpawn -= Time.deltaTime;
            else
            {
                int burstAmount = this.currentInfo.BurstAmount;
                while (burstAmount > 0)
                {
                    Vector2 pos = GetRandomSpawnPosition();
                    Quaternion rotation = GetRotationFromSpawnPosition(pos);
                    GameObject pref = this.currentInfo.FishPrefab;

                    pref.Spawn(pos, rotation);
                    burstAmount--;
                }
                ResetSpawnTimer();
            }
        }
        protected void ResetSpawnTimer()
        {
            this.timeBtwSpawn = 1f /  this.currentInfo.SpawnRate;
        }
        protected Vector3 GetRandomSpawnPosition()
        {
            Vector2 levelPos = this.level.transform.RandomPositionOutsideRectangle(this.level.Bounds.x, this.level.Bounds.y, 2);
            return levelPos;
        }
        protected Quaternion GetRotationFromSpawnPosition(Vector2 spawnPosition)
        {
            float levelPosX = this.level.transform.position.x;
            return Quaternion.LookRotation(spawnPosition.x < levelPosX ? Vector3.right : Vector3.left, Vector3.up);
        }
#if UNITY_EDITOR
        [ContextMenu("SetupPool")]
        public void SetupPool()
        {
            var levelObject = FindObjectOfType<Level>();
            if (levelObject == null)
                return;
            var poolManagerTransform = levelObject.PoolManagerTransform;
            poolManagerTransform.DeleteChildrenEditor();
            var objectPoolInstance = new GameObject("ObjectPool");
            objectPoolInstance.transform.SetParent(poolManagerTransform);
            var objectPool = objectPoolInstance.AddComponent<ObjectPool>();
            objectPool.PoolList = new();
            foreach (var (_, info) in this.fishSpawnInfoDict)
            {
                objectPool.PoolList.Add(
                    new Pool(info.FishPrefab, info.MaxAmount, transform) { Name = info.FishPrefab.name, HasMaxSize = true, MaxSize = info.MaxAmount }
                );
            }
        }
#endif
    }
    [System.Serializable]
    public class FishSpawnInfoDict : SerializableDictionaryBase<int, FishSpawnInfo> {}
    [System.Serializable]
    public class FishSpawnInfo
    {
        public eAnimalType FishType;
        public GameObject FishPrefab;
        [AllowNesting, Tooltip("How many fishes spawned for each round")]
        public int BurstAmount;
        [AllowNesting, Tooltip("How many fishes spawned per second")]
        public float SpawnRate;
        public int MaxAmount;
    }
}
