using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UICard : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
    
        private HeroType _heroType;
        public Button Button => button;

        public void SetType(HeroType heroType)
        {
            if (heroType == _heroType) return;
            _heroType = heroType;
            if(heroType == HeroType.Invalid)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            var info = config.heroConfig.GetInfo(heroType);
            image.sprite = info.sprite;
            nameText.text = info.name;
            priceText.text = "price: " + info.price;
        }
    }
}
