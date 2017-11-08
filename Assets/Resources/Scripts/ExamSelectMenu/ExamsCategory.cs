using UnityEngine;
using System.Collections;

public class ExamsCategory : MonoBehaviour
{
    public ExamsList examsList;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Venipuncture()
    {
        examsList.CreateExamsList("venipuncture");
        examsList.ExamViewOn();
    }
    public void Eyes()
    {
        examsList.CreateExamsList("eyes");
        examsList.ExamViewOn();
    }
    public void Decompression()
    {
        examsList.CreateExamsList("decompression");
        examsList.ExamViewOn();
    }
    public void Auscultation()
    {
        examsList.CreateExamsList("auscultation");
        examsList.ExamViewOn();
    }
    public void Reanimation()
    {
        examsList.CreateExamsList("reanimation");
        examsList.ExamViewOn();
    }
    public void HeartRateTest()
    {
        examsList.CreateExamsList("heart_rate");
        examsList.ExamViewOn();
    }
    public void IntraosseousAccess()
    {
        examsList.CreateExamsList("intraosseous_access");
        examsList.ExamViewOn();
    }
    public void BloodPressureMeasurement()
    {
        examsList.CreateExamsList("blood_pressure_measurement");
        examsList.ExamViewOn();
    }

}
