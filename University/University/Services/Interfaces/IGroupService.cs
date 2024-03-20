namespace University.Services.Interfaces
{
    public interface IGroupService
    {
        Task<Groups?> GetGroupByIdAsync(int id);
        Task<Groups?> GetAllGroupDataAsync(int id);
        Task<IList<Groups>> ListAsync();
        Task<IList<Groups>> ListByCurriculumIdAsync(int curriculumId);
        Task<int> AddAsync(Groups group);
        Task<int> UpdateAsync(Groups group);
        Task<int> DeleteAsync(int id);
        Task ExportGroupToPdf(Groups group, string filePath);
        Task ExportGroupToDocx(Groups group, string selectedPath);
        Task<IList<Groups>> FilterByNameListAsync(string filterName);
    }
}
