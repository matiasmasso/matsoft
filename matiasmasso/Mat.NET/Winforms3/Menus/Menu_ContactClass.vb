Public Class Menu_ContactClass

    Inherits Menu_Base

    Private _ContactClasses As List(Of DTOContactClass)
    Private _ContactClass As DTOContactClass


    Public Sub New(ByVal oContactClasses As List(Of DTOContactClass))
        MyBase.New()
        _ContactClasses = oContactClasses
        If _ContactClasses IsNot Nothing Then
            If _ContactClasses.Count > 0 Then
                _ContactClass = _ContactClasses.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oContactClass As DTOContactClass)
        MyBase.New()
        _ContactClass = oContactClass
        _ContactClasses = New List(Of DTOContactClass)
        If _ContactClass IsNot Nothing Then
            _ContactClasses.Add(_ContactClass)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _ContactClasses.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
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
        Dim oFrm As New Frm_ContactClass(_ContactClass)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.ContactClass.Delete(_ContactClasses.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


