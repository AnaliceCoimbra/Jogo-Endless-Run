using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -8);
    public float smoothSpeed = 5f;

    private float fixedY; // altura fixa da câmera

    void Start()
    {
        // Armazena a altura inicial da câmera
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        // Mantém a altura fixa, segue apenas X e Z do player
        Vector3 targetPos = new Vector3(target.position.x, fixedY, target.position.z) + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
