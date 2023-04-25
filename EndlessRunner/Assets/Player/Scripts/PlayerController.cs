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

    void FixedUpdate()
    {
        //Debug.Log(currentState);
        // GetInputs(true);
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
        Debug.Log(_grounded);
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

        if (rightSwipe)
        {
            if (CanShuvit())
            {
                Shuvit();
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
    }

    private void ConstantMove()
    {
        _rb.velocity = new Vector2(_model.movementSpeed * Time.deltaTime, _rb.velocity.y);
    }

    private void AddToCurrentVelocity(Vector2 addedVelocity)
    {
        _rb.velocity = addedVelocity;
        return;
    }

    #region Trick stuff

    // Assuming it's checked after all the other tricks
    private bool CanCoast()
    {
        return _grounded && _rb.velocity.y <= -Mathf.Epsilon; // epsilon is a "really tiny number" // leo
    }

    private bool CanFall()
    {
        return (!_grounded && _rb.velocity.y < _model.initialFallingVelocity);
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

    private bool CanShuvit()
    {
        if (!_grounded) return false;

        if (currentState == SkateboardTrickState.Coast) return true;

        return false;
    }

    private void Shuvit()
    {
        Debug.Log("Shuvit");
        currentState = SkateboardTrickState.Shuvit;
        AddToCurrentVelocity(Vector2.up * _model.shuvitJumpForce);
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

    #endregion

    ContactPoint2D[] _collisionBuffer = new ContactPoint2D[100];

    private bool GetGrounded()
    {
        int count = _col.GetContacts(_collisionBuffer);
        for (int i = 0; i < count; i++)
        {
            if (((1 << _collisionBuffer[i].collider.gameObject.layer) & _model.groundLayers) == 0) continue;

            if (Vector2.Angle(_collisionBuffer[i].normal, Vector2.up) < _model.maxGroundAngle)
            {
                return true;
            }
        }

        return false;
    }
}