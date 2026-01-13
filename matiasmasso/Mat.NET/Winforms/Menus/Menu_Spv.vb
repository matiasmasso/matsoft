

Public Class Menu_Spv
    Private _Spvs As List(Of DTOSpv)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oSpvs As List(Of DTOSpv))
        MyBase.New()
        _Spvs = oSpvs
    End Sub
    Public Sub New(ByVal oSpv As DTOSpv)
        MyBase.New()
        _Spvs = New List(Of DTOSpv)
        _Spvs.Add(oSpv)
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
         MenuItem_TrpAlb(),
         MenuItem_MailLabel(),
       MenuItem_Del()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oMenuItem.Enabled = _Spvs.Count = 1
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        Return oMenuItem
    End Function

    Private Function MenuItem_TrpAlb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_TrpAlb
        oMenuItem.Enabled = _Spvs.Count = 1
        oMenuItem.Text = "Albarà recollida transportista"
        Return oMenuItem
    End Function

    Private Function MenuItem_MailLabel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_MailLabel
        oMenuItem.Enabled = _Spvs.Count = 1
        oMenuItem.Text = "etiqueta per e-mail"
        oMenuItem.Image = My.Resources.MailSobreGroc
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = _Spvs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Del
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.DelText
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Spv(_Spvs.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_TrpAlb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oTrp = FEB2.Default.ContactSync(GlobalVariables.Emp, DTODefault.Codis.SpvTrp, exs)
        Dim oTaller = FEB2.Default.ContactSync(GlobalVariables.Emp, DTODefault.Codis.Taller, exs)

        Dim oPdfStream As Byte() = LegacyHelper.PdfSpvCustomerDelivery.Factory(exs, _Spvs.First, oTrp, oTaller)
        If exs.Count = 0 Then
            Dim oDocfile = LegacyHelper.DocfileHelper.Factory(exs, oPdfStream, MimeCods.Pdf)
            If exs.Count = 0 Then
                Dim sFilename As String = "albaran de entrega a transportista reparacion " & _Spvs.First.id & ".pdf"
                If Not Await UIHelper.ShowStreamAsync(exs, oDocfile, sFilename) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailLabel(ByVal sender As Object, ByVal e As System.EventArgs)
        _Spvs.First.labelEmailedTo = ""

        Dim exs As New List(Of Exception)
        Dim oMailMessage = Await SpvMailMessageHelper.MailMessage(GlobalVariables.Emp, _Spvs.First, exs)
        If exs.Count = 0 Then
            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "error al redactar el missatge")
        End If

    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem la reparació num." & _Spvs.First.id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Spv.Delete(_Spvs.First, exs) Then
                MsgBox("Reparació num." & _Spvs.First.id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                MsgBox("Operació cancelada." & vbCrLf & "La reparació ja ha sortit." & vbCrLf & "Cal eliminar primer l'albará", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub



    '=======================================================================


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
