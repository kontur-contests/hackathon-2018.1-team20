using System.Collections;
using System.Collections.Generic;
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

    public void Shot()
    {
        var xScale = transform.localScale.x;
        var colliderWidth = GetComponent<BoxCollider>().size.x;
        var delta = colliderWidth * xScale;
        var startPosition = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        var bullet = Instantiate(Bullet, startPosition, Quaternion.identity);
        //bullet.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
