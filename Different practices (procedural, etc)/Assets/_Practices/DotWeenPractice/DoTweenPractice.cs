using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenPractice : MonoBehaviour
{

    [SerializeField] private Transform innerShape, outerShape;
    [SerializeField] private float cycleLenght =2;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(6.6f, 0,0f), cycleLenght).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);

        outerShape.DORotate(new Vector3(0, 360, 360), cycleLenght, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);

        innerShape.DOScale(new Vector3(2,2, 2), cycleLenght*0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
