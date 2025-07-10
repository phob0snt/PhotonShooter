using UnityEngine;

namespace ShooterGame.UI.Elements
{
    public abstract class UIElement : MonoBehaviour
    {
        public virtual void Initialize() { }
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}