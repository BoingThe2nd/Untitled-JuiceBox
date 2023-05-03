using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapTile : MonoBehaviour
{

    public enum TileStatus {Clear, Obstructed, Brush }

    public TileStatus m_Status;

    [SerializeField]
    GameObject HighlightZone;

    public Collider ObstructionCollision;
    public NavMeshObstacle BrushCollision;
    public SpriteRenderer BrushSprite;

    private void Start()
    {
        UpdateTileStatus(m_Status);
    }

    public void HighlightTile()
    {
        HighlightZone.SetActive(true);
        
    }

    public bool CheckTile()
    {

        switch (m_Status)
        {
            case TileStatus.Clear:
                return true;
            case TileStatus.Obstructed:
                return false;
            case TileStatus.Brush:
                return false;
            default:
                return false;
        }
    }

    public void UpdateTileStatus(TileStatus newStatus)
    {
        switch (newStatus)
        {
            case TileStatus.Clear:
                ObstructionCollision.enabled = false;
                BrushCollision.enabled = false;
                BrushSprite.enabled = false;
                break;
            case TileStatus.Obstructed:
                ObstructionCollision.enabled = true;
                BrushCollision.enabled = true;
                BrushSprite.enabled = false;
                break;
            case TileStatus.Brush:
                ObstructionCollision.enabled = false;
                BrushCollision.enabled = true;
                BrushSprite.enabled = true;
                break;
            default:
                break;
        }
        m_Status = newStatus;
    }


    public void UnHighlightTile()
    {
        HighlightZone.SetActive(false);
    }

    public void OnHitEnter()
    {
        HighlightTile();
    }
    public void OnHitExit()
    {
        UnHighlightTile();
    }

}
