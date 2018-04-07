using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckwheatGunController : BasicCollaboratorController
{

    void Start()
    {
        Type = Card.CardType.BuckwheatGun;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
