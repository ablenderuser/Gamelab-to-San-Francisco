using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlingParent : MonoBehaviour
{
    private bool INzone = false;
    private Renderer sprite;

    // Start is called before the first frame update
    void Start()
    {
    	sprite = GetComponentInParent<SpriteRenderer>();
        if (sprite == null)
        {
            sprite = GetComponentInParent<ParticleSystem>().GetComponent<Renderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(INzone);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entré");
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
        Debug.Log("Sorti");
        sprite.sortingOrder = 3;
        Vector3 newPosition = transform.parent.position;
        newPosition.z = transform.parent.position.z - 0.5f;
        transform.parent.position = newPosition;
        INzone =false;
    }
}
