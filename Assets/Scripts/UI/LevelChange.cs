using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

namespace UI {
	public class LevelChange : MonoBehaviour {
		public Enums.LevelEnum ButtonAction;
		
		private Button _btn;
		
		private void Start () {
			_btn = GetComponent<Button>();

			switch (ButtonAction) {
				case Enums.LevelEnum.NextLevel:
					break;
				case Enums.LevelEnum.LastLevel:
					break;
				case Enums.LevelEnum.RestartLevel:
					_btn.onClick.AddListener(RestartLevel);
					break;
				case Enums.LevelEnum.Home:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public void RestartLevel () {
			SceneManager.LoadSceneAsync("Test Level");
		}
	}
}
