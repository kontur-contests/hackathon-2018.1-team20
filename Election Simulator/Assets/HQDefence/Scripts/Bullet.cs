using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    public float Damage;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = 3 * Vector2.right;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Grandmother"))
        {
            var controller = collision.collider.GetComponent<GrandmotherController>();
            controller.ReceiveDamage(Damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Grandmother"))
        {
            var controller = collision.GetComponent<GrandmotherController>();
            controller.ReceiveDamage(Damage);
            Destroy(gameObject);
        }

        if (collision.tag.Equals("EndLevel"))
            Destroy(gameObject);
    }
}
