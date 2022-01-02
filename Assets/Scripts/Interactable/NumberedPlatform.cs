using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberedPlatform : MonoBehaviour
{
    public float speed = 10f;

    public float distanceToTravel;
    
    private float targetHeight;
    private Vector3 initialPosition;

    public bool shouldRaise = false;

    [SerializeField] private int numPlayers = 1;

    private int playersOnBoard;

    [SerializeField] private Sprite[] allSpriteNumbers;

    [SerializeField] private SpriteRenderer numberPlaceholder;

    void Start()
    {
        initialPosition = transform.position;
        targetHeight = transform.position.y + distanceToTravel;
        playersOnBoard = 0;
        numPlayers = 1;//FindObjectOfType<NetworkSpawner>().playersInScene.Count;
        numberPlaceholder.sprite = allSpriteNumbers[numPlayers];
    }


    // Update is called once per frame
    void Update()
    {

        numberPlaceholder.sprite = allSpriteNumbers[numPlayers - playersOnBoard];

        if (playersOnBoard == numPlayers)
        {
            shouldRaise = true;
        }
        else 
        {
            shouldRaise = false;
        }

        if (shouldRaise)
        {
            RaisePlatform();
        }
        else
        {
            LowerPlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && (collision is BoxCollider2D))
        {
            playersOnBoard++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && (collision is BoxCollider2D))
        {
            playersOnBoard--;
        }
    }

    private void RaisePlatform()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, 
                                            new Vector3(transform.position.x, targetHeight, transform.position.z), 
                                                step);
    }

    private void LowerPlatform()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);
    }
}
