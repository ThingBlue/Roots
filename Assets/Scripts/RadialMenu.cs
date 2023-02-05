using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ?? : 2021-04-26 PM 2:47:40
// ??? : Rito

namespace Roots
{
    [DisallowMultipleComponent]
    public partial class RadialMenu : MonoBehaviour
    {
        //[Header("Options")]
        //[Range(2, 16)]
        [SerializeField] private int _pieceCount = 8; // ?? ??

        //[Range(0.2f, 1f)]
        [SerializeField] private float _appearanceDuration = .3f; // ??? ??? ??
        //[Range(0.2f, 1f)]
        [SerializeField] private float _disppearanceDuration = .3f; // ??? ??? ??
        [SerializeField] private float _pieceDist = 180f; // ?????? ? ??? ??

        //[Range(0.01f, 0.5f)]
        [SerializeField] private float _centerRange = 0.1f; // ?? ?? ??? ?? ??

        //[Header("Objects")]
        [SerializeField] private GameObject _pieceSample; // ??? ?? ??????
        [SerializeField] private RectTransform _arrow;    // ??? ???? ?? ????

        // ??? ???
        private Image[] _pieceImages;
        private RectTransform[] _pieceRects;
        private Vector2[] _pieceDirections; // ? ??? ??? ??? ??

        private float _arrowRotationZ;

        //[Header("Debug"), Space(20)]
        [SerializeField]
        private int _selectedIndex = -1;

        private const float NotSelectedPieceAlpha = 0.5f;
        private static readonly Color SelectedPieceColor = new Color(1f, 1f, 1f, 1f);
        private static readonly Color NotSelectedPieceColor = new Color(1f, 1f, 1f, NotSelectedPieceAlpha);

        /***********************************************************************
        *                               Unity Events
        ***********************************************************************/
        #region .

        private void Awake()
        {
            InitPieceImages();
            InitPieceDirections();
            InitStateDicts();

            HideGameObject();
        }

        #endregion
        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
        #region .
        /// <summary> ?? ?? ???? ??? ?? </summary>
        private void InitPieceImages()
        {
            _pieceSample.SetActive(true);

            _pieceImages = new Image[_pieceCount];
            _pieceRects = new RectTransform[_pieceCount];

            for (int i = 0; i < _pieceCount; i++)
            {
                // ?? ??
                var clone = Instantiate(_pieceSample, transform);
                clone.name = $"Piece {i}";

                // Image, RectTransform ??? ??? ???
                _pieceImages[i] = clone.GetComponent<Image>();
                _pieceRects[i] = _pieceImages[i].rectTransform;
            }

            _pieceSample.SetActive(false);
        }

        /// <summary> ?? ????? ??? ? ???? ???? ?? </summary>
        private void InitPieceDirections()
        {
            _pieceDirections = new Vector2[_pieceCount];

            float angle = 360f / _pieceCount;

            for (int i = 0; i < _pieceCount; i++)
            {
                _pieceDirections[i] = new ClockwisePolarCoord(1f, angle * i).ToVector2();
            }
        }

        private void ShowGameObject()
        {
            gameObject.SetActive(true);
        }

        private void HideGameObject()
        {
            gameObject.SetActive(false);
        }

        /// <summary> ??? ???? ??? ?? </summary>
        private void SetPieceAlpha(int index, float alpha)
        {
            _pieceImages[index].color = new Color(1f, 1f, 1f, alpha);
        }

        /// <summary> ??? ???? ??????? ?? ?? </summary>
        private void SetPieceDistance(int index, float distance)
        {
            _pieceRects[index].anchoredPosition = _pieceDirections[index] * distance;
        }

        /// <summary> ?? ???? ?? ?? ?? </summary>
        private void SetPieceScale(int index, float scale)
        {
            _pieceRects[index].localScale = new Vector3(scale, scale, 1f);
        }

        /// <summary> ?? ??? ?????? ?? ???? ?? </summary>
        private void SetAllPieceDistance(float distance)
        {
            for (int i = 0; i < _pieceCount; i++)
            {
                _pieceRects[i].anchoredPosition = _pieceDirections[i] * distance;
            }
        }

        /// <summary> ?? ?? ???? ??? ?? </summary>
        private void SetAllPieceAlpha(float alpha)
        {
            for (int i = 0; i < _pieceCount; i++)
            {
                _pieceImages[i].color = new Color(1f, 1f, 1f, alpha);
            }
        }

        /// <summary> ?? ??? ?? ?? </summary>
        private void SetAllPieceScale(float scale)
        {
            for (int i = 0; i < _pieceCount; i++)
            {
                _pieceRects[i].localScale = new Vector3(scale, scale, 1f);
            }
        }

        private void SetAllPieceImageEnabled(bool enabled)
        {
            for (int i = 0; i < _pieceCount; i++)
            {
                _pieceImages[i].enabled = enabled;
            }
        }

        /// <summary> ??? ??? ?????? ??? ??, ?? ?? </summary>
        private void SetArrow(bool show)
        {
            _arrow.gameObject.SetActive(show);

            if (show)
            {
                _arrow.eulerAngles = Vector3.forward * _arrowRotationZ;
            }
        }
        #endregion
        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
        #region .

        /// <summary> ?? </summary>
        public void Show()
        {
            ForceToEnterAppearanceState();
        }

        /// <summary> ????? ??? ?? </summary>
        public int Hide()
        {
            ForceToEnterDisappearanceState();
            SetArrow(false);

            return _selectedIndex;
        }

        /// <summary> ?? ?? ???(?????) ?? </summary>
        public void SetPieceImageSprites(Sprite[] sprites)
        {
            int i = 0;
            int len = sprites.Length;
            for (; i < _pieceCount && i < len; i++)
            {
                if (sprites[i] != null)
                {
                    _pieceImages[i].sprite = sprites[i];
                }
            }
        }

        #endregion
    }
}