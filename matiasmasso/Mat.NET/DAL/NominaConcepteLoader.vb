Public Class NominaConcepteLoader

End Class

Public Class NominaConceptesLoader

    Shared Function All(oTrans As SqlTransaction) As List(Of DTONomina.Concepte)
        Dim retval As New List(Of DTONomina.Concepte)
        Dim SQL As String = "SELECT Id, Concepte FROM NominaConcepte ORDER BY Id"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            Dim item As New DTONomina.Concepte(CInt(oRow("Id")), oRow("Concepte").ToString())
            retval.Add(item)
        Next
        Return retval
    End Function

End Class
