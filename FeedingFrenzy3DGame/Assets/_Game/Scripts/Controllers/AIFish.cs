using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace FeedingFrenzy
{
    public class AIFish : Animal
    {
        [ReadOnly, SerializeField] Vector2 minPivotPosition;
        [ReadOnly, SerializeField] Vector3 maxPivotPosition;
        public void Setup(Transform minPivot, Transform maxPivot)
        {
            this.minPivotPosition = minPivot.position;
            this.maxPivotPosition = maxPivot.position;
        }
        public override Vector3 GatherMouseInput()
        {
            return Vector3.zero;
        }
        protected void Move()
        {
            var pos = transform.position;
            pos = Vector3.Lerp(pos, this.mousePositionWorldSpace, this.movementSpeed * Time.deltaTime);
            transform.position = pos;
        }
        protected void FlipRotate()
        {
            Vector3 lookDirection = this.mousePositionWorldSpace.x < this.transform.position.x ? Vector3.left : Vector3.right;
            Quaternion quaternion = Quaternion.LookRotation(lookDirection, Vector3.up);
            Quaternion currentQuad = this.targetModel.transform.rotation;
            currentQuad = Quaternion.Slerp(currentQuad, quaternion, this.rotateSpeed * Time.deltaTime);
            this.targetModel.transform.rotation = currentQuad;
        }
    }
}
