using System.Collections;
using System.Collections.Generic;
using FeedingFrenzy.Interfaces;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace FeedingFrenzy
{
    public enum eAnimalType
    {
        Arctic,
        Desert,
        Farm,
        Forest,
        Island,
        Jungle,
        Pets,
        River,
        Safari,
        Sea
    }
    public abstract class Animal : MonoBehaviour, IController
    {
        [SerializeField] protected eAnimalType animalType;
        [SerializeField] protected string animalName;
        [SerializeField] protected GameObject targetModel;
        [Range(0f, 15f), SerializeField] protected float movementSpeed;
        [Range(0f, 15f), SerializeField] protected float rotateSpeed;
        [SerializeField] protected bool useCustomCamera;
        [SerializeField] protected float zDepth = 0;
        [ShowIf("useCustomCamera"), SerializeField] protected Camera customCamera;
        [ReadOnly, SerializeField] protected Vector3 mousePositionWorldSpace;
        [ReadOnly, SerializeField] protected Vector3 mousePositionScreenSpace;
        public Camera MainCamera
        {
            get
            {
                if (this.mainCamera != null)
                    return this.mainCamera;
                this.mainCamera = this.useCustomCamera ? this.customCamera : Camera.main;
                return this.mainCamera;
            }
        } private Camera mainCamera = null;
        public eAnimalType AnimalType => this.animalType;
        public string AnimalName => this.animalName;
        public float MovementSpeed => this.movementSpeed;
        public float RotateSpeed => this.rotateSpeed;
        public Vector3 MovementMouseDirection => this.mousePositionWorldSpace;
        public float ZDepth => this.zDepth;
        public GameObject TargetModel => this.targetModel;
        public abstract Vector3 GatherMouseInput();
        public abstract Vector3 GatherMouseScreenInput();

        protected virtual void Start()
        {
            Cursor.visible = false;
        }
        protected virtual void Update()
        {
            this.mousePositionWorldSpace = GatherMouseInput();
            this.mousePositionScreenSpace = GatherMouseScreenInput();
        }
        protected virtual void FixedUpdate()
        {
            
        }
    }
}
