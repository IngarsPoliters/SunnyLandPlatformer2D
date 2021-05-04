using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPickup : MonoBehaviour
{
    [SerializeField] private AudioClip cherryPickup;
    [SerializeField] private int cherryPoint = 1;
    private bool IsTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTriggered == true) return;
        IsTriggered = false;
        FindObjectOfType<GameSession>().AddToCherries(cherryPoint);
        //this.GetComponent<AudioSource>().PlayOneShot(cherryPickup);
        AudioSource.PlayClipAtPoint(cherryPickup, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
