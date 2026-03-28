using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObj;
    private Rigidbody _rb;
    const float G = 6.67f;

    [SerializeField] bool isSun = false;
    [SerializeField] int orbitSpeed = 1000;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (otherObj == null)
        { 
            otherObj = new List<Gravitation>();
        }
        otherObj.Add(this);

        if (!isSun) { _rb.AddForce(Vector3.left * orbitSpeed, ForceMode.Acceleration); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObj)
        {
            if ( obj != this )
            {
                Attract(obj);
            }
           
        }
    }
    void Attract (Gravitation other)
    {
        Rigidbody otherRb = other._rb;
        Vector3 direction = _rb.position - otherRb.position;

        float distance = direction.magnitude;
        if (distance == 0f) return;

        float forceMagnitude = G * (_rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce);
    }
}
