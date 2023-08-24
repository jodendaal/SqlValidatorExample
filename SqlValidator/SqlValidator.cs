using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlValidator
{
    public class SqlValidator
    {

        public SqlValidationResult IsValidSelectStatement(string sql)
        {
            TSql100Parser parser = new TSql100Parser(false);
            TSqlFragment fragment;
            IList<ParseError> errors;
            List<string> errorList = new List<string>();
            fragment = parser.Parse(new StringReader(sql), out errors);

            var Script = fragment as TSqlScript;

            foreach (var token in Script.Batches)
            {
                var selectStatement = token.Statements[0] as SelectStatement;
                if(selectStatement == null || selectStatement.Into != null)
                {
                    errorList.Add($"Invalid select statement");
                    return new SqlValidationResult(errorList);
                }
            }

            if (errors != null && errors.Count > 0)
            {

                foreach (var error in errors)
                {
                    errorList.Add(error.Message);
                }
            }

            if (errorList.Count > 0)
            {
                return new SqlValidationResult(errorList);
            }
            return new SqlValidationResult(true);
        }
    }
}
