using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MagicPoolMenu : MonoBehaviour
{
    public MagicUiItem MagicItem;

    public TMP_Text NameTextField;
    public TMP_Text TypeTextField;
    public TMP_Text SpellFormTextField;

    public GameObject MagicPoolItem;
    public Transform Content;

    public MagicItem _AttackItem;

    public Button CloseButton;

    public Transform DetailInfoPanel;
    public Button ApplyButton;

    public List<GameObject> allButtons = new List<GameObject>();
    public Button ActualButton;

    public void InitializeMenu()
    {
        List<MagicItem> myScriptableObjects = InventoryManager.Instance.AllMagicSkills.Select(s => s.GetComponent<MagicLootObject>()._MagicItem).ToList();
        //List<AttackItem> myScriptableObjects = GetScriptableObjectsOfType<AttackItem>();

        allButtons.Clear();
        _AttackItem = null;
        DetailInfoPanel.gameObject.SetActive(false);

        foreach (Transform child in Content)
        {
            // Zerstöre das Kind-GameObject
            Destroy(child.gameObject);
        }

        foreach (var s in myScriptableObjects)
        {
            var item = Instantiate(MagicPoolItem, Content);
            allButtons.Add(item);

            item.GetComponent<MagicUiItem>().IsSelected = false;
            item.GetComponent<MagicUiItem>().OutlineGO.SetActive(false);

            item.GetComponentInChildren<TMP_Text>().text = "";
            item.GetComponent<Image>().sprite = s.Image;
            item.GetComponent<Button>().onClick.AddListener(() => ChooseButton(item.GetComponent<Button>(), item, s));
            //item.GetComponent<WorldImage>().m_worldObjects.Add(s.AnimationObject);
        }
    }
    public void ChooseButton(Button button, GameObject item, MagicItem aitem)
    {
        ActualButton = button;
        ActualButton.GetComponent<MagicUiItem>().IsSelected = true;
        ActualButton.GetComponent<MagicUiItem>().OutlineGO.SetActive(true);

        _AttackItem = aitem;
        DetailInfoPanel.gameObject.SetActive(true);

        SetValuesInDetailwindow(aitem);

        //allButtons.Where(b => !item.Equals(b)).ToList().ForEach(b1 => b1.GetComponent<MagicUiItem>().IsSelected = false);
        //allButtons.Where(b => !item.Equals(b)).ToList().ForEach(b1 => b1.GetComponent<MagicUiItem>().OutlineGO.SetActive(false));
    }

    public void SetComboItem()
    {
        SetComboAttackItem(_AttackItem);
    }

    public void SetComboAttackItem(MagicItem item)
    {
        MagicItem.SetAttackItem(item);
        gameObject.SetActive(false);

        UIManager.Instance._MainMenu.gameObject.SetActive(true);
        UIManager.Instance._ComboPanel.gameObject.SetActive(true);



        ComboManager.Instance.Combos.FirstOrDefault().ComboList.Clear();

        foreach (var a in FindObjectOfType<ComboPanel>().AtackItemPanel)
        {
            ComboManager.Instance.Combos.FirstOrDefault().ComboList.Add(a.GetComponent<SkillUIItem>()._AttackItem);
        }

        ComboManager.Instance.Combos.FirstOrDefault().MagicList.Clear();

        foreach (var a in FindObjectOfType<ComboPanel>().MagicItemPanel)
        {
            ComboManager.Instance.Combos.FirstOrDefault().MagicList.Add(a.GetComponent<MagicUiItem>()._MagicItem);
        }
    }  

    private void SetValuesInDetailwindow(MagicItem aitem)
    {
        NameTextField.text = aitem.SpellName;
        SpellFormTextField.text = aitem.Type.ToString();
        TypeTextField.text = aitem.effect.ToString();
        //subscriptionText.text = aitem.Description;
        //damageText.text = aitem.Damage.ToString();
        //speedText.text = aitem.Speed.ToString();

        //if (aitem.StatusEffect == EStatusEffects.Fire)
        //{
        //    effectText.color = Color.red;
        //    effectText.text = aitem.StatusEffect.ToString();
        //}
        //else if (aitem.StatusEffect == EStatusEffects.Ice)
        //{
        //    effectText.color = Color.cyan;
        //    effectText.text = aitem.StatusEffect.ToString();
        //}
        //else if (aitem.StatusEffect == EStatusEffects.None)
        //{
        //    effectText.color = Color.black;
        //    effectText.text = aitem.StatusEffect.ToString();
        //}
    }
}
