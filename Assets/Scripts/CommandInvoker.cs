using System;
using System.Collections.Generic;

public class CommandInvoker
{
    public static Stack<ICommand> _undoStack = new Stack<ICommand>();
    public static Stack<ICommand> _redoStack = new Stack<ICommand>();
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);

        _redoStack.Clear();
    }

    public static void UndoCommand()
    {
        if (_undoStack.Count <= 0) return;
        ICommand command = _undoStack.Pop();
        _redoStack.Push(command);
        command.Undo();
    }

    public static void RedoCommand()
    {
        if (_redoStack.Count <= 0) return;
        ICommand command = _redoStack.Pop();
        _undoStack.Push(command);
        command.Execute();
    }

    public static void ResetCommand()
    {
        _redoStack.Clear();
        _undoStack.Clear();
    }
}