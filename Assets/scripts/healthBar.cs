using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Slider Slider;

    public void setHealth(int health)
    {
        Slider.value = health;
    }
}
