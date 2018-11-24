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


    public Food[] foods;

    // put the following variables in private after some tests

    private float nbDollarMax = 100, nbDollar = 100, nbFood = 100, nbFoodMax = 100, nbPlusMax = 100, nbPlus = 100;

    private int t, i = 1;

    private bool alive;

    // Use this for initialization
    void Start()
    {
        alive = true;
        t = Random.Range(0, 45);
        FoodImage.sprite = foods[t].sprite;
        desc.text = foods[t].desc;
        Debug.Log(foods[t].desc);
        checkDot(foods[t].Argent, foods[t].Calories, foods[t].Health);
    }

    //private bool IsAlive()
    //{
    //    if (nbFood <= 0 || nbFood >= 100 ||
    //        nbDollar <= 0 || nbDollar >= 100 ||
    //        nbPlus <= 0 || nbPlus >= 100)
    //    {
    //        desc.text = "You are dead...";
    //        return false;
    //    }
    //    else
    //        return true;
    //    return false;
    //}

    private void checkDot(int nbDollar, int nbFood, int nbPlus)
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

    private void SwipeLeft()
    { 
        nbFood -= 35;
        nbDollar += 10;
        nbPlus -= 20;

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

    private void SwipeRight()
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
        DollarBar.fillAmount = nbDollar / 100;
        FoodBar.fillAmount = nbFood / 100;
        PlusBar.fillAmount = nbPlus / 100;

        t = Random.Range(0, 45);
        FoodImage.sprite = foods[t].sprite;
        desc.text = foods[t].desc;
        checkDot(foods[t].Argent, foods[t].Calories, foods[t].Health);
        Debug.Log(foods[t].desc);

        i++;
        day.text = "Day " + i;
    }

    private void dead()
    {
        desc.text = "You are dead...";
    }

    // Update is called once per frame
    void Update()
    {
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
