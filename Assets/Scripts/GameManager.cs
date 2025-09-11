using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        if (inventoryContainer != null && allItemsContainer != null)
        {
            // Procura o Axe no catálogo global
            ItemSlot axeSlot = allItemsContainer.slots
                .Find(slot => slot.item != null && slot.item.Name == "Axe");

            if (axeSlot != null && axeSlot.item != null)
            {
                // Coloca o Axe no slot 0 do inventário do jogador
                inventoryContainer.slots[0].Set(axeSlot.item, 1);
                Debug.Log("🪓 Axe adicionado ao inventário!");
            }
            else
            {
                Debug.LogWarning("⚠️ Axe não encontrado no AllItems!");
            }
        }
    }




    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemContainer allItemsContainer;
    public SeedContainer allSeedsContainer;
    public DragAndDropController dragAndDropController;

    public ToolbarController toolbarControllerGlobal;
}
