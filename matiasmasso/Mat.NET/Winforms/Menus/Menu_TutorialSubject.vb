Public Class Menu_TutorialSubject

    Inherits Menu_Base

    Private _TutorialSubjects As List(Of DTOTutorialSubject)
    Private _TutorialSubject As DTOTutorialSubject


    Public Sub New(ByVal oTutorialSubjects As List(Of DTOTutorialSubject))
        MyBase.New()
        _TutorialSubjects = oTutorialSubjects
        If _TutorialSubjects IsNot Nothing Then
            If _TutorialSubjects.Count > 0 Then
                _TutorialSubject = _TutorialSubjects.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oTutorialSubject As DTOTutorialSubject)
        MyBase.New()
        _TutorialSubject = oTutorialSubject
        _TutorialSubjects = New List(Of DTOTutorialSubject)
        If _TutorialSubject IsNot Nothing Then
            _TutorialSubjects.Add(_TutorialSubject)
        End If
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Tutorials())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _TutorialSubjects.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Tutorials() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Tutorials"
        oMenuItem.Enabled = _TutorialSubjects.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Tutorials
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Browse"
        oMenuItem.Enabled = _TutorialSubjects.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
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
        Dim oFrm As New Frm_TutorialSubject(_TutorialSubject)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Tutorials(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Tutorials(_TutorialSubject)
        oFrm.Show()
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = BLLTutorialSubject.Url(_TutorialSubject, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLTutorialSubject.Delete(_TutorialSubjects.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

