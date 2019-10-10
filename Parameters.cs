
public static class Parameters
{ 

    // Bool AI is set if the game is set against an AI:
    public static bool AI { get; set; }

    // Bool AIPlaying is true during the AIs turn:
    public static bool AIPlaying { get; set; }

    // Int winner is 0 if no player has won, 1 if palyer one has won, 2 if player 2 has won.
    public static int winner { get; set; }

}
