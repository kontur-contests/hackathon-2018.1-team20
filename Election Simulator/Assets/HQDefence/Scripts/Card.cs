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
    private CardState _currentCardState;
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
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void OnMouseDown()
    {
        _cardController.SelectCard(this.gameObject);
    }

    public void MoveUpWhileNotCollide()
    {
        UpdateState(CardState.MoveUp);
    }

    public void SpectateToMouse()
    {
        UpdateState(CardState.MoveToMouse);
    }

    public void Sleep()
    {
        UpdateState(CardState.Idle);
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
        var colliderHeigth = GetComponent<BoxCollider2D>().size.y * yScale / 2 + 0.05F;
        var startPosition = new Vector3(transform.position.x, transform.position.y + colliderHeigth);
        var hit = Physics2D.RaycastAll(startPosition, Vector2.up, 0.01F);
        return hit.Length != 0;
    }

    void UpdateState(CardState cardState)
    {
        _currentAction = ConvertStateToAction(cardState);
        _currentCardState = cardState;
    }

    Action ConvertStateToAction(CardState cardState)
    {
        switch (cardState)
        {
            case CardState.Idle:
                return () => { if (!IsCollide()) UpdateState(CardState.MoveUp); };
            case CardState.MoveUp:
                return MoveUp;
            case CardState.MoveToMouse:
                return MoveToMouse;
            default: throw new ArgumentException("Can't convert this cardState to action");
        }
    }

    public enum CardState
    {
        Idle,
        MoveUp,
        MoveToMouse
    }

    public enum CardType
    {
        BuckwheatGun,
        MegaBuckweatGun,
        Lego,
        BadWithBuckweat,
        Portrait
    }
}
