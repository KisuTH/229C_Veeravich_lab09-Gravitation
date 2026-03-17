using UnityEngine;
using System.Collections.Generic;

public class Gravitational : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.3006674f;
    public static List<Gravitational> OtherObjectList;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (OtherObjectList == null) 
        {
            OtherObjectList = new List<Gravitational>();
        }
        OtherObjectList.Add(this);
            
    }

    private void FixedUpdate()
    {
        foreach (Gravitational obj in OtherObjectList)
        {
            if (obj != this)
            {
                AttractForce(obj);
            }
        }

    }
    void AttractForce(Gravitational other)
    {
        Rigidbody otherRb = other.rb;
        // หาทิศทางของวัตถุ
        Vector3 direction = rb.position - otherRb.position;
        // Distance between object
        float distance = direction.magnitude;
        //ถ้าวัตถุอยู่ตำแหน่งเดียวกัน ไม่ให้ทำอะไร
        if (distance == 0f) { return; }
        // ฝช้สูตรหาแรงดึงดูด F = G*((m1* m2)/r^2)
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        // รวมทิศทางเข้ากับแรงดึงดูดที่ได้
        Vector3 gravityForce = forceMagnitude * direction.normalized;
        // ใส่แรงดึงดูดที่ได้กับวัตถุอื่น
        otherRb.AddForce(gravityForce);
    }
}
