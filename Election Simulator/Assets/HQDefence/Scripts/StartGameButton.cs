using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour {

    void OnMouseDown()
    {
        FindObjectOfType<NextLevelLoader>().LoadNextLevel();
    }


    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }
}
