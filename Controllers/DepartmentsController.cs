using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homework1.Models;

namespace homework1.Controllers_
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public DepartmentsController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.ToListAsync();
        }

        // GET: api/departments/5/courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<Department>> GetDepartmentCourses(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Entry(department).Collection(p => p.Course).Load();

            return department;
        }

        // GET: api/Departments/5/num
        [HttpGet("{id}/num")]
        public async Task<ActionResult<IList<部門課程數量表>>> Get部門課程數量表(int id)
        {
            var department = await _context.部門課程數量表.FromSqlInterpolated($@"SELECT
                    DepartmentId as Id,
                    Name,
                    (SELECT COUNT(*) FROM dbo.Course c WHERE c.DepartmentID = d.DepartmentId) as num
                FROM
                    dbo.Department d
                WHERE d.DepartmentId = {id}").ToListAsync();

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // GET: api/Departments/5/num
        [HttpGet("num")]
        public async Task<ActionResult<IList<部門課程數量表>>> Get部門課程數量表All(int id)
        {
            var department = await _context.部門課程數量表.FromSqlInterpolated($@"SELECT
                    0 as Id,
                    Name,
                    (SELECT COUNT(*) FROM dbo.Course c WHERE c.DepartmentID = d.DepartmentId) as num
                FROM
                    dbo.Department d").ToListAsync();

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
