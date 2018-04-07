using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Field : MonoBehaviour
{
    private bool IsEmpty = true;

    private CardController _cardController;

    void Start()
    {
        _cardController = FindObjectOfType<CardController>();
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!IsEmpty)
            return;
        var selectedCard = _cardController.GetSelectedCard();
        if (selectedCard == null)
            return;
        var card = _cardController.DistributeSelectedCard();
        Instantiate(card, transform.position, Quaternion.identity);
        IsEmpty = false;
        Debug.Log(this.gameObject.name);
    }

    void AddHero(GameObject card)
    {
    }
}
