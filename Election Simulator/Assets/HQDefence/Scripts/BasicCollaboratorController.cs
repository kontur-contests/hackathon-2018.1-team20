using UnityEngine;

public class BasicCollaboratorController : MonoBehaviour
{
    public Card.CardType Type;
    
    [Range(0, 10)]
    public float SecondsToDestroy;

    public delegate void DieDelegate();

    public event DieDelegate OnDie;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Die()
    {
        if (OnDie != null)
            OnDie();
        Destroy(gameObject);
    }
}
