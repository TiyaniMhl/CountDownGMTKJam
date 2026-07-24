using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI bulletCountText;
    public TextMeshProUGUI countFlash;
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
        countFlash.alpha = 0f;
        bulletCountText.text = GameController.Instance.bullets + "";
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
        countFlash.text = b + "";
        StartCoroutine(CountFlash());
    }

    public IEnumerator CountFlash()
    {
        countFlash.alpha = 1f;
        countFlash.fontSize = bulletCountText.fontSize + 10f;

        const float duration = 2f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            countFlash.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        countFlash.alpha = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
