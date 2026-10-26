using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace SkrinshoterApp;

public class UndoManager : LocalizableObservableObject
{
	private readonly Stack<IUndoableAction> undo = new Stack<IUndoableAction>();

	private readonly Stack<IUndoableAction> redo = new Stack<IUndoableAction>();

	private readonly ILogger logger;

	private int suppressionLevel;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? undoCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? redoCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UndoCommand => undoCommand ?? (undoCommand = new RelayCommand(Undo, CanUndo));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand RedoCommand => redoCommand ?? (redoCommand = new RelayCommand(Redo, CanRedo));

	public event EventHandler<ObjectAddedEventArgs> ObjectAdded;

	public UndoManager(ILogger<UndoManager> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
	}

	[RelayCommand(CanExecute = "CanUndo")]
	private void Undo()
	{
		if (undo.Count != 0)
		{
			RunWithoutTracking(() =>
			{
				IUndoableAction undoableAction = undo.Pop();
				logger.LogInformation("Undo editor action: {action}", undoableAction);
				undoableAction.Undo();
				redo.Push(undoableAction);
			});
			RaiseCanExecuteChanged();
		}
	}

	private bool CanUndo()
	{
		return undo.Count > 0;
	}

	[RelayCommand(CanExecute = "CanRedo")]
	private void Redo()
	{
		if (redo.Count != 0)
		{
			RunWithoutTracking(() =>
			{
				IUndoableAction undoableAction = redo.Pop();
				logger.LogInformation("Redo editor action: {action}", undoableAction);
				undoableAction.Redo();
				undo.Push(undoableAction);
			});
			RaiseCanExecuteChanged();
		}
	}

	private bool CanRedo()
	{
		return redo.Count > 0;
	}

	public void RegisterAction(IUndoableAction action)
	{
		if (suppressionLevel <= 0 && (!(action is IMergeableAction action2) || undo.Count <= 0 || !(undo.Peek() is IMergeableAction mergeableAction) || !mergeableAction.TryMerge(action2)))
		{
			logger.LogInformation("Editor action: {action}", action);
			undo.Push(action);
			redo.Clear();
			RaiseCanExecuteChanged();
			if (action is AddObjectAction addObjectAction)
			{
				ObjectAdded?.Invoke(this, new ObjectAddedEventArgs(addObjectAction.VisualObject));
			}
		}
	}

	private void RunWithoutTracking(Action action)
	{
		suppressionLevel++;
		try
		{
			action();
		}
		finally
		{
			suppressionLevel--;
		}
	}

	private void RaiseCanExecuteChanged()
	{
		UndoCommand.NotifyCanExecuteChanged();
		RedoCommand.NotifyCanExecuteChanged();
	}
}
