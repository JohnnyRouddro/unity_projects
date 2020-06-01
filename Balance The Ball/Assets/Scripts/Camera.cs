using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float smoothMove = 1f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    public Transform playerTransform;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, smoothMove * Time.deltaTime);
    }
}
