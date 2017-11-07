using System;
using System.Collections.Generic;
using System.Linq;
using DB.Models;

// ReSharper disable once CheckNamespace
public abstract class BaseExam: IExamInterface
{
    private readonly TupleList<int, bool, string> _takenSteps = new TupleList<int, bool, string>();

    public TupleList<int, bool, string> TakenSteps => _takenSteps;

    public string CurrentBallLiquid = "none";
    public bool NeedleInsideTarget;

    private Exam _examModel;

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

        if (_examModel == null)
        {
            _examModel = new Exam(CurrentUser.User, Name, "");
            _examModel.Save();
        }

        new Step(_examModel, CorrectSteps[stepNumber - 1].Item2, errorMessage, stepNumber, _takenSteps.Count + 1, result).Save();

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
            if (_examModel == null)
                _examModel = new Exam(CurrentUser.User, Name, "");

            _examModel.Passed = false;
            _examModel.Error = errorMessage;
            _examModel.Save();
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
            if (_examModel == null)
                _examModel = new Exam(CurrentUser.User, Name, "");

            _examModel.Passed = false;
            _examModel.Error = errorMessage;
            _examModel.Save();
            return false;
        }

        bool stepResult = String.IsNullOrEmpty(errorMessage);
        if (stepNumber != null)
            TakeStep((int)stepNumber, stepResult, errorMessage);

        return true;
    }

    public bool Finish()
    {
        if (_examModel == null)
            _examModel = new Exam(CurrentUser.User, Name, "");

        _examModel.Passed = false;

        if (_takenSteps.Count != CorrectSteps.Count)
        {
            _examModel.Error = "Було проведено недостатньо кроків.";
            _examModel.Save();
            return false;
        }
        
        int currentStepNumber = 1;
        foreach (var step in _takenSteps)
        {
            if (step.Item1 != currentStepNumber)
            {
                _examModel.Error = "Хибний порядок дій.";
                _examModel.Save();
                return false;
            }
            if (!step.Item2)
            {
                _examModel.Error = "Пропущено кроки.";
                _examModel.Save();
                return false;
            }
            currentStepNumber++;
        }

        _examModel.Passed = true;
        _examModel.Error = "";
        _examModel.Save();

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

