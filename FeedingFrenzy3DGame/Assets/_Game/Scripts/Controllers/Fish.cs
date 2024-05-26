using System;
using System.Collections;
using System.Collections.Generic;
using HenryDev;
using UnityEngine;

namespace FeedingFrenzy
{
    public class Fish : Animal
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] int maxSize;
        Vector3 currentLookDirection = Vector3.zero;
        Vector3 lastMouseInputPosition;
        protected int size = 0;
        public int Size => this.size;
        public static event Action<int> OnSizeUpdated;
        protected override void Start()
        {
            base.Start();
            UpdateSize();
        }
        public override Vector3 GatherMouseInput()
        {
            var pos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(pos.x, pos.y, this.zDepth);
        }
        protected override void Update()
        {
            base.Update();
            Move();
            FlipRotate();
            this.currentLookDirection = this.lastMouseInputPosition.x > this.mousePositionScreenSpace.x ? Vector3.left : this.lastMouseInputPosition.x < mousePositionScreenSpace.x ? Vector3.right : this.currentLookDirection;
            this.lastMouseInputPosition = this.mousePositionScreenSpace;
        }
        protected void Move()
        {
            transform.Follow(this.mousePositionWorldSpace, this.movementSpeed);
        }
        protected void FlipRotate()
        {
            this.targetModel.transform.RotateToward(this.currentLookDirection, this.rotateSpeed, upAxis: Vector3.up, eAxis.X | eAxis.Z);
        }
        public override Vector3 GatherMouseScreenInput()
        {
            var pos = Input.mousePosition;
            return new Vector3(pos.x, pos.y, this.zDepth);
        }
        public virtual void UpdateSize()
        {
            this.size++;
            this.size %= this.maxSize;
            OnSizeUpdated?.Invoke(this.size);
        }
    }
}
