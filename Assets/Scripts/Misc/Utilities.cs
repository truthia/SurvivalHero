using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.AI;
using DG.Tweening;

public static class Utilities
{
    public static List<T> Shuffle<T>(List<T> list)
    {
        List<T> shuffledList = new List<T>(list);

        int n = shuffledList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }

        return shuffledList;
    }
    public static List<T> PushToTop<T>(List<T> list, T element)
    {
        list.Insert(0, element);
        list.RemoveAt(list.Count - 1);

        return list;
    }
    public static string ConvertToMMSS(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        return timeString;
    }
    public static string ConvertToKMB(float number)
    {
        string unit = "";
        float divisor = 1;
        number = Mathf.Ceil(number);
        // Chuyển đổi số về dạng k, m, B
        if (number >= 1000000000) // Tỷ (Billion)
        {
            unit = "B";
            divisor = 1000000000;
        }
        else if (number >= 1000000) // Triệu (Million)
        {
            unit = "M";
            divisor = 1000000;
        }
        else if (number >= 1000) // Nghìn (Thousand)
        {
            unit = "k";
            divisor = 1000;
        }
        else
        {
            return number.ToString(); // Trả về nguyên gốc nếu không cần chuyển đổi
        }

        // Làm tròn đến số thập phân 1
        float roundedNumber = (float)Math.Round(number / divisor, 1);

        // Chuyển đổi kết quả thành chuỗi và thêm đơn vị
        return roundedNumber.ToString("F1") + unit;
    }

    public static IEnumerator TransitionCoroutine(TextMeshProUGUI text, float startNumber, float endNumber, float transitionDuration)
    {
        float transitionTimer = 0f;
        float currentValue;

        while (transitionTimer < transitionDuration)
        {
            // Tính toán giá trị hiện tại dựa trên thời gian trôi qua và thời gian chuyển đổi
            currentValue = Mathf.Lerp(startNumber, endNumber, transitionTimer / transitionDuration);


            // Cập nhật thời gian chuyển đổi
            transitionTimer += Time.deltaTime;

            yield return null;
        }

        // Kết thúc chuyển đổi
        currentValue = endNumber;
        text.text = Mathf.FloorToInt(currentValue * 100) + "%";
    }
    public static string ConvertSecondsToTimeString(float totalSeconds)
    {
        int hours = (int)totalSeconds / 3600;
        int minutes = ((int)totalSeconds % 3600) / 60;
        int seconds = (int)totalSeconds % 60;

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        return timeString;
    }
    public static Vector3 ChooseRandomPos(Transform center, float radius, LayerMask mask)
    {
        Vector3 randomPoint = center.position + Random.insideUnitSphere * radius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, radius, mask)) //
        {
            return hit.position;

        }

        return Vector3.zero;
    }
    public static void ParabolicMovement(GameObject go, Vector3 targetPosition, float duration, float height, TweenCallback OnComplete)
    {
        Vector3[] path = new Vector3[3];
        path[0] = go.transform.position;
        path[1] = (targetPosition + go.transform.position) / 2 + new Vector3(0, 2 * height, 0);
        path[2] = targetPosition;

        go.transform.DOPath(path, duration, PathType.CatmullRom, PathMode.Full3D, 10, Color.red)
            .SetEase(Ease.OutQuad)
            .OnComplete(OnComplete);
    }
    public static Vector3 GetInclinedVector(this Vector3 inputVector, float angle, Vector3 axis)
    {
        // Chuyển đổi góc từ độ sang radian
        float radianAngle = angle * Mathf.Deg2Rad;

        // Tạo một Quaternion để biểu diễn quay quanh trục được chỉ định
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        // Quay vector ban đầu
        Vector3 inclinedVector = rotation * inputVector;

        // Trả về vector mới
        return inclinedVector;
    }
    public static void SetGameLayerRecursive(GameObject _go, int _layer)
    {
        _go.layer = _layer;
        foreach (Transform child in _go.transform)
        {
            child.gameObject.layer = _layer;

            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
                SetGameLayerRecursive(child.gameObject, _layer);

        }
    }
    public static IEnumerator SpamAction(Action _action, int time, float delay)
    {
        for(int i = 0; i < time; i++)
        {
            _action();
            yield return new WaitForSeconds(delay);
        }
    }
    public static Vector2 GetWorldToScreenPos(Vector3 pos)
    {
        return RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
    }
    public static bool CompareString(string string1,string string2)
    {
        return String.Equals(string1, string2, StringComparison.Ordinal);
    }
}
