// ReSharper disable once CheckNamespace
public static class VenflonHelper
{

    public static bool BloodInsidePavilion(this BaseExam exam, string colliderTag, string targetColliderTag)
    {
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && colliderTag.Contains(targetColliderTag))
        {
            CurrentTool.Instance.Tool.StateParams["blood_inside"] = "true";
            // Запустить анимацию крови

            return true;
        }

        return false;
    }
}