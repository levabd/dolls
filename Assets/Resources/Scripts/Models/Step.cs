using System;
using System.Collections.Generic;
using System.Data;

// ReSharper disable once CheckNamespace
namespace DB.Models
{
    public class Step: BaseModel
    {
        private readonly int _examId;

        public int? Id { get; }
        public Exam Exam => Exam.FindById(_examId);
        public string Name { get; }
        public string Error { get; }
        public bool Passed { get; }
        public int OrderNumber { get; }
        public int OrderedAt { get; }

        public Step(int examId, string name, string error, int orderNumber, int orderedAt, bool passed = false)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Step name cannot be empty", nameof(name));

            _examId = examId;
            Name = name.Trim();
            Error = error;
            OrderNumber = orderNumber;
            OrderedAt = orderedAt;
            Passed = passed;
        }

        public Step(Exam exam, string name, string error, int orderNumber, int orderedAt, bool passed = false)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Step name cannot be null", nameof(name));
            if (exam?.Id == null)
                throw new ArgumentException("Exam and exam_id cannot be null", nameof(exam));

            _examId = exam.Id ?? 0;
            Name = name.Trim();
            Error = error;
            OrderNumber = orderNumber;
            OrderedAt = orderedAt;
            Passed = passed;
        }

        private Step(int id, int examId, string name, string error, int orderNumber, int orderedAt, int passed)
        {
            Id = id;
            _examId = examId;
            Name = name.Trim();
            Error = error;
            OrderNumber = orderNumber;
            OrderedAt = orderedAt;
            Passed = passed != 0;
        }

        public static List<Step> FindByExam(Exam exam)
        {
            if (exam?.Id == null)
                throw new ArgumentException("Exam and exam_id cannot be null", nameof(exam));

            List<Step> steps = new List<Step>();
            
            List<List<object>> rawSteps = SelectAll("SELECT id, exam_id, name, error_message, order_number, ordered_at, passed FROM Steps WHERE exam_id = '" + exam.Id + "'");

            foreach (var rawStep in rawSteps)
            {
                Step step = new Step((int)rawStep[0], (int)rawStep[1], (string)rawStep[2], (string)rawStep[3], (int)rawStep[4], (int)rawStep[5], (int)rawStep[6]);
                steps.Add(step);
            }

            return steps;
        }

        public void Save()
        {
            if (Id == null) // Create
                Execute("INSERT INTO Steps (exam_id, name, error_message, order_number, ordered_at, passed) VALUES ('"
                    + _examId + "', '" + Name + "', '" + Error + "', '" + OrderNumber + "', '" + OrderedAt + "', '" + (Passed ? "1" : "0") + "')");
            else
                throw new ConstraintException("id not null. We can't UPDATE Step DB Record.");
        }
    }
}
