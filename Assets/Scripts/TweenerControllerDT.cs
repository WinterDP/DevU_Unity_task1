using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TweenerControllerDT : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    [SerializeField] private float _speed = 2f;

    [SerializeField] private float xPos;

    [SerializeField] private int typeMovement;

    [SerializeField] private float increaseX;

    Sequence _moveSequence; 

    private void Start ()
    {
        //_moveSequence = DOTween.Sequence().SetAutoKill(false);
        //_moveSequence.SetLoops(-1, LoopType.Yoyo);
        

        /*foreach (Transform point in _points)
        {
            float timeToMove = CalcTime(Vector3.Distance(transform.position, point.position));
            _moveSequence.Append(transform.DOMove(point.position, timeToMove).SetEase(Ease.OutQuad));
        }*/
    }

    private void FixedUpdate() {
        MovementMathFunction();
    }

    private void MovementMathFunction(){
        float zPos = 0;

        switch (typeMovement){
            case 0:
                zPos = 0;
                xPos = 0;
            break;
            case 1:
                zPos = (float)Math.Pow(xPos,2);
                xPos+=increaseX;
            break;
            case 2:
                zPos = (float)Math.Sin(xPos);
                xPos+=increaseX;
            break;
            case 3:
                zPos = (float)Math.Pow(xPos,3);
                xPos+=increaseX;
            break;
            case 4:
                zPos = (float)Math.Sqrt(xPos);
                xPos+=increaseX;
            break;
            case 5:
                zPos = (float)Math.Cos(xPos);
                xPos+=increaseX;
            break;
            case 6:
                zPos = (float)Math.Exp(xPos);
                xPos+=increaseX;
            break;
        }

        transform.DOMoveZ(zPos,0.00001f);
        transform.DOMoveX(xPos,0.00001f);
        
    }

    private float CalcTime (float distance)
    {
        return (distance / _speed);
    }
}
