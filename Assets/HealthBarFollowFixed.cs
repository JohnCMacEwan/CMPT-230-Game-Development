using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform character;
    public Vector3 offset; 

    void Update()
    {
    
        transform.position = character.position + offset;

    
        transform.rotation = Quaternion.identity;
    }
}