using System;
using System.Collections.Generic;

interface IExamInterface
{
    /// <summary>
    /// Exam name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Correct ordered list of steps. Defining during exam creation
    /// </summary>
    TupleList<string, string> CorrectSteps { get; }

    /// <summary>
    /// Ordered list of steps taken by the user
    /// </summary>
    TupleList<int, bool, string> TakenSteps { get; }

    /// <summary>
    /// Available tools codes and action codes for tools
    /// </summary>
    /// <param name="tool">Current medical tool</param>
    /// <returns>Available tools codes and action codes for tools with localizations</returns>
    TupleList<string, string> ToolActions(ToolItem tool);

    /// <summary>
    /// Available tools in inventory
    /// </summary>
    Dictionary<string, string> InventoryTool { get; }

    /// <summary>
    /// Collider intersection checking
    /// </summary>
    /// <param name="tool">Current medical tool</param>
    /// <param name="colliderTag">Current collider checking for</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <returns>True if moving is valid. Othervise False with errorMessage</returns>
    bool CheckMove(ref ToolItem tool, string colliderTag, out string errorMessage);

    /// <summary>
    /// Collider intersection checking
    /// </summary>
    /// <param name="tool">Current medical tool</param>
    /// <param name="colliderTag">Current collider checking for</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <returns>True if moving is valid. Othervise False with errorMessage</returns>
    bool Move(ref ToolItem tool, string colliderTag, out string errorMessage);

    /// <summary>
    /// Action step checking
    /// </summary>
    /// <param name="tool">Current medical tool</param>
    /// <param name="actionCode">Tool action codename</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <param name="locatedColliderTag">Current collider checking for</param>
    /// <returns>Correct step numbe. If Null than error was throwed into errorMessage</returns>
    int? CheckAction(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = null);

    /// <summary>
    /// Action step checking
    /// </summary>
    /// <param name="tool">Current medical tool</param>
    /// <param name="actionCode">Tool action codename</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <param name="locatedColliderTag">Current collider checking for</param>
    /// <returns>Correct step numbe. If Null than error was throwed into errorMessage</returns>
    bool Action(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = null);

    /// <summary>
    /// Final steps check
    /// </summary>
    bool Finish();
}