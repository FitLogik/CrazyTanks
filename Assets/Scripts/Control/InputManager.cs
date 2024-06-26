using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private KeyCode selectedButton;
    Text text;

    private void OnGUI()
    {
        if (Event.current.keyCode != KeyCode.None)
        {
            selectedButton = Event.current.keyCode;
        }
    }

    public IEnumerator SetButton(GameObject button)
    {
        yield return new WaitUntil(() =>
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    return true;
                }
                typeof(ControlSystem).GetProperty(button.name).SetValue(null, selectedButton, null);
                GameObject.Find($"Canvas/{button.name}/{button.name}Text");
                GetComponent<Text>().text = selectedButton.ToString();
                return true;
            }
            return false;
        });
    }
}