using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Image loading;
    [SerializeField] private float loadingTime;

    private UIManager uIManager => UIManager.Instance;
    private void OnEnable()
    {
        OnInit();
    }

    private void OnInit()
    {
        loading.fillAmount = 0f;
    }

    private void Update()
    {
        if (loading.fillAmount < 1f)
        {
            loading.fillAmount += Time.deltaTime / loadingTime;
            if (loading.fillAmount >= 1f)
            {
                loading.fillAmount = 1f;
                uIManager.OnMenu();
            }
        }
    }

    private void OnDespawn()
    {
        
    }
}
