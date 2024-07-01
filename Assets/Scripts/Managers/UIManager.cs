using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public MainMenu _MainMenu;
    public AttackPoolMenu _AttackPoolMenu;
    public ComboPanel _ComboPanel;
    public InventoryPanel _InventoryPanel;
    public EnemyStatusPanel _EnemyStatusPanel;
    public DialogueWindow _DialogueMenu;
    public KeySuggestionMenu _KeySuggestionMenu;
    public DarknessPanel _DarknessPanel;
    
    public static UIManager Instance;

    private void Start()
    {
        _MainMenu = MainMenu.Instance;
        _AttackPoolMenu = AttackPoolMenu.Instance;
        _ComboPanel = ComboPanel.Instance;
        _InventoryPanel = InventoryPanel.Instance;
        _DialogueMenu = DialogueWindow.Instance;
        _KeySuggestionMenu = KeySuggestionMenu.Instance;

        _DarknessPanel.gameObject.SetActive(false);


        _KeySuggestionMenu.gameObject.SetActive(false);
        _DialogueMenu.gameObject.SetActive(false);
        _MainMenu.gameObject.SetActive(false);
        _AttackPoolMenu.gameObject.SetActive(false);
        //_ComboPanel.gameObject.SetActive(false);
        _InventoryPanel.gameObject.SetActive(false);
        _EnemyStatusPanel.gameObject.SetActive(false);
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
