using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CursorDetection : MonoBehaviour {

    private GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);

    public Transform currentCharacter;

    public Transform token;
    public bool hasToken;

	void Start () {

        gr = GetComponentInParent<GraphicRaycaster>();

        SmashCSS.instance.ShowCharacterInSlot(0, null);

    }

    void Update () {

        //CONFIRM
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentCharacter != null)
            {
                TokenFollow(false);
                SmashCSS.instance.ConfirmCharacter(0, SmashCSS.instance.characters[currentCharacter.GetSiblingIndex()]);
            }
        }

        //CANCEL
        if (Input.GetKeyDown(KeyCode.X))
        {
            SmashCSS.instance.confirmedCharacter = null;
            TokenFollow(true);
        }

        if (hasToken)
        {
            token.position = transform.position;
        }

        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);

        if (hasToken)
        {
            if (results.Count > 0)
            {
                Transform raycastCharacter = results[0].gameObject.transform;

                if (raycastCharacter != currentCharacter)
                {
                    if (currentCharacter != null)
                    {
                        currentCharacter.Find("selectedBorder").GetComponent<Image>().DOKill();
                        currentCharacter.Find("selectedBorder").GetComponent<Image>().color = Color.clear;
                    }
                    SetCurrentCharacter(raycastCharacter);
                }
            }
            else
            {
                if (currentCharacter != null)
                {
                    currentCharacter.Find("selectedBorder").GetComponent<Image>().DOKill();
                    currentCharacter.Find("selectedBorder").GetComponent<Image>().color = Color.clear;
                    SetCurrentCharacter(null);
                }
            }
        }
		
	}

    void SetCurrentCharacter(Transform t)
    {
        
        if(t != null)
        {
            t.Find("selectedBorder").GetComponent<Image>().color = Color.white;
            t.Find("selectedBorder").GetComponent<Image>().DOColor(Color.red, .7f).SetLoops(-1);
        }

        currentCharacter = t;

        if (t != null)
        {
            int index = t.GetSiblingIndex();
            Character character = SmashCSS.instance.characters[index];
            SmashCSS.instance.ShowCharacterInSlot(0, character);
        }
        else
        {
            SmashCSS.instance.ShowCharacterInSlot(0, null);
        }
    }

    void TokenFollow (bool trigger)
    {
        hasToken = trigger;
    }
}
