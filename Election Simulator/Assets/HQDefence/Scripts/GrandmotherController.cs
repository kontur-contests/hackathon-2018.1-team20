using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GrandmotherController : MonoBehaviour
{
    [Range(0, 200)]
    public float Damage;

    public float Health;

    private bool _isMoving = true;


    void Update()
    {
        if (_isMoving)
            MovingLeft();
    }

    void MovingLeft()
    {
        transform.Translate(2f * Vector3.left * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag + " Collision");
        if (!collision.collider.tag.Equals("Contributor"))
            return;
        var controller = collision.collider.GetComponent<BasicCollaboratorController>();
        if (controller == null)
            return;
        _isMoving = false;
        StartCoroutine(Attack(controller));
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag + " Trigger");
        if (!collision.tag.Equals("Contributor"))
            return;
        var controller = collision.GetComponent<BasicCollaboratorController>();
        if (controller == null)
            return;
        _isMoving = false;
        StartCoroutine(Attack(controller));
    }

    IEnumerator Attack(BasicCollaboratorController controller)
    {
        yield return new WaitForSeconds(controller.SecondsToDestroy);
        _isMoving = true;
        controller.Die();
    }

    public void ReceiveDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
