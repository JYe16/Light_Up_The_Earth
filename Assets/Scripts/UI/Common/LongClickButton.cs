using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player
{
	public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private bool pointerDown;
		private float timer;
		[SerializeField] private Image fillImage;

		public UnityEvent onLongClick;
		public float requiredHoldTime;
		public bool isActive = false;
		
		public void OnPointerDown(PointerEventData eventData)
		{
			pointerDown = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Reset();
		}

		private void Reset()
		{
			pointerDown = false;
			timer = 0;
			fillImage.fillAmount = timer / requiredHoldTime;
		}

		private void Update()
		{
			if (isActive && pointerDown)
			{
				timer += Time.deltaTime;
				if (timer > requiredHoldTime)
				{
					if (onLongClick != null)
					{
						onLongClick.Invoke();
					}
					Reset();
				}
				fillImage.fillAmount = timer / requiredHoldTime;
			}
		}
	}
}