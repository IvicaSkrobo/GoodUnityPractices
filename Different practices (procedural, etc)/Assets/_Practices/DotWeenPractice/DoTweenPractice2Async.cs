using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DoTweenPractice2Async : MonoBehaviour
{
    [SerializeField] Transform[] shapes;
    void Start()
    {
        //hard to read, disasta
        /* shapes[0].DOMoveX(10, Random.Range(1, 2f)).OnComplete(
         () => shapes[1].DOMoveX(10, Random.Range(1, 2f)).OnComplete(() => shapes[2].DOMoveX(10, Random.Range(1, 2f)).OnComplete(
             () =>
             {
                 foreach (var shape in shapes)
                 {
                     shape.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce);
                 }
             }
             )));
        */
        //Sequence a better way but async way better
        /*
        var sequence = DOTween.Sequence();

        foreach (var shape in shapes)
        {
            sequence.Append(shape.DOMoveX(10, Random.Range(1, 2f)));
        }
        sequence.OnComplete(() =>
        {
            foreach (var shape in shapes)
            {
                shape.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce);
            }

        });
     */

       // AsyncBabyLetsGoo();
        Tasks();
    }

    //way to do asyncs
    async void AsyncBabyLetsGoo()
    {
        foreach (var shape in shapes)
        {
            await shape.DOMoveX(10, Random.Range(1, 2f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        }
    }

    // and way to sequence them better with tasks
    async void Tasks()
    {
        var tasks = new List<Task>();

        foreach (var shape in shapes)
        {
            tasks.Add( shape.DOMoveX(10, Random.Range(1, 2f)).SetEase(Ease.InOutQuad).AsyncWaitForCompletion());
        }

        await Task.WhenAll(tasks);

        foreach (var shape in shapes)
        {
            shape.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce);
        }

    }


}
