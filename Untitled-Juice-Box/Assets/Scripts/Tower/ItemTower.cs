using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTower : ItemBase
{

    public enum TowerType {Standard, Support }
    TowerType ThisTowerType;

    public enum TowerDirection {North,NE,East,SE,South,SW,West,NW}

    public TowerDirection[] m_TwoDirections = new TowerDirection[2];

    [SerializeField] GameObject TowerBase;
    [SerializeField] GameObject LargeSprite;
    // Start is called before the first frame update
    void Start()
    {
        m_TwoDirections[0] = (ItemTower.TowerDirection)Random.Range(0, System.Enum.GetValues(typeof(ItemTower.TowerDirection)).Length);
        m_TwoDirections[1] = (ItemTower.TowerDirection)Random.Range(0, System.Enum.GetValues(typeof(ItemTower.TowerDirection)).Length);
        while (m_TwoDirections[0] == m_TwoDirections[1])
        {
            m_TwoDirections[0] = (ItemTower.TowerDirection)Random.Range(0, System.Enum.GetValues(typeof(ItemTower.TowerDirection)).Length);
        }
    }
    public override void OnPickUp()
    {
        base.OnPickUp();
       
    }

    public override void OnPutDown()
    {
        base.OnPutDown();
        RaycastHit hitOne;
        if (Physics.Raycast((transform.position + new Vector3(DirectionConvert(m_TwoDirections[0]).x, 2, DirectionConvert(m_TwoDirections[0]).y)), Vector3.down, out hitOne, 5f))
        {
            if(hitOne.collider.gameObject.GetComponent<MapTile>() != null)
            {
                hitOne.collider.gameObject.GetComponent<MapTile>().UpdateTileStatus(MapTile.TileStatus.Brush);
            }
        }
        RaycastHit hitTwo;
        if (Physics.Raycast((transform.position + new Vector3(DirectionConvert(m_TwoDirections[1]).x, 2, DirectionConvert(m_TwoDirections[1]).y)), Vector3.down, out hitTwo, 5f))
        {
            if (hitTwo.collider.gameObject.GetComponent<MapTile>() != null)
            {
                hitTwo.collider.gameObject.GetComponent<MapTile>().UpdateTileStatus(MapTile.TileStatus.Brush);
            }
        }

        GameObject SpawnedTower = Instantiate(TowerBase, this.transform.parent);
        SpawnedTower.transform.localPosition = Vector3.zero;

        Destroy(this.gameObject);
        //Create Tower and destroy yourself
    }
    public static Vector2Int DirectionConvert(TowerDirection DirectionToConvert)
    {
        switch (DirectionToConvert)
        {
            case TowerDirection.North:
                return Vector2Int.up;
            case TowerDirection.NE:
                return Vector2Int.up + Vector2Int.right;
            case TowerDirection.East:
                return Vector2Int.right;
            case TowerDirection.SE:
                return Vector2Int.down + Vector2Int.right;
            case TowerDirection.South:
                return Vector2Int.down;
            case TowerDirection.SW:
                return Vector2Int.down + Vector2Int.left;
            case TowerDirection.West:
                return Vector2Int.left;
            case TowerDirection.NW:
                return Vector2Int.up + Vector2Int.left;
            default:
                return Vector2Int.zero;
        }
    }

    public void PreviewPlacement(Vector3 PreviewPosition)
    {
        transform.position = PreviewPosition;
        LargeSprite.SetActive(true);
    }

    public void DisablePreview()
    {
        LargeSprite.SetActive(false);
    }
}
