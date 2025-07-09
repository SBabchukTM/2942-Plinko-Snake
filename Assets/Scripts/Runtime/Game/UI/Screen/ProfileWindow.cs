using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class ProfileWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Image _avatarImage;
        [SerializeField] private Button _changeAvatarButton;
        [SerializeField] private TMP_InputField _nameInputField;
        
        public event Action OnBackPressed;
        public event Action OnSavePressed;
        public event Action OnAvatarChangePressed;
        public event Action<string> OnNameChanged;
        
        public void Setup()
        {
            Sub();

            _changeAvatarButton.onClick.AddListener(() => OnAvatarChangePressed?.Invoke());
            _nameInputField.onEndEdit.AddListener((value) => OnNameChanged?.Invoke(value));
        }

        private void Sub()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _saveButton.onClick.AddListener(() => OnSavePressed?.Invoke());
        }

        public void SetName(string value)
        {
            _nameInputField.text = value;
        }

        public void SetAvatar(Sprite avatar)
        {
            if(avatar!= null)
                _avatarImage.sprite = avatar;
        }
    }
}