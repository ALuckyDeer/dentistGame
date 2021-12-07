using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class write : MonoBehaviour
{
    StreamWriter writer;
    StreamReader reader;

    List<int> Allmytxt = new List<int>();
    // Use this for initialization
    void Start()
    {
        FileInfo file = new FileInfo(Application.dataPath + "/mytxt.txt");
        if (file.Exists)
        {
            file.Delete();
            file.Refresh();
        }
    }
    //把所有的数据写入文本中
    public void WriteIntoTxt(string message)
    {
        FileInfo file = new FileInfo(Application.dataPath + "/mytxt.txt");
        if (!file.Exists)
        {
            writer = file.CreateText();
        }
        else
        {
            writer = file.AppendText();
        }
        writer.WriteLine(message);
        writer.Flush();
        writer.Dispose();
        writer.Close();
    }
    //读取分数 存储到列表中
    public void ReadOutTxt()
    {
        Allmytxt.Clear();
        reader = new StreamReader(Application.dataPath + "/mytxt.txt", Encoding.UTF8);
        string text;
        while ((text = reader.ReadLine()) != null)
        {
            Allmytxt.Add(int.Parse(text));
        }
        reader.Dispose();
        reader.Close();
    }
    /// <summary>
    /// 获取从列表读取数据之后的List
    /// </summary>
    /// <returns></returns>
    public List<int> GetmytxtList()
    {
        ReadOutTxt();
        return Allmytxt;
    }

}
