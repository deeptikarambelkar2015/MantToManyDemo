using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace MantToManyDemo.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        //navigation property
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public string Address { get; set; }
        
        //navigation property
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        //Navtion properties
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }

    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(s => new { s.StudentId, s.SubjectId });
            
            //A student can have multiple records in StudentSubject table
            builder.HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            //There can be multiple records for a subject in the StudentSubject table
            builder.HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }
}
