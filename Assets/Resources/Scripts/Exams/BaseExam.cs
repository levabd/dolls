using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
public abstract class BaseExam: IExamInterface
{
    private readonly TupleList<int, bool, string> _takenSteps = new TupleList<int, bool, string>();

    public TupleList<int, bool, string> TakenSteps => _takenSteps;

    public int LastTakenStep()
    {
        int lastStep = 0;
        if (TakenSteps.Count != 0)
            lastStep = TakenSteps.Last().Item1;

        return lastStep;
    }

    private void TakeStep(int stepNumber, bool result, string errorMessage)
    {
        Tuple<int, bool, string> step = new Tuple<int, bool, string>(stepNumber, result, errorMessage);

        // TODO: Save Taken Step to DB
        // Step Name = CorrectSteps[stepNumber - 1].Item2

        _takenSteps.Add(step);
    }

    public bool Move(ref ToolItem tool, string colliderTag, out string errorMessage)
    {
        string currentErrorMessage;
        bool result = CheckMove(ref tool, colliderTag, out currentErrorMessage);
        errorMessage = currentErrorMessage;
        if (!result)
        {
            // TODO: Save false result to DB
        }
        return result;
    }

    public bool Action(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        string currentErrorMessage;
        UnityEngine.Debug.Log("lastTakenStep inside Base class" + LastTakenStep());
        int? stepNumber = CheckAction(ref tool, actionCode, out currentErrorMessage, locatedColliderTag);
        errorMessage = currentErrorMessage;
        if ((stepNumber == null) && (!String.IsNullOrEmpty(errorMessage)))
        {
            // TODO: Save false result to DB
            return false;
        }

        bool stepResult = String.IsNullOrEmpty(errorMessage);
        TakeStep(stepNumber ?? 0, stepResult, errorMessage);

        return true;
    }

    public bool Finish()
    {
        if (_takenSteps.Count != CorrectSteps.Count)
            return false;
        
        int currentStepNumber = 0;
        foreach (var step in _takenSteps)
        {
            if (step.Item1 != currentStepNumber)
                return false;
            if (!step.Item2)
                return false;
            currentStepNumber++;
        }

        return true;
    }
    public abstract string Name { get; }

    public abstract string LoadName { get; }

    public abstract TupleList<string, string> CorrectSteps { get; }

    public abstract TupleList<string, string> ToolActions(ToolItem tool);

    public abstract Dictionary<string, string> InventoryTool { get; }

    public abstract bool CheckMove(ref ToolItem tool, string colliderTag, out string errorMessage);

    public abstract int? CheckAction(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = "");
}

