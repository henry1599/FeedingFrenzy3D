using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace FeedingFrenzy
{
    public class Level : MonoBehaviour
    {
        [Range(0f, 20f), SerializeField] float width; 
        [Range(0f, 20f), SerializeField] float height;
        [SerializeField] PolygonCollider2D polygonCollider2D;
        [SerializeField] float zDepth;
        [SerializeField] FishSpawner spawner;
        [SerializeField] Transform poolManagerTransform;
        [ShowNativeProperty] public Vector2 Bounds
        {
            get => new Vector2(this.width, this.height);
        }
        public Transform PoolManagerTransform => this.poolManagerTransform;

        void Start()
        {
            this.spawner?.Setup(this);
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Bounds);
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            if (this.polygonCollider2D == null)
                return;
            this.polygonCollider2D.points = new Vector2[4]
            {
                new Vector2(transform.position.x + this.width / 2f, transform.position.y + this.height / 2f),
                new Vector2(transform.position.x - this.width / 2f, transform.position.y + this.height / 2f),
                new Vector2(transform.position.x - this.width / 2f, transform.position.y - this.height / 2f),
                new Vector2(transform.position.x + this.width / 2f, transform.position.y - this.height / 2f)
            };
        }
#endif
    }
}
