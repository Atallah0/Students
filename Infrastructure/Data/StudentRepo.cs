using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StudentRepo : IStudent
    {
        private readonly AppDbContext _context;
        public StudentRepo(AppDbContext context)
        {
            _context = context;
        }

        //GetStudents
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students
            .Include(nameof(Gender))
            .Include(nameof(Address))
            .ToListAsync();
        }

        //GetStudent
        public async Task<Student> GetStudentAsync(int id)
        {
            return await _context.Students
            .Include(nameof(Gender))
            .Include(nameof(Address))
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        //GetGender
        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        //Check for student
        public async Task<bool> Exists(int id)
        {
            return await _context.Students.AnyAsync(x => x.Id == id);
        }

        //UpdateStudent
        public async Task<Student> UpdateStudentAsync(int id, Student request)
        {
            var existingStudent = await GetStudentAsync(id);
            if(existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;
                
                await _context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        //DeleteStudent
        public async Task<Student> DeleteStudenteAsync(int id)
        {
            var student = await GetStudentAsync(id);
            if(student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            }
            return null;
        }

        //CreateStudent
        public async Task<Student> AddStudentAsync(Student request)
        {
            var student = await _context.Students.AddAsync(request);
            await _context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> UpdateProfileImageAsync(int id, string profileImageUrl)
        {
            var student = await GetStudentAsync(id);
            if(student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}