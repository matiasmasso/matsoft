Public Class Menu_SurveyQuestion

    Inherits Menu_Base

    Private _SurveyQuestions As List(Of DTOSurveyQuestion)
    Private _SurveyQuestion As DTOSurveyQuestion


    Public Sub New(ByVal oSurveyQuestions As List(Of DTOSurveyQuestion))
        MyBase.New()
        _SurveyQuestions = oSurveyQuestions
        If _SurveyQuestions IsNot Nothing Then
            If _SurveyQuestions.Count > 0 Then
                _SurveyQuestion = _SurveyQuestions.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSurveyQuestion As DTOSurveyQuestion)
        MyBase.New()
        _SurveyQuestion = oSurveyQuestion
        _SurveyQuestions = New List(Of DTOSurveyQuestion)
        If _SurveyQuestion IsNot Nothing Then
            _SurveyQuestions.Add(_SurveyQuestion)
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
        oMenuItem.Enabled = _SurveyQuestions.Count = 1
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
        Dim oFrm As New Frm_SurveyQuestion(_SurveyQuestion)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLSurveyQuestion.Delete(_SurveyQuestions.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


