using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GrandmotherController : MonoBehaviour
{
    [Range(0, 200)]
    public float Damage;

    public float DelayInSeconds;

    private Stack<Action> _fsm;
    private float _timeInSleep;

    void Start()
    {
        _fsm = new Stack<Action>();
        _fsm.Push(MovingLeft);
    }

    void Update()
    {
        if (_fsm.Count != 0)
            _fsm.Peek().Invoke();
    }

    void MovingLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.tag.Equals("Contributor"))
            return;
        var controller = collision.collider.GetComponent<BasicCollaboratorController>();
        if (controller == null)
            return;
        _fsm.Push(() => Attack(collision.gameObject, controller));
    }

    void OnTriggerEnter(Collider collision)
    {
        var controller = collision.gameObject.GetComponent<BasicCollaboratorController>();
        Debug.Log(controller.Type);
    }

    void Attack(GameObject gameObject, BasicCollaboratorController controller)
    {
        if (gameObject == null)
            _fsm.Pop();
        if (controller == null)
            _fsm.Pop();
        controller.ReceiveDamage(Damage);
        Debug.Log("Hit to " + gameObject.tag);
        _fsm.Push(() => Sleep(1));
    }

    void Sleep(float millseconds)
    {
        _timeInSleep += Time.deltaTime;
        if (_timeInSleep >= millseconds)
            _fsm.Pop();
    }
}
