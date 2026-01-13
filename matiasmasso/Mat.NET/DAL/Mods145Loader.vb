Public Class Mods145Loader
    Shared Function GetValues(empId As DTOEmp.Ids) As List(Of DTOMod145)
        Dim retval As New List(Of DTOMod145)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliDoc.Guid, CliDoc.Contact, CliGral.RaoSocial, CliDoc.Fch, CliDoc.Hash ")
        sb.AppendLine("FROM CliDoc ")
        sb.AppendLine("INNER JOIN CliGral ON CliDoc.Contact=CliGral.Guid ")
        sb.AppendLine("WHERE CliDoc.Type=" & CInt(DTOContactDoc.Types.Model_145) & " ")
        sb.AppendLine("ORDER BY CliDoc.Fch DESC, CliGral.RaoSocial")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMod145(oDrd("Guid"))
            With item
                .Titular = New Models.Base.GuidNom(oDrd("Contact"), SQLHelper.GetStringFromDataReader(oDrd("RaoSocial")))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Hash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
