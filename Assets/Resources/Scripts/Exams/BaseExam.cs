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

    private void SaveModel(string errorMessage)
    {
        _examModel.Error = errorMessage;
        _examModel.Save();
        CurrentAdminExam.Exam = _examModel;
    }

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
            _examModel = new Exam(CurrentUser.User, CurrentExam.Instance.Exam.GetType().Name, Name, "");
            _examModel.Save();
        }

        if (result && stepNumber != _takenSteps.Count + 1)
            errorMessage = "Крок виконано вірно, проте не в тій послідовності. Бажано переробити сценарій. Натисніть «Завершити сценарій».";
        new Step(_examModel, CorrectSteps[stepNumber - 1].Item2, errorMessage, stepNumber, _takenSteps.Count + 1, result).Save();

        _takenSteps.Add(step);
    }

    public bool Move(string colliderTag, out string errorMessage, out string tipMessage)
    {
        errorMessage = "";

        string currentErrorMessage;
        if (colliderTag == "Untagged" || String.IsNullOrWhiteSpace(colliderTag))
        {
            errorMessage = "";
            tipMessage = "";
            return true;
        }

        bool result = CheckMove(colliderTag, out currentErrorMessage, out tipMessage);
        errorMessage = currentErrorMessage;
        if (!result)
        {
            if (_examModel == null)
                _examModel = new Exam(CurrentUser.User, CurrentExam.Instance.Exam.GetType().Name, Name, "");

            _examModel.Passed = false;
            SaveModel(errorMessage);
        }
        return result;
    }

    public bool Action(string actionCode, out string errorMessage, out string tipMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        tipMessage = "";
        showAnimation = true;

        string currentErrorMessage;
        int? stepNumber = CheckAction(actionCode, out currentErrorMessage, ref tipMessage, out showAnimation, locatedColliderTag);
        errorMessage = currentErrorMessage;
        if (stepNumber == null && !String.IsNullOrEmpty(errorMessage))
        {
            if (_examModel == null)
                _examModel = new Exam(CurrentUser.User, CurrentExam.Instance.Exam.GetType().Name, Name, "");

            _examModel.Passed = false;
            SaveModel(errorMessage);
            return false;
        }

        bool stepResult = String.IsNullOrEmpty(errorMessage);
        if (stepNumber != null)
        {
            if (stepResult)
            {
                if (stepNumber != _takenSteps.Count + 1)
                {
                    tipMessage = "Крок виконано вірно, проте не в тій послідовності. Бажано переробити сценарій. Натисніть «Завершити сценарій».";
                    showAnimation = false;
                }
                else
                    tipMessage = "Вітаємо. Крок виконано абсолютно вірно.";
            }
            else
                tipMessage = "Крок виконано невірно. " + errorMessage + ". Бажано переробити сценарій. Натисніть «Завершити сценарій».";
            TakeStep((int) stepNumber, stepResult, errorMessage);
        }

        if (String.IsNullOrEmpty(tipMessage) && !showAnimation)
            tipMessage = "Схоже що ця дія була зайвою, або сценарій вже провалено.";

        return true;
    }

    public bool Finish()
    {
        if (_examModel == null)
            _examModel = new Exam(CurrentUser.User, CurrentExam.Instance.Exam.GetType().Name, Name, "");

        _examModel.Passed = false;

        if (_takenSteps.Count != CorrectSteps.Count)
        {
            SaveModel("Було проведено недостатньо кроків");
            return false;
        }
        
        int currentStepNumber = 1;
        foreach (var step in _takenSteps)
        {
            if (step.Item1 != currentStepNumber)
            {
                SaveModel("Хибний порядок дій");
                return false;
            }
            if (!step.Item2)
            {
                SaveModel("Пропущено кроки");
                return false;
            }
            currentStepNumber++;
        }

        _examModel.Passed = true;
        SaveModel("");

        return true;
    }

    public string CheckAirEmbolism()
    {
        if (NeedleRemovingMoment != DateTime.MinValue && (DateTime.Now - NeedleRemovingMoment).TotalSeconds > 5)
        {
            NeedleRemovingMoment = DateTime.MinValue;
            return "Повітряна емболія";
        }

        return "";
    }

    public void AirEmbolismFinish(string errorMessage)
    {
        if (_examModel == null)
            _examModel = new Exam(CurrentUser.User, CurrentExam.Instance.Exam.GetType().Name, Name, "");

        _examModel.Passed = false;
        SaveModel(errorMessage);
    }

    public abstract string Name { get; }

    public abstract string HelpString { get; }

    public abstract DateTime NeedleRemovingMoment { get; set; }

    public abstract string LoadName { get; }

    public abstract TupleList<string, string> CorrectSteps { get; }

    public abstract TupleList<string, string> ToolActions(ToolItem tool);

    public abstract Dictionary<string, string> InventoryTool { get; }

    public abstract bool CheckMove(string colliderTag, out string errorMessage, out string tipMessage);

    public abstract int? CheckAction(string actionCode, out string errorMessage, ref string tipMessage, out bool showAnimation, string locatedColliderTag = "");
}

