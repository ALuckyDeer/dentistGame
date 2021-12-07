using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class create_patient : MonoBehaviour {//预制体   
    public Object[] prefabs;
    public GameObject[] swpan;//出生点数组
    public Object Obj;//返回的的object
    public int allChairNum;
    void Awake()
    {
        allChairNum = GameObject.FindGameObjectsWithTag("chairs").Length;
    } 
    void Start () {
        
    }
	void Update () {
       //create();
    }
   
    public Object create()//创建人物
    {
        Obj = null;//防止引用上一个
        prefabs = Resources.LoadAll("person") as Object[];
        swpan = GameObject.FindGameObjectsWithTag("create");
        int tempa, tempb;
        tempa = Random.Range(0, 5);
        tempb = Random.Range(0, 1);//1个出生点
        Vector3 vec = new Vector3(0,0.5f,0);
        Obj =Instantiate(prefabs[tempa],swpan[tempb].transform.position,Quaternion.identity);

        
        if (Obj != null)
        {
            GameObject.Find("Camera").GetComponent<maincontrol>().havedPer += 1;     
        }
        return Obj;
    }
    
}
