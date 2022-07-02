using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum TypeMovement
{
    NOTHING, 
    QUADRATIC, 
    SINE, 
    CUBIC, 
    ROOT, 
    COSSINE, 
    EXPONENTIAL
}

public class TweenerControllerDT : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    [SerializeField] private float _speed = 2f;

    [SerializeField] private float xPos;

    [SerializeField] private TypeMovement tm;

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

        switch (tm){
            case TypeMovement.NOTHING:
                zPos = 0;
                xPos = 0;
            break;
            case TypeMovement.QUADRATIC:
                zPos = (float)Math.Pow(xPos,2);
                xPos+=increaseX;
            break;
            case TypeMovement.SINE:
                zPos = (float)Math.Sin(xPos);
                xPos+=increaseX;
            break;
            case TypeMovement.CUBIC:
                zPos = (float)Math.Pow(xPos,3);
                xPos+=increaseX;
            break;
            case TypeMovement.ROOT:
                zPos = (float)Math.Sqrt(xPos);
                xPos+=increaseX;
            break;
            case TypeMovement.COSSINE:
                zPos = (float)Math.Cos(xPos);
                xPos+=increaseX;
            break;
            case TypeMovement.EXPONENTIAL:
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
