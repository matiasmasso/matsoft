Public Class Frm_Balances
    Private _Allowevents As Boolean

    Private Async Sub Frm_Balances_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetCurrentFch()
        Await refresca()
        _Allowevents = True
    End Sub


    Private Async Function refresca() As Task
        If _Allowevents Then Disable()

        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Dim oTree = Await FEB.Balance.Tree(Current.Session.Emp, CurrentFch)
        Dim oLang As DTOLang = CurrentLang()

        Dim oResultados As DTOPgcClass = DTOPgcClass.RecursiveSearch(oTree, DTOPgcClass.Cods.aBA17_Resultado_del_ejercicio)
        Dim oExplotacio As DTOPgcClass = DTOPgcClass.RecursiveSearch(oTree, DTOPgcClass.Cods.b_Cuenta_Explotacion)
        If oExplotacio IsNot Nothing Then
            Xl_BalanceExplotacio.Load(oExplotacio, oLang)
        End If

        Dim oPassiu As DTOPgcClass = DTOPgcClass.RecursiveSearch(oTree, DTOPgcClass.Cods.aB_Pasivo)
        FEB.Balance.InsertResultadosIntoPassiu(oPassiu, oResultados)
        Xl_BalancePassiu.Load(oPassiu, oLang)

        Dim oActiu As DTOPgcClass = DTOPgcClass.RecursiveSearch(oTree, DTOPgcClass.Cods.aA_Activo)
        Xl_BalanceActiu.Load(oActiu, oLang)

        Enable()
        Cursor = Cursors.Default
    End Function


    Private Async Sub Xl_DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles Xl_DateTimePicker1.ValueChanged
        If _Allowevents Then
            ButtonRefresh.Enabled = True
            Disable()
            Dim exs As New List(Of Exception)
            Dim sFch As String = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.LastBalanceQuadrat, exs)
            If exs.Count = 0 Then
                If IsNumeric(sFch) Then
                    PictureBoxOk.Visible = Xl_DateTimePicker1.Value = Date.FromOADate(sFch)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        UIHelper.SaveSetting(DTOSession.Settings.Last_Balance_Fch, Xl_DateTimePicker1.Value.ToOADate)
        Await refresca()
        ButtonRefresh.Enabled = False
    End Sub

    Private Function CurrentLang() As DTOLang
        Dim retval As DTOLang = Current.Session.Lang
        Return retval
    End Function

    Private Function CurrentFch() As Date
        Dim retval As Date = Xl_DateTimePicker1.Value
        Return retval
    End Function

    Private Function CurrentExercici() As DTOExercici
        Dim DtFch As Date = CurrentFch()
        Dim retval As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, DtFch.Year)
        Return retval
    End Function

    Private Sub SetCurrentFch()
        Dim DtFch As Date = DTO.GlobalVariables.Today()
        Dim sFch As String = UIHelper.GetSetting(DTOSession.Settings.Last_Balance_Fch)
        If IsNumeric(sFch) Then
            Try
                DtFch = Date.FromOADate(sFch)
                If DtFch > DTO.GlobalVariables.Today() Then DtFch = DTO.GlobalVariables.Today()
            Catch ex As Exception
                DtFch = DTO.GlobalVariables.Today()
                UIHelper.SaveSetting(DTOSession.Settings.Last_Balance_Fch, DtFch.ToOADate)
            End Try
        Else
            UIHelper.SaveSetting(DTOSession.Settings.Last_Balance_Fch, DtFch.ToOADate)
        End If
        Xl_DateTimePicker1.Value = DtFch
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Do_Excel()
    End Sub

    Private Async Sub Xl_BalanceActiu_RequestToRefresh(sender As Object, e As MatEventArgs) Handles _
        Xl_BalanceActiu.RequestToRefresh,
         Xl_BalancePassiu.RequestToRefresh,
          Xl_BalanceExplotacio.RequestToRefresh

        Await refresca()
    End Sub

    Private Sub Xl_BalanceActiu_RequestAnExcel(sender As Object, e As MatEventArgs) Handles _
        Xl_BalanceActiu.RequestAnExcel,
         Xl_BalancePassiu.RequestAnExcel,
          Xl_BalanceExplotacio.RequestAnExcel
        Do_Excel()
    End Sub

    Private Async Sub Do_Excel()
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = CurrentFch()
        Dim oBook As MatHelper.Excel.Book = Await FEB.Balance.Excel(Current.Session.Emp, DtFch)
        If Not UIHelper.ShowExcel(oBook, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_BalanceActiu_RequestAnExtracte(sender As Object, e As MatEventArgs) Handles _
        Xl_BalanceActiu.RequestAnExtracte,
         Xl_BalancePassiu.RequestAnExtracte,
          Xl_BalanceExplotacio.RequestAnExtracte

        Dim oCta As DTOPgcCta = e.Argument
        Dim oFrm As New Frm_Extracte(Nothing, oCta, CurrentExercici)
        oFrm.Show()
    End Sub

    Private Async Sub TancarBalançToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TancarBalançToolStripMenuItem.Click
        Dim rc As MsgBoxResult = MsgBox("Aquesta operació determina el balanç al que accedeixen els bancs per la web." & vbCrLf & "Es recomana cuadrar-lo i revisar-lo abans de acceptar", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.LastBalanceQuadrat, Xl_DateTimePicker1.Value.ToOADate, exs) Then
                MsgBox("Nova data darrer balanç tancat: " & Xl_DateTimePicker1.Value.ToShortDateString, MsgBoxStyle.Information, "Mat.NET")
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub UltimBalançTancatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UltimBalançTancatToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim sFch As String = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.LastBalanceQuadrat, exs)
        If exs.Count = 0 Then
            If IsNumeric(sFch) Then
                Dim DtFch As Date = Date.FromOADate(sFch)
                Xl_DateTimePicker1.Value = DtFch
                UIHelper.SaveSetting(DTOSession.Settings.Last_Balance_Fch, DtFch.ToOADate)
                Await refresca()
            Else
                UIHelper.WarnError("No hi ha cap balanç tancat")
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub WebToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebToolStripMenuItem.Click
        Dim sUrl As String = FEB.Balance.Url(Xl_DateTimePicker1.Value, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Disable()
        Xl_BalanceActiu.Disable()
        Xl_BalancePassiu.Disable()
        Xl_BalanceExplotacio.Disable()
    End Sub

    Private Sub Enable()
        Xl_BalanceActiu.Enable()
        Xl_BalancePassiu.Enable()
        Xl_BalanceExplotacio.Enable()
    End Sub
End Class