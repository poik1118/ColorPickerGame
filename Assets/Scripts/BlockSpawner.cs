using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block               blockPrefab;        // Block Prefab

    [SerializeField]
    private GridLayoutGroup     gridLayout;         // Grid Layout Group


    public List<Block> SpawnBlocks(int blockCount)
    {
        List<Block> blockList = new List<Block> (blockCount * blockCount);

        // 셀 크기
        int cellSize = 300 - 50 * (blockCount - 2);
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
        // 가로에 배치되는 셀 개수
        gridLayout.constraintCount = blockCount;

        // blockCount * blockCount 만큼 블록 생성
        for ( int i = 0; i < blockCount; ++i){
            for ( int j = 0; j < blockCount; ++j){

                Block block = Instantiate(blockPrefab, gridLayout.transform);     // 오브젝트 복제
                blockList.Add(block);
            }
        }

        return blockList;
    }

}