using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Power : MonoBehaviour {
    public float speed;
    public float count;
    public bool mutex = false;//不用圆盘
    public int bottom1;
    public int bottom2;//底下的第一条，第二条
    void Awake()
    {
       // gg.GetComponent<Slider>().fillRect.transform.GetComponent<Image>().color = new Color(255, 0, 0, 255);
        //gg.SetParent(GameObject.Find("Canvas").transform);

    }
    // Use this for initialization
    void Start () {
        
       
    }
	
	// Update is called once per frame
	void Update () {
        bottom1 = (int)(GameObject.Find("Slider (1)").GetComponent<Slider>().value * 10);
        bottom2 = (int)(GameObject.Find("Slider (2)").GetComponent<Slider>().value * 5);
        Debug.Log(bottom1+"_________________________");

        //speed= GameObject.Find("Slider").GetComponent<Slider>().value + 0.1f;
        count = GameObject.Find("Camera").GetComponent<maincontrol>().sleepcount;
        if (mutex == true)
        {
            if (count <= 100)
            {
                //count += speed;
                //Debug.Log(count);
                GameObject.Find("Image").GetComponent<Image>().fillAmount = count / 100.0f;
                GameObject.Find("Text (3)").GetComponent<Text>().text = ((int)count).ToString() + "%";

            }
            if (count >= 99)
            {
                GameObject.Find("Image").GetComponent<Image>().color = new Color(255, 0, 0, 255);

            }
            if (count > 100)
            {
                //count = 0;
                GameObject.Find("Image").GetComponent<Image>().color = new Color(0, 255, 0, 255);
                GameObject.Find("Text (3)").GetComponent<Text>().text = "0%";
                GameObject.Find("Image").GetComponent<Image>().fillAmount = 0;
                mutex = false;

            }

        }



    }
}
