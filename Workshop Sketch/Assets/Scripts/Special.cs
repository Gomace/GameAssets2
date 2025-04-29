using UnityEngine;

public class Special : MonoBehaviour
{
    [SerializeField] private Transform machine;
    [SerializeField] private Transform extraMachine;

    private int cogCheck = 0;
    private int screwLarge = 0;
    private int screwSmall = 3;
    private int cablesFirst = 0;

    private Animation anim;
    
    public void WhichItem(int i, bool itemOn)
    {
        
        switch (i)
        {
            case 0:
                Item1(i, itemOn);
                break;
            case 1:
                Item2(i, itemOn);
                break;
            case 2:
                Item3(i, itemOn);
                break;
            case 3:
                Item4to5(i, itemOn);
                break;
            case 4:
                Item4to5(i, itemOn);
                break;
            case 5:
                Item6(i, itemOn);
                break;
            case 6:
                Item7(i, itemOn);
                break;
            case 7:
                Item8(i, itemOn);
                break;
            case 8:
                Item9to11(i, itemOn);
                break;
            case 9:
                Item9to11(i, itemOn);
                break;
            case 10:
                Item9to11(i, itemOn);
                break;
            case 11:
                Item12(i, itemOn);
                break;
            case 12:
                Item13(i, itemOn);
                break;
            case 13:
                Item14to16(i, itemOn);
                break;
            case 14:
                Item14to16(i, itemOn);
                break;
            case 15:
                Item14to16(i, itemOn);
                break;
            case 16:
                Item17to20(i, itemOn);
                break;
            case 17:
                Item17to20(i, itemOn);
                break;
            case 18:
                Item17to20(i, itemOn);
                break;
            case 19:
                Item17to20(i, itemOn);
                break;
            case 20:
                Item21(i, itemOn);
                break;
            case 21:
                Item22(i, itemOn);
                break;
            case 22:
                Item23(i, itemOn);
                break;
            case 33:
                Item34to36(i, itemOn);
                break;
            case 34:
                Item34to36(i, itemOn);
                break;
            case 35:
                Item34to36(i, itemOn);
                break;
            case 36:
                Item37to40(i, itemOn);
                break;
            case 37:
                Item37to40(i, itemOn);
                break;
            case 38:
                Item37to40(i, itemOn);
                break;
            case 39:
                Item37to40(i, itemOn);
                break;
            
        }
    }

