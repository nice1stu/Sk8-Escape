using System;
using System.Collections;
using static RunInventoryManager;
using UnityEngine;

public class InRunChest : MonoBehaviour, IPickupable
{
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _col;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }

    public void OnPickup()
    {
        chestAmount++;
        Debug.Log("chest amount:" + chestAmount);
        _audioSource.Play();
        _spriteRenderer.color = Color.clear;
        _col.enabled = false;
        StartCoroutine(CO_DelayedDeath());
    }

    private IEnumerator CO_DelayedDeath()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }
}