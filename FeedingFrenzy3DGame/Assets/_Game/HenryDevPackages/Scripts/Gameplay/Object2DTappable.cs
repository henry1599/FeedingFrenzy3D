using System;
using System.Collections;
using System.Collections.Generic;
using HenryDev.Intefaces;
using HenryDev.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HenryDev.Gameplay
{
    public abstract class Object2DTappable : MonoBehaviour, ITappable<Object2DTappable>
    {
        public Collider2D Col2D 
        {
            get 
            {
                if (this.col2D == null)
                    this.col2D = GetComponent<Collider2D>();
                return this.col2D;
            }
        } protected Collider2D col2D;
        
        protected bool wasTapped = false;

        public event Action<Object2DTappable> OnTapped;
        public event Action<Object2DTappable> OnReleased;
        public UnityEvent OnTapped_UnityEvent;
        public UnityEvent OnReleased_UnityEvent;

        void OnMouseDown()
        {
            if (RaycastManager.Instance?.IsPointerOverUIElement() ?? false)
                return;
            if (this.wasTapped)
                return;
            this.wasTapped = true;
            OnTap();
        }
        void OnMouseUp()
        {
            if (RaycastManager.Instance?.IsPointerOverUIElement() ?? false)
                return;
            if (!this.wasTapped)    
                return;
            this.wasTapped = false;
            OnRelease();
        }
        public virtual void OnTap()
        {
            OnTapped?.Invoke(this);
            OnTapped_UnityEvent?.Invoke();
        }
        public virtual void OnRelease()
        {
            OnReleased?.Invoke(this);
            OnReleased_UnityEvent?.Invoke();
        }
        public virtual void SetBlockTap(bool isBlock)
        {
            if (this.col2D == null)
                return;
            this.col2D.enabled = !isBlock;
        }
    }
}
