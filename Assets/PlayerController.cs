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
        public float maxSpeed = 3;
        public float accel = 32;
        public float dampingFactor = 4;
        public float minDamping = 4;

        public GameObject rootSpawnPrefab;
        List<GameObject> rootList;

        public int runes;
        RuneType[] runeTypes;

        public Rigidbody2D body;

        // Start is called before the first frame update
        void Start()
        {
            if (_event == null)
                _event = new UnityEvent();

            if(!body)
            {
                body = GetComponent<Rigidbody2D>();
            }

            rootList = new List<GameObject>();

            runes = 0;
        }

        // Fixed Update
        private void FixedUpdate()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            #region Movement

            if (InputManager.getKey("UP"))
            {
                //transform.Translate(Vector2.up * speed);
                if (body.velocity.y < maxSpeed)
                {
                    body.AddForce(Vector2.up * accel);
                    //body.velocity = new Vector2(body.velocity.x, maxSpeed);
                }
            }
            if (InputManager.getKey("LEFt"))
            {
                //transform.Translate(Vector2.left * accel);
                body.AddForce(Vector2.left * accel);
                if (body.velocity.x < -maxSpeed)
                {
                    body.velocity = new Vector2(-maxSpeed, body.velocity.y);
                }
            }
            if (InputManager.getKey("DOWN"))
            {
                //transform.Translate(Vector2.down * accel);
                body.AddForce(Vector2.down * accel);
                if (body.velocity.y < -maxSpeed)
                {
                    body.velocity = new Vector2(body.velocity.x, -maxSpeed);
                }
            }
            if (InputManager.getKey("RIGHT"))
            {
                //transform.Translate(Vector2.right * accel);
                body.AddForce(Vector2.right * accel);
                if (body.velocity.x > maxSpeed)
                {
                    body.velocity = new Vector2(maxSpeed, body.velocity.y);
                }
            }

            if (!(InputManager.getKey("UP") || InputManager.getKey("DOWN")))
            {
                Vector2 damping = Vector2.zero;
                damping.y = dampingFactor * -body.velocity.y;
                body.AddForce(damping);

                if (Mathf.Abs(body.velocity.y) <= minDamping)
                {
                    body.velocity = new Vector2(body.velocity.x, 0);
                }
            }

            if (!(InputManager.getKey("LEFt") || InputManager.getKey("RIGHT")))
            {
                Vector2 damping = Vector2.zero;
                damping.x = dampingFactor * -body.velocity.x;
                body.AddForce(damping);

                if (Mathf.Abs(body.velocity.x) <= minDamping)
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                }
            }

            #endregion

            if (InputManager.getKeyDown("Interact"))
            {
                BroadcastMessage("interactWithObject");
            }
            if (InputManager.getKeyDown("SpawnRoot"))
            {
                rootList.Add(Instantiate(rootSpawnPrefab, body.transform.position, Quaternion.identity));
            }
        }

        void collectRune(RuneType collectedType)
        {
            Debug.Log("Collected rune! Now have " + ++runes + " runes.");
            if (collectedType == RuneType.AIR)
                Debug.Log("Collected air rune!");
        }
        
    }
}