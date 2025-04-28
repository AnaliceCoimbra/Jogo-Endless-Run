using System.Net;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; //velocidade de movimento lateral, o qu�o r�pido ele desvia
    public float initialForwardSpeed = 5f; //velocidade inicial para andar para frente sozinho
    public float maxForwardSpeed = 20f; //at� onde a velocidade aumenta para andar para frente sozinho
    public float accelerationRate = 0.05f;
    public float laneLimit = 9f;//limite m�ximo que o jogador pode ir na tela, para os lados. Primeiro retorno: 3, muito curto. Atualiza��o: 4.
    public float jumpForce = 13f; //for�a do pulo normal
    public float DoubleJumpForce = 4f; //for�a do pulo duplo

    private Rigidbody rb;
    private bool isGrounded = true; //controla se o cubo est� no ch�o ou no ar
    private bool canDoubleJump = false; //pode pular
    private Vector3 originalScale; //armazena o tamanho de escala original do cubo, para voltar a isso depois de abaixar ou esticar
    private float currentForwardSpeed;
    private bool jumpRequested = false;
    private bool doubleJumpRequested = false;

  



    void Start()
    {
        

        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale; // Salva o tamanho de escala original do cubo
        currentForwardSpeed = initialForwardSpeed;// velocidade inicial do cubo/jogador

        // Ajusta o centro de massa para evitar tombar
        rb.centerOfMass = new Vector3(0, -1f, 0);
        rb.freezeRotation = true; // Impede o Rigidbody de rodar (n�o tomba)

    }

    void Update()
    {
        HandleInput(); //chama o m�todo que cuida do movimento lateral <- -> 
        HandleJump(); //fun��o do pulo
        HandleCrouchAndStretch(); //fun��o de esticar e abaixar
        AccelerateForward(); // chama acelera��o de velocidade
    }

    void FixedUpdate()
    {
        // Movimento lateral
        Vector3 move = Vector3.right * horizontalInput * moveSpeed * Time.fixedDeltaTime;
        Vector3 targetPosition = rb.position + move;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -laneLimit, laneLimit);

        rb.MovePosition(new Vector3(targetPosition.x, rb.position.y, rb.position.z));


        // Movimento para frente (corrigido)
        Vector3 velocity = rb.linearVelocity;
        velocity = new Vector3(velocity.x, velocity.y, currentForwardSpeed);
        rb.linearVelocity = velocity;

        // Pular no momento certo (corrigir pulo pequeno)
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpRequested = false;
        }
        else if (doubleJumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, DoubleJumpForce, rb.linearVelocity.z);
            doubleJumpRequested = false;

        }
    }

    private float horizontalInput = 0f;

    void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void AccelerateForward()
    {
        // Aumenta gradualmente a velocidade para frente
        if (currentForwardSpeed < maxForwardSpeed)
        {
            currentForwardSpeed += accelerationRate * Time.deltaTime;
        }
    }



    void HandleJump() //pulos
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                // Primeiro pulo normal
                jumpRequested = true;
                isGrounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                // Segundo pulo (double jump)
                doubleJumpRequested = true;
                canDoubleJump = false;
            }
        }
    }

    void HandleCrouchAndStretch() //abaixar e esticar
    {
        // Abaixar
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z); //se apertar tecla para baixo, ele encolhe e vira um retangulo

            // Corrige a velocidade ap�s mudar escala
            Vector3 velocity = rb.linearVelocity;
            velocity.z = currentForwardSpeed;
            rb.linearVelocity = velocity;

        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) //se soltar  a tecla, volta a escala normal
        {
            transform.localScale = originalScale;

            // Corrige a velocidade ap�s mudar escala
            Vector3 velocity = rb.linearVelocity;
            velocity.z = currentForwardSpeed;
            rb.linearVelocity = velocity;
        }

        // Esticar
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.localScale = new Vector3(originalScale.x * 0.5f, originalScale.y * 2f, originalScale.z * 0.5f); //se apertar tecla pra cima ele fica mais alto

            // Corrige a velocidade ap�s mudar escala
            Vector3 velocity = rb.linearVelocity;
            velocity.z = currentForwardSpeed;
            rb.linearVelocity = velocity;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) //se soltar, volta a escala normal
        {
            transform.localScale = originalScale;

            // Corrige a velocidade ap�s mudar escala
            Vector3 velocity = rb.linearVelocity;
            velocity.z = currentForwardSpeed;
            rb.linearVelocity = velocity;

        }
    }

    private void OnCollisionEnter(Collision collision) //para detectar quando o jogador toc NO CH�O, para poder pular de novo
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }
    }

    
}