using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum SkateboardTrickState
    {
        Coast,
        Falling,
        Ollie,
        Kickflip,
        Shuvit,
        Coffin,
        Grind
    }

    private SkateboardTrickState currentState;

    // References
    private Rigidbody2D _rb;
    private PlayerModel _model;
    private BoxCollider2D _col;

    // Variables
    private bool _grounded;
    private bool _canGrind;

    public bool IsFalling
    {
        get { return _rb.velocity.y < 0.01f; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _model = GetComponent<PlayerModel>();
        _col = GetComponent<BoxCollider2D>();

        currentState = SkateboardTrickState.Coast;
    }

    private bool upSwipe, downSwipe, leftSwipe, rightSwipe, press;


    private void Update()
    {
        GetInputs(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(currentState);
        GetInputs(true);
        _grounded = GetGrounded();
        ConstantMove();
        DummyInputHandling();
        GetInputs(false);
    }

    private void GetInputs(bool get)
    {
        // tihihi
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
    }

    private void DummyInputHandling()
    {
        if (upSwipe)
        {
            if (CanOllie())
            {
                Ollie();
                return;
            }

            if (CanKickflip())
            {
                Kickflip();
                return;
            }
        }

        if (CanCoast())
        {
            currentState = SkateboardTrickState.Coast;
            return;
        }

        if (CanFall())
        {
            currentState = SkateboardTrickState.Falling;
            return;
        }
        
        
        return;
        if (currentState != SkateboardTrickState.Falling && IsFalling) // Makes it so it only changes the state once.
            currentState = SkateboardTrickState.Falling;
        else if (currentState != SkateboardTrickState.Coast && _grounded)
            currentState = SkateboardTrickState.Coast;

        if (upSwipe)
        {
            if (currentState == SkateboardTrickState.Coast && currentState != SkateboardTrickState.Ollie)
            {
                Ollie();
                return;
            }
            else if (currentState == SkateboardTrickState.Ollie && currentState != SkateboardTrickState.Kickflip)
            {
                Kickflip();
                return;
            }
        }

        if (rightSwipe)
        {
            if (currentState == SkateboardTrickState.Coast) Shuvit();
        }

        if (downSwipe)
        {
            if (currentState == SkateboardTrickState.Coast)
                StartCoroutine(Coffin());
        }

        if (press)
        {
            if (_canGrind)
                Grind();
        }
    }

    private void ConstantMove()
    {
        _rb.velocity = new Vector2(_model.movementSpeed * Time.deltaTime, _rb.velocity.y);
    }

    private void AddToCurrentVelocity(Vector2 addedVelocity)
    {
        Vector3 vel = _rb.velocity + addedVelocity;
        _rb.velocity = new Vector2(vel.x, Mathf.Clamp(vel.y, addedVelocity.y, _model.maxJumpVelocity));
    }

    // Assuming it's checked after all the other tricks
    private bool CanCoast()
    {
        return _grounded && _rb.velocity.y <= -Mathf.Epsilon; // epsilon is a "really tiny number" // leo
    }

    private bool CanFall()
    {
        return (!_grounded && _rb.velocity.y < 0);
    }

    private bool CanOllie()
    {
        if (!_grounded) return false;

        if (currentState == SkateboardTrickState.Coast) return true;

        return false;
    }

    private void Ollie()
    {
        Debug.Log("Ollie");
        currentState = SkateboardTrickState.Ollie;
        AddToCurrentVelocity(Vector2.up * _model.ollieJumpForce);
        _grounded = false;
    }

    private bool CanKickflip()
    {
        if (_grounded) return false;

        if (currentState == SkateboardTrickState.Ollie) return true;

        return false;
    }

    private void Kickflip()
    {
        Debug.Log("Kickflip");
        currentState = SkateboardTrickState.Kickflip;
        AddToCurrentVelocity(Vector2.up * _model.kickflipJumpForce);
    }

    private void Shuvit()
    {
        Debug.Log("Shuvit");
        currentState = SkateboardTrickState.Shuvit;
        _rb.velocity = new Vector2(_rb.velocity.x, _model.shuvitJumpForce);
    }

    private void Grind()
    {
        Debug.Log("Grind");
        currentState = SkateboardTrickState.Grind;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }

    private IEnumerator Coffin()
    {
        Debug.Log("Coffin");
        currentState = SkateboardTrickState.Coffin;
        _col.size = new Vector2(_col.size.x, _col.size.y / 4);
        yield return new WaitForSecondsRealtime(_model.coffinTime);
        _col.size = new Vector2(_col.size.x, _col.size.y * 4);
    }

    ContactPoint2D[] _collisionBuffer = new ContactPoint2D[100];

    private bool GetGrounded()
    {
        int count = _col.GetContacts(_collisionBuffer);
        for (int i = 0; i < count; i++)
        {
            if (LayerMask.GetMask(LayerMask.LayerToName(_collisionBuffer[i].collider.gameObject.layer)) == _model.groundLayers) continue; 
            if (Vector2.Angle(_collisionBuffer[i].normal, Vector2.up) < _model.maxGroundAngle)
            {
                return true;
            }
        }

        return false;
    }
}