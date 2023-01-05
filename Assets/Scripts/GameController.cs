using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // 1. 색상을 배열로 등록해두고 사용하거나, 2. Random.ColorHSV의 메소드로 임의의 색상 사용(Test)
    [SerializeField]
    private Color[]             colorPalette;       // 색상 목록
    [SerializeField]
    private float               colorDifference;    // 색상의 차이 정도 (수치가 높을수록 쉬움)
    [SerializeField][Range(2,5)]
    private int                 blockCount = 2;     // 블록 개수
    [SerializeField]
    private BlockSpawner        blockSpawner;

    // 생성한 블록의 정보를 갖고 있는 리스트
    private List<Block>         blockList = new List<Block>();

    private Color               currentColor;       // 현재 블록들의 색상
    private Color               otherOneColor;      // 특정 블록에 적용될 다른 색
    private int                 otherBlockIndex;    // 다른 색상이 될 한 블록의 인덱스

    private int                 gameScore = 0;


    private void Awake()
    {
        blockList = blockSpawner.SpawnBlocks(blockCount);
        for ( int i = 0; i < blockList.Count; ++i){
            blockList[i].Setup(this);
        }

        SetColor();

    }

#region Test
    // Test
    private void Start(){
        SetColor();
        //Debug.Log("Game Start");
    }
    // Test
    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            SetColor();
            //Debug.Log("Color Change");
        }
    }
#endregion Test

#region SetColor
/*
    private void RandomColor(){
        Color color = Random.ColorHSV();

        for (int i = 0; i < blockList.Count; ++i){
            blockList[i].Color = color;
        }
    }
*/
    private void SetColor(){ 
        // 블록의 색이 바뀔때 마다 정답 블록의 색상이 다른 블록들과 더 비슷한 색상으로 보이도록 값 감소
        colorDifference *= 0.92f;

        // 기본 블록들의 색상
        currentColor = colorPalette[ Random.Range(0, colorPalette.Length) ];

        //정답 블록의 색상 (RGB값의 차이)
        float diff = (1.0f / 255.0f) * colorDifference;
        otherOneColor = new Color(currentColor.r - diff, currentColor.g - diff, currentColor.b - diff);

        // 정답 블록의 순번
        otherBlockIndex = Random.Range(0, blockList.Count);
        //Debug.Log(otherBlockIndex);     // 정답 블록의 인덱스를 디버깅

        // 정답 블록과 나머지 블록들의 색상 설정 
        for ( int i = 0; i < blockList.Count; ++i){
            if(i == otherBlockIndex){
                blockList[i].Color = otherOneColor;
            }
            else{
                blockList[i].Color = currentColor;
            }
            //blockList[i].Color = currentColor;
        }
    }
#endregion SetColor

    public void  CheckBlock(Color color){
        // 색상이 다른 하나의 블록과 매개변수 color의 색상이 같으면
        // 플레이어가 선택한 블록이 정답 블록 = 정답처리
        if(blockList[otherBlockIndex].Color == color){
            //색상 재 선택
            SetColor();
            gameScore += 1;
            Debug.Log("색상 일치, 현재 점수 : "+ gameScore);
        }
        else{
            Debug.Log("색상 불일치, 실패");
            UnityEditor.EditorApplication.ExitPlaymode();
        }
        
    }

}