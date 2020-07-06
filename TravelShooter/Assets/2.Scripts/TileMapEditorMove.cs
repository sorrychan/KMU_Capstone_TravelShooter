using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TileMapEditorMove : MonoBehaviour
{

    public void MoveToMain()
    {
        SceneManager.LoadScene("Scene01");
    }

    public void MoveToUserMap()
    {
        SceneManager.LoadScene("UserMap");
    }
}
