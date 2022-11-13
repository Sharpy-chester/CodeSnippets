#include <vector>

bool gameover = false;
Board newBoard;

struct Vector2
{
	int x;
	int y;
};

struct Move //made this to represent a move
{
	std::vector<Vector2> positions;
	int evaluation;
};

struct Board
{
	char board[8][8];
};

Board ExecuteMove(Move move, Board board);
Move EvaluateMove(Move move, Board board);
std::vector<Move> GetAllPossibleMoves(Board board, bool maxPlayer);
Move bestMove;

Move miniMax(Board board, int depth, bool maxPlayer)
{
    //if the depth of the tree is 0 (if its at the bottom of the tree) or the game is over, then return the best move
	if (depth == 0 || gameover)
	{
		EvaluateMove(bestMove, board);
		return bestMove;
	}
	//if its the ai's turn
	if (maxPlayer)
	{
		//create int called maxeval and set it to -infinity (will screw everything up if i dont set it as it will default to 0)(10000 is pretty much infinity right?)
		Move maxEval;
		maxEval.evaluation = -10000;
		//get all possible moves of AI
		std::vector<Move> allPossibleMoves = GetAllPossibleMoves(board, maxPlayer);
		//for each possible move of AI
		for (Move i : allPossibleMoves) 
		{
			newBoard = ExecuteMove(i, board);
			//eval = miniMax(board with possible move added, depth - 1, false). This is where the recursion happens to go down the tree of possible moves
			Move eval = miniMax(newBoard, depth - 1, false);

			//set max eval to the highest between maxeval and eval and give it the value of the current move
			if (eval.evaluation > maxEval.evaluation)
			{
				maxEval.evaluation = eval.evaluation;
				maxEval.positions = i.positions;
				
			}
		}
		//return possible move from maxeval
		return maxEval;
	}
	else 
	{
		//if its the players turn, create int called minEval and set it to +infinity
		Move minEval;
		minEval.evaluation = 10000;
		//get all possible moves of player
		std::vector<Move> allPossibleMoves = GetAllPossibleMoves(board, maxPlayer);

		//for each possible move of player
		for (Move i : allPossibleMoves)
		{
			newBoard = ExecuteMove(i, board);
			//eval = miniMax(board with possible move added, depth - 1, true) This is where the recursion happens to go down the tree of possible moves
			Move eval = miniMax(newBoard, depth - 1, true);
			//set min eval to the highest between mineval and eval and give it the value of the current move
			if (eval.evaluation < minEval.evaluation)
			{
				minEval.evaluation = eval.evaluation;
				minEval.positions = i.positions;
			}
		}
		//return minEval
		return minEval;
	}
	
}