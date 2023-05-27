using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    
    private PlayerController playerController;
    public AudioSource audioSource;
    public AudioSource jumpAudioSource;
    public AudioSource grindingAudioSource;
    public AudioSource crashAudioSource;
    private bool isGrounded;
    public LayerMask targetLayer;
    private PlayerModel _death;
    public PauseMenu _isPaused;
    
    private void Start()
    {
       playerController = FindObjectOfType<PlayerController>();
       _death = FindObjectOfType<PlayerModel>(); 
       //_isPaused = FindObjectOfType<PauseMenu>();
    }


   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Ground"))
       {
           isGrounded = true;

           if (isGrounded && !audioSource.isPlaying)
           {
               audioSource.Play();
           }
       }
       if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
       {
           audioSource.Stop();
          crashAudioSource.Play();
       }


   }

   private void Update()
   {
       if (!isGrounded && playerController.isGrinding && !grindingAudioSource.isPlaying && !_isPaused.IsItPaused)
       {
           audioSource.Stop();
           grindingAudioSource.Play();
       }
       else if (!playerController.isGrinding && grindingAudioSource.isPlaying)
       {
           grindingAudioSource.Stop();
       }
   }

   

   private void OnCollisionExit2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Ground"))
       {
           isGrounded = false;

           if (!isGrounded && audioSource.isPlaying)
           {
               audioSource.Stop();
               jumpAudioSource.Play();
           }
       }
      
   }
   
}
   


