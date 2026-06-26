using System;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Config.Config config;
    [SerializeField] private UICard cardSample;
    [SerializeField] private OptionalCards optionalCards;
    [SerializeField] private GridLayoutGroup grid;

    private UICard[] _cards;

    public Action<int> OnCardClick;
    
    private void Start()
    {
        grid.constraintCount = config.optionalCardCount;
        _cards = new UICard[config.optionalCardCount];
        for (var i = 0; i < config.optionalCardCount; i++)
        {
            var lambdaIndex = i;
            var newCard = Instantiate(cardSample, transform);
            newCard.OnClickButton += () =>
            {
                OnCardClick?.Invoke(lambdaIndex);
            };
            _cards[i] = newCard;
        }
        RefreshCards(optionalCards.Get());
        optionalCards.OnChange += RefreshCards;
    }

    private void RefreshCards(HeroType[] heroTypes)
    {
        for(var i = 0; i < _cards.Length; i++)
        {
            _cards[i].SetType(heroTypes[i]);
        }
    }
}
