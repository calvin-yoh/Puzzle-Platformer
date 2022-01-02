using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            Debug.LogError("Implement multiplayer complete functionality");
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                collision.gameObject.SetActive(false);
                Debug.LogError("Implement multiplayer complete functionality");
            }
        }
    }
    */
}
