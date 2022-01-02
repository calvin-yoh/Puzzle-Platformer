using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private Sprite openDoorSprite;

    private bool opened;

    // Start is called before the first frame update
    void Start()
    {
        opened = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: " + collision.tag);
        if (collision.tag == "Key")
        {
            Debug.Log("Exit open");
            opened = true;
            GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(opened)
        {
            if (collision.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    collision.gameObject.SetActive(false);
                    Debug.LogError("Implement multiplayer complete functionality");
                }
            }
        } 
    }
}
