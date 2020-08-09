using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;



public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
