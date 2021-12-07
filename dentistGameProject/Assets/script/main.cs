using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
public class main : MonoBehaviour
{
    
    public Transform spawnPoint;//出生点
    public int spawnTimer;//出生时间
    Vector3[] v3 = new Vector3[4];
    public List<GameObject> G;
    GameObject g1;
    int use = 0;
    // Use this for initialization
    void Start()
    {
        v3[0] = new Vector3(-10f, -2.24f, 0);
        v3[1] = new Vector3(-8.5f, -2.24f, 0);
        v3[2] = new Vector3(-7f, -2.24f, 0);
        v3[3] = new Vector3(-5.5f, -2.24f, 0);
        g1 = Resources.Load("wait_chair") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnGUI()
    {
        //just to show how to make a button on GUI - this is hardcoded, not dynamical
        if (GUI.Button(new Rect(230, 10, 100, 70), "暂停/恢复"))
        {
            print("暂停/恢复");
            if (Time.timeScale == 0) Time.timeScale = 1;   // pause toggle / 1 = 100%, 0 = 0%
            else Time.timeScale = 0;
            //			Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(340, 10, 100, 70), "保存记录"))
        {
            DateTime dt = DateTime.Now;
            GameObject.Find("Camera").GetComponent<write>().WriteIntoTxt("当前保存时间："+dt);
            Messagebox.MessageBox(IntPtr.Zero, "已经保存完毕", "提示", 0);
        }
        if (GUI.Button(new Rect(10, 10, 100, 70), "开始"))
        {
            //SceneManager.LoadScene(1);
            GameObject.Find("Camera").GetComponent<maincontrol>().lock1 = true;
        }
        if (GUI.Button(new Rect(120, 10, 100, 70), "结束"))
        {
            SceneManager.LoadScene(0);
            //GameObject.Find("Canvas").GetComponent<maincontrol>().lock1 = true;
        }
        if (GUI.Button(new Rect(450, 10, 100, 70), "增加椅子"))
        {
            
            switch (use)
            {
                case 0:

                    GameObject Gg = Instantiate(g1, v3[0], Quaternion.identity);
                    
                    
                    G.Add(Gg);
                    Gg.transform.SetParent(GameObject.Find("chairs").transform);

                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    use++;
                    break;
                case 1:
                    GameObject Gg1 = Instantiate(g1, v3[1], Quaternion.identity);
                    G.Add(Gg1);
                    Gg1.transform.SetParent(GameObject.Find("chairs").transform);
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    use++;
                    break;
                case 2:
                    GameObject Gg2 = Instantiate(g1, v3[2], Quaternion.identity);
                    G.Add(Gg2);
                    Gg2.transform.SetParent(GameObject.Find("chairs").transform);
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    use++;
                    break;
                case 3:
                    GameObject Gg3 = Instantiate(g1, v3[3], Quaternion.identity);
                    G.Add(Gg3);
                    Gg3.transform.SetParent(GameObject.Find("chairs").transform);
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    use++;
                    break;
            }
        }
            if (GUI.Button(new Rect(560, 10, 100, 70), "删除椅子"))
            {
                switch (G.Count)
                {
                    case 4:
                    GameObject a = G[0];
                    Destroy(a,0);
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    G.RemoveAt(0);
                    
                    use--;
                        break;
                    case 3:
                    GameObject a1 = G[0];
                    Destroy(a1, 0);
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    G.RemoveAt(0);
                        use--;
                        break;
                    case 2:
                    GameObject a2 = G[0];
                    Destroy(a2, 0);
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    G.RemoveAt(0);
                        use--;
                        break;
                    case 1:
                    Debug.Log("adasd");
                    GameObject a3 = G[0];
                    DestroyImmediate(a3);
                    
                    GameObject.Find("Camera").GetComponent<maincontrol>().onenable();
                    G.RemoveAt(0);
                        use--;
                        break;
                }
                
           }
            
        
    }
}

