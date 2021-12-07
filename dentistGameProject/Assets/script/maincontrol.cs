using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
public class maincontrol : MonoBehaviour {

    public int max_patient=8;
    public int min_patient=3;
    public int chair_number;
    public int havedPer=0;//已经存在的人
    public Thread[] th=new Thread[9];//测试线程
    public int create_num;
    public List<GameObject> per;//人的列表
    public GameObject[] chairs;//椅子
    public List<GameObject> ch;//空椅子队列
    public List<GameObject> ou;//外面排队空队列
    public List<GameObject> onchairpersons;//在椅子上的人的队列
    public List<GameObject> onchairs;//在椅子上的人的椅子的队列
    public bool allbeoccupied = false;
    Semaphore se = new Semaphore(1,1);//初始允许请求为1，最大请求为1
    Semaphore se2 = new Semaphore(1, 1);//初始允许请求为1，最大请求为1
    public GameObject g;//保存中介用来访问三条条路
    int mutex = 1;
    int ii = 0;
    int chansheng=0;//产生
    private float timer = 0;//计时变量//等候的人
    public int time = 3;//计时频率
    private float timer1 = 0;//计时变量//医生
    public int time1 = 3;//计时频率
    public string thname;//当前线程的名字
    public int temp;//线程编号
    public float sleepcount = 0;
    public float speed;
    public bool lock1 = false;
    public int waitnumber=0;
    public int waitmax=5;//等待人数的最大值
    void Awake()
    {


        chair_number = GameObject.FindGameObjectsWithTag("chairs").Length;//得到椅子数量
        chairs = GameObject.FindGameObjectsWithTag("chairs");


        ch.Clear();
        for (int i = 0; i < chair_number; i++)//椅子是否占有队列初始化 chair_number
        {
            if (chairs[i].GetComponent<chair>().isOccupied() == false)//没有被占的椅子保存
            {
                ch.Add(chairs[i]);
            }
        }
        //Debug.Log(chair_number+"______________________________");
    }
    void OnEnable()
    {
        
    }
    void Start()
    {

        
    }
    
    //public void lthmdo()//less than min do 小于最小该做的
    //{
    //    create_num = Random.Range(1, 5);//max_patient - havedPer
    //    for (int i = 0; i < create_num; i++)//create_num
    //    {
    //            GameObject g = (GameObject)GameObject.Find("Camera").GetComponent<create_patient>().create();
    //            per.Add(g);//增加人
    //        //Debug.Log(per.Count+"拥有的人："+havedPer);
            
    //        }
        
