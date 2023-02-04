using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Roots;

namespace Roots
{
    public class PlayerController : MonoBehaviour
    {
        UnityEvent _event;
        public float speed=1;

        // Start is called before the first frame update
        void Start()
        {
            if (_event == null)
                _event = new UnityEvent();

            InputManager.addKeyToMap("UP", KeyCode.W);
            InputManager.addKeyToMap("LEFt", KeyCode.A);
            InputManager.addKeyToMap("RIGHT", KeyCode.D);
            InputManager.addKeyToMap("DOWN", KeyCode.S);
        }

        // Fixed Update
        private void FixedUpdate()
        {
            if(InputManager.getKey("UP"))
            {
                transform.Translate(Vector2.up * speed);
            }
            if (InputManager.getKey("LEFt"))
            {
                transform.Translate(Vector2.left * speed);
            }
            if (InputManager.getKey("DOWN"))
            {
                transform.Translate(Vector2.down * speed);
            }
            if (InputManager.getKey("RIGHT"))
            {
                transform.Translate(Vector2.right * speed);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}