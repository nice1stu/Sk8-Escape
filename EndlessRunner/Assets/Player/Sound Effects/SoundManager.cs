using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ private PlayerController playerController;
   public AudioSource audioSource;
   public AudioSource jumpAudioSource;
   public AudioSource grindingAudioSource;
   public AudioSource crashAudioSource;
   private bool isGrounded;
   public LayerMask targetLayer;
   private PlayerModel _death;
   
   private void Start()
   {
       playerController = FindObjectOfType<PlayerController>();
       _death = FindObjectOfType<PlayerModel>();
   }


   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Ground"))
       {
           isGrounded = true;
           Debug.Log("is grounded");
           
           if (isGrounded && !audioSource.isPlaying)
           {
               audioSource.Play();
               Debug.Log("playing sound");
           }
       }
       if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
       {
           Debug.Log("crashed");
          audioSource.Stop();
          crashAudioSource.Play();
       }


   }

   private void Update()
   {
       if (!isGrounded && playerController.isGrinding && !grindingAudioSource.isPlaying)
       {
           audioSource.Stop();
           grindingAudioSource.Play();
           Debug.Log("Play grind sound");
           
       }
       else if (!playerController.isGrinding && grindingAudioSource.isPlaying)
       {
           grindingAudioSource.Stop();
           Debug.Log("Stop grind sound");
       }
   }

   

   private void OnCollisionExit2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Ground"))
       {
           Debug.Log("is not grounded");
           isGrounded = false;

           if (!isGrounded && audioSource.isPlaying)
           {
               audioSource.Stop();
               jumpAudioSource.Play();
               Debug.Log("not playing sound");
           }
       }
      
   }
   
}
   


