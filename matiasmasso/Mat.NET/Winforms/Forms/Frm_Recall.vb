Public Class Frm_Recall

    Private _Recall As DTORecall
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTORecall)
        MyBase.New()
        Me.InitializeComponent()
        _Recall = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Recall.Load(exs, _Recall) Then
            refresca()

            With _Recall
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged,
         DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Recall
            .Nom = TextBox1.Text
            .Fch = DateTimePicker1.Value
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Recall.Update(exs, _Recall) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Recall))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Recall.Delete(exs, _Recall) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Recall))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = DTORecall.Excel(_Recall)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        _Recall.IsLoaded = False
        Dim exs As New List(Of Exception)
        If FEB2.Recall.Load(exs, _Recall) Then
            With _Recall
                TextBox1.Text = .Nom
                DateTimePicker1.Value = .Fch
                Xl_RecallClis1.Load(.Clis)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_RecallClis1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RecallClis1.RequestToRefresh
        refresca()
    End Sub

    Private Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_RecallClis1.Filter = e.Argument
    End Sub
End Class


