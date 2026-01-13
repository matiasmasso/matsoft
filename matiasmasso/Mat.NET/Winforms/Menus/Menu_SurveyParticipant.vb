Public Class Menu_SurveyParticipant
    Inherits Menu_Base

    Private _SurveyParticipants As List(Of DTOSurveyParticipant)
    Private _SurveyParticipant As DTOSurveyParticipant


    Public Sub New(ByVal oSurveyParticipants As List(Of DTOSurveyParticipant))
        MyBase.New()
        _SurveyParticipants = oSurveyParticipants
        If _SurveyParticipants IsNot Nothing Then
            If _SurveyParticipants.Count > 0 Then
                _SurveyParticipant = _SurveyParticipants.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSurveyParticipant As DTOSurveyParticipant)
        MyBase.New()
        _SurveyParticipant = oSurveyParticipant
        _SurveyParticipants = New List(Of DTOSurveyParticipant)
        If _SurveyParticipant IsNot Nothing Then
            _SurveyParticipants.Add(_SurveyParticipant)
        End If
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_User())
        MyBase.AddMenuItem(MenuItem_Survey())
        MyBase.AddMenuItem(MenuItem_Play())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _SurveyParticipants.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_User() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Usuari"
        Dim oMenuUser As New Menu_User(_SurveyParticipant.User)
        oMenuItem.DropDownItems.AddRange(oMenuUser.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Survey() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enquesta"
        Dim oMenuSurvey As New Menu_Survey(_SurveyParticipant.Survey)
        oMenuItem.DropDownItems.AddRange(oMenuSurvey.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Play() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Participar"
        oMenuItem.Enabled = _SurveyParticipants.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Play
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
        Dim oFrm As New Frm_SurveyParticipant(_SurveyParticipant)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Play(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = BLLSurveyParticipant.Url(_SurveyParticipant, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLSurveyParticipant.Delete(_SurveyParticipants.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

