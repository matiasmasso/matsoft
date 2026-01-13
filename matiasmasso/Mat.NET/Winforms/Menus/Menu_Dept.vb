Public Class Menu_Dept
    Inherits Menu_Base

    Private _Depts As List(Of DTODept)
    Private _Dept As DTODept

    Public Sub New(ByVal oDepts As List(Of DTODept))
        MyBase.New()
        _Depts = oDepts
        If _Depts IsNot Nothing Then
            If _Depts.Count > 0 Then
                _Dept = _Depts.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oDept As DTODept)
        MyBase.New()
        _Dept = oDept
        _Depts = New List(Of DTODept)
        If _Dept IsNot Nothing Then
            _Depts.Add(_Dept)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_LangText())
        MyBase.AddMenuItem(MenuItem_LandingPage())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Depts.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_LangText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = Current.Session.Tradueix("nombres y descripciones", "noms i descripcions", "names & descriptions")
        AddHandler oMenuItem.Click, AddressOf Do_LangText
        Return oMenuItem
    End Function


    Private Function MenuItem_LandingPage() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Landing page"
        oMenuItem.Enabled = _Depts.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_LandingPage
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
        Dim oFrm As New Frm_Dept(_Dept)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_LandingPage(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_LangResource(_Dept, DTOLangText.Srcs.ProductText)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Dept.Delete(_Depts.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_LangText()
        Dim oFrm As New Frm_ProductDescription(_Depts.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
End Class

