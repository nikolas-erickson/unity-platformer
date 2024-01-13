using Unity.VisualScripting;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    void Start()
    {
        //Application.targetFrameRate = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
