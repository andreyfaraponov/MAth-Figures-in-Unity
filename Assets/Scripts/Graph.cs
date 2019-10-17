using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    private GraphFunction[] functions = {SineFunction, MultiSineFunction};
    [Range(10, 100)] public int resolution = 10;
    Transform[] points;
    public GraphFunctionName function;

    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = Vector3.zero;
        points = new Transform[resolution];
        for (int i = 0; i < resolution; i++)
        {
            Transform point = Instantiate(pointPrefab, transform);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }
    }

    private void Update()
    {
        float t = Time.time;
        var f = functions[(int)function];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, t);
            point.localPosition = position;
        }
    }

    float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    float MultiSineFunction(float x,float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f * t)) / 2f;
        y *= 2f / 3f;
        return y;
    }
}

public enum GraphFunctionName {
    Sine,
    MultiSine
}