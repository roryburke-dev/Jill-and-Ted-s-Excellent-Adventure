// Made with the help of this great post: https://joshondesign.com/2013/03/01/improvedEasingEquations

// --------------------------------- Other Related Links --------------------------------------------------------------------
// Original equations, bad formulation:	https://github.com/danro/jquery-easing/blob/master/jquery.easing.js
// A few equations, very simplified:	https://gist.github.com/gre/1650294
// Easings.net equations, simplified:	https://github.com/ai/easings.net/blob/master/src/easings/easingsFunctions.ts

// Easing Functions Cheat Sheet: https://easings.net/

using System;

namespace Kryz.Tweening
{
    public enum EasingFunctionEnum
    {
        none, linear, inQuad, outQuad, inOutQuad, inCubic, outCubic, inOutCubic, inQuart, outQuart, inOutQuart,
        inQuint, outQuint, inOutQuint, inSine, outSine, inOutSine, inExpo, outExpo, inOutExpo, inCirc, outCirc, inOutCirc,
        inElastic, outElastic, inOutElastic, inBack, outBack, inOutBack, inBounce, outBounce, inOutBounce
    }

    public static class EasingFunctions
    {
        public static float GetEasingFunctionFromEnum(EasingFunctionEnum easingFunctionEnum, float t) 
        {
            switch (easingFunctionEnum)
            {
                case EasingFunctionEnum.linear:         return Linear(t);
                case EasingFunctionEnum.inQuad:         return InQuad(t);
                case EasingFunctionEnum.outQuad:        return OutQuad(t);
                case EasingFunctionEnum.inOutQuad:      return InOutQuad(t);
                case EasingFunctionEnum.inCubic:        return InCubic(t);
                case EasingFunctionEnum.outCubic:       return OutCubic(t);
                case EasingFunctionEnum.inOutCubic:     return InOutCubic(t);
                case EasingFunctionEnum.inQuart:        return InQuart(t);
                case EasingFunctionEnum.outQuart:       return OutQuart(t);
                case EasingFunctionEnum.inOutQuart:     return InOutQuart(t);
                case EasingFunctionEnum.inQuint:        return InQuint(t);
                case EasingFunctionEnum.outQuint:       return OutQuint(t);
                case EasingFunctionEnum.inOutQuint:     return InOutQuint(t);
                case EasingFunctionEnum.inSine:         return InSine(t);
                case EasingFunctionEnum.outSine:        return OutSine(t);
                case EasingFunctionEnum.inOutSine:      return InOutSine(t);
                case EasingFunctionEnum.inExpo:         return InExpo(t);
                case EasingFunctionEnum.outExpo:        return OutExpo(t);
                case EasingFunctionEnum.inOutExpo:      return InOutExpo(t);
                case EasingFunctionEnum.inCirc:         return InCirc(t);
                case EasingFunctionEnum.outCirc:        return OutCirc(t);
                case EasingFunctionEnum.inOutCirc:      return InOutCirc(t);
                case EasingFunctionEnum.inElastic:      return InElastic(t);
                case EasingFunctionEnum.outElastic:     return OutElastic(t);
                case EasingFunctionEnum.inOutElastic:   return InOutElastic(t);
                case EasingFunctionEnum.inBack:         return InBack(t);
                case EasingFunctionEnum.outBack:        return OutBack(t);
                case EasingFunctionEnum.inOutBack:      return InOutBack(t);
                case EasingFunctionEnum.inBounce:       return InBounce(t);
                case EasingFunctionEnum.outBounce:      return OutBounce(t);
                case EasingFunctionEnum.inOutBounce:    return InOutBounce(t);
            }
            return t;
        }

        public static float Linear(float t) => t;

        public static float InQuad(float t) => t * t;
        public static float OutQuad(float t) => 1 - InQuad(1 - t);
        public static float InOutQuad(float t)
        {
            if (t < 0.5) return InQuad(t * 2) / 2;
            return 1 - InQuad((1 - t) * 2) / 2;
        }

        public static float InCubic(float t) => t * t * t;
        public static float OutCubic(float t) => 1 - InCubic(1 - t);
        public static float InOutCubic(float t)
        {
            if (t < 0.5) return InCubic(t * 2) / 2;
            return 1 - InCubic((1 - t) * 2) / 2;
        }

        public static float InQuart(float t) => t * t * t * t;
        public static float OutQuart(float t) => 1 - InQuart(1 - t);
        public static float InOutQuart(float t)
        {
            if (t < 0.5) return InQuart(t * 2) / 2;
            return 1 - InQuart((1 - t) * 2) / 2;
        }

        public static float InQuint(float t) => t * t * t * t * t;
        public static float OutQuint(float t) => 1 - InQuint(1 - t);
        public static float InOutQuint(float t)
        {
            if (t < 0.5) return InQuint(t * 2) / 2;
            return 1 - InQuint((1 - t) * 2) / 2;
        }

        public static float InSine(float t) => (float)-Math.Cos(t * Math.PI / 2);
        public static float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
        public static float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;

        public static float InExpo(float t) => (float)Math.Pow(2, 10 * (t - 1));
        public static float OutExpo(float t) => 1 - InExpo(1 - t);
        public static float InOutExpo(float t)
        {
            if (t < 0.5) return InExpo(t * 2) / 2;
            return 1 - InExpo((1 - t) * 2) / 2;
        }

        public static float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
        public static float OutCirc(float t) => 1 - InCirc(1 - t);
        public static float InOutCirc(float t)
        {
            if (t < 0.5) return InCirc(t * 2) / 2;
            return 1 - InCirc((1 - t) * 2) / 2;
        }

        public static float InElastic(float t) => 1 - OutElastic(1 - t);
        public static float OutElastic(float t)
        {
            float p = 0.3f;
            return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
        }
        public static float InOutElastic(float t)
        {
            if (t < 0.5) return InElastic(t * 2) / 2;
            return 1 - InElastic((1 - t) * 2) / 2;
        }

        public static float InBack(float t)
        {
            float s = 1.70158f;
            return t * t * ((s + 1) * t - s);
        }
        public static float OutBack(float t) => 1 - InBack(1 - t);
        public static float InOutBack(float t)
        {
            if (t < 0.5) return InBack(t * 2) / 2;
            return 1 - InBack((1 - t) * 2) / 2;
        }

        public static float InBounce(float t) => 1 - OutBounce(1 - t);
        public static float OutBounce(float t)
        {
            float div = 2.75f;
            float mult = 7.5625f;

            if (t < 1 / div)
            {
                return mult * t * t;
            }
            else if (t < 2 / div)
            {
                t -= 1.5f / div;
                return mult * t * t + 0.75f;
            }
            else if (t < 2.5 / div)
            {
                t -= 2.25f / div;
                return mult * t * t + 0.9375f;
            }
            else
            {
                t -= 2.625f / div;
                return mult * t * t + 0.984375f;
            }
        }
        public static float InOutBounce(float t)
        {
            if (t < 0.5) return InBounce(t * 2) / 2;
            return 1 - InBounce((1 - t) * 2) / 2;
        }
    }
}