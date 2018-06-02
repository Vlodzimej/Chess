using System;


namespace Chess
{
    public class Chess
    {
        public string fen {get; private set; }
        Board board;

        public Chess (string fen = "rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 2")
        {
            this.fen = fen;
            board = new Board(fen);
        }

        Chess (Board board) 
        {
            this.board = board;
        }

        public Chess Move (string move) //Pe2e4 Pe7e8Q
        {
            FigureMoving fm = new FigureMoving(move);
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt (int x, int y)
        {
        
        }
    }
}
