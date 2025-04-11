using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speedIncreaseRate = 0.1f; // quanto a velocidade aumenta por segundo

    private MeshRenderer meshRenderer;

    public float forwardSpeed = 10f;
    public float lateralSpeed = 10f;

    private CharacterController controller;
    private Vector3 moveDirection;
    public float jumpForce = 8f;
    public float gravity = 20f;

    private bool isSliding = false;
    private bool isStretching = false;

    private float originalHeight;
    private Vector3 originalCenter;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        originalCenter = controller.center;
    }

    void Update()
    {
        forwardSpeed += speedIncreaseRate * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C))
        {
            meshRenderer.material.color = Random.ColorHSV();
        }

        Vector3 move = Vector3.zero;

        // Movimento lateral contínuo (livre)
        float horizontalInput = Input.GetAxis("Horizontal");
        move.x = horizontalInput * lateralSpeed;

        // Movimento contínuo para frente
        move.z = forwardSpeed;

        // Gravidade e pulo
        if (controller.isGrounded)
        {
            moveDirection.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }

            if (Input.GetKeyDown(KeyCode.S) && !isSliding)
            {
                StartCoroutine(Slide());
            }

            if (Input.GetKeyDown(KeyCode.W) && !isStretching)
            {
                StartCoroutine(Stretch());
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        move.y = moveDirection.y;

        controller.Move(move * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("O jogador colidiu com um obstáculo!");
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        transform.localScale = new Vector3(1.8f, 0.2f, 1.8f);
        controller.height = originalHeight * 0.5f;
        controller.center = new Vector3(originalCenter.x, controller.height / 2f, originalCenter.z);

        yield return new WaitForSeconds(1f);

        transform.localScale = Vector3.one;
        controller.height = originalHeight;
        controller.center = originalCenter;
        isSliding = false;
    }

    IEnumerator Stretch()
    {
        isStretching = true;
        transform.localScale = new Vector3(0.5f, 2f, 1.5f);
        yield return new WaitForSeconds(1f);
        transform.localScale = Vector3.one;
        isStretching = false;
    }
}
