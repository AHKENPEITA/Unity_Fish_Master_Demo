﻿using UnityEngine;

public class Ef_AutoTurn : MonoBehaviour
{
   public float speed=10f;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
