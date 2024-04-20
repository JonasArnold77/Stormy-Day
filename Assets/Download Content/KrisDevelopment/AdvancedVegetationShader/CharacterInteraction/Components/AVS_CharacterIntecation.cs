using UnityEngine;

namespace AdvancedVegetationShaders
{
	[RequireComponent(typeof(Renderer))]
	[DisallowMultipleComponent]
	public class AVS_CharacterIntecation : MonoBehaviour
	{
		[SerializeField] private Transform character = null;
		[SerializeField] [Tooltip("Character's radius")] private float radius = 0;

		private Material[] materialInstances = new Material[0];
		private static int characterPositionProperty = Shader.PropertyToID("_AVS_CharacterPosition");


		public void SetCharacter(Transform t, float radius)
		{
			character = t;
			this.radius = radius;
		}

		private void OnEnable()
		{
			var _renderer = GetComponent<Renderer>();
			if(_renderer == null)
            {
				return;
            }

			var _sharedMaterials = _renderer.sharedMaterials;
			materialInstances = new Material[_sharedMaterials.Length];

			for(int i = 0; i < _sharedMaterials.Length; i++) {
				materialInstances[i] = new Material(_sharedMaterials[i]);
			}

			_renderer.sharedMaterials = materialInstances;
		}

		private void Update()
		{
			if (character == null) {
				return;
			}

			foreach (var _mat in materialInstances) {
				var _pos = character.position;
				_mat.SetVector(characterPositionProperty, new Vector4(_pos.x, _pos.y, _pos.z, radius));
			}
		}

		private void OnDisable()
		{
			foreach (var _mat in materialInstances) {
				if(_mat != null) {
					Destroy(_mat);
				}
			}

			materialInstances = new Material[0];
		}
	}
}
