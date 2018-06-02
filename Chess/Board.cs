using System.Text;

namespace Chess {
    class Board {
        string fen { get; set; }
        Figure[, ] figures;
        public Color moveColor { get; private set; }
        public int moveNumber { get; private set; }

        public Board (string fen) {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init ();
        }

        void Init () {
            //
            // 0
            string[] parts = fen.Split ();
            if (parts.Length != 6) return;
            InitFigures (parts[0]);
            moveColor = (parts[1] == "b") ? Color.black : Color.white;
            moveNumber = int.Parse (parts[5]);

            SetFigureAt (new Square ("a1"), Figure.whiteKing);
            SetFigureAt (new Square ("h8"), Figure.blackKing);
            moveColor = Color.white;
        }

        void InitFigures (string data) {
            for (int j = 8; j >= 2; j--)
                data = data.Replace (j.ToString (), (j - 1).ToString () + "1");
            data = data.Replace("1", ".");
            string[] lines = data.Split ('/');
            for (int y = 7; y >= 0; y--)
                for (int x = 0; x < 8; x++)
                    figures[x, y] = (Figure) lines[7 - y][x];
        }

        void GenerateFEN()
        {
            fen = FenFigures() + " " + 
                   (moveColor == Color.white ? "w" : "b") +
                   " - - 0 " + moveNumber.ToString();
        }

        string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                    if(y > 0)
                    sb.Append('/');
            }
            return sb.ToString();
        }

        public Figure GetFigureAt (Square square) {
            if (square.OnBoard ())
                return figures[square.x, square.y];
            return Figure.none;
        }

        void SetFigureAt (Square square, Figure figure) {
            if (square.OnBoard ())
                figures[square.x, square.y] = figure;
        }

        public Board Move (FigureMoving fm) {
            Board next = new Board (fen);
            next.SetFigureAt (fm.from, Figure.none);
            next.SetFigureAt (fm.to, fm.promotion == Figure.none ? fm.figure : fm.promotion);
            if (moveColor == Color.black)
                next.moveNumber++;
            next.moveColor = moveColor.FlipColor ();
            next.GenerateFEN();
            return next;
        }
    }
}