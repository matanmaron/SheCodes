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
        }

        internal void ToggleSwitch(bool ison)
        {
            string status = ison ? "On" : "Off";
            Debug.Log($"Switch is now {status}");
            IsOn = ison;
            GetSprite();
            LinkedGate.ToggleGate(IsOn);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == Consts.PLAYER)
            {
                ToggleSwitch(!IsOn);
            }
        }
    }
}