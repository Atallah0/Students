using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.RepoInterfaces
{
    public interface IStudent
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(int id);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(int id);
        Task<Student> UpdateStudentAsync(int id, Student request);
        Task<Student> DeleteStudenteAsync(int id);
        Task<Student> AddStudentAsync(Student request);
        Task<bool> UpdateProfileImageAsync(int id, string profileImageUrl);
    }
}