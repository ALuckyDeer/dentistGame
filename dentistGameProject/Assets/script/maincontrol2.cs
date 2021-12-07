using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class maincontrol2 : MonoBehaviour
{
    public Thread[] th = new Thread[8];//第二个线程
    Semaphore se = new Semaphore(1, 1);//初始允许请求为1，最大请求为1
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Loom.RunAsync(() =>
        {
            if (GetComponent<maincontrol>().allbeoccupied == true)//证明存在一个座位被占了
        {

            //抢占开始                
            for (int i = 0; i < GetComponent<maincontrol>().chair_number; i++)
            {

                th[i] = new Thread(Refresh2);
                // Debug.LogError("2线程开启");
                th[i].Start(i);

            }
            //抢占成功的人开始走路
        }
        });
    }
    public void Refresh2(object o2)
    {
        Loom.QueueOnMainThread((parama) => {

            Debug.Log("准备抢占");
            se.WaitOne();
            int temp = (int)o2;
            Debug.Log(temp + "抢占成功" + Time.deltaTime);
            
           // GetComponent<maincontrol>().per[temp].GetComponent<person>().walk2();
            se.Release();

        }, null);
    }
}
    