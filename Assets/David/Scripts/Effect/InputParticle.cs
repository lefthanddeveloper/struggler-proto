using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputParticle : MonoBehaviour
{
    [SerializeField] private bool m_LookAtCam = true;

    [SerializeField] private TMP_Text m_InputKey;
    [SerializeField] private AnimationCurve m_AlphaCurve;
    [SerializeField] private float m_LifeTime = 1.0f;
    [SerializeField] private SpriteRenderer m_SpriteRend;
    [SerializeField] private Color m_Color;

    private Coroutine fadeCor = null;
    private Transform _camTr;
    private Transform camTr
    {
        get
        {
            if(_camTr == null)
            {
                _camTr = PlayerLocal.Instance.m_MainCam.transform;
            }
            return _camTr;
        }
    }

    public void ShowParticle(Vector3 pos, string _inputKey)
    {
        if(fadeCor != null)
        {
            StopCoroutine(fadeCor);
        }

        this.transform.position = pos;
        m_InputKey.text = _inputKey;
        SetColor(m_Color);

        this.gameObject.SetActive(true);

        fadeCor = StartCoroutine(FadeCor(() =>
        {
            fadeCor = null;
            this.gameObject.SetActive(false);  
        }));
    }

    private IEnumerator FadeCor(Action done )
    {
        float timePassed = 0f;
        float ratio = 0f;
        while(ratio < 1f)
        {
            timePassed += Time.deltaTime;
            ratio = timePassed / m_LifeTime;

            Color color = m_Color;
            color.a = m_AlphaCurve.Evaluate(ratio);
            SetColor(color);
            yield return null;
        }

        done?.Invoke();
    }

    private void SetColor(Color _color)
    {
        m_SpriteRend.color = _color;
        m_InputKey.color = _color;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_LookAtCam)
        {
            Vector3 look = this.transform.position - camTr.position;
            this.transform.rotation = Quaternion.LookRotation(look);
        }
    }
}
