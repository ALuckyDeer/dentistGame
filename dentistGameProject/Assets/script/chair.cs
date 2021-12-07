using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair : MonoBehaviour {

    public bool occupied = false;
    public GameObject[] person= {null};
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.occupied = isOccupied();//每次都判断当前被占用情况
	}
    public bool freeChair()
    {
        return false;
    }
    public void occupyChair(GameObject patient)
    {

    }
    public bool isOccupied()//椅子被占，人和椅子的状态都改变
    {
        person = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < person.Length; i++)
        {
            if (this.transform.position == person[i].transform.position)
            {
                person[i].GetComponent<person>().isgetchair = true;
                GameObject.Find("Camera").GetComponent<maincontrol>().allbeoccupied = true;//被占一个就改变状态              
                return true;
            }
            
                
        }
        return false;
    }
}
