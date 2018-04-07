using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Card : MonoBehaviour
{
    public Sprite ObjectSprite;

    private Sprite _originSprite;
    private Action _currentAction;
    private State _currentState;

    void Awake()
    {
        _originSprite = GetComponent<SpriteRenderer>().sprite;
        //if (ObjectSprite == null)
        //    Debug.LogError("SameObject is null");
    }

    void Update()
    {
        if (_currentAction != null)
        {
            _currentAction.Invoke();
        }
    }

    void OnMouseDown()
    {
        UpdateState(State.MoveToMouse);
    }

    public void MoveUpWhileNotCollide()
    {
        UpdateState(State.MoveUp);
    }

    public void SpectateToMouse()
    {
        UpdateState(State.MoveToMouse);
    }

    public void Sleep()
    {
        UpdateState(State.Idle);
    }

    void MoveUp()
    {
        transform.Translate(2 * Vector3.up * Time.deltaTime);
        if (IsCollide())
            Sleep();
    }

    void MoveToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 10;
        transform.position = mousePos;
    }

    bool IsCollide()
    {
        var yScale = transform.localScale.y;
        var colliderHeigth = GetComponent<BoxCollider2D>().size.y * yScale / 2 + 0.01F;
        var startPosition = new Vector3(transform.position.x, transform.position.y + colliderHeigth);
        var hit = Physics2D.RaycastAll(startPosition, Vector2.up, 0.01F);
        return hit.Length != 0;
    }

    void UpdateState(State state)
    {
        _currentAction = ConvertStateToAction(state);
        _currentState = state;
    }

    Action ConvertStateToAction(State state)
    {
        switch (state)
        {
            case State.Idle:
                return () => { };
            case State.MoveUp:
                return MoveUp;
            case State.MoveToMouse:
                return MoveToMouse;
            default: throw new ArgumentException("Can't convert this state to action");
        }
    }

    enum State
    {
        Idle,
        MoveUp,
        MoveToMouse
    }
}
