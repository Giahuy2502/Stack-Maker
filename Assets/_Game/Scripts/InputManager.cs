using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    private Vector3 startPos, endPos;
    private Direct direct;
    public Direct Direct
    {
        get => direct;
    }
    public static InputManager Instance { get; private set; }
    private GameManager GameManager => GameManager.Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnInit()
    {
        startPos = player.transform.position;
        endPos = player.transform.position;
    }
    private void Update()
    {
        if (IsClickToUI())
        {
            return;
        }
        if(GameManager.State != GameState.Playing)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !player.IsMoving)
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && !player.IsMoving)
        {
            endPos = Input.mousePosition;
            direct = GetDirect(startPos, endPos);
            player.Move(direct);
        }
    }

    public bool IsClickToUI()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
        }
        return false;
    }
    public Direct GetDirect(Vector3 startPos, Vector3 endPos)
    {
        Vector3 direction = (endPos - startPos).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? Direct.Right : Direct.Left;
        }
        else
        {
            return direction.y > 0 ? Direct.Forward : Direct.Back;
        }
    }
}
