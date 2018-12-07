using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    [System.Serializable]
    public class Food
    {
        public Sprite sprite;
        public int Calories;
        public int Argent;
        public int Health;
        public string desc;
    }

    public Image FoodImage;
    public Text desc, day;
    public Image DollarBar, PlusBar, FoodBar;
    public Image DollarDotSmall, DollarDotLarge, PlusDotSmall, PlusDotLarge, FoodDotSmall, FoodDotLarge;
    public Image RightArrow, LeftArrow;

    public Food[] foods;

    private float nbDollarMax = 100, nbDollar = 50, nbFood = 50, nbFoodMax = 100, nbPlusMax = 100, nbPlus = 80;

    private int t, i = 1;

    private bool alive;

    private float lerpSpeed = 2;

    // Use this for initialization
    void Start()
    {
        alive = true;
        t = Random.Range(0, 22);
        FoodImage.sprite = foods[t].sprite;
        desc.text = foods[t].desc;
        Debug.Log(foods[t].desc);
        CheckDot(foods[t].Argent, foods[t].Calories, foods[t].Health);
    }

    private void CheckDot(int nbDollar, int nbFood, int nbPlus)
    {
        if (nbDollar >= 10 || nbDollar <= -10)
        {
            DollarDotLarge.gameObject.SetActive(true);
            DollarDotSmall.gameObject.SetActive(false);
        }
        else if (nbDollar <= 10 && nbDollar > 0 || nbDollar >= -10 && nbDollar < 0)
        {
            DollarDotLarge.gameObject.SetActive(false);
            DollarDotSmall.gameObject.SetActive(true);
        }
        else
        {
            DollarDotLarge.gameObject.SetActive(false);
            DollarDotSmall.gameObject.SetActive(false);
        }
        if (nbFood >= 10 || nbFood <= -10)
        {
            FoodDotLarge.gameObject.SetActive(true);
            FoodDotSmall.gameObject.SetActive(false);
        }
        else if (nbFood <= 10 && nbFood > 0 || nbFood >= -10 && nbFood < 0)
        {
            FoodDotLarge.gameObject.SetActive(false);
            FoodDotSmall.gameObject.SetActive(true);
        }
        else
        {
            FoodDotLarge.gameObject.SetActive(false);
            FoodDotSmall.gameObject.SetActive(false);
        }
        if (nbPlus >= 10 || nbPlus <= -10)
        {
            PlusDotLarge.gameObject.SetActive(true);
            PlusDotSmall.gameObject.SetActive(false);
        }
        else if (nbPlus <= 10 && nbPlus > 0 || nbPlus >= -10 && nbPlus < 0)
        {
            PlusDotLarge.gameObject.SetActive(false);
            PlusDotSmall.gameObject.SetActive(true);
        }
        else
        {
            PlusDotLarge.gameObject.SetActive(false);
            PlusDotSmall.gameObject.SetActive(false);
        }
    }

    public void SwipeLeft()
    { 
        nbFood -= 15;
        nbPlus -= 20;

        if (nbFood >= 100)
            nbFood = nbFoodMax;
        else if (nbDollar >= 100)
            nbDollar = nbDollarMax;
        else if (nbPlus >= 100)
            nbPlus = nbPlusMax;

        if (nbFood <= 0 || nbPlus <= 0 || nbDollar <= 0)
            alive = false;

        if (foods[t].desc == "Indigestion")
        {
            nbFood -= foods[t].Calories;
            nbPlus -= foods[t].Health;
        }


        NewImage(nbFood, nbDollar, nbPlus);
    }

    public void SwipeRight()
    {
        nbFood += foods[t].Calories;
        nbDollar -= foods[t].Argent;
        nbPlus += foods[t].Health;

        if (nbFood >= 100)
            nbFood = nbFoodMax;
        else if (nbDollar >= 100)
            nbDollar = nbDollarMax;
        else if (nbPlus >= 100)
            nbPlus = nbPlusMax;

        if (nbFood <= 0 || nbPlus <= 0 || nbDollar <= 0)
            alive = false;

        NewImage(nbFood, nbDollar, nbPlus);     
    }

    private void NewImage(float nbFood, float nbDollar, float nbPlus)
    {
        t = Random.Range(0, 22);
        FoodImage.sprite = foods[t].sprite;
        desc.text = foods[t].desc;
        CheckDot(foods[t].Argent, foods[t].Calories, foods[t].Health);
        Debug.Log(foods[t].desc);

        i++;
        day.text = "Day " + i;
    }

    private void dead()
    {
        desc.text = "You are dead...";
        RightArrow.gameObject.SetActive(false);
        LeftArrow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DollarBar.fillAmount = Mathf.Lerp(DollarBar.fillAmount, nbDollar / 100, Time.deltaTime * lerpSpeed);
        FoodBar.fillAmount = Mathf.Lerp(FoodBar.fillAmount, nbFood / 100, Time.deltaTime * lerpSpeed);
        PlusBar.fillAmount = Mathf.Lerp(PlusBar.fillAmount, nbPlus / 100, Time.deltaTime * lerpSpeed);
        if (alive)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwipeLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwipeRight();
            }
        }
        else
            dead();
    }
}
