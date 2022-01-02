using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    [SerializeField] private GameObject openDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            Debug.Log("Exit open");
            openDoor.SetActive(true);
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
