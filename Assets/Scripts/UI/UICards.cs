using UnityEngine;

namespace UI
{
    public class UICards : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private UICard cardSample;
        [SerializeField] private OptionalCards optionalCards;
        [SerializeField] private float interval;
        
        private UICard[] _cards;

        public UICard[] Cards => _cards;
        
        [ContextMenu("Build")]
        private void Build()
        {
            _cards = new UICard[config.optionalCardCount];
            var startX = -interval * (config.optionalCardCount - 1) / 2;
            for (var i = 0; i < config.optionalCardCount; i++)
            {
                var newCard = Instantiate(cardSample, transform);
                newCard.transform.position += new Vector3(startX + interval * i, 0, 0);
                _cards[i] = newCard;
            }
        }

        private void Awake()
        {
            Build();
            Refresh(optionalCards.Get());
            optionalCards.OnChange += Refresh;
        }
        
        private void Refresh(HeroType[] heroTypes)
        {
            for(var i = 0; i < _cards.Length; i++)
            {
                var heroType = i < heroTypes.Length ? heroTypes[i] : HeroType.Invalid;
                _cards[i].SetType(heroType);
            }
        }
    }
}