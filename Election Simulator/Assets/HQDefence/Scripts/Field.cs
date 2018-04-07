using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Field : MonoBehaviour
{
    private CardController _cardController;
    private GameObject _currentContributor;

    void Start()
    {
        _cardController = FindObjectOfType<CardController>();
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
        _currentContributor = Instantiate(contributor, transform.position, Quaternion.identity);
        var controller = _currentContributor.GetComponent<BasicCollaboratorController>();
        controller.OnDie += () => { RemoveCurrentContributor(controller); };
    }

    void RemoveCurrentContributor(BasicCollaboratorController controler)
    {
        Debug.Log("remove");
        _currentContributor = null;
    }

    private bool IsEmpty()
    {
        return _currentContributor == null;
    }
}
