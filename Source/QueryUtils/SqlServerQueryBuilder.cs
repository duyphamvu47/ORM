using ORM.QueryUtils.QueryUtilsEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.QueryUtils
{
    class SqlServerQueryBuilder : IQueryBuilder
    {
        private StringBuilder _whereStringBuilder { get; set; }

        public SqlServerQueryBuilder()
        {
            this._whereStringBuilder = new StringBuilder();
        }

        public SqlServerQueryBuilder(IQueryBuilder queryBuilder)
        {
            this._whereStringBuilder = new StringBuilder();
            this._whereStringBuilder.Append(queryBuilder.getWhereBuilder());
        }

        public IQueryBuilder where<Tval>(string colName, WhereOperatorEnum op, Tval value)
        {
            if (!typeof(WhereOperatorEnum).IsEnum)
            {
                throw new ArgumentException("operatorType must be enumerated type");
            }

            if (!isCanAddWhereOperator())
            {
                throw new Exception("Where condition with operator can only be added 1 time");
            }

            this._whereStringBuilder.Append(colName);
            this._whereStringBuilder.Append(op.GetEnumDescription());

            this._whereStringBuilder.Append(getSqlValueString(value));

            return clone();
        }

        public IQueryBuilder whereAnd(IQueryBuilder query)
        {
            if (this._whereStringBuilder.Length > 0)
            {
                var leftWhere = new StringBuilder();
                leftWhere.Append(_whereStringBuilder);

                this._whereStringBuilder.Clear();

                this._whereStringBuilder.Append("(");
                this._whereStringBuilder.Append(leftWhere);
                this._whereStringBuilder.Append(")");
                this._whereStringBuilder.Append(" AND ");

                var rightWhere = query.getWhereBuilder();

                this._whereStringBuilder.Append("(");
                this._whereStringBuilder.Append(rightWhere);
                this._whereStringBuilder.Append(")");
            } else
            {
                this._whereStringBuilder.Append(query.getWhereBuilder());
            }

            return clone();
        }

        public IQueryBuilder whereIn<T>(string colName, T[] list)
        {
            if (!isCanAddWhereOperator())
            {
                throw new Exception("Where condition with Operator can only be use 1 time");
            }

            if (list == null || !list.Any())
            {
                throw new Exception("List value is empty");
            }

            this._whereStringBuilder.Append(colName);
            this._whereStringBuilder.Append(" IN (");


            for (int i = 0; i < list.Length; i++)
            {
                var item = list[i];

                if (i > 0)
                {
                    this._whereStringBuilder.Append(",");
                }

                this._whereStringBuilder.Append(getSqlValueString(item));
            }

            this._whereStringBuilder.Append(")");

            return clone();
        }

        public IQueryBuilder whereOr(IQueryBuilder query)
        {
            if (this._whereStringBuilder.Length > 0)
            {
                var leftWhere = new StringBuilder();
                leftWhere.Append(_whereStringBuilder);

                this._whereStringBuilder.Clear();

                this._whereStringBuilder.Append("(");
                this._whereStringBuilder.Append(leftWhere);
                this._whereStringBuilder.Append(")");
                this._whereStringBuilder.Append(" OR ");

                var rightWhere = query.getWhereBuilder();

                this._whereStringBuilder.Append("(");
                this._whereStringBuilder.Append(rightWhere);
                this._whereStringBuilder.Append(")");
            } else
            {
                this._whereStringBuilder.Append(query.getWhereBuilder());
            }

            return clone();
        }

        public IQueryBuilder whereCustomOperator(string customClause)
        {
            if (!isCanAddWhereOperator())
            {
                throw new Exception("Where condition with operator can only be added 1 time");
            }

            this._whereStringBuilder.Append(customClause);

            return clone();
        }

        public StringBuilder getWhereBuilder()
        {
            return this._whereStringBuilder;
        }

        public string ToWhereString()
        {
            return this._whereStringBuilder.ToString();
        }

        private StringBuilder getSqlValueString<Tval>(Tval value)
        {
            StringBuilder strBuilder = new StringBuilder();

            if (typeof(Tval) == typeof(string))
            {
                strBuilder.Append("'");
                strBuilder.Append(value);
                strBuilder.Append("'");
            }
            else
            {
                strBuilder.Append(value);
            }

            return strBuilder;
        }

        private IQueryBuilder clone()
        {
            return new SqlServerQueryBuilder(this);
        }

        private bool isCanAddWhereOperator()
        {
            return this._whereStringBuilder.Length < 1;
        }
    }
}
