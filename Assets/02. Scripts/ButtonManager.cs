using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    #region SingleTon
    public static ButtonManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public ButtonAddFloorController addFloorBtn;
    public ButtonDropTermController dropTermBtn;
    public ButtonPiggySellController piggySellBtn;
}
