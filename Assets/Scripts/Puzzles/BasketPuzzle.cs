using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPuzzle : MonoBehaviour
{
    public GameObject light;
    public GameObject basket;
    public GameObject secondPlate;
    
    public void showLight()
    {
        light.SetActive(true);
    }

    public void showBasket()
    {
        basket.SetActive(true);

    }
    
    public void showSecondPlate()
    {
        secondPlate.SetActive(true);

    }
    public void hideSecondPlate()
    {
        secondPlate.SetActive(false);
    }
    
    public void hideBasket()
    {
        basket.SetActive(false);
    }
    

    public void hideLight()
    {
        light.SetActive(false);
    }
}
