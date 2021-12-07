using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour {
    private Text uiText;
    private Text uiText1;
    private Text uiText2;

    //储存中间值
    private string words;
    private string words2;
   
    private float timer;
    //限制条件，是否可以进行文本的输出
    
   

    // Use this for initialization
    void Start () {
        uiText = GameObject.Find("Text").GetComponent<Text>();
        uiText1 = GameObject.Find("Text (1)").GetComponent<Text>();
        uiText2 = GameObject.Find("Text (2)").GetComponent<Text>();



    }

    // Update is called once per frame
    void Update () {

        
        words = "当前病人数量："+GameObject.Find("Camera").GetComponent<maincontrol>().havedPer+"\n"+ "候诊椅的剩余情况：" + (GameObject.Find("Camera").GetComponent<maincontrol>().ch.Count) +"\n"+ "候诊椅的使用情况：" + (GameObject.Find("Camera").GetComponent<maincontrol>().chair_number - GameObject.Find("Camera").GetComponent<maincontrol>().ch.Count) + "\n" +
            "当前运行线程编号：" + GameObject.Find("Camera").GetComponent<maincontrol>().temp + "\n" + "阻塞线程个数：" + (GameObject.Find("Camera").GetComponent<maincontrol>().onchairpersons.Count)+"\n"+"当前外面等待人数："+ GameObject.Find("Camera").GetComponent<maincontrol>().waitnumber;
        if(words!=words2)
        GameObject.Find("Camera").GetComponent<write>().WriteIntoTxt(words+"游戏时间"+Time.deltaTime);
        uiText1.text = "随机生成的最大人数：" + GameObject.Find("Canvas").GetComponent<Power>().bottom1;
        uiText2.text = "室外等待最大人数：" + GameObject.Find("Canvas").GetComponent<Power>().bottom2;
        
        uiText.text = words;
        words2 = words;

    }
    void Latedate()
    {
        
    }
    




}
