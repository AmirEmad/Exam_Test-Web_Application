namespace Exam_Test.Services
{
    public interface IExamServices<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity GetByIdAsync(int id);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Add(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
