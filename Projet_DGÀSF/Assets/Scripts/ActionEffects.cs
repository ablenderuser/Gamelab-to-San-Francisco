using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEffects : MonoBehaviour
{
    public void Run(GameObject gameObject)
    {
        Rigidbody2D m_RigidBody = gameObject.GetComponent<Rigidbody2D>();
        Vector2 m_Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log("Run");
        while (m_Movement.magnitude != 0)
        {
            Debug.Log("Déplacement");
            m_RigidBody.MovePosition(m_RigidBody.position + 60 * m_Movement * Time.fixedDeltaTime);
            m_Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
