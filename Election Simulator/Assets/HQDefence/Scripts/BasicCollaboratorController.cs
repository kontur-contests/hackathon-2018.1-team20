using UnityEngine;

public class BasicCollaboratorController : MonoBehaviour
{
    public Card.CardType Type;
    
    [Range(0, 10)]
    public float SecondsToDestroy;

    public delegate void DieDelegate();

    public event DieDelegate OnDie;

    public void Die()
    {
        if (OnDie != null)
            OnDie();
        Destroy(gameObject);
    }

    //void Start()
    //{
    //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    //}
}
