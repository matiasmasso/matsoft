Public Class Menu_SurveyStep

    Inherits Menu_Base

    Private _SurveySteps As List(Of DTOSurveyStep)
    Private _SurveyStep As DTOSurveyStep


    Public Sub New(ByVal oSurveySteps As List(Of DTOSurveyStep))
        MyBase.New()
        _SurveySteps = oSurveySteps
        If _SurveySteps IsNot Nothing Then
            If _SurveySteps.Count > 0 Then
                _SurveyStep = _SurveySteps.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSurveyStep As DTOSurveyStep)
        MyBase.New()
        _SurveyStep = oSurveyStep
        _SurveySteps = New List(Of DTOSurveyStep)
        If _SurveyStep IsNot Nothing Then
            _SurveySteps.Add(_SurveyStep)
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
        oMenuItem.Enabled = _SurveySteps.Count = 1
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
        Dim oFrm As New Frm_SurveyStep(_SurveyStep)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLSurveyStep.Delete(_SurveySteps.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


