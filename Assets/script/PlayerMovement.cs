using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour


{
    public Rigidbody rb;
    private MeshRenderer meshRenderer;

    private float horizontal;
    private float vertical;

    public float forwardForce = 2000f;

    [SerializeField] private int velocidade;
    void Start()
    {
        //sempre que o jogador apertar a tecla C o usuario vai trocar de cor
        meshRenderer = GetComponent<MeshRenderer>(); //o get component procura o componente desse objeto c esse nome e retorna o pedido

        rb.AddForce(0, 200, 500); //se o rb add force estiver aqui no start, vai empurrar o bloco pra frente assim que clicar no play
    }

    // Update is called once per frame
    void FixedUpdate()

 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            meshRenderer.material.color = Random.ColorHSV();
        }

        //fazer o cubo andar constantemente, aplicar força constante
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);


        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, vertical, 0) * Time.deltaTime * velocidade); //andar com velocidade, velocidade adicionada no inspector com serializefield

        //limites de movimentação

        if ((Input.GetAxisRaw("Vertical") > 0) && (transform.position.y < 4.95f))
        {
            transform.Translate(0, 0.1f, 0);
        }

        else if ((Input.GetAxisRaw("Vertical") < 0) && (transform.position.y > 0.97f))
        {
            transform.Translate(0, -0.1f, 0);
        }

        if ((Input.GetAxisRaw("Horizontal") > 0) && (transform.position.x < 2.6f))
        {
            transform.Translate(0.1f, 0, 0);
        }
        else if ((Input.GetAxisRaw("Horizontal") < 0) && (transform.position.x > -2.98f))
        {
            transform.Translate(-0.1f, 0, 0);
        }


    }
   

}



