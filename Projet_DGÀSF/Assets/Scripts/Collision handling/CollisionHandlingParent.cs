using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlingParent : MonoBehaviour
{
    private bool INzone = false;
    private Renderer sprite;

    private int initialOrder;

    public int targetOrder = 5;

    // Start is called before the first frame update
    void Start()
    {
    	sprite = GetComponentInParent<SpriteRenderer>();
        if (sprite == null)
        {
            sprite = GetComponentInParent<ParticleSystem>().GetComponent<Renderer>();
        }
        initialOrder = sprite.sortingOrder;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(INzone);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Pour éviter que les colliders des autres décors posent problème
        bool decorCollider = (collision.tag == "DecorCollider");
        if (!decorCollider) decorCollider = (collision.tag == "Linge");

        //Debug.Log("Entré");
        if (!INzone && !decorCollider) {
            INzone = true;
            sprite.sortingOrder = targetOrder;
            Vector3 newPosition = transform.parent.position;
            newPosition.z = transform.parent.position.z + 0.5f;
            transform.parent.position = newPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Sorti");
        sprite.sortingOrder = initialOrder;
        Vector3 newPosition = transform.parent.position;
        newPosition.z = transform.parent.position.z - 0.5f;
        transform.parent.position = newPosition;
        INzone = false;
    }
}
