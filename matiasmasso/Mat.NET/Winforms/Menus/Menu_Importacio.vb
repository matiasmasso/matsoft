

Public Class Menu_Importacio
    Private _Importacio As DTOImportacio
    Private _Importacions As List(Of DTOImportacio)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oImportacions As List(Of DTOImportacio))
        MyBase.New()
        _Importacions = oImportacions
        If _Importacions.Count > 0 Then
            _Importacio = _Importacions.First
        End If
    End Sub

    Public Sub New(ByVal oImportacio As DTOImportacio)
        MyBase.New()
        _Importacio = oImportacio
        _Importacions = New List(Of DTOImportacio)
        _Importacions.Add(_Importacio)
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_PrevisioSkus(),
        MenuItem_NewEntrada(),
        MenuItem_NewImportacio(),
        MenuItem_NewFra(),
        MenuItem_ExcelRemeses(),
        MenuItem_MailOrdreDeCarrega(),
        MenuItem_ExcelGoods(),
        MenuItem_Del()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_PrevisioSkus() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Qué es lo que arriba?"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_PrevisioSkus
        Return oMenuItem
    End Function


    Private Function MenuItem_NewEntrada() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "albará d'entrada"
        oMenuItem.Image = My.Resources.notepad
        AddHandler oMenuItem.Click, AddressOf Do_NewEntrada
        Return oMenuItem
    End Function

    Private Function MenuItem_NewFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "entrada factura"
        oMenuItem.Visible = False 'tret perque per aqui no ho assigna a la remesa. Cal arrossegar el PDF a la remesa corresponent del llistat de remeses de importacio per posar en marxa el proces
        oMenuItem.Image = My.Resources.NewDoc
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelRemeses() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel remeses"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelRemeses
        Return oMenuItem
    End Function

    Private Function MenuItem_MailOrdreDeCarrega() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email Ordre de Càrrega"
        AddHandler oMenuItem.Click, AddressOf Do_MailOrdreDeCarrega
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelGoods() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel mercancía"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelGoods
        Return oMenuItem
    End Function


    Private Function MenuItem_NewImportacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Nova importacio de " & _Importacio.Proveidor.NomComercialOrDefault()
        AddHandler oMenuItem.Click, AddressOf Do_NewImportacio
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Importacio(_Importacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_PrevisioSkus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Importacio.Load(exs, _Importacio) Then
            Dim oFrm As New Frm_Importacio(_Importacio, Frm_Importacio.Tabs.Previsio)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem remesa " & _Importacio.Id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then

            Dim exs As New List(Of Exception)
            If Await FEB2.Importacio.Delete(_Importacio, exs) Then
                MsgBox("remesa " & _Importacio.Id & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                MsgBox("error al eliminar el document de la remesa" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_NewEntrada(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = _Importacio.proveidor
        If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oContact, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oFrm As New Frm_AlbNew2(_Importacio)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Wz_Proveidor_NewFra(_Importacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_NewImportacio(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If _Importacio.Proveidor IsNot Nothing Then FEB2.Proveidor.Load(_Importacio.Proveidor, exs)
        Dim oImportacio = DTOImportacio.Factory(GlobalVariables.Emp, _Importacio.Proveidor)
        Dim oFrm As New Frm_Importacio(oImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ExcelRemeses(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSheet = DTOImportacio.Excel(_Importacions, Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailOrdreDeCarrega()
        Dim exs As New List(Of Exception)
        Dim oMailMessage = Await FEB2.Importacio.MailMessageOrdenDeCarga(exs, Current.Session.Emp, _Importacio)
        If exs.Count = 0 Then
            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_ExcelGoods(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB2.Importacio.ExcelGoods(exs, _Importacio, Current.Session.Lang)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    '===================================================================


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
