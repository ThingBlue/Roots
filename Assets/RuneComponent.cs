using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roots 
{
    public enum RuneType
    {
        FIRE,
        EARTH,
        WATER,
        AIR
    }

    public class RuneComponent : MonoBehaviour
    {

        public RuneType type;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) // Check if collider is player
            {
                collision.BroadcastMessage("collectRune", type);
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}