    //}
    public void lthmdo(int num)//less than min do 小于最小该做的
    {
       
        for (int i = 0; i < num; i++)//create_num
        {
            GameObject g = (GameObject)GameObject.Find("Camera").GetComponent<create_patient>().create();
            per.Add(g);//增加人
                       //Debug.Log(per.Count+"拥有的人："+havedPer);

        }

    }
    public void onenable()
    {
        Awake();
    }
    void Update()
    {
        
        if (lock1 == true)
        {
            
            //Debug.Log(chair_number+"QQQQQQQQQQQQQQQQQQQ");
            speed = GameObject.Find("Slider").GetComponent<Slider>().value + 0.1f;
            max_patient = GameObject.Find("Canvas").GetComponent<Power>().bottom1;
            waitmax  = GameObject.Find("Canvas").GetComponent<Power>().bottom2;
            if (ii == 0)
            {
                lthmdo(5);

            }
            ii++;
            //if (havedPer < 5)

            //    lthmdo(create_num);//产生

            outdo();
            lthmdo(create_num);//产生
           

            for (int i = 0; i < havedPer; i++)
            {
                //Debug.Log(havedPer+"、、、、、"+per.Count);
                if (per[i].GetComponent<person>().nothavein == false)
                {
                    for (int j = 0; j < chair_number; j++)
                    {
                        if (per[i].GetComponent<person>().transform.position == GameObject.Find("Camera").GetComponent<maincontrol>().chairs[j].transform.position)
                        {
                            GameObject.Find("Camera").GetComponent<maincontrol>().onchairpersons.Add(per[i]);
                            GameObject.Find("Camera").GetComponent<maincontrol>().onchairs.Add(GameObject.Find("Camera").GetComponent<maincontrol>().chairs[j]);
                            per[i].GetComponent<person>().isgetchair = true;
                            per[i].GetComponent<person>().nothavein = true;
                        }
                    }





                }
            }
            // Refresh();

            Loom.RunAsync(() =>
            {

                if (allbeoccupied == true)//如果队列小于5 证明有一个座位被占了
                {


                    //Debug.Log(onchairpersons.Count);
                    //抢占开始                
                    for (int i = 0; i < onchairpersons.Count; i++)
                    {

                        th[i] = new Thread(Refresh2);
                        th[i].Start(i);

                        //timer1 += Time.deltaTime;
                        if (timer1 > time1)
                        {

                            //th[i].Resume();

                            timer1 = 0;//计时器清零

                        }
                    }

                    Loom.QueueOnMainThread((parama) =>
                    {

                    }, null);
                    //抢占成功的人开始走路
                }
            });



        }


    }
    void outdo()
    {
        if (waitnumber == 0)
        {
            if (create_num > ch.Count)
            {
                waitnumber += create_num - ch.Count;
                create_num = ch.Count;
            }
           
        }
        if (waitnumber>0)
        {
            if (waitnumber > ch.Count)
            {
                waitnumber -= ch.Count;
                if (waitnumber + create_num > waitmax)
                {
                    waitnumber = waitmax;
                    create_num = 0;
                }
                else
                {
                    waitnumber += create_num;
                    create_num = 0;
                }
            }
            else
            {
                waitnumber = create_num - (ch.Count - waitnumber);
                create_num = ch.Count - waitnumber;
                if (waitnumber > waitmax)
                {
                    waitnumber = waitmax;
                }
            }

                
        }
    }
    void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            create_num = Random.Range(0, max_patient);//max_patient - havedPer            
            Debug.Log("门外等候的人" + create_num);
            timer = 0;//计时器清零
            Debug.Log("计时1次");
        }

    }

    public void Refresh2(object o2)
    {
        temp = (int)o2;
        //se.WaitOne(1); //Thread.Sleep(30);
        Loom.QueueOnMainThread((parama) =>
        {
            se.WaitOne();
            //thname = "线程编号：{" + temp.ToString() + "}";
            //th[temp].Join();
            //Debug.Log(temp + "抢占成功" + Time.deltaTime);
            //Debug.Log(onchairpersons.Count + "shuliang");
            if (onchairpersons.Count > 0)//onchairpersons.Count >-1&&
            {
                g = onchairpersons[0];
                //onchairpersons.RemoveAt(0);
                if (onchairpersons[0].GetComponent<person>().gettwo == false)//没走到workchair就走
                    onchairpersons[0].GetComponent<person>().walk2();
                if (onchairpersons[0].transform.position == GameObject.Find("work_chair").transform.position)
                {
                    onchairpersons[0].GetComponent<person>().gettwo = true;
                    
                    Debug.Log("到达了椅子");
                    //g = onchairpersons[0];

                }
                if (onchairpersons[0].GetComponent<person>().gettwo == true && onchairpersons[0].GetComponent<person>().getthreed == false)
                {
                    //Debug.Log("asdasdassssssssssssssssssssssssssssssssssssssssssssss");
                    sleepcount += speed;
                    //Debug.LogError(sleepcount);
                    GameObject.Find("Canvas").GetComponent<Power>().mutex = true;
                    if (sleepcount >100)
                    {


                        onchairpersons[0].GetComponent<person>().walk3();
                        if (onchairpersons[0].transform.position == GameObject.Find("death").transform.position)
                        {
                            Debug.Log("到达了终点");
                            onchairpersons[0].GetComponent<person>().getthreed = true;

                            havedPer--;


                            ch.Add(onchairs[0]);//空椅子++
                            onchairpersons.RemoveAt(0);
                            onchairs.RemoveAt(0);
                            per.RemoveAt(0);
                            sleepcount = 0;
                            // Destroy(g, 0);
                            //th[temp].Abort();


                        }
                    }
                }

            }
            else
                th[temp].Abort();



            se.Release(1);
        }, null);
        //if (onchairpersons[0].GetComponent<person>().getthreed == true)
        //{
        //    th[temp].Suspend();
        //}
       
    }
    public void Refresh()
    {
        Loom.QueueOnMainThread((parama) =>
       {
           if (mutex == 1)
           {
               mutex--;
               for (int i = 0; i < onchairpersons.Count; i++)
               {
                   if (onchairpersons[i] != null)
                   {


                       if (onchairpersons[i].GetComponent<person>().gettwo == false)//没走到workchair就走，走到第三个椅子
                           onchairpersons[i].GetComponent<person>().walk2();
                       Debug.Log("开始走");
                       if (onchairpersons[i].transform.position == GameObject.Find("work_chair").transform.position)
                       {
                           onchairpersons[i].GetComponent<person>().gettwo = true;
                           Debug.Log("到达了椅子");
                           //g = onchairpersons[0];

                       }
                       if (onchairpersons[i].GetComponent<person>().gettwo == true && onchairpersons[0].GetComponent<person>().getthreed == false)
                       {
                          
                             onchairpersons[i].GetComponent<person>().walk3();
                             if (onchairpersons[i].transform.position == GameObject.Find("death").transform.position)
                             {
                               Debug.Log("到达了终点");

                               //g = onchairpersons[0];
                               onchairpersons[i].GetComponent<person>().getthreed = true;
                               g = onchairpersons[i];
                               //onchairpersons.RemoveAt(i);
                               onchairpersons[i] = null;
                                   sleepcount = 0;
                               //Destroy(g, 0);

                             }
                           
                       }
                   }
               }

               mutex++;
           }
           else
           {
               //跳过这个for的一个
               
           }
       },null);

    }

}
