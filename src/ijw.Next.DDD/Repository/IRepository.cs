using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.DDD {
    /// <summary>
    /// 仓储接口, 定义公共的泛型GRUD
    /// </summary>
    /// <typeparam name="TEntity">泛型聚合根, 因为在DDD里面仓储只能对聚合根做操作</typeparam>
    public interface IRepository<TEntity> where TEntity : AggregateRootBase {
        #region 属性
        /// <summary>
        /// 实体
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Find(Guid key);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="shouldCommit"></param>
        /// <returns></returns>
        int Insert(TEntity entity, bool shouldCommit);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="shouldCommit"></param>
        /// <returns></returns>
        int Insert(IEnumerable<TEntity> entities, bool shouldCommit);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shouldCommit"></param>
        /// <returns></returns>
        int Delete(Guid id, bool shouldCommit);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="shouldCommit"></param>
        /// <returns></returns>
        int Delete(TEntity entity, bool shouldCommit);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="shouldCommit"></param>
        /// <returns></returns>
        int Delete(IEnumerable<TEntity> entities, bool shouldCommit);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="shouldCommit"></param>
        /// <returns></returns>
        int Update(TEntity entity, bool shouldCommit);
        #endregion
    }
}
