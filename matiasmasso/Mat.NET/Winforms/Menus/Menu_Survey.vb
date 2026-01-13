Public Class Menu_Survey
    Inherits Menu_Base

    Private _Surveys As List(Of DTOSurvey)
    Private _Survey As DTOSurvey


    Public Sub New(ByVal oSurveys As List(Of DTOSurvey))
        MyBase.New()
        _Surveys = oSurveys
        If _Surveys IsNot Nothing Then
            If _Surveys.Count > 0 Then
                _Survey = _Surveys.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSurvey As DTOSurvey)
        MyBase.New()
        _Survey = oSurvey
        _Surveys = New List(Of DTOSurvey)
        If _Survey IsNot Nothing Then
            _Surveys.Add(_Survey)
        End If
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Play())
        MyBase.AddMenuItem(MenuItem_ImportUsers())
        MyBase.AddMenuItem(MenuItem_MissingParticipants())
        MyBase.AddMenuItem(MenuItem_Excel())
        MyBase.AddMenuItem(MenuItem_ResetScores())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Surveys.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Play() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Participar en proves"
        oMenuItem.Enabled = _Surveys.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Play
        Return oMenuItem
    End Function

    Private Function MenuItem_ImportUsers() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "importar usuaris"
        oMenuItem.Enabled = _Surveys.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ImportUsers
        Return oMenuItem
    End Function

    Private Function MenuItem_MissingParticipants() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "participants que falten per contestar"
        oMenuItem.Enabled = _Surveys.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_MissingParticipants
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Enabled = _Surveys.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function

    Private Function MenuItem_ResetScores() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Posar a zero"
        AddHandler oMenuItem.Click, AddressOf Do_ResetScores
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
        Dim oFrm As New Frm_SurveyTree(_Survey)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Play(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oUser As DTOUser = DTOUser.WellKnown(DTOUser.WellKnowns.ZabalaHoyos)
        Dim oParticipant As DTOSurveyParticipant = BLLSurveyParticipant.FindOrNew(_Survey, oUser)
        Dim url As String = BLLSurveyParticipant.Url(oParticipant, True)
        UIHelper.ShowHtml(url)
    End Sub


    Private Sub Do_Edit(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Survey(_Survey)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ImportUsers()
        Dim oCsv As DTOCsv = Nothing
        Dim exs As New List(Of Exception)
        If UIHelper.LoadCsvDialog(oCsv, "fitxer de participants a la enquesta") Then
            Dim iCount As Integer = oCsv.Rows.Count - 1
            For i As Integer = 0 To iCount
                If Not GuidHelper.IsGuid(oCsv.Rows(i).Cells(0)) Then
                    If i = oCsv.Rows.Count - 1 Then 'obviem l'error de la ultima fila quan está en blanc
                        iCount -= 1
                    Else
                        exs.Add(New Exception(String.Format("fila {0} columna 1: '{1}' no es un identificador de usuari valid", i, oCsv.Rows(i).Cells(0))))
                        If exs.Count > 10 Then Exit Sub
                    End If
                End If
            Next


            If exs.Count = 0 Then
                Dim BlCancel As Boolean
                Dim oFrm As New Frm_Progress("Importació de participants a enquesta", "Importació de participants a enquesta")
                oFrm.Show()

                Dim idx As Integer
                For i As Integer = 0 To iCount
                    Dim userGuid As New Guid(oCsv.Rows(i).Cells.First)
                    Dim oUser As New DTOUser(userGuid)
                    Dim oParticipant As DTOSurveyParticipant = BLLSurveyParticipant.FindOrNew(_Survey, oUser)
                    If Not BLLSurveyParticipant.Update(oParticipant, exs) Then
                        UIHelper.WarnError(exs)
                    End If

                    oFrm.ShowProgress(0, oCsv.Rows.Count - 1, idx, "desant participants", BlCancel)
                    If BlCancel Then Exit For
                    idx += 1
                Next

                MsgBox(String.Format("{0:N0} participants importats satisfactoriament", iCount + 1), MsgBoxStyle.Information, "Mat.NET")
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Do_MissingParticipants()
        Dim oCsv As DTOCsv = BLLSurvey.MissingParticipants(_Survey)
        UIHelper.SaveCsvDialog(oCsv, "Llistat de participants que falten per contestar")
    End Sub


    Private Sub Do_Excel()
        Dim oBook As DTOExcelBook = BLLSurvey.Excel(_Survey)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oBook, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ResetScores()
        Dim rc As MsgBoxResult = MsgBox("Atenció: s'esborraran les respostes i es mantindran els usuaris", MsgBoxStyle.OkCancel, "Mat.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLLSurvey.ResetScores(_Survey, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLSurvey.Delete(_Surveys.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

