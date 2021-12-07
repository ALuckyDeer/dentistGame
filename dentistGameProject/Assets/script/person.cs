using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class person : MonoBehaviour {
    public float movespeed = 2;//
    public Vector3[] gos = new Vector3[3];
    public Vector3[] gosto = new Vector3[7];//返回点
    int i = 0;
    float des;
    public bool isgetchair = false;
    public GameObject bloodbar;
    public Transform par;//父类
    public bool gettwo = false;//得到了第二条路的终点
    public bool getthreed=false;//得到了第三条路
    public bool nothavein=false;//没放进onchair中去过
    public int away = 0;//离开了候诊椅就不是0了
    void Awake()
    {
        gos[0] = GameObject.Find("fir_obj").transform.position;       
        gos[1] = GameObject.Find("sec_obj").transform.position;       
        if (GameObject.Find("Camera").GetComponent<maincontrol>().ch.Count != 0)
        {
            gos[2] = GameObject.Find("Camera").GetComponent<maincontrol>().ch[0].transform.position;
            GameObject.Find("Camera").GetComponent<maincontrol>().ch.RemoveAt(0);

        }

        bloodbar = Instantiate(Resources.Load("Slider/Slider"),this.transform.position, Quaternion.identity) as GameObject; //生成预设体
        bloodbar.GetComponent<Slider>().fillRect.transform.GetComponent<Image>().color = new Color(0, 255, 0, 255);
    }
    // Use this for initialization
    void Start () {

       
        bloodbar.transform.SetParent(GameObject.Find("Canvas").transform);
        speedd = Random.Range(0.1f,0.5f);
       
    }
    public float count=0;
    float speedd;
    
    // Update is called once per frame
    void Update () {
        if (count < 500)
        {
            count += speedd;
            bloodbar.GetComponent<Slider>().value = count / 100.0f;

        }
        if (isgetchair == false)//是否得到过第一个椅子
        {
            walk();
        }
        bloodbar.transform.position = transform.position + new Vector3(0, 0.5f, 0);
       
       
       

    }
    public void walk()
    {       
        Vector3 v = (gos[i] - transform.position).normalized;//向量
        transform.right = v;//朝向目标点
        des = Vector3.Distance(this.transform.position, gos[i]);
        transform.localPosition = Vector3.MoveTowards(this.transform.position, gos[i], Time.deltaTime * movespeed);
        if (this.transform.position == gos[2])//gos[2]是座位
        {
            this.GetComponent<Rigidbody2D>().isKinematic = true;//刚体运动学防抖动
        }       
        if (des < 1f && i < 2)
        {
            i++;
        }
       


    }
    public void walk2()
    {
        //Debug.Log("走2");
        //if (away==0)
        //{
        //   GameObject.Find("Camera").GetComponent<maincontrol>().ch.Add(GameObject.Find("Camera").GetComponent<maincontrol>().onchairs[0]);//空椅子++
        //    GameObject.Find("Camera").GetComponent<maincontrol>().onchairpersons.RemoveAt(0);
        //    GameObject.Find("Camera").GetComponent<maincontrol>(). onchairs.RemoveAt(0);
        //    away++;
        //}
        Vector3 v = (GameObject.Find("work_chair").transform.position - transform.position).normalized;//向量
        transform.right = v;//朝向目标点
        des = Vector3.Distance(this.transform.position, GameObject.Find("work_chair").transform.position);
        //Debug.Log(this.transform.position+"+++++++++++++"+ GameObject.Find("work_chair").transform.position);
        transform.localPosition = Vector3.MoveTowards(this.transform.position, GameObject.Find("work_chair").transform.position, Time.deltaTime * movespeed);
       
    }
    public void walk3()
    {
        Vector3 v = (GameObject.Find("death").transform.position - transform.position).normalized;//向量
        transform.right = v;//朝向目标点
        des = Vector3.Distance(this.transform.position, GameObject.Find("death").transform.position);
        transform.localPosition = Vector3.MoveTowards(this.transform.position, GameObject.Find("death").transform.position, Time.deltaTime * movespeed);
    }
}
