using UnityEngine;

public class Ef_AutoMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Vector3 moveDirection = Vector3.right;

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
