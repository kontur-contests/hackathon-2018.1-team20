using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Field : MonoBehaviour
{
    private CardController _cardController;
    private GameObject _currentContributor;

    void Start()
    {
        _cardController = FindObjectOfType<CardController>();
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!IsEmpty())
            return;
        var selectedCard = _cardController.GetSelectedCard();
        if (selectedCard == null)
            return;
        AddContributor(selectedCard);
    }

    void AddContributor(GameObject card)
    {
        var contributor = _cardController.DistributeSelectedCard();
        var delta = card.GetComponent<SpriteRenderer>().size.y * card.transform.localScale.y / 2;
        var z = 7.66;
        z -= Int32.Parse(name[0].ToString());
        var spawnPosition = new Vector3(transform.position.x, transform.position.y + delta, (float)z);
        _currentContributor = Instantiate(contributor, spawnPosition, Quaternion.identity);
        contributor.transform.position = new Vector3(transform.position.x, transform.position.y, name[0]);
        var controller = _currentContributor.GetComponent<BasicCollaboratorController>();
        controller.OnDie += () => { RemoveCurrentContributor(controller); };
    }

    void RemoveCurrentContributor(BasicCollaboratorController controler)
    {
        _currentContributor = null;
    }

    private bool IsEmpty()
    {
        return _currentContributor == null;
    }

    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
    }

}
