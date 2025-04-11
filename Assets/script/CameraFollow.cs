using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -8);
    public float smoothSpeed = 5f;

    private float fixedY; // altura fixa da c�mera

    void Start()
    {
        // Armazena a altura inicial da c�mera
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        // Mant�m a altura fixa, segue apenas X e Z do player
        Vector3 targetPos = new Vector3(target.position.x, fixedY, target.position.z) + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
