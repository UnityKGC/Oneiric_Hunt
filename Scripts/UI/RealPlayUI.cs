using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayUI : MonoBehaviour
{
    public enum RealPlayUIState
    {
        None = -1,
        Quest,
        Dialogue,
    }

    public RealPlayUIState _uiState = RealPlayUIState.None;

    public List<GameObject> _sceneUILst;

    void Start()
    {
        
    }

    
}
