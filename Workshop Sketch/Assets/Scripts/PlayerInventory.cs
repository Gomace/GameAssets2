using System;
using System.Collections;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private RectTransform hotbar;
    [SerializeField] private Sprite empty;
    public Transform[] inventory = new Transform[10];

    public int heldItem = 0;
    public bool Busy = false;
    
    private float oldSpeed;
    private float oldSens;
    private Transform thing;

    private Animation anim;
    
    void Update()
    {
        if (Busy)
            return;
        
        if (Input.GetKeyDown("1"))
            ChangeSlot(0);
        else if (Input.GetKeyDown("2"))
            ChangeSlot(1);
        else if (Input.GetKeyDown("3"))
            ChangeSlot(2);
        else if (Input.GetKeyDown("4"))
            ChangeSlot(3);
        else if (Input.GetKeyDown("5"))
            ChangeSlot(4);
        else if (Input.GetKeyDown("6"))
            ChangeSlot(5);
        else if (Input.GetKeyDown("7"))
            ChangeSlot(6);
        else if (Input.GetKeyDown("8"))
            ChangeSlot(7);
        else if (Input.GetKeyDown("9"))
            ChangeSlot(8);
        else if (Input.GetKeyDown("0"))
            ChangeSlot(9);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldItem > 0)
                ChangeSlot(heldItem - 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem < 9)
                ChangeSlot(heldItem + 1);
        }
    }
    
    public void PickUp(Transform obj)
    {
        if (obj.parent)
        {
            if (obj.parent.parent && obj.parent.GetChild(1))
            {
                if (obj.parent.parent.transform.name == "Machine")
                {
                    obj.gameObject.SetActive(false);
                    obj.parent.GetComponent<BoxCollider>().enabled = true;
                    
                    for (int i = 0; i < obj.parent.parent.childCount; i++)
                    {
                        if (obj.parent == obj.parent.parent.GetChild(i))
                            obj.parent.parent.GetComponent<Special>().WhichItem(i, false);
                    }
                    thing = obj.parent.GetChild(1);
                }
                else
                    thing = obj;
            }
            else
                thing = obj;
        }
        else
            thing = obj;
        
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = thing;
                hotbar.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + thing.name);

                thing.gameObject.layer = 0;
                thing.position = Vector3.zero;

                thing.parent = cam;
                thing.localPosition = cam.localPosition + new Vector3(0.9f, -1.55f, 1f);
                thing.localRotation = Quaternion.Euler(20, 0, 0);

                thing.gameObject.SetActive(heldItem == i);

                return;
            }
        }
    }

    public void PutDown(Vector3 hit, Transform obj)
    {
        if (inventory[heldItem] != null)
        {
            inventory[heldItem].parent = null;
            hotbar.GetChild(heldItem).GetComponent<Image>().sprite = empty;

            inventory[heldItem].position = hit;
            inventory[heldItem].rotation = obj.rotation;
            
            inventory[heldItem].gameObject.layer = 7;
            inventory[heldItem] = null;
        }
    }

    private void ChangeSlot(int slot)
    {
        if (inventory[heldItem] != null)
            inventory[heldItem].gameObject.SetActive(false);
        
        heldItem = slot;
        
        Vector2 anchoredPos = hotbar.GetChild(heldItem).GetComponent<RectTransform>().anchoredPosition;
        anchoredPos.y = -470;
        hotbar.parent.GetChild(1).GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        
        if (inventory[heldItem] != null)
            inventory[heldItem].gameObject.SetActive(true);
    }

    public void UseItem(Transform hole)
    {
        if (inventory[heldItem].name != hole.name || !hole.GetChild(0))
            return;

        hotbar.GetChild(heldItem).GetComponent<Image>().sprite = empty;
        inventory[heldItem].parent = null;

        inventory[heldItem].position = hole.position;
        inventory[heldItem].rotation = hole.rotation;
        inventory[heldItem].parent = hole;

        inventory[heldItem].gameObject.SetActive(false);
        hole.GetChild(0).gameObject.SetActive(true);
        hole.GetComponent<BoxCollider>().enabled = false;
        inventory[heldItem] = null;
        
        for (int i = 0; i < hole.parent.childCount; i++)
        {
            if (hole == hole.parent.GetChild(i))
                hole.parent.GetComponent<Special>().WhichItem(i, true);
        }
    }

    public IEnumerator UseTool(Transform screw)
    {
        bool match = (screw.name is "Bolt" && inventory[heldItem].name is "Wrench") || (screw.name is "Screw" && inventory[heldItem].name is "Screwdriver");
        if (!match)
            yield break;
        oldSpeed = transform.GetComponent<PlayerMovement>().speed;
        oldSens = transform.GetChild(0).GetComponent<MouseLook>().mouseSensitivity;
        
        inventory[heldItem].gameObject.SetActive(false);
        transform.GetComponent<PlayerMovement>().speed = 0f;
        transform.GetChild(0).GetComponent<MouseLook>().mouseSensitivity = 0f;
        Busy = true;
        
        for (int i = 0; i < screw.parent.parent.childCount; i++)
        {
            if (screw.parent == screw.parent.parent.GetChild(i))
                screw.parent.parent.GetComponent<Special>().WhichItem(i+20, true);
        }
        
        if (inventory[heldItem].name is "Screwdriver")
        {
            float time = 0;
            while (time < 2)
            {
                yield return null;
                time += Time.deltaTime * 1;
            }
        }
        else
        {
            float time = 0;
            while (time < 5)
            {
                yield return null;
                time += Time.deltaTime * 1;
            }
        }
        
        for (int i = 0; i < screw.parent.parent.childCount; i++)
        {
            if (screw.parent == screw.parent.parent.GetChild(i))
                screw.parent.parent.GetComponent<Special>().WhichItem(i+20, false);
        }
        inventory[heldItem].gameObject.SetActive(true);
        transform.GetComponent<PlayerMovement>().speed = oldSpeed;
        transform.GetChild(0).GetComponent<MouseLook>().mouseSensitivity = oldSens;
        Busy = false;
    }

    public void ReadNote(String NoteType, Boolean Reading)
    {
        for (int i = 0; i < hotbar.parent.GetChild(2).childCount; i++)
        {
            if (hotbar.parent.GetChild(2).GetChild(i).name == NoteType)
                hotbar.parent.GetChild(2).GetChild(i).gameObject.SetActive(Reading);
        }
    }
}
