Public Class Frm_Amortitzations
    Private Async Sub Frm_Amortitzations_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oExercici As DTOExercici = DTOExercici.Past(Current.Session.Emp)
        Await RefrescaMenu(oExercici)
        Await refrescaGrid(oExercici)
        Cursor = Cursors.Default
    End Function

    Private Async Function RefrescaMenu(oExercici As DTOExercici) As Task
        Dim exs As New List(Of Exception)

        Dim oImmobilitzat = Await FEB.Amortizations.All(exs, oExercici.Emp)
        If exs.Count = 0 Then
            Dim oAllAmortitzacions = oImmobilitzat.Where(Function(x) x.Items.Count > 0).ToList
            Dim DtFch As New Date(oExercici.Year, 12, 31)
            Dim oLastAmortitzacions = oAllAmortitzacions.SelectMany(Function(x) x.Items).Where(Function(y) y.Cod = DTOAmortizationItem.Cods.Amortitzacio And y.Fch = DtFch).ToList
            Dim oPendentsDeAmortitzarItemsLess = Await FEB.Amortizations.Pendents(exs, oExercici)
            Dim oPendentsDeAmortitzar = oImmobilitzat.Where(Function(x) oPendentsDeAmortitzarItemsLess.Any(Function(y) y.Guid.Equals(x.Guid))).ToList
            ' Dim oPendentsDeAmortitzar = oAllAmortitzacions.Where(Function(x) oPendentsDeAmortitzarItemsLess.Any(Function(y) y.Guid.Equals(x.Guid))).ToList


            If exs.Count = 0 Then
                If oLastAmortitzacions.Count = 0 Then
                    RetrocedirAmortitzacióToolStripMenuItem.Text = "Retrocedir amortitzacions " & oExercici.Year
                Else
                    RetrocedirAmortitzacióToolStripMenuItem.Text = "Retrocedir " & oLastAmortitzacions.Count & " amortitzacions de " & oExercici.Year
                End If

                If oPendentsDeAmortitzar.Count = 0 Then
                    AmortitzarToolStripMenuItem.Text = "Amortizar " & oExercici.Year
                Else
                    AmortitzarToolStripMenuItem.Text = "Amortizar " & oExercici.Year & ": " & oPendentsDeAmortitzar.Count & " partides"
                End If

                AmortitzarToolStripMenuItem.Enabled = (oPendentsDeAmortitzar.Count > 0)
                RetrocedirAmortitzacióToolStripMenuItem.Enabled = (oLastAmortitzacions.Count > 0)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Async Function refrescaGrid(oExercici As DTOExercici) As Task
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = oExercici.LastFch
        ProgressBar1.Visible = True
        Dim oAll = Await FEB.Amortizations.All(exs, Current.Session.Emp)
        ProgressBar1.Visible = False
        Dim oSumasySaldos = Await FEB.SumasYSaldos.Summary(exs, Current.Session.Emp, DtFch)
        If exs.Count = 0 Then
            Xl_Amortizations1.Load(oAll, oSumasySaldos)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Amortizations1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Amortizations1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub AmortitzarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmortitzarToolStripMenuItem.Click
        ProgressBar1.Visible = True

        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.Past(Current.Session.Emp)
        If Await FEB.Amortizations.Amortitza(exs, Current.Session.User, oExercici) Then
            Await refresca()
            MsgBox("Amortitzat any " & oExercici.Year, MsgBoxStyle.Information)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            Await refresca()
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub RetrocedirAmortitzacióToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RetrocedirAmortitzacióToolStripMenuItem.Click
        ProgressBar1.Visible = True

        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.Past(Current.Session.Emp)
        If Await FEB.Amortizations.RevertAmortitzacions(exs, oExercici) Then
            Await refresca()
            MsgBox("Retrocedides les amortitzacions de l'any " & oExercici.Year, MsgBoxStyle.Information)
            ProgressBar1.Visible = False
        Else
            Await refresca()
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Amortizations1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Amortizations1.RequestToAddNew
        Dim value As New DTOAmortization()
        value.Cta = e.Argument

        Dim oFrm As New Frm_Amortization(value)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class