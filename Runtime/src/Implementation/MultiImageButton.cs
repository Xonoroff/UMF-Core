using UnityEngine;

namespace UMF.Core.Implementation
{
    public class MultiImageButton : Button
    {
        private Graphic[] m_graphics;

        protected Graphic[] Graphics
        {
            get
            {
                if (m_graphics == null) m_graphics = targetGraphic.transform.parent.GetComponentsInChildren<Graphic>();

                return m_graphics;
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            Color color;
            switch (state)
            {
                case Selectable.SelectionState.Normal:
                    color = this.colors.normalColor;
                    break;
                case Selectable.SelectionState.Highlighted:
                    color = this.colors.highlightedColor;
                    break;
                case Selectable.SelectionState.Pressed:
                    color = this.colors.pressedColor;
                    break;
                case Selectable.SelectionState.Disabled:
                    color = this.colors.disabledColor;
                    break;
                case Selectable.SelectionState.Selected:
                    color = this.colors.selectedColor;
                    break;
                default:
                    color = Color.black;
                    break;
            }

            if (base.gameObject.activeInHierarchy)
                switch (this.transition)
                {
                    case Selectable.Transition.ColorTint:
                        ColorTween(color * this.colors.colorMultiplier, instant);
                        break;
                    default:
                        throw new NotSupportedException();
                }
        }

        private void ColorTween(Color targetColor, bool instant)
        {
            if (this.targetGraphic == null) return;

            foreach (Graphic g in Graphics)
                g.CrossFadeColor(targetColor, !instant ? this.colors.fadeDuration : 0f, true, true);
        }
    }
}