using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
    

        public TrickState currentState;

        //Input stuff for the touch controls
        private InputAction dragActionUp;
        private InputAction dragActionDown;
        private InputAction dragActionRight;
        private InputAction dragActionLeft;
        private InputAction touch;
        private InputAction tap;
        private PlayerInput playerInput;

        private bool SwipeLock = false;
        private bool tappedOnce = false;

        // References
        [FormerlySerializedAs("_rb")] [HideInInspector]
        public Rigidbody2D rb;
        [FormerlySerializedAs("_model")] [HideInInspector]
        public PlayerModel model;
        private BoxCollider2D _col;
        public PlayerView view;

        // Variables
        [FormerlySerializedAs("_grounded")] [HideInInspector]
        public bool grounded;
        private bool _canGrind;


        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
            model = GetComponent<PlayerModel>();
            _col = GetComponent<BoxCollider2D>();
            dragActionUp = playerInput.actions.FindAction("SwipeUp");
            dragActionDown = playerInput.actions.FindAction("SwipeDown");
            dragActionLeft = playerInput.actions.FindAction("SwipeLeft");
            dragActionRight = playerInput.actions.FindAction("SwipeRight");
            touch = playerInput.actions.FindAction("Touch");
            tap = playerInput.actions.FindAction("Tap");


            // Transitions
            var coast = new CoastState();
            var ollie = new OllieState(0.45f);
            var kickflip = new KickflipState(0.3f);
            var shuvit = new ShuvitState(0.3f);
            var coffin = new CoffinState();
            var falling = new FallingState();
            var crashed = new CrashState();
            currentState = coast;
            
            // Up swipe
            new OllieTransition(coast, ollie, dragActionUp);
            new KickflipTransition(ollie, kickflip, dragActionUp);
            new InputTransition(coffin, coast, dragActionUp);
            
            // Right swipe
            new ShuvitTransition(coast, shuvit, dragActionRight);
            
            // Down swipe
            new CoffinTransition(coast, coffin, dragActionDown);

            // Transitions to falling
            new FallingTransition(coast, falling);
            new FallingTransition(ollie, falling);
            new FallingTransition(kickflip, falling);
            new FallingTransition(shuvit, falling);
            new FallingTransition(coffin, falling);
            
            // Crash Transitions
            new CrashTransition(coast, crashed);
            new CrashTransition(ollie, crashed);
            new CrashTransition(kickflip, crashed);
            new CrashTransition(shuvit, crashed);
            new CrashTransition(coffin, crashed);
            new CrashTransition(falling, crashed);
            
            // Other transitions
            new TimedTransition(coffin, coast, model.coffinTime);
            new GroundedTransition(falling, coast);
            
            currentState.Enter(this);

            targetPlayerHeight = model.playerStandHeight;
            UpdatePlayerHeight(targetPlayerHeight);
        }

        private void OnEnable()
        {
            dragActionUp.performed += SwipeUpReceived;
            dragActionDown.performed += SwipeDownReceived;
            dragActionRight.performed += SwipeRightReceived;
            dragActionLeft.performed += SwipeLeftReceived;
            touch.canceled += TouchStopped;
            tap.performed += Tap;
        }

        private void OnDisable()
        {
            dragActionUp.performed -= SwipeUpReceived;
            dragActionDown.performed -= SwipeDownReceived;
            dragActionRight.performed -= SwipeRightReceived;
            dragActionLeft.performed -= SwipeLeftReceived;
            touch.canceled -= TouchStopped;
            tap.performed -= Tap;
        }
    

        private bool upSwipe, downSwipe, leftSwipe, rightSwipe, press, hold;

        #region TouchInput stuff

        private void SwipeUpReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Up!");
                upSwipe = true;
            }
        }

        private void SwipeRightReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Right!");
                rightSwipe = true;
            }
        }

        private void SwipeLeftReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Left!");
                leftSwipe = true;
            }
        }

        private void SwipeDownReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Down!"); //TODO: Trigger crouch here!
                downSwipe = true;
            }
        }

        private void Tap(InputAction.CallbackContext context)
        {
            Debug.Log("Running tap event");
            if (!tappedOnce)
            {
                Debug.Log("Tap!");
                tappedOnce = true;
                StartCoroutine(DoubleTapCooldown());
            }
            else
            {
                DoubleTap();
                tappedOnce = false;
                StopCoroutine(DoubleTapCooldown());
            }
        }

        private void DoubleTap()
        {
            Debug.Log("Double tap!");
        }

        IEnumerator DoubleTapCooldown()
        {
            yield return new WaitForSeconds(0.5f);
            tappedOnce = false;
        }

        private void TouchStopped(InputAction.CallbackContext context)
        {
            SwipeLock = false;
        }

        #endregion

        private void Update()
        {
            GetInputsKeyboard(true);
            currentState.Update(this);
        }

        void FixedUpdate()
        {
            //Debug.Log(currentState);
            // GetInputs(true);
            grounded = GetGrounded();
            UpdatePlayerHeight(targetPlayerHeight, model.smoothCrouch);
            if (model.isAlive)
                ConstantMove();
            GetInputsKeyboard(false);
        }

        private void GetInputsKeyboard(bool get)
        {
            // tihihi

            //So how does this work?
            if (upSwipe != get)
                upSwipe = Input.GetKeyDown(KeyCode.UpArrow);
            if (downSwipe != get)
                downSwipe = Input.GetKeyDown(KeyCode.DownArrow);
            if (leftSwipe != get)
                leftSwipe = Input.GetKeyDown(KeyCode.LeftArrow);
            if (rightSwipe != get)
                rightSwipe = Input.GetKeyDown(KeyCode.RightArrow);
            if (press != get)
                press = Input.GetKeyDown(KeyCode.Space);
            if (hold != get)
                hold = Input.GetKey(KeyCode.Space);
        }
        
        private void ConstantMove()
        {
            rb.velocity = new Vector2(model.movementSpeed * Time.deltaTime, rb.velocity.y);
        }

        public void AddToCurrentVelocity(Vector2 addedVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, addedVelocity.y);
            return;
        }

        #region Trick stuff

        // Assuming it's checked after all the other tricks
        private bool CanCoast()
        {
            return grounded && rb.velocity.y <= -Mathf.Epsilon; // epsilon is a "really tiny number" // leo
        }

        private bool CanFall()
        {
            return (!grounded && rb.velocity.y < model.initialFallingVelocity);
        }

        #endregion

        private Collider2D[] interactBuffer = new Collider2D[100];
        private void CheckInteract()
        {
            int count = Physics2D.OverlapBoxNonAlloc(_col.bounds.center, _col.size, transform.rotation.z, interactBuffer);
            for (int i = 0; i < count; i++) 
                if(interactBuffer[i].TryGetComponent(out IInteractable interactable)) interactable.Interact(this);
        }
    
        [HideInInspector]
        public float targetPlayerHeight;
        private void UpdatePlayerHeight(float height, bool smooth = false)
        {
            view.DummyCoffinScaler(_col.size, _col.offset);
            if (smooth)
            {
                float heightFrom = _col.size.y;
                _col.size = new Vector2(_col.size.x,
                    Mathf.Lerp(_col.size.y, height, model.crouchSharpness * Time.fixedDeltaTime));
                _col.offset = (_col.size.y / 2) * Vector2.up;
                if (!grounded)
                {
                    transform.position += (heightFrom - _col.size.y) * model.crouchAirRatio * Vector3.up;
                }
            }
            else
            {
                _col.size = new Vector2(_col.size.x, height);
                _col.offset = (_col.size.y / 2) * Vector2.up;
            }

            view.DummyCoffinScaler(_col.size, _col.offset);
        }


        ContactPoint2D[] _collisionBuffer = new ContactPoint2D[100];

        private bool GetGrounded()
        {
            int count = _col.GetContacts(_collisionBuffer);
            for (int i = 0; i < count; i++)
            {
                if (((1 << _collisionBuffer[i].collider.gameObject.layer) & model.groundLayers) == 0) continue;

                if (Vector2.Angle(_collisionBuffer[i].normal, Vector2.up) < model.maxGroundAngle)
                {
                    return true;
                }
            }

            return false;
        }
    }
}