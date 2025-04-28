using UnityEngine;
using TMPro;

public class PlayerPoints : MonoBehaviour
{
    public int moedasColetadas = 0;
    public float distanciaPercorrida = 0f;
    public int obstaculosDesviados = 0;

    public Transform player; 
    private Vector3 ultimaPosicao; // para retomar a ultima posição do player

    //public TextMeshProUGUI textoMoedas; 
    public TextMeshProUGUI textoKm;     
    public TextMeshProUGUI textoObstaculos; 

    void Start()
    {
        ultimaPosicao = player.position; // posição inicial do jogador
    }

   
    void Update()
    {
        AtualizarDistancia();  
        AtualizarTexto();      // atualiza os textos na tela
    }
  void AtualizarDistancia()
    {
        float distanciaFrame = player.position.z - ultimaPosicao.z;
        if (distanciaFrame > 0)
        {
            distanciaPercorrida += distanciaFrame;
        }
        ultimaPosicao = player.position;
    }

    void AtualizarTexto()
    {
        //textoMoedas.text = "Moedas: " + moedasColetadas;
        textoKm.text = "Km: " + (distanciaPercorrida / 1000f).ToString("F2");
        textoObstaculos.text = "Obstáculos: " + obstaculosDesviados;
    }

    public void ColetarMoeda()
    {
        moedasColetadas++;  // aumenta as moedas que o jogador coletou
    }

    public void DesviarObstaculo()
    {
        obstaculosDesviados++;  // aumenta os obstáculos desviados
    }
}