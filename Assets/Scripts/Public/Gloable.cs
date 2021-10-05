using System.Collections.Generic;

public static class Gloable
{
        public static int MAX_CAPTURE_RADIUS = 1000;

        public static float LASER_LINE_MOVE_SPEED = 6.0f;
        
        public enum PropsType
        {
                BOMB = 0,
                POWER_WATER = 1,
                TIME_INCREASE = 2,
                SCORE_INCREASE = 3
        }
}