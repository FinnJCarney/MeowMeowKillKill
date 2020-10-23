using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager me;

    public bool mouseLock;
    public int timer;

    public Text text;

    public int ratNum;

    void Awake()
    {
        me = this;
        if (mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (ratNum == 0 && timer > 120)
        {
            text.gameObject.active = true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        
    }
}
