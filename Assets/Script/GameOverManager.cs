using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public ItemDropController dropController;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            var item = other.GetComponent<ItemManager>();
            if (item.isHit == true)
            {
                GameOverUI.SetActive(true);
                this.enabled = false;
                dropController.enabled = false;
                Time.timeScale = 0;
            }
        }
    }
}
