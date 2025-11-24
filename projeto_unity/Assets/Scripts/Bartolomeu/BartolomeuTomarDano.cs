using UnityEngine;
using UnityEngine.InputSystem;

public class BartolomeuTomarDano : MonoBehaviour
{
    [Header("Configuração de Dano")]
    public float distanciaParaSerAtingido = 1.3f;
    public float danoRecebido = 25f;

    [Header("Jogador e Input")]
    public Transform player;
    public PlayerInput playerInput; 
    private InputAction atacarAction;

    private BartolomeuVida vida;

    void Start()
    {
        vida = GetComponent<BartolomeuVida>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerInput == null)
            playerInput = player.GetComponent<PlayerInput>();

        // Pega a ação "Atacar" do seu Player Actions
        atacarAction = playerInput.actions["Atacar"];
        atacarAction.performed += ctx => TentarDarDano();
    }

    void OnDestroy()
    {
        if (atacarAction != null)
            atacarAction.performed -= ctx => TentarDarDano();
    }

    void TentarDarDano()
    {
        if (vida == null || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // Só toma dano se estiver perto o suficiente
        if (dist <= distanciaParaSerAtingido)
        {
            vida.ReduzirVida(danoRecebido);

        }
    }
}
