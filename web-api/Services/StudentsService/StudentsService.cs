global using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;

namespace web_api.Services.StudentsService
{
    public class StudentsService : IStudentsService
    {
        private static List<Students> students = new List<Students>()
        {
            new Students{Id=1, First_Name="John", Last_Name="Doe",},
            new Students{Id=2, First_Name="jane", Last_Name="Doe",}
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public StudentsService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetStudentsDto>>> CreateStudent(AddStudentsDto new_student)
        {
            var serviceResponse = new ServiceResponse<List<GetStudentsDto>>();
            try
            {
                Students student = _mapper.Map<Students>(new_student);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Students.Select(s => _mapper.Map<GetStudentsDto>(s)).ToListAsync();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Error while creating data";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteStudent(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var student = await _context.Students.FirstAsync(s => s.Id == id);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                serviceResponse.Data = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = false;
                serviceResponse.Message = "Error while deleting data";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<GetStudentsDto>>> GetAllStudent()
        {
            var serviceResponse = new ServiceResponse<List<GetStudentsDto>>();
            var dbStudents = await _context.Students.ToListAsync();
            serviceResponse.Data = dbStudents.Select(s => _mapper.Map<GetStudentsDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudentsDto>> GetStudentById(int id)
        {
            var serviceResponse = new ServiceResponse<GetStudentsDto>();
            try
            {
                var dbStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (dbStudent == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Student not found";
                    return serviceResponse;
                }
                serviceResponse.Data = _mapper.Map<GetStudentsDto>(dbStudent);
                return serviceResponse;
            }
            catch (System.Exception)
            {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Error while fetching data";
                    return serviceResponse;
            }
            
        }

        public async Task<ServiceResponse<GetStudentsDto>> isCheckStudent(string email, string nic)
        {
            var serviceResponse = new ServiceResponse<GetStudentsDto>();
            try
            {
                var dbStudent = await _context.Students.FirstOrDefaultAsync(s => s.Email == email || s.NIC == nic);
                if (dbStudent == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Student not found";
                    return serviceResponse;
                }
                serviceResponse.Data = _mapper.Map<GetStudentsDto>(dbStudent);
                return serviceResponse;
            }
            catch (System.Exception)
            {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Error while fetching data";
                    return serviceResponse;
            }
            

        }

        public async Task<ServiceResponse<GetStudentsDto>> UpdateStudent(int id, UpdateStudentsDto student)
{
    var serviceResponse = new ServiceResponse<GetStudentsDto>();
    try
    {
        var dbStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (dbStudent == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            serviceResponse.Message = "Student not found";
            return serviceResponse;
        }

        // Update the properties based on the incoming student DTO
        if (!string.IsNullOrEmpty(student.First_Name))
            dbStudent.First_Name = student.First_Name;

        if (!string.IsNullOrEmpty(student.Last_Name))
            dbStudent.Last_Name = student.Last_Name;

        if (!string.IsNullOrEmpty(student.Email))
            dbStudent.Email = student.Email;

        if (!string.IsNullOrEmpty(student.Phone))
            dbStudent.Phone = student.Phone;
        
        if (!string.IsNullOrEmpty(student.NIC))
            dbStudent.NIC = student.NIC;
        
        if (student.DOB != null)
            dbStudent.DOB = student.DOB;
        
        if (!string.IsNullOrEmpty(student.Address))
            dbStudent.Address = student.Address;

        // Save the changes to the database
        await _context.SaveChangesAsync();

        // Map and return the updated student DTO
        serviceResponse.Data = _mapper.Map<GetStudentsDto>(dbStudent);
        return serviceResponse;
    }
    catch (Exception ex)
    {
        serviceResponse.Success = false;
        serviceResponse.Data = null;
        serviceResponse.Message = "Error while updating data: " + ex.Message;
        return serviceResponse;
    }
}
    }
}
