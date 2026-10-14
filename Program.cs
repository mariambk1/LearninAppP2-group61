using System;
using System.Collections.Generic;
using System.IO;

class Grid
{
    private CommandProgram program;
    private Position finalPosition;
    private Direction finalDirection;
    private Player player;
    private List<string> trace; 


    // constructor voor het createn van een nieuwe grid
    public Grid()
    {
        player = new Player ();
        trace = new List<string>();
    }

    //
}

class Player
{
    public Position position; 
    public Direction direction;

    // constructor voor nieuwe player
    public Player()
    {
        position = new Position(0,0);
        direction = Direction.East;
    }
    
    //logic voor updaten van position en direction 
    public void Move(int amountSteps)
    {
        // movement per direction
            if (direction == Direction.North)
                position.Y += amountSteps;
            else if (direction == Direction.East)
                position.X += amountSteps;
            else if (direction == Direction.South)
                position.Y -= amountSteps;
            else if (direction == Direction.West)
                position.X -= amountSteps;
    }
    void TurnLeft()
    {
        if (direction == Direction.North)
            direction = Direction.West;
        else if (direction == Direction.West)
            direction = Direction.South;
        else if (direction == Direction.South)
            direction = Direction.East;
        else if (direction == Direction.East)
            direction = Direction.North;
    }
    void TurnRight()
    {
        if (direction == Direction.North)
            direction = Direction.East;
        else if (direction == Direction.East)
            direction = Direction.South;
        else if (direction == Direction.South)
            direction = Direction.West;
        else if (direction == Direction.West)
            direction = Direction.North;
    } 

}

class Position
{
    public int X {get; set;}
    public int Y {get; set;}

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    // translate function, maar is dit wel nodig als we alle logic in Move() hebben?
}

enum Direction{
    North,
    East,
    South,
    West
}

enum TurnDirection{
    Left,
    Right
}

//interface for execution commands
interface ICommand
{
    void Execute(Player player);
}

//classes to implement and execute commands
class MoveCommand : ICommand
{
    int steps; 
}

class TurnCommand : ICommand
{
    TurnDirection direction;
}

class RepeatCommand : ICommand
{
    int times;
    //Commands list<commands>
}


class Metrics
{
    int commandCount;
    int maxNestingLevel;
    int repeatCount;

    // calculate function
}


interface IProgramImport
{
    // import function
}

class FileImport : IProgramImport
{
    
}


interface ICommandProgram {}

class CommandProgram : ICommandProgram
{
    Metrics metrics;
    // list commands
}
