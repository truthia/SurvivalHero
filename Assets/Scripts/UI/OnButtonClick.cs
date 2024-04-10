using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
     Button button;
    public float duration = 0.1f;
    public float scaleAmount = 0.8f;
    public string sound="Button";
    public bool mute,temporaryDisable;

    private Vector3 originalScale;
    bool canClick;

    private void Start()
    {
        button = GetComponent<Button>();
        // Lưu kích thước gốc của button
        originalScale = button.transform.localScale;

        // Đăng ký phương thức OnButtonClick() cho sự kiện OnClick của button
        button.onClick.AddListener(OnClick);
        canClick = true;
    }

    private void OnClick()
    {

        if (canClick)
        {
            // Tạo một sequence để xử lý hiệu ứng lún xuống và trở lại
            Sequence sequence = DOTween.Sequence();

            // Lún xuống
            sequence.Append(button.transform.DOScale(originalScale * scaleAmount, duration));

            // Trở lại kích thước gốc
            sequence.Append(button.transform.DOScale(originalScale, duration));
        }

        /* if (!mute)
             SoundManager.instance?.PlaySound(sound);*/
        if (temporaryDisable&&canClick)
        {
            StartCoroutine(DisableCor());
        }
    }
    IEnumerator DisableCor()
    {
        canClick = false;
        yield return new WaitForSeconds(0.5f);
        canClick = true;
    }
}
