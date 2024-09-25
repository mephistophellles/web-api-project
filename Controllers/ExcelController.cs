using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;

namespace Zakaz.Controllers;

public class ExcelController : ControllerBase
{
    private readonly DbHelper database;
    private readonly XLWorkbook workbook;

    public ExcelController(DbHelper database, XLWorkbook workbook)
    {
        this.database = database;
        this.workbook = workbook;
    }

    //[Authorize]
    [HttpGet("/api/excel")]
    public async Task<IActionResult> GetExcel()
    {
        var tasks = new List<Task>()
        {
            Teachers(),
            Classes(),
            ClassVenue(),
            Venue(),
            RegistrationCourse(),
            TeachersCourses(),
            Courses(),
            Reviews(),
            Students(),
            Grades(),
            DiffLevels()
        };
        await Task.WhenAll(tasks);
        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);

        var name = "Отчет.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
    }

    private async Task Teachers()
    {
        var teachers = await database.Teachers.ToListAsync();
        var teacherSheet = workbook.Worksheets.Add("Учители");
        teacherSheet.Cell("A1").Value = "Id";
        teacherSheet.Cell("B1").Value = "ФИО";
        teacherSheet.Cell("C1").Value = "Опыт";
        teacherSheet.Cell("D1").Value = "Контакты";

        int i = 2;
        foreach (var teacher in teachers)
        {
            teacherSheet.Cell($"A{i}").Value = teacher.TeachersId;
            teacherSheet.Cell($"B{i}").Value = teacher.Fio;
            teacherSheet.Cell($"C{i}").Value = teacher.Internship;
            teacherSheet.Cell($"D{i}").Value = teacher.ContactInformation;
        }
    }

    private async Task Classes()
    {
        var classes = await database.Classess.ToListAsync();
        var classesSheet = workbook.Worksheets.Add("Классы");
        classesSheet.Cell("A1").Value = "Id";
        classesSheet.Cell("B1").Value = "CourseId";
        classesSheet.Cell("C1").Value = "Дата";

        int i = 2;
        foreach (var clas in classes)
        {
            classesSheet.Cell($"A{i}").Value = clas.ClassessId;
            classesSheet.Cell($"B{i}").Value = clas.CourseId;
            classesSheet.Cell($"C{i}").Value = clas.Date.Date.ToString("dd.MM.yyyy");
        }
    }

    private async Task ClassVenue()
    {
        var classVenues = await database.ClassesVenue.ToListAsync();
        var classVenueSheet = workbook.Worksheets.Add("Занятия-Помещения");
        classVenueSheet.Cell("A1").Value = "ClassesId";
        classVenueSheet.Cell("B1").Value = "VenueId";

        int i = 2;
        foreach (var classVenue in classVenues)
        {
            classVenueSheet.Cell($"A{i}").Value = classVenue.ClassesId;
            classVenueSheet.Cell($"B{i}").Value = classVenue.VenueId;
        }
    }

    private async Task Venue()
    {
        var venues = await database.Venue.ToListAsync();
        var venuesSheet = workbook.Worksheets.Add("Помещения");
        venuesSheet.Cell("A1").Value = "Id";
        venuesSheet.Cell("B1").Value = "Город";
        venuesSheet.Cell("C1").Value = "Район";
        venuesSheet.Cell("D1").Value = "Улица";
        venuesSheet.Cell("E1").Value = "Дом";
        venuesSheet.Cell("F1").Value = "Этаж";
        venuesSheet.Cell("G1").Value = "Офис";

        int i = 2;
        foreach (var venue in venues)
        {
            venuesSheet.Cell($"A{i}").Value = venue.VenueId;
            venuesSheet.Cell($"B{i}").Value = venue.City;
            venuesSheet.Cell($"C{i}").Value = venue.District;
            venuesSheet.Cell($"D{i}").Value = venue.Street;
            venuesSheet.Cell($"E{i}").Value = venue.House;
            venuesSheet.Cell($"F{i}").Value = venue.Floor;
            venuesSheet.Cell($"G{i}").Value = venue.Office;
        }
    }

    private async Task RegistrationCourse()
    {
        var regCourses = await database.RegistrationCourse.ToListAsync();
        var regCourseSheet = workbook.Worksheets.Add("Регистрации на курсы");
        regCourseSheet.Cell("A1").Value = "Id";
        regCourseSheet.Cell("B1").Value = "StudentId";
        regCourseSheet.Cell("C1").Value = "CourseId";
        regCourseSheet.Cell("D1").Value = "Дата";

        int i = 2;
        foreach (var regCourse in regCourses)
        {
            regCourseSheet.Cell($"A{i}").Value = regCourse.RegistrationCourseId;
            regCourseSheet.Cell($"B{i}").Value = regCourse.StudentId;
            regCourseSheet.Cell($"C{i}").Value = regCourse.CourseId;
            regCourseSheet.Cell($"D{i}").Value = regCourse.Date.Date.ToString("dd.MM.yyyy");
        }
    }

    private async Task TeachersCourses()
    {
        var teachersCourses = await database.TeachersCourses.ToListAsync();
        var teachersCoursesSheet = workbook.Worksheets.Add("Учители-Курсы");
        teachersCoursesSheet.Cell("A1").Value = "TeacherId";
        teachersCoursesSheet.Cell("B1").Value = "CourseId";

        int i = 2;
        foreach (var teachersCourse in teachersCourses)
        {
            teachersCoursesSheet.Cell($"A{i}").Value = teachersCourse.TeacherId;
            teachersCoursesSheet.Cell($"B{i}").Value = teachersCourse.CourseId;
        }
    }

    private async Task Courses()
    {
        var courses = await database.Courses.ToListAsync();
        var coursesSheet = workbook.Worksheets.Add("Курсы");
        coursesSheet.Cell("A1").Value = "Id";
        coursesSheet.Cell("B1").Value = "Название";
        coursesSheet.Cell("C1").Value = "Описание";
        coursesSheet.Cell("D1").Value = "DifficultyId";

        int i = 2;
        foreach (var course in courses)
        {
            coursesSheet.Cell($"A{i}").Value = course.CoursesId;
            coursesSheet.Cell($"B{i}").Value = course.CourseName;
            coursesSheet.Cell($"C{i}").Value = course.Description;
            coursesSheet.Cell($"D{i}").Value = course.LevelId;
        }
    }

    private async Task Reviews()
    {
        var reviews = await database.Reviews.ToListAsync();
        var reviewsSheet = workbook.Worksheets.Add("Отзывы");
        reviewsSheet.Cell("A1").Value = "Id";
        reviewsSheet.Cell("B1").Value = "StudentId";
        reviewsSheet.Cell("C1").Value = "CourseId";
        reviewsSheet.Cell("D1").Value = "Текст";

        int i = 2;
        foreach (var review in reviews)
        {
            reviewsSheet.Cell($"A{i}").Value = review.ReviewsId;
            reviewsSheet.Cell($"B{i}").Value = review.StudentId;
            reviewsSheet.Cell($"C{i}").Value = review.CourseId;
            reviewsSheet.Cell($"D{i}").Value = review.Text;
        }
    }

    private async Task Students()
    {
        var students = await database.Students.ToListAsync();
        var studentsSheet = workbook.Worksheets.Add("Студенты");
        studentsSheet.Cell("A1").Value = "Id";
        studentsSheet.Cell("B1").Value = "ФИО";
        studentsSheet.Cell("C1").Value = "Дата рождения";

        int i = 2;
        foreach (var student in students)
        {
            studentsSheet.Cell($"A{i}").Value = student.StudentsId;
            studentsSheet.Cell($"B{i}").Value = student.Fio;
            studentsSheet.Cell($"C{i}").Value = student.DateOfBirth.Date.ToString("dd.MM.yyyy");
        }
    }

    private async Task Grades()
    {
        var grades = await database.Grades.ToListAsync();
        var gradesSheet = workbook.Worksheets.Add("Оценки");
        gradesSheet.Cell("A1").Value = "Id";
        gradesSheet.Cell("B1").Value = "StudentId";
        gradesSheet.Cell("C1").Value = "CourseId";
        gradesSheet.Cell("D1").Value = "Оценка";
        gradesSheet.Cell("E1").Value = "Комментарий";

        int i = 2;
        foreach (var grade in grades)
        {
            gradesSheet.Cell($"A{i}").Value = grade.GradesId;
            gradesSheet.Cell($"B{i}").Value = grade.StudentId;
            gradesSheet.Cell($"C{i}").Value = grade.CourseId;
            gradesSheet.Cell($"D{i}").Value = grade.Grade;
            gradesSheet.Cell($"E{i}").Value = grade.Comment;
        }
    }

    private async Task DiffLevels()
    {
        var diffLevels = await database.DifficultyLevels.ToListAsync();
        var diffLevelsSheet = workbook.Worksheets.Add("Уровни сложности");
        diffLevelsSheet.Cell("A1").Value = "Id";
        diffLevelsSheet.Cell("B1").Value = "Название";
        diffLevelsSheet.Cell("C1").Value = "Описание";

        int i = 2;
        foreach (var level in diffLevels)
        {
            diffLevelsSheet.Cell($"A{i}").Value = level.DifficultyLevelId;
            diffLevelsSheet.Cell($"B{i}").Value = level.Name;
            diffLevelsSheet.Cell($"C{i}").Value = level.Description;
        }
    }
}