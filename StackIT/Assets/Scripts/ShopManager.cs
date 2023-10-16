using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject item2x; // Reference to the 2x item GameObject

    private static ShopManager instance;

    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShopManager>();
            }
            return instance;
        }
    }

    public bool is2xItemUnlocked { get; private set; }

    // Call this method to unlock the 2x item
    public void Unlock2xItem()
    {
        is2xItemUnlocked = true;

        // Activate the 2x item in your game, for example:
        if (item2x != null)
        {
            item2x.SetActive(true);
        }
    }

    // Other methods for your shop functionality can be added here.

    private void Awake()
    {
        // Ensure there's only one instance of ShopManager
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep the ShopManager between scenes if needed.
        }
    }

    // Other shop-related functions...
}