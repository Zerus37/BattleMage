using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellInfoPopup : MonoBehaviour
{
    private static SpellInfoPopup instance;

    [SerializeField] private GameObject _popupElement;
    [SerializeField] private TextMeshProUGUI _spellName;
    [SerializeField] private TextMeshProUGUI _spellScool;
    [SerializeField] private TextMeshProUGUI _spellDescription;

    private Dictionary<MagicScool, string> _magicScoolTitiles = 
        new Dictionary<MagicScool, string>
        {
            { MagicScool.baseMagic, "Базовая магия" },
            { MagicScool.death, "смерть" },
            { MagicScool.fire, "огонь" },
            { MagicScool.kinematic, "кинематика" },
            { MagicScool.lightning, "молния" },
            { MagicScool.mindControl, "контроль разума" },
        };
    private Vector2 _halfSize;
    private RectTransform _popupElementTransform;
    private bool _on = false;

    void Start()
    {
        instance = this;
        _popupElement.SetActive(false);
        _popupElementTransform = _popupElement.GetComponent<RectTransform>();
        _halfSize = _popupElementTransform.rect.size / 2 + Vector2.one * 3;
    }

    public static void Show(SpellSO spell, Vector2 position)
	{
        instance._popupElement.SetActive(true);

        instance._popupElementTransform.anchoredPosition = position + instance._halfSize;
        instance._spellName.text = spell.spellName;
        instance._spellScool.text = instance._magicScoolTitiles[spell.scool];
        instance._spellDescription.text = spell.description;
        instance._on = true;
    }

    public static void Hide()
    {
        instance._popupElement.SetActive(false);
        instance._on = false;
    }

	public void Update()
	{
        if (!_on)
            return;

        MovePopupElement();
    }

	private void MovePopupElement()
	{
        instance._popupElementTransform.anchoredPosition = 
            (Vector2)Input.mousePosition + instance._halfSize;
    }
}