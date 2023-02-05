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

        public int runes;
        RuneType[] runeTypes;

        public Rigidbody2D body;

        public Vector2 playerInput;
        public Vector2 velocity;
        public float deceleration = 100;

        private bool interactKey = false;

        // Start is called before the first frame update
        void Start()
        {
            if (_event == null)
                _event = new UnityEvent();

            if(!body)
            {
                body = GetComponent<Rigidbody2D>();
            }

            runes = 0;
        }

        // Fixed Update
        private void FixedUpdate()
        {
            handleMovement();

            // Check for interaction key
            if (interactKey) BroadcastMessage("interactWithObject");
        }

        #region Movement

        private void handleMovement()
        {
            // X
            // Player input is in the opposite direction of current velocity
            if (playerInput.x != 0 && velocity.x != 0 && Mathf.Sign(playerInput.x) != Mathf.Sign(velocity.x))
            {
                // Instantly reset velocity
                velocity.x = 0;
            }
            // Deceleration
            else if (playerInput.x == 0)
            {
                // Decelerate towards 0
                velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            // Regular Horizontal Movement
            else
            {
                // Accelerate towards max speed
                velocity.x = Mathf.MoveTowards(velocity.x, playerInput.x * maxSpeed, accel * Time.fixedDeltaTime);
            }

            // Y
            // Player input is in the opposite direction of current velocity
            if (playerInput.y != 0 && velocity.y != 0 && Mathf.Sign(playerInput.y) != Mathf.Sign(velocity.y))
            {
                // Instantly reset velocity
                velocity.y = 0;
            }
            // Deceleration
            else if (playerInput.y == 0)
            {
                // Decelerate towards 0
                velocity.y = Mathf.MoveTowards(velocity.y, 0, deceleration * Time.fixedDeltaTime);
            }
            // Regular Horizontal Movement
            else
            {
                // Accelerate towards max speed
                velocity.y = Mathf.MoveTowards(velocity.y, playerInput.y * maxSpeed, accel * Time.fixedDeltaTime);
            }

            body.velocity = velocity;
        }

        #endregion

        // Update is called once per frame
        void Update()
        {
            handleInput();

            /*
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
            */
        }

        private void handleInput()
        {
            // Reset input
            playerInput = Vector2.zero;
            interactKey = false;

            if (InputManager.getKey("LEFt")) playerInput.x -= 1;
            if (InputManager.getKey("RIGHT")) playerInput.x += 1;
            if (InputManager.getKey("UP")) playerInput.y += 1;
            if (InputManager.getKey("DOWN")) playerInput.y -= 1;

            if (InputManager.getKeyDown("Interact")) interactKey = true;
        }

        void collectRune(RuneType collectedType)
        {
            Debug.Log("Collected rune! Now have " + ++runes + " runes.");
            if (collectedType == RuneType.AIR)
                Debug.Log("Collected air rune!");
        }
        
    }
}