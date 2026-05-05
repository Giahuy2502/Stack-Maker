using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject model;
    [SerializeField] private Vector3 brickHeight;
    [SerializeField] private BrickPool brickPool;
    
    private Vector3 startPos, endPos, targetPos,raycastDirect,rotation;
    private Direct direct;
    private bool isMoving = false;
    private Vector3 offset = new Vector3();
    private Stack<GameObject> stack = new Stack<GameObject>();
    private string animName = "idle";
    private GameManager manager => GameManager.Instance;

    private void Awake()
    {
        rotation = model.transform.rotation.eulerAngles;
    }

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        isMoving = false;
        startPos = transform.position;
        endPos = transform.position;
        RemoveAllBrick();
        transform.position = Vector3.zero;
        RotateModel(rotation);
        ChangeAnim("idle");
        //
        manager.onWinGame.AddListener(RemoveAllBrick);
        manager.onWinGame.AddListener(RotateToChess);
        manager.onWinGame.AddListener(ChangeWinAnim);
        // Debug.Log("player Oninit.....");
    }
    public void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) 
        {
            return; 
        }
        if(manager.State == GameState.Start)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            if (manager.State != GameState.Playing)
            {
                manager.ChangeState(GameState.Playing);
            }
            startPos = Input.mousePosition;
            ChangeAnim("jump");
        }
        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            endPos = Input.mousePosition;
            direct = GetDirect(startPos, endPos);
            isMoving = true;
            Move(direct);
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f && manager.State == GameState.Playing)
            {
                transform.position = targetPos;
                isMoving = false;
                ChangeAnim("idle");
            }
        }
        
    }
    private Direct GetDirect(Vector3 startPos, Vector3 endPos)
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
    private Vector3 GetTargetPos(Direct direct)
    {
        RaycastHit hitInfo;
        switch (direct)
        {
            case Direct.Right:
                offset = Vector3.left;
                raycastDirect = Vector3.right;
                break;
            case Direct.Left:
                offset = Vector3.right;
                raycastDirect = Vector3.left;
                break;
            case Direct.Forward:
                offset = Vector3.back;
                raycastDirect = Vector3.forward;
                break;
            case Direct.Back:
                offset = Vector3.forward;
                raycastDirect = Vector3.back;
                break;
        }
        if (Physics.Raycast(transform.position, raycastDirect, out hitInfo, Mathf.Infinity, wallLayer))
        {
            if (hitInfo.collider.CompareTag("Wall"))
            {
                return hitInfo.transform.position + offset;
            }
        }

        return transform.position;
    }
    private void Move(Direct direct)
    {
        targetPos = GetTargetPos(direct);
        offset = Vector3.zero;
        
    }
    public void AddBrick()
    {
        // if (manager.State != GameState.Playing)
        // {
        //     return;
        // }
        // sinh brick mới bên dưới -> nên sử dụng object pooling
        // GameObject newBrick = Instantiate(brickPrefab, transform.position, Quaternion.identity);
        var newBrick = brickPool.GetBrick();
        newBrick.transform.SetParent(this.transform);
        newBrick.transform.position = transform.position + brickHeight * stack.Count;
        stack.Push(newBrick);
        // cho nhân vật nhảy lên
        model.transform.position = transform.position + brickHeight * stack.Count;
    }
    public void RemoveBrick()
    {
        if (stack.Count <= 0)
        {
            manager.OnLoseGame();
            return;
        }
        GameObject brick = stack.Pop();
        brickPool.ReturnBrick(brick);
        model.transform.position = transform.position + brickHeight * stack.Count;
        ChangeAnim("idle");
    }
    public void RemoveAllBrick()
    {
        if (stack.Count <= 0)
        {
            return;
        }
        while(stack.Count > 0)
        {
            GameObject brick = stack.Pop();
            brickPool.ReturnBrick(brick);
            model.transform.position = transform.position + brickHeight * stack.Count;
        }
    }
    public void ChangeAnim(String animName)
    {
        // Debug.Log("ChangeAnim: "+ animName);
        this.animName = animName;
        anim.SetTrigger(this.animName);
    }
    public void RotateToChess()
    {
        RotateModel(Vector3.zero);
    }

    private void RotateModel(Vector3 dicrection)
    {
        model.transform.rotation = Quaternion.Euler(dicrection);
    }
    private void ChangeWinAnim()
    {
        ChangeAnim("win");
    }
    private void OnDisable()
    {
        OnDespawn();
    }
    public void OnDespawn()
    {
        manager.onWinGame.RemoveListener(RemoveAllBrick);
        manager.onWinGame.RemoveListener(RotateToChess);
        manager.onWinGame.RemoveListener(ChangeWinAnim);
    }
}
