using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CardController : MonoBehaviour
{
    public Transform CardSpawn;
    public float ReloadTime;

    private float _reloadingTime;

    private GameObject[] _cards;

    void Start()
    {
        _cards = GetAllCards().ToArray();
        CreateNewCard();
        _reloadingTime = ReloadTime;
    }

    void Update()
    {
        if (_reloadingTime < 0)
        {
            _reloadingTime = ReloadTime;
            CreateNewCard();
        }

        _reloadingTime -= Time.deltaTime;
    }


    void CreateNewCard()
    {
        var nextCard = GenerateNextCard();
        var newCard = Instantiate(nextCard, CardSpawn.position, Quaternion.identity);
        newCard.GetComponent<Card>().MoveUpWhileNotCollide();
    }

    GameObject GenerateNextCard()
    {
        var randomIndex = _randomizer.Next(0, _cards.Length);
        return _cards[randomIndex];
    }

    IEnumerable<GameObject> GetAllCards()
    {
        var result = new GameObject[transform.childCount];
        for (var i = 0; i < transform.childCount; i++)
        {
            result[i] = transform.GetChild(i).gameObject;
            if (result[i].GetComponent<Card>() == null)
                Debug.LogError("You try add not a card");
        }

        return result;
    }

    private readonly Random _randomizer = new Random();
}
