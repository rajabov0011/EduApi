using EduApi.Application.Models.Requests.Common;

namespace EduApi.Application.Models.Requests.Teacher
{
    public class TopTeachersByStudentPerformanceRequest
    {
        public string Subject { get; set; } = null!;
        public int TopStudentsCount { get; set; } = 5;
        public bool WithLowestMarks { get; set; } = false;
        public int TeachersCount { get; set; } = 10;
    }
}
