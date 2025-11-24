using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public DanoNoBartolomeu danoBartolomeu;

    // Chamado pelo Input System (ex: Player/Attack)
    public void OnAttackInput()
    {
        if (danoBartolomeu != null)
        {
            danoBartolomeu.OnAttack();
        }
    }
}
