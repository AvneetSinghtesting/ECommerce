using ECommerceAPI.DataAccess;
using ECommerceAPI.Model;
using ECommerceAPI.Repository.IRepository;

namespace ECommerceAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public void CreateEmp(Employee employee)
        {
            _context.Employees.Add(employee);
            SaveUpdate();
        }

        public void DeleteEmp(int id)
        {

            _context.Remove(_context.Employees.FirstOrDefault(i=>i.id==id));
            SaveUpdate();
        }

        public IEnumerable<Employee> GetAllEmp()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.FirstOrDefault(x=>x.id==id);
        }

        public void UpdateEmp(Employee employee)
        {
            _context.Update(employee);
            SaveUpdate();
        }

        public void SaveUpdate()
        {
            _context.SaveChanges();
        }
    }
}
