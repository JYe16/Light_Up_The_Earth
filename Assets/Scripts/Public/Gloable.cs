using System.Collections.Generic;

public static class Gloable
{
        public static int MAX_CAPTURE_RADIUS = 5000;

        public static int BACK_WITH_NOTHING_RADIUS = 100;

        public static float LASER_LINE_MOVE_SPEED = 20.0f;

        public static float POPUP_ANIMATION_DURATION = 0.2F;

        public enum PropsType
        {
                BOMB = 0,
                POWER_WATER = 1,
                TIME_INCREASE = 2,
                SCORE_INCREASE = 3
        }
}