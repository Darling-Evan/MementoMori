using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShow : MonoBehaviour
{
    public static bool mapIsShowing = false;

    public GameObject mapShowingUI;

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused == false && CardShow.CardsAreShowing == false && Player.inventoryIsShowing == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (mapIsShowing)
                {
                    RevealMap();
                }
                else
                {
                    HideMap();
                }
            }
        }
    }

    void RevealMap()
    {
        mapShowingUI.SetActive(false);
        mapIsShowing = false;
    }

    
    void HideMap()
    {
        mapShowingUI.SetActive(true);
        mapIsShowing = true;
    }

}
