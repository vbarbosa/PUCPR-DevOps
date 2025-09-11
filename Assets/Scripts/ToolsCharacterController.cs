using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ToolsCharacterController : MonoBehaviour
{
    PlayerControl character;
    Rigidbody2D rgbd2d;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] CropsReadController cropsReadController;
    [SerializeField] float maxDistance = 2f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;
    [SerializeField] TileData toMowTiles;
    [SerializeField] TileData toSeedTiles;
    [SerializeField] TileData waterableTiles;
    InventoryController inventoryController;
    ToolbarController toolbarController;
    [SerializeField] GameObject toolbarPanel;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    private static int cornPickUpCount = 3;
    private static int parsleyPickUpCount = 1;
    private static int potatoPickUpCount = 1;
    private static int strawberryPickUpCount = 1;
    private static int tomatoPickUpCount = 1;

    private static int cornSeedsCount = 4;
    private static int parsleySeedsCount = 3;
    private static int potatoSeedsCount = 1;
    private static int strawberrySeedsCount = 6;
    private static int tomatoSeedsCount = 3;

    Vector3Int selectedTilePosition;
    Vector3Int selectedCropPosition;
    bool selectable;

    public static Dictionary<Vector2Int, TileData> fields;
    public static Dictionary<Vector2Int, CropData> crops;

    UI_ShopController shopPanel;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("✅ ToolsCharacterController ativo no Player!");

        character = GetComponent<PlayerControl>();
        rgbd2d = GetComponent<Rigidbody2D>();
        fields = new Dictionary<Vector2Int, TileData>();
        crops = new Dictionary<Vector2Int, CropData>();
        toolbarController = GetComponent<ToolbarController>();
        inventoryController = GetComponent<InventoryController>();

        var shopPanelAll = Resources.FindObjectsOfTypeAll<UI_ShopController>();
        shopPanel = shopPanelAll[0];
    }

    // Update is called once per frame
    void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0)) // lewy przycisk myszki
        {
            Debug.Log("➡️ Clique detectado! Ferramenta: " +
                 (toolbarController.GetItem != null ? toolbarController.GetItem.Name : "Nenhuma"));

            if (!inventoryController.isOpen) //you can use tools only if inventory is closed
            {
                if (UseToolWorld() == true)
                {
                    return;
                }
                UseTool();
            }

        }
    }

    private bool CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.name.Contains("Tree"))
            {
                return true;
            }
            if (hit.collider.gameObject.name.Contains("CampFire"))
            {
                return true;
            }
            if (hit.collider.gameObject.name.Contains("Chest"))
            {
                return true;
            }
        }
        return false;
    }
    private bool CastRayPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            if (hit.collider.gameObject.name.Contains("Player"))
            {
                return true;
            }
        }
        return false;
    }
    private void SelectTile()
    {
        // --- Campos (fields) ---
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);

        if (tileBase != null) // só processa se realmente houver tile
        {
            TileData tileData = tileMapReadController.GetTileData(tileBase);
            if (tileData != null)
            {
                Vector2Int pos = (Vector2Int)selectedTilePosition;

                if (!fields.ContainsKey(pos))
                    fields.Add(pos, tileData);
                else
                    fields[pos] = tileData;
            }
        }

        // --- Cultivos (crops) ---
        selectedCropPosition = cropsReadController.GetGridPosition(Input.mousePosition, true);
        TileBase cropBase = cropsReadController.GetTileBase(selectedTilePosition);

        if (cropBase != null) // só processa se houver tile válido de crop
        {
            CropData cropData = cropsReadController.GetCropData(cropBase);
            if (cropData != null)
            {
                Vector2Int pos = (Vector2Int)selectedTilePosition;

                if (!crops.ContainsKey(pos))
                    crops.Add(pos, cropData);
                else
                    crops[pos] = cropData;
            }
        }
    }


    void CanSelectCheck()
    {
        if (Time.timeScale == 0) //if game paused
            return;

        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    // interacting with physical objects in the world
    private bool UseToolWorld()
    {
        if (Time.timeScale == 0)
            return false;

        Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D collidor in colliders)
        {
            Debug.Log("🔍 Detectado collider: " + collidor.gameObject.name);

            ToolHit hitTree = collidor.GetComponent<ToolHit>();

            if (hitTree != null && toolbarController.GetItem != null &&
                toolbarController.GetItem.Name == "Axe" && CastRay() == true)
            {
                Debug.Log("🌳 Tentando cortar árvore com Axe!");
                hitTree.Hit();
                return true;
            }
        }

        return false;
    }


    private void RefreshToolbar()
    {
        toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
        toolbarPanel.SetActive(true);
    }

    private void UseTool()
    {
        if (Time.timeScale == 0) //if game paused - return
            return;

        if (selectable == true && toolbarController.GetItem != null)
        {
            Debug.Log("Tile clicado: " + selectedTilePosition +
                  " | Item: " + toolbarController.GetItem.Name);


            TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            TileData tileData = tileMapReadController.GetTileData(tileBase);

            // Se o tile não é válido para nenhuma ação → sai
            if (tileData != plowableTiles && tileData != toMowTiles &&
                tileData != toSeedTiles && tileData != waterableTiles)
            {
                return;
            }

            Vector2Int pos = (Vector2Int)selectedTilePosition;

            // Garante que a chave existe antes de acessar
            if (crops.ContainsKey(pos) && crops[pos].noPlant)
            {
                // SHOVEL
                if (fields.ContainsKey(pos) &&
                    fields[pos].ableToMow &&
                    toolbarController.GetItem.Name == "Shovel" &&
                    shopPanel.isOpen == false)
                {
                    cropsManager.Mow(selectedTilePosition);
                }

                // HOE
                else if (fields.ContainsKey(pos) &&
                         fields[pos].plowable &&
                         toolbarController.GetItem.Name == "Hoe")
                {
                    cropsManager.Plow(selectedTilePosition);
                }

                // SEEDS
                else if (fields.ContainsKey(pos) &&
                         fields[pos].ableToSeed &&
                         toolbarController.GetItem.isSeed == true)
                {
                    switch (toolbarController.GetItem.Name)
                    {
                        case "Seeds_Corn":
                            if (GameManager.instance.inventoryContainer.slots[toolbarController.selectedTool].count >= cornSeedsCount)
                            {
                                cropsManager.SeedCrop(selectedTilePosition, "corn");
                                GameManager.instance.inventoryContainer.RemoveItem(toolbarController.GetItem, cornSeedsCount);
                            }
                            break;

                        case "Seeds_Parsley":
                            if (GameManager.instance.inventoryContainer.slots[toolbarController.selectedTool].count >= parsleySeedsCount)
                            {
                                cropsManager.SeedCrop(selectedTilePosition, "parsley");
                                GameManager.instance.inventoryContainer.RemoveItem(toolbarController.GetItem, parsleySeedsCount);
                            }
                            break;

                        case "Seeds_Potato":
                            if (GameManager.instance.inventoryContainer.slots[toolbarController.selectedTool].count >= potatoSeedsCount)
                            {
                                cropsManager.SeedCrop(selectedTilePosition, "potato");
                                GameManager.instance.inventoryContainer.RemoveItem(toolbarController.GetItem, potatoSeedsCount);
                            }
                            break;

                        case "Seeds_Strawberry":
                            if (GameManager.instance.inventoryContainer.slots[toolbarController.selectedTool].count >= strawberrySeedsCount)
                            {
                                cropsManager.SeedCrop(selectedTilePosition, "strawberry");
                                GameManager.instance.inventoryContainer.RemoveItem(toolbarController.GetItem, strawberrySeedsCount);
                            }
                            break;

                        case "Seeds_Tomato":
                            if (GameManager.instance.inventoryContainer.slots[toolbarController.selectedTool].count >= tomatoSeedsCount)
                            {
                                cropsManager.SeedCrop(selectedTilePosition, "tomato");
                                GameManager.instance.inventoryContainer.RemoveItem(toolbarController.GetItem, tomatoSeedsCount);
                            }
                            break;
                    }

                    RefreshToolbar();
                }
            }

            // WATERING CAN
            else if (crops.ContainsKey(pos) &&
                     crops[pos].planted &&
                     fields.ContainsKey(pos) &&
                     fields[pos].waterable &&
                     toolbarController.GetItem.Name == "WateringCan")
            {
                cropsManager.Water(selectedTilePosition);
                FindObjectOfType<SoundManager>().Play("Water");
            }

            // BAG → coleta
            else if (toolbarController.GetItem.Name == "Bag")
            {
                if (crops.ContainsKey(pos) && crops[pos].collectibleCorn)
                {
                    cropsManager.Collect(selectedTilePosition, "corn");
                    foreach (ItemSlot itemSlot in GameManager.instance.allItemsContainer.slots)
                    {
                        if (itemSlot.item.Name == "Food_Corn")
                        {
                            GameManager.instance.inventoryContainer.Add(itemSlot.item, cornPickUpCount);
                            RefreshToolbar();
                            break;
                        }
                    }
                }
                else if (crops.ContainsKey(pos) && crops[pos].collectibleParsley)
                {
                    cropsManager.Collect(selectedTilePosition, "parsley");
                    foreach (ItemSlot itemSlot in GameManager.instance.allItemsContainer.slots)
                    {
                        if (itemSlot.item.Name == "Food_Parsley")
                        {
                            GameManager.instance.inventoryContainer.Add(itemSlot.item, parsleyPickUpCount);
                            RefreshToolbar();
                            break;
                        }
                    }
                }
                else if (crops.ContainsKey(pos) && crops[pos].collectiblePotato)
                {
                    cropsManager.Collect(selectedTilePosition, "potato");
                    foreach (ItemSlot itemSlot in GameManager.instance.allItemsContainer.slots)
                    {
                        if (itemSlot.item.Name == "Food_Potato")
                        {
                            GameManager.instance.inventoryContainer.Add(itemSlot.item, potatoPickUpCount);
                            RefreshToolbar();
                            break;
                        }
                    }
                }
                else if (crops.ContainsKey(pos) && crops[pos].collectibleStrawberry)
                {
                    cropsManager.Collect(selectedTilePosition, "strawberry");
                    foreach (ItemSlot itemSlot in GameManager.instance.allItemsContainer.slots)
                    {
                        if (itemSlot.item.Name == "Food_Strawberry")
                        {
                            GameManager.instance.inventoryContainer.Add(itemSlot.item, strawberryPickUpCount);
                            RefreshToolbar();
                            break;
                        }
                    }
                }
                else if (crops.ContainsKey(pos) && crops[pos].collectibleTomato)
                {
                    cropsManager.Collect(selectedTilePosition, "tomato");
                    foreach (ItemSlot itemSlot in GameManager.instance.allItemsContainer.slots)
                    {
                        if (itemSlot.item.Name == "Food_Tomato")
                        {
                            GameManager.instance.inventoryContainer.Add(itemSlot.item, tomatoPickUpCount);
                            RefreshToolbar();
                            break;
                        }
                    }
                }
            }
        }
    }

}
