using System;
using System.Linq;
using System.Collections.Generic;

public abstract class BaseExam: IExamInterface
{
    private TupleList<int, bool, string> takenSteps;

    public TupleList<int, bool, string> TakenSteps{ get { return takenSteps; } }

    public int LastTakenStep()
    {
        int lastStep = 0;
        if (this.TakenSteps.Count() != 0)
            lastStep = this.TakenSteps.Last().Item1;

        return lastStep;
    }

    private void TakeStep(int stepNumber, bool result, string errorMessage)
    {
        Tuple<int, bool, string> step = new Tuple<int, bool, string>(stepNumber, result, errorMessage);

        // TODO: Save Taken Step to DB
        // Step Name = CorrectSteps[stepNumber - 1].Item2

        takenSteps.Add(step);
    }

    public bool Move(ref ToolItem tool, string colliderTag, out string errorMessage)
    {
        string _errorMessage = "";
        bool result = CheckMove(ref tool, colliderTag, out _errorMessage);
        errorMessage = _errorMessage;
        if (!result)
        {
            // TODO: Save false result to DB
        }
        return result;
    }

    public bool Action(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = null)
    {
        string _errorMessage = "";
        int? stepNumber = CheckAction(ref tool, actionCode, out _errorMessage, locatedColliderTag);
        errorMessage = _errorMessage;
        if ((stepNumber == null) && (!String.IsNullOrEmpty(errorMessage)))
        {
            // TODO: Save false result to DB
            return false;
        }

        bool stepResult = String.IsNullOrEmpty(errorMessage) ? true : false;
        TakeStep(stepNumber ?? 0, stepResult, errorMessage);

        return true;
    }

    public bool Finish()
    {
        if (takenSteps.Count != CorrectSteps.Count)
            return false;
        
        int currentStepNumber = 0;
        foreach (var step in takenSteps)
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

    public abstract TupleList<string, string> CorrectSteps { get; }

    public abstract TupleList<string, string> ToolActions(ToolItem tool);

    public abstract Dictionary<string, string> InventoryTool { get; }

    public abstract bool CheckMove(ref ToolItem tool, string colliderTag, out string errorMessage);

    public abstract int? CheckAction(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = null);
}

