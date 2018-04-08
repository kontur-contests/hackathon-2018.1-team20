using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Card : MonoBehaviour
{
    public CardType Type;

    private Action _currentAction;
    private CardController _cardController;

    void Awake()
    {
        _cardController = FindObjectOfType<CardController>();
    }

    void Update()
    {
        if (_currentAction != null)
        {
            _currentAction.Invoke();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void OnMouseDown()
    {
        _cardController.SelectCard(this.gameObject);
    }

    public void MoveUpWhileNotCollide()
    {
        UpdateState(CardState.MoveLeft);
    }

    public void SpectateToMouse()
    {
        UpdateState(CardState.MoveToMouse);
    }

    public void Sleep()
    {
        UpdateState(CardState.Idle);
    }
    
    void MoveLeft()
    {
        transform.Translate(2 * Vector3.right * Time.deltaTime);
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
        var xScale = transform.localScale.x;
        var colliderWidth = GetComponent<BoxCollider2D>().size.x * xScale / 2 + 0.05F + GetComponent<BoxCollider2D>().offset.x;
        var startPosition = new Vector3(transform.position.x + colliderWidth, transform.position.y);
        var hit = Physics2D.RaycastAll(startPosition, Vector2.right, 0.01F);
        return hit.Length != 0;
    }

    void UpdateState(CardState cardState)
    {
        _currentAction = ConvertStateToAction(cardState);
    }

    Action ConvertStateToAction(CardState cardState)
    {
        switch (cardState)
        {
            case CardState.Idle:
                return () => { if (!IsCollide()) UpdateState(CardState.MoveLeft); };
            case CardState.MoveLeft:
                return MoveLeft;
            case CardState.MoveToMouse:
                return MoveToMouse;
            default: throw new ArgumentException("Can't convert this cardState to action");
        }
    }

    public enum CardState
    {
        Idle,
        MoveLeft,
        MoveToMouse
    }

    public enum CardType
    {
        BuckwheatGun,
        MegaBuckweatGun,
        Cactus,
        BadWithBuckweat,
        Portrait
    }
}
