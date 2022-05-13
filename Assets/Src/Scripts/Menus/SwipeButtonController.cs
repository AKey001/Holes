using Menus;
using UnityEngine;

public class SwipeButtonController : MonoBehaviour
{
    public SwipeController swipeController;
    
    public void ButtonClicked(int direction)
    {
        swipeController.Swipe(direction);
    }
}
