using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Common
{
	public class ImageEffects : MonoBehaviour
	{
		public AnimationEffect effect;
		public enum AnimationEffect
		{
			ScaleIn,
			ScaleOut,
			SlideFromLeft,
			SlideFromRight,
			SlideFromBottom,
			SlideFromTop,
			FadeIn,
			FadeOut
		}
		public Vector3 endPosition;
		private float TIME_DURATION = 0.4f;
		
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

		private void ScaleIn()
		{
			transform.DOScale(1, TIME_DURATION * 2);
		}
		
		private void SlideByDirection()
		{
			transform.DOMove(endPosition, TIME_DURATION);
		}
	}
}