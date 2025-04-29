using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UseText;
    [SerializeField] private Transform Camera;
    [SerializeField] private float MaxUseDistance = 3f;
    [SerializeField] private LayerMask UseLayers;
    [SerializeField] private PlayerInventory Inventory;
    
    private bool Reading = false;
    private string NoteType;

    private void Update()
    {
        if (Inventory.Busy)
            return;
        
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers) && !Reading)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hit.collider.CompareTag("Untagged")) // Put Down
                Inventory.PutDown(hit.point, hit.transform);
            else if (hit.collider.TryGetComponent<Door>(out Door door)) // Door
            {
                UseText.SetText(door.IsOpen ? "Press \"F\" to Close" : "Press \"F\" to Open");
                UseText.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.F))
                    door.OpenClose();
            }
            else if (hit.collider.TryGetComponent<Drawer>(out Drawer drawer))   // Drawer
            {
                UseText.SetText(drawer.IsOpen ? "Press \"F\" to Close" : "Press \"F\" to Open");
                UseText.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.F))
                    drawer.OpenClose();
            }
            else if (hit.collider.CompareTag("Item"))   // Pick up Items or use Tools
            {
                if (Inventory.inventory[Inventory.heldItem] && hit.transform.parent)
                    UseText.SetText(Inventory.inventory[Inventory.heldItem].name is "Screwdriver" or "Wrench" && hit.transform.parent.name == hit.transform.name && hit.transform.name is "Screw" or "Bolt" ? "Press \"F\" to Use Tool" : "Press \"F\" to Pick Up");
                else 
                    UseText.SetText("Press \"F\" to Pick Up");
                UseText.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.F) && Inventory.inventory[Inventory.heldItem] && hit.transform.parent)
                {
                    if (Inventory.inventory[Inventory.heldItem].name is "Screwdriver" or "Wrench" && hit.transform.parent.name == hit.transform.name && hit.transform.name is "Screw" or "Bolt")
                        StartCoroutine(Inventory.UseTool(hit.transform));
                    else
                        Inventory.PickUp(hit.transform);
                }
                else if (Input.GetKeyDown(KeyCode.F))
                    Inventory.PickUp(hit.transform);
            }
            else if (hit.collider.CompareTag("Fix") && hit.transform.childCount < 2)    // Put parts on machine
            {  
                UseText.SetText("Press \"F\" to Fix this");
                UseText.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.F) && Inventory.inventory[Inventory.heldItem])
                {
                    if(Inventory.inventory[Inventory.heldItem].name is not "Wrench" or "Screwdriver")
                        Inventory.UseItem(hit.transform);
                }
            }
            else if (hit.collider.TryGetComponent<MachineCheck>(out MachineCheck machine))  // Check machine result
            {
                UseText.SetText("Press \"F\" to put Core here");
                UseText.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.F) && Inventory.inventory[Inventory.heldItem])
                {
                    if (Inventory.inventory[Inventory.heldItem].name == hit.transform.name)
                    { 
                        Inventory.UseItem(hit.transform);
                        machine.CheckResult();
                    }
                }
            }
            else if(hit.collider.CompareTag("Note")) // Read Note
            {
                UseText.SetText("Press \"F\" to read");
                UseText.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Reading = true;
                    NoteType = hit.transform.name;
                    Inventory.ReadNote(NoteType, Reading);
                    UseText.SetText("Press \"F\" to stop reading");
                }
            }
            else
                UseText.gameObject.SetActive(false);
        }
        else if (Reading)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Reading = false;
                Inventory.ReadNote(NoteType, Reading);
            }
        }
        else
            UseText.gameObject.SetActive(false);
    }
}