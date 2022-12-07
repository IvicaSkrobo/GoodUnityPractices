using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] List<Sprite> spriteList;
    [SerializeField] PuzzleSlot prefabSlot;
    [SerializeField] DragAndDrop prefabPuzzlePiece;
    [SerializeField]  Transform slotParent;
    [SerializeField]  Transform puzzleParent;

    private void Start()
    {
        Spawn();
    }

    [EditorButton]
    void Spawn()
    {
        var randomSet = spriteList.OrderBy(x => Random.value).Take(3).ToList();

        for (int i = 0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(prefabSlot, slotParent.GetChild(i).position, Quaternion.identity);
            var spawnedPiece = Instantiate(prefabPuzzlePiece, puzzleParent.GetChild(i).position, Quaternion.identity);
            spawnedPiece.name = randomSet[i].name;
            spawnedPiece.SetSprite(randomSet[i], spawnedSlot);

        }

    

    }

}
