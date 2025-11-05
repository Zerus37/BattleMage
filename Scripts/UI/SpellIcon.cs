using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SpellIcon : ActionIcon, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _manaCostText;
	private SpellSO _so;
	private int _altVariant = -1;

	private Vector2 _pointerOffset = Vector2.zero;
	private Vector2 _dragStartPosition = Vector2.zero;
	private Transform _dragStartParent = null;
	private RectTransform _rectTransform;
	private PlayerAction _myPlayerAction;

	public PlayerAction PlayerAction => _myPlayerAction;
	public SpellSO SO => _so;

	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
	}

	public void SetPlayerAction(PlayerAction _playerAction)
	{
		_myPlayerAction = _playerAction;
	}

	public void SetSO(SpellSO so)
	{
		_so = so;
		_icon.sprite = so.sprite;
		_manaCostText.text = so.manaCost.ToString();
		_keyKodeText.text = "";
	}

	public void SetSO(SpellSO so, int variant)
	{
		SetSO(so);
		_altVariant = variant;
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		KeyBindingSystem.SelectAction(this);

		if(_altVariant < 0)
			SpellInfoPopup.Show(_so, eventData.position);
		else
			SpellInfoPopup.Show(_so, eventData.position, _altVariant);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		KeyBindingSystem.SelectAction(null);
		SpellInfoPopup.Hide();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_dragStartPosition = _rectTransform.anchoredPosition;
		_dragStartParent = transform.parent;

		transform.parent = MainCanvas.Transform;
		_pointerOffset = eventData.position - _rectTransform.anchoredPosition;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_rectTransform.anchoredPosition = _dragStartPosition;
		transform.parent = _dragStartParent;
		KeyBindingSystem.TrySetSlot(this);
	}

	public void OnDrag(PointerEventData eventData)
	{
		_rectTransform.anchoredPosition = eventData.position - _pointerOffset;
	}
}