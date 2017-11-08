using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamsList : MonoBehaviour {
	public List<BaseExam> Based;
	public Transform TargetTransform;
	public ExamsView ExamsViewPrefab;
	public UserPanel UserPanelPrefab;
    private ExamsView exams;
    // Use this for initialization
    void Start () {
        //CreateExamsList ("venopunction");
        //UserPanel users = Instantiate (UserPanelPrefab);
        //users.transform.SetParent(TargetTransform, false);
        //users.name = UserPanelPrefab.name;
        //foreach (var item in Based) {
        //	Debug.Log (item.Name);
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExamViewOn()
    {
        exams = Instantiate(ExamsViewPrefab);
        exams.transform.SetParent(TargetTransform, false);
        exams.name = ExamsViewPrefab.name;
        exams.Prime(Based);
    }

    public void ExamViewOff()
    {
        Destroy(GameObject.Find("Main Interface/"+ exams.name));
    }

	public void CreateExamsList(string cathegory)
	{
        switch (cathegory)
        {
            case "venipuncture":
                Based = new List<BaseExam>();
                Exam1 ex1 = new Exam1();
                Exam2 ex2 = new Exam2();
                Exam3 ex3 = new Exam3();
                Exam4 ex4 = new Exam4();
                Exam5 ex5 = new Exam5();
                Exam6 ex6 = new Exam6();
                Exam7 ex7 = new Exam7();
                Exam8 ex8 = new Exam8();
                Exam9 ex9 = new Exam9();
                Exam10 ex10 = new Exam10();
                Exam11 ex11 = new Exam11();
                Exam12 ex12 = new Exam12();
                Exam13 ex13 = new Exam13();
                Exam14 ex14 = new Exam14();
                Exam15 ex15 = new Exam15();
                Exam16 ex16 = new Exam16();
                Exam17 ex17 = new Exam17();
                Exam18 ex18 = new Exam18();
                Exam19 ex19 = new Exam19();
                Exam20 ex20 = new Exam20();
                Exam21 ex21 = new Exam21();

                Based.Add(ex1);
                Based.Add(ex2);
                Based.Add(ex3);
                Based.Add(ex4);
                Based.Add(ex5);
                Based.Add(ex6);
                Based.Add(ex7);
                Based.Add(ex8);
                Based.Add(ex9);
                Based.Add(ex10);
                Based.Add(ex11);
                Based.Add(ex12);
                Based.Add(ex13);
                Based.Add(ex14);
                Based.Add(ex15);
                Based.Add(ex16);
                Based.Add(ex17);
                Based.Add(ex18);
                Based.Add(ex19);
                Based.Add(ex20);
                Based.Add(ex21);
                break;
            case "eyes":
                Based = new List<BaseExam>();
                break;
            case "decompression":
                Based = new List<BaseExam>();
                HydrotoraxExam hydrotorax = new HydrotoraxExam();
                PneumotoraxExam pneumotorax = new PneumotoraxExam();
                Based.Add(hydrotorax);
                Based.Add(pneumotorax);
                break;
            case "auscultation":
                Based = new List<BaseExam>();
                break;
            case "reanimation":
                Based = new List<BaseExam>();
                break;
            case "heart_rate":
                Based = new List<BaseExam>();
                break;
            case "intraosseous_access":
                Based = new List<BaseExam>();
                TibiaExam tibia = new TibiaExam();
                Based.Add(tibia);
                break;
            case "blood_pressure_measurement":
                Based = new List<BaseExam>();
                BloodPressureExam1 bloodPressure1 = new BloodPressureExam1();
                BloodPressureExam2 bloodPressure2 = new BloodPressureExam2();
                Based.Add(bloodPressure1);
                Based.Add(bloodPressure2);
                break;    
            default:
                break;
        }
        
	}
}
