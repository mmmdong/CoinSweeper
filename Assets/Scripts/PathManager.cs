using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;

    public GeneratePathExample _generatePath;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;

        
    }
}
