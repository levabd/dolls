using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
public abstract class BaseExam: IExamInterface
{
    private readonly TupleList<int, bool, string> _takenSteps = new TupleList<int, bool, string>();

    public TupleList<int, bool, string> TakenSteps => _takenSteps;

    public string CurrentBallLiquid = "none";
    public bool NeedleInsideTarget;

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

    public bool Move(string colliderTag, out string errorMessage)
    {
        errorMessage = "";

        string currentErrorMessage;
        if (colliderTag == "Untagged" || String.IsNullOrWhiteSpace(colliderTag))
        {
            errorMessage = "";
            return true;
        }

        bool result = CheckMove(colliderTag, out currentErrorMessage);
        errorMessage = currentErrorMessage;
        if (!result)
        {
            // TODO: Save false result to DB
        }
        return result;
    }

    public bool Action(string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";

        string currentErrorMessage;
        int? stepNumber = CheckAction(actionCode, out currentErrorMessage, locatedColliderTag);
        errorMessage = currentErrorMessage;
        if (stepNumber == null && !String.IsNullOrEmpty(errorMessage))
        {
            // TODO: Save false result to DB
            return false;
        }

        bool stepResult = String.IsNullOrEmpty(errorMessage);
        if (stepNumber != null)
            TakeStep((int)stepNumber, stepResult, errorMessage);

        return true;
    }

    public bool Finish()
    {
        if (_takenSteps.Count != CorrectSteps.Count)
            return false;
        
        int currentStepNumber = 1;
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

    public abstract bool CheckMove(string colliderTag, out string errorMessage);

    public abstract int? CheckAction(string actionCode, out string errorMessage, string locatedColliderTag = "");
}

