using ORM.QueryUtils.QueryUtilsEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    interface IQueryBuilder
    {
        public IQueryBuilder whereAnd(IQueryBuilder query);
        public IQueryBuilder whereOr(IQueryBuilder query);
        public IQueryBuilder where<Tval>(string colName, WhereOperatorEnum op, Tval value);
        public IQueryBuilder whereIn<T>(string colName, T[] list);
        public IQueryBuilder whereCustomOperator(string customClause);
        public StringBuilder getWhereBuilder();
        public string ToWhereString();
    }
}
