using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CardController : MonoBehaviour
{
    public Transform CardSpawn;
    public float ReloadTime;
    public int MaxCardsCount;

    private float _reloadingTime;
    private GameObject[] _cards;
    private GameObject _selectedCard;
    private Dictionary<Card.CardType, GameObject> cardTypeToGameObject;
    private int _currentCardsCount;

    void Start()
    {
        InitCardTypeToDictionary();
        _cards = GetAllCards().ToArray();
        CreateNewCard();
        _reloadingTime = ReloadTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnselectCurrentCard();
        }

        if (_reloadingTime < 0 && _currentCardsCount < MaxCardsCount)
        {
            _reloadingTime = ReloadTime;
            CreateNewCard();
        }

        _reloadingTime -= Time.deltaTime;
    }

    public GameObject GetSelectedCard()
    {
        return _selectedCard;
    }

    public void SelectCard(GameObject card)
    {
        if (card.GetComponent<Card>() == null)
        {
            Debug.LogError("You try add not card");
            return;
        }
        UnselectCurrentCard();
        _selectedCard = card;
        _selectedCard.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void UnselectCurrentCard()
    {
        if (_selectedCard == null)
            return;
        _selectedCard.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        _selectedCard = null;
    }

    public GameObject DistributeSelectedCard()
    {
        if (_selectedCard == null)
            return null;

        var cardType = _selectedCard.GetComponent<Card>().Type;
        Debug.Log("Type not null");
        var objectPrefab = cardTypeToGameObject[cardType];
        DeleteCard(_selectedCard);
        _currentCardsCount--;
        return objectPrefab;
    }

    public void DeleteCard(GameObject card)
    {
        _selectedCard = null;
        Destroy(card);
    }

    void CreateNewCard()
    {
        var nextCard = GenerateNextCard();
        var newCard = Instantiate(nextCard, CardSpawn.position, Quaternion.identity);
        newCard.GetComponent<Card>().MoveUpWhileNotCollide();
        _currentCardsCount++;
    }

    void InitCardTypeToDictionary()
    {
        cardTypeToGameObject = new Dictionary<Card.CardType, GameObject>();
        cardTypeToGameObject[Card.CardType.BuckwheatGun] =
            Resources.Load<GameObject>("HQDefence/BuckwheatGun");
        cardTypeToGameObject[Card.CardType.MegaBuckweatGun] =
            Resources.Load<GameObject>("HQDefence/MegaBuckwheatGun");
        cardTypeToGameObject[Card.CardType.Cactus] =
            Resources.Load<GameObject>("HQDefence/Cactus");
        cardTypeToGameObject[Card.CardType.Portrait] =
            Resources.Load<GameObject>("HQDefence/Portrait");
    }

    GameObject GenerateNextCard()
    {
        var randomIndex = _randomizer.Next(0, _cards.Length);
        return _cards[randomIndex];
    }

    GameObject[] GetAllCards()
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
