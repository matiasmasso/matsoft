Public Class Menu_Amortization

    Inherits Menu_Base

    Private _Amortizations As List(Of DTOAmortization)
    Private _Amortization As DTOAmortization


    Public Sub New(ByVal oAmortizations As List(Of DTOAmortization))
        MyBase.New()
        _Amortizations = oAmortizations
        If _Amortizations IsNot Nothing Then
            If _Amortizations.Count > 0 Then
                _Amortization = _Amortizations.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oAmortization As DTOAmortization)
        MyBase.New()
        _Amortization = oAmortization
        _Amortizations = New List(Of DTOAmortization)
        If _Amortization IsNot Nothing Then
            _Amortizations.Add(_Amortization)
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Amortitza(),
        MenuItem_Baixa(),
        MenuItem_RevertBaixa(),
        MenuItem_Delete()})
    End Function



    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Amortizations.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Amortitza() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Amortitza"
        'oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Amortitza
        Return oMenuItem
    End Function

    Private Function MenuItem_Baixa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Donar de baixa"
        oMenuItem.Visible = _Amortization.Baixa Is Nothing
        AddHandler oMenuItem.Click, AddressOf Do_Baixa
        Return oMenuItem
    End Function

    Private Function MenuItem_RevertBaixa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir baixa"
        oMenuItem.Visible = _Amortization.Baixa IsNot Nothing
        AddHandler oMenuItem.Click, AddressOf Do_RevertBaixa
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
        Dim oFrm As New Frm_Amortization(_Amortization)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Amortitza(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oExercici As DTOExercici = DTOExercici.Past(Current.Session.Emp)
        Dim exs As New List(Of Exception)
        If Await FEB2.Amortization.Amortitza(exs, Current.Session.User, oExercici.Emp, oExercici.Year, _Amortization) Then
            MyBase.RefreshRequest(Me, New MatEventArgs(_Amortization))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Baixa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim item = DTOAmortization.BaixaItem(_Amortization)
        Dim oFrm As New Frm_AmortizationItem(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_RevertBaixa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Retrocedim aquesta baixa ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            Dim item As DTOAmortizationItem = _Amortization.Items.Find(Function(x) x.Cod = DTOAmortizationItem.Cods.Baixa)
            If item IsNot Nothing Then
                If Await FEB2.AmortizationItem.Delete(item, exs) Then
                    MyBase.RefreshRequest(Me, MatEventArgs.Empty)
                Else
                    UIHelper.WarnError(exs, "error al retrocedir la baixa")
                End If
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Amortization.Delete(_Amortizations.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


