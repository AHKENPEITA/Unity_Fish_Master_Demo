using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Collections;

public class Controller : MonoBehaviour
{

    private static Controller _instance;
    public static Controller Instance
    {
        get
        {
            return _instance;
        }
    }


    public GameObject[] gunLists;

    public GameObject[] BulletGos0;
    public GameObject[] BulletGos1;
    public GameObject[] BulletGos2;
    public GameObject[] BulletGos3;
    public GameObject[] BulletGos4;

    private GameObject[] useBulletGos;


    public Transform BulletHolder;


    public int gunLevel = 0;

    public Color goldColor;

    public Text costText;
    public Text goldText;
    public Text LvText;
    public Text LvNameText;
    public Text smallCountDownText;
    public Text bigCountDownText;

    public Button back;
    public Button setting;
    public Button addMoneyButton;
    public Slider expSlider;

    public int lv = 0;
    public int exp = 0;
    public int gold = 500;
    public const int bigCountdown = 240;
    public const int smallCountdown = 60;
    public float bigTimer=bigCountdown;
    public float smallTimer = smallCountdown;

    private int costListsIndex = 0;
    public int cost = 5;
    private int[] costLists = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    private String[] lvName = { "入门","学徒","学助","技工","技师","专家","掌门","大师","宗师","祖师"};


    private void Start()
    {
        useBulletGos = BulletGos0;
        goldColor = goldText.color;
    }
    void Update()
    {
        ChangeBulletCost();
        Fire();
        UpdateUI();
    }

     void Awake()
    {
        _instance = this;
        
    }
    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnButtonMDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonPDown();
        }
    }

    void UpdateUI()
    {
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer <= 1)
        {
            smallTimer = smallCountdown;
            gold += 10*lv+1;
        }
        if (bigTimer <= 1 && addMoneyButton.gameObject.activeSelf == false)
        {
            bigCountDownText.gameObject.SetActive(false);
            addMoneyButton.gameObject.SetActive(true);
        }



        //升级所需经验为100+20*当前等级；
        while (exp >= 100 + (20 * lv)*(lv/5+1))
        {
            exp -= 100 + (20 * lv) * (lv / 5 + 1);
            lv++;
            
            
        }
        if (lv > 99)
        {
            LvText.text = "Max";
        }
        else
        {
            LvText.text = lv.ToString();
        }
    
        if (lv / 10 > 9)
        {
            LvNameText.text = lvName[9];
        }
        else
        {
            LvNameText.text = lvName[lv / 10];
        }
        goldText.text = "$"+gold;
        smallCountDownText.text = (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
        bigCountDownText.text = (int)bigTimer+"s";
        expSlider.value = (float)exp / (100 + (20 * lv) * (lv / 5 + 1));
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0)&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (gold - cost >= 0)
            {
                gold -= cost;
                GameObject BulletGos = lv > 99 ? useBulletGos[9] : useBulletGos[lv % 10];
                GameObject bullet = Instantiate(BulletGos);
                bullet.transform.SetParent(BulletHolder, false);
                Transform FirePos = gunLists[gunLevel].transform.Find("FirePosition").transform;
                // Debug.Log(gunLists[gunLevel].transform.rotation.z);
                bullet.transform.position = FirePos.transform.position;
                bullet.transform.rotation = FirePos.transform.rotation;
                bullet.AddComponent<Ef_AutoMove>().moveDirection = Vector3.up;
                bullet.GetComponent<Ef_AutoMove>().moveSpeed = bullet.GetComponent<BulletAttr>().speed;
                bullet.GetComponent<BulletAttr>().damage = costLists[costListsIndex];
                bullet.GetComponent<BulletAttr>().disapperTimeFactor = (0.01*(costListsIndex/4+1)*lv);
            }
            else
            {
                StartCoroutine(goldNotEnough());
            }
        }
        
    }
    public void OnButtonPDown()
    {
        if ((costListsIndex < costLists.Length - 1)&&(lv/5>=costListsIndex+1))
        {
            gunLists[gunLevel].SetActive(false);
            costListsIndex++;
            gunLevel = costListsIndex / 4;
            gunLists[gunLevel].SetActive(true);
            switch (gunLevel)
            {
                case 0 : useBulletGos = BulletGos0;
                    break;
                case 1 : useBulletGos = BulletGos1;
                    break;
                case 2 : useBulletGos = BulletGos2;
                    break;
                case 3 : useBulletGos = BulletGos3;
                    break;
                case 4 : useBulletGos = BulletGos4;
                    break;
            }
            cost = costLists[costListsIndex];

            costText.text = "$" + costLists[costListsIndex];

        }
    }
    public void OnButtonMDown()
    {
        if (costListsIndex > 0)
        {
            gunLists[gunLevel].SetActive(false);
            costListsIndex--;
            gunLevel = costListsIndex / 4;
            gunLists[gunLevel].SetActive(true);
            switch (gunLevel)
            {
                case 0:useBulletGos = BulletGos0;
                    break;
                case 1:useBulletGos = BulletGos1;
                    break;
                case 2:useBulletGos = BulletGos2;
                    break;
                case 3:useBulletGos = BulletGos3;
                    break;
                case 4:useBulletGos = BulletGos4;
                    break;
            }
            cost = costLists[costListsIndex];

            costText.text = "$" + costLists[costListsIndex];
        }
    }

    public void OnAddMoneyButtonDown()
    {
        int n = 0;
        for (int i = lv / 10; i > 0; i--)
        {
            n += i;
        }
        int money = 50 + 5 * (((lv / 10) + 1) * lv - (10 * n));
        
        gold += money;
        addMoneyButton.gameObject.SetActive(false);
        bigCountDownText.gameObject.SetActive(true);
        bigTimer = bigCountdown;

    }

    IEnumerator goldNotEnough()
    {
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        goldText.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        goldText.color = goldColor;
    }

}
