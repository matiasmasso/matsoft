Public Class Menu_SatRecall

    Inherits Menu_Base

    Private _SatRecalls As List(Of DTOSatRecall)
    Private _SatRecall As DTOSatRecall

    Public Sub New(ByVal oSatRecalls As List(Of DTOSatRecall))
        MyBase.New()
        _SatRecalls = oSatRecalls
        If _SatRecalls IsNot Nothing Then
            If _SatRecalls.Count > 0 Then
                _SatRecall = _SatRecalls.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSatRecall As DTOSatRecall)
        MyBase.New()
        _SatRecall = oSatRecall
        _SatRecalls = New List(Of DTOSatRecall)
        If _SatRecall IsNot Nothing Then
            _SatRecalls.Add(_SatRecall)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Incidencia)
        MyBase.AddMenuItem(MenuItem_EmailCustomer)
        MyBase.AddMenuItem(MenuItem_EmailManufacturer)
        MyBase.AddMenuItem(MenuItem_PickUpLabel)
        MyBase.AddMenuItem(MenuItem_Plantilla)
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _SatRecalls.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Incidencia() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencia"
        oMenuItem.Enabled = _SatRecalls.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ShowIncidencia
        Return oMenuItem
    End Function

    Private Function MenuItem_Plantilla() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Formulari fabricant"
        oMenuItem.Enabled = _SatRecalls.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Plantilla
        Return oMenuItem
    End Function


    Private Function MenuItem_EmailCustomer() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email al client"
        oMenuItem.Enabled = _SatRecalls.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_EmailCustomer
        Return oMenuItem
    End Function

    Private Function MenuItem_EmailManufacturer() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email al fabricant"
        oMenuItem.Enabled = _SatRecalls.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_EmailManufacturer
        Return oMenuItem
    End Function

    Private Function MenuItem_PickUpLabel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Etiqueta recullida"
        oMenuItem.Enabled = _SatRecalls.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_PickUpLabel
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_SatRecall(_SatRecall)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ShowIncidencia()
        Dim oFrm As New Frm_Incidencia(_SatRecall.Incidencia)
        oFrm.Show()
    End Sub

    Private Sub Do_Plantilla(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sFilename As String = ""
        If FEB.SatRecall.Load(_SatRecall, exs) Then
            If LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, sFilename, exs) Then
                Process.Start(sFilename)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_EmailCustomer()
        Dim exs As New List(Of Exception)
        Dim recallFormFilename As String = ""
        FEB.SatRecall.Load(_SatRecall, exs)
        FEB.Incidencia.Load(exs, _SatRecall.Incidencia)
        FEB.Product.Load(_SatRecall.Incidencia.product, exs)

        LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, recallFormFilename, exs)
        If exs.Count = 0 Then

            Dim oMailMessage = Await FEB.SatRecall.MailMessageToCustomer(_SatRecall, recallFormFilename, exs)
            If exs.Count = 0 Then
                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs, "error al redactar el missatge")
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_EmailManufacturer()
        Dim exs As New List(Of Exception)
        Dim recallFormFilename As String = ""
        FEB.SatRecall.Load(_SatRecall, exs)
        FEB.Incidencia.Load(exs, _SatRecall.Incidencia)
        FEB.Product.Load(_SatRecall.Incidencia.product, exs)
        LegacyHelper.SatRecallLegacyHelper.FillForm(_SatRecall, recallFormFilename, exs)
        If exs.Count = 0 Then
            Dim oMailMessage As DTOMailMessage = Await FEB.SatRecall.MailMessageToManufacturer(GlobalVariables.Emp, _SatRecall, recallFormFilename, exs)
            If exs.Count = 0 Then
                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs, "error al redactar el missatge")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_PickUpLabel()
        Dim exs As New List(Of Exception)
        If FEB.SatRecall.Load(_SatRecall, exs) Then
            Dim stream = LegacyHelper.PdfRecallLabel.Factory(_SatRecalls.First)
            UIHelper.ShowPdf(stream, "Etiqueta recogida")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.SatRecall.Delete(_SatRecalls.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


