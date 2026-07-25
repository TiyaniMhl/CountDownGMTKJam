using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameObject thirdStar;
    public GameObject secondStar;
    public GameObject nextLevelButton;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void HideNextLevelButton()
    {
        nextLevelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
