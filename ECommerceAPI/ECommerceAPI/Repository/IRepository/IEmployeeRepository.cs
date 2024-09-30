using ECommerceAPI.Model;

namespace ECommerceAPI.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAllEmp();
        public Employee GetById(int id);
        public void CreateEmp(Employee employee);
        public void UpdateEmp(Employee employee);
        public void DeleteEmp(int id);
    }
}
