using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image lockIcon2xItem;

    private bool is2xItemUnlocked = false;

    public void Unlock2xItem()
    {
        is2xItemUnlocked = true;
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        if (is2xItemUnlocked)
        {
            lockIcon2xItem.enabled = false; // Disable the lock icon
        }
    }
}
