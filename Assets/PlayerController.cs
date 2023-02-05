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
        public float maxSpeed = 4;
        public float accel = 100;
        public float decel = 100;

        public GameObject[] rootSpawnPrefabs;
        private int lastRootNum = 0;

        private Animator animator;

        public int maxRoots = 4;
        public List<GameObject> rootList;

        public int runes;
        RuneType[] runeTypes;

        public Rigidbody2D body;

        private Vector2 velocity;

        private Vector2 playerInput;
        private bool interactKeyDown;
        private bool spawnRootKeyDown;
        private bool teleportRootKeyDown;

        private enum AnimationState
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            TP,
            NULL
        }

        private List<AnimationState> animstate;
        private AnimationState lastState = AnimationState.DOWN;
        private AnimationState curState = AnimationState.DOWN;

        private void Start()
        {
            if (_event == null)
                _event = new UnityEvent();

            if(!body)
            {
                body = GetComponent<Rigidbody2D>();
            }

            rootList = new List<GameObject>();

            runes = 0;

            animator = GetComponentInChildren<Animator>();
            animstate = new List<AnimationState>();
        }

        private void Update()
        {
            handleInput();
        }

        private void handleInput()
        {
            // Reset input
            playerInput = Vector2.zero;
            interactKeyDown = false;
            spawnRootKeyDown = false;
            teleportRootKeyDown = false;

            if (InputManager.getKey("LEFt"))
            {
                playerInput.x -= 1;
                curState = AnimationState.LEFT;
            }
            if (InputManager.getKey("RIGHT"))
            {
                playerInput.x += 1;
                curState = AnimationState.RIGHT;
            }
            if (InputManager.getKey("UP"))
            {
                playerInput.y += 1;
                curState = AnimationState.UP;
            }
            if (InputManager.getKey("DOWN"))
            {
                playerInput.y -= 1;
                curState = AnimationState.DOWN;
            }

            if (InputManager.getKeyDown("Interact")) interactKeyDown = true;
            if (InputManager.getKeyDown("SpawnRoot")) spawnRootKeyDown = true;
            if (InputManager.getKeyDown("TeleportRoot"))
            {
                teleportRootKeyDown = true;
                curState = AnimationState.TP;
            }
        }


        // Fixed Update
        private void FixedUpdate()
        {
            handleMovement();
            runAnimation();

            if (interactKeyDown)
            {
                BroadcastMessage("interactWithObject");
            }
            if (spawnRootKeyDown)
            {
                if (rootList.Count >= maxRoots)
                {
                    GameObject rootToDestroy = rootList[lastRootNum];
                    rootList.RemoveAt(lastRootNum);
                    Destroy(rootToDestroy);
                }
                rootList.Insert(lastRootNum, Instantiate(rootSpawnPrefabs[lastRootNum], body.transform.position, Quaternion.identity));
                lastRootNum++;
                lastRootNum %= 4;
            }
            if (teleportRootKeyDown)
            {
                body.transform.position = rootList[rootList.Count - 1].transform.position;
            }
        }

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
                velocity.x = Mathf.MoveTowards(velocity.x, 0, decel * Time.fixedDeltaTime);
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
                velocity.y = Mathf.MoveTowards(velocity.y, 0, decel * Time.fixedDeltaTime);
            }
            // Regular Horizontal Movement
            else
            {
                // Accelerate towards max speed
                velocity.y = Mathf.MoveTowards(velocity.y, playerInput.y * maxSpeed, accel * Time.fixedDeltaTime);
            }

            // Set velocity and move
            body.velocity = velocity;
        }

        private void addAnimationStatus(AnimationState addState)
        {
            animstate.Add(addState);
        }

        private void runAnimation()
        {
            switch (curState)
            {
                case AnimationState.LEFT:
                    animator.Play("PlayerWalkLeft");
                    lastState = AnimationState.LEFT;
                    break;
                case AnimationState.RIGHT:
                    animator.Play("PlayerWalkRight");
                    lastState = AnimationState.RIGHT;
                    break;
                case AnimationState.UP:
                    animator.Play("PlayerWalkUp");
                    lastState = AnimationState.UP;
                    break;
                case AnimationState.DOWN:
                    animator.Play("PlayerWalkDown");
                    lastState = AnimationState.DOWN;
                    break;
                case AnimationState.TP:
                    animator.Play("PlayerTeleport");
                    lastState = AnimationState.TP;
                    break;
                case AnimationState.NULL:
                    break;
            }

            if (curState == AnimationState.NULL)
            {
                switch (lastState)
                {
                    case AnimationState.LEFT:
                        animator.Play("PlayerIdleLeft");
                        break;
                    case AnimationState.RIGHT:
                        animator.Play("PlayerIdleRight");
                        break;
                    case AnimationState.UP:
                        animator.Play("PlayerIdleUp");
                        break;
                    case AnimationState.DOWN:
                        animator.Play("PlayerIdleDown");
                        break;
                    case AnimationState.TP:
                        animator.Play("PlayerTeleport");
                        break;
                }
            }

            curState = AnimationState.NULL;


            //if (animstate.Count > 0)
            //{
            //    if (animstate.Contains(AnimationState.LEFT))
            //    {
            //        animator.Play("PlayerWalkLeft");
            //    }
            //    else if (animstate.Contains(AnimationState.RIGHT))
            //    {
            //        animator.Play("PlayerWalkRight");
            //    }
            //    else if (animstate.Contains(AnimationState.UP))
            //    {
            //        animator.Play("PlayerWalkUp");
            //    }
            //    else if (animstate.Contains(AnimationState.DOWN))
            //    {
            //        animator.Play("PlayerWalkDown");
            //    }
            //}
            //else
            //{
            //    if (lastState == AnimationState.LEFT)
            //        animator.Play("PlayerIdleLeft");
            //    else if (lastState == AnimationState.RIGHT)
            //        animator.Play("PlayerIdleRight");
            //    else if (lastState == AnimationState.UP)
            //        animator.Play("PlayerIdleUp");
            //    else if (lastState == AnimationState.DOWN)
            //        animator.Play("PlayerIdleDown");
            //}

            //animstate.Clear();
        }

        private void collectRune(RuneType collectedType)
        {
            Debug.Log("Collected rune! Now have " + ++runes + " runes.");
            if (collectedType == RuneType.AIR)
                Debug.Log("Collected air rune!");
        }
        
    }
}