using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckwheatGunController : BasicCollaboratorController
{
    [Range(1, 10)]
    public float ReloadTime;

    private float _reloadTime;

    public Rigidbody Bullet;

    void Start()
    {
        Type = Card.CardType.BuckwheatGun;
    }

    void Update()
    {
        _reloadTime -= Time.deltaTime;

        if (_reloadTime <= 0)
        {
            Shot();
            _reloadTime = ReloadTime;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void Shot()
    {
        var xScale = transform.localScale.x;
        var colliderWidth = GetComponent<BoxCollider>().size.x;
        var delta = colliderWidth * xScale;
        var startPosition = new Vector3(transform.position.x + delta, transform.position.y, transform.position.y);
        var bullet = Instantiate(Bullet, startPosition, Quaternion.identity);
        Debug.Break();
    }
}
