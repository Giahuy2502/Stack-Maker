using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Vector3 brickHeight;
    
    private Vector3 startPos, endPos, targetPos,raycastDirect;
    private Direct direct;
    private bool isMoving = false;
    private Vector3 offset = new Vector3();
    private Stack<GameObject> stack = new Stack<GameObject>();

    private int animIdx = 0;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        isMoving = false;
        startPos = transform.position;
        endPos = transform.position;
        stack.Clear();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            direct = GetDirect(startPos, endPos);
            isMoving = true;
            Move(direct);
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                transform.position = targetPos;
                isMoving = false;
                ChangeAnim(0);
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
        // sinh brick mới bên dưới
        GameObject newBrick = Instantiate(brickPrefab, transform.position, Quaternion.identity);
        newBrick.transform.SetParent(this.transform);
        newBrick.transform.position = transform.position + brickHeight * stack.Count;
        stack.Push(newBrick);
        // cho nhân vật nhảy lên
        ChangeAnim(1);
        model.transform.position = transform.position + brickHeight * stack.Count;
    }
    public void RemoveBrick()
    {
        if (stack.Count <= 0)
        {
            return;
        }
        GameObject brick = stack.Pop();
        Destroy(brick);
        model.transform.position = transform.position + brickHeight * stack.Count;
        
    }
    public void ChangeAnim(int animIdx)
    {
        if (this.animIdx == animIdx) return;
        Debug.Log("ChangeAnim: "+ animIdx);
        this.animIdx = animIdx;
        anim.SetInteger("renwu", this.animIdx);
    }
}
