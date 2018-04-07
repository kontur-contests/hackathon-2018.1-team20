using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    public float Damage;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = 3 * Vector3.right;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Grandmother"))
        {
            Debug.Log("Hit");
            var controller = collision.collider.GetComponent<GrandmotherController>();
            controller.ReceiveDamage(Damage);
            Destroy(gameObject);
        }
    }
}
