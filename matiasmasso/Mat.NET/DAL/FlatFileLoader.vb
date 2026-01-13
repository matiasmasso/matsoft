Public Class FlatFileLoader
    Shared Function Find(oId As DTOFlatFile.Ids) As DTOFlatFile
        Dim retval As DTOFlatFile = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT [File].Nom AS FileNom, Reg.RegId, Reg.Nom as RegNom, Reg.Regex ")
        sb.AppendLine(",Field.Lin as FieldId, Field.Nom as FieldNom, Field.Len, Field.[Format], Field.[Default] ")
        sb.AppendLine("FROM FlatFileFixLen [File] ")
        sb.AppendLine("LEFT OUTER JOIN FlatFileFixLen_Regs Reg ON Reg.FF = [File].Id ")
        sb.AppendLine("LEFT OUTER JOIN FlatFileFixLen_Fields Field ON Field.FF = Reg.FF AND Field.Reg = Reg.RegId ")
        sb.AppendLine("WHERE [File].Id=@FileId ")
        sb.AppendLine("ORDER BY Reg.RegId, Field.Lin")

        Dim oReg As New DTOFlatReg
        oReg.Id = -1
        Dim iOffset As Integer
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@FileId", CInt(oId))
        Do While oDrd.Read
            If retval Is Nothing Then
                retval = New DTOFlatFile(oId)
                With retval
                    .Nom = oDrd("FileNom")
                    .Regs = New List(Of DTOFlatReg)
                End With
            End If

            If Not IsDBNull(oDrd("RegId")) Then
                If oReg.Id <> oDrd("RegId") Then
                    oReg = New DTOFlatReg
                    With oReg
                        .Id = oDrd("RegId")
                        .Nom = oDrd("RegNom")
                        .Regex = oDrd("Regex")
                        .Fields = New List(Of DTOFlatField)
                    End With
                    retval.Regs.Add(oReg)
                    iOffset = 0
                End If

                Dim oField As New DTOFlatField
                With oField
                    .Id = oDrd("FieldId")
                    .Nom = oDrd("FieldNom")
                    .Length = oDrd("Len")
                    .Offset = iOffset
                    .Format = oDrd("Format")
                    If Not IsDBNull(oDrd("Default")) Then
                        .DefaultValue = oDrd("Default")
                    End If
                    iOffset += .Length
                End With
                oReg.Fields.Add(oField)
            End If
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class
Public Class FlatFilesLoader

    Shared Function FromFirstLineRegex(sLine As String) As List(Of DTOFlatFile.Ids)
        Dim retval As New List(Of DTOFlatFile.Ids)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT FF, Regex ")
        sb.AppendLine("FROM FlatFileFixLen_Regs ")
        sb.AppendLine("WHERE RegId = 1 AND Regex IS NOT NULL")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim pattern As String = oDrd("Regex").ToString
            Dim oMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(sLine, pattern)
            If oMatch.Success Then
                retval.Add(CInt(oDrd("FF")))
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
