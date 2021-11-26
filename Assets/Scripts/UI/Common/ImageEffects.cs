using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Common
{
	public class ImageEffects : MonoBehaviour
	{
		public Canvas parentCanvas;
		public AnimationEffect effect;
		public enum AnimationEffect
		{
			ScaleIn,
			SlideFromLeft,
			SlideFromRight,
			SlideFromBottom,
			SlideFromTop,
		}
		private Vector3 endPosition;
		public float timeDuration;
		private RectTransform rt;

		private void Start()
		{
			rt = GetComponent<RectTransform>();
			endPosition = transform.position;
			MoveToStartPosition();
		}

		public void ActiveEffect()
		{
			switch (effect)
			{
				case AnimationEffect.ScaleIn:
					ScaleIn();
					break;
				case AnimationEffect.SlideFromLeft:
				case AnimationEffect.SlideFromRight:
				case AnimationEffect.SlideFromTop:
				case AnimationEffect.SlideFromBottom:
					SlideByDirection();
					break;
			}
		}

		private void MoveToStartPosition()
		{
			float canvasWidth = parentCanvas.GetComponent<RectTransform>().rect.width;
			float canvasHeight = parentCanvas.GetComponent<RectTransform>().rect.height;
			Vector3 originPos = rt.localPosition;
			switch (effect)
			{
				case AnimationEffect.SlideFromLeft:
					transform.localPosition = new Vector2(originPos.x - canvasWidth, originPos.y);
					break;
				case AnimationEffect.SlideFromRight:
					transform.localPosition = new Vector2(originPos.x + canvasWidth, originPos.y);
					break;
				case AnimationEffect.SlideFromBottom:
					transform.localPosition = new Vector2(originPos.x, originPos.y - canvasHeight);
					break;
				case AnimationEffect.SlideFromTop:
					transform.localPosition = new Vector2(originPos.x, originPos.y + canvasHeight);
					break;
			}
		}

		private void ScaleIn()
		{
			transform.DOScale(1, timeDuration);
		}
		
		private void SlideByDirection()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
			transform.DOMove(endPosition, timeDuration);
		}
	}
}