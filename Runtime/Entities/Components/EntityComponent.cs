using UnityEngine;

namespace EntiCS.Entities.Components
{
    public class EntityComponent : IEntityComponent
    {
        // This is picked up by Unity's Editor UI automatically,
        // to show for example in lists instead of "Element 0"
        [SerializeField, HideInInspector] private string _name = nameof(EntityComponent);

#if UNITY_EDITOR
        public void UpdateEditorName()
        {
            _name = GetType().Name;
        }
#endif
    }
}
