using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlocks : MonoBehaviour
{
    [SerializeField] private float speed = 0.65f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
