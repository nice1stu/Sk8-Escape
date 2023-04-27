using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingmovement : MonoBehaviour
{
     public float speed = 5f; // Adjust this to change the player's speed
   
       void Update()
       {
           transform.Translate(Vector3.right * speed * Time.deltaTime);
       }
}
