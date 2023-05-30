using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System.Diagnostics;
using Player.Input;
using Debug = UnityEngine.Debug; //Needed to stop the default C# diagnostics from taking over debug commands

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public TrickState CurrentState;


        private float oldTimeScale;
        private PlayerScoreModel scoreModel;

        public Coroutine slowmoCoolDown;

        private Stopwatch slowmoCoolDownTimer;

        public ParticleSystem trickParticles;
        public ParticleSystem grindParticles;

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

        [HideInInspector] public PlayerDeathHandler deathHandler;


        private void OnEnable()
        {
            dragActionUp.performed += SwipeUpReceived;
            dragActionDown.performed += SwipeDownReceived;
            dragActionRight.performed += SwipeRightReceived;
            dragActionLeft.performed += SwipeLeftReceived;
            touch.performed += OnTouchDownPerformed;
            touch.canceled += TouchStopped;
            tap.performed += Tap;


            scoreModel = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
            //trickParticles = gameObject.GetComponentInChildren<ParticleSystem>();
            
            trickParticles.Stop();
            grindParticles.Stop();

        }

        private void OnDisable()
        {
            dragActionUp.performed -= SwipeUpReceived;
            dragActionDown.performed -= SwipeDownReceived;
            dragActionRight.performed -= SwipeRightReceived;
            dragActionLeft.performed -= SwipeLeftReceived;
            touch.performed -= OnTouchDownPerformed;
            touch.canceled -= TouchStopped;
            tap.performed -= Tap;
            CancelSlowmo();
        }

        // Variables
        [HideInInspector] public bool grounded;
        [HideInInspector] public bool walled;
        [HideInInspector] public Transform[] grindPath;
        [HideInInspector] public bool canGrind = false;
        [HideInInspector] public bool isGrinding = false;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
            model = GetComponent<PlayerModel>();
            _col = GetComponent<BoxCollider2D>();
            deathHandler = GetComponent<PlayerDeathHandler>();
            dragActionUp = playerInput.actions.FindAction("SwipeUp");
            dragActionDown = playerInput.actions.FindAction("SwipeDown");
            dragActionLeft = playerInput.actions.FindAction("SwipeLeft");
            dragActionRight = playerInput.actions.FindAction("SwipeRight");
            touch = playerInput.actions.FindAction("Touch");
            tap = playerInput.actions.FindAction("Tap");
            slowmoCoolDownTimer = new Stopwatch();
            touch.performed += context => Debug.Log("Touch Down Performed");

            var swipeActionUp = new SwipeInputAction(dragActionUp, touch, touch);
            var swipeActionDown = new SwipeInputAction(dragActionDown, touch, touch);
            var swipeActionRight = new SwipeInputAction(dragActionRight, touch, touch);
            
            
            oldTimeScale = Time.timeScale;


            // Transitions
            var coast = new CoastState();
            var ollie = new OllieState(0.45f);
            var kickflip = new KickflipState(0.3f);
            var shuvit = new ShuvitState(0.3f);
            var coffin = new CoffinState();
            var grind = new GrindingState();
            var falling = new FallingState();
            var crashed = new CrashState();
            CurrentState = coast;

            // Up swipe
            new OllieTransition(coast, ollie, swipeActionUp);
            new OllieTransition(grind, ollie, swipeActionUp);
            new KickflipTransition(ollie, kickflip, swipeActionUp);
            new InputTransition(coffin, coast, dragActionUp);

            // Right swipe
            new ShuvitTransition(coast, shuvit, swipeActionRight);

            // Down swipe
            new CoffinTransition(coast, coffin, swipeActionDown);

            // Transitions to grind
            new GrindTransition(coast, grind, touch);
            new GrindTransition(ollie, grind, touch);
            new GrindTransition(kickflip, grind, touch);
            new GrindTransition(shuvit, grind, touch);
            new GrindTransition(falling, grind, touch);

            // Transitions to falling
            new FallingTransition(coast, falling);
            new FallingTransition(ollie, falling);
            new FallingTransition(kickflip, falling);
            new FallingTransition(shuvit, falling);
            new FallingTransition(coffin, falling);
            new FallingTransition(grind, falling);
            new InputTransition(grind, falling, touch, true); // add holding here

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
            new GroundedTransition(grind, coast);


            CurrentState.Enter(this);

            targetPlayerHeight = model.playerStandHeight;
            UpdatePlayerHeight(targetPlayerHeight);
        }


        private void DoubleTap()
        {
            //Debug.Log("Double tap!");
            if (scoreModel != null)
            {
                if (scoreModel.TryToUsePowerUp())
                {
                    oldTimeScale = Time.timeScale;
                    Time.timeScale = 0.5f;
                    slowmoCoolDown = StartCoroutine(SlowmoCoolDown());
                    slowmoCoolDownTimer.Reset();
                    slowmoCoolDownTimer.Start();
                }
            }
        }

        public void PauseSlowmo()
        {
            if (slowmoCoolDownTimer.IsRunning)
            {
                slowmoCoolDownTimer.Stop();
            }
            else
            {
                slowmoCoolDownTimer.Restart();
            }
        }

        public void CancelSlowmo()
        {
            if (slowmoCoolDownTimer.IsRunning)
            {
                Time.timeScale = oldTimeScale;
            }
            Time.timeScale = oldTimeScale;
        }

        IEnumerator SlowmoCoolDown()
        {
            while (true)
            {
                //Debug.Log("Elapsed time: " + slowmoCoolDownTimer.Elapsed.Seconds);
                if (slowmoCoolDownTimer.Elapsed.Seconds > 3f)
                {
                    Time.timeScale = oldTimeScale;
                    yield break;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        #region TouchInput stuff

        private void SwipeUpReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                //Debug.Log("Up!");
            }
        }

        private void SwipeRightReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                //Debug.Log("Right!");
            }
        }

        private void SwipeLeftReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                //Debug.Log("Left!");
            }
        }

        private void SwipeDownReceived(InputAction.CallbackContext context)
        {
            if (!SwipeLock)
            {
                SwipeLock = true;
                //Debug.Log("Down!"); //TODO: Trigger crouch here!
            }
        }

        private void Tap(InputAction.CallbackContext context)
        {
            //Debug.Log("Running tap event");
            if (!tappedOnce)
            {
                //Debug.Log("Tap!");
                tappedOnce = true;
                StartCoroutine(DoubleTapCooldown());
                //CheckInteract();
            }
            else
            {
                DoubleTap();
                tappedOnce = false;
                StopCoroutine(DoubleTapCooldown());
            }
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

        private void OnTouchDownPerformed(InputAction.CallbackContext context)
        {
            //Debug.Log("TouchDown");
            CheckInteract();
        }
        

        #endregion

        void FixedUpdate()
        {
            GroundCheck();
            UpdatePlayerHeight(targetPlayerHeight, model.smoothCrouch);
            if (model.isAlive)
                ConstantMove();
            CurrentState.Update(this);
        }

        private void ConstantMove()
        {
            rb.velocity = new Vector2(model.movementSpeed * Time.deltaTime, rb.velocity.y);
        }

        public void AddToCurrentVelocity(Vector2 addedVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, addedVelocity.y);
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
            if(interactBuffer == null)
                return; 
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, model.interactRadius, interactBuffer,
                model.groundLayers);
            for (int i = 0; i < count; i++)
            {
                if (interactBuffer[i].transform.parent == null) continue;

                if (interactBuffer[i].transform.parent.TryGetComponent(out IInteractable interactable))
                    interactable.Interact(this);
            }
        }

        [HideInInspector] public float targetPlayerHeight;

        private void UpdatePlayerHeight(float height, bool smooth = false)
        {
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
        }

        public void EnterGrinding(Transform[] rail)
        {
            canGrind = true;
            grindPath = rail;
        }


        ContactPoint2D[] _collisionBuffer = new ContactPoint2D[100];

        [HideInInspector] public Vector2 wallNormal;

        private void GroundCheck()
        {
            walled = false;
            grounded = false;
            int count = _col.GetContacts(_collisionBuffer);
            for (int i = 0; i < count; i++)
            {
                
                
                if (((1 << _collisionBuffer[i].collider.gameObject.layer) & model.groundLayers) == 0) continue;

                if (Vector2.Angle(_collisionBuffer[i].normal, Vector2.up) < model.maxGroundAngle)
                {
                    grounded = true;
                }

                if (Vector2.Angle(_collisionBuffer[i].normal, Vector2.left) < model.maxWallAngle)
                {
                    if (deathHandler.invincible == true)
                    {
                        //Physics.IgnoreCollision(_collisionBuffer[i].rigidbody.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                        _collisionBuffer[i].collider.enabled = false;
                        continue;
                    }
                    
                    wallNormal = _collisionBuffer[i].normal;
                    walled = deathHandler.OnDeath();
                    if (walled == false)
                    {
                        _collisionBuffer[i].collider.enabled = false;
                    }
                }
            }
        }
    }
}