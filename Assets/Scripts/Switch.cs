using Shecodes.Frame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shecodes.Elements
{
    public class Switch : MonoBehaviour
    {
        [SerializeField] Sprite On;
        [SerializeField] Sprite Off;
        [SerializeField] bool IsOn = false;
        [SerializeField] Gate LinkedGate = null;
        SpriteRenderer spriteRenderer = null;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GetSprite();
        }

        private void GetSprite()
        {
            spriteRenderer.sprite = IsOn ? On : Off;
            LinkedGate.ToggleGate(IsOn);
        }

        internal void ToggleSwitch(bool ison)
        {
            IsOn = ison;
            GetSprite();
        }
    }
}