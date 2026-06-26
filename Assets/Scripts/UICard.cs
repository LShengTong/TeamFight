using System;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    [SerializeField] private Config.Config config;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    public Action OnClickButton;
    
    private HeroType _heroType;

    private void Start()
    {
        button.onClick.AddListener(HandleButtonClick);
    }

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
    }

    private void HandleButtonClick()
    {
        OnClickButton?.Invoke();
    }
}
