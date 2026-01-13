Public Class Menu_SurveyAnswer

    Inherits Menu_Base

    Private _SurveyAnswers As List(Of DTOSurveyAnswer)
    Private _SurveyAnswer As DTOSurveyAnswer


    Public Sub New(ByVal oSurveyAnswers As List(Of DTOSurveyAnswer))
        MyBase.New()
        _SurveyAnswers = oSurveyAnswers
        If _SurveyAnswers IsNot Nothing Then
            If _SurveyAnswers.Count > 0 Then
                _SurveyAnswer = _SurveyAnswers.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSurveyAnswer As DTOSurveyAnswer)
        MyBase.New()
        _SurveyAnswer = oSurveyAnswer
        _SurveyAnswers = New List(Of DTOSurveyAnswer)
        If _SurveyAnswer IsNot Nothing Then
            _SurveyAnswers.Add(_SurveyAnswer)
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
        oMenuItem.Enabled = _SurveyAnswers.Count = 1
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
        Dim oFrm As New Frm_SurveyAnswer(_SurveyAnswer)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLSurveyAnswer.Delete(_SurveyAnswers.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