    private void Item1(int i, bool ItemOn)
    {
        machine.GetChild(6).GetChild(0).gameObject.SetActive(!ItemOn);
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
        anim = extraMachine.GetChild(i).GetChild(0).GetComponent<Animation>();
        anim["Bolt_1"].speed = 0;
        anim["Bolt_1"].time = anim["Bolt_1"].length;
    }
    private void Item2(int i, bool ItemOn)
    {
        machine.GetChild(5).GetChild(0).gameObject.SetActive(!ItemOn);
        extraMachine.GetChild(2).gameObject.SetActive(ItemOn);
        anim = extraMachine.GetChild(2).GetChild(0).GetComponent<Animation>();
        anim["Bolt_1"].speed = 0;
        anim["Bolt_1"].time = anim["Bolt_1"].length;
    }
    private void Item3(int i, bool ItemOn)
    {
        machine.GetChild(7).GetChild(0).gameObject.SetActive(!ItemOn);
        extraMachine.GetChild(1).gameObject.SetActive(ItemOn);
        anim = extraMachine.GetChild(1).GetChild(0).GetComponent<Animation>();
        anim["Bolt_1"].speed = 0;
        anim["Bolt_1"].time = anim["Bolt_1"].length;
    }
    private void Item4to5(int i, bool ItemOn)
    {
        if (ItemOn)
            cablesFirst++;
        else
            cablesFirst--;
        machine.GetChild(12).gameObject.SetActive(cablesFirst == 2);
        
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
    }
    private void Item6(int i, bool ItemOn)
    {
        machine.GetChild(1).gameObject.SetActive(ItemOn);
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
    }
    private void Item7(int i, bool ItemOn)
    {
        machine.GetChild(0).gameObject.SetActive(ItemOn);
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
    }
    private void Item8(int i, bool ItemOn)
    {
        machine.GetChild(2).gameObject.SetActive(ItemOn);
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
    }
    private void Item9to11(int i, bool ItemOn)
    {
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
    }
    private void Item12(int i, bool ItemOn)
    {
        for (int l = 0; l < 3; l++)
            machine.GetChild(l+13).gameObject.SetActive(ItemOn);
        
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
        extraMachine.GetChild(21).gameObject.SetActive(ItemOn);
    }
    private void Item13(int i, bool ItemOn)
    {
        for (int l = 0; l < 4; l++)
            machine.GetChild(l+16).gameObject.SetActive(ItemOn);
        machine.GetChild(3).gameObject.SetActive(!ItemOn);
        machine.GetChild(4).gameObject.SetActive(!ItemOn);
        machine.GetChild(12).gameObject.SetActive(cablesFirst == 2);

        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
        extraMachine.GetChild(20).gameObject.SetActive(ItemOn);
    }
    private void Item14to16(int i, bool ItemOn)
    {
        if (ItemOn)
            screwLarge++;
        else
            screwLarge--;
        machine.GetChild(11).GetChild(0).gameObject.SetActive(screwLarge <= 0);
        
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
        anim = extraMachine.GetChild(i).GetChild(0).GetComponent<Animation>();
        anim["Screw_1"].speed = 0;
        anim["Screw_1"].time = anim["Screw_1"].length;
    }
    private void Item17to20(int i, bool ItemOn)
    {
        if (ItemOn)
            screwSmall++;
        else
            screwSmall--;
        machine.GetChild(12).GetChild(0).gameObject.SetActive(screwSmall <= 0);
        
        extraMachine.GetChild(i).gameObject.SetActive(ItemOn);
        anim = extraMachine.GetChild(i).GetChild(0).GetComponent<Animation>();
        anim["Screw_2"].speed = 0;
        anim["Screw_2"].time = anim["Screw_2"].length;
    }
    private void Item21(int i, bool ItemOn)
    {
        extraMachine.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(ItemOn);
        if (ItemOn)
        {
            anim = extraMachine.GetChild(0).GetChild(0).GetComponent<Animation>();
            anim["Bolt_1"].speed = -1;
            anim.Play();
        }
        else
        {
            machine.GetChild(i-20).GetChild(0).gameObject.SetActive(ItemOn);
            PanelCheck();
        }
    }
    private void Item22(int i, bool ItemOn)
    {
        extraMachine.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(ItemOn);
        if (ItemOn)
        {
            anim = extraMachine.GetChild(2).GetChild(0).GetComponent<Animation>();
            anim["Bolt_1"].speed = -1;
            anim.Play();
        }
        else
        {
            machine.GetChild(i-20).GetChild(0).gameObject.SetActive(ItemOn);
            PanelCheck();
        }
    }
    private void Item23(int i, bool ItemOn)
    {
        extraMachine.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(ItemOn);
        if (ItemOn)
        {
            anim = extraMachine.GetChild(1).GetChild(0).GetComponent<Animation>();
            anim["Bolt_1"].speed = -1;
            anim.Play();
        }
        else
        {
            machine.GetChild(i-20).GetChild(0).gameObject.SetActive(ItemOn);
            PanelCheck();
        }
    }
    private void Item34to36(int i, bool ItemOn)
    {
        extraMachine.GetChild(i-20).GetChild(0).GetChild(0).gameObject.SetActive(ItemOn);
        if (ItemOn)
        {
            anim = extraMachine.GetChild(i-20).GetChild(0).GetComponent<Animation>();
            anim["Screw_1"].speed = -1;
            anim.Play();
        }
        else
            machine.GetChild(i-20).GetChild(0).gameObject.SetActive(ItemOn);
    }
    private void Item37to40(int i, bool ItemOn)
    {
        extraMachine.GetChild(i-20).GetChild(0).GetChild(0).gameObject.SetActive(ItemOn);
        if (ItemOn)
        {
            anim = extraMachine.GetChild(i-20).GetChild(0).GetComponent<Animation>();
            anim["Screw_2"].speed = -1;
            anim.Play();
        }
        else
            machine.GetChild(i-20).GetChild(0).gameObject.SetActive(ItemOn);
    }

    private void PanelCheck()
    {
        cogCheck++;
        if (cogCheck == 3)
            machine.GetChild(11).gameObject.SetActive(true);
    }
}
