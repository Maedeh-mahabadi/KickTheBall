using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Sprite bgSoccer;
    public Sprite bgBasketball;
    public Sprite bgTennis;

    private SpriteRenderer sr;
    private GameManager gameManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        UpdateBackground();
    }

    void UpdateBackground()
    {
        int score = gameManager.GetScore();

        if (score >= 20 && sr.sprite != bgTennis)
        {
            sr.sprite = bgTennis;
        }
        else if (score >= 10 && score < 20 && sr.sprite != bgBasketball)
        {
            sr.sprite = bgBasketball;
        }
        else if (score < 10 && sr.sprite != bgSoccer)
        {
            sr.sprite = bgSoccer;
        }
    }
}

