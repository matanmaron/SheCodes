using Shecodes.Frame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shecodes.Elements
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] Sprite Open;
        [SerializeField] Sprite Close;
        [SerializeField] bool IsOpened = false;

        SpriteRenderer spriteRenderer = null;
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GetSprite();
        }

        private void GetSprite()
        {
            spriteRenderer.sprite = IsOpened ? Open : Close;
            gameObject.tag = IsOpened ? Consts.FREESPOT : Consts.BLOCK;
        }

        internal void ToggleGate(bool isopen)
        {
            string status = isopen ? "Open" : "Closed";
            Debug.Log($"Gate is now {status}");
            IsOpened = isopen;
            GetSprite();
        }
    }
}