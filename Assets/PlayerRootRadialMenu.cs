using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ?? : 2021-04-26 PM 3:12:50
// ??? : Rito

namespace Roots
{
    public class PlayerRootRadialMenu : MonoBehaviour
    {
        public RadialMenu radialMenu;
        public PlayerController player;

        [Space]
        public Sprite[] sprites;

        private void Start()
        {
            radialMenu.SetPieceImageSprites(sprites);
        }

        private void Update()
        {
            if (InputManager.getKeyDown("RadialRoot"))
            {
                radialMenu.Show();
            }
            else if (InputManager.getKeyUp("RadialRoot"))
            {
                int selected = radialMenu.Hide();
                // Debug.Log($"selected: {selected}");
                if (selected <= player.rootList.Count - 1)
                {
                    player.body.transform.position = player.rootList[selected].transform.position;
                }
            }
        }
    }
}