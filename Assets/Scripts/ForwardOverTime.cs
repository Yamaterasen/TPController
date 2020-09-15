using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardOverTime : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * m_Speed;
    }
}
