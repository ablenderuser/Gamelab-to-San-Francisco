using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    public float m_XShift;
    public float m_YShift;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*Transform m_CameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        Transform m_SpriteMaskTransform = GameObject.Find("Sprite Mask").GetComponent<Transform>();
        if ((transform.position.x - m_CameraTransform.position.x) > 130){
            Debug.Log("Droite");
            m_SpriteMaskTransform.Translate(new Vector3(260, 0, 0));
            m_CameraTransform.Translate(new Vector3(260, 0, 0));
        } else {
            if ((transform.position.x - m_CameraTransform.position.x) < -130){
                Debug.Log("Gauche");
                m_SpriteMaskTransform.Translate(new Vector3(-260, 0, 0));
                m_CameraTransform.Translate(new Vector3(-260, 0, 0));
            } else {
                if ((transform.position.y - m_CameraTransform.position.y) > 70){
                    Debug.Log("Haut");
                    m_SpriteMaskTransform.Translate(new Vector3(0, 140, 0));
                    m_CameraTransform.Translate(new Vector3(0, 140, 0));
                } else {
                    if ((transform.position.y - m_CameraTransform.position.y) < -70) {
                        Debug.Log("Bas");
                        m_SpriteMaskTransform.Translate(new Vector3(0, -140, 0));
                        m_CameraTransform.Translate(new Vector3(0, -140, 0));
                    }
                }
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Passé");

        Transform m_CameraTransform = GameObject.Find("MainCamera").GetComponent<Transform>();
        Transform m_SpriteMaskTransform = GameObject.Find("CameraSpriteMask").GetComponent<Transform>();
        Transform m_CollidersTransform = GameObject.Find("CameraShiftColliders").GetComponent<Transform>();
        Debug.Log(m_CollidersTransform.position);

        Vector3 m_Shift = new Vector3(m_XShift, m_YShift, 0);
        m_SpriteMaskTransform.Translate(m_Shift);
        m_CameraTransform.Translate(m_Shift);
        m_CollidersTransform.Translate(m_Shift);
        Debug.Log(m_CollidersTransform.position);
    }
}
