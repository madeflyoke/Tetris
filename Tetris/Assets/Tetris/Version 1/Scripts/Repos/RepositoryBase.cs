using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryBase : MonoBehaviour
{
    private FieldInfo fieldInfo;
    public FieldInfo FieldInfo => fieldInfo;
    public void Initialize()
    {
        fieldInfo = new FieldInfo();
    } 

   
}
