using System;
using System.Collections.Generic;
using System.IO;

class Player{}


class Position {}

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


interface ICommand{}

class MoveCommand : ICommand {}

class TurnCommand : ICommand{}

class RepeatCommand : ICommand {}


class Metrics{}


interface IProgramImport{}

class FileImport : IProgramImport {}


interface ICommandProgram {}

class CommandProgram : ICommandProgram{}
