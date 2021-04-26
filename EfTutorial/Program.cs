using EfTutorial.Models;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace EfTutorial {
    class Program {
        static async Task Main(string[] args) {

            var sctrl = new StudentsController();

            var sGreg = new Student {
                Id = 0, Firstname = "Greg", Lastname = "Doud", StateCode = "OH",
                Gpa = 2.1m, Sat = 805, MajorId = 1
            };
            var sGregNew = await sctrl.Create(sGreg);
            Console.WriteLine($"{sGregNew.Id} {sGregNew.Firstname} {sGregNew.Lastname}");

            var std = await sctrl.GetByPK(sGregNew.Id);

            std.Firstname = "Gregory";
            await sctrl.Change(std);

            var studentDeleted = await sctrl.Remove(std.Id);


            var st = await sctrl.GetByPK(sGregNew.Id);
            if(st == null) {
                Console.WriteLine("Not found");
            } else {
                Console.WriteLine($"{st.Firstname} {st.Lastname}");
            }

            var st1 = await sctrl.GetByPK(11111);
            if(st == null) {
                Console.WriteLine("Not found");
            } else {
                Console.WriteLine($"{st1.Firstname} {st1.Lastname}");
            }

            var students = await sctrl.GetAll();
            foreach(var s in students) {
                Console.WriteLine($"{s.Id} {s.Firstname} {s.Lastname}");
            }

        }
        static void Run1() { 

            var _context = new eddbContext();
            _context.Students.ToList()
                .ForEach(s => Console.WriteLine($"{s.Firstname} {s.Lastname}"));

            var majors = from m in _context.Majors
                         where m.MinSat > 1000
                         orderby m.Description
                         select m;
            foreach(var m in majors) {
                Console.WriteLine($"{m.Description} | {m.MinSat}");
            }

            // Join Students & Majors. Print Name and Major

            var allStudents = (from s in _context.Students
                              join m in _context.Majors
                              on s.MajorId equals m.Id into grp
                              from mm in grp.DefaultIfEmpty()
                              select new {
                                  Name = s.Firstname + " " + s.Lastname,
                                  Major = mm == null ? "Undeclared" : mm.Description
                              }).ToList();
            
            
            
           
            allStudents.ForEach(s => Console.WriteLine($"{s.Name} - {s.Major}"));
        }
    }
}
