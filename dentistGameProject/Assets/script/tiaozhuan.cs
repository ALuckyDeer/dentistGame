using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tiaozhuan : MonoBehaviour {
    public float speed = 1f;
    public float count;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count += speed;
        if(count<=100)
        {
            GameObject.Find("Slider").GetComponent<Slider>().value = count / 100.0f;
        }
        else
        {
            SceneManager.LoadScene("1");
        }

    }
}
