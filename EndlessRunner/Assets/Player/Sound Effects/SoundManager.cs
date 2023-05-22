using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
   public AudioSource audioSource;
   public AudioSource jumpAudioSource;
   private bool isGrounded;

   

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
   


