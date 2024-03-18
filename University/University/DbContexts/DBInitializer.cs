using System.Security.Cryptography;
using System.Text;
using University.Models;

namespace University.DbContexts
{
    public static class DBInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Faculties.Any())
            {
                await context.Faculties.AddRangeAsync(GetFaculty());
                await context.SaveChangesAsync();
            }

            if (!context.Curriculums.Any())
            {
                await context.Curriculums.AddRangeAsync(GetCurriculum());
                await context.SaveChangesAsync();
            }

            if (!context.Humans.Any())
            {
                await context.Humans.AddRangeAsync(GetHuman());
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(GetUser());
                await context.SaveChangesAsync();
            }

            if (!context.Teachers.Any())
            {
                await context.Teachers.AddRangeAsync(GetTeacher());
                await context.SaveChangesAsync();
            }

            if (!context.Groups.Any())
            {
                await context.Groups.AddRangeAsync(GetGroup());
                await context.SaveChangesAsync();
            }

            if (!context.Students.Any())
            {
                await context.Students.AddRangeAsync(GetStudent());
                await context.SaveChangesAsync();
            }
        }
        private static IList<Faculty> GetFaculty()
        {
            return new List<Faculty>
            {
                new()
                {
                    Name = "Computer Science",
                    Description = "Study of algorithms, data structures, and software engineering principles.",
                },
                new()
                {
                    Name = "Electrical Engineering",
                    Description = "Focuses on the study of electricity, electronics, and electromagnetism."
                },
                new()
                {
                    Name = "Mechanical Engineering",
                    Description = "Deals with the design and operation of machines and mechanical systems."
                },
                new()
                {
                    Name = "Civil Engineering",
                    Description = "Involves the design, construction, and maintenance of infrastructure projects."
                },
                new()
                {
                    Name = "Chemical Engineering",
                    Description = "Concerned with the design and operation of chemical processes and plants."
                },
                new()
                {
                    Name = "Biomedical Engineering",
                    Description = "Combines principles of engineering and biology to solve medical problems."
                },
                new()
                {
                    Name = "Aerospace Engineering",
                    Description = "Focuses on the design and development of aircraft and spacecraft."
                },
                new()
                {
                    Name = "Environmental Engineering",
                    Description = "Addresses environmental issues and develops solutions to protect the environment."
                },
                new()
                {
                    Name = "Industrial Engineering",
                    Description = "Optimizes complex processes or systems by improving efficiency and effectiveness."
                },
                new()
                {
                    Name = "Science and Engineering",
                    Description = "Studies the properties of materials and their applications in various fields."
                }
            };
        }

        private static IList<Curriculum> GetCurriculum()
        {
            return new List<Curriculum>()
            {
                new()
                {
                    Name = "Introduction to Computer Science",
                    Description = "Basic concepts and principles of computer science.",
                    FacultyId = 1 // Computer Science
                },
                new()
                {
                    Name = "Digital Logic Design",
                    Description = "Study of digital circuits and systems.",
                    FacultyId = 1 // Computer Science
                },
                new()
                {
                    Name = "Data Structures and Algorithms",
                    Description = "Study of data organization, storage, and retrieval methods.",
                    FacultyId = 1 // Computer Science
                },
                new()
                {
                    Name = "Electric Circuits",
                    Description = "Fundamental principles of electric circuits.",
                    FacultyId = 2 // Electrical Engineering
                },
                new()
                {
                    Name = "Control Systems",
                    Description = "Study of control and automation systems.",
                    FacultyId = 2 // Electrical Engineering
                },
                new()
                {
                    Name = "Mechanics",
                    Description = "Study of motion and forces.",
                    FacultyId = 3 // Mechanical Engineering
                },
                new()
                {
                    Name = "Thermodynamics",
                    Description = "Study of energy and its transformations.",
                    FacultyId = 3 // Mechanical Engineering
                },
                new()
                {
                    Name = "Structural Engineering",
                    Description = "Study of structural analysis and design.",
                    FacultyId = 4 // Civil Engineering
                },
                new()
                {
                    Name = "Environmental Engineering",
                    Description = "Study of environmental systems and processes.",
                    FacultyId = 4 // Civil Engineering
                },
                new()
                {
                    Name = "Chemical Kinetics",
                    Description = "Study of reaction rates in chemical processes.",
                    FacultyId = 5 // Chemical Engineering
                },
                new()
                {
                    Name = "Process Control",
                    Description = "Control and optimization of chemical processes.",
                    FacultyId = 5 // Chemical Engineering
                },
                new()
                {
                    Name = "Biomechanics",
                    Description = "Application of mechanical principles to biological systems.",
                    FacultyId = 6 // Biomedical Engineering
                },
                new()
                {
                    Name = "Medical Imaging",
                    Description = "Study of various imaging modalities in medicine.",
                    FacultyId = 6 // Biomedical Engineering
                },
                new()
                {
                    Name = "Aerodynamics",
                    Description = "Study of the motion of air and other gases.",
                    FacultyId = 7 // Aerospace Engineering
                },
                new()
                {
                    Name = "Spacecraft Design",
                    Description = "Principles and methods for designing spacecraft.",
                    FacultyId = 7 // Aerospace Engineering
                },
                new()
                {
                    Name = "Water Resources Engineering",
                    Description = "Study of the management of water resources.",
                    FacultyId = 8 // Environmental Engineering
                },
                new()
                {
                    Name = "Air Pollution Control",
                    Description = "Methods for controlling air pollution.",
                    FacultyId = 8 // Environmental Engineering
                },
                new()
                {
                    Name = "Operations Research",
                    Description = "Application of mathematical methods to optimize systems.",
                    FacultyId = 9 // Industrial Engineering
                },
                new()
                {
                    Name = "Supply Chain Management",
                    Description = "Management of the flow of goods and services.",
                    FacultyId = 9 // Industrial Engineering
                },
                new()
                {
                    Name = "Materials Science",
                    Description = "Study of the properties and applications of materials.",
                    FacultyId = 10 // Materials Science and Engineering
                },
                new()
                {
                    Name = "Nanotechnology",
                    Description = "Study of manipulation of matter on an atomic and molecular scale.",
                    FacultyId = 10 // Materials Science and Engineering
                },
                new()
                {
                    Name = "Advanced Programming",
                    Description = "Advanced concepts and techniques in programming.",
                    FacultyId = 1 // Computer Science
                },
                new()
                {
                    Name = "Power Systems Analysis",
                    Description = "Analysis and optimization of power systems.",
                    FacultyId = 2 // Electrical Engineering
                },
                new()
                {
                    Name = "Manufacturing Processes",
                    Description = "Study of processes used to convert raw materials into finished products.",
                    FacultyId = 3 // Mechanical Engineering
                },
                new()
                {
                    Name = "Transportation Engineering",
                    Description = "Study of transportation systems and their design.",
                    FacultyId = 4 // Civil Engineering
                }
            };
        }

        private static IList<Human> GetHuman()
        {
            return new List<Human>()
            {
                new()
                {
                    LastName = "Smith",
                    FirstName = "John",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Gender = "Male",
                    Address = "123 Main Street",
                    Email = "john.smith@example.com",
                    Phone = "+380995435754"
                },
                new()
                {
                    LastName = "Johnson",
                    FirstName = "Emily",
                    DateOfBirth = new DateTime(1985, 8, 23),
                    Gender = "Female",
                    Address = "456 Elm Avenue",
                    Email = "emily.johnson@example.com",
                    Phone = "+380995435755"
                },
                new()
                {
                    LastName = "Williams",
                    FirstName = "Michael",
                    DateOfBirth = new DateTime(1982, 3, 10),
                    Gender = "Male",
                    Address = "789 Oak Lane",
                    Email = "michael.williams@example.com",
                    Phone = "+380995435756"
                },
                new()
                {
                    LastName = "Jones",
                    FirstName = "Jessica",
                    DateOfBirth = new DateTime(1995, 11, 7),
                    Gender = "Female",
                    Address = "321 Pine Road",
                    Email = "jessica.jones@example.com",
                    Phone = "+380995435757"
                },
                new()
                {
                    LastName = "Brown",
                    FirstName = "David",
                    DateOfBirth = new DateTime(1988, 9, 20),
                    Gender = "Male",
                    Address = "654 Cedar Boulevard",
                    Email = "david.brown@example.com",
                    Phone = "+380995435758"
                },
                new()
                {
                    LastName = "Davis",
                    FirstName = "Sarah",
                    DateOfBirth = new DateTime(1992, 6, 30),
                    Gender = "Female",
                    Address = "987 Maple Drive",
                    Email = "sarah.davis@example.com",
                    Phone = "+380995435759"
                },
                new()
                {
                    LastName = "Miller",
                    FirstName = "Christopher",
                    DateOfBirth = new DateTime(1980, 4, 5),
                    Gender = "Male",
                    Address = "135 Walnut Street",
                    Email = "christopher.miller@example.com",
                    Phone = "+380995435760"
                },
                new()
                {
                    LastName = "Wilson",
                    FirstName = "Jennifer",
                    DateOfBirth = new DateTime(1984, 10, 12),
                    Gender = "Female",
                    Address = "246 Birch Avenue",
                    Email = "jennifer.wilson@example.com",
                    Phone = "+380995435761"
                },
                new()
                {
                    LastName = "Moore",
                    FirstName = "James",
                    DateOfBirth = new DateTime(1978, 7, 18),
                    Gender = "Male",
                    Address = "579 Pinecrest Avenue",
                    Email = "james.moore@example.com",
                    Phone = "+380995435762"
                },
                new()
                {
                    LastName = "Taylor",
                    FirstName = "Elizabeth",
                    DateOfBirth = new DateTime(1998, 2, 25),
                    Gender = "Female",
                    Address = "802 Chestnut Lane",
                    Email = "elizabeth.taylor@example.com",
                    Phone = "+380995435763"
                },
                new()
                {
                    LastName = "Anderson",
                    FirstName = "Robert",
                    DateOfBirth = new DateTime(1993, 12, 8),
                    Gender = "Male",
                    Address = "1010 Oak Street",
                    Email = "robert.anderson@example.com",
                    Phone = "+380995435764"
                },
                new()
                {
                    LastName = "Thomas",
                    FirstName = "Mary",
                    DateOfBirth = new DateTime(1983, 1, 14),
                    Gender = "Female",
                    Address = "203 Elm Street",
                    Email = "mary.thomas@example.com",
                    Phone = "+380995435765"
                },
                new()
                {
                    LastName = "Jackson",
                    FirstName = "William",
                    DateOfBirth = new DateTime(1991, 9, 3),
                    Gender = "Male",
                    Address = "304 Maple Avenue",
                    Email = "william.jackson@example.com",
                    Phone = "+380995435766"
                },
                new()
                {
                    LastName = "White",
                    FirstName = "Patricia",
                    DateOfBirth = new DateTime(1979, 6, 22),
                    Gender = "Female",
                    Address = "405 Pine Road",
                    Email = "patricia.white@example.com",
                    Phone = "+380995435767"
                },
                new()
                {
                    LastName = "Harris",
                    FirstName = "Daniel",
                    DateOfBirth = new DateTime(1986, 8, 7),
                    Gender = "Male",
                    Address = "506 Cedar Boulevard",
                    Email = "daniel.harris@example.com",
                    Phone = "+380995435768"
                },
                new()
                {
                    LastName = "Martin",
                    FirstName = "Linda",
                    DateOfBirth = new DateTime(1981, 4, 11),
                    Gender = "Female",
                    Address = "607 Maple Drive",
                    Email = "linda.martin@example.com",
                    Phone = "+380995435769"
                },
                new()
                {
                    LastName = "Clark",
                    FirstName = "Mark",
                    DateOfBirth = new DateTime(1994, 11, 28),
                    Gender = "Male",
                    Address = "708 Walnut Street",
                    Email = "mark.clark@example.com",
                    Phone = "+380995435770"
                },
                new()
                {
                    LastName = "Lewis",
                    FirstName = "Karen",
                    DateOfBirth = new DateTime(1987, 3, 16),
                    Gender = "Female",
                    Address = "809 Birch Avenue",
                    Email = "karen.lewis@example.com",
                    Phone = "+380995435771"
                },
                new()
                {
                    LastName = "Lee",
                    FirstName = "Paul",
                    DateOfBirth = new DateTime(1989, 7, 4),
                    Gender = "Male",
                    Address = "910 Pinecrest Avenue",
                    Email = "paul.lee@example.com",
                    Phone = "+380995435772"
                },
                new()
                {
                    LastName = "Walker",
                    FirstName = "Barbara",
                    DateOfBirth = new DateTime(1996, 5, 9),
                    Gender = "Female",
                    Address = "1011 Chestnut Lane",
                    Email = "barbara.walker@example.com",
                    Phone = "+380995435773"
                }
            };
        }

        private static IList<User> GetUser()
        {
            return new List<User>()
            {
                new()
                {
                    Login = "123",
                    Password = GetHashPassword("123"),
                    Role = "Teacher",
                    HumanId = 1
                },
                new()
                {
                    Login = "test",
                    Password = GetHashPassword("test"),
                    Role = "Teacher",
                    HumanId = 2
                },
                new()
                {
                    Login = "111",
                    Password = GetHashPassword("111"),
                    Role = "Student",
                    HumanId = 3
                },
                new()
                {
                    Login = "user1",
                    Password = GetHashPassword("user1pass"),
                    Role = "Student",
                    HumanId = 4
                },
                new()
                {
                    Login = "user2",
                    Password = GetHashPassword("user2pass"),
                    Role = "Student",
                    HumanId = 5
                }
            };
        }

        private static IList<Teacher> GetTeacher()
        {
            return new List<Teacher>()
            {
                new()
                {
                    Position = "Professor",
                    AcademicDegreee = "Ph.D.",
                    HumanId = 1
                },
                new()
                {
                    Position = "Associate Professor",
                    AcademicDegreee = "Ph.D.",
                    HumanId = 2
                },
                new()
                {
                    Position = "Assistant Professor",
                    AcademicDegreee = "Ph.D.",
                    HumanId = 6
                },
                new()
                {
                    Position = "Lecturer",
                    AcademicDegreee = "M.Sc.",
                    HumanId = 7
                },
                new()
                {
                    Position = "Adjunct Faculty",
                    AcademicDegreee = "M.Sc.",
                    HumanId = 8
                },
                new()
                {
                    Position = "Visiting Professor",
                    AcademicDegreee = "Ph.D.",
                    HumanId = 9
                },
                new()
                {
                    Position = "Teaching Assistant",
                    AcademicDegreee = "B.Sc.",
                    HumanId = 10
                },
                new()
                {
                    Position = "Research Professor",
                    AcademicDegreee = "Ph.D.",
                    HumanId = 11
                },
                new()
                {
                    Position = "Postdoctoral Fellow",
                    AcademicDegreee = "Ph.D.",
                    HumanId = 12
                },
            };
        }

        private static IList<Group> GetGroup()
        {
            return new List<Group>
            {
                new()
                {
                    GroupName = "CS101",
                    CuratorId = 1,
                    CurriculumId = 1
                },
                new()
                {
                    GroupName = "EE201",
                    CuratorId = 4,
                    CurriculumId = 1
                },
                new()
                {
                    GroupName = "ME301",
                    CuratorId = 6,
                    CurriculumId = 1 
                },
                new()
                {
                    GroupName = "CE401",
                    CuratorId = 8,
                    CurriculumId = 2
                },
                new()
                {
                    GroupName = "CHE501",
                    CuratorId = 2,
                    CurriculumId = 2
                },
                new()
                {
                    GroupName = "BME601",
                    CuratorId = 3,
                    CurriculumId = 3
                },
                new()
                {
                    GroupName = "AE701",
                    CuratorId = 5,
                    CurriculumId = 3
                },
                new()
                {
                    GroupName = "ENVE801",
                    CuratorId = 7,
                    CurriculumId = 3
                },
                new()
                {
                    GroupName = "IE901",
                    CuratorId = 8,
                    CurriculumId = 4
                },
                new()
                {
                    GroupName = "MSE1010",
                    CuratorId = 9,
                    CurriculumId = 5
                }
            };
        }

        private static IList<Student> GetStudent()
        {
            return new List<Student>
            {
                new()
                {
                    Course = 1,
                    Speciality = "Computer Science",
                    HumanId = 3,
                },
                new()
                {
                    Course = 2,
                    Speciality = "Electrical Engineering",
                    HumanId = 4, 
                    GroupId = 1 
                },
                new()
                {
                    Course = 3,
                    Speciality = "Mechanical Engineering",
                    HumanId = 5,
                    GroupId = 1
                },
                new()
                {
                    Course = 1,
                    Speciality = "Civil Engineering",
                    HumanId = 19,
                },
                new()
                {
                    Course = 1,
                    Speciality = "Chemical Engineering",
                    HumanId = 14,
                    GroupId = 2
                },
                new()
                {
                    Course = 3,
                    Speciality = "Biomedical Engineering",
                    HumanId = 15,
                    GroupId = 3
                },
                new()
                {
                    Course = 2,
                    Speciality = "Aerospace Engineering",
                    HumanId = 16,
                    GroupId = 4
                },
                new()
                {
                    Course = 4,
                    Speciality = "Environmental Engineering",
                    HumanId = 17,
                    GroupId = 4
                },
                new()
                {
                    Course = 3,
                    Speciality = "Industrial Engineering",
                    HumanId = 18,
                },
            };
        }

        private static string GetHashPassword(string password)
        {
            var md5 = MD5.Create();

            var hashPass = md5.ComputeHash
                (
                    Encoding.UTF8.GetBytes(password)
                );

            return Convert.ToBase64String(hashPass);
        }
    }
}
