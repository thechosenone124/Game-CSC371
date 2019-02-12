using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputContainer : MonoBehaviour {

    public bool isOperatingStation = false;

    private float horizontal = 0f;
    private float vertical = 0f;
    private float rtButton = 0f;
    private bool aButton = false;
    private bool bButton = false;
    private bool xButton = false;
    private bool yButton = false;


    public void SetInput(float hori, float vert, bool aBut, bool bBut, float rtBut)
    {
        horizontal = hori;
        vertical = vert;
        aButton = aBut;
        bButton = bBut;
        rtButton = rtBut;
    }

    public float GetHorizontal() { return horizontal; }
    public float GetVertical  () { return vertical;   }
    public bool  GetAButton   () { return aButton;    }
    public bool  GetBButton   () { return bButton;    }
    public bool  GetXButton   () { return xButton;    }
    public bool  GetYButton   () { return yButton;    }
    public float GetRTButton  () { return rtButton;   }
}
