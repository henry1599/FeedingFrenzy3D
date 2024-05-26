using System.Collections;
using System.Collections.Generic;
using HenryDev;
using UnityEngine;

namespace FeedingFrenzy
{
    public class Fish : Animal
    {
        [SerializeField] Rigidbody rb;
        Vector3 currentLookDirection = Vector3.zero;
        Vector3 lastMouseInputPosition;
        
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
            this.currentLookDirection = this.lastMouseInputPosition.x > this.mousePositionWorldSpace.x ? Vector3.left : this.lastMouseInputPosition.x < mousePositionWorldSpace.x ? Vector3.right : this.currentLookDirection;
            this.lastMouseInputPosition = this.mousePositionWorldSpace;
        }
        protected void Move()
        {
            transform.Follow(this.mousePositionWorldSpace, this.movementSpeed);
        }
        protected void FlipRotate()
        {
            this.targetModel.transform.RotateToward(this.currentLookDirection, this.rotateSpeed, upAxis: Vector3.up, eAxis.X | eAxis.Z);
        }
    }
}
