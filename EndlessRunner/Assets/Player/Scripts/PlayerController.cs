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


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _model = GetComponent<PlayerModel>();
        _col = GetComponent<BoxCollider2D>();

        currentState = SkateboardTrickState.Coast;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ConstantMove();
        DummyInputHandling();
    }

    private void DummyInputHandling()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentState == SkateboardTrickState.Coast)
                Ollie();
            if (currentState == SkateboardTrickState.Ollie)
                Kickflip();
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentState == SkateboardTrickState.Coast)
                Shuvit();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentState == SkateboardTrickState.Coast)
                StartCoroutine(Coffin());
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canGrind)
                Grind();
        }
    }

    private void ConstantMove()
    {
        _rb.velocity = new Vector2(_model.movementSpeed * Time.deltaTime, _rb.velocity.y);
    }

    private void Ollie()
    {
        currentState = SkateboardTrickState.Ollie;
        _rb.velocity = new Vector2(_rb.velocity.x, _model.ollieJumpForce);
        _grounded = false;
    }
    
    private void Kickflip()
    {
        currentState = SkateboardTrickState.Kickflip;
        _rb.velocity = new Vector2(_rb.velocity.x, _model.kickflipJumpForce);
        _grounded = false;
    }
    
    private void Shuvit()
    {
        currentState = SkateboardTrickState.Shuvit;
        _rb.velocity = new Vector2(_rb.velocity.x, _model.shuvitJumpForce);
        _grounded = false;
    }
    private void Grind()
    {
        currentState = SkateboardTrickState.Grind;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _grounded = false;
    }

    private IEnumerator Coffin()
    {
        currentState = SkateboardTrickState.Coffin;
        _col.size = new Vector2(_col.size.x, _col.size.y/4);
        yield return new WaitForSecondsRealtime(_model.coffinTime);
        _col.size = new Vector2(_col.size.x, _col.size.y*4);
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        _grounded = true;
    }
}