using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{

    private bool INzone = false;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
    	sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(INzone);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!INzone)
        {
            INzone = true;
            sprite.sortingOrder = 2;
            Vector3 newPosition = transform.position;
            newPosition.z = transform.position.z + 0.5f;
            transform.position = newPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.sortingOrder = 0;
        Vector3 newPosition = transform.position;
        newPosition.z = transform.position.z-0.5f;
        transform.position = newPosition;
        INzone =false;
    }
}
