using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShow : MonoBehaviour
{
    public static bool CardsAreShowing = false;

    public GameObject cardShowingUI;

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused == false && MapShow.mapIsShowing == false && Player.inventoryIsShowing == false)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (CardsAreShowing)
                {
                    HideCards();
                }
                else
                {
                    ShowCards();
                }
            }
        }
    }

    void HideCards()
    {
        InventoryManager.Instance.hotBarGO.SetActive(true);
        cardShowingUI.SetActive(false);
        Time.timeScale = 1f;
        CardsAreShowing = false;
    }

    void ShowCards()
    {
        InventoryManager.Instance.hotBarGO.SetActive(false);
        cardShowingUI.SetActive(true);
        Time.timeScale = 0f;
        CardsAreShowing = true;
    }

}
