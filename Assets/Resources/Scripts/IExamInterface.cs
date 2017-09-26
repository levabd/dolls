using System;
using System.Collections.Generic;

interface IExamInterface
{
    /// <summary>
    /// Exam name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Ordered list of steps taken by the user
    /// </summary>
    List<Tuple<int, string, bool>> TakenSteps { get; set; }

    /// <summary>
    /// Correct ordered list of steps. Defining during exam creation
    /// </summary>
    List<string> CorrectSteps { get; }

    /// <summary>
    /// Available tools codes and action codes for tools
    /// </summary>
    Dictionary<ToolItem, Dictionary<string, string>> ToolActions { get; }

    /// <summary>
    /// Collider intersection checking
    /// </summary>
    /// <param name="toolTag">Current medical tool</param>
    /// <param name="toolParams">Dictionary of medical tool parameters</param>
    /// <param name="colliderTag">Current collider checking for</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <returns>True if moving is valid. Othervise False with errorMessage</returns>
    bool CheckMove(string toolTag, Dictionary<string, string> toolParams, string colliderTag, out string errorMessage);

    /// <summary>
    /// Action step checking
    /// </summary>
    /// <param name="toolTag">Current medical tool</param>
    /// <param name="toolParams">Dictionary of medical tool parameters</param>
    /// <param name="actionCode">Tool action codename</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <param name="locatedColliderTag">Current collider checking for</param>
    /// <returns>Correct step numbe. If Null than error was throwed into errorMessage</returns>
    int CheckAction(string toolTag, Dictionary<string, string> toolParams, string actionCode, out string errorMessage, string locatedColliderTag = null);
}