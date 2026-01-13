Module root
    Public PRINT_PREVIEWS As Boolean '= False 'True ' False 'evita impresora en development process
    Public Const EmpId = 1
    Public Const MgzId As Long = 5782


    Public Function Organitzacio() As String
        Dim oOrg As DTOContact = GlobalVariables.Emp.Org
        Dim retval As String = oOrg.Nom
        Return retval
    End Function


    Public Sub ShowNewSpvsIn()
        Dim Frm As New frm_NewSpvIn()
        With Frm
            .Show()
        End With
    End Sub


    Public Sub ShowNewSpvOut()
        Dim Frm As New frm_NewSpvOut()
        With Frm
            .Show()
        End With
    End Sub


    Public Sub PrintSpvs(ByVal oSpvs As List(Of DTOSpv))
        Dim rpt As New rpt_SPV(oSpvs)
        If PRINT_PREVIEWS Then
            rpt.Printpreview()
        Else
            rpt.Print()
        End If
    End Sub


    Public Async Sub PrintDelivery(oDelivery As DTODelivery)
        Dim exs As New List(Of Exception)
        Dim oDeliveries = {oDelivery}.ToList()
        Dim oByteArray As Byte() = Await FEB.Deliveries.Pdf(exs, oDeliveries, False)
        If exs.Count = 0 Then
            Dim sFilename = DTODelivery.FileName(oDeliveries.First)
            UIHelper.ShowPdf(oByteArray, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If

        'Dim rpt As New rpt_SPV_Out(oDelivery)
        'If PRINT_PREVIEWS Then
        'rpt.PrintPreview()
        'Else
        'rpt.Print()
        'End If
    End Sub

    Public Sub PrintDeliveryLabel(ByRef oDelivery As DTODelivery)
        Try
            Dim oLabel As New Etiqueta()
            With oLabel
                .Nom = oDelivery.Nom
                .Adr = oDelivery.Address.Text
                .Cit = oDelivery.Address.Zip.Location.Nom
                .Alb = oDelivery.Id
                .Spv = oDelivery.Spvs?.FirstOrDefault?.id
                .Bultos = oDelivery.Bultos
                .Kg = oDelivery.Kg
                .M3 = oDelivery.M3 / 1000
                .Print()
            End With
        Catch ex As Exception
            MsgBox("No s'ha pogut imprimir la etiqueta" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "TALLER")
        End Try

    End Sub



End Module
