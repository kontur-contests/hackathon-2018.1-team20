using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(SpriteRenderer))]
public class GrandmotherController : MonoBehaviour
{
    [Range(0, 200)]
    public float Damage;

    public float Health;

    private bool _isMoving = true;
    private Vector3 _direction;
    private float _speed = 1f;

    void Start()
    {
        _direction = new Vector3(-1, 0, 0);
    }

    void Update()
    {
        if (_isMoving)
            MovingLeft();
    }

    void MovingLeft()
    {
        transform.Translate(_speed*_direction* Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Contributor"))
        {
            var controller = collision.collider.GetComponent<BasicCollaboratorController>();
            if (controller == null)
                return;
            _isMoving = false;
            StartCoroutine(Attack(controller));
        } else if (collision.collider.tag.Equals("Finish"))
        {
            var fader = FindObjectOfType<ScreenFader>();
            if (fader != null)
                fader.State = ScreenFader.FadeState.In;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Contributor"))
        {
            var controller = collision.GetComponent<BasicCollaboratorController>();
            if (controller == null)
                return;
            _isMoving = false;
            StartCoroutine(Attack(controller));
        }
        else if (collision.tag.Equals("Finish"))
        {
            Debug.Break();
            var fader = FindObjectOfType<ScreenFader>();
            if (fader != null)
                fader.State = ScreenFader.FadeState.In;
            StartCoroutine(LoadNextSceneWithDelay(3));
        }
    }

    IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<NextLevelLoader>().LoadNextLevel();
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
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SpriteRenderer>().flipX = true;
        _direction.x = -_direction.x;
        _speed = 5f;
        FindObjectOfType<GrandmotherGenerator>().RemoveGrandMother();
        Destroy(gameObject, 10);
    }
}
