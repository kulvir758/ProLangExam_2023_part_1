using Microsoft.AspNetCore.Mvc;
using University.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace University.Controllers;

[Route("{controller}")]
public class TeacherController
{
    private readonly UniversityDbContext _universityDbContext;
    private readonly UniversityDbContext _dbContext;

    public TeacherController(UniversityDbContext universityDbContext) => _universityDbContext = universityDbContext;

        [HttpGet]
        public string Test(string message) => $"Echo reply for: {message}";

        

        [HttpGet("All")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _dbContext.Teachers.ToList();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeacher(int id)
        {
            var teacher = _dbContext.Teachers.Include(t => t.Skills).FirstOrDefault(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        

        [HttpPost]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSkills = _dbContext.Skills.ToList();
            var teacherSkills = new List<Skill>();

            foreach (var skill in teacher.Skills)
            {
                var existingSkill = existingSkills.FirstOrDefault(s => s.Name.Equals(skill.Name, StringComparison.OrdinalIgnoreCase));

                if (existingSkill != null)
                {
                    teacherSkills.Add(existingSkill);
                }
                else
                {
                    _dbContext.Skills.Add(skill);
                    teacherSkills.Add(skill);
                }
            }

            teacher.Skills = teacherSkills;

            _dbContext.Teachers.Add(teacher);
            _dbContext.SaveChanges();

            return Ok(teacher);
        }




        [HttpPut("{id}")]
        public IActionResult EditTeacher(int id, Teacher teacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = _dbContext.Teachers.Include(t => t.Skills).FirstOrDefault(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            teacher.FirstName = teacherDto.FirstName;
            teacher.LastName = teacherDto.LastName;
            teacher.Age = teacherDto.Age;
            teacher.Skills.Clear();

            var existingSkills = _dbContext.Skills.Where(s => teacherDto.Skills.Contains(s.Name)).ToList();
            teacher.Skills = existingSkills;

            _dbContext.SaveChanges();

            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _dbContext.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _dbContext.Teachers.Remove(teacher);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
