using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject ConnectOptionsPanelGameobject;
    public GameObject ConnectWithNamePanelGameobject;

    #region Unity Methods
    void Start()
    {
        ConnectOptionsPanelGameobject.SetActive(true);
        ConnectWithNamePanelGameobject.SetActive(false);
    }

    void Update()
    {

    }
    #endregion
}
