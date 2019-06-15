using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerObject;
    [SerializeField]
    private Sprite drageSprite;
    [SerializeField]
    private int _towerPrice;

    public int TowerPrice
    {
        get { return _towerPrice; }
    }

    public GameObject TowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return drageSprite;
        }
    }
}
