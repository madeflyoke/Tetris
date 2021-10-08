using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryBase : MonoBehaviour
{
    private FieldInfo fieldInfo;
    public FieldInfo FieldInfo => fieldInfo;

    private PlayerPrefsInfo playerPrefsInfo;
    public PlayerPrefsInfo PlayerPrefsInfo => playerPrefsInfo;

    public void Initialize()
    {
        fieldInfo = new FieldInfo();
        playerPrefsInfo = new PlayerPrefsInfo();
    } 

   
}
