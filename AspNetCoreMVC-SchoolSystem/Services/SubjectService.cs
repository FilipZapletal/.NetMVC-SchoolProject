using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;

namespace AspNetCoreMVC_SchoolSystem.Services {
    public class SubjectService {
        ApplicationDbContext _dbContext;

        public SubjectService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<SubjectDTO> GetAll() {
            var allSubjects = _dbContext.Subjects.ToList();
            var subjectDtos = new List<SubjectDTO>();
            foreach (var subject in allSubjects) {
                SubjectDTO subjectDTO = ModelToDto(subject);
                subjectDtos.Add(subjectDTO);
            }
            return subjectDtos;
        }

        internal async Task CreateAsync(SubjectDTO newSubject) {
            Subject subjectToSave = DtoToModel(newSubject);
            await _dbContext.Subjects.AddAsync(subjectToSave);
            await _dbContext.SaveChangesAsync();
        }

        private Subject DtoToModel(SubjectDTO newSubject) {
            return new Subject {
                Id = newSubject.Id,
                Name = newSubject.Name,
            };
        }

        internal async Task<SubjectDTO> GetByIdAsync(int id) {
            var subjectToEdit = await _dbContext.Subjects.FindAsync(id);
            if (subjectToEdit==null) {
                return null;
            }
            return ModelToDto(subjectToEdit);
        }

        internal async Task UpdateAsync(SubjectDTO subjectDTO, int id) {
            _dbContext.Update(DtoToModel(subjectDTO));
            await _dbContext.SaveChangesAsync();
        }

        private SubjectDTO ModelToDto(Subject? subject) {
            return new SubjectDTO() {
                Id = subject.Id,
                Name = subject.Name,
            };
        }

        internal async Task DeleteAsync(int id) {
            var subjectToDelete = await _dbContext.Subjects.FindAsync(id);
            if (subjectToDelete != null) {
                _dbContext.Subjects.Remove(subjectToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
