using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block2048 : MonoBehaviour
{


    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    TextMeshPro text;
    
    
    public Vector2 pos => transform.position;
    public Node2048 node;
    public Block2048 mergingBlock;
    public bool Merging;
    public int value;

    public void Init(BlockType type)
    {
        value = type.value;
        sprite.color = type.color;
        text.text = type.value.ToString();
    }

    public void SetBlock(Node2048 newNode)
    {
        if (node != null) node.occupiedBlock = null;
        node = newNode;
        node.occupiedBlock = this;
    }
    
    public void MergeBlock(Block2048 blockToMergeWith)
    {
        //set the block we are merging with
        mergingBlock = blockToMergeWith;  //stops other blocks from merging with it
        blockToMergeWith.Merging = true; //stops other blocks from merging with the merging block

        node.occupiedBlock = null; // losing the spot this block used to occupy

    }

    public bool CanMerge(int value) => value == this.value && mergingBlock == null && !Merging;
}
