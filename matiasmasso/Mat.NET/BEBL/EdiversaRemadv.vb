Public Class EdiversaRemadv

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOEdiversaRemadv
        Dim retval As DTOEdiversaRemadv = EdiversaRemadvLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaRemadv As DTOEdiversaRemadv) As Boolean
        Dim retval As Boolean = EdiversaRemadvLoader.Load(oEdiversaRemadv)
        Return retval
    End Function

    Shared Function Update(oEdiversaRemadv As DTOEdiversaRemadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaRemadvLoader.Update(oEdiversaRemadv, exs)
        Return retval
    End Function

    Shared Function Retrocedeix(oEdiversaRemadv As DTOEdiversaRemadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaRemadvLoader.Retrocedeix(oEdiversaRemadv, exs)
        Return retval
    End Function

#End Region

    Shared Function Procesa(oEdiversaFile As DTOEdiversaFile, oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim eExs As New List(Of DTOEdiversaException)
            Dim oEdiversaRemadv As DTOEdiversaRemadv = BEBL.EdiversaRemadv.FromEdiversaFile(oEdiversaFile, eExs)
            If eExs.Count > 0 Then exs.AddRange(DTOEdiversaException.ToSystemExceptions(eExs))

            Dim oPnds As List(Of DTOPnd) = PndsLoader.All(oEmp, oEdiversaRemadv.EmisorPago)

            For Each oItem In oEdiversaRemadv.Items
                oItem.Pnd = oPnds.Find(Function(x) x.FraNum = oItem.Num And x.Fch.Date = oItem.Fch.Date And x.Amt.Eur = oItem.Amt.Eur)
            Next

            retval = EdiversaRemadvLoader.Procesa(oEdiversaFile, oEdiversaRemadv, exs)

        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al procesar ", oEdiversaFile.FileName)))
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function FromEdiversaFile(src As DTOEdiversaFile, exs As List(Of DTOEdiversaException)) As DTOEdiversaRemadv
        Dim retval As New DTOEdiversaRemadv(src.Guid)
        LoadFromEdiversaFile(retval, src, exs)
        Return retval
    End Function

    Shared Sub LoadFromEdiversaFile(ByRef oRemadv As DTOEdiversaRemadv, src As DTOEdiversaFile, exs As List(Of DTOEdiversaException))
        src.LoadSegments()

        oRemadv.Items = New List(Of DTOEdiversaRemadvItem)
        Dim oItem As DTOEdiversaRemadvItem = Nothing
        Dim oCur As DTOCur = Nothing

        Dim sb As New System.Text.StringBuilder
        For Each oSegment As DTOEdiversaSegment In src.Segments
            sb.AppendLine(String.Join("|", oSegment.Fields))
            With oRemadv
                Select Case oSegment.Fields.First
                    Case "BGM"
                        If oSegment.Fields.Count > 1 Then .DocNum = oSegment.Fields(1)
                    Case "DTM"
                        If oSegment.Fields.Count > 1 Then .FchDoc = DTOEdiversaFile.parseFch(oSegment.Fields(1), exs)
                        'If oSegment.Fields.Count > 2 Then .FchEmision = DTOEdiversaFile.parseFch(oSegment.Fields(2), exs)
                        If oSegment.Fields.Count > 3 Then .FchVto = DTOEdiversaFile.parseFch(oSegment.Fields(3), exs)
                    Case "RFF"
                        If oSegment.Fields.Count > 1 Then .DocRef = oSegment.Fields(1)
                    Case "NADPR"
                        If oSegment.Fields.Count > 1 Then .EmisorPago = BEBL.Contact.FromGLN(DTOEan.Factory(oSegment.Fields(1)))
                    Case "NADPE"
                        If oSegment.Fields.Count > 1 Then .ReceptorPago = BEBL.Contact.FromGLN(DTOEan.Factory(oSegment.Fields(1)))
                    Case "CUX"
                        '.Cur = DTOCur.Factory(oSegment.Fields(1))
                    Case "DOC"
                        oItem = New DTOEdiversaRemadvItem(oRemadv)
                        If oSegment.Fields.Count > 1 Then oItem.Type = oSegment.Fields(1)
                        If oSegment.Fields.Count > 2 Then oItem.Nom = oSegment.Fields(2)
                        If oSegment.Fields.Count > 3 Then oItem.Num = oSegment.Fields(3)
                        .Items.Add(oItem)
                    Case "MOALIN"
                        If oSegment.Fields.Count > 1 And Not DTOEdiversaFile.TryParseAmt(oSegment.Fields(1), oItem.Amt, exs) Then
                            .Exceptions.Add(New Exception(oSegment.Fields(1) & " no identificat com a import a segment MOALIN"))
                        End If
                    Case "DTMLIN"
                        If oSegment.Fields.Count > 1 Then oItem.Fch = DTOEdiversaFile.parseFch(CDec(oSegment.Fields(1)), exs)
                    Case "MOARES"
                        If oSegment.Fields.Count > 1 And Not DTOEdiversaFile.TryParseAmt(oSegment.Fields(1), .Amt, exs) Then
                            .Exceptions.Add(New Exception(oSegment.Fields(1) & " no identificat com a import a segment MOARES"))
                        End If
                End Select
            End With

        Next
        Dim s As String = sb.ToString
    End Sub


End Class

Public Class EdiversaRemadvs

    Shared Function All(IncludeProcessed As Boolean) As List(Of DTOEdiversaRemadv)
        Dim retval As List(Of DTOEdiversaRemadv) = EdiversaRemadvsLoader.All(IncludeProcessed)
        Return retval
    End Function


End Class
