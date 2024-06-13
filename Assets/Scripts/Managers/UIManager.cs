using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainMenu _MainMenu;
    public AttackPoolMenu _AttackPoolMenu;
    public ComboPanel _ComboPanel;
    public InventoryPanel _InventoryPanel;
    public EnemyStatusPanel _EnemyStatusPanel;
    
    public static UIManager Instance;

    private void Start()
    {
        _MainMenu = MainMenu.Instance;
        _AttackPoolMenu = AttackPoolMenu.Instance;
        _ComboPanel = ComboPanel.Instance;
        _InventoryPanel = InventoryPanel.Instance;
        _EnemyStatusPanel = EnemyStatusPanel.Instance;

        _MainMenu.gameObject.SetActive(false);
        _AttackPoolMenu.gameObject.SetActive(false);
        //_ComboPanel.gameObject.SetActive(false);
        _InventoryPanel.gameObject.SetActive(false);

    }

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_MainMenu.gameObject.activeSelf)
            {
                _MainMenu.gameObject.SetActive(false);
            }
            else
            {
                _MainMenu.gameObject.SetActive(true);
            }
        }
    }
}
