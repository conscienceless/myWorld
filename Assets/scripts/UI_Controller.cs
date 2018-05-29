using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Uniblocks;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using UnityEngine.UI;


public class UI_Controller : MonoBehaviour {

    private bool IsGetESC;
    public GameObject leaveConfirmPanel,cross,bagPanel,infoPanel,dropList,shadePanel;
    public Toggle bagToggle;
    private bool IsBagShow;
    private bool IsInfoShow;
    public GameObject[] dropDowns = new GameObject[9];

    private void Awake()
    {
        NewDrop();
    }

    // Use this for initialization
    void Start () {
        IsGetESC = false;
        leaveConfirmPanel.SetActive(false);
        shadePanel.SetActive(false);
        IsBagShow = false;
        IsInfoShow = true;
        bagToggle.isOn = false;
        //bagPanel.SetActive(false);

        for (int i = 0; i < 9; i++)
        {
            Dropdown dp = dropDowns[i].GetComponent<Dropdown>();
            dp.onValueChanged.AddListener(delegate
                {
                    SelectedChanged(dp.gameObject);
                });
        }

    }
	
    private void SelectedChanged(GameObject gameObject)
    {     
        //List<Dropdown.OptionData> dataList = gameObject.GetComponent<Dropdown>().options;
        int index = gameObject.GetComponent<Dropdown>().value;
        gameObject.GetComponent<DropItems>().selectedID = gameObject.GetComponent<DropItems>().blocksID[index];
        print(gameObject.GetComponent<DropItems>().selectedID);
    }

    protected virtual void NewDrop()
    {       
        for (int i = 0; i < 9; i++)
        {
            dropDowns[i] = Instantiate(dropList, bagPanel.transform, false);
            //dropDowns[0].transform.position = new Vector3(10,0,0);
            dropDowns[i].transform.position += new Vector3(80 * i+15, 5, 0);
        }
    }

	// Update is called once per frame
	void Update () {     

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGetESC == false)
            {
                cross.SetActive(false);
                leaveConfirmPanel.SetActive(true);
                IsGetESC = true;
                //GameObject.Find("player").GetComponent<FirstPersonController>().enabled=false;
                GameObject.Find("manager").GetComponent<Block_manager>().enabled = false;
                Cursor.visible=true;
                GameObject.Find("player").GetComponent<FirstPersonController>().cursorVisible=true;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (IsInfoShow)
            {
                infoPanel.SetActive(false);
                IsInfoShow = false;
            }
            else
            {
                infoPanel.SetActive(true);
                IsInfoShow = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsBagShow == false)
            {
                //bagPanel.SetActive(true);
                GameObject.Find("player").GetComponent<FirstPersonController>().cursorVisible=true;
                GameObject.Find("manager").GetComponent<Block_manager>().enabled = false;
                shadePanel.SetActive(true);
                bagToggle.isOn = true;
                IsBagShow = true;
            }
            else
            {
                //bagPanel.SetActive(false);             
                GameObject.Find("player").GetComponent<FirstPersonController>().cursorVisible = false;
                GameObject.Find("manager").GetComponent<Block_manager>().enabled = true;
                shadePanel.SetActive(false);
                bagToggle.isOn = false;
                IsBagShow = false;
            }

        }
        
	}

    public void CancelClick()
    {
        cross.SetActive(true);
        leaveConfirmPanel.SetActive(false);
        IsGetESC = false;
        Cursor.visible = false;
        GameObject.Find("player").GetComponent<FirstPersonController>().cursorVisible = false;
        //GameObject.Find("player").GetComponent<FirstPersonController>().enabled = true;
        GameObject.Find("manager").GetComponent<Block_manager>().enabled = true;
    }

    public void ConfirmClick()
    {
        Engine.SaveWorldInstant();
        IsGetESC = false;
        SceneManager.LoadScene(0);
    }

}
