using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    public Sprite buttonDown;

    [SerializeField] private GameObject attachedBar;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToTravel;

    private float targetPosition;
    private bool pressed;

    private void Start()
    {
        pressed = false;
        targetPosition = attachedBar.transform.position.x + distanceToTravel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !pressed)
        {
            GetComponent<SpriteRenderer>().sprite = buttonDown;
            pressed = true;
            Debug.Log("Button Pressed");
        }
    }

    private void Update()
    {
        if (pressed)
        {
            MoveBar();
        }
    }


    private void MoveBar()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        attachedBar.transform.position = Vector3.MoveTowards(attachedBar.transform.position,
                                                            new Vector3(targetPosition,
                                                                        attachedBar.transform.position.y,
                                                                        attachedBar.transform.position.z),
                                                            step);
    }
}