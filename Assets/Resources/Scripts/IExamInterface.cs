using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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
    List<Tuple<int, string>> CorrectSteps { get; }
	
	/// <summary>
    /// Tools codes and action codes
    /// </summary>
    Dictionary<string, List<string>> ToolActions { get; }

    /// <summary>
    /// Collider intersection checking
    /// </summary>
    /// <param name="toolCode">Current medical tool</param>
    /// <param name="toolParams">Dictionary of medical tool parameters</param>
    /// <param name="collider">Current collider checking for</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <returns>True if moving is valid. Othervise False with errorMessage</returns>
    bool CheckMove(string toolCode, Dictionary<string, string> toolParams, GameObject collider, out string errorMessage);

    /// <summary>
    /// Action step checking
    /// </summary>
    /// <param name="toolCode">Current medical tool</param>
    /// <param name="toolParams">Dictionary of medical tool parameters</param>
    /// <param name="actionCode">Tool action codename</param>
    /// <param name="collider">Current collider checking for</param>
    /// <param name="errorMessage">Displayed error message</param>
    /// <returns>Correct step numbe. If Null than error was throwed into errorMessage</returns>
    int CheckAction(string toolCode, Dictionary<string, string> toolParams, string actionCode, System.Nullable<GameObject> locatedCollider, out string errorMessage);
}
