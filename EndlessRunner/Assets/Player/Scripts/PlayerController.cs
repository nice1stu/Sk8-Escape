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


            currentState = new CoastState();
            var ollie = new OllieState();
            var falling = new FallingState();
            new OllieTransition(currentState, ollie, dragActionUp);
            new OllieTransition(falling, ollie, dragActionUp);
            new FallingTransition(currentState, falling);
            new FallingTransition(ollie, falling);
            
            
            currentState.Enter(this);

            _targetPlayerHeight = model.playerStandHeight;
            UpdatePlayerHeight(_targetPlayerHeight);
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
                InputHandling();
            }
        }

        private void SwipeRightReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Right!");
                rightSwipe = true;
                InputHandling();
            }
        }

        private void SwipeLeftReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Left!");
                leftSwipe = true;
                InputHandling();
            }
        }

        private void SwipeDownReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                Debug.Log("Down!"); //TODO: Trigger crouch here!
                downSwipe = true;
                InputHandling();
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
            UpdatePlayerHeight(_targetPlayerHeight, model.smoothCrouch);
            if (model.isAlive)
                ConstantMove();
            ConstantMove();
            InputHandling();
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
    

        private void InputHandling()
        {
            // //Debug.Log(_grounded);
            // if (upSwipe)
            // {
            //     if (_currentCoffin != null)
            //     {
            //         CancelCoffin();
            //         return;
            //     }
            //
            //     if (CanOllie())
            //     {
            //         Ollie();
            //         return;
            //     }
            //
            //     if (CanKickflip())
            //     {
            //         Kickflip();
            //         return;
            //     }
            //
            //     upSwipe = false;
            // }
            //
            // if (rightSwipe)
            // {
            //     if (CanShuvit())
            //     {
            //         Shuvit();
            //     }
            //
            //     rightSwipe = false;
            // }
            //
            // if (downSwipe)
            // {
            //     if (CanCoffin())
            //     {
            //         _currentCoffin = StartCoroutine(Coffin());
            //     }
            //
            //     downSwipe = false;
            // }
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

        // private bool CanOllie()
        // {
        //     if (!grounded) return false;
        //
        //     if (currentState == SkateboardTrickState.Coast) return true;
        //
        //     return false;
        // }

        // private void Ollie()
        // {
        //     Debug.Log("Ollie");
        //     currentState = SkateboardTrickState.Ollie;
        //     AddToCurrentVelocity(Vector2.up * _model.ollieJumpForce);
        //     grounded = false;
        // }
        //
        // private bool CanKickflip()
        // {
        //     if (grounded) return false;
        //
        //     if (currentState == SkateboardTrickState.Ollie) return true;
        //
        //     return false;
        // }
        //
        // private void Kickflip()
        // {
        //     Debug.Log("Kickflip");
        //     currentState = SkateboardTrickState.Kickflip;
        //     AddToCurrentVelocity(Vector2.up * _model.kickflipJumpForce);
        // }
        //
        // private bool CanShuvit()
        // {
        //     if (!grounded) return false;
        //
        //     if (currentState == SkateboardTrickState.Coast) return true;
        //
        //     return false;
        // }
        //
        // private void Shuvit()
        // {
        //     Debug.Log("Shuvit");
        //     currentState = SkateboardTrickState.Shuvit;
        //     AddToCurrentVelocity(Vector2.up * _model.shuvitJumpForce);
        // }
        //
        // private void Grind()
        // {
        //     Debug.Log("Grind");
        //     currentState = SkateboardTrickState.Grind;
        //     _rb.velocity = new Vector2(_rb.velocity.x, 0);
        // }
        //
        // private bool CanCoffin()
        // {
        //     if (!grounded) return false;
        //     if (currentState == SkateboardTrickState.Coast) return true;
        //     return false;
        // }
        //
        // private Coroutine _currentCoffin;
        //
        // private IEnumerator Coffin()
        // {
        //     Debug.Log("Coffin");
        //     currentState = SkateboardTrickState.Coffin;
        //     _targetPlayerHeight = _model.playerCrouchHeight;
        //     yield return new WaitForSeconds(_model.coffinTime);
        //     _targetPlayerHeight = _model.playerStandHeight;
        //     currentState = SkateboardTrickState.Coast;
        //     CancelCoffin();
        // }
        //
        // private void CancelCoffin()
        // {
        //     if (_currentCoffin != null) StopCoroutine(_currentCoffin);
        //     _targetPlayerHeight = _model.playerStandHeight;
        //     _currentCoffin = null;
        //     currentState = SkateboardTrickState.Coast;
        // }
        //
        // private bool CanGrind()
        // {
        //     
        // }

        #endregion

        private Collider2D[] interactBuffer = new Collider2D[100];
        private void CheckInteract()
        {
            int count = Physics2D.OverlapBoxNonAlloc(_col.bounds.center, _col.size, transform.rotation.z, interactBuffer);
            for (int i = 0; i < count; i++) 
                if(interactBuffer[i].TryGetComponent(out IInteractable interactable)) interactable.Interact(this);
        }
    
        private float _targetPlayerHeight;

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