using UnityEngine;

public class BuckwheatGunController : BasicCollaboratorController
{
    [Range(1, 10)]
    public float ReloadTime;

    public Rigidbody Bullet;

    void Start()
    {
        Type = Card.CardType.BuckwheatGun;
    }

    public virtual void Shot()
    {
        var xScale = transform.localScale.x;
        var colliderWidth = GetComponent<BoxCollider>().size.x;
        var delta = colliderWidth * xScale;
        var startPosition = new Vector3(transform.position.x + delta + 1.8F, transform.position.y, transform.position.z - 0.5f);
        Instantiate(Bullet, startPosition, Quaternion.identity);
    }
}
