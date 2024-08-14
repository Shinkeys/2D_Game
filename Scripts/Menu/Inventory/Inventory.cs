using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // You can initialize anything related to the inventory here
    }

    // Update is called once per frame
    void Update()
    {
        // You can add inventory-related update logic here
    }

    public void ToggleInventory(GameObject inventory)
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}