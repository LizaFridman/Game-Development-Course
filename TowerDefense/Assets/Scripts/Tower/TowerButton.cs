using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private Tower towerObject;
    [SerializeField]
    private Sprite drageSprite;
    [SerializeField]
    private int _towerPrice;

    public int TowerPrice
    {
        get { return _towerPrice; }
    }

    public Tower TowerObject
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
