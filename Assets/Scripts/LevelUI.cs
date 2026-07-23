using System;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI bulletCountText;
    public GameObject thirdStar;
    public GameObject secondStar;

    private void OnEnable()
    {
        GameController.OnBulletsChanged += UpdateBullets;
        GameController.OnThreeStars += ThreeStars;
        GameController.OnTwoStars += TwoStars;
        GameController.OnOneStar += OneStar;
    }

    private void OnDisable()
    {
        GameController.OnBulletsChanged -= UpdateBullets;
        GameController.OnThreeStars -= ThreeStars;
        GameController.OnTwoStars -= TwoStars;
        GameController.OnOneStar -= OneStar;
    }

    void Start()
    {
        
    }

    public void ThreeStars()
    {
        secondStar.SetActive(true);
        thirdStar.SetActive(true);
    }
    public void TwoStars()
    {
        secondStar.SetActive(true);
        thirdStar.SetActive(false);
    }
    public void OneStar()
    {
        secondStar.SetActive(false);
        thirdStar.SetActive(false);
    }

    public void UpdateBullets(int b)
    {
        bulletCountText.text = b + "";
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
