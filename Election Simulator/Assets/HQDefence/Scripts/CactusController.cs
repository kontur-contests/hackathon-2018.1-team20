using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusController : BasicCollaboratorController
{
    [Range(1, 10)]

    public float Damage;

    void Start()
    {
        Type = Card.CardType.Cactus;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Grandmother"))
        {
            var controller = collision.GetComponent<GrandmotherController>();
            controller.ReceiveDamage(99999);
            Destroy(gameObject);
        }
    }
}
