Imports Microsoft.VisualBasic
Imports DevExpress.DataAccess.Sql
Imports System.Data


Public Class MyDBSchemaProvider
    Implements IDBSchemaProvider

    Public Function GetSchema(connection As DevExpress.DataAccess.Sql.SqlDataConnection) As DevExpress.DataAccess.Sql.DBSchema Implements DevExpress.DataAccess.Sql.IDBSchemaProvider.GetSchema

        ' Load DB Schema without loading columns. 
        Dim defaultSchema As DBSchema = connection.GetDBSchema(False)

        ' Select only required tables/views/procedures. 
        Dim tables As DevExpress.Xpo.DB.DBTable() = defaultSchema.Tables.Where(Function(table)
                                                                                   Return table.Name.StartsWith("Olap")

                                                                               End Function).ToArray()
        Dim views As DevExpress.Xpo.DB.DBTable() = defaultSchema.Views.Where(Function(view)
                                                                                 Return view.Name.StartsWith("Olap")

                                                                             End Function).ToArray()
        Dim storedProcedures As DevExpress.Xpo.DB.DBStoredProcedure() = defaultSchema.StoredProcedures.Where(Function(storedProcedure)
                                                                                                                 Return storedProcedure.Arguments.Count = 0

                                                                                                             End Function).ToArray()

        ' Create a new schema. 
        Return New DBSchema(tables, views, storedProcedures)
    End Function

    Public Sub LoadColumns(connection As DevExpress.DataAccess.Sql.SqlDataConnection, ParamArray tables() As DevExpress.Xpo.DB.DBTable) Implements DevExpress.DataAccess.Sql.IDBSchemaProvider.LoadColumns
        ' Load columns for current tables. 
        connection.LoadDBColumns(tables)
    End Sub

    
End Class
