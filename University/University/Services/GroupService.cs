namespace University.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _grouprepository;

        public GroupService(IGroupRepository grouprepository)
        {
            _grouprepository = grouprepository;
        }

        public async Task<Groups?> GetGroupByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid group id");

            var group = await _grouprepository.GetAsync(id);

            if (group is null)
                throw new NotFoundException($"Group with id = {id} does not exist");

            return group;
        }

        public async Task<Groups?> GetAllGroupDataAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid group id");

            var group = await _grouprepository.GetAllAsync(id);

            if (group is null)
                throw new NotFoundException($"Group with id = {id} does not exist");

            return group;
        }

        public async Task<IList<Groups>> ListByCurriculumIdAsync(int curriculumId)
        {
            if (curriculumId <= 0)
                throw new ArgumentException("Invalid curriculum id");

            return await _grouprepository.ListByCurriculumIdAsync(curriculumId);
        }

        public async Task<IList<Groups>> ListAsync()
        {
            return await _grouprepository.ListAsync();
        }

        public async Task<int> AddAsync(Groups group)
        {
            ValidateGroup(group);
            var isExist = await _grouprepository.GetByNameAsync(group.GroupName);
            if (!(isExist is null))
                throw new ArgumentException("Group with this name already exists");

            return await _grouprepository.AddAsync(group);
        }

        public async Task<int> UpdateAsync(Groups group)
        {
            ValidateGroup(group);
            return await _grouprepository.UpdateAsync(group);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var group = await _grouprepository.GetAllAsync(id);
            var students = group.Students.Count();
            if (students == 0)
                return await _grouprepository.DeleteAsync(id);

            throw new ArgumentException("Can`t delete a group in which students are connected");
        }

        public async Task ExportGroupToPdf(Groups group, string selectedPath)
        {
            ValidateGroup(group);
            var groupData = await GetAllGroupDataAsync(group.Id);

            string fileName = $"{groupData.Curriculum.Name}_{groupData.GroupName}.pdf";
            string filePath = Path.Combine(selectedPath, fileName);

            var groupStudents = GetStudentsGroupList(groupData);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                var pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
                PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                foreach (var student in groupStudents)
                {
                    pdfDoc.Add(new Paragraph(student));
                }

                pdfDoc.Close();
            }
        }

        public async Task ExportGroupToDocx(Groups group, string selectedPath)
        {
            ValidateGroup(group);
            var groupData = await GetAllGroupDataAsync(group.Id);

            string fileName = $"{groupData.Curriculum.Name}_{groupData.GroupName}.docx";
            string filePath = Path.Combine(selectedPath, fileName);

            var groupStudents = GetStudentsGroupList(groupData);

            using (DocX doc = DocX.Create(filePath))
            {
                foreach (var student in groupStudents)
                {
                    doc.InsertParagraph(student);
                }

                doc.Save();
            }
        }

        public async Task<IList<Groups>> FilterByNameListAsync(string filterName)
        {
            ValidateGroupName(filterName);
            return await _grouprepository.FilterByNameListAsync(filterName);
        }

        private IList<string> GetStudentsGroupList(Groups groupData)
        {
            var students = new List<string>();

            foreach (var student in groupData.Students)
                students.Add($"{student.Human.FirstName} {student.Human.LastName}");

            return students;
        }

        private static void ValidateGroup(Groups group)
        {
            if (group is null)
                throw new ArgumentNullException(nameof(group), "Group is empty");

            ValidateGroupName(group.GroupName);
        }

        private static void ValidateGroupName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentNullException(nameof(groupName), "Group name is empty");
            }
            else 
            {
                groupName = groupName.Trim();

                if (groupName.Length > 10)
                    throw new ArgumentException(nameof(groupName), "Group name must be maximum of 10 characters");
            }
        }
    }
}
