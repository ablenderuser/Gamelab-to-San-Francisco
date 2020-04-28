using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlingParent : MonoBehaviour
{
    private bool INzone = false;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
    	sprite = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(INzone);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!INzone)
        {
            INzone = true;
            sprite.sortingOrder = 5;
            Vector3 newPosition = transform.parent.position;
            newPosition.z = transform.parent.position.z + 0.5f;
            transform.parent.position = newPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.sortingOrder = 3;
        Vector3 newPosition = transform.parent.position;
        newPosition.z = transform.parent.position.z-0.5f;
        transform.parent.position = newPosition;
        INzone =false;
    }
}
