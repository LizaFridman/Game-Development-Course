
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{
    public TowerButton TowerButtonPressed { get; set; }
    private SpriteRenderer spriteRenderer;
    private List<Tower> _towers = new List<Tower>();
    private List<Collider2D> _buildSites = new List<Collider2D>();
    private Collider2D _buildTile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        //_buildTile = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "BuildSite" && TowerButtonPressed != null)
            {
                /*_buildTile = hit.collider;
                _buildTile.tag = "BuildSiteFull";
                RegistedBuildSite(_buildTile);*/
                hit.collider.tag = "BuildSiteFull";
                RegistedBuildSite(hit.collider);
                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void RegistedBuildSite(Collider2D buildSite) {
        _buildSites.Add(buildSite);
    }

    public void RegisterTower(Tower tower) {
        _towers.Add(tower);
    }

    public void RenameBuildSitesTag() {
        foreach (var buildSite in _buildSites) {
            buildSite.tag = "BuildSite";
        }
        _buildSites.Clear();
    }

    public void DestroyAllTowers() {
        foreach (var tower in _towers) {
            Destroy(tower.gameObject);
        }
        _towers.Clear();
    }

    private void FollowMouse()
    {
        var cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cameraPosition.x, cameraPosition.y);
    }

    public void EnableDragSprite(Sprite sprite) {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisableDragSprite()
    {
        spriteRenderer.enabled = false;
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && TowerButtonPressed != null)
        {
            var newTower = Instantiate(TowerButtonPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            RegisterTower(newTower);
            DisableDragSprite();
        }
    }

    public void SelectedTower(TowerButton towerSelected) {
        if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            TowerButtonPressed = towerSelected;
            BuyTower(towerSelected.TowerPrice);
            EnableDragSprite(towerSelected.DragSprite);
        }
    }

    public void BuyTower(int price) {
        GameManager.Instance.SubtractMoney(price);
    }
}
