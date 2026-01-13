Public Class FlatRegLoader

    Shared Function Find(FileId As DTOFlatFile.Ids, RegId As Integer) As DTOFlatReg
        Dim retval As DTOFlatReg = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Reg.RegId, Reg.Nom as RegNom ")
        sb.AppendLine(",Field.Lin as FieldId, Field.Nom as FieldNom, Field.Len, Field.Format, Field.[Default] ")
        sb.AppendLine("FROM FlatFileFixLen_Regs Reg ")
        sb.AppendLine("LEFT OUTER JOIN FlatFileFixLen_Fields Field ON Field.FF = Reg.FF AND Field.Reg = Reg.RegId ")
        sb.AppendLine("WHERE Reg.FF=@FileId AND Reg.RegId=@RegId ")
        sb.AppendLine("ORDER BY Field.Lin")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@FileId", CInt(FileId), "@RegId", RegId)
        Do While oDrd.Read
            If retval Is Nothing Then
                retval = New DTOFlatReg
                With retval
                    .Id = oDrd("RegId")
                    .Nom = oDrd("RegNom")
                    .Fields = New List(Of DTOFlatField)
                End With
            End If
            Dim oField As New DTOFlatField
            With oField
                .Id = oDrd("FieldId")
                .Nom = oDrd("FieldNom")
                .Length = oDrd("Len")
                .Format = oDrd("Format")
                If Not IsDBNull(oDrd("Default")) Then
                    .DefaultValue = oDrd("Default")
                End If
            End With
            retval.Fields.Add(oField)
        Loop

        oDrd.Close()
        Return retval
    End Function

End Class
