using UnityEngine;

public class BasicCollaboratorController : MonoBehaviour
{
    public Card.CardType Type;

    [Range(0, 200)]
    public float Health;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ReceiveDamage(float value)
    {
        Health -= value;
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
