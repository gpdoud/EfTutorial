using EfTutorial.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfTutorial {
    
    public class StudentsController {

        private readonly eddbContext _context;

        // read all the student with SAT score between 1000 and 1200 inclusive
        // and order the result by SAT score descending
        public IEnumerable<Student> GetBySatRange(int LowSat, int HighSat) {

            return _context.Students
                            .Where(s => s.Sat >= LowSat && s.Sat <= HighSat)
                            .OrderByDescending(s => s.Sat)
                            .ToList();

            return (from s in _context.Students
                   where s.Sat >= LowSat && s.Sat <= HighSat
                   orderby s.Sat descending
                   select s).ToList();
        }


        public IEnumerable<Student> GetAll() {
            return _context.Students.ToList();
        }

        public Student GetByPK(int id) {
            return _context.Students.Find(id);
        }

        public Student Create(Student student) {
            if(student == null) {
                throw new Exception("Student cannot be null!");
            }
            if(student.Id != 0) {
                throw new Exception("Student.Id must be zero!");
            }
            _context.Students.Add(student);
            var rowsAffected = _context.SaveChanges();
            if(rowsAffected != 1) {
                throw new Exception("Create failed!");
            }
            return student;
        }

        public void Change(Student student) {
            if(student == null) {
                throw new Exception("Student cannot be null!");
            }
            if(student.Id <= 0) {
                throw new Exception("Student.Id must be greater than zero!");
            }
            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = _context.SaveChanges();
            if(rowsAffected != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }

        public Student Remove(int id) {
            var student = _context.Students.Find(id);
            if(student == null) {
                return null;
            }
            _context.Students.Remove(student);
            var rowsAffected = _context.SaveChanges();
            if(rowsAffected != 1) {
                throw new Exception("Remove failed!");
            }
            return student;
        }

        public StudentsController() {
            _context = new eddbContext();
        }
    }
}
