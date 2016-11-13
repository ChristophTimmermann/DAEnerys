using System;

namespace DAEnerys
{
    public static class Rainbow
    {
        static RainbowState state = RainbowState.RED_TO_GREEN;
        static float progress = 0;

        static float teamR, teamG, teamB;
        static float stripeR, stripeG, stripeB;

        public static void Update()
        {
            switch (state)
            {
                case RainbowState.RED_TO_GREEN:
                    teamR = (1 - progress) * 255;
                    teamG = progress * 255;
                    teamB = 0;

                    stripeR = progress * 255;
                    stripeG = 0;
                    stripeB = (1 - progress) * 255;
                    break;

                case RainbowState.GREEN_TO_BLUE:
                    teamR = 0;
                    teamG = (1 - progress) * 255;
                    teamB = progress * 255;

                    stripeR = (1 - progress) * 255;
                    stripeG = progress * 255;
                    stripeB = 0;
                    break;

                case RainbowState.BLUE_TO_RED:
                    teamR = progress * 255;
                    teamG = 0;
                    teamB = (1 - progress) * 255;

                    stripeR = 0;
                    stripeG = (1 - progress) * 255;
                    stripeB = progress * 255;
                    break;
            }

            Renderer.TeamColor = System.Drawing.Color.FromArgb((int)Math.Round(teamR), (int)Math.Round(teamG), (int)Math.Round(teamB));
            Renderer.StripeColor = System.Drawing.Color.FromArgb((int)Math.Round(stripeR), (int)Math.Round(stripeG), (int)Math.Round(stripeB));

            progress += (float)Program.ElapsedSeconds;

            if (progress > 1)
            {
                switch (state)
                {
                    case RainbowState.RED_TO_GREEN:
                        state = RainbowState.GREEN_TO_BLUE;
                        break;

                    case RainbowState.GREEN_TO_BLUE:
                        state = RainbowState.BLUE_TO_RED;
                        break;

                    case RainbowState.BLUE_TO_RED:
                        state = RainbowState.RED_TO_GREEN;
                        break;
                }

                progress = 0;
            }
        }

        enum RainbowState
        {
            RED_TO_GREEN,
            GREEN_TO_BLUE,
            BLUE_TO_RED,
        }
    }
}
