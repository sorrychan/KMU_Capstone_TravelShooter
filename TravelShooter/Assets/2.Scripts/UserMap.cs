using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UserMap : MonoBehaviour
{
    public void MoveToTileMapEditor()
    {
        SceneManager.LoadScene("TileMapEditor");
    }
}
