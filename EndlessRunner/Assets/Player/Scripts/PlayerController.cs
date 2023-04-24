using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References
    private Rigidbody2D _rb;
    private PlayerModel _model;
    
    // Variables
    private bool _grounded;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _model = GetComponent<PlayerModel>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ConstantMove();
        DummyInputHandling();
    }

    private void DummyInputHandling()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _grounded)
        {
            Ollie();
        }
    }
    
    private void ConstantMove()
    {
        
        _rb.velocity = new Vector2 (_model.movementSpeed * Time.deltaTime, _rb.velocity.y);;
        
    }

    private void Ollie()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _model.ollieJumpForce);
        _grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _grounded = true;
    }
}
