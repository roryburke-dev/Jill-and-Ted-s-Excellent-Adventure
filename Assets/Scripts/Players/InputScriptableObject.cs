using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputDevice { keyboard, controller }

public enum KeyBoardInput
{
    Q, W, E, R, T, Y, U, I, O, P, A, S, D, F, G, H, J, K, L, Z, X, C, V, B, N, M,
    one, two, three, four, five, six, seven, eight, nine, zero,
    leftBracket, rightBracket, backSlash, slash, colon, quote, comma, period, minus, plus,
    leftShift, rightShift, leftControl, rightControl, leftAlt, rightAlt,
    upArrow, rightArrow, downArrow, leftArrow, space, enter, backSpace
}

public enum ControllerInput
{

}

[CreateAssetMenu(fileName = "Input", menuName = "ScriptableObjects/Players/Input", order = 1)]
public class InputScriptableObject : ScriptableObject
{
    public InputDevice inputDevice;
    public KeyBoardInput pause;
    public KeyBoardInput shoot;
    public KeyBoardInput up;
    public KeyBoardInput down;
    public KeyBoardInput left;
    public KeyBoardInput right;
}
