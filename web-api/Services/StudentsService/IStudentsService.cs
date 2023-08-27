using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos.Students;

namespace web_api.Services.StudentsService
{
    public interface IStudentsService
    {
        Task<ServiceResponse<List<GetStudentsDto>>> GetAllStudent();

        Task<ServiceResponse<GetStudentsDto>> GetStudentById(int id);

        Task<ServiceResponse<List<GetStudentsDto>>> CreateStudent(AddStudentsDto student);

        Task<ServiceResponse<GetStudentsDto>> UpdateStudent(int id, UpdateStudentsDto student);

        Task<ServiceResponse<bool>> DeleteStudent(int id);

        Task<ServiceResponse<GetStudentsDto>> isCheckStudent(string email, string nic);

    }
}