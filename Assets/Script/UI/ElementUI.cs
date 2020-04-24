using UnityEngine;
using UnityEngine.UI;

public enum ElementType
{
    Red = 1,
    Yellow = 2,
    Blue = 3,
}

public class ElementUI : ListItem<ElementType>
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public override void Refresh(ElementType etype)
    {
        base.Refresh(etype);
        image.color = GetColor();
    }

    private Color GetColor()
    {
        switch(data)
        {
            case ElementType.Red: return Color.red;
            case ElementType.Yellow: return Color.yellow;
            case ElementType.Blue: return Color.blue;
        }

        return Color.white;
    }
}