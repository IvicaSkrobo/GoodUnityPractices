using DG.Tweening;
using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager2048 : MonoBehaviour
{
    [SerializeField]
    int width;
    [SerializeField]
    int height;

    [SerializeField]
    Node2048 nodePrefab;
    [SerializeField]
    SpriteRenderer boardPrefab;
    [SerializeField]
    Block2048 blockPrefab;
    [SerializeField]
    private List<BlockType> blockTypes;
    [SerializeField]
    float travelTime = 2f;
    [SerializeField]
    private int _winCondition = 2048;
  
    private int round = 0;


    List<Node2048> nodeList;
    List<Block2048> blocksList;
    public GameState2048 GameState2048 {get; private set; }
    BlockType GetBlockTypeByValue(int value) => blockTypes.First(x => x.value == value);

    void Start()
    {
        ChangeState(GameState2048.GenerateLevel);
    }

    private void ChangeState(GameState2048 newState)
    {
        GameState2048 = newState;

        switch (newState)
        {
            case GameState2048.GenerateLevel:
                GenerateGrid();
                break;
            case GameState2048.SpawningBlocks:
                SpawnBlocks(round++==0 ? 2:1);
                break;
            case GameState2048.WaitingInput:
                break;
            case GameState2048.Moving:
                break;
            case GameState2048.Win:
                break;
            case GameState2048.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }


    public void OnMove(InputValue value)
    {

        if (GameState2048 != GameState2048.WaitingInput) return;
        var movement = value.Get<Vector2>();

        if (movement.x < 0)
        {
            Shift(Vector2.left);
        }
        else if(movement.x>0)
        {
            Shift(Vector2.right);
        }
        if (movement.y < 0)
        {
            Shift(Vector2.down);
        }
        else if (movement.y > 0)
        {
            Shift(Vector2.up);
        }
    }




    private void GenerateGrid()
    {
        round = 0;
        nodeList = new List<Node2048>();
        blocksList = new List<Block2048>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                nodeList.Add(Instantiate(nodePrefab, new Vector2(i, j), Quaternion.identity));
            }
        }
        var center = new Vector2(width / 2 - 0.5f, height / 2 - 0.5f);

       var board = Instantiate(boardPrefab, center, Quaternion.identity);

        board.size = new Vector2(width, height);
        Helpers.Camera.transform.position = new Vector3(center.x, center.y, -10);

        ChangeState(GameState2048.SpawningBlocks);
    }


    private void SpawnBlocks(int amount)
    {
        //find the free nodes
        var freeNodes = nodeList.Where(n => n.occupiedBlock == null).OrderBy(x => UnityEngine.Random.value);

        //take takes first amount of elements from the list
        foreach (var node in freeNodes.Take(amount))
        {
            SpawnBlock(node, UnityEngine.Random.value>0.8f ? 4:2);


        }

        if(freeNodes.Count()==1)
        {
            ChangeState(GameState2048.Lose);
            // Game lost
            return;
        }

        ChangeState(blocksList.Any(b=>b.value ==_winCondition)? GameState2048.Win : GameState2048.WaitingInput);
    }

    void SpawnBlock(Node2048 node, int value)
    {
        var block = Instantiate(blockPrefab, node.pos, Quaternion.identity);
        block.Init(GetBlockTypeByValue(value));
        block.SetBlock(node);
        blocksList.Add(block);
    }



    void Shift(Vector2 dir)
    {
        ChangeState(GameState2048.Moving);

        //this ascends x first and then y for all blocked nodes
        var orderedBlocks = blocksList.OrderBy(b => b.pos.x).ThenBy(b => b.pos.y);

        if (dir == Vector2.right || dir == Vector2.up) orderedBlocks.Reverse();

        foreach (var block in orderedBlocks)
        {
            var next = block.node;  
            do
            {
                //we set the block to next, if next==block.node we couldnt find the next free node
                //and we exit this loop
                block.SetBlock(next);

                //check for the next possible node in dir
                var possibleNode = GetNodeAtPos(next.pos+dir); 
                
                if(possibleNode!=null)
                {
                    //node exists
                    // check if its possible to merge
                    if(possibleNode.occupiedBlock!=null && possibleNode.occupiedBlock.CanMerge(block.value))
                    {
                        //set merge 
                        block.MergeBlock(possibleNode.occupiedBlock);
                    }
                    //otherwise check if we can move to the next block 
                    else if (possibleNode.occupiedBlock==null)
                    {
                        //if it isnt occupied we set the next to it, and thererfore while resets
                        next = possibleNode;
                    }
                    // none hit end while loop
                }
            } while (next != block.node);


        }

        var sequence = DOTween.Sequence();

        //where to move
        // if its a mergingblock move to a merging pos
        // if not move to a new node.pos
        foreach(var block in orderedBlocks)
        {
            var movePoint = block.mergingBlock != null ? block.mergingBlock.node.pos : block.node.pos;
        
            sequence.Insert(0, block.transform.DOMove(movePoint, travelTime));
        
        }

        sequence.OnComplete(() =>
        {
            foreach (var block in orderedBlocks.Where(b => b.mergingBlock != null))
            {
                MergeBlocks(block.mergingBlock, block);
            }
            ChangeState(GameState2048.SpawningBlocks);
        });
    }

    void MergeBlocks(Block2048 baseBlock2048, Block2048 mergingBlock)
    {
        SpawnBlock(baseBlock2048.node, baseBlock2048.value * 2);

        RemoveBlock(baseBlock2048);
        RemoveBlock(mergingBlock);
        
    }

    void RemoveBlock(Block2048 block)
    {
        blocksList.Remove(block);
        Destroy(block.gameObject);
    }

    Node2048 GetNodeAtPos(Vector2 pos)
    {
        return nodeList.FirstOrDefault(n => n.pos == pos);
    }
}


[Serializable]
public struct BlockType
{
    public int value;
    public Color color;
}

public enum GameState2048
{
    GenerateLevel =0,
    SpawningBlocks=1,
    WaitingInput=2,
    Moving=3,
    Win=4,
    Lose=5

}